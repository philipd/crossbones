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
	public partial class SplashForm : Form
	{
		int iSecs=0;

		public SplashForm()
		{
			InitializeComponent();
		}

		private void Splash_Shown(object sender, EventArgs e)
		{
			tmrSplash.Interval=1000;
			tmrSplash.Start();
		}

		private void tmrSplash_Tick(object sender, EventArgs e)
		{
			iSecs++;
			if (iSecs > 2)
			{
				this.Close();
			}
		}
	}
}
