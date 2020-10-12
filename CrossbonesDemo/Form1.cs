using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPAddressList;

namespace CrossbonesDemo
{
	public partial class Form1 : Form
	{
		BeaconSender myBeaconSender;
		StreamReceiver myStreamReceiver;
		FilelistReceiver myFilelistReceiver;
		DownloadReceiver myDownloadReceiver;
		delegate void delVoidBool(bool b);
		delegate void delVoidString(string s);

		public static string sShareFolder { get; set; }
		public static string sServerAddress { get; set; }

		public Form1()
		{
			InitializeComponent();
			myBeaconSender = new BeaconSender();
			myStreamReceiver = new StreamReceiver();
			myFilelistReceiver = new FilelistReceiver();
			myDownloadReceiver = new DownloadReceiver();
			myBeaconSender.myConnectEvent += new BeaconSender.ConnectHandler(ConnectDetected);
			myBeaconSender.myConnectFailEvent += new BeaconSender.ConnectFailHandler(ConnectFailDetected);
			myBeaconSender.myDisconnectEvent += new BeaconSender.DisconnectHandler(DisconnectDetected);
			myFilelistReceiver.mySearchFinishedEvent += new FilelistReceiver.SearchFinishedHandler(SearchFinishedDetected);
			myStreamReceiver.myConnectEvent += new StreamReceiver.ConnectHandler(StreamConnectDetected);
			myStreamReceiver.myDisconnectEvent += new StreamReceiver.DisconnectHandler(StreamDisconnectDetected);
			myStreamReceiver.myPlayEvent += new StreamReceiver.PlayHandler(PlayDetected);
			myStreamReceiver.myStopEvent += new StreamReceiver.StopHandler(StopDetected);
			myDownloadReceiver.myConnectEvent += new DownloadReceiver.ConnectHandler(DownloadConnectDetected);
			myDownloadReceiver.myDisconnectEvent += new DownloadReceiver.DisconnectHandler(DownloadDisconnectDetected);


			FilelistServer.Start();
		}

		//-----------custom event handlers

		private void DownloadConnectDetected(object sender, DownloadReceiver.ConnectEventArgs e)
		{
			DownloadForm myDownloadForm = new DownloadForm(e.Filename, e.Filesize, myDownloadReceiver);
			Invoke(new delVoidBool(btnDownloadUpdate), false);
			myDownloadForm.Show();
		}

		private void DownloadDisconnectDetected(object sender, DownloadReceiver.DisconnectEventArgs e)
		{
			Invoke(new delVoidBool(btnDownloadUpdate), true);
		}

		private void PlayDetected(object sender, StreamReceiver.PlayEventArgs e)
		{
			Invoke(new delVoidBool(btnPlayUpdate), false);
			Invoke(new delVoidBool(btnStopUpdate), true);
		}

		private void StopDetected(object sender, StreamReceiver.StopEventArgs e)
		{
			Invoke(new delVoidBool(btnPlayUpdate), true);
			Invoke(new delVoidBool(btnStopUpdate), false);
		}

		private void StreamConnectDetected(object sender, EventArgs e)
		{
		}

		private void StreamDisconnectDetected(object sender, EventArgs e)
		{
			Invoke(new delVoidBool(btnStopUpdate), false);
			Invoke(new delVoidBool(btnPlayUpdate), true);
		}

		private void ConnectDetected(object sender, EventArgs e)
		{
			Invoke(new delVoidBool(btnConnectUpdate), false);
			Invoke(new delVoidBool(btnDisconnectUpdate), true);
			Invoke(new delVoidBool(gbxLocalSetupUpdate), true);
			Form1.sServerAddress = tbxServerAddress.Text;
		}

		private void ConnectFailDetected(object sender, EventArgs e)
		{
			Invoke(new delVoidBool(btnConnectUpdate), true);
			Invoke(new delVoidBool(tbxServerAddressUpdate), true);
			MessageBox.Show("Failed to connected to server at the address provided");
		}

		private void DisconnectDetected(object sender, EventArgs e)
		{
			Invoke(new delVoidBool(btnConnectUpdate), true);
			Invoke(new delVoidBool(btnDisconnectUpdate), false);
			Invoke(new delVoidBool(gbxLocalSetupUpdate), false);
			Invoke(new delVoidBool(tbxServerAddressUpdate), true);
			Invoke(new delVoidBool(gbxStreamServerUpdate), false);
			Invoke(new delVoidBool(gbxFileServerUpdate), false);
			Invoke(new delVoidBool(gbxSearchUpdate), false);
			Invoke(new delVoidBool(btnStreamServerActivateUpdate), true);
			Invoke(new delVoidBool(btnStreamServerDeactivateUpdate), false);
			Invoke(new delVoidString(tbxShareFolderUpdate), "");
			StreamServer.Shutdown();
			myStreamReceiver.Stop();
			//also; stop audio
			//also; disable client gbx
		}

		private void SearchFinishedDetected(object sender, FilelistReceiver.SearchFinishedEventArgs e)
		{
			//Invoke(new delVoidString(lbxSearchResultsUpdate), e.sSearchResults);
			Invoke(new delVoidString(lvwSearchResultsUpdate), e.sSearchResults);
		}

		//-------------stuff for cross-threads----

		private void tbxShareFolderUpdate(string sText)
		{
			tbxShareFolder.Text = sText;
		}

		private void lvwSearchResultsUpdate(string sResults)
		{
			string[] a_sResults = sResults.Split(new char[] { '|' });

			for (int i = 1; i < a_sResults.Length; i++)
			{
				if (a_sResults[i] != "")        //remove null entries from listbox
				{
					lvwSearchResults.Items.Add(new ListViewItem(new string[] { a_sResults[0].Split(new char[] { ':' })[0], a_sResults[i] }));
				}
			}
		}

		private void btnConnectUpdate(bool bEnable)
		{
			btnConnect.Enabled = bEnable;
		}

		private void btnDownloadUpdate(bool bEnable)
		{
			btnDownload.Enabled = bEnable;
		}

		private void btnDisconnectUpdate(bool bEnable)
		{
			btnDisconnect.Enabled = bEnable;
		}

		private void gbxLocalSetupUpdate(bool bEnable)
		{
			gbxLocalSetup.Enabled = bEnable;
		}

		private void tbxServerAddressUpdate(bool bEnable)
		{
			tbxServerAddress.Enabled = bEnable;
		}

		private void gbxStreamServerUpdate(bool bEnable)
		{
			gbxStreamServer.Enabled = bEnable;
		}

		private void gbxFileServerUpdate(bool bEnable)
		{
			gbxFileServer.Enabled = bEnable;
		}

		private void btnStreamServerActivateUpdate(bool bEnable)
		{
			btnStreamServerActivate.Enabled = bEnable;
		}

		private void btnStreamServerDeactivateUpdate(bool bEnable)
		{
			btnStreamServerDeactivate.Enabled = bEnable;
		}
		private void btnPlayUpdate(bool bEnable)
		{
			if (lvwSearchResults.SelectedIndices.Count != 0)
				btnPlay.Enabled = bEnable;
		}
		private void btnStopUpdate(bool bEnable)
		{
			btnStop.Enabled = bEnable;
		}

		private void gbxSearchUpdate(bool bEnable)
		{
			gbxSearch.Enabled = bEnable;
			tbxSearchQuery.Text = "";
			lvwSearchResults.Items.Clear();
		}

		//--------MSVS gui event handlers---------
		private void Form1_Load(object sender, EventArgs e)
		{
			SplashForm mySplash = new SplashForm();
			mySplash.Show();
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			tbxServerAddress.Enabled = false;
			btnConnect.Enabled = false;
			myBeaconSender.Connect(tbxServerAddress.Text);
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			myBeaconSender.Disconnect();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			fbdSelectShareFolder.ShowDialog();
			sShareFolder = fbdSelectShareFolder.SelectedPath;
			tbxShareFolder.Text = sShareFolder;

			gbxFileServer.Enabled = true;
			gbxSearch.Enabled = true;
			gbxStreamServer.Enabled = true;
		}

		private void tbxShareFolder_TextChanged(object sender, EventArgs e)
		{
			sShareFolder = tbxShareFolder.Text;
		}

		private void tbxServerAddress_Enter(object sender, EventArgs e)
		{
			this.AcceptButton = btnConnect;
			this.CancelButton = btnDisconnect;
		}

		private void btnStreamServerActivate_Click(object sender, EventArgs e)
		{
			StreamServer.Start();

			btnStreamServerActivate.Enabled = false;
			btnStreamServerDeactivate.Enabled = true;
		}

		private void btnStreamServerDeactivate_Click(object sender, EventArgs e)
		{
			StreamServer.Shutdown();

			btnStreamServerActivate.Enabled = true;
			btnStreamServerDeactivate.Enabled = false;
		}

		private void btnDownload_Click(object sender, EventArgs e)
		{
			if (lvwSearchResults.SelectedItems.Count != 0)	// make sure something is actually selected! otherwise an exception is thrown
			{
				string s = lvwSearchResults.SelectedItems[0].Text;
				myDownloadReceiver.Connect(lvwSearchResults.SelectedItems[0].Text + "|" + lvwSearchResults.SelectedItems[0].SubItems[1].Text);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			lvwSearchResults.Items.Clear();
			myFilelistReceiver.Search(tbxSearchQuery.Text);
		}

		private void btnPlay_Click(object sender, EventArgs e)
		{
			if (lvwSearchResults.SelectedItems.Count != 0)	// make sure something is actually selected! otherwise an exception is thrown
			{
				string s = lvwSearchResults.SelectedItems[0].Text;
				if(myStreamReceiver.Playing)
					myStreamReceiver.Stop();
				myStreamReceiver.Connect(lvwSearchResults.SelectedItems[0].Text + "|" + lvwSearchResults.SelectedItems[0].SubItems[1].Text);
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			myStreamReceiver.Stop();
		}

		private void btnFileServerDeactivate_Click(object sender, EventArgs e)
		{
			DownloadServer.Shutdown();
			btnFileServerActivate.Enabled = true;
			btnFileServerDeactivate.Enabled = false;

		}

		private void btnFileServerActivate_Click(object sender, EventArgs e)
		{
			btnFileServerActivate.Enabled = false;
			btnFileServerDeactivate.Enabled = true;
			DownloadServer.Start();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			StreamServer.Shutdown();
		}

		private void lvwSearchResults_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvwSearchResults.SelectedIndices.Count != 0)
				btnPlay.Enabled = true;
			else
				btnPlay.Enabled = false;
		}

		private void btnMembershipServerActivate_Click(object sender, EventArgs e)
		{
			MembershipServer.Activate();
			btnMembershipServerActivate.Enabled = false;
			btnMembershipServerDeactivate.Enabled = true;
		}

		private void btnMembershipServerDeactivate_Click(object sender, EventArgs e)
		{
			MembershipServer.Deactivate();
			btnMembershipServerActivate.Enabled = true;
			btnMembershipServerDeactivate.Enabled = false;
		}
	}
}
