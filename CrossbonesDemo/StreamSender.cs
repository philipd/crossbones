using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using NAudio;
using NAudio.Wave;
using System.Runtime.InteropServices;
using System.Threading;

namespace CrossbonesDemo
{
	class StreamSender
	{
		IWavePlayer waveOutDevice;
		WaveStream mainOutputStream;
		WaveChannel32 volumeStream;
		TcpClient tcManage;
		Socket sktUDP;
		EndPoint epRemote;
		Thread thManage;
		Thread thStream;
		int intSize;
		byte[] buffer;
		WaveStream blockAlignedStream;

		//CTOR
		public StreamSender(TcpClient in_tcManage)
		{
			tcManage = in_tcManage;
			tcManage.LingerState = new LingerOption(true, 0);
			thManage = new Thread(new ThreadStart(Manage));
			thManage.IsBackground = true;
			thManage.Start();
		}

		private int GetBlockSize(NAudio.Wave.WaveFormat waveFormat, int intLatencyMS)
		{
			int bytes = intLatencyMS * (waveFormat.AverageBytesPerSecond / 1000);
			bytes -= bytes % waveFormat.BlockAlign;
			return bytes;
		}

		private void CloseWaveOut()
		{
			try
			{
				if (waveOutDevice != null)
				{
					waveOutDevice.Stop();
				}
				if (mainOutputStream != null)
				{
					// this one really closes the file and ACM conversion
					volumeStream.Close();
					volumeStream = null;
					// this one does the metering stream
					mainOutputStream.Close();
					mainOutputStream = null;
				}
				if (waveOutDevice != null)
				{
					waveOutDevice.Dispose();
					waveOutDevice = null;
				}
				if (blockAlignedStream != null)
				{
					blockAlignedStream.Dispose();
					blockAlignedStream = null;
				}
			}
			catch (Exception)
			{
			}
		}

		public void Shutdown()
		{
			CloseWaveOut();
			if (sktUDP != null)
				sktUDP.Close();
			if (tcManage != null)
				tcManage.Close();
			if (thManage != null)
				thManage.Abort();
			if (thStream != null)
				thStream.Abort();
		}

		public void Stop()
		{
			CloseWaveOut();
			if (sktUDP != null)
				sktUDP.Close();
			if (thStream != null)
				thStream.Abort();
		}

		// Runs in separate thread, called from Manage
		private void Stream()
		{
			try
			{
				// Hey, it's our good friends Socket and EndPoint
				sktUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

				////epRemote = (EndPoint)new IPEndPoint(IPAddress.Parse(tbxAddress.Text), 54321);
				epRemote = tcManage.Client.RemoteEndPoint;
				string epString = epRemote.ToString();
				IPAddress ipaRemote = IPAddress.Parse(epString.Split(new char[]{':'})[0]);
				epRemote = new IPEndPoint(ipaRemote, 54321);

				// send that bitch over
				while (true)
				{
					int intOffset = 0;
					int intRead = blockAlignedStream.Read(buffer, intOffset, intSize);
					if (intRead == 0)
					{
						break;
					}

					sktUDP.SendTo(buffer, epRemote);
					Thread.Sleep(100);
				}
			}
			catch (ThreadAbortException)
			{
				sktUDP.Close();
			}
			catch (NullReferenceException)
			{ }
		}

		// Runs in separate thread, called from ctor
		private void Manage()
		{
			//TcpClient tcManage = (TcpClient)o_tcManage;
			NetworkStream nsManage = tcManage.GetStream();
			StreamWriter swManage = new StreamWriter(nsManage);
			swManage.AutoFlush = true;
			StreamReader srManage = new StreamReader(nsManage);
			string sInput;

			while (true)
			{
				try
				{
					sInput = srManage.ReadLine();
				}
				catch (IOException)
				{
					sInput = "disconnect";
				}

				if (sInput == "start")
				{
					string fileName = srManage.ReadLine();
					if (File.Exists(fileName))				// make sure the file actually EXISTS
					{
						// should probably put a try-catch block here, with swManage.WriteLine("fail") in the catch block
						try
						{
							WaveStream mp3Reader = new Mp3FileReader(fileName);
							WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader);

							// convert to 441khz if necessary
							// this prevents chipmunking
							if (pcmStream.WaveFormat.SampleRate != 44100)
							{
								var format = new WaveFormat(44100,
								   16, pcmStream.WaveFormat.Channels);
								pcmStream = new WaveFormatConversionStream(format, pcmStream);
							}

							// I don't know what the hell this does, but it's probably important
							blockAlignedStream = new BlockAlignReductionStream(pcmStream);

							intSize = GetBlockSize(blockAlignedStream.WaveFormat, 200);
							buffer = new byte[intSize];

							// send size of the buffer
							// to co-ordinate buffer size in the client
							// this prevents the client's buffer from overrunning
							swManage.WriteLine(intSize);
							swManage.Flush();

						thStream = new Thread(new ThreadStart(Stream));
						thStream.IsBackground = true;
						thStream.Start();
						}
						catch (Exception) { swManage.WriteLine("fail"); }
					}
					else
					{
						swManage.WriteLine("fail");
						swManage.Flush();
					}
				}
				else if (sInput == "stop")
				{
					Stop();
				}
				else if (sInput == "disconnect")
				{
					Shutdown();
				}
				else if (sInput == "ping")
				{
					swManage.WriteLine("pingreply");
				}
			}
		}
	}
}