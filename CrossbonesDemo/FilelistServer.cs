using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CrossbonesDemo
{
    //uses port 9102
    class FilelistServer
    {
		static List<FilelistSender> lFilelistSenders;
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
				lFilelistSenders = new List<FilelistSender>();

				tcListen = new TcpListener(IPAddress.Any, 9102);
				tcListen.Start();

				while (true)
				{
					lFilelistSenders.Add(new FilelistSender(tcListen.AcceptTcpClient()));
				}
			}
			catch (ThreadAbortException)
			{
				if (lFilelistSenders != null)
				{
					FilelistSender[] aFilelistSenders = (FilelistSender[])lFilelistSenders.ToArray();
					for (int i = 0; i < aFilelistSenders.Length; i++)
					{
						if (aFilelistSenders[i] != null)
							aFilelistSenders[i].Shutdown();
					}
				}
			}
		}

		public static void Shutdown()
		{
			if (thListen != null)
				thListen.Abort();

			if (lFilelistSenders != null)
			{
				FilelistSender[] aFilelistSenders = (FilelistSender[])lFilelistSenders.ToArray();
				for (int i = 0; i < aFilelistSenders.Length; i++)
				{
					if (aFilelistSenders[i] != null)
						aFilelistSenders[i].Shutdown();
				}
			}

			if (tcListen != null)
				tcListen.Stop();
		}
	}
}
