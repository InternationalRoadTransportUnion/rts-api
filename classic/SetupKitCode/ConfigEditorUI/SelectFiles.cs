using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace IRU.Common.ConfigEditorUI
{
	/// <summary>
	/// Summary description for SelectFiles.
	/// </summary>
	public class SelectFiles : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtConfigFilePath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.Button cmdConfig;
		private System.Windows.Forms.Button cmdTemplate;
		private System.Windows.Forms.TextBox txtTemplateFilePath;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SelectFiles()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtConfigFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTemplateFilePath = new System.Windows.Forms.TextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.cmdConfig = new System.Windows.Forms.Button();
			this.cmdTemplate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtConfigFilePath
			// 
			this.txtConfigFilePath.Location = new System.Drawing.Point(80, 24);
			this.txtConfigFilePath.Name = "txtConfigFilePath";
			this.txtConfigFilePath.Size = new System.Drawing.Size(448, 20);
			this.txtConfigFilePath.TabIndex = 0;
			this.txtConfigFilePath.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Config File:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "Template File Path";
			// 
			// txtTemplateFilePath
			// 
			this.txtTemplateFilePath.Location = new System.Drawing.Point(80, 56);
			this.txtTemplateFilePath.Name = "txtTemplateFilePath";
			this.txtTemplateFilePath.Size = new System.Drawing.Size(448, 20);
			this.txtTemplateFilePath.TabIndex = 2;
			this.txtTemplateFilePath.Text = "";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(344, 96);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "&Ok";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(440, 96);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdConfig
			// 
			this.cmdConfig.Location = new System.Drawing.Point(536, 24);
			this.cmdConfig.Name = "cmdConfig";
			this.cmdConfig.Size = new System.Drawing.Size(32, 24);
			this.cmdConfig.TabIndex = 1;
			this.cmdConfig.Text = "...";
			this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
			// 
			// cmdTemplate
			// 
			this.cmdTemplate.Location = new System.Drawing.Point(536, 56);
			this.cmdTemplate.Name = "cmdTemplate";
			this.cmdTemplate.Size = new System.Drawing.Size(32, 24);
			this.cmdTemplate.TabIndex = 3;
			this.cmdTemplate.Text = "...";
			this.cmdTemplate.Click += new System.EventHandler(this.cmdTemplate_Click);
			// 
			// SelectFiles
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(586, 136);
			this.Controls.Add(this.cmdTemplate);
			this.Controls.Add(this.cmdConfig);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.txtTemplateFilePath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtConfigFilePath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "SelectFiles";
			this.Text = "SelectFiles";
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdConfig_Click(object sender, System.EventArgs e)
		{
			ofd.Multiselect=false;
			ofd.Title = "Select Configuration file";
				if (ofd.ShowDialog(this)==DialogResult.Cancel)
					return;
			txtConfigFilePath.Text=ofd.FileName;
			txtTemplateFilePath.Text = txtConfigFilePath.Text.Replace(".xml","_template.xml");

		}

		private void cmdTemplate_Click(object sender, System.EventArgs e)
		{
			ofd.Multiselect=false;
			ofd.Title = "Select Template file";
			if (ofd.ShowDialog(this)==DialogResult.Cancel)
				return;
			txtTemplateFilePath.Text=ofd.FileName;
		
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			this.Close();
		}

		public string ConfigFilePath
		{
			get
			{
				return txtConfigFilePath.Text;
			}
			set
			{
				txtConfigFilePath.Text=value;
			}
		}

		public string TemplateFilePath
		{
			get
			{
				return txtTemplateFilePath.Text;
			}
			set
			{
				txtTemplateFilePath.Text=value;
			}
		}
	}
}
