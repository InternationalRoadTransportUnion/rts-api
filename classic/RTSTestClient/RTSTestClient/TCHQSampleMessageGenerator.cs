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


namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQSampleMessageGenerator.
	/// </summary>
	public class TCHQSampleMessageGenerator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdGenerateQuery;
		private System.Windows.Forms.TextBox txtInFile;
		private System.Windows.Forms.TextBox txtIRUCertFile;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnSelectCer;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		//used by Regex class to extract values
		public string m_RequestNSURN;
		//public string m_ResponseNSURN;


		public TCHQSampleMessageGenerator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//try to read the last file touched from the parent static variable

			txtIRUCertFile.Text = TestParent.m_IRUCertFilePath;
			
			txtInFile.Text=TestParent.m_MesseageFileName;
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
			this.btnSelectCer = new System.Windows.Forms.Button();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.txtIRUCertFile = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdGenerateQuery = new System.Windows.Forms.Button();
			this.txtInFile = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSelectCer);
			this.groupBox1.Controls.Add(this.btnSelectFile);
			this.groupBox1.Controls.Add(this.txtIRUCertFile);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmdGenerateQuery);
			this.groupBox1.Controls.Add(this.txtInFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(512, 96);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Generate Query Message";
			// 
			// btnSelectCer
			// 
			this.btnSelectCer.Location = new System.Drawing.Point(365, 59);
			this.btnSelectCer.Name = "btnSelectCer";
			this.btnSelectCer.Size = new System.Drawing.Size(24, 16);
			this.btnSelectCer.TabIndex = 10;
			this.btnSelectCer.Text = "...";
			this.btnSelectCer.Click += new System.EventHandler(this.btnSelectCer_Click);
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
			// txtIRUCertFile
			// 
			this.txtIRUCertFile.Location = new System.Drawing.Point(104, 56);
			this.txtIRUCertFile.Name = "txtIRUCertFile";
			this.txtIRUCertFile.Size = new System.Drawing.Size(256, 20);
			this.txtIRUCertFile.TabIndex = 4;
			this.txtIRUCertFile.Text = "IRU.Cer";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "IRU Cert File";
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
			this.cmdGenerateQuery.Location = new System.Drawing.Point(392, 24);
			this.cmdGenerateQuery.Name = "cmdGenerateQuery";
			this.cmdGenerateQuery.Size = new System.Drawing.Size(80, 24);
			this.cmdGenerateQuery.TabIndex = 1;
			this.cmdGenerateQuery.Text = "Generate";
			this.cmdGenerateQuery.Click += new System.EventHandler(this.cmdGenerateQuery_Click);
			// 
			// txtInFile
			// 
			this.txtInFile.Location = new System.Drawing.Point(104, 24);
			this.txtInFile.Name = "txtInFile";
			this.txtInFile.Size = new System.Drawing.Size(256, 20);
			this.txtInFile.TabIndex = 0;
			this.txtInFile.Text = "C:\\ApplicationData\\RussianCustoms\\RTS_Builds\\bin\\TCHQuery.xml";
			// 
			// TCHQSampleMessageGenerator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 102);
			this.Controls.Add(this.groupBox1);
			this.Name = "TCHQSampleMessageGenerator";
			this.Text = "Encrypted Message Generator";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdGenerateQuery_Click(object sender, System.EventArgs e)
		{
			string sQueryXML;

			try
			{	
				//read the orginal file
				StreamReader sr = new StreamReader(txtInFile.Text);
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
				//0x03 0x01  0x04 0x01 0x05 0x09 0x02 0x06
				ht["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
				byte[] aEncMessage  = ic.Encrypt(aDocWithHash,"3DES",ref ht);

				// Op this to File

				string opFileName= txtInFile.Text.Trim()+".encrypted.bin";

				FileStream fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(aEncMessage,0,aEncMessage.Length);
				fs.Close();

				//Write the plain key out

				byte[] a3DesKey = (byte[])ht["KEY"];

				opFileName= txtInFile.Text.Trim()+".3DesKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesKey,0,a3DesKey.Length);
				fs.Close();




				RSAParameters rParams;
				string thumbPrint = "", messages=""; 
				DateTime expiryDate = DateTime.Now;

				CertGenerator cg = new CertGenerator();
				cg.ExtractKeysFromSubscriberCertFile3(txtIRUCertFile.Text,out rParams,out thumbPrint,out messages, out expiryDate);

			
				StreamWriter sw = new StreamWriter(txtInFile.Text.Trim()+".thumbprint.txt",false);
				sw.Write(thumbPrint);
			
				sw.Close();



				// Start encrypting the Key
			
				ht = new Hashtable();
				ht["EXPONENT"]= rParams.Exponent;
				ht["MODULUS"] = rParams.Modulus;
				System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));
				byte[] a3DesEncKey =null; ;
				//for (int i=0; i<100 ; i++)
				a3DesEncKey  = ic.Encrypt(a3DesKey,"RSA",ref ht);
				System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));

				//write encryptedkey to file

				opFileName= txtInFile.Text.Trim()+".3DesEncKey.bin";

				fs = new FileStream(opFileName,FileMode.Create,FileAccess.Write,FileShare.None);

				fs.Write(a3DesEncKey,0,a3DesEncKey.Length);
				fs.Close();
			
				//set the cert file path so it can be used in later activations
				TestParent.m_IRUCertFilePath = txtIRUCertFile.Text;


				MessageBox.Show("Encrypted Message File Set generated.");
				TestParent.m_MesseageFileName = this.txtInFile.Text;



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

		private void btnSelectFile_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.DefaultExt = "xml";
			openFileDialog1.Filter = "XML files (*.xml)|*.xml";
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.Multiselect = false;
			openFileDialog1.Title = "Select Message Input File";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtInFile.Text = openFileDialog1.FileName;
			}
		}

		private void btnSelectCer_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.DefaultExt = "cer";
			openFileDialog1.Filter = "Certificate File (*.cer)|*.cer";
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.Multiselect = false;
			openFileDialog1.Title = "Select Certificate File";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string strCerName = openFileDialog1.FileName;
				//txtIRUCertFile.Text = strCerName.Substring(strCerName.LastIndexOf(@"\")+1);
				txtIRUCertFile.Text = strCerName;
			}
		}
	}
}
