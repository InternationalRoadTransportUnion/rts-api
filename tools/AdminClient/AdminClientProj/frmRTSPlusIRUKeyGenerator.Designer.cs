namespace IRU.RTS.AdminClient
{
    partial class frmRTSPlusIRUKeyGenerator
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
            this.lblSubscriber = new System.Windows.Forms.Label();
            this.txtSubscriber = new System.Windows.Forms.TextBox();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.lblEMail = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.fbdOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.gbOutputResult = new System.Windows.Forms.GroupBox();
            this.txtOutputResult = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblKeyLength = new System.Windows.Forms.Label();
            this.cbKeyLength = new System.Windows.Forms.ComboBox();
            this.cbSignatureAlgorithm = new System.Windows.Forms.ComboBox();
            this.lblSignatureAlgorithm = new System.Windows.Forms.Label();
            this.gbOutputResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSubscriber
            // 
            this.lblSubscriber.AutoSize = true;
            this.lblSubscriber.Location = new System.Drawing.Point(13, 13);
            this.lblSubscriber.Name = "lblSubscriber";
            this.lblSubscriber.Size = new System.Drawing.Size(71, 13);
            this.lblSubscriber.TabIndex = 0;
            this.lblSubscriber.Text = "Subscriber ID";
            // 
            // txtSubscriber
            // 
            this.txtSubscriber.Location = new System.Drawing.Point(90, 10);
            this.txtSubscriber.Name = "txtSubscriber";
            this.txtSubscriber.Size = new System.Drawing.Size(128, 20);
            this.txtSubscriber.TabIndex = 1;
            this.txtSubscriber.TextChanged += new System.EventHandler(this.txtSubscriber_TextChanged);
            this.txtSubscriber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubscriber_KeyPress);
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(413, 10);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(200, 20);
            this.txtEMail.TabIndex = 3;
            this.txtEMail.Text = "rtsplus@iru.org";
            // 
            // lblEMail
            // 
            this.lblEMail.AutoSize = true;
            this.lblEMail.Location = new System.Drawing.Point(321, 13);
            this.lblEMail.Name = "lblEMail";
            this.lblEMail.Size = new System.Drawing.Size(86, 13);
            this.lblEMail.TabIndex = 2;
            this.lblEMail.Text = "Certificate E-Mail";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(13, 45);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(90, 45);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 5;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(413, 45);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(200, 20);
            this.dtpExpiryDate.TabIndex = 7;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(321, 45);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(61, 13);
            this.lblExpiryDate.TabIndex = 6;
            this.lblExpiryDate.Text = "Expiry Date";
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(13, 82);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(71, 13);
            this.lblOutputFolder.TabIndex = 8;
            this.lblOutputFolder.Text = "Output Folder";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(90, 82);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(473, 20);
            this.txtOutputFolder.TabIndex = 9;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputFolder.Location = new System.Drawing.Point(569, 79);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(44, 23);
            this.btnOutputFolder.TabIndex = 10;
            this.btnOutputFolder.Text = "...";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // gbOutputResult
            // 
            this.gbOutputResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOutputResult.Controls.Add(this.txtOutputResult);
            this.gbOutputResult.Location = new System.Drawing.Point(16, 175);
            this.gbOutputResult.Name = "gbOutputResult";
            this.gbOutputResult.Size = new System.Drawing.Size(597, 156);
            this.gbOutputResult.TabIndex = 17;
            this.gbOutputResult.TabStop = false;
            this.gbOutputResult.Text = "Output Result";
            // 
            // txtOutputResult
            // 
            this.txtOutputResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputResult.Location = new System.Drawing.Point(7, 20);
            this.txtOutputResult.Multiline = true;
            this.txtOutputResult.Name = "txtOutputResult";
            this.txtOutputResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputResult.Size = new System.Drawing.Size(584, 130);
            this.txtOutputResult.TabIndex = 0;
            this.txtOutputResult.WordWrap = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerate.Location = new System.Drawing.Point(143, 146);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(413, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblKeyLength
            // 
            this.lblKeyLength.AutoSize = true;
            this.lblKeyLength.Location = new System.Drawing.Point(13, 118);
            this.lblKeyLength.Name = "lblKeyLength";
            this.lblKeyLength.Size = new System.Drawing.Size(61, 13);
            this.lblKeyLength.TabIndex = 11;
            this.lblKeyLength.Text = "Key Length";
            // 
            // cbKeyLength
            // 
            this.cbKeyLength.AutoCompleteCustomSource.AddRange(new string[] {
            "1024",
            "2048"});
            this.cbKeyLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeyLength.FormattingEnabled = true;
            this.cbKeyLength.Items.AddRange(new object[] {
            "1024",
            "2048"});
            this.cbKeyLength.Location = new System.Drawing.Point(90, 115);
            this.cbKeyLength.Name = "cbKeyLength";
            this.cbKeyLength.Size = new System.Drawing.Size(121, 21);
            this.cbKeyLength.TabIndex = 12;
            // 
            // cbSignatureAlgorithm
            // 
            this.cbSignatureAlgorithm.AutoCompleteCustomSource.AddRange(new string[] {
            "1024",
            "2048"});
            this.cbSignatureAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSignatureAlgorithm.FormattingEnabled = true;
            this.cbSignatureAlgorithm.Items.AddRange(new object[] {
            "MD5",
            "SHA1"});
            this.cbSignatureAlgorithm.Location = new System.Drawing.Point(351, 115);
            this.cbSignatureAlgorithm.Name = "cbSignatureAlgorithm";
            this.cbSignatureAlgorithm.Size = new System.Drawing.Size(83, 21);
            this.cbSignatureAlgorithm.TabIndex = 14;
            // 
            // lblSignatureAlgorithm
            // 
            this.lblSignatureAlgorithm.AutoSize = true;
            this.lblSignatureAlgorithm.Location = new System.Drawing.Point(247, 118);
            this.lblSignatureAlgorithm.Name = "lblSignatureAlgorithm";
            this.lblSignatureAlgorithm.Size = new System.Drawing.Size(98, 13);
            this.lblSignatureAlgorithm.TabIndex = 13;
            this.lblSignatureAlgorithm.Text = "Signature Algorithm";
            // 
            // frmRTSPlusIRUKeyGenerator
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 343);
            this.Controls.Add(this.cbSignatureAlgorithm);
            this.Controls.Add(this.lblSignatureAlgorithm);
            this.Controls.Add(this.cbKeyLength);
            this.Controls.Add(this.lblKeyLength);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.gbOutputResult);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.txtEMail);
            this.Controls.Add(this.lblEMail);
            this.Controls.Add(this.txtSubscriber);
            this.Controls.Add(this.lblSubscriber);
            this.Name = "frmRTSPlusIRUKeyGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RTS+ IRU Key Generator";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmRTSPlusIRUKeyGenerator_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRTSPlusIRUKeyGenerator_FormClosing);
            this.gbOutputResult.ResumeLayout(false);
            this.gbOutputResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubscriber;
        private System.Windows.Forms.TextBox txtSubscriber;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.Label lblEMail;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdOutput;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.GroupBox gbOutputResult;
        private System.Windows.Forms.TextBox txtOutputResult;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblKeyLength;
        private System.Windows.Forms.ComboBox cbKeyLength;
        private System.Windows.Forms.ComboBox cbSignatureAlgorithm;
        private System.Windows.Forms.Label lblSignatureAlgorithm;
    }
}