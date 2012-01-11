namespace RTSDotNETClient.TestClient
{
    partial class SafeTIRTransmissionTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSender = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btAddTestRecord = new System.Windows.Forms.Button();
            this.btLoadQuery = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMessageId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btSend
            // 
            this.btSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSend.Location = new System.Drawing.Point(16, 308);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 23);
            this.btSend.TabIndex = 2;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Sender :";
            // 
            // tbSender
            // 
            this.tbSender.Location = new System.Drawing.Point(16, 35);
            this.tbSender.Name = "tbSender";
            this.tbSender.Size = new System.Drawing.Size(219, 20);
            this.tbSender.TabIndex = 16;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(680, 182);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // btAddTestRecord
            // 
            this.btAddTestRecord.Location = new System.Drawing.Point(241, 32);
            this.btAddTestRecord.Name = "btAddTestRecord";
            this.btAddTestRecord.Size = new System.Drawing.Size(118, 23);
            this.btAddTestRecord.TabIndex = 19;
            this.btAddTestRecord.Text = "Add Test Record";
            this.btAddTestRecord.UseVisualStyleBackColor = true;
            this.btAddTestRecord.Visible = false;
            this.btAddTestRecord.Click += new System.EventHandler(this.btAddTestRecord_Click);
            // 
            // btLoadQuery
            // 
            this.btLoadQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoadQuery.Location = new System.Drawing.Point(592, 32);
            this.btLoadQuery.Name = "btLoadQuery";
            this.btLoadQuery.Size = new System.Drawing.Size(104, 23);
            this.btLoadQuery.TabIndex = 20;
            this.btLoadQuery.Text = "Load Query";
            this.btLoadQuery.UseVisualStyleBackColor = true;
            this.btLoadQuery.Click += new System.EventHandler(this.btLoadQuery_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.Filter = "XML Files | *.xml";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Message ID :";
            // 
            // tbMessageId
            // 
            this.tbMessageId.Location = new System.Drawing.Point(16, 82);
            this.tbMessageId.Name = "tbMessageId";
            this.tbMessageId.Size = new System.Drawing.Size(237, 20);
            this.tbMessageId.TabIndex = 27;
            // 
            // SafeTIRTransmissionTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbMessageId);
            this.Controls.Add(this.btLoadQuery);
            this.Controls.Add(this.btAddTestRecord);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSender);
            this.Controls.Add(this.btSend);
            this.Name = "SafeTIRTransmissionTab";
            this.Size = new System.Drawing.Size(713, 343);
            this.Load += new System.EventHandler(this.SafeTIRTransmissionTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSender;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btAddTestRecord;
        private System.Windows.Forms.Button btLoadQuery;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbMessageId;
    }
}
