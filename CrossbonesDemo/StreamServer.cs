using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CrossbonesDemo
{
	abstract class StreamServer
	{
		static List<StreamSender> lStreamSenders;
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
				lStreamSenders = new List<StreamSender>();

				tcListen = new TcpListener(IPAddress.Any, 9090);
				tcListen.Start();

				while (true)
				{
					lStreamSenders.Add(new StreamSender(tcListen.AcceptTcpClient()));
				}
			}
			catch (ThreadAbortException)
			{
				if (lStreamSenders != null)
				{
					StreamSender[] aStreamers = (StreamSender[])lStreamSenders.ToArray();
					for (int i = 0; i < aStreamers.Length; i++)
					{
						if (aStreamers[i] != null)
							aStreamers[i].Shutdown();
					}
				}
			}
		}

		public static void Shutdown()
		{
			if (thListen != null)
				thListen.Abort();

			if (lStreamSenders != null)
			{
				StreamSender[] aStreamSenders = (StreamSender[])lStreamSenders.ToArray();
				for (int i = 0; i < aStreamSenders.Length; i++)
				{
					if (aStreamSenders[i] != null)
						aStreamSenders[i].Shutdown();
				}
			}

			if (tcListen != null)
				tcListen.Stop();
		}
	}
}
