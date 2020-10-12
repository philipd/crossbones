using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CrossbonesDemo
{
	public class DownloadReceiver
	{
		Thread thDownload;
        string sFileName;

		TcpListener tcListen;
        TcpClient tcManage;
        NetworkStream nsManage;
        StreamWriter swManage;
        StreamReader srManage;
        string sInput;

		IPAddress ipaRemotePeerAddress;

        /*
        public DownloadReceiver(string sFileName)
        {
            this.sFileName = sFileName;
        }
         * */

        public void Connect(string sRemotePeerAddressAndFilePath)
        {
            tcManage = new TcpClient();
			ipaRemotePeerAddress = IPAddress.Parse(sRemotePeerAddressAndFilePath.Split(new char[] { '|' })[0]);
			string sFilenamePath = sRemotePeerAddressAndFilePath.Split(new char[] { '|' })[1];

			string[] sTemp = sFilenamePath.Split(new char[] {'\\'});
			this.sFileName = sTemp[sTemp.Length-1];

            try
            {
                tcManage.Connect(ipaRemotePeerAddress, 9103);

                nsManage = tcManage.GetStream();
                swManage = new StreamWriter(nsManage);
                swManage.AutoFlush = true;
                srManage = new StreamReader(nsManage);

                swManage.WriteLine("ping");
                sInput = srManage.ReadLine();

				if (sInput == "pingreply")
				{
					////TriggerConnectEvent(new ConnectEventArgs());
					swManage.WriteLine(sFilenamePath);
					sInput=srManage.ReadLine();
					TriggerConnectEvent(new ConnectEventArgs(sFilenamePath, sInput));
				}
				else
					throw new Exception();
            }
            catch (Exception e)
            {
				string s = e.Message;
                TriggerDisconnectEvent(new DisconnectEventArgs());
            }
		}

		public void StartDownload()
		{
			swManage.WriteLine("start");
			thDownload = new Thread(new ThreadStart(Download));
			thDownload.IsBackground = true;
			thDownload.Start();
		}

		public void StopDownload()
		{
			if(thDownload!=null && thDownload.ThreadState==ThreadState.Running)
				thDownload.Abort();
		}

		public void Disconnect()
		{
			StopDownload();
			try
			{
				swManage.WriteLine("shutdown");
				tcManage.Close();
				TriggerDisconnectEvent(new DisconnectEventArgs());
			}
			catch (ObjectDisposedException) { }
		}

		//=========HELPER METHODS===========

		private void Download()
		{
			TcpClient tcDownload=null;
			FileStream fs=null;
			BinaryWriter bw=null;
			string sFile=null;

			try
			{
				TriggerDownloadStartedEvent(new DownloadStartedEventArgs());
				tcDownload = new TcpClient();

				//swap
				tcListen = new TcpListener(IPAddress.Any, 9105);
				tcListen.Start();
				tcDownload = tcListen.AcceptTcpClient();
				tcListen.Stop();
				//swap

				sFile=Form1.sShareFolder + "\\" + sFileName + "new";
				fs = new FileStream(sFile, FileMode.Create, FileAccess.Write);
				bw = new BinaryWriter(fs);

				int iRecv = 0;
				byte[] bBuffer = new byte[10];
				int iReceivedThisPass = 0;

				do
				{
					iReceivedThisPass = 0;
					// change the condition value to make downloads faster or slower
					for (int i = 0; i < 1000; i++)
					{
						//Number of bytes received from buffer
						//number of data received from client
						iRecv = tcDownload.Client.Receive(bBuffer);

						// this is necessary or else the last data received will be duplicated
						if (iRecv == 0)
							break;

						//Total up number of bytes received
						iReceivedThisPass += iRecv;

						foreach (byte byWrite in bBuffer)
						{
							bw.Write(byWrite);
						}
					}

					TriggerReceiveEvent(new ReceiveEventArgs(iReceivedThisPass));
				}
				while (iRecv > 0);

				bw.Close();
				fs.Close();
			}
			// FOR SOME REASON, THIS IS NEVER CAUGHT???
			catch (ThreadAbortException)
			{
				if(tcDownload!=null)
					tcDownload.Close();
				if (bw != null)
					bw.Close();
				if (fs != null)
				{
					fs.Close();
					fs.Dispose();
					File.Delete(sFile);
				}
			}
		}

		//----------eventing------------

		//--disconnect
		public event DisconnectHandler myDisconnectEvent;

		protected virtual void TriggerDisconnectEvent(DisconnectEventArgs e)
		{
			myDisconnectEvent(this, e);
		}

		public delegate void DisconnectHandler(object sender, DisconnectEventArgs e);

		public class DisconnectEventArgs : EventArgs
		{
		}

		//--download started
		public event DownloadStartedHandler myDownloadStartedEvent;

		protected virtual void TriggerDownloadStartedEvent(DownloadStartedEventArgs e)
		{
			myDownloadStartedEvent(this, e);
		}

		public delegate void DownloadStartedHandler(object sender, DownloadStartedEventArgs e);

		public class DownloadStartedEventArgs : EventArgs
		{
		}

		//--receive
		public event ReceiveHandler myReceiveEvent;

		protected virtual void TriggerReceiveEvent(ReceiveEventArgs e)
		{
			myReceiveEvent(this, e);
		}

		public delegate void ReceiveHandler(object sender, ReceiveEventArgs e);

		public class ReceiveEventArgs : EventArgs
		{
			private int iRecv;

			public int Received
			{
				get { return iRecv; }
				set { iRecv = value; }
			}

			public ReceiveEventArgs(int iRecv)
			{
				this.iRecv = iRecv;
			}
		}

		//--connect

		public event ConnectHandler myConnectEvent;

		protected virtual void TriggerConnectEvent(ConnectEventArgs e)
		{
			myConnectEvent(this, e);
		}

		public delegate void ConnectHandler(object sender, ConnectEventArgs e);

		public class ConnectEventArgs : EventArgs
		{
			private string sFilesize;
			private string sFilename;

			public string Filesize
			{
				get { return sFilesize; }
				set { sFilesize = value; }
			}

			public string Filename
			{
				get { return sFilename; }
				set { sFilename = value; }
			}

			public ConnectEventArgs(string sFilename,string sFilesize)
			{
				this.sFilesize = sFilesize;
				this.sFilename = sFilename;
			}
		}
	}
}
