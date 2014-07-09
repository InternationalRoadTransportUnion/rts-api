namespace IRU.RTS.TIREPD.B2G.CipherHelper
{
    partial class FormMain
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
            this.bnLoadCert = new System.Windows.Forms.Button();
            this.tcCrypto = new System.Windows.Forms.TabControl();
            this.tpEncrypt = new System.Windows.Forms.TabPage();
            this.cbMessageVersion = new System.Windows.Forms.ComboBox();
            this.cbMessageName = new System.Windows.Forms.ComboBox();
            this.dtpTimestamp = new System.Windows.Forms.DateTimePicker();
            this.tbSubscriberMessageID = new System.Windows.Forms.TextBox();
            this.tbSubscriberID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSOAP = new System.Windows.Forms.TextBox();
            this.bnEncrypt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tpDecrypt = new System.Windows.Forms.TabPage();
            this.tbPayload = new System.Windows.Forms.TextBox();
            this.bnDecrypt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tcCrypto.SuspendLayout();
            this.tpEncrypt.SuspendLayout();
            this.tpDecrypt.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnLoadCert
            // 
            this.bnLoadCert.Location = new System.Drawing.Point(13, 13);
            this.bnLoadCert.Name = "bnLoadCert";
            this.bnLoadCert.Size = new System.Drawing.Size(104, 23);
            this.bnLoadCert.TabIndex = 0;
            this.bnLoadCert.Text = "&Load certificate";
            this.bnLoadCert.UseVisualStyleBackColor = true;
            this.bnLoadCert.Click += new System.EventHandler(this.bnLoadCert_Click);
            // 
            // tcCrypto
            // 
            this.tcCrypto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcCrypto.Controls.Add(this.tpEncrypt);
            this.tcCrypto.Controls.Add(this.tpDecrypt);
            this.tcCrypto.Location = new System.Drawing.Point(13, 42);
            this.tcCrypto.Name = "tcCrypto";
            this.tcCrypto.SelectedIndex = 0;
            this.tcCrypto.Size = new System.Drawing.Size(723, 545);
            this.tcCrypto.TabIndex = 4;
            // 
            // tpEncrypt
            // 
            this.tpEncrypt.Controls.Add(this.cbMessageVersion);
            this.tpEncrypt.Controls.Add(this.cbMessageName);
            this.tpEncrypt.Controls.Add(this.dtpTimestamp);
            this.tpEncrypt.Controls.Add(this.tbSubscriberMessageID);
            this.tpEncrypt.Controls.Add(this.tbSubscriberID);
            this.tpEncrypt.Controls.Add(this.label7);
            this.tpEncrypt.Controls.Add(this.label6);
            this.tpEncrypt.Controls.Add(this.label5);
            this.tpEncrypt.Controls.Add(this.label4);
            this.tpEncrypt.Controls.Add(this.label3);
            this.tpEncrypt.Controls.Add(this.tbSOAP);
            this.tpEncrypt.Controls.Add(this.bnEncrypt);
            this.tpEncrypt.Controls.Add(this.label2);
            this.tpEncrypt.Location = new System.Drawing.Point(4, 22);
            this.tpEncrypt.Name = "tpEncrypt";
            this.tpEncrypt.Padding = new System.Windows.Forms.Padding(3);
            this.tpEncrypt.Size = new System.Drawing.Size(715, 519);
            this.tpEncrypt.TabIndex = 1;
            this.tpEncrypt.Text = "Encryption";
            this.tpEncrypt.UseVisualStyleBackColor = true;
            // 
            // cbMessageVersion
            // 
            this.cbMessageVersion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMessageVersion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMessageVersion.FormattingEnabled = true;
            this.cbMessageVersion.Items.AddRange(new object[] {
            "1.0.0"});
            this.cbMessageVersion.Location = new System.Drawing.Point(144, 118);
            this.cbMessageVersion.Name = "cbMessageVersion";
            this.cbMessageVersion.Size = new System.Drawing.Size(314, 21);
            this.cbMessageVersion.TabIndex = 18;
            // 
            // cbMessageName
            // 
            this.cbMessageName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMessageName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMessageName.FormattingEnabled = true;
            this.cbMessageName.Items.AddRange(new object[] {
            "TIRPreDeclaration"});
            this.cbMessageName.Location = new System.Drawing.Point(144, 91);
            this.cbMessageName.Name = "cbMessageName";
            this.cbMessageName.Size = new System.Drawing.Size(314, 21);
            this.cbMessageName.TabIndex = 17;
            // 
            // dtpTimestamp
            // 
            this.dtpTimestamp.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpTimestamp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimestamp.Location = new System.Drawing.Point(144, 143);
            this.dtpTimestamp.Name = "dtpTimestamp";
            this.dtpTimestamp.Size = new System.Drawing.Size(314, 20);
            this.dtpTimestamp.TabIndex = 19;
            // 
            // tbSubscriberMessageID
            // 
            this.tbSubscriberMessageID.Location = new System.Drawing.Point(144, 65);
            this.tbSubscriberMessageID.Name = "tbSubscriberMessageID";
            this.tbSubscriberMessageID.Size = new System.Drawing.Size(314, 20);
            this.tbSubscriberMessageID.TabIndex = 16;
            this.tbSubscriberMessageID.Text = "A free short Text which should be unique";
            // 
            // tbSubscriberID
            // 
            this.tbSubscriberID.Location = new System.Drawing.Point(144, 39);
            this.tbSubscriberID.Name = "tbSubscriberID";
            this.tbSubscriberID.Size = new System.Drawing.Size(314, 20);
            this.tbSubscriberID.TabIndex = 15;
            this.tbSubscriberID.Text = "XXXCUS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Timestamp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Message Version";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Message Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Subscriber Message ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Subscriber ID";
            // 
            // tbSOAP
            // 
            this.tbSOAP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSOAP.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSOAP.Location = new System.Drawing.Point(9, 188);
            this.tbSOAP.Multiline = true;
            this.tbSOAP.Name = "tbSOAP";
            this.tbSOAP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSOAP.Size = new System.Drawing.Size(700, 323);
            this.tbSOAP.TabIndex = 21;
            this.tbSOAP.WordWrap = false;
            // 
            // bnEncrypt
            // 
            this.bnEncrypt.Location = new System.Drawing.Point(9, 9);
            this.bnEncrypt.Name = "bnEncrypt";
            this.bnEncrypt.Size = new System.Drawing.Size(150, 23);
            this.bnEncrypt.TabIndex = 8;
            this.bnEncrypt.Text = "&Create B2G SOAP request";
            this.bnEncrypt.UseVisualStyleBackColor = true;
            this.bnEncrypt.Click += new System.EventHandler(this.bnEncrypt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "B2G SOAP request";
            // 
            // tpDecrypt
            // 
            this.tpDecrypt.Controls.Add(this.tbPayload);
            this.tpDecrypt.Controls.Add(this.bnDecrypt);
            this.tpDecrypt.Controls.Add(this.label1);
            this.tpDecrypt.Location = new System.Drawing.Point(4, 22);
            this.tpDecrypt.Name = "tpDecrypt";
            this.tpDecrypt.Padding = new System.Windows.Forms.Padding(3);
            this.tpDecrypt.Size = new System.Drawing.Size(715, 519);
            this.tpDecrypt.TabIndex = 0;
            this.tpDecrypt.Text = "Decryption";
            this.tpDecrypt.UseVisualStyleBackColor = true;
            // 
            // tbPayload
            // 
            this.tbPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPayload.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPayload.Location = new System.Drawing.Point(9, 53);
            this.tbPayload.Multiline = true;
            this.tbPayload.Name = "tbPayload";
            this.tbPayload.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbPayload.Size = new System.Drawing.Size(700, 460);
            this.tbPayload.TabIndex = 6;
            this.tbPayload.WordWrap = false;
            // 
            // bnDecrypt
            // 
            this.bnDecrypt.Location = new System.Drawing.Point(9, 11);
            this.bnDecrypt.Name = "bnDecrypt";
            this.bnDecrypt.Size = new System.Drawing.Size(123, 23);
            this.bnDecrypt.TabIndex = 5;
            this.bnDecrypt.Text = "&Decrypt B2G request";
            this.bnDecrypt.UseVisualStyleBackColor = true;
            this.bnDecrypt.Click += new System.EventHandler(this.bnDecrypt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Decrypted payload";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 599);
            this.Controls.Add(this.tcCrypto);
            this.Controls.Add(this.bnLoadCert);
            this.Name = "FormMain";
            this.Text = "IRU RTS TIREPD B2G Cipher Helper";
            this.tcCrypto.ResumeLayout(false);
            this.tpEncrypt.ResumeLayout(false);
            this.tpEncrypt.PerformLayout();
            this.tpDecrypt.ResumeLayout(false);
            this.tpDecrypt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnLoadCert;
        private System.Windows.Forms.TabControl tcCrypto;
        private System.Windows.Forms.TabPage tpDecrypt;
        private System.Windows.Forms.TextBox tbPayload;
        private System.Windows.Forms.Button bnDecrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpEncrypt;
        private System.Windows.Forms.TextBox tbSOAP;
        private System.Windows.Forms.Button bnEncrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSubscriberMessageID;
        private System.Windows.Forms.TextBox tbSubscriberID;
        private System.Windows.Forms.DateTimePicker dtpTimestamp;
        private System.Windows.Forms.ComboBox cbMessageName;
        private System.Windows.Forms.ComboBox cbMessageVersion;
    }
}

