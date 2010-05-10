using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using IRU.RTS.Crypto;
using IRU.RTS.CommonComponents;
using IRU.RTS.CryptoInterfaces;
using System.Xml;

using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;



namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQStressTester.
	/// </summary>
	public class TCHQStressTester : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.CheckBox chkFileSelected;
		private System.Windows.Forms.CheckBox chkQueryProcessed;
		private System.ComponentModel.IContainer components;

	
		private string m_StressCSV;

		//used by Regex class to extract values
		public string m_RequestNSURN;
		//public string m_ResponseNSURN;


		private string m_IRUCertFile;
		private System.Windows.Forms.FolderBrowserDialog fbd;

		private ArrayList m_aStressData;
		private string m_messageGenPath; //folder 

		private string m_thumbPrint;
		private System.Windows.Forms.LinkLabel lnk3;
		private System.Windows.Forms.LinkLabel lnk2;
		private System.Windows.Forms.LinkLabel lnk1;
		private System.Windows.Forms.Timer tmrStressResults;
		private System.Windows.Forms.LinkLabel lnk4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtStartTime;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox txtEndTime;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.TextBox txtLogFileName;
		private System.Windows.Forms.CheckBox chkSaveOpInfile;
		private System.Windows.Forms.CheckBox chkResponseVerfication;
		private System.Windows.Forms.NumericUpDown numThdCnt;
		private System.Windows.Forms.Label label2;

		private RSAParameters m_rsaParams;


		public TCHQStressTester()
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.txtLogFileName = new System.Windows.Forms.TextBox();
			this.chkSaveOpInfile = new System.Windows.Forms.CheckBox();
			this.chkResponseVerfication = new System.Windows.Forms.CheckBox();
			this.numThdCnt = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtEndTime = new System.Windows.Forms.TextBox();
			this.txtStartTime = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lnk4 = new System.Windows.Forms.LinkLabel();
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
			this.tmrStressResults = new System.Windows.Forms.Timer(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numThdCnt)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtEndTime);
			this.groupBox1.Controls.Add(this.txtStartTime);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lnk4);
			this.groupBox1.Controls.Add(this.lnk3);
			this.groupBox1.Controls.Add(this.chkQueryProcessed);
			this.groupBox1.Controls.Add(this.lnk2);
			this.groupBox1.Controls.Add(this.lnk1);
			this.groupBox1.Controls.Add(this.chkFileSelected);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(680, 288);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnSelectFile);
			this.groupBox3.Controls.Add(this.txtLogFileName);
			this.groupBox3.Controls.Add(this.chkSaveOpInfile);
			this.groupBox3.Controls.Add(this.chkResponseVerfication);
			this.groupBox3.Controls.Add(this.numThdCnt);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(16, 104);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(648, 100);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Enabled = false;
			this.btnSelectFile.Location = new System.Drawing.Point(600, 72);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(40, 24);
			this.btnSelectFile.TabIndex = 24;
			this.btnSelectFile.Text = "...";
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click_1);
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
			this.chkSaveOpInfile.CheckedChanged += new System.EventHandler(this.chkSaveOpInfile_CheckedChanged_1);
			// 
			// chkResponseVerfication
			// 
			this.chkResponseVerfication.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkResponseVerfication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkResponseVerfication.Location = new System.Drawing.Point(16, 32);
			this.chkResponseVerfication.Name = "chkResponseVerfication";
			this.chkResponseVerfication.Size = new System.Drawing.Size(240, 32);
			this.chkResponseVerfication.TabIndex = 21;
			this.chkResponseVerfication.Text = "Perform Response Decryption and Hash Verficiation";
			// 
			// numThdCnt
			// 
			this.numThdCnt.Location = new System.Drawing.Point(248, 8);
			this.numThdCnt.Maximum = new System.Decimal(new int[] {
																	  50,
																	  0,
																	  0,
																	  0});
			this.numThdCnt.Minimum = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.numThdCnt.Name = "numThdCnt";
			this.numThdCnt.TabIndex = 20;
			this.numThdCnt.Value = new System.Decimal(new int[] {
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
			this.label2.Text = "Concurrent Reuqest Threads";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(312, 256);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 24);
			this.label4.TabIndex = 15;
			this.label4.Text = "End Time";
			// 
			// txtEndTime
			// 
			this.txtEndTime.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtEndTime.Enabled = false;
			this.txtEndTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtEndTime.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.txtEndTime.Location = new System.Drawing.Point(360, 256);
			this.txtEndTime.Name = "txtEndTime";
			this.txtEndTime.Size = new System.Drawing.Size(272, 19);
			this.txtEndTime.TabIndex = 14;
			this.txtEndTime.Text = "";
			// 
			// txtStartTime
			// 
			this.txtStartTime.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStartTime.Enabled = false;
			this.txtStartTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStartTime.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.txtStartTime.Location = new System.Drawing.Point(368, 224);
			this.txtStartTime.Name = "txtStartTime";
			this.txtStartTime.Size = new System.Drawing.Size(264, 19);
			this.txtStartTime.TabIndex = 13;
			this.txtStartTime.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(304, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 24);
			this.label3.TabIndex = 12;
			this.label3.Text = "Start Time";
			// 
			// lnk4
			// 
			this.lnk4.Enabled = false;
			this.lnk4.Location = new System.Drawing.Point(24, 256);
			this.lnk4.Name = "lnk4";
			this.lnk4.Size = new System.Drawing.Size(200, 24);
			this.lnk4.TabIndex = 11;
			this.lnk4.TabStop = true;
			this.lnk4.Text = "4: Stop Test";
			this.lnk4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk4_LinkClicked);
			// 
			// lnk3
			// 
			this.lnk3.Enabled = false;
			this.lnk3.Location = new System.Drawing.Point(24, 224);
			this.lnk3.Name = "lnk3";
			this.lnk3.Size = new System.Drawing.Size(200, 24);
			this.lnk3.TabIndex = 7;
			this.lnk3.TabStop = true;
			this.lnk3.Text = "3: Start Test";
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
			this.lnk2.Text = "2: Process  Query Request Messages";
			this.lnk2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// lnk1
			// 
			this.lnk1.Location = new System.Drawing.Point(24, 56);
			this.lnk1.Name = "lnk1";
			this.lnk1.Size = new System.Drawing.Size(168, 24);
			this.lnk1.TabIndex = 4;
			this.lnk1.TabStop = true;
			this.lnk1.Text = "1: Prepare CSV  file with values";
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
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
			this.groupBox2.Location = new System.Drawing.Point(0, 288);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(680, 190);
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
			this.txtOutPut.Size = new System.Drawing.Size(674, 171);
			this.txtOutPut.TabIndex = 0;
			this.txtOutPut.Text = "";
			// 
			// fbd
			// 
			this.fbd.Description = "Select Folder to store the generated XML Messages";
			// 
			// tmrStressResults
			// 
			this.tmrStressResults.Tick += new System.EventHandler(this.tmrStressResults_Tick);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "textBox1";
			// 
			// sfd
			// 
			this.sfd.DefaultExt = "txt";
			this.sfd.FileName = "StressResults.txt";
			this.sfd.Title = "Select file to save stress log.";
			// 
			// TCHQStressTester
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 478);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "TCHQStressTester";
			this.Text = "TCHQStressTester";
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numThdCnt)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			//load file
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.Title="Select .csv file containing test data";
			ofd.Filter="Test Data Files (*.csv)|*.csv";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			try
			{
				txtOutPut.Text+="\r\n" + " Selected data file : " + ofd.FileName;
				m_StressCSV = ofd.FileName;

				TCHQStressTest tcs = new TCHQStressTest();
				m_aStressData= tcs.GetDataFromCSV(m_StressCSV);

				txtOutPut.Text+="\r\n" + " Records found in file : " + m_aStressData.Count;
				chkFileSelected.Checked=true;
				lnk2.Enabled=true;
				
			}
			catch (Exception exx)
			{
				m_aStressData = null;
				m_StressCSV=null;
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
			}
			catch (Exception exx)
			{
				
				m_IRUCertFile=null;
				m_thumbPrint=null;
				
				txtOutPut.Text+="\r\n" + " Exception Occured " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			}			
		
			#endregion


			#region Select folder and generate XML message
						
			if (m_messageGenPath!="")
				fbd.SelectedPath=m_messageGenPath;
			dr = fbd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			try
			{
				txtOutPut.Text+="\r\n" + " Generated Message Folder : " + fbd.SelectedPath;
				m_messageGenPath = fbd.SelectedPath;

				TCHQStressTest tcs = new TCHQStressTest();
				txtOutPut.Text+="\r\n" + " About to start generating message .xml Files";
				tcs.GenerateMsgFiles(fbd.SelectedPath,m_aStressData);

				txtOutPut.Text+="\r\n" + " Completing writing xml files.";
				chkQueryProcessed.Checked=true;
			}
			catch (Exception exx)
			{
				m_aStressData = null;
				m_StressCSV=null;
				txtOutPut.Text+="\r\n" + " Exception Occured while Generating Messages " + exx.Message + "\r\n" + exx.StackTrace;
				return;
			}			
			#endregion

			#region Generate Encrypted File Set

			try
			{
			
				int nCount = 0;
				txtOutPut.Text+="\r\n" + " About to start Encrypting Files";

				foreach (string[] aFileQID in m_aStressData)
				{
					nCount++;
					string sFileName =  aFileQID[0] + ".xml";
					string sFilePath = m_messageGenPath + "\\" + sFileName;
					generateEncFile(sFilePath,m_rsaParams);
					txtOutPut.Text+="\r\n" + " Processed File " + sFilePath  +  " :" + nCount + " of " + m_aStressData.Count;
			
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

				bodyContents = RegExHelper.ExtractBODYContents(sQueryXML);

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

				string sDocwithHash = sQueryXML;
			*/
				//Encrypt with 3DES

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

			if (MessageBox.Show(this,"Start testing with " + numThdCnt.Value.ToString() + " thread(s)?\r\n ","TCHQ Tester", MessageBoxButtons.OKCancel) ==DialogResult.Cancel)
			{
				return;
			}

			#region cleanup before test
			//clear contents
			sbStressMessages = new StringBuilder();
			tmrStressResults.Interval = 1000;
			tmrStressResults.Start();
			lnk4.Enabled=true;
			m_StopThread=new System.Threading.ManualResetEvent(false);

			#endregion


			txtStartTime.Text =DateTime.Now.ToLongTimeString();
			txtEndTime.Text="";
			
			for (int nthreadCount=0; nthreadCount<numThdCnt.Value;nthreadCount++)
			{
				Thread t = new Thread(new ThreadStart(StressThreadFunction));
				t.Name="Stress thread no." + nthreadCount;
				t.IsBackground = true; // terminiates when main thread kills
				t.Start();
			
			}

			lnk4.Enabled=true;
			return; // earlier code for single run below

			#region loop through all files
			try
			{
			
				int nCount = 0;
				txtOutPut.Text+="\r\n" + " About to start calling WebService..";

				foreach (string[] aFileQID in m_aStressData)
				{
					nCount++;
					string sFileName =  aFileQID[0] + ".xml";
					string sFilePath = m_messageGenPath + "\\" + sFileName;
					CallWS(sFilePath,m_thumbPrint,aFileQID[0],aFileQID[1],m_rsaParams, chkResponseVerfication.Checked);			 
						
					txtOutPut.Text+="\r\n" + " Processed File " + aFileQID[0]  +  " :" + nCount + " of " + m_aStressData.Count;
			
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

				while(true)
				{
					bool bDidFire;
					bDidFire= m_StopThread.WaitOne(10,false); //10 millisec wait

					if (bDidFire)
						break; 
					nCount = 0;

					foreach (string[] aFileQID in m_aStressData)
					{
						nCount++;
						string sFileName =  aFileQID[0] + ".xml";
						string sFilePath = m_messageGenPath + "\\" + sFileName;
						CallWS(sFilePath,m_thumbPrint,aFileQID[0],aFileQID[1],m_rsaParams, chkResponseVerfication.Checked);			 
						
						//AppendStressMessage( "\r\n" + " Processed File " + aFileQID[0]  +  " :" + nCount + " of " + m_aStressData.Count);
						bDidFire= m_StopThread.WaitOne(10,false); //10 millisec wait

						if (bDidFire)
							break;
					
			
					}
				}
				AppendStressMessage( Thread.CurrentThread.Name + " exiting on stop");

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
		
		private void CallWS(string sFileName,string Thumbprint, string sQueryID, string SubscriberID, RSAParameters rParams, bool bRespVerification)
		{
			Query.TIRHolderQuery tq = new Query.TIRHolderQuery();

			//read encsession key file

			string fileName =sFileName+".3DesEncKey.bin";

			tq.ESessionKey = TCHQTest.GetFileContents(fileName);

			tq.Query_ID = sQueryID;
			tq.MessageTag=Thumbprint;

			fileName = sFileName+".encrypted.bin";
			tq.TIRCarnetHolderQueryParams = TCHQTest.GetFileContents(fileName);
			tq.SubscriberID = SubscriberID;
		
			Application.DoEvents();

			Query.SafeTIRHolderQueryServiceClass  queryClass= new Query.SafeTIRHolderQueryServiceClass();

			Query.TIRHolderResponse tr ;
			
			DateTime dtStart, dtEnd;

			dtStart = DateTime.Now;

			tr= queryClass.WSTCHQ(tq);
			dtEnd = DateTime.Now;
			TimeSpan ts = dtEnd- dtStart;
			
			AppendStressMessage( " \r\n" + Thread.CurrentThread.Name + "|" +sFileName +"|"+ sQueryID + " |" +   "The Query ID is |" + tr.Query_ID.ToString() + " |The return code is |" + tr.ReturnCode.ToString() + " |The Message Tag is |" +  tr.MessageTag + " |Response time is (ms)|" + ts.TotalMilliseconds.ToString());
			
			if (tr.ReturnCode !=2)
			{
				return;
			}

			if (bRespVerification ==false)
				return;
					AppendStressMessage(" \r\n"  + sQueryID + " -- " + " Starting to process response");

			#region Get Keys
			
			SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["SubscriberDB"]);
			
			string sSelect = "select ENCRYPTION_KEY_ID,MODULUS,EXPONENT,D,P,Q,DP,DQ,INVERSEQ,DISTRIBUTED_TO,DISTRIBUTION_DATE,KEY_ACTIVE,KEY_ACTIVE_REASON,CERT_IS_CURRENT,CERT_EXPIRY_DATE from iru_encryption_keys where encryption_key_id=@KeyID";

			SqlCommand sqlSelect = new SqlCommand(sSelect);

			sqlSelect.Parameters.Add("@KeyID",SqlDbType.VarChar,50);

			sqlSelect.Parameters["@KeyID"].Value= tr.MessageTag;
			
			sqlSelect.Connection= sqlConn;
			
			RSACryptoKey Key = new RSACryptoKey();
			bool bFound=false;

			try
			{
				sqlConn.Open();

				SqlDataReader sdr = sqlSelect.ExecuteReader();

				while (sdr.Read())
				{
					Key.Modulus =    (byte[])sdr["MODULUS"];
					Key.Exponent =    (byte[])sdr["EXPONENT"];
					Key.D =    (byte[])sdr["D"];


					Key.P =    (byte[])sdr["P"];
					Key.Q =    (byte[])sdr["Q"];

					Key.DP =    (byte[])sdr["DP"];

					Key.DQ =    (byte[])sdr["DQ"];

					Key.INVERSEQ =    (byte[])sdr["INVERSEQ"];

					bFound=true;
				}
			}
			catch (Exception exx)
			{
				AppendStressMessage("Exception encountered communicating with SQL Server for keys, Verify connect string in  configuration file \r\n" +System.Configuration.ConfigurationSettings.AppSettings["SubscriberDB"] );
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, exx.Message + "\r\n" +exx.Source + "\r\n" + exx.StackTrace	+ "\r\n" + System.Configuration.ConfigurationSettings.AppSettings["SubscriberDB"]	);
				return;
			}
			finally
			{
				sqlConn.Close();
			}

			if (bFound==false)
			{
				AppendStressMessage(" \r\n"  + sQueryID + " -- " + "No key found in IRU_ENCRYPTION_KEYS table corresponding to " + tq.MessageTag);
				return;
			}

			#endregion

			#region Decrypt message

			ICryptoOperations ic = 
				(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), System.Configuration.ConfigurationSettings.AppSettings["CryptoEndPoint"]);
			
			Hashtable ht2 = new Hashtable();
			ht2["EXPONENT"]= Key.Exponent;
			ht2["MODULUS"] = Key.Modulus;
			ht2["DP"]=Key.DP;
			ht2["DQ"]=Key.DQ;
			ht2["Q"]=Key.Q;
			ht2["P"]=Key.P;
			ht2["INVERSEQ"]=Key.INVERSEQ;
			ht2["D"]=Key.D;
			
			byte[] decoutput=null ;
			
			try
			{
				decoutput = ic.Decrypt(tr.ESessionKey ,"RSA",ht2);
			}
			catch  (Exception crypEx)
			{
				//MessageBox.Show(" \r\n"  + sQueryID + " -- " + "Crypt Exception: Check log file for details: " + crypEx.Message);
				AppendStressMessage(" \r\n"  + sQueryID + " -- " + "Crypt Exception: Check log file for details: " + crypEx.Message);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, crypEx.Message + "\r\n" +crypEx.Source + "\r\n" + crypEx.StackTrace				);
				return;
			}
			
			byte[] decoutputMsg=null ;
			
			Hashtable ht3 = new Hashtable();
			ht3["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
			ht3["KEY"] = decoutput;

			try
			{
				decoutputMsg = ic.Decrypt(tr.TIRCarnetHolderResponseParams,"3DES",ht3);
			}
			catch  (Exception crypEx)
			{
				//MessageBox.Show(" \r\n"  + sQueryID + " -- " + "Crypt Exception: Check log file for details: " + crypEx.Message);
				AppendStressMessage(" \r\n"  + sQueryID + " -- " + "Crypt Exception: Check log file for details: " + crypEx.Message);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, crypEx.Message + "\r\n" +crypEx.Source + "\r\n" + crypEx.StackTrace				);
				return;
			}

			string respXML = System.Text.Encoding.Unicode.GetString(decoutputMsg);

			AppendStressMessage(" \r\n"  + sQueryID + " -- " + "Response Params is : " + respXML);

			Application.DoEvents();
			
			#endregion
			#region hash verification

			/*
			int iHashStart = respXML.IndexOf("<Hash>");

			if(iHashStart >0)
			{
				iHashStart += 6;
			}
			else
			{
				txtOutPut.Text+= " \r\n"  + sQueryID + " -- " + "Hash node not found" ;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, "Hash node not found");
				return;
			}
			int iHashEnd = respXML.IndexOf("</Hash>");
			int iHashLength = iHashEnd - iHashStart;  
			string sHash =  respXML.Substring(iHashStart,iHashLength);  

			*/
			string sHash = RegExHelper.ExtractHASH(respXML);

			AppendStressMessage(" \r\n"  + sQueryID + " -- " + " Response Hash is : " + sHash);
			Application.DoEvents();


			byte [] baHash = Convert.FromBase64String(sHash);

////			int iBodyStart = respXML.IndexOf("<Body>");
////			if(iBodyStart > 0)
////			{
////				iBodyStart += 6;
////			}
////			else
////			{
////				txtOutPut.Text+= " \r\n"  + sQueryID + " -- " + "  Body node not found" ;
////				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, "Body node not found");
////				return;
////			}
////			int iBodyEnd =respXML.IndexOf("</Body>");
////			int iBodyLength = iBodyEnd - iBodyStart ; 
////			string sBody =  respXML.Substring(iBodyStart,iBodyLength );  

			string sBody= RegExHelper.ExtractBODYContents(respXML);
			AppendStressMessage("\r\n  Response Body is : " + sBody);

			AppendStressMessage(" \r\n"  + sQueryID + " -- " + "Response Body is : " + sBody);
			Application.DoEvents();



			byte [] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);

			bool bVerify=false;
			try
			{
			
				bVerify= ic.VerifyHash(baBody,"SHA1", null, baHash);
			}

			catch  (Exception crypEx)
			{
				//MessageBox.Show(" \r\n"  + sQueryID + " -- " + "Hash Exception: Check log file for details: " + crypEx.Message);
				AppendStressMessage(" \r\n"  + sQueryID + " -- " + "Hash Exception: Check log file for details: " + crypEx.Message);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, crypEx.Message + "\r\n" +crypEx.Source + "\r\n" + crypEx.StackTrace				);
				throw;
				
			}
			AppendStressMessage("\r\n  Hash verification result (true/false):" + bVerify.ToString());
			

			#endregion

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

		private void lnk4_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
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

	

	

		private void chkSaveOpInfile_CheckedChanged_1(object sender, System.EventArgs e)
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

		private void btnSelectFile_Click_1(object sender, System.EventArgs e)
		{
				if (sfd.ShowDialog()==DialogResult.Cancel)
				return;
			txtLogFileName.Text=sfd.FileName;

		
		}

	}
}
