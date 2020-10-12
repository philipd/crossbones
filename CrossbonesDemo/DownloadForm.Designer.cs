namespace CrossbonesDemo
{
    partial class DownloadForm
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblFilename = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.pbrDownload = new System.Windows.Forms.ProgressBar();
			this.label3 = new System.Windows.Forms.Label();
			this.lblReceived = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(307, 91);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(100, 28);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "Download";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(415, 91);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 28);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblFilename
			// 
			this.lblFilename.AutoSize = true;
			this.lblFilename.Location = new System.Drawing.Point(60, 11);
			this.lblFilename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(79, 17);
			this.lblFilename.TabIndex = 2;
			this.lblFilename.Text = "lblFilename";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "File:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Size: ";
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(61, 31);
			this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(49, 17);
			this.lblSize.TabIndex = 5;
			this.lblSize.Text = "lblSize";
			// 
			// pbrDownload
			// 
			this.pbrDownload.Location = new System.Drawing.Point(16, 64);
			this.pbrDownload.Margin = new System.Windows.Forms.Padding(4);
			this.pbrDownload.Name = "pbrDownload";
			this.pbrDownload.Size = new System.Drawing.Size(497, 20);
			this.pbrDownload.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Received:";
			// 
			// lblReceived
			// 
			this.lblReceived.AutoSize = true;
			this.lblReceived.Location = new System.Drawing.Point(95, 92);
			this.lblReceived.Name = "lblReceived";
			this.lblReceived.Size = new System.Drawing.Size(81, 17);
			this.lblReceived.TabIndex = 8;
			this.lblReceived.Text = "lblReceived";
			// 
			// DownloadForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(531, 129);
			this.Controls.Add(this.lblReceived);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.pbrDownload);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblFilename);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(539, 161);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(539, 161);
			this.Name = "DownloadForm";
			this.ShowInTaskbar = false;
			this.Text = "Download File";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ProgressBar pbrDownload;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblReceived;
    }
}