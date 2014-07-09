using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Xml;
using IRU.RTS.Crypto;
using IRU.RTS.CryptoInterfaces;
using System.IO;


namespace GenTestHost
{
	/// <summary>
	/// Summary description for CommonComponentsTest.
	/// </summary>
	public class CommonComponentsTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpIDHelper;
		private System.Windows.Forms.TextBox txtIDTOGen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDBnameIDH;
		private System.Windows.Forms.Button cmdGenID;
		private System.Windows.Forms.GroupBox gpfileCache;
		private System.Windows.Forms.Button cmdLoadAndShow;
		private System.Windows.Forms.Button cmdKeyManager;
		private System.Windows.Forms.TextBox txtKeyID;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdRemoteCryptoCall;
		private System.Windows.Forms.Button cmdRSA;
		private System.Windows.Forms.Button cmdGetSubsKeys;
		private System.Windows.Forms.Button cmdHashtest;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cmdValidateXSD;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CommonComponentsTest()
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
				this.Text= "Common Comp tester:" +  m_ConfigFile;
			}
		
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gpIDHelper = new System.Windows.Forms.GroupBox();
			this.cmdGenID = new System.Windows.Forms.Button();
			this.txtDBnameIDH = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIDTOGen = new System.Windows.Forms.TextBox();
			this.gpfileCache = new System.Windows.Forms.GroupBox();
			this.cmdLoadAndShow = new System.Windows.Forms.Button();
			this.cmdKeyManager = new System.Windows.Forms.Button();
			this.txtKeyID = new System.Windows.Forms.TextBox();
			this.cmdGetSubsKeys = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdHashtest = new System.Windows.Forms.Button();
			this.cmdRSA = new System.Windows.Forms.Button();
			this.cmdRemoteCryptoCall = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdValidateXSD = new System.Windows.Forms.Button();
			this.gpIDHelper.SuspendLayout();
			this.gpfileCache.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpIDHelper
			// 
			this.gpIDHelper.Controls.Add(this.cmdGenID);
			this.gpIDHelper.Controls.Add(this.txtDBnameIDH);
			this.gpIDHelper.Controls.Add(this.label2);
			this.gpIDHelper.Controls.Add(this.label1);
			this.gpIDHelper.Controls.Add(this.txtIDTOGen);
			this.gpIDHelper.Location = new System.Drawing.Point(8, 8);
			this.gpIDHelper.Name = "gpIDHelper";
			this.gpIDHelper.Size = new System.Drawing.Size(696, 88);
			this.gpIDHelper.TabIndex = 0;
			this.gpIDHelper.TabStop = false;
			this.gpIDHelper.Text = "IDHelper";
			// 
			// cmdGenID
			// 
			this.cmdGenID.Location = new System.Drawing.Point(320, 32);
			this.cmdGenID.Name = "cmdGenID";
			this.cmdGenID.Size = new System.Drawing.Size(72, 24);
			this.cmdGenID.TabIndex = 4;
			this.cmdGenID.Text = "Generate";
			this.cmdGenID.Click += new System.EventHandler(this.cmdGenID_Click);
			// 
			// txtDBnameIDH
			// 
			this.txtDBnameIDH.Location = new System.Drawing.Point(104, 56);
			this.txtDBnameIDH.Name = "txtDBnameIDH";
			this.txtDBnameIDH.Size = new System.Drawing.Size(120, 20);
			this.txtDBnameIDH.TabIndex = 3;
			this.txtDBnameIDH.Text = "TCHQDB";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "DBName";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "ID to Generate";
			// 
			// txtIDTOGen
			// 
			this.txtIDTOGen.Location = new System.Drawing.Point(104, 24);
			this.txtIDTOGen.Name = "txtIDTOGen";
			this.txtIDTOGen.Size = new System.Drawing.Size(120, 20);
			this.txtIDTOGen.TabIndex = 0;
			this.txtIDTOGen.Text = "TCHQ_ID";
			// 
			// gpfileCache
			// 
			this.gpfileCache.Controls.Add(this.cmdLoadAndShow);
			this.gpfileCache.Location = new System.Drawing.Point(16, 104);
			this.gpfileCache.Name = "gpfileCache";
			this.gpfileCache.Size = new System.Drawing.Size(192, 64);
			this.gpfileCache.TabIndex = 1;
			this.gpfileCache.TabStop = false;
			this.gpfileCache.Text = "FileCache";
			// 
			// cmdLoadAndShow
			// 
			this.cmdLoadAndShow.Location = new System.Drawing.Point(24, 24);
			this.cmdLoadAndShow.Name = "cmdLoadAndShow";
			this.cmdLoadAndShow.Size = new System.Drawing.Size(128, 24);
			this.cmdLoadAndShow.TabIndex = 0;
			this.cmdLoadAndShow.Text = "Load and Show";
			this.cmdLoadAndShow.Click += new System.EventHandler(this.cmdLoadAndShow_Click);
			// 
			// cmdKeyManager
			// 
			this.cmdKeyManager.Location = new System.Drawing.Point(504, 112);
			this.cmdKeyManager.Name = "cmdKeyManager";
			this.cmdKeyManager.Size = new System.Drawing.Size(152, 24);
			this.cmdKeyManager.TabIndex = 2;
			this.cmdKeyManager.Text = "IRU KeyDBHelper";
			this.cmdKeyManager.Click += new System.EventHandler(this.cmdKeyManager_Click);
			// 
			// txtKeyID
			// 
			this.txtKeyID.Location = new System.Drawing.Point(224, 112);
			this.txtKeyID.Name = "txtKeyID";
			this.txtKeyID.Size = new System.Drawing.Size(264, 20);
			this.txtKeyID.TabIndex = 3;
			this.txtKeyID.Text = "5481F8C6EC384D95DB7ECD5CCD8C7397C7F727EA";
			// 
			// cmdGetSubsKeys
			// 
			this.cmdGetSubsKeys.Location = new System.Drawing.Point(504, 144);
			this.cmdGetSubsKeys.Name = "cmdGetSubsKeys";
			this.cmdGetSubsKeys.Size = new System.Drawing.Size(152, 24);
			this.cmdGetSubsKeys.TabIndex = 4;
			this.cmdGetSubsKeys.Text = "Subs Keys DB Helper";
			this.cmdGetSubsKeys.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdHashtest);
			this.groupBox1.Controls.Add(this.cmdRSA);
			this.groupBox1.Controls.Add(this.cmdRemoteCryptoCall);
			this.groupBox1.Location = new System.Drawing.Point(16, 184);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(688, 96);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Remote Crypto";
			// 
			// cmdHashtest
			// 
			this.cmdHashtest.Location = new System.Drawing.Point(376, 24);
			this.cmdHashtest.Name = "cmdHashtest";
			this.cmdHashtest.Size = new System.Drawing.Size(144, 32);
			this.cmdHashtest.TabIndex = 2;
			this.cmdHashtest.Text = "Hash";
			this.cmdHashtest.Click += new System.EventHandler(this.cmdHashtest_Click);
			// 
			// cmdRSA
			// 
			this.cmdRSA.Location = new System.Drawing.Point(208, 24);
			this.cmdRSA.Name = "cmdRSA";
			this.cmdRSA.Size = new System.Drawing.Size(152, 32);
			this.cmdRSA.TabIndex = 1;
			this.cmdRSA.Text = "Crypt: RSA";
			this.cmdRSA.Click += new System.EventHandler(this.cmdRSA_Click);
			// 
			// cmdRemoteCryptoCall
			// 
			this.cmdRemoteCryptoCall.Location = new System.Drawing.Point(40, 24);
			this.cmdRemoteCryptoCall.Name = "cmdRemoteCryptoCall";
			this.cmdRemoteCryptoCall.Size = new System.Drawing.Size(152, 32);
			this.cmdRemoteCryptoCall.TabIndex = 0;
			this.cmdRemoteCryptoCall.Text = "Crypt:3DES Enc";
			this.cmdRemoteCryptoCall.Click += new System.EventHandler(this.cmdRemoteCryptoCall_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmdValidateXSD);
			this.groupBox2.Location = new System.Drawing.Point(16, 288);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(688, 88);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "XMLValidations";
			// 
			// cmdValidateXSD
			// 
			this.cmdValidateXSD.Location = new System.Drawing.Point(48, 32);
			this.cmdValidateXSD.Name = "cmdValidateXSD";
			this.cmdValidateXSD.Size = new System.Drawing.Size(144, 32);
			this.cmdValidateXSD.TabIndex = 0;
			this.cmdValidateXSD.Text = "Validate XML";
			this.cmdValidateXSD.Click += new System.EventHandler(this.cmdValidateXSD_Click);
			// 
			// CommonComponentsTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(720, 382);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cmdGetSubsKeys);
			this.Controls.Add(this.txtKeyID);
			this.Controls.Add(this.cmdKeyManager);
			this.Controls.Add(this.gpfileCache);
			this.Controls.Add(this.gpIDHelper);
			this.Name = "CommonComponentsTest";
			this.Text = "CommonComponentsTest";
			this.Load += new System.EventHandler(this.CommonComponentsTest_Load);
			this.gpIDHelper.ResumeLayout(false);
			this.gpfileCache.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CommonComponentsTest_Load(object sender, System.EventArgs e)
		{
		
		}

		private void cmdGenID_Click(object sender, System.EventArgs e)
		{
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			IDBHelperFactory dbf= (IDBHelperFactory)pm.GetPluginByName("DBHelperFactory");
			IDBHelper tCHQDBHelper= dbf.GetDBHelper(txtDBnameIDH.Text);
			MessageBox.Show( IDHelper.GenerateID("TCHQ_ID",tCHQDBHelper).ToString());
			pm.Unload();



			

			
			

		}

		private void cmdLoadAndShow_Click(object sender, System.EventArgs e)
		{
		
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			ICache cache= (ICache)pm.GetPluginByName("InMemoryCache");
			string sContents = cache.GetStringFromCache("tchqquery");
			XmlDocument xDoc;

			xDoc = cache.GetXMLDomFromCache("tchqquery2");


			MessageBox.Show( sContents);

			MessageBox.Show( xDoc.InnerXml);
			pm.Unload();

		}

		private void cmdStartWatching_Click(object sender, System.EventArgs e)
		{
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			
			
		
		}

		private void cmdKeyManager_Click(object sender, System.EventArgs e)
		{
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			IDBHelperFactory dbf= (IDBHelperFactory)pm.GetPluginByName("DBHelperFactory");


			IDBHelper SubsDBHelper= dbf.GetDBHelper("SubscriberDB");
			int kStatus;
			RSACryptoKey rKey;
			SubsDBHelper.ConnectToDB();

			int nRetValue=  KeyManager.GetIRUKeyDetails(txtKeyID.Text,"FCS",out rKey, SubsDBHelper);
			MessageBox.Show("Ok case return value ;" + nRetValue.ToString());



			nRetValue=  KeyManager.GetIRUKeyDetails(txtKeyID.Text,"JAmes",out rKey, SubsDBHelper);
			MessageBox.Show("Subscriber Mismatch case return value ;" + nRetValue.ToString());

			nRetValue=  KeyManager.GetIRUKeyDetails("jnk","JAmes",out rKey, SubsDBHelper);
			MessageBox.Show("Key Missing case return value ;" + nRetValue.ToString());


			
			
			pm.Unload();
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			IDBHelperFactory dbf= (IDBHelperFactory)pm.GetPluginByName("DBHelperFactory");


			IDBHelper SubsDBHelper= dbf.GetDBHelper("SubscriberDB");
			int kStatus;
			RSACryptoKey rKey;
			SubsDBHelper.ConnectToDB();
			string encryptionKeyID="--";

			int nRetValue=  KeyManager.AssignSubscriberKey( "FCS",out rKey,out encryptionKeyID, SubsDBHelper);
			MessageBox.Show("Ok case return value ;" + nRetValue.ToString()  + " " + encryptionKeyID);



			nRetValue=  KeyManager.AssignSubscriberKey( "FCS1",out rKey,out encryptionKeyID ,SubsDBHelper);
			MessageBox.Show("Missing subs Subscriber case return value ;" + nRetValue.ToString() + " " + encryptionKeyID);

			MessageBox.Show("Set the key inactive now");

			nRetValue=  KeyManager.AssignSubscriberKey( "FCS",out rKey,out encryptionKeyID, SubsDBHelper);
			MessageBox.Show("Inactive Key Missing case return value ;" + nRetValue.ToString() + " " + encryptionKeyID);


			
			
			pm.Unload();
		
		}

		private void cmdRemoteCryptoCall_Click(object sender, System.EventArgs e)
		{
		
				ICryptoOperations ic = 

					(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://localhost:9012/CryptoProvider.rem");
				byte[] bData = System.Text.Encoding.Unicode.GetBytes("Data");
				Hashtable ht = new Hashtable();
				ht["IV"]= new byte[] {0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa};
				byte[] output  = ic.Encrypt(bData,"3DES",ref ht);

				MessageBox.Show(System.Convert.ToBase64String((byte[])ht["Key"]));
				byte[] decoutput = ic.Decrypt(output,"3DES",ht);

				string sDecoded = System.Text.Encoding.Unicode.GetString(decoutput);

				MessageBox.Show("Decrypted string :" + sDecoded);



		}

		private void cmdRSA_Click(object sender, System.EventArgs e)
		{
 
			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();
			IDBHelperFactory dbf= (IDBHelperFactory)pm.GetPluginByName("DBHelperFactory");


			IDBHelper SubsDBHelper= dbf.GetDBHelper("SubscriberDB");
			int kStatus;
			RSACryptoKey rKey;
			SubsDBHelper.ConnectToDB();
			string encryptionKeyID="--";

			int nRetValue=  KeyManager.AssignSubscriberKey( "FCS",out rKey,out encryptionKeyID, SubsDBHelper);
			MessageBox.Show("Subs Keys :Ok case return value ;" + nRetValue.ToString()  + " " + encryptionKeyID);

			
			ICryptoOperations ic = 
				(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://192.168.0.4:9012/CryptoProvider.rem");
			byte[] bData = System.Text.Encoding.Unicode.GetBytes("Data");
			Hashtable ht = new Hashtable();
			ht["EXPONENT"]= rKey.Exponent;
			ht["MODULUS"] = rKey.Modulus;
			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
			byte[] output =null; ;
			for (int i=0; i<100 ; i++)
				output  = ic.Encrypt(bData,"RSA",ref ht);
			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));

			// Start decrypting

			nRetValue=  KeyManager.GetIRUKeyDetails(encryptionKeyID ,"FCS",out rKey, SubsDBHelper);
			MessageBox.Show("IRU Keys: Ok case return value ;" + nRetValue.ToString()  + " " + encryptionKeyID);


		
			Hashtable ht2 = new Hashtable();
			ht2["EXPONENT"]= rKey.Exponent;
			ht2["MODULUS"] = rKey.Modulus;
			ht2["DP"]=rKey.DP;
			ht2["DQ"]=rKey.DQ;
			ht2["Q"]=rKey.Q;
			ht2["P"]=rKey.P;
			ht2["INVERSEQ"]=rKey.INVERSEQ;
			ht2["D"]=rKey.D;
			
			byte[] decoutput=null ;
			try
			{

				System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
				for (int i=0; i<100 ; i++)
				 decoutput = ic.Decrypt(output,"RSA",ht2);
			}
			catch (Exception ess)
			{
				MessageBox.Show(ess.Message);
				decoutput=System.Text.Encoding.Unicode.GetBytes("did not work");
			}

			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
			string sDecoded = System.Text.Encoding.Unicode.GetString(decoutput);

			MessageBox.Show("Decrypted string :" + sDecoded);
		
		}

		private void cmdHashtest_Click(object sender, System.EventArgs e)
		{


			ICryptoOperations ic = 
				(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://192.168.0.4:9012/CryptoProvider.rem");
			
			//read file contents

			StreamReader sr = new StreamReader("CommonInterfaces.xml");
			string sData ;
			try
			{
				sData = sr.ReadToEnd();
			}
			finally
			{
				sr.Close();
			}

			
			
			
			byte[] bData = System.Text.Encoding.Unicode.GetBytes(sData);
			Hashtable ht = new Hashtable();
			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
			byte[] outputHash =null; ;
			for (int i=0; i<100 ; i++)
				outputHash  = ic.Hash(bData,"SHA1", ht);
			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));

			// Start verifying
			
			bool bVerified=false;
			try
			{

				System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
				for (int i=0; i<100 ; i++)
					bVerified = ic.VerifyHash(bData,"SHA1", ht,outputHash);
			}
			catch (Exception ess)
			{
				MessageBox.Show(ess.Message);
				
			}

			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
			;

			MessageBox.Show("Hash verification string :" + bVerified.ToString());
		
		}

		private void cmdValidateXSD_Click(object sender, System.EventArgs e)
		{
			string sSampleForFile = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes("Data"));


			XMLValidationHelper.PopulateSchemas("http://www.iru.org/TCHQuery","schemas\\TCHQuery.xsd");
			//XMLValidationHelper.PopulateSchemas("http://www.iru.org/TTCHQuery","schemas\\TTCHQuery.xsd");
			XMLValidationHelper xv = new XMLValidationHelper();
			string ValidationResult;

			StreamReader sr = new StreamReader("TCHQuery_Bad.xml");
			string sXMl = sr.ReadToEnd();
			sr.Close();


			bool bResult = xv.ValidateXML(sXMl,out ValidationResult);

			int iTCHQueryXmlErrCode =0;
			
			if(!bResult)
			{
				iTCHQueryXmlErrCode = GetTCHQueryXMLErrorCode(ValidationResult, out ValidationResult);	
			}
			MessageBox.Show(this,bResult.ToString() + "  " + ValidationResult,"XML Validation Result");
		}

		private int GetTCHQueryXMLErrorCode(string sValidationResult, out string sNewValidationResult)
		{
			int iTCHQueryInvalidReasonNo = 0;
			string sErrNode ="";
			sNewValidationResult = "";
			if(sValidationResult.Trim().Length >0)
			{
				string sFind = "TCHQuery:";
				int pos1=0, pos2=0,iNoOfchars =0;
				pos1 = sValidationResult.LastIndexOf(sFind);
				if(pos1>=0)
				{
					pos2 = sValidationResult.IndexOf("'",pos1);
					iNoOfchars = pos2-pos1;
					sErrNode = sValidationResult.Substring(pos1+sFind.Length,iNoOfchars-sFind.Length); 
				}
				else
				{
					sNewValidationResult = sValidationResult ;
				}

				iTCHQueryInvalidReasonNo = 1200;
				#region Sample XML
				//			<Query xmlns="http://www.iru.org/TCHQuery">
				//				<Envelope>
				//					<Hash>RABhAHQAYQA=</Hash>
				//				</Envelope>
				//				<Body>
				//				<Sender>FCS</Sender>
				//				<SentTime>2004-05-19T13:54:50Z</SentTime>
				//				<Originator>Originator1</Originator/>
				//				<OriginTime>2004-05-19T13:54:50Z</OriginTime>
				//				<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
				//				<Query_Type>1</Query_Type>
				//				<Query_Reason>1</Query_Reason>
				//				<Carnet_Number>15042217</Carnet_Number>
				//				</Body>
				//			</Query>
				#endregion

				if(sErrNode.Trim().ToUpper() == "ENVELOPE")
				{
					iTCHQueryInvalidReasonNo = 1200;
				}
				else if(sErrNode.Trim().ToUpper() == "SENDER")
				{
					iTCHQueryInvalidReasonNo = 1200;
				}
				else if(sErrNode.Trim().ToUpper() == "SENTTIME")
				{
					iTCHQueryInvalidReasonNo =1234;
				}
				else if(sErrNode.Trim().ToUpper() == "ORIGINATOR")
				{
					iTCHQueryInvalidReasonNo = 1236;
				}
				else if(sErrNode.Trim().ToUpper() == "ORIGINTIME")
				{
					iTCHQueryInvalidReasonNo = 1237;
				}
				else if(sErrNode.Trim().ToUpper() == "PASSWORD")
				{
					iTCHQueryInvalidReasonNo = 1200;
				}
				else if(sErrNode.Trim().ToUpper() == "QUERY_TYPE")
				{
					iTCHQueryInvalidReasonNo = 1239;
				}
				else if(sErrNode.Trim().ToUpper() == "QUERY_REASON")
				{
					iTCHQueryInvalidReasonNo = 1240;
				}
				else if(sErrNode.Trim().ToUpper() == "CARNET_NUMBER")
				{
					iTCHQueryInvalidReasonNo  =1241;
				}

				if(pos1>=0)
				{
					sNewValidationResult = sValidationResult + " (Node:" + sErrNode + " - ErrorCode:"+iTCHQueryInvalidReasonNo.ToString().Trim() + ")" ;
				}
			}
			return iTCHQueryInvalidReasonNo ;
		}
	}
}
