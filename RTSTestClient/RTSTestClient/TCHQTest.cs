using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using TraceHelper;
using IRU.RTS.CryptoInterfaces;
using IRU.RTS.CommonComponents;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQTest.
	/// </summary>
	public class TCHQTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCall;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.TextBox txtOriginalQueryFile;
		private System.Windows.Forms.TextBox txtQueryID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnSelectFile;
		//used by Regex class to extract values
		public string m_RequestNSURN;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtSubsID;
		//public string m_ResponseNSURN;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TCHQTest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			txtOriginalQueryFile.Text= TestParent.m_MesseageFileName;
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
			this.label2 = new System.Windows.Forms.Label();
			this.txtQueryID = new System.Windows.Forms.TextBox();
			this.cmdCall = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtOriginalQueryFile = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtOutPut = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSubsID = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtSubsID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btnSelectFile);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtQueryID);
			this.groupBox1.Controls.Add(this.cmdCall);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtOriginalQueryFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(608, 112);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Location = new System.Drawing.Point(416, 16);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(24, 16);
			this.btnSelectFile.TabIndex = 8;
			this.btnSelectFile.Text = "...";
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 24);
			this.label2.TabIndex = 7;
			this.label2.Text = "Query ID";
			// 
			// txtQueryID
			// 
			this.txtQueryID.Location = new System.Drawing.Point(96, 48);
			this.txtQueryID.MaxLength = 50;
			this.txtQueryID.Name = "txtQueryID";
			this.txtQueryID.Size = new System.Drawing.Size(312, 20);
			this.txtQueryID.TabIndex = 6;
			this.txtQueryID.Text = "FCSQ1";
			// 
			// cmdCall
			// 
			this.cmdCall.Location = new System.Drawing.Point(472, 16);
			this.cmdCall.Name = "cmdCall";
			this.cmdCall.Size = new System.Drawing.Size(96, 24);
			this.cmdCall.TabIndex = 5;
			this.cmdCall.Text = "Call";
			this.cmdCall.Click += new System.EventHandler(this.cmdCall_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "Query File";
			// 
			// txtOriginalQueryFile
			// 
			this.txtOriginalQueryFile.Location = new System.Drawing.Point(96, 16);
			this.txtOriginalQueryFile.MaxLength = 256;
			this.txtOriginalQueryFile.Name = "txtOriginalQueryFile";
			this.txtOriginalQueryFile.Size = new System.Drawing.Size(312, 20);
			this.txtOriginalQueryFile.TabIndex = 3;
			this.txtOriginalQueryFile.Text = "o:\\bin\\TCHQuery.xml";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtOutPut);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 112);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(608, 166);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			// 
			// txtOutPut
			// 
			this.txtOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutPut.Location = new System.Drawing.Point(3, 16);
			this.txtOutPut.Multiline = true;
			this.txtOutPut.Name = "txtOutPut";
			this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutPut.Size = new System.Drawing.Size(602, 147);
			this.txtOutPut.TabIndex = 0;
			this.txtOutPut.Text = "-";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Subscriber ID";
			// 
			// txtSubsID
			// 
			this.txtSubsID.Location = new System.Drawing.Point(96, 80);
			this.txtSubsID.Name = "txtSubsID";
			this.txtSubsID.Size = new System.Drawing.Size(312, 20);
			this.txtSubsID.TabIndex = 10;
			this.txtSubsID.Text = "FCS";
			// 
			// TCHQTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 278);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "TCHQTest";
			this.Text = "TCHQ WS Tester";
			this.Load += new System.EventHandler(this.TCHQTest_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdCall_Click(object sender, System.EventArgs e)
		{
			Query.TIRHolderQuery tq = new Query.TIRHolderQuery();
			

			string op = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");


            
			//read encsession key file
			try
			{

				string fileName =txtOriginalQueryFile.Text+".3DesEncKey.bin";

				tq.ESessionKey = GetFileContents(fileName);

				tq.Query_ID = txtQueryID.Text;
				tq.MessageTag=GetFileString( txtOriginalQueryFile.Text+".ThumbPrint.txt");
			
				fileName = txtOriginalQueryFile.Text+".encrypted.bin";
				tq.TIRCarnetHolderQueryParams = GetFileContents(fileName);
				//tq.SubscriberID = "FCS";
				tq.SubscriberID = txtSubsID.Text.Trim(); 
			}
			catch (IOException ioex)
			{
				MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				txtOutPut.Text+= "\r\n  IO Exception: Check log file for details: " + ioex.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
				return;
			}
			
			txtOutPut.Text+= "\r\n Files read successfully";
			txtOutPut.Text+= "\r\n Before calling Web Service";
			Application.DoEvents();


			Query.SafeTIRHolderQueryServiceClass  queryClass= new Query.SafeTIRHolderQueryServiceClass();
			

			Query.TIRHolderResponse tr ;
			try
			{
				tr= queryClass.WSTCHQ(tq);
				txtOutPut.Text+= "\r\n After Successful call to Web Service";
				
			}
			catch (SoapException soapEx)
			{
				MessageBox.Show("SOAP Exception: Check log file for details: " + soapEx.Message);
				txtOutPut.Text+= "\r\n  SOAP Exception: Check log file for details: " + soapEx.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, soapEx.Message + "\r\n" +soapEx.Source + "\r\n" + soapEx.StackTrace				);
				return;
			}
			catch (System.Net.WebException webEx)
			{
				MessageBox.Show("Web Exception: Check log file for details: " + webEx.Message);
				txtOutPut.Text+= "\r\n  Web Exception: Check log file for details: " + webEx.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, webEx.Message + "\r\n" +webEx.Source + "\r\n" + webEx.StackTrace				);
				return;
			}
			Application.DoEvents();

			txtOutPut.Text += "\r\n The return code is :" + tr.ReturnCode.ToString();
			txtOutPut.Text += "\r\n The Query ID is :" + tr.Query_ID.ToString();
			txtOutPut.Text += "\r\n The Message Tag is :" +  tr.MessageTag ;
			
			if (tr.ReturnCode !=2)
			{
			
				txtOutPut.Text += "\r\n Completed with errors";
				return;
			}
			txtOutPut.Text += "\r\n Starting to process response";



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
				MessageBox.Show(this,"Exception encountered communicating with SQL Server for keys, Verify connect string in  configuration file \r\n" +System.Configuration.ConfigurationSettings.AppSettings["SubscriberDB"] );
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, exx.Message + "\r\n" +exx.Source + "\r\n" + exx.StackTrace	+ "\r\n" + System.Configuration.ConfigurationSettings.AppSettings["SubscriberDB"]	);
				return;
			}
			finally
			{
				sqlConn.Close();
			}

			if (bFound==false)
			{
				txtOutPut.Text += "\r\n No key found in IRU_ENCRYPTION_KEYS table corresponding to " + tr.MessageTag;
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
				MessageBox.Show("Crypt Exception: Check log file for details: " + crypEx.Message);
				txtOutPut.Text+= "\r\n  Crypt Exception: Check log file for details: " + crypEx.Message;
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
				MessageBox.Show("Crypt Exception: Check log file for details: " + crypEx.Message);
				txtOutPut.Text+= "\r\n  Crypt Exception: Check log file for details: " + crypEx.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, crypEx.Message + "\r\n" +crypEx.Source + "\r\n" + crypEx.StackTrace				);
				return;
			}

			string respXML = System.Text.Encoding.Unicode.GetString(decoutputMsg);

			txtOutPut.Text+= "\r\n  Response Params is : " + respXML;

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
				txtOutPut.Text+= "\r\n  Hash node not found" ;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, "Hash node not found");
				return;
			}
			int iHashEnd = respXML.IndexOf("</Hash>");
			int iHashLength = iHashEnd - iHashStart;  
			string sHash =  respXML.Substring(iHashStart,iHashLength);		
			*/
			
			string sHash = RegExHelper.ExtractHASH(respXML);

			txtOutPut.Text+= "\r\n  Response Hash is : " + sHash;
			Application.DoEvents();


			byte [] baHash = Convert.FromBase64String(sHash);
