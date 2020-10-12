using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace CrossbonesDemo
{
	class DownloadSender
	{
		TcpClient tcManage;
		TcpClient tcUpload;
		EndPoint epRemote;
		Thread thManage;
		Thread thUpload;
		int intSize;
		byte[] buffer;

		//CTOR
		public DownloadSender(TcpClient in_tcManage)
		{
			tcManage = in_tcManage;
			tcManage.LingerState = new LingerOption(true, 0);
			thManage = new Thread(new ThreadStart(Manage));
			thManage.IsBackground = true;
			thManage.Start();
		}

		public void Shutdown()
		{
			if (tcUpload != null)
				tcUpload.Close();
			if (tcManage != null)
				tcManage.Close();
			if (thManage != null)
				thManage.Abort();
			if (thUpload != null)
				thUpload.Abort();
		}

		public void Stop()
		{
			if (tcUpload != null)
				tcUpload.Close();
			if (thUpload != null)
				thUpload.Abort();
		}

		// Runs in separate thread, called from Manage
		private void Upload(object o_sRemotePeerAndFilePath)
		{
			string sInput = (string)o_sRemotePeerAndFilePath;
			IPAddress ipaRemotePeerAddress = IPAddress.Parse(sInput.Split(new char[] { '|' })[0]);
			string sFilenamePath = sInput.Split(new char[]{'|'})[1];

			long fileLength = 0;      //number of bytes in file
			string sFileName = "";         //Name of File

			int iSent = 0;                  //Number of bytes sent from buffer
			int iTotalBytesSent = 0;        //Total number of bytes sent
			byte[] bBuffer = new byte[0];


			FileStream fs = new FileStream(sFilenamePath,
				FileMode.Open, FileAccess.Read);

			BinaryReader br = new BinaryReader(fs);

			try
			{
				//swap
				//swap
				tcUpload = new TcpClient();
				tcUpload.Connect(ipaRemotePeerAddress, 9105);

				// send that bitch over
					do
					{
						try
						{
							//read 1200 bytes from file into buffer
							bBuffer = br.ReadBytes(1200);

							//Sends whats in buffer and iSent is given how many bytes sent from buffer
							iSent = tcUpload.Client.Send(bBuffer, 0, bBuffer.Length, SocketFlags.None);
						}
						catch (SocketException se)
						{ }
					}
					while (iSent > 0);
					tcUpload.Close();
			}
			catch (ThreadAbortException)
			{
				tcUpload.Close();
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
			string sFileNamePath="";

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
					if (sFileNamePath != "")
					{
						thUpload = new Thread(new ParameterizedThreadStart(Upload));
						thUpload.IsBackground = true;
						thUpload.Start(tcManage.Client.RemoteEndPoint.ToString().Split(new char[] { ':' })[0] + "|" + sFileNamePath);
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
				else if (sInput == "shutdown")
				{
					Shutdown();
				}
				else
				{
					sFileNamePath = sInput;

					//Get Length of File
					string sFileLength = "" + new FileInfo(sFileNamePath).Length;

					//Send length of file
					swManage.WriteLine(sFileLength);
				}
			}
		}
	}
}