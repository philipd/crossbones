using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CrossbonesDemo
{
	public partial class DownloadForm : Form
	{
		public string sFilename;
		public string sFilesize;
		delegate void delVoidInt(int i);
		delegate void delVoidBool(bool b);
		delegate void delVoidString(string s);
		DownloadReceiver myDownloadReceiver;
		private int iReceived;

		public DownloadForm(string sFilename, string sFilesize, DownloadReceiver myDownloadReceiver)
		{
			InitializeComponent();
			myDownloadReceiver.myReceiveEvent += new DownloadReceiver.ReceiveHandler(ReceiveDetected);
			myDownloadReceiver.myDownloadStartedEvent += new DownloadReceiver.DownloadStartedHandler(DownloadStartedDetected);

			this.sFilename = sFilename;
			this.sFilesize = sFilesize;
			this.myDownloadReceiver = myDownloadReceiver;

			lblFilename.Text = sFilename;
			lblSize.Text = sFilesize;
			pbrDownload.Maximum = Int32.Parse(sFilesize);

			iReceived = 0;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			btnOK.Enabled = false;
			myDownloadReceiver.StartDownload();
		}

		private void ReceiveDetected(object sender, DownloadReceiver.ReceiveEventArgs e)
		{
			try
			{
				if (pbrDownload.InvokeRequired)
					Invoke(new delVoidInt(pbrDownloadUpdate), e.Received);
				iReceived += e.Received;
				if (pbrDownload.InvokeRequired)
					Invoke(new delVoidInt(lblReceivedUpdate), iReceived);
				if (lblReceived.Text == lblSize.Text)
				{
					if (btnCancel.InvokeRequired)
						Invoke(new delVoidString(btnCancelTextUpdate), "OK");
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnCancelTextUpdate(string s)
		{
			btnCancel.Text = s;
		}

		private void DownloadStartedDetected(object sender, DownloadReceiver.DownloadStartedEventArgs e)
		{
			if (btnCancel.InvokeRequired)
				Invoke(new delVoidBool(btnCancelUpdate), true);
		}

		private void pbrDownloadUpdate(int i)
		{
			pbrDownload.Increment(i);
		}

		private void lblReceivedUpdate(int i)
		{
			lblReceived.Text = "" + i;
		}

		private void btnCancelUpdate(bool b)
		{
			btnCancel.Enabled = b;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			myDownloadReceiver.StopDownload();
			myDownloadReceiver.Disconnect();
		}
	}
}
