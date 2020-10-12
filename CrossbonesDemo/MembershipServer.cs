using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using IPAddressList;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CrossbonesDemo
{
	class MembershipServer
	{
		public static Thread thRegisterDeregisterUsers;     //This thread will listen for users that wish to join
		//or leave the p2p
		public static Thread thSendIPAddressList;           //Will send a list of addresses of online peers to a requesting client

		public static void Activate()
		{
            //Create class 
            ServerIPAddressList PeerAddressList = new ServerIPAddressList();
            //Declare and initialize List
            PeerAddressList.m_IPAddressPeerList = new List<IPAddress>();

            //Start Threads
            thRegisterDeregisterUsers = new Thread(new ParameterizedThreadStart(AddOrRemovePeer));
			thRegisterDeregisterUsers.IsBackground = true;
            thSendIPAddressList = new Thread(new ParameterizedThreadStart(IPAddressList));
			thSendIPAddressList.IsBackground = true;

            thRegisterDeregisterUsers.Start(PeerAddressList);
            thSendIPAddressList.Start(PeerAddressList);

		}

		public static void Deactivate()
		{
			thRegisterDeregisterUsers.Abort();
			thSendIPAddressList.Abort();
		}

		        //Method will wait for an incoming connection to register or remove a peer from the address list
        static void AddOrRemovePeer(object objData)
        {
            //unbox class
            ServerIPAddressList PeerAddressList = (ServerIPAddressList)objData;



            while (true)
            {
                //String variable to see if user wants to connect or disconnect
                string sConnectOrDisconnect = "";

                IPAddress ip = null;        //Ip Address of remote client

                int iIndex = 0;            //Int variable which hold the index value of the 
                //ip address to be removed from the list

                Console.WriteLine("Waiting for a connection");

                //Standard TCP Helper Class shit
                TcpListener tlListen = new TcpListener(IPAddress.Any, 9051);
                tlListen.Start(5);

                TcpClient tcClient = tlListen.AcceptTcpClient();
                NetworkStream ns = tcClient.GetStream();
                StreamReader sr = new StreamReader(ns);
                //Read clients answer, connect or disconnect
                sConnectOrDisconnect = sr.ReadLine();

                //Parse ip address of remote endpoint
                ip = IPAddress.Parse(((IPEndPoint)tcClient.Client.RemoteEndPoint)
                    .Address.ToString());

                //Compare strings if user sends connect, add user to peer 'cluster'
                //else remove the peer from the 'cluster'
                if (sConnectOrDisconnect == "Connect")
                {
                    if (!PeerAddressList.m_IPAddressPeerList.Contains(ip))
                    {
                        Console.WriteLine("{0} Joined the cluster", tcClient.Client.RemoteEndPoint);
                        //Add the clients ip address to the list
                        PeerAddressList.m_IPAddressPeerList.Add(ip);
                    }
                    else
                    {
                        Console.WriteLine("{0} already in the cluster!", tcClient.Client.RemoteEndPoint);
                    }
                }
                else if (sConnectOrDisconnect == "Disconnect")
                {
                    if (PeerAddressList.m_IPAddressPeerList.Contains(ip))
                    {
                        Console.WriteLine("{0} Disconnected from the cluster", tcClient.Client.RemoteEndPoint);

                        //Remove the clients ip address from the list

                        //Find the index of the ip address in the IPAddressList
                        iIndex = PeerAddressList.m_IPAddressPeerList.IndexOf(ip);

                        //Remove the ip address from the list
                        PeerAddressList.m_IPAddressPeerList.RemoveAt(iIndex);
                    }
                    else
                    {
                        Console.WriteLine("{0} not in the cluster!", tcClient.Client.RemoteEndPoint);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input from {0}", tcClient.Client.RemoteEndPoint);
                }
                

                //Close Sockets
                ns.Close();
                sr.Close();
                tcClient.Close();
                tlListen.Stop();




            }

        }

        //Will wait and answer for any requests for a list of addresses of online peers
        static void IPAddressList(object objData)
        {
            //unbox class
            ServerIPAddressList PeerAddressList = (ServerIPAddressList)objData;

            while (true)
            {
                byte[] byData = null;
                IPEndPoint ipepRemote = new IPEndPoint(IPAddress.Any, 0);

                byte[] byUploadPeerAddressList = null;      //byte array which houses the class of the ip addresses of peers

                try
                {   //Socket Helpers
                    UdpClient udpcClient = new UdpClient(9052);
                    byData = udpcClient.Receive(ref ipepRemote);

                    //Send Server IPAddressList Class which has a list of online peers to client
                    //Serialize
                    MemoryStream ms1 = new MemoryStream();
                    BinaryFormatter bfinmt = new BinaryFormatter();
                    bfinmt.Serialize(ms1, PeerAddressList);

                    byUploadPeerAddressList = ms1.GetBuffer();

                    udpcClient.Send(byUploadPeerAddressList, byUploadPeerAddressList.Length, ipepRemote);

                    ms1.Close();
                    udpcClient.Close();
                }


                catch (SocketException se)
                {
                    Console.WriteLine(se.Message);
                }
            }
        }
	}
}
