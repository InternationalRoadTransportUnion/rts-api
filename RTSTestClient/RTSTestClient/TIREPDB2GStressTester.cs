using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using IRU.RTS.Crypto;
using IRU.RTS.CommonComponents;

using System.Xml;

using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using IRU.RTS.CryptoInterfaces;




namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQStressTester.
	/// </summary>
	public class TIREPDB2GStressTester : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.CheckBox chkFileSelected;
		private System.Windows.Forms.CheckBox chkQueryProcessed;
		private System.ComponentModel.IContainer components;

	
		private string [] m_aStressXML;

		//used by Regex class to extract values
		public string m_RequestNSURN;
		//public string m_ResponseNSURN;


		private string m_IRUCertFile;
		private System.Windows.Forms.FolderBrowserDialog fbd;

		//private ArrayList m_aStressData;
		private string m_messageGenPath; //folder 

		private string m_thumbPrint;
		private System.Windows.Forms.LinkLabel lnk3;
		private System.Windows.Forms.LinkLabel lnk2;
		private System.Windows.Forms.LinkLabel lnk1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.TextBox txtLogFileName;
		private System.Windows.Forms.CheckBox chkSaveOpInfile;
		private System.Windows.Forms.NumericUpDown numThdCnt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lnk4;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.Timer tmrStressResults;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtEndTime;
		private System.Windows.Forms.TextBox txtStartTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtSubsID;
		private System.Windows.Forms.CheckBox chk_Continuous;
		private System.Windows.Forms.TextBox txtCycles;
		private System.Windows.Forms.Label lblCycles;

		private RSAParameters m_rsaParams;


        public TIREPDB2GStressTester()
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lnk4 = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCycles = new System.Windows.Forms.Label();
            this.txtCycles = new System.Windows.Forms.TextBox();
            this.chk_Continuous = new System.Windows.Forms.CheckBox();
            this.txtSubsID = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtLogFileName = new System.Windows.Forms.TextBox();
            this.chkSaveOpInfile = new System.Windows.Forms.CheckBox();
            this.numThdCnt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lnk3 = new System.Windows.Forms.LinkLabel();
            this.chkQueryProcessed = new System.Windows.Forms.CheckBox();
            this.lnk2 = new System.Windows.Forms.LinkLabel();
            this.lnk1 = new System.Windows.Forms.LinkLabel();
            this.chkFileSelected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.tmrStressResults = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThdCnt)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEndTime);
            this.groupBox1.Controls.Add(this.txtStartTime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lnk4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lnk3);
            this.groupBox1.Controls.Add(this.chkQueryProcessed);
            this.groupBox1.Controls.Add(this.lnk2);
            this.groupBox1.Controls.Add(this.lnk1);
            this.groupBox1.Controls.Add(this.chkFileSelected);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(688, 312);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(280, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 24);
            this.label4.TabIndex = 25;
            this.label4.Text = "End Time";
            // 
            // txtEndTime
            // 
            this.txtEndTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEndTime.Enabled = false;
            this.txtEndTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtEndTime.Location = new System.Drawing.Point(328, 272);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(272, 19);
            this.txtEndTime.TabIndex = 24;
            // 
            // txtStartTime
            // 
            this.txtStartTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStartTime.Enabled = false;
            this.txtStartTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtStartTime.Location = new System.Drawing.Point(336, 232);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(264, 19);
            this.txtStartTime.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(272, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "Start Time";
            // 
            // lnk4
            // 
            this.lnk4.Enabled = false;
            this.lnk4.Location = new System.Drawing.Point(32, 272);
            this.lnk4.Name = "lnk4";
            this.lnk4.Size = new System.Drawing.Size(200, 24);
            this.lnk4.TabIndex = 21;
            this.lnk4.TabStop = true;
            this.lnk4.Text = "4: Stop Test";
            this.lnk4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk4_LinkClicked_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblCycles);
            this.groupBox3.Controls.Add(this.txtCycles);
            this.groupBox3.Controls.Add(this.chk_Continuous);
            this.groupBox3.Controls.Add(this.txtSubsID);
            this.groupBox3.Controls.Add(this.btnSelectFile);
            this.groupBox3.Controls.Add(this.txtLogFileName);
            this.groupBox3.Controls.Add(this.chkSaveOpInfile);
            this.groupBox3.Controls.Add(this.numThdCnt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(24, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(648, 100);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // lblCycles
            // 
            this.lblCycles.Location = new System.Drawing.Point(392, 43);
            this.lblCycles.Name = "lblCycles";
            this.lblCycles.Size = new System.Drawing.Size(112, 16);
            this.lblCycles.TabIndex = 30;
            this.lblCycles.Text = "No Of Cycles / thread";
            this.lblCycles.Visible = false;
            // 
            // txtCycles
            // 
            this.txtCycles.Location = new System.Drawing.Point(512, 40);
            this.txtCycles.Name = "txtCycles";
            this.txtCycles.Size = new System.Drawing.Size(48, 20);
            this.txtCycles.TabIndex = 29;
            this.txtCycles.Text = "10";
            this.txtCycles.Visible = false;
            // 
            // chk_Continuous
            // 
            this.chk_Continuous.Checked = true;
            this.chk_Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Continuous.Location = new System.Drawing.Point(392, 11);
            this.chk_Continuous.Name = "chk_Continuous";
            this.chk_Continuous.Size = new System.Drawing.Size(112, 16);
            this.chk_Continuous.TabIndex = 28;
            this.chk_Continuous.Text = "Continuous";
            this.chk_Continuous.CheckStateChanged += new System.EventHandler(this.chk_Continuous_CheckStateChanged);
            this.chk_Continuous.CheckedChanged += new System.EventHandler(this.chk_Continuous_CheckedChanged);
            // 
            // txtSubsID
            // 
            this.txtSubsID.Location = new System.Drawing.Point(248, 40);
            this.txtSubsID.Name = "txtSubsID";
            this.txtSubsID.Size = new System.Drawing.Size(120, 20);
            this.txtSubsID.TabIndex = 27;
            this.txtSubsID.Text = "BLRCUS";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Enabled = false;
            this.btnSelectFile.Location = new System.Drawing.Point(600, 72);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(40, 24);
            this.btnSelectFile.TabIndex = 24;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtLogFileName
            // 
            this.txtLogFileName.Enabled = false;
            this.txtLogFileName.Location = new System.Drawing.Point(304, 72);
            this.txtLogFileName.Name = "txtLogFileName";
            this.txtLogFileName.Size = new System.Drawing.Size(304, 20);
            this.txtLogFileName.TabIndex = 23;
            this.txtLogFileName.Text = "o:\\bin\\StressLog.txt";
            // 
            // chkSaveOpInfile
            // 
            this.chkSaveOpInfile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSaveOpInfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSaveOpInfile.Location = new System.Drawing.Point(16, 72);
            this.chkSaveOpInfile.Name = "chkSaveOpInfile";
            this.chkSaveOpInfile.Size = new System.Drawing.Size(240, 24);
            this.chkSaveOpInfile.TabIndex = 22;
            this.chkSaveOpInfile.Text = "Save Output  in File";
            this.chkSaveOpInfile.CheckedChanged += new System.EventHandler(this.chkSaveOpInfile_CheckedChanged);
            // 
            // numThdCnt
            // 
            this.numThdCnt.Location = new System.Drawing.Point(248, 8);
            this.numThdCnt.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numThdCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThdCnt.Name = "numThdCnt";
            this.numThdCnt.Size = new System.Drawing.Size(120, 20);
            this.numThdCnt.TabIndex = 20;
            this.numThdCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Concurrent Request Threads";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "SubscriberID";
            // 
            // lnk3
            // 
            this.lnk3.Enabled = false;
            this.lnk3.Location = new System.Drawing.Point(32, 232);
            this.lnk3.Name = "lnk3";
            this.lnk3.Size = new System.Drawing.Size(200, 24);
            this.lnk3.TabIndex = 7;
            this.lnk3.TabStop = true;
            this.lnk3.Text = "3: Start Testing";
            this.lnk3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // chkQueryProcessed
            // 
            this.chkQueryProcessed.AutoCheck = false;
            this.chkQueryProcessed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkQueryProcessed.Location = new System.Drawing.Point(264, 80);
            this.chkQueryProcessed.Name = "chkQueryProcessed";
            this.chkQueryProcessed.Size = new System.Drawing.Size(32, 24);
            this.chkQueryProcessed.TabIndex = 6;
            // 
            // lnk2
            // 
            this.lnk2.Enabled = false;
            this.lnk2.Location = new System.Drawing.Point(24, 88);
            this.lnk2.Name = "lnk2";
            this.lnk2.Size = new System.Drawing.Size(208, 24);
            this.lnk2.TabIndex = 5;
            this.lnk2.TabStop = true;
            this.lnk2.Text = "2: Process Upload Messages";
            this.lnk2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // lnk1
            // 
            this.lnk1.Location = new System.Drawing.Point(24, 56);
            this.lnk1.Name = "lnk1";
            this.lnk1.Size = new System.Drawing.Size(224, 16);
            this.lnk1.TabIndex = 4;
            this.lnk1.TabStop = true;
            this.lnk1.Text = "1: Select Message file(s)(XML) with values";
            this.lnk1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chkFileSelected
            // 
            this.chkFileSelected.AutoCheck = false;
            this.chkFileSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFileSelected.Location = new System.Drawing.Point(264, 48);
            this.chkFileSelected.Name = "chkFileSelected";
            this.chkFileSelected.Size = new System.Drawing.Size(32, 24);
            this.chkFileSelected.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Steps:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOutPut);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(688, 166);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtOutPut
            // 
            this.txtOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutPut.Location = new System.Drawing.Point(3, 16);
            this.txtOutPut.Multiline = true;
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutPut.Size = new System.Drawing.Size(682, 147);
            this.txtOutPut.TabIndex = 0;
            // 
            // ofd
            // 
            this.ofd.Multiselect = true;
            // 
            // fbd
            // 
            this.fbd.Description = "Select Folder to store the generated XML Messages";
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "txt";
            this.sfd.FileName = "StressResults.txt";
            this.sfd.Title = "Select file to save stress log.";
            // 
            // tmrStressResults
            // 
            this.tmrStressResults.Tick += new System.EventHandler(this.tmrStressResults_Tick);
            // 
            // WSSTStressTester
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(688, 478);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TIREPDB2GStressTester";
            this.Text = "TIREPDB2GStressTester";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThdCnt)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			//load file
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.Title="Select .xml file(s) containing Upload Messages";
			ofd.Filter="Test Data Files (*.xml)|*.xml";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			try
			{
				if(ofd.FileNames.Length >0)
				{
					m_aStressXML = ofd.FileNames;
					m_messageGenPath = Path.GetDirectoryName(m_aStressXML[0]); 

					txtOutPut.Text+="\r\n" + " Selected "+ofd.FileNames.Length+" data files : ";
					
					//= ofd.FileName;

                    TIREPDB2GStressTest tcs = new TIREPDB2GStressTest();
					//m_aStressData= tcs.GetDataFromXML(m_aStressXML);

					//txtOutPut.Text+="\r\n" + " Records found in file: " + m_aStressData.Count;

					chkFileSelected.Checked=true;
					lnk2.Enabled=true;
				}				
			}
			catch (Exception exx)
			{
				m_aStressXML = null;
				m_aStressXML=null;
				txtOutPut.Text+="\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			
			}

		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			#region Select Certificate
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.Title="Select IRU Certificate File";
			ofd.Filter="Certficate File (*.cer)|*.cer";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			try
			{
				txtOutPut.Text+="\r\n" + " Selected certificate file : " + ofd.FileName;
				m_IRUCertFile = ofd.FileName;
				
				CertGenerator cg = new CertGenerator();
							
				DateTime expiryDate;
				
				string messages;

				bool bCertSuccess = cg.ExtractKeysFromSubscriberCertFile3(m_IRUCertFile,out m_rsaParams,out m_thumbPrint,out messages, out expiryDate);

				if (!bCertSuccess)
				{
					txtOutPut.Text+="\r\n" + " Error Extracting Certificate data " + messages;
				}
				txtOutPut.Text+="\r\n" + messages;

				chkFileSelected.Checked=true;
				chkQueryProcessed.Checked = true;
			}
			catch (Exception exx)
			{
				
				m_IRUCertFile=null;
				m_thumbPrint=null;
				
				txtOutPut.Text+="\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			}			
		
			#endregion


            #region Select folder and generate XML message - Not in Use for TIREPDB2GStressTest

            //			if (m_messageGenPath!="")
//				fbd.SelectedPath=m_messageGenPath;
//			dr = fbd.ShowDialog();
//
//			if (dr!=DialogResult.OK)
//				return;
//
//			try
//			{
//				txtOutPut.Text+="\r\n" + " Generated Message Folder : " + fbd.SelectedPath;
//				m_messageGenPath = fbd.SelectedPath;
//
//				TCHQStressTest tcs = new TCHQStressTest();
//				txtOutPut.Text+="\r\n" + " About to start generating message .xml Files";
//				tcs.GenerateMsgFiles(fbd.SelectedPath,m_aStressData);
//
//				txtOutPut.Text+="\r\n" + " Completing writing xml files.";
//				chkQueryProcessed.Checked=true;
//			}
//			catch (Exception exx)
//			{
//				m_aStressData = null;
//				m_aStressXML=null;
//				txtOutPut.Text+="\r\n" + " Exception Occured while Generating Messages " + exx.Message + "\r\n" + exx.StackTrace;
//				return;
//			}			
			#endregion

			#region Generate Encrypted File Set

			try
			{
			
				int nCount = 0;
				txtOutPut.Text+="\r\n" + " About to start Encrypting Files";

				foreach (string aFileQID in m_aStressXML)
				{
					nCount++;
					//string sFileName =  aFileQID[0] + ".xml";
					//string sFilePath = m_messageGenPath + "\\" + sFileName;
					generateEncFile(aFileQID,m_rsaParams);
					txtOutPut.Text+="\r\n" + " Processed File " + aFileQID  +  " :" + nCount + " of " + m_aStressXML.Length;
			
				}
			}
			catch (Exception exx)
			{
				txtOutPut.Text+="\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			}

			#endregion


			lnk3.Enabled=true;
		}


		private void generateEncFile(string sFileName, RSAParameters rParams)
		{
			#region Notin use
			/* 
		
			string sQueryXML;

				//read the orginal file
				StreamReader sr = new StreamReader(sFileName);
				try
				{
					sQueryXML = sr.ReadToEnd();
				}
				finally
				{
					sr.Close();
				}

			
				


				// extract BodyContents

				string bodyContents;
			/*
				int startPos, endPos;
				startPos = sQueryXML.IndexOf("<Body>")+6;
				endPos= sQueryXML.IndexOf("</Body>");
				bodyContents=sQueryXML.Substring(startPos, endPos-startPos);
				*/

			/*
				bodyContents = RegExHelper.ExtractBODYContents(sQueryXML);

				byte[] aBodyContents = System.Text.Encoding.Unicode.GetBytes(bodyContents);

				ICryptoOperations ic = 
					(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), System.Configuration.ConfigurationSettings.AppSettings["CryptoEndPoint"]);
			
			
		
				Hashtable ht = new Hashtable();
				System.Diagnostics.Debug.WriteLine("Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
				byte[] outputHash =null; ;
				//for (int i=0; i<100 ; i++)
				outputHash  = ic.Hash(aBodyContents,"SHA1", ht);
				System.Diagnostics.Debug.WriteLine(">>Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));

				string hashValue = Convert.ToBase64String(outputHash);



				/*XmlDocument xd = new XmlDocument();
				xd.PreserveWhitespace=true;
				xd.LoadXml(sQueryXML);
				XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
				xns.AddNamespace("def","http://www.iru.org/TCHQuery");

				xd.DocumentElement.SelectSingleNode("//def:Hash",xns).InnerText=hashValue;
				string sDocwithHash = xd.OuterXml;
*/

				// hashreplacement a string operation
			/*
			int iIdxStart=0, iIdxEnd = 0;

				iIdxStart = sQueryXML.IndexOf("<Hash>");
				if (iIdxStart >0 ) 
				{
					iIdxEnd = sQueryXML.IndexOf("</Hash>") + 7;
				
				
				}
				if (iIdxStart < 0)
				{
					iIdxStart = sQueryXML.IndexOf("<Hash/>");
					if (iIdxStart >0 ) 
					{
						iIdxEnd = iIdxStart + 7;
					}	

				}

				// search for <Hash />
				if (iIdxStart < 0)
				{
					iIdxStart = sQueryXML.IndexOf("<Hash />");
					if (iIdxStart >0 ) 
					{
						iIdxEnd = iIdxStart + 8;
					}	

				}

				
				string strToReplace = sQueryXML.Substring(iIdxStart,iIdxEnd-iIdxStart);   
				sQueryXML = sQueryXML.Replace(strToReplace,"<Hash>" + hashValue + "</Hash>");

				string sDocwithHash = sQueryXML;
			*/
				//Encrypt with 3DES

			/*
				string nsPrefix = RegExHelper.ExtractNameSpacePrefix(sQueryXML,m_RequestNSURN);

				string sDocwithHash = RegExHelper.SetHASH(sQueryXML,nsPrefix,hashValue);
				
				byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
 

				ht = new Hashtable();
				ht["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
				byte[] aEncMessage  = ic.Encrypt(aDocWithHash,"3DES",ref ht);

				// Op this to File

				string opFileName= sFileName +".encrypted.bin";

				FileStream fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(aEncMessage,0,aEncMessage.Length);
				fs.Close();

				//Write the plain key out

				byte[] a3DesKey = (byte[])ht["KEY"];

				opFileName= sFileName + ".3DesKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesKey,0,a3DesKey.Length);
				fs.Close();




				
			 
				DateTime expiryDate = DateTime.Now;

			
				// Start encrypting the Key
			
				ht = new Hashtable();
				ht["EXPONENT"]= rParams.Exponent;
				ht["MODULUS"] = rParams.Modulus;
			
				byte[] a3DesEncKey =null; ;
				//for (int i=0; i<100 ; i++)
				a3DesEncKey  = ic.Encrypt(a3DesKey,"RSA",ref ht);
				//write encryptedkey to file

				opFileName= sFileName+".3DesEncKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesEncKey,0,a3DesEncKey.Length);
				fs.Close();
			*/
			#endregion

			string sQueryXML;

			try
			{	
				//read the orginal file
				StreamReader sr = new StreamReader(sFileName);
				try
				{
					sQueryXML = sr.ReadToEnd();
				}
				finally
				{
					sr.Close();
				}

			
				this.Cursor=Cursors.WaitCursor;


				// extract BodyContents

				string bodyContents;
				//////				int startPos, endPos;
				//////				startPos = sQueryXML.IndexOf("<Body>")+6;
				//////				endPos= sQueryXML.IndexOf("</Body>");
				//////				bodyContents=sQueryXML.Substring(startPos, endPos-startPos);
				///
				bodyContents=RegExHelper.ExtractBODYContents(sQueryXML);

				byte[] aBodyContents = System.Text.Encoding.Unicode.GetBytes(bodyContents);




				ICryptoOperations ic = 
					(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), System.Configuration.ConfigurationSettings.AppSettings["CryptoEndPoint"]);
			
			
			
			
			
		
				Hashtable ht = new Hashtable();
				System.Diagnostics.Debug.WriteLine("Hash " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"));
				byte[] outputHash =null; ;
				//for (int i=0; i<100 ; i++)
				outputHash  = ic.Hash(aBodyContents,"SHA1", ht);
				System.Diagnostics.Debug.WriteLine(">>Hash " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"));

				string hashValue = Convert.ToBase64String(outputHash);



				/*XmlDocument xd = new XmlDocument();
				xd.PreserveWhitespace=true;
				xd.LoadXml(sQueryXML);
				XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
				xns.AddNamespace("def","http://www.iru.org/TCHQuery");

				xd.DocumentElement.SelectSingleNode("//def:Hash",xns).InnerText=hashValue;
				string sDocwithHash = xd.OuterXml;
*/

				// hashreplacement a string operation
				/*
				int iIdxStart=0, iIdxEnd = 0;

				iIdxStart = sQueryXML.IndexOf("<Hash>");
				if (iIdxStart >0 ) 
				{
					iIdxEnd = sQueryXML.IndexOf("</Hash>") + 7;
				
				
				}
				if (iIdxStart < 0)
				{
					iIdxStart = sQueryXML.IndexOf("<Hash/>");
					if (iIdxStart >0 ) 
					{
						iIdxEnd = iIdxStart + 7;
					}	

				}

				// search for <Hash />
				if (iIdxStart < 0)
				{
					iIdxStart = sQueryXML.IndexOf("<Hash />");
					if (iIdxStart >0 ) 
					{
						iIdxEnd = iIdxStart + 8;
					}	

				}

				
				string strToReplace = sQueryXML.Substring(iIdxStart,iIdxEnd-iIdxStart);   
				sQueryXML = sQueryXML.Replace(strToReplace,"<Hash>" + hashValue + "</Hash>");
*/


				string nsPrefix = RegExHelper.ExtractNameSpacePrefix(sQueryXML,m_RequestNSURN);

				string sDocwithHash = RegExHelper.SetHASH(sQueryXML,nsPrefix,hashValue);
				//Encrypt with 3DES


				byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
 

				ht = new Hashtable();
				ht["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
				byte[] aEncMessage  = ic.Encrypt(aDocWithHash,"3DES",ref ht);

				// Op this to File

				string opFileName= sFileName+".encrypted.bin";

				FileStream fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(aEncMessage,0,aEncMessage.Length);
				fs.Close();

				//Write the plain key out

				byte[] a3DesKey = (byte[])ht["KEY"];

				opFileName= sFileName+".3DesKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesKey,0,a3DesKey.Length);
				fs.Close();

				DateTime expiryDate = DateTime.Now;

				// Start encrypting the Key
			
				ht = new Hashtable();
				ht["EXPONENT"]= rParams.Exponent;
				ht["MODULUS"] = rParams.Modulus;
				System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"));
				byte[] a3DesEncKey =null; ;
				//for (int i=0; i<100 ; i++)
				a3DesEncKey  = ic.Encrypt(a3DesKey,"RSA",ref ht);
				System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"));

				//write encryptedkey to file

				opFileName= sFileName+".3DesEncKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesEncKey,0,a3DesEncKey.Length);
				fs.Close();
			
			}	
		
			catch (Exception exx)
			{
				MessageBox.Show("Error Encountered generating Encrypted Message File Set generated.\r\n" + exx.Message + " \r\n" + exx.StackTrace );
			}
			finally
			{
				this.Cursor=Cursors.Default;
			}
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            
			if (chkSaveOpInfile.Checked)
			{
				if (txtLogFileName.Text=="")
				{
					MessageBox.Show(this,"Select File to save log results, before continuing");
					return;
				}

			}

            ////////if (MessageBox.Show(this, "Start testing with " + numThdCnt.Value.ToString() + " thread(s)?\r\n ", "TIREPDB2G Tester", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            ////////{
            ////////    return;
            ////////}

            #region cleanup before test
            //clear contents
            sbStressMessages = new StringBuilder();
            tmrStressResults.Interval = 1000;
            tmrStressResults.Start();
            lnk4.Enabled = true;
            m_StopThread = new System.Threading.ManualResetEvent(false);

            #endregion
            txtStartTime.Text = DateTime.Now.ToLongTimeString();
            txtEndTime.Text = "";
			
            ////////for (int nthreadCount=0; nthreadCount<numThdCnt.Value;nthreadCount++)
            ////////{
            ////////    Thread t = new Thread(new ThreadStart(StressThreadFunction));
            ////////    t.Name="Stress thread no." + nthreadCount;
            ////////    t.IsBackground = true; // terminiates when main thread kills
            ////////    t.Start();
			
            ////////}

            ////////lnk4.Enabled=true;
            ////////return; // earlier code for single run below


            lnk4.Enabled=true;
			#region loop through all files
			try
			{
			
				int nCount = 0;
				txtOutPut.Text+="\r\n" + " About to start calling WebService..";

				foreach (string aFileQID in m_aStressXML)
				{
					nCount++;
					//string sFileName =  aFileQID[0] + ".xml";
					//string sFilePath = m_messageGenPath + "\\" + sFileName;

                    CallWS(aFileQID, m_thumbPrint, aFileQID, "BLRCUS", "ASMAP");			 
						
					txtOutPut.Text+="\r\n" + " Processed File " + aFileQID[0]  +  " :" + nCount + " of " + m_aStressXML.Length ;
			
				}
			}
			
			catch (IOException ioex)
			{
				MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				txtOutPut.Text+= "\r\n  IO Exception: Check log file for details: " + ioex.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
				return;
			}
			catch (SoapException soapEx)
			{
				MessageBox.Show("SOAP Exception: Check log file for details: " + soapEx.Message);
				txtOutPut.Text+= "\r\n  SOAP Exception: Check log file for details: " + soapEx.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, soapEx.Message + "\r\n" +soapEx.Source + "\r\n" + soapEx.StackTrace				);
				return;
			}
			catch (Exception exx)
			{
				txtOutPut.Text+="\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			}
			#endregion
			
		}
			


		private void StressThreadFunction()
		{
			#region loop
			try
			{
			
				int nCount = 0;
				AppendStressMessage( "\r\n" + " About to start thread WebService.." + Thread.CurrentThread.Name);

				int ctr = 0;
				int iCycles = 0;
				if(chk_Continuous.Checked == false)
				{
					iCycles = int.Parse(txtCycles.Text); 
				}
				while(true)
				{

					bool bDidFire;
					bDidFire= m_StopThread.WaitOne(10,false); //10 millisec wait

					if (bDidFire)
						break; 
					nCount = 0;

					foreach (string  aFileQID in m_aStressXML)
					{
						nCount++;
						string sFileName =  aFileQID[0] + ".xml";
						string sFilePath = m_messageGenPath + "\\" + sFileName;

                        //CallWS(aFileQID,m_thumbPrint,aFileQID,"BLRCUS","ASMAP");	
						CallWS(aFileQID,m_thumbPrint,aFileQID,txtSubsID.Text.Trim(),"ASMAP");	
						//AppendStressMessage( "\r\n" + " Processed File " + aFileQID[0]  +  " :" + nCount + " of " + m_aStressData.Count);

						//bDidFire= m_StopThread.WaitOne(10,false); //10 millisec wait //commented to complete the cycle


						//if (bDidFire)
						//	break;
		
					}

					ctr++;
					if(iCycles >0 )
					{
						if(ctr >= iCycles)
						{
							break;
						}
					}

				}
				AppendStressMessage( Thread.CurrentThread.Name + " exiting on stop - \n (# of Files Processed in this thread - " + nCount.ToString()+" ("+ctr.ToString() +") )" );

			}
			
			catch (IOException ioex)
			{
				//MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				AppendStressMessage( "\r\n  IO Exception: Check log file for details: " + ioex.Message);

				AppendStressMessage( "\r\n  Thread aborts:" + Thread.CurrentThread.Name);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,"\r\n  Thread aborts:" + Thread.CurrentThread.Name);

				return;
			}
			catch (SoapException soapEx)
			{
				//MessageBox.Show("SOAP Exception: Check log file for details: " + soapEx.Message);
				AppendStressMessage( "\r\n  SOAP Exception: Check log file for details: " + soapEx.Message);
				AppendStressMessage( "\r\n  Thread aborts:" + Thread.CurrentThread.Name);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, soapEx.Message + "\r\n" +soapEx.Source + "\r\n" + soapEx.StackTrace				);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,"\r\n  Thread aborts:" + Thread.CurrentThread.Name);

				return;
			}
			catch (Exception exx)
			{
				AppendStressMessage( "\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace);
				AppendStressMessage( "\r\n  Thread aborts:" + Thread.CurrentThread.Name);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,"\r\n  Thread aborts:" + Thread.CurrentThread.Name);

				return;
			}
			#endregion
		
		}

		


		private void CallWS(string sFileName,string Thumbprint, string sMessageID, string SubscriberID, string CopyToId)
		{

            TIREPDB2G.TIREPDB2GUploadParams su = new TIREPDB2G.TIREPDB2GUploadParams();
            byte[] Emptybyte = new byte[0];
            //read encsession key file
            DateTime dtStart, dtEnd;
            dtStart = DateTime.Now;
            try
            {

                string fileName = sFileName + ".3DesEncKey.bin";
                su.ESessionKey = TCHQTest.GetFileContents(fileName);
                su.SubscriberMessageID = sMessageID;
                su.CertificateID = Thumbprint;//GetFileString(txtOriginalSafeTirFile.Text + ".ThumbPrint.txt");

                fileName = sFileName + ".encrypted.bin";
                su.MessageContent = TCHQTest.GetFileContents(fileName);
                //tq.SubscriberID = "FCS";
                su.SubscriberID = SubscriberID;
                su.MessageName = "TIRPreDeclaration";
                su.TimeSent = DateTime.Now;//.ToString("yyyy-MM-ddTHH:mm:ss");
                su.InformationExchangeVersion = "1.0.0";

            }
            catch (IOException ioex)
            {
                MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
                txtOutPut.Text += "\r\n  IO Exception: Check log file for details: " + ioex.Message;
                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" + ioex.Source + "\r\n" + ioex.StackTrace);
                return;
            }

            txtOutPut.Text += "\r\n Files read successfully";
            txtOutPut.Text += "\r\n Before calling Web Service";
            Application.DoEvents();

            TIREPDB2G.TIREPDB2GServiceClass uploadClass = new TIREPDB2G.TIREPDB2GServiceClass();
            TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceInfo, uploadClass.Url);

            TIREPDB2G.TIREPDB2GUploadAck tr;
            //WSST.SafeTIRUploadAck  tr ;
            try
            {
                uploadClass.Timeout = 10000000;
                tr = uploadClass.TIREPDB2G(su);
                txtOutPut.Text += "\r\n After Successful call to Web Service";

            }
            catch (SoapException soapEx)
            {
                MessageBox.Show("SOAP Exception: Check log file for details: " + soapEx.Message);
                txtOutPut.Text += "\r\n  SOAP Exception: Check log file for details: " + soapEx.Message;
                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, soapEx.Message + "\r\n" + soapEx.Source + "\r\n" + soapEx.StackTrace);
                return;
            }
            catch (System.Net.WebException webEx)
            {
                MessageBox.Show("Web Exception: Check log file for details: " + webEx.Message);
                txtOutPut.Text += "\r\n  Web Exception: Check log file for details: " + webEx.Message;
                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, webEx.Message + "\r\n" + webEx.Source + "\r\n" + webEx.StackTrace);
                return;
            }

            Application.DoEvents();
            dtEnd = DateTime.Now;
            TimeSpan ts = dtEnd - dtStart;

            //////////////////////////////////////////////////////////////////

            //WSST.SafeTIRUploadParams tq = new WSST.SafeTIRUploadParams();

            ////read encsession key file
            //    string fileName =sFileName+".3DesEncKey.bin";

            //    tq.ESessionKey = TCHQTest.GetFileContents(fileName);

            //    tq.Sender_MessageID = sMessageID;
            //    tq.MessageTag=Thumbprint ;
			
            //    fileName = sFileName+".encrypted.bin";
            //    tq.safeTIRUploadData = TCHQTest.GetFileContents(fileName);
            //    tq.SubscriberID = SubscriberID;
            //    tq.CopyToID= CopyToId;
				
            //Application.DoEvents();


            //WSST.SafeTirUpload  uploadClass= new WSST.SafeTirUpload();
			
            //DateTime dtStart, dtEnd;
            //WSST.SafeTIRUploadAck  tr ;
            //dtStart = DateTime.Now;

			
            //tr= uploadClass.WSST(tq);
			
            //dtEnd = DateTime.Now;
            //TimeSpan ts = dtEnd- dtStart;
            //Application.DoEvents();

			AppendStressMessage( " \r\n" + Thread.CurrentThread.Name + "|" +sFileName +"|"+ sMessageID + " |The return code is |" + tr.ReturnCode.ToString() +  " |Response time is (ms)|" + ts.TotalMilliseconds.ToString());
		}

		private void chkSaveOpInfile_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkSaveOpInfile.Checked)
			{
				btnSelectFile.Enabled=true;
			}
			else
			{
				btnSelectFile.Enabled=true;
			}
		}

		private void btnSelectFile_Click(object sender, System.EventArgs e)
		{
			
			if (sfd.ShowDialog()==DialogResult.Cancel)
				return;
			txtLogFileName.Text=sfd.FileName;
		}
		

		#region Getting messages from various stress threads

		
		static StringBuilder sbStressMessages = new StringBuilder();

		
		static object SyncLock = new object();

		static  void AppendStressMessage(string sMessage)
		{
			lock(SyncLock)
			{
				sbStressMessages.Append(sMessage);
			
			}
		}

		static  string GetStressMessage()
		{
			lock(SyncLock)
			{
				return sbStressMessages.ToString();
			
			}
		}

		System.Threading.ManualResetEvent m_StopThread;
		#endregion

	

		private void lnk4_LinkClicked_1(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_StopThread.Set();
			tmrStressResults.Stop();
			lnk4.Enabled=false;
			txtEndTime.Text=DateTime.Now.ToLongTimeString();
			txtOutPut.Text=GetStressMessage();
			if (chkSaveOpInfile.Checked)
			{
				StreamWriter sw = new StreamWriter(txtLogFileName.Text,false,System.Text.Encoding.ASCII);
				sw.Write(GetStressMessage());
				sw.Close();
			}	
		}

		private void tmrStressResults_Tick(object sender, System.EventArgs e)
		{
			txtOutPut.Text=GetStressMessage();
		}

		private void chk_Continuous_CheckedChanged(object sender, System.EventArgs e)
		{
			setCyclesControls();
		}

		private void chk_Continuous_CheckStateChanged(object sender, System.EventArgs e)
		{
			setCyclesControls();
		}

		private void setCyclesControls()
		{
			if(chk_Continuous.Checked == false )
			{
				txtCycles.Visible = true;
				lblCycles.Visible = true;
			}
			else
			{
				txtCycles.Visible = false;
				lblCycles.Visible = false;
			}
		}


	}
}
