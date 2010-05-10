using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.IO;
//using IRU.CommonInterfaces;
//using IRU.RTS.CommonComponents;
using System.Xml;
using IRU.RTS.Crypto;
using System.Security.Cryptography;
using IRU.RTS.CommonComponents;
using IRU.RTS.CryptoInterfaces;
using IRU.CommonInterfaces;


namespace RTSTestClient
{
	/// <summary>
	/// Summary description for WSRQNewRequestFileUpload.
	/// </summary>
	public class WSRQNewRequestFileUpload : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdGenerateQuery;
        private System.Windows.Forms.TextBox txtInFile;
		private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private IPlugInManager m_PluginManager;
        private string m_PollFolderLocation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		//used by Regex class to extract values
		public string m_RequestNSURN;
		//public string m_ResponseNSURN;


        public WSRQNewRequestFileUpload()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//try to read the last file touched from the parent static variable

			//txtIRUCertFile.Text = TestParent.m_IRUCertFilePath;
			
			//txtInFile.Text=TestParent.m_MesseageFileName;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdGenerateQuery = new System.Windows.Forms.Button();
            this.txtInFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectFile);
            this.groupBox1.Controls.Add(this.cmdGenerateQuery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtInFile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select File to Upload to folders set in config file of WSRQ NEW REQUEST PROCESSOR";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(365, 27);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(24, 16);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input File";
            // 
            // cmdGenerateQuery
            // 
            this.cmdGenerateQuery.Location = new System.Drawing.Point(398, 23);
            this.cmdGenerateQuery.Name = "cmdGenerateQuery";
            this.cmdGenerateQuery.Size = new System.Drawing.Size(114, 24);
            this.cmdGenerateQuery.TabIndex = 1;
            this.cmdGenerateQuery.Text = "Upload";
            this.cmdGenerateQuery.Click += new System.EventHandler(this.cmdGenerateQuery_Click);
            // 
            // txtInFile
            // 
            this.txtInFile.Location = new System.Drawing.Point(104, 24);
            this.txtInFile.Name = "txtInFile";
            this.txtInFile.Size = new System.Drawing.Size(256, 20);
            this.txtInFile.TabIndex = 0;
            // 
            // WSRQNewRequestFileUpload
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 54);
            this.Controls.Add(this.groupBox1);
            this.Name = "WSRQNewRequestFileUpload";
            this.Text = "New Request File Upload";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdGenerateQuery_Click(object sender, System.EventArgs e)
		{
			try
			{	
				/*//read the orginal file
                FileSystemListener flis = new FileSystemListener();
                m_PluginManager = new PlugInManager();
                m_PluginManager.ConfigFile = "C:\\ApplicationData\\RussianCustoms\\RTS_Builds\\bin\\WSRQNewRequest_Processor_Config.xml";
                m_PluginManager.LoadPlugins();
                //flis.Configure(m_PluginManager);
                this.Cursor=Cursors.WaitCursor;
                MessageBox.Show("New Request Upload File set.");
                flis.Stop();*/
                m_PluginManager = new PlugInManager();
                m_PluginManager.ConfigFile = "C:\\ApplicationData\\RussianCustoms\\RTS_Builds\\bin\\WSRQNewRequest_Processor_Config.xml";
                m_PluginManager.LoadPlugins();
                XmlNode sectionNode = m_PluginManager.GetConfigurationSection("FileSystemListener");
                XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,"./FileSystemListener");
                m_PollFolderLocation = parameterNode.Attributes["PollFolderLocation"].Value;
                int filenamepos = txtInFile.Text.LastIndexOf("\\");
                string filename = txtInFile.Text.Substring(filenamepos + 1);
                if (!File.Exists(m_PollFolderLocation + "\\" + filename))
                {
                    string dest=m_PollFolderLocation + "\\" + filename;
                    File.Copy(txtInFile.Text.ToString(), dest, true);
                }
                MessageBox.Show("New Request File is uploaded.");
                
                
                
                				
			}	
		
			catch (Exception exx)
			{
				MessageBox.Show("Error Encountered while uploading file.\r\n" + exx.Message + " \r\n" + exx.StackTrace );
				
	
			}
			finally
			{
				this.Cursor=Cursors.Default;
			}
		}

		private void btnSelectFile_Click(object sender, System.EventArgs e)
		{
			
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.Multiselect = false;
			openFileDialog1.Title = "Select Message Input File";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtInFile.Text = openFileDialog1.FileName;
			}
		}

	}
}
