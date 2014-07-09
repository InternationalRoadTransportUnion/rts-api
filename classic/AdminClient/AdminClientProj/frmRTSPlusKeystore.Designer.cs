namespace IRU.RTS.AdminClient
{
    partial class frmRTSPlusKeystore
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
            this.dgwRTSPlusKeys = new System.Windows.Forms.DataGridView();
            this.WAY = new System.Windows.Forms.DataGridViewImageColumn();
            this.SUBSCRIBER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALID_FROM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALID_TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THUMBPRINT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CERT_BLOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRIVATE_KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KEY_ACTIVE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CREATION_USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATION_DATETIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_UPDATE_USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_UPDATE_DATETIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.ofdCerts = new System.Windows.Forms.OpenFileDialog();
            this.sfdCerts = new System.Windows.Forms.SaveFileDialog();
            this.btnGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRTSPlusKeys)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwRTSPlusKeys
            // 
            this.dgwRTSPlusKeys.AllowUserToAddRows = false;
            this.dgwRTSPlusKeys.AllowUserToDeleteRows = false;
            this.dgwRTSPlusKeys.AllowUserToOrderColumns = true;
            this.dgwRTSPlusKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwRTSPlusKeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WAY,
            this.SUBSCRIBER_ID,
            this.VALID_FROM,
            this.VALID_TO,
            this.THUMBPRINT,
            this.CERT_BLOB,
            this.PRIVATE_KEY,
            this.KEY_ACTIVE,
            this.CREATION_USERID,
            this.CREATION_DATETIME,
            this.LAST_UPDATE_USERID,
            this.LAST_UPDATE_DATETIME});
            this.dgwRTSPlusKeys.Location = new System.Drawing.Point(0, 0);
            this.dgwRTSPlusKeys.MultiSelect = false;
            this.dgwRTSPlusKeys.Name = "dgwRTSPlusKeys";
            this.dgwRTSPlusKeys.ReadOnly = true;
            this.dgwRTSPlusKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwRTSPlusKeys.Size = new System.Drawing.Size(1357, 292);
            this.dgwRTSPlusKeys.TabIndex = 0;
            // 
            // WAY
            // 
            this.WAY.DataPropertyName = "WAY";
            this.WAY.HeaderText = "Way";
            this.WAY.Name = "WAY";
            this.WAY.ReadOnly = true;
            this.WAY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.WAY.Width = 50;
            // 
            // SUBSCRIBER_ID
            // 
            this.SUBSCRIBER_ID.DataPropertyName = "SUBSCRIBER_ID";
            this.SUBSCRIBER_ID.HeaderText = "Subscriber Id";
            this.SUBSCRIBER_ID.Name = "SUBSCRIBER_ID";
            this.SUBSCRIBER_ID.ReadOnly = true;
            // 
            // VALID_FROM
            // 
            this.VALID_FROM.DataPropertyName = "VALID_FROM";
            this.VALID_FROM.HeaderText = "Valid From";
            this.VALID_FROM.Name = "VALID_FROM";
            this.VALID_FROM.ReadOnly = true;
            this.VALID_FROM.Width = 95;
            // 
            // VALID_TO
            // 
            this.VALID_TO.DataPropertyName = "VALID_TO";
            this.VALID_TO.HeaderText = "Valid To";
            this.VALID_TO.Name = "VALID_TO";
            this.VALID_TO.ReadOnly = true;
            this.VALID_TO.Width = 95;
            // 
            // THUMBPRINT
            // 
            this.THUMBPRINT.DataPropertyName = "THUMBPRINT";
            this.THUMBPRINT.HeaderText = "Thumbprint";
            this.THUMBPRINT.Name = "THUMBPRINT";
            this.THUMBPRINT.ReadOnly = true;
            this.THUMBPRINT.Width = 280;
            // 
            // CERT_BLOB
            // 
            this.CERT_BLOB.DataPropertyName = "CERT_BLOB";
            this.CERT_BLOB.HeaderText = "X509 Certificate";
            this.CERT_BLOB.Name = "CERT_BLOB";
            this.CERT_BLOB.ReadOnly = true;
            this.CERT_BLOB.Width = 200;
            // 
            // PRIVATE_KEY
            // 
            this.PRIVATE_KEY.DataPropertyName = "PRIVATE_KEY";
            this.PRIVATE_KEY.HeaderText = "Private Key";
            this.PRIVATE_KEY.Name = "PRIVATE_KEY";
            this.PRIVATE_KEY.ReadOnly = true;
            this.PRIVATE_KEY.Width = 200;
            // 
            // KEY_ACTIVE
            // 
            this.KEY_ACTIVE.DataPropertyName = "KEY_ACTIVE";
            this.KEY_ACTIVE.HeaderText = "Active";
            this.KEY_ACTIVE.Name = "KEY_ACTIVE";
            this.KEY_ACTIVE.ReadOnly = true;
            this.KEY_ACTIVE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.KEY_ACTIVE.Width = 50;
            // 
            // CREATION_USERID
            // 
            this.CREATION_USERID.DataPropertyName = "CREATION_USERID";
            this.CREATION_USERID.HeaderText = "Created by";
            this.CREATION_USERID.Name = "CREATION_USERID";
            this.CREATION_USERID.ReadOnly = true;
            this.CREATION_USERID.Width = 120;
            // 
            // CREATION_DATETIME
            // 
            this.CREATION_DATETIME.DataPropertyName = "CREATION_DATETIME";
            this.CREATION_DATETIME.HeaderText = "Created on";
            this.CREATION_DATETIME.Name = "CREATION_DATETIME";
            this.CREATION_DATETIME.ReadOnly = true;
            this.CREATION_DATETIME.Width = 95;
            // 
            // LAST_UPDATE_USERID
            // 
            this.LAST_UPDATE_USERID.DataPropertyName = "LAST_UPDATE_USERID";
            this.LAST_UPDATE_USERID.HeaderText = "Updated by";
            this.LAST_UPDATE_USERID.Name = "LAST_UPDATE_USERID";
            this.LAST_UPDATE_USERID.ReadOnly = true;
            this.LAST_UPDATE_USERID.Width = 120;
            // 
            // LAST_UPDATE_DATETIME
            // 
            this.LAST_UPDATE_DATETIME.DataPropertyName = "LAST_UPDATE_DATETIME";
            this.LAST_UPDATE_DATETIME.HeaderText = "Updated on";
            this.LAST_UPDATE_DATETIME.Name = "LAST_UPDATE_DATETIME";
            this.LAST_UPDATE_DATETIME.ReadOnly = true;
            this.LAST_UPDATE_DATETIME.Width = 95;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(186, 307);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(155, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import new Key/Certificate";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(362, 307);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(174, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export selected Key/Certificate";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivate.Location = new System.Drawing.Point(555, 307);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(184, 23);
            this.btnActivate.TabIndex = 4;
            this.btnActivate.Text = "Activate selected Key/Certificate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeactivate.Location = new System.Drawing.Point(758, 307);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(203, 23);
            this.btnDeactivate.TabIndex = 5;
            this.btnDeactivate.Text = "Deactivate selected Key/Certificate";
            this.btnDeactivate.UseVisualStyleBackColor = true;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // ofdCerts
            // 
            this.ofdCerts.Filter = "X509 Certificate|*.cer|Personal Keystore|*.pfx";
            // 
            // sfdCerts
            // 
            this.sfdCerts.Filter = "X509 Certificate|*.cer|Personal Keystore|*.pfx";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 307);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(155, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate IRU Key";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // frmRTSPlusKeystore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 342);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnDeactivate);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dgwRTSPlusKeys);
            this.Name = "frmRTSPlusKeystore";
            this.Text = "Manage RTS+ Keystore";
            this.Load += new System.EventHandler(this.frmRTSPlusKeystore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwRTSPlusKeys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwRTSPlusKeys;
        private System.Windows.Forms.DataGridViewImageColumn WAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUBSCRIBER_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALID_FROM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALID_TO;
        private System.Windows.Forms.DataGridViewTextBoxColumn THUMBPRINT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CERT_BLOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRIVATE_KEY;
        private System.Windows.Forms.DataGridViewCheckBoxColumn KEY_ACTIVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATION_USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATION_DATETIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_UPDATE_USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_UPDATE_DATETIME;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.OpenFileDialog ofdCerts;
        private System.Windows.Forms.SaveFileDialog sfdCerts;
        private System.Windows.Forms.Button btnGenerate;
    }
}