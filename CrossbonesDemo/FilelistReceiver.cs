using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPAddressList;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace CrossbonesDemo
{
    class FilelistReceiver
    {
        Thread thDoSearch;
        const bool SEARCH_LOCAL = true;

        public void Search(string sSearchString)
        {
            thDoSearch = new Thread(new ParameterizedThreadStart(DoSearch));
            thDoSearch.IsBackground = true;
            thDoSearch.Start(sSearchString);
        }

        private void DoSearch(object o_sSearchString)
        {
            string sSearchString = (string)o_sSearchString;

            //

            byte[] byGetAddressClass = null;
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(Form1.sServerAddress), 9052);
            UdpClient udpcServer = new UdpClient();
            udpcServer.Connect(ipepServer);
            udpcServer.Send(BitConverter.GetBytes(53), sizeof(int));
            byGetAddressClass = udpcServer.Receive(ref ipepServer);
            MemoryStream msServer = new MemoryStream(byGetAddressClass);
            BinaryFormatter binfmtServer = new BinaryFormatter();
            ServerIPAddressList PeerAddressList = (ServerIPAddressList)binfmtServer.Deserialize(msServer);
            msServer.Close();
            udpcServer.Close();

            //

            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            //

            bool bTimeOut = false;
            for (int a = 0; a < PeerAddressList.m_IPAddressPeerList.Count; a++)
            {
                try
                {
                    if (SEARCH_LOCAL || (PeerAddressList.m_IPAddressPeerList[a].ToString() != localIP && PeerAddressList.m_IPAddressPeerList[a].ToString() != "127.0.0.1"))
                    {

                        TcpClient tcClient = new TcpClient(PeerAddressList.m_IPAddressPeerList[a].ToString(), 9102);
                        NetworkStream nsStream = tcClient.GetStream();
                        StreamWriter swOut = new StreamWriter(nsStream);
                        StreamReader srIn = new StreamReader(nsStream);
                        byte[] byDataDownloadSearch = new byte[50];

                        swOut.WriteLine(sSearchString);
                        swOut.Flush();

                        try
                        {
                            //int iRecv = nsStream.Read(byDataDownloadSearch, 0, byDataDownloadSearch.Length);
                        }

                        catch (SocketException)
                        {
                            bTimeOut = true;
                        }

                        if (bTimeOut == false)
                        {
                            /*
                            MemoryStream msPeer = new MemoryStream(byDataDownloadSearch);
                            BinaryFormatter binfmtPeer = new BinaryFormatter();

                            SearchResults = (SearchResult)binfmtPeer.Deserialize(msPeer);
                            */

                            string sSearchResults = srIn.ReadLine();


							sSearchResults = tcClient.Client.RemoteEndPoint.ToString() + "|" + sSearchResults;
							TriggerSearchFinished(new SearchFinishedEventArgs(sSearchResults));

                            /*
                            //MOVE TO THE FORM
                            //Display Search Results to Listbox
                            foreach (string i in SearchResults.sFileNamesSend)
                            {
                                //The search results will have the filenames with their full
                                //pathname, so it should be split before displayed in the listbox
                                string sFileName = null;
                                string[] sPathFile = null;

                                sFileName = i;
                                sPathFile = sFileName.Split('\\');
                            }
                            msPeer.Close();
                            */
                        }
                        swOut.Close();
                        nsStream.Close();
                        tcClient.Close();
                    }
                }
                catch (Exception se)
                {
                }
            }
        }

        //---------search finished event---

        public event SearchFinishedHandler mySearchFinishedEvent;

        protected virtual void TriggerSearchFinished(SearchFinishedEventArgs e)
        {
            mySearchFinishedEvent(this, e);
        }

        public delegate void SearchFinishedHandler(object sender, SearchFinishedEventArgs e);

        public class SearchFinishedEventArgs : EventArgs
        {
            public string sSearchResults;

            public SearchFinishedEventArgs(string sSearchResults)
            {
                this.sSearchResults = sSearchResults;
            }
        }
    }
}