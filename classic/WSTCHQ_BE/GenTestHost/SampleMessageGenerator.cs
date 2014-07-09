using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS.CryptoInterfaces;
using System.IO;
using System.Text;
using System.Xml;
using IRU.RTS.Crypto;

namespace GenTestHost
{
	/// <summary>
	/// Summary description for SampleMessageGenerator.
	/// </summary>
	public class SampleMessageGenerator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtInFile;
		private System.Windows.Forms.Button cmdGenerateQuery;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtIRUKeyID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SampleMessageGenerator()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtInFile = new System.Windows.Forms.TextBox();
			this.cmdGenerateQuery = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtIRUKeyID = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtIRUKeyID);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmdGenerateQuery);
			this.groupBox1.Controls.Add(this.txtInFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(520, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Generate Query Message";
			// 
			// txtInFile
			// 
			this.txtInFile.Location = new System.Drawing.Point(104, 24);
			this.txtInFile.Name = "txtInFile";
			this.txtInFile.Size = new System.Drawing.Size(256, 20);
			this.txtInFile.TabIndex = 0;
			this.txtInFile.Text = "o:\\bin\\TCHQuery.xml";
			// 
			// cmdGenerateQuery
			// 
			this.cmdGenerateQuery.Location = new System.Drawing.Point(360, 24);
			this.cmdGenerateQuery.Name = "cmdGenerateQuery";
			this.cmdGenerateQuery.Size = new System.Drawing.Size(80, 24);
			this.cmdGenerateQuery.TabIndex = 1;
			this.cmdGenerateQuery.Text = "Generate";
			this.cmdGenerateQuery.Click += new System.EventHandler(this.cmdGenerateQuery_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Input File";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "IRU Key ID";
			// 
			// txtIRUKeyID
			// 
			this.txtIRUKeyID.Location = new System.Drawing.Point(104, 56);
			this.txtIRUKeyID.Name = "txtIRUKeyID";
			this.txtIRUKeyID.Size = new System.Drawing.Size(256, 20);
			this.txtIRUKeyID.TabIndex = 4;
			this.txtIRUKeyID.Text = "84876C0463A38DF2FD64F3C90E1EABF01C77B3ED";
			// 
			// SampleMessageGenerator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 266);
			this.Controls.Add(this.groupBox1);
			this.Name = "SampleMessageGenerator";
			this.Text = "SampleMessageGenerator";
			this.Load += new System.EventHandler(this.SampleMessageGenerator_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



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

		private void cmdGenerateQuery_Click(object sender, System.EventArgs e)
		{
		
			string sQueryXML;

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

			PlugInManager pm;
			pm = new PlugInManager();
			pm.ConfigFile = m_ConfigFile;
			pm.LoadPlugins();


			// extract BodyContents

			string bodyContents;
			int startPos, endPos;
			startPos = sQueryXML.IndexOf("<Body>")+6;
			endPos= sQueryXML.IndexOf("</Body>");
			bodyContents=sQueryXML.Substring(startPos, endPos-startPos);

			byte[] aBodyContents = System.Text.Encoding.Unicode.GetBytes(bodyContents);




			ICryptoOperations ic = 
				(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://192.168.0.4:9012/CryptoProvider.rem");
			
			
			
			
			
		
			Hashtable ht = new Hashtable();
			System.Diagnostics.Debug.WriteLine("Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
			byte[] outputHash =null; ;
			//for (int i=0; i<100 ; i++)
			outputHash  = ic.Hash(aBodyContents,"SHA1", ht);
			System.Diagnostics.Debug.WriteLine(">>Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));

			string hashValue = Convert.ToBase64String(outputHash);



			XmlDocument xd = new XmlDocument();
			xd.PreserveWhitespace=true;
			xd.LoadXml(sQueryXML);
			XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
			xns.AddNamespace("def","http://www.iru.org/TCHQuery");

			xd.DocumentElement.SelectSingleNode("//def:Hash",xns).InnerText=hashValue;
			string sDocwithHash = xd.OuterXml;

			//Encrypt with 3DES


			byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
 

			ht = new Hashtable();
			ht["IV"]= new byte[] {0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa};
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



			// Start encrypting the Key


			// Get Key from DB
			IDBHelperFactory dbf= (IDBHelperFactory)pm.GetPluginByName("DBHelperFactory");


			IDBHelper SubsDBHelper= dbf.GetDBHelper("SubscriberDB");
			int kStatus;
			
			SubsDBHelper.ConnectToDB();
			string encryptionKeyID="--";



			RSACryptoKey rKey = null;

			int nRetValue=-1;
			nRetValue=  KeyManager.GetIRUKeyDetails(txtIRUKeyID.Text ,"FCS",out rKey, SubsDBHelper);
			MessageBox.Show("IRU Keys: Ok case return value ;" + nRetValue.ToString()  + " " + encryptionKeyID);


			
			ic = 
				(ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://192.168.0.4:9012/CryptoProvider.rem");
			
			ht = new Hashtable();
			ht["EXPONENT"]= rKey.Exponent;
			ht["MODULUS"] = rKey.Modulus;
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
			MessageBox.Show("Completed");




		}

		private void SampleMessageGenerator_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
