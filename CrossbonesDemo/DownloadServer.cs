using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CrossbonesDemo
{
	abstract class DownloadServer
	{
		static List<DownloadSender> lDownloadSenders;
		static Thread thListen;
		static TcpListener tcListen;

		public static void Start()
		{
			thListen = new Thread(new ThreadStart(Listen));
			thListen.IsBackground = true;
			thListen.Start();
		}

		private static void Listen()
		{
			try
			{
				lDownloadSenders = new List<DownloadSender>();

				tcListen = new TcpListener(IPAddress.Any, 9103);
				tcListen.Start();

				while (true)
				{
					lDownloadSenders.Add(new DownloadSender(tcListen.AcceptTcpClient()));
				}
			}
			catch (ThreadAbortException)
			{
				if (lDownloadSenders != null)
				{
					DownloadSender[] aDownloadSenders = (DownloadSender[])lDownloadSenders.ToArray();
					for (int i = 0; i < aDownloadSenders.Length; i++)
					{
						if (aDownloadSenders[i] != null)
							aDownloadSenders[i].Shutdown();
					}
				}
			}
		}

		public static void Shutdown()
		{
			if (thListen != null)
				thListen.Abort();

			if (lDownloadSenders != null)
			{
				DownloadSender[] aDownloadSenders = (DownloadSender[])lDownloadSenders.ToArray();
				for (int i = 0; i < aDownloadSenders.Length; i++)
				{
					if (aDownloadSenders[i] != null)
						aDownloadSenders[i].Shutdown();
				}
			}

			if (tcListen != null)
				tcListen.Stop();
		}
	}
}
