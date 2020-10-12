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
	class BeaconSender
	{
		string sServerAddress;

		//---CTOR
		public void Connect(string sServerAddress)
		{
			this.sServerAddress = sServerAddress;
			Thread thConnect = new Thread(new ParameterizedThreadStart(Join));
			thConnect.IsBackground=true;
			thConnect.Start(sServerAddress);
		}

		//==================================================
		//==========PUBLIC METHODS==========================
		//==================================================

		public void Disconnect()
		{
			TcpClient tcBeacon = new TcpClient();

			try
			{
				//TCP socket helper classes once again
				tcBeacon.Connect(sServerAddress, 9051);

				NetworkStream nsStream = tcBeacon.GetStream();
				StreamWriter swOut = new StreamWriter(nsStream);
				swOut.AutoFlush = true;

				swOut.WriteLine("Disconnect");
				swOut.Close();

				tcBeacon.Close();
				TriggerDisconnectEvent();
			}
			catch (Exception)
			{	}
		}

		private void Join(object o_ServerAddress)
		{
			string sServerAddress = (string)o_ServerAddress;
			TcpClient tcBeacon = new TcpClient();
			
			try
			{
				tcBeacon.Connect(IPAddress.Parse(sServerAddress), 9051);

				NetworkStream nsBeacon = tcBeacon.GetStream();
				StreamWriter swBeacon = new StreamWriter(nsBeacon);

				swBeacon.WriteLine("Connect");
				swBeacon.Flush();
				swBeacon.Close();

				TriggerConnectEvent();
			}
			catch (Exception)
			{
				if (tcBeacon != null)
					tcBeacon.Close();
				TriggerConnectFailEvent();
			}
			tcBeacon.Close();
		}

		//==================================================
		//==========EVENTING================================
		//==================================================

		//---connected
		public event ConnectHandler myConnectEvent;

		public delegate void ConnectHandler(object sender, EventArgs e);

		protected virtual void TriggerConnectEvent()
		{
			myConnectEvent(this, new EventArgs());
		}

		//---connection failed
		public event ConnectFailHandler myConnectFailEvent;

		public delegate void ConnectFailHandler(object sender, EventArgs e);

		protected virtual void TriggerConnectFailEvent()
		{
			myConnectFailEvent(this, new EventArgs());
		}

		//---disconnect
		public event DisconnectHandler myDisconnectEvent;

		public delegate void DisconnectHandler(object sender, EventArgs e);

		protected virtual void TriggerDisconnectEvent()
		{
			myDisconnectEvent(this, new EventArgs());
		}
	}
}
