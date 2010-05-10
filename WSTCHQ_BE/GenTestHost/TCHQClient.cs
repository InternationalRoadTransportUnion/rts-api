using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS.WSTCHQ;
using IRU.RTS.Crypto;
using IRU.RTS;


namespace GenTestHost
{
	/// <summary>
	/// Summary description for TCHQClient.
	/// </summary>
	public class TCHQClient : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TCHQClient()
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

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCall;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.TextBox txtKeyID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdLoad;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private string m_ConfigFile;

		public  string ConfigFile
		{
			get
			{
				return m_ConfigFile;
			}

			set
			{
				m_ConfigFile=value;
				this.Text= "Message Generator tester:" +  m_ConfigFile;
			}
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdLoad = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtKeyID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.cmdCall = new System.Windows.Forms.Button();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdLoad);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtKeyID);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtFilePath);
			this.groupBox1.Controls.Add(this.cmdCall);
			this.groupBox1.Controls.Add(this.btnSelectFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(664, 136);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "-";
			// 
			// cmdLoad
			// 
			this.cmdLoad.Location = new System.Drawing.Point(512, 24);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.Size = new System.Drawing.Size(128, 32);
			this.cmdLoad.TabIndex = 5;
			this.cmdLoad.Text = "Load";
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "Key ID";
			// 
			// txtKeyID
			// 
			this.txtKeyID.Location = new System.Drawing.Point(72, 56);
			this.txtKeyID.Name = "txtKeyID";
			this.txtKeyID.Size = new System.Drawing.Size(384, 20);
			this.txtKeyID.TabIndex = 3;
			this.txtKeyID.Text = "DBD92419709D76332AB11775BF0FEAD302B34620";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "XML Query File";
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(72, 24);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(384, 20);
			this.txtFilePath.TabIndex = 1;
			this.txtFilePath.Text = "o:\\bin\\TCHQuery.xml";
			// 
			// cmdCall
			// 
			this.cmdCall.Location = new System.Drawing.Point(512, 88);
			this.cmdCall.Name = "cmdCall";
			this.cmdCall.Size = new System.Drawing.Size(128, 32);
			this.cmdCall.TabIndex = 0;
			this.cmdCall.Text = "Call";
			this.cmdCall.Click += new System.EventHandler(this.cmdCall_Click);
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Location = new System.Drawing.Point(464, 24);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(24, 16);
			this.btnSelectFile.TabIndex = 9;
			this.btnSelectFile.Text = "...";
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// TCHQClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 237);
			this.Controls.Add(this.groupBox1);
			this.Name = "TCHQClient";
			this.Text = "TCHQClient";
			this.Load += new System.EventHandler(this.TCHQClient_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdCall_Click(object sender, System.EventArgs e)
		{
		

			// read encrzpted session kez

			
			FileStream fs = new FileStream(txtFilePath.Text +  ".3DesEncKey.bin" ,FileMode.Open ,FileAccess.Read);

			byte [] byEncrKey = new byte[fs.Length];
			fs.Read(byEncrKey,0,byEncrKey.Length);
			fs.Close();
				
			FileStream fs2 = new FileStream(txtFilePath.Text +  ".encrypted.bin" ,FileMode.Open ,FileAccess.Read);

			byte [] byEncrParams = new byte[fs2.Length];
			fs2.Read(byEncrParams,0,byEncrParams.Length);
			fs2.Close();

			TIRHolderQuery tirHolderData = new TIRHolderQuery();
			tirHolderData.Query_ID = "Q1"; 
			tirHolderData.SubscriberID = "FCS";
			tirHolderData.MessageTag = txtKeyID.Text ;
			tirHolderData.TIRCarnetHolderQueryParams = byEncrParams;
			tirHolderData.ESessionKey = byEncrKey;



			TCHQ_QueryProcessor tqp = new TCHQ_QueryProcessor();
			long QueryId ;
			tqp.ProcessQuery(tirHolderData,"1.1.1.100",out QueryId);
			
			//read encrzpted message

			//

			//

		}

		PlugInManager pm;
	

		private void TCHQClient_Load(object sender, System.EventArgs e)
		{

		
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
		}

		private void btnSelectFile_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.DefaultExt = "xml";
			openFileDialog1.Filter = "XML files (*.xml)|*.xml";
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.Multiselect = false;
			openFileDialog1.Title = "Select Query File";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtFilePath.Text = openFileDialog1.FileName;
			}

		
		}
	}
}
