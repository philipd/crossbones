using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace CrossbonesDemo
{
    //uses port 9102
    class FilelistSender
    {
        TcpClient tcFilelist;
        NetworkStream nsFilelist;
        StreamReader srFilelist;
        StreamWriter swFilelist;
        Thread thSend;
        byte[] byDataUploadSearch = new byte[1500];

        public FilelistSender(TcpClient tcFilelist)
        {
            this.tcFilelist = tcFilelist;
            nsFilelist = this.tcFilelist.GetStream();
            srFilelist = new StreamReader(nsFilelist);
            swFilelist = new StreamWriter(nsFilelist);

            thSend = new Thread(new ThreadStart(Send));
            thSend.IsBackground = true;
            thSend.Start();
        }

        private void Send()
        {
            //Create a new SearchResult class to send the results of the search to the client
            SearchResult SearchResultToClient = new SearchResult();

            //Convert the byte format of the search request into a string
            string sSearchFieldRequest = srFilelist.ReadLine() + "*";

            SearchResultToClient.sFileNamesSend = Directory.GetFiles(@Form1.sShareFolder, "*"+sSearchFieldRequest);	// without the star, this only searches at the beginning of filenames

            /*
            //Serialize and Send Class with the search results back to the client
            MemoryStream ms1 = new MemoryStream();
            BinaryFormatter bfinmt = new BinaryFormatter();
            bfinmt.Serialize(ms1, SearchResultToClient);

            byDataUploadSearch = ms1.GetBuffer();

            //Send class with search results back to client
            nsFilelist.Write(byDataUploadSearch, 0, byDataUploadSearch.Length);
             * */
            string sFilelist = "";
            foreach (string s in SearchResultToClient.sFileNamesSend)
            {
                sFilelist += s + "|";
            }

            swFilelist.WriteLine(sFilelist);
            swFilelist.Flush();
            
            //Close Socket
            srFilelist.Close();
            nsFilelist.Close();
            tcFilelist.Close();
        }

        public void Shutdown()
        {
        }
    }
}
