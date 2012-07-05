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
            this.dgwRTSPlusKeys.Location = new System.Drawing.Point(12, 12);
            this.dgwRTSPlusKeys.Name = "dgwRTSPlusKeys";
            this.dgwRTSPlusKeys.ReadOnly = true;
            this.dgwRTSPlusKeys.Size = new System.Drawing.Size(1035, 318);
            this.dgwRTSPlusKeys.TabIndex = 0;
            // 
            // SUBSCRIBER_ID
            // 
            this.SUBSCRIBER_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SUBSCRIBER_ID.DataPropertyName = "SUBSCRIBER_ID";
            this.SUBSCRIBER_ID.HeaderText = "Subscriber Id";
            this.SUBSCRIBER_ID.Name = "SUBSCRIBER_ID";
            this.SUBSCRIBER_ID.ReadOnly = true;
            this.SUBSCRIBER_ID.Width = 94;
            // 
            // VALID_FROM
            // 
            this.VALID_FROM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VALID_FROM.DataPropertyName = "VALID_FROM";
            this.VALID_FROM.HeaderText = "Valid From";
            this.VALID_FROM.Name = "VALID_FROM";
            this.VALID_FROM.ReadOnly = true;
            this.VALID_FROM.Width = 81;
            // 
            // VALID_TO
            // 
            this.VALID_TO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VALID_TO.DataPropertyName = "VALID_TO";
            this.VALID_TO.HeaderText = "Valid To";
            this.VALID_TO.Name = "VALID_TO";
            this.VALID_TO.ReadOnly = true;
            this.VALID_TO.Width = 71;
            // 
            // THUMBPRINT
            // 
            this.THUMBPRINT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.THUMBPRINT.DataPropertyName = "THUMBPRINT";
            this.THUMBPRINT.HeaderText = "Thumbprint";
            this.THUMBPRINT.Name = "THUMBPRINT";
            this.THUMBPRINT.ReadOnly = true;
            this.THUMBPRINT.Width = 85;
            // 
            // CERT_BLOB
            // 
            this.CERT_BLOB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CERT_BLOB.DataPropertyName = "CERT_BLOB";
            this.CERT_BLOB.HeaderText = "X509 Certificate";
            this.CERT_BLOB.Name = "CERT_BLOB";
            this.CERT_BLOB.ReadOnly = true;
            this.CERT_BLOB.Width = 107;
            // 
            // PRIVATE_KEY
            // 
            this.PRIVATE_KEY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PRIVATE_KEY.DataPropertyName = "PRIVATE_KEY";
            this.PRIVATE_KEY.HeaderText = "Private Key";
            this.PRIVATE_KEY.Name = "PRIVATE_KEY";
            this.PRIVATE_KEY.ReadOnly = true;
            this.PRIVATE_KEY.Width = 86;
            // 
            // KEY_ACTIVE
            // 
            this.KEY_ACTIVE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.KEY_ACTIVE.DataPropertyName = "KEY_ACTIVE";
            this.KEY_ACTIVE.HeaderText = "Active";
            this.KEY_ACTIVE.Name = "KEY_ACTIVE";
            this.KEY_ACTIVE.ReadOnly = true;
            this.KEY_ACTIVE.Width = 43;
            // 
            // CREATION_USERID
            // 
            this.CREATION_USERID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CREATION_USERID.DataPropertyName = "CREATION_USERID";
            this.CREATION_USERID.HeaderText = "Created by";
            this.CREATION_USERID.Name = "CREATION_USERID";
            this.CREATION_USERID.ReadOnly = true;
            this.CREATION_USERID.Width = 83;
            // 
            // CREATION_DATETIME
            // 
            this.CREATION_DATETIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CREATION_DATETIME.DataPropertyName = "CREATION_DATETIME";
            this.CREATION_DATETIME.HeaderText = "Created on";
            this.CREATION_DATETIME.Name = "CREATION_DATETIME";
            this.CREATION_DATETIME.ReadOnly = true;
            this.CREATION_DATETIME.Width = 84;
            // 
            // LAST_UPDATE_USERID
            // 
            this.LAST_UPDATE_USERID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LAST_UPDATE_USERID.DataPropertyName = "LAST_UPDATE_USERID";
            this.LAST_UPDATE_USERID.HeaderText = "Updated by";
            this.LAST_UPDATE_USERID.Name = "LAST_UPDATE_USERID";
            this.LAST_UPDATE_USERID.ReadOnly = true;
            this.LAST_UPDATE_USERID.Width = 87;
            // 
            // LAST_UPDATE_DATETIME
            // 
            this.LAST_UPDATE_DATETIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LAST_UPDATE_DATETIME.DataPropertyName = "LAST_UPDATE_DATETIME";
            this.LAST_UPDATE_DATETIME.HeaderText = "Updated on";
            this.LAST_UPDATE_DATETIME.Name = "LAST_UPDATE_DATETIME";
            this.LAST_UPDATE_DATETIME.ReadOnly = true;
            this.LAST_UPDATE_DATETIME.Width = 88;
            // 
            // frmRTSPlusKeystore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 342);
            this.Controls.Add(this.dgwRTSPlusKeys);
            this.Name = "frmRTSPlusKeystore";
            this.Text = "Manage RTS+ Keystore";
            this.Load += new System.EventHandler(this.frmRTSPlusKeystore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwRTSPlusKeys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwRTSPlusKeys;
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
    }
}