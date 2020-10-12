namespace CrossbonesDemo
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            ""}, -1);
			this.label1 = new System.Windows.Forms.Label();
			this.tbxServerAddress = new System.Windows.Forms.TextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnDisconnect = new System.Windows.Forms.Button();
			this.gbxBeacon = new System.Windows.Forms.GroupBox();
			this.gbxLocalSetup = new System.Windows.Forms.GroupBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.tbxShareFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gbxStreamServer = new System.Windows.Forms.GroupBox();
			this.btnStreamServerDeactivate = new System.Windows.Forms.Button();
			this.btnStreamServerActivate = new System.Windows.Forms.Button();
			this.gbxSearch = new System.Windows.Forms.GroupBox();
			this.lvwSearchResults = new System.Windows.Forms.ListView();
			this.columnPeer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnDownload = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPlay = new System.Windows.Forms.Button();
			this.tbxSearchQuery = new System.Windows.Forms.TextBox();
			this.gbxFileServer = new System.Windows.Forms.GroupBox();
			this.btnFileServerDeactivate = new System.Windows.Forms.Button();
			this.btnFileServerActivate = new System.Windows.Forms.Button();
			this.fbdSelectShareFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.gbxMembershipServer = new System.Windows.Forms.GroupBox();
			this.btnMembershipServerDeactivate = new System.Windows.Forms.Button();
			this.btnMembershipServerActivate = new System.Windows.Forms.Button();
			this.gbxBeacon.SuspendLayout();
			this.gbxLocalSetup.SuspendLayout();
			this.gbxStreamServer.SuspendLayout();
			this.gbxSearch.SuspendLayout();
			this.gbxFileServer.SuspendLayout();
			this.gbxMembershipServer.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server IP Address:";
			// 
			// tbxServerAddress
			// 
			this.tbxServerAddress.Location = new System.Drawing.Point(21, 50);
			this.tbxServerAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbxServerAddress.Name = "tbxServerAddress";
			this.tbxServerAddress.Size = new System.Drawing.Size(157, 22);
			this.tbxServerAddress.TabIndex = 1;
			this.tbxServerAddress.Enter += new System.EventHandler(this.tbxServerAddress_Enter);
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(21, 78);
			this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 2;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnDisconnect
			// 
			this.btnDisconnect.Enabled = false;
			this.btnDisconnect.Location = new System.Drawing.Point(103, 78);
			this.btnDisconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
			this.btnDisconnect.TabIndex = 3;
			this.btnDisconnect.Text = "Disconnect";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
			// 
			// gbxBeacon
			// 
			this.gbxBeacon.Controls.Add(this.label1);
			this.gbxBeacon.Controls.Add(this.btnDisconnect);
			this.gbxBeacon.Controls.Add(this.tbxServerAddress);
			this.gbxBeacon.Controls.Add(this.btnConnect);
			this.gbxBeacon.Location = new System.Drawing.Point(128, 11);
			this.gbxBeacon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxBeacon.Name = "gbxBeacon";
			this.gbxBeacon.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxBeacon.Size = new System.Drawing.Size(203, 121);
			this.gbxBeacon.TabIndex = 4;
			this.gbxBeacon.TabStop = false;
			this.gbxBeacon.Text = "Join Swarm";
			// 
			// gbxLocalSetup
			// 
			this.gbxLocalSetup.Controls.Add(this.btnBrowse);
			this.gbxLocalSetup.Controls.Add(this.tbxShareFolder);
			this.gbxLocalSetup.Controls.Add(this.label2);
			this.gbxLocalSetup.Enabled = false;
			this.gbxLocalSetup.Location = new System.Drawing.Point(337, 11);
			this.gbxLocalSetup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxLocalSetup.Name = "gbxLocalSetup";
			this.gbxLocalSetup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxLocalSetup.Size = new System.Drawing.Size(292, 121);
			this.gbxLocalSetup.TabIndex = 5;
			this.gbxLocalSetup.TabStop = false;
			this.gbxLocalSetup.Text = "Setup";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(181, 78);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(91, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// tbxShareFolder
			// 
			this.tbxShareFolder.Location = new System.Drawing.Point(19, 50);
			this.tbxShareFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbxShareFolder.Name = "tbxShareFolder";
			this.tbxShareFolder.Size = new System.Drawing.Size(253, 22);
			this.tbxShareFolder.TabIndex = 1;
			this.tbxShareFolder.TextChanged += new System.EventHandler(this.tbxShareFolder_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(137, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Select Share Folder:";
			// 
			// gbxStreamServer
			// 
			this.gbxStreamServer.Controls.Add(this.btnStreamServerDeactivate);
			this.gbxStreamServer.Controls.Add(this.btnStreamServerActivate);
			this.gbxStreamServer.Enabled = false;
			this.gbxStreamServer.Location = new System.Drawing.Point(739, 11);
			this.gbxStreamServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxStreamServer.Name = "gbxStreamServer";
			this.gbxStreamServer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxStreamServer.Size = new System.Drawing.Size(96, 121);
			this.gbxStreamServer.TabIndex = 7;
			this.gbxStreamServer.TabStop = false;
			this.gbxStreamServer.Text = "Streaming Server";
			// 
			// btnStreamServerDeactivate
			// 
			this.btnStreamServerDeactivate.Enabled = false;
			this.btnStreamServerDeactivate.Location = new System.Drawing.Point(10, 78);
			this.btnStreamServerDeactivate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStreamServerDeactivate.Name = "btnStreamServerDeactivate";
			this.btnStreamServerDeactivate.Size = new System.Drawing.Size(75, 23);
			this.btnStreamServerDeactivate.TabIndex = 1;
			this.btnStreamServerDeactivate.Text = "Deactivate";
			this.btnStreamServerDeactivate.UseVisualStyleBackColor = true;
			this.btnStreamServerDeactivate.Click += new System.EventHandler(this.btnStreamServerDeactivate_Click);
			// 
			// btnStreamServerActivate
			// 
			this.btnStreamServerActivate.Location = new System.Drawing.Point(10, 50);
			this.btnStreamServerActivate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStreamServerActivate.Name = "btnStreamServerActivate";
			this.btnStreamServerActivate.Size = new System.Drawing.Size(75, 23);
			this.btnStreamServerActivate.TabIndex = 0;
			this.btnStreamServerActivate.Text = "Activate";
			this.btnStreamServerActivate.UseVisualStyleBackColor = true;
			this.btnStreamServerActivate.Click += new System.EventHandler(this.btnStreamServerActivate_Click);
			// 
			// gbxSearch
			// 
			this.gbxSearch.Controls.Add(this.lvwSearchResults);
			this.gbxSearch.Controls.Add(this.btnSearch);
			this.gbxSearch.Controls.Add(this.btnDownload);
			this.gbxSearch.Controls.Add(this.btnStop);
			this.gbxSearch.Controls.Add(this.btnPlay);
			this.gbxSearch.Controls.Add(this.tbxSearchQuery);
			this.gbxSearch.Enabled = false;
			this.gbxSearch.Location = new System.Drawing.Point(12, 155);
			this.gbxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxSearch.Name = "gbxSearch";
			this.gbxSearch.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxSearch.Size = new System.Drawing.Size(823, 262);
			this.gbxSearch.TabIndex = 6;
			this.gbxSearch.TabStop = false;
			this.gbxSearch.Text = "Client";
			// 
			// lvwSearchResults
			// 
			this.lvwSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPeer,
            this.columnFile});
			this.lvwSearchResults.FullRowSelect = true;
			this.lvwSearchResults.HideSelection = false;
			this.lvwSearchResults.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.lvwSearchResults.Location = new System.Drawing.Point(21, 59);
			this.lvwSearchResults.Margin = new System.Windows.Forms.Padding(4);
			this.lvwSearchResults.MultiSelect = false;
			this.lvwSearchResults.Name = "lvwSearchResults";
			this.lvwSearchResults.Size = new System.Drawing.Size(772, 158);
			this.lvwSearchResults.TabIndex = 7;
			this.lvwSearchResults.UseCompatibleStateImageBehavior = false;
			this.lvwSearchResults.View = System.Windows.Forms.View.Details;
			this.lvwSearchResults.SelectedIndexChanged += new System.EventHandler(this.lvwSearchResults_SelectedIndexChanged);
			// 
			// columnPeer
			// 
			this.columnPeer.Text = "Peer";
			this.columnPeer.Width = 123;
			// 
			// columnFile
			// 
			this.columnFile.Text = "File";
			this.columnFile.Width = 645;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(369, 28);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(92, 23);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnDownload
			// 
			this.btnDownload.Location = new System.Drawing.Point(704, 223);
			this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(91, 23);
			this.btnDownload.TabIndex = 5;
			this.btnDownload.Text = "Download...";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(103, 223);
			this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 4;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(21, 224);
			this.btnPlay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(75, 23);
			this.btnPlay.TabIndex = 3;
			this.btnPlay.Text = "Play";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// tbxSearchQuery
			// 
			this.tbxSearchQuery.Location = new System.Drawing.Point(21, 28);
			this.tbxSearchQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbxSearchQuery.Name = "tbxSearchQuery";
			this.tbxSearchQuery.Size = new System.Drawing.Size(339, 22);
			this.tbxSearchQuery.TabIndex = 1;
			// 
			// gbxFileServer
			// 
			this.gbxFileServer.Controls.Add(this.btnFileServerDeactivate);
			this.gbxFileServer.Controls.Add(this.btnFileServerActivate);
			this.gbxFileServer.Enabled = false;
			this.gbxFileServer.Location = new System.Drawing.Point(635, 11);
			this.gbxFileServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxFileServer.Name = "gbxFileServer";
			this.gbxFileServer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gbxFileServer.Size = new System.Drawing.Size(98, 121);
			this.gbxFileServer.TabIndex = 8;
			this.gbxFileServer.TabStop = false;
			this.gbxFileServer.Text = "Download Server";
			// 
			// btnFileServerDeactivate
			// 
			this.btnFileServerDeactivate.Enabled = false;
			this.btnFileServerDeactivate.Location = new System.Drawing.Point(11, 78);
			this.btnFileServerDeactivate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnFileServerDeactivate.Name = "btnFileServerDeactivate";
			this.btnFileServerDeactivate.Size = new System.Drawing.Size(75, 23);
			this.btnFileServerDeactivate.TabIndex = 1;
			this.btnFileServerDeactivate.Text = "Deactivate";
			this.btnFileServerDeactivate.UseVisualStyleBackColor = true;
			this.btnFileServerDeactivate.Click += new System.EventHandler(this.btnFileServerDeactivate_Click);
			// 
			// btnFileServerActivate
			// 
			this.btnFileServerActivate.Location = new System.Drawing.Point(11, 50);
			this.btnFileServerActivate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnFileServerActivate.Name = "btnFileServerActivate";
			this.btnFileServerActivate.Size = new System.Drawing.Size(75, 23);
			this.btnFileServerActivate.TabIndex = 0;
			this.btnFileServerActivate.Text = "Activate";
			this.btnFileServerActivate.UseVisualStyleBackColor = true;
			this.btnFileServerActivate.Click += new System.EventHandler(this.btnFileServerActivate_Click);
			// 
			// gbxMembershipServer
			// 
			this.gbxMembershipServer.Controls.Add(this.btnMembershipServerDeactivate);
			this.gbxMembershipServer.Controls.Add(this.btnMembershipServerActivate);
			this.gbxMembershipServer.Location = new System.Drawing.Point(12, 11);
			this.gbxMembershipServer.Name = "gbxMembershipServer";
			this.gbxMembershipServer.Size = new System.Drawing.Size(110, 121);
			this.gbxMembershipServer.TabIndex = 9;
			this.gbxMembershipServer.TabStop = false;
			this.gbxMembershipServer.Text = "Membership Server";
			// 
			// btnMembershipServerDeactivate
			// 
			this.btnMembershipServerDeactivate.Enabled = false;
			this.btnMembershipServerDeactivate.Location = new System.Drawing.Point(18, 78);
			this.btnMembershipServerDeactivate.Name = "btnMembershipServerDeactivate";
			this.btnMembershipServerDeactivate.Size = new System.Drawing.Size(75, 23);
			this.btnMembershipServerDeactivate.TabIndex = 1;
			this.btnMembershipServerDeactivate.Text = "Deactivate";
			this.btnMembershipServerDeactivate.UseVisualStyleBackColor = true;
			this.btnMembershipServerDeactivate.Click += new System.EventHandler(this.btnMembershipServerDeactivate_Click);
			// 
			// btnMembershipServerActivate
			// 
			this.btnMembershipServerActivate.Location = new System.Drawing.Point(18, 50);
			this.btnMembershipServerActivate.Name = "btnMembershipServerActivate";
			this.btnMembershipServerActivate.Size = new System.Drawing.Size(75, 23);
			this.btnMembershipServerActivate.TabIndex = 0;
			this.btnMembershipServerActivate.Text = "Activate";
			this.btnMembershipServerActivate.UseVisualStyleBackColor = true;
			this.btnMembershipServerActivate.Click += new System.EventHandler(this.btnMembershipServerActivate_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(845, 427);
			this.Controls.Add(this.gbxMembershipServer);
			this.Controls.Add(this.gbxFileServer);
			this.Controls.Add(this.gbxStreamServer);
			this.Controls.Add(this.gbxSearch);
			this.Controls.Add(this.gbxLocalSetup);
			this.Controls.Add(this.gbxBeacon);
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(853, 459);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(853, 459);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Crossbones Demo";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.gbxBeacon.ResumeLayout(false);
			this.gbxBeacon.PerformLayout();
			this.gbxLocalSetup.ResumeLayout(false);
			this.gbxLocalSetup.PerformLayout();
			this.gbxStreamServer.ResumeLayout(false);
			this.gbxSearch.ResumeLayout(false);
			this.gbxSearch.PerformLayout();
			this.gbxFileServer.ResumeLayout(false);
			this.gbxMembershipServer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxServerAddress;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.GroupBox gbxBeacon;
		private System.Windows.Forms.GroupBox gbxLocalSetup;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox tbxShareFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox gbxStreamServer;
		private System.Windows.Forms.GroupBox gbxSearch;
		private System.Windows.Forms.Button btnStreamServerDeactivate;
		private System.Windows.Forms.Button btnStreamServerActivate;
		private System.Windows.Forms.GroupBox gbxFileServer;
		private System.Windows.Forms.Button btnFileServerDeactivate;
		private System.Windows.Forms.Button btnFileServerActivate;
        private System.Windows.Forms.TextBox tbxSearchQuery;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.FolderBrowserDialog fbdSelectShareFolder;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView lvwSearchResults;
        private System.Windows.Forms.ColumnHeader columnPeer;
        private System.Windows.Forms.ColumnHeader columnFile;
		private System.Windows.Forms.GroupBox gbxMembershipServer;
		private System.Windows.Forms.Button btnMembershipServerDeactivate;
		private System.Windows.Forms.Button btnMembershipServerActivate;
	}
}