/*
			int iBodyStart = respXML.IndexOf("<Body>");
			if(iBodyStart > 0)
			{
				iBodyStart += 6;
			}
			else
			{
				txtOutPut.Text+= "\r\n  Body node not found" ;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, "Body node not found");
				return;
			}
			int iBodyEnd =respXML.IndexOf("</Body>");
			int iBodyLength = iBodyEnd - iBodyStart ; 
			string sBody =  respXML.Substring(iBodyStart,iBodyLength );  
*/

			string sBody= RegExHelper.ExtractBODYContents(respXML);
			txtOutPut.Text+= "\r\n  Response Body is : " + sBody;
			Application.DoEvents();



			byte [] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);

			bool bVerify=false;
			try
			{
			
				bVerify= ic.VerifyHash(baBody,"SHA1", null, baHash);
			}

			catch  (Exception crypEx)
			{
				MessageBox.Show("Hash Exception: Check log file for details: " + crypEx.Message);
				txtOutPut.Text+= "\r\n  Hash Exception: Check log file for details: " + crypEx.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, crypEx.Message + "\r\n" +crypEx.Source + "\r\n" + crypEx.StackTrace				);
				return;
			}
			txtOutPut.Text+= "\r\n  Hash verification result (true/false):" + bVerify.ToString();
			

			#endregion

			


		}

		internal  static string GetFileString(string FilePath)
		{
			string sFileContents;
			StreamReader sr = new StreamReader(FilePath);
			try
			{
				sFileContents = sr.ReadToEnd();
				
			}
			finally
			{
				sr.Close();
			}
			return sFileContents;
		
		}

		internal static byte[] GetFileContents(string FilePath)
		{
			byte[] afileContents ;
			FileStream fs = File.OpenRead(FilePath);
			try
			{
				afileContents = new byte[fs.Length];
				fs.Read(afileContents,0,afileContents.Length);
			}
			finally
			{
				fs.Close();
			}
			return afileContents;
		}

		private void TCHQTest_Load(object sender, System.EventArgs e)
		{
		
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
				txtOriginalQueryFile.Text = openFileDialog1.FileName;
			}

		}
	}


}
