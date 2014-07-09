namespace IRU.RTS.WS.TestClient
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
            this.bnSearch = new System.Windows.Forms.Button();
            this.tvResult = new System.Windows.Forms.TreeView();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cbTo = new System.Windows.Forms.CheckBox();
            this.cbOffset = new System.Windows.Forms.CheckBox();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.cbCount = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // bnSearch
            // 
            this.bnSearch.Location = new System.Drawing.Point(500, 10);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(75, 38);
            this.bnSearch.TabIndex = 7;
            this.bnSearch.Text = "&Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // tvResult
            // 
            this.tvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvResult.Location = new System.Drawing.Point(12, 55);
            this.tvResult.Name = "tvResult";
            this.tvResult.Size = new System.Drawing.Size(871, 346);
            this.tvResult.TabIndex = 8;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(12, 29);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(125, 20);
            this.dtpFrom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(153, 29);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(125, 20);
            this.dtpTo.TabIndex = 2;
            // 
            // cbTo
            // 
            this.cbTo.AutoSize = true;
            this.cbTo.Location = new System.Drawing.Point(153, 10);
            this.cbTo.Name = "cbTo";
            this.cbTo.Size = new System.Drawing.Size(39, 17);
            this.cbTo.TabIndex = 1;
            this.cbTo.Text = "To";
            this.cbTo.UseVisualStyleBackColor = true;
            this.cbTo.CheckedChanged += new System.EventHandler(this.cbTo_CheckedChanged);
            // 
            // cbOffset
            // 
            this.cbOffset.AutoSize = true;
            this.cbOffset.Location = new System.Drawing.Point(295, 6);
            this.cbOffset.Name = "cbOffset";
            this.cbOffset.Size = new System.Drawing.Size(54, 17);
            this.cbOffset.TabIndex = 3;
            this.cbOffset.Text = "Offset";
            this.cbOffset.UseVisualStyleBackColor = true;
            this.cbOffset.CheckedChanged += new System.EventHandler(this.cbOffset_CheckedChanged);
            // 
            // nudOffset
            // 
            this.nudOffset.Enabled = false;
            this.nudOffset.Location = new System.Drawing.Point(295, 28);
            this.nudOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudOffset.Name = "nudOffset";
            this.nudOffset.Size = new System.Drawing.Size(79, 20);
            this.nudOffset.TabIndex = 4;
            // 
            // nudCount
            // 
            this.nudCount.Enabled = false;
            this.nudCount.Location = new System.Drawing.Point(396, 28);
            this.nudCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(80, 20);
            this.nudCount.TabIndex = 6;
            this.nudCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // cbCount
            // 
            this.cbCount.AutoSize = true;
            this.cbCount.Location = new System.Drawing.Point(396, 6);
            this.cbCount.Name = "cbCount";
            this.cbCount.Size = new System.Drawing.Size(54, 17);
            this.cbCount.TabIndex = 5;
            this.cbCount.Text = "Count";
            this.cbCount.UseVisualStyleBackColor = true;
            this.cbCount.CheckedChanged += new System.EventHandler(this.cbCount_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 413);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.cbCount);
            this.Controls.Add(this.nudOffset);
            this.Controls.Add(this.cbOffset);
            this.Controls.Add(this.cbTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.tvResult);
            this.Controls.Add(this.bnSearch);
            this.Name = "FormMain";
            this.Text = "IRU RTS WS - Test Client";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnSearch;
        private System.Windows.Forms.TreeView tvResult;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.CheckBox cbTo;
        private System.Windows.Forms.CheckBox cbOffset;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.CheckBox cbCount;
    }
}

