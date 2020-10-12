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
	class StreamReceiver
	{
		IWavePlayer waveOutDevice;
		WaveStream mainOutputStream;
		WaveChannel32 volumeStream;
		int iBufferSize;

		TcpClient tcManage;
		NetworkStream nsManage;
		StreamWriter swManage;
		StreamReader srManage;
		string sInput;

		Socket sktUDP;
		IPEndPoint ipepBind;

		bool bPlaying;

		public bool Playing
		{
			get { return bPlaying; }
			set { bPlaying = value; }
		}

		Thread thStream;

		object m_lock = new object();

		//public delegate void delVoidVoid();

		delegate void dDisconnect();
		dDisconnect myDelegate;

		//==================================================
		//==========PUBLIC METHODS==========================
		//==================================================

        //OLD SHIT
		public bool Connect(IPAddress ipaRemotePeerAddress)
		{
			tcManage = new TcpClient();

			try
			{
				tcManage.Connect(ipaRemotePeerAddress, 9090);

				nsManage = tcManage.GetStream();
				swManage = new StreamWriter(nsManage);
				swManage.AutoFlush = true;
				srManage = new StreamReader(nsManage);

				swManage.WriteLine("ping");
				sInput = srManage.ReadLine();

				if (sInput == "pingreply")
					TriggerConnectEvent(new ConnectEventArgs());
				else
					throw new Exception();
			}
			catch (Exception)
			{
				TriggerDisconnectEvent(new DisconnectEventArgs());
				return false;
			}
			return true;
		}

		public bool Connect(string sPeerAndFilename)
		{
			tcManage = new TcpClient();

			try
			{
                tcManage.Connect(IPAddress.Parse(sPeerAndFilename.Split(new char[] { '|' })[0]), 9090);

				nsManage = tcManage.GetStream();
				swManage = new StreamWriter(nsManage);
				swManage.AutoFlush = true;
				srManage = new StreamReader(nsManage);

				swManage.WriteLine("ping");
				sInput = srManage.ReadLine();

                if (sInput == "pingreply")
                {
                    TriggerConnectEvent(new ConnectEventArgs());
                    Play(sPeerAndFilename);
                }
                else
                    throw new Exception();
			}
			catch (Exception)
			{
				TriggerDisconnectEvent(new DisconnectEventArgs());
				return false;
			}
			return true;
		}

		public void Disconnect()
		{
			try
			{
				swManage.WriteLine("disconnect");
			}
			catch (IOException) { }
			catch (ObjectDisposedException) { }
			catch (NullReferenceException) { }

			CloseWaveOut();

			if (tcManage != null)
				tcManage.Close();
			if (sktUDP != null)
				sktUDP.Close();

			bPlaying = false;
			TriggerDisconnectEvent(new DisconnectEventArgs());
		}

		public void Play(string sPeerAndFilename)
		{
            sktUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			sktUDP.Blocking = false;
			ipepBind = new IPEndPoint(IPAddress.Any, 54321);
			sktUDP.Bind(ipepBind);

			try
			{
				swManage.WriteLine("start");
				swManage.Flush();
				swManage.WriteLine(sPeerAndFilename.Split(new char[]{'|'})[1]);
				swManage.Flush();
				sInput = srManage.ReadLine();

				if (sInput == "fail")
				{
					Stop();
					TriggerStopEvent(new StopEventArgs());
				}
				else
				{
					iBufferSize = Int32.Parse(sInput);

					thStream = new Thread(new ThreadStart(Stream));
					thStream.Name = "StreamThread";
					thStream.IsBackground = true;
					thStream.Start();

					bPlaying = true;
					TriggerPlayEvent(new PlayEventArgs());
				}
			}
			catch (IOException)
			{
				Disconnect();
			}
			catch (ObjectDisposedException)
			{
				Disconnect();
			}
			catch (Exception)
			{
				Disconnect();
			}
		}

		public void Stop()
		{
			try
			{
				swManage.WriteLine("stop");
				swManage.Flush();
			}
			catch (IOException)
			{ }
			catch (ObjectDisposedException)
			{ }
			catch (NullReferenceException)
			{ }
			CloseWaveOut();
			sktUDP.Close();

			bPlaying = false;
			TriggerStopEvent(new StopEventArgs());

			if (thStream != null)
				thStream.Abort();
		}

		//==================================================
		//==========HELPERS=================================
		//==================================================

		private void CloseWaveOut()
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
		}

		private void Stream()
		{
			int iSize = 1024;
			byte[] buffer = new byte[iSize];
			int iRecv;
			buffer = new byte[iBufferSize];

			IPEndPoint ipepRemote = new IPEndPoint(IPAddress.Any, 0);
			EndPoint epRemote = (EndPoint)ipepRemote;

			NAudio.Wave.BufferedSampleStream bufferedStream = new BufferedSampleStream();
			waveOutDevice = new NAudio.Wave.DirectSoundOut(100);
			waveOutDevice.Init(bufferedStream);
			waveOutDevice.Play();

			int iFails = 0;

			do
			{
				try
				{
					iRecv = sktUDP.ReceiveFrom(buffer, ref epRemote);
					iFails = 0;
					bufferedStream.AddSamples(buffer, 0, buffer.Length);
				}
				catch (SocketException)
				{
					iFails++;
					// For running with debugging, set this value much lower, e.g. 50
					if (iFails > (System.Diagnostics.Debugger.IsAttached ? 50 : 25000))
						break;

				}
				catch (ObjectDisposedException)
				{
					// I can't remember why I put this here
					/*
					iFails++;
					if (iFails > 25000)*/
					break;
				}
			} while (true);

			Disconnect();
			//myDelegate = new dDisconnect(Disconnect);            
		}

		//==================================================
		//==========EVENTING================================
		//==================================================

		//==========DISCONNECT=====

		public event DisconnectHandler myDisconnectEvent;

		protected virtual void TriggerDisconnectEvent(DisconnectEventArgs e)
		{
			myDisconnectEvent(this, e);
		}

		public delegate void DisconnectHandler(object sender, DisconnectEventArgs e);

		public class DisconnectEventArgs : EventArgs
		{
		}


		//==========CONNECT=====

		public event ConnectHandler myConnectEvent;

		protected virtual void TriggerConnectEvent(ConnectEventArgs e)
		{
			myConnectEvent(this, e);
		}

		public delegate void ConnectHandler(object sender, ConnectEventArgs e);

        
		public class ConnectEventArgs : EventArgs{}


		//==========PLAY=====

		public event PlayHandler myPlayEvent;

		protected virtual void TriggerPlayEvent(PlayEventArgs e)
		{
			myPlayEvent(this, e);
		}

		public delegate void PlayHandler(object sender, PlayEventArgs e);

		public class PlayEventArgs : EventArgs
		{
		}


		//==========STOP=====

		public event StopHandler myStopEvent;

		protected virtual void TriggerStopEvent(StopEventArgs e)
		{
			myStopEvent(this, e);
		}

		public delegate void StopHandler(object sender, StopEventArgs e);

		public class StopEventArgs : EventArgs
		{
		}
	}
}