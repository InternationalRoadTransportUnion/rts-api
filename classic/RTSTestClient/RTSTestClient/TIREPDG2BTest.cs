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

using System.Xml;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TIREPDG2BTest.
	/// </summary>
	public class TIREPDG2BTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCall;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.TextBox txtOriginalSafeTirFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtMessageID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSubscriberID;
        private RadioButton rdbCertEmpty;
        private RadioButton rdbCertNotEmpty;
        private Label lblCertID;
        private RadioButton rdbEsessionKeyEmpty;
        private RadioButton rdbEsessionKeyNotEmpty;
        private Label lblTimeSent;
        private TextBox txtMessageName;
        private Label lblMessageName;
        private TextBox txtExchangeVersion;
        private Label lblExchangeVer;
        private Label label3;
        private DateTimePicker dtpck_TimeSent;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public TIREPDG2BTest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            System.Net.ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			txtOriginalSafeTirFile.Text= TestParent.m_MesseageFileName;
             
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="policyErrors"></param>
        /// <returns></returns>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain,
                                             SslPolicyErrors policyErrors)
        {
            //Note: You need an AppSettings key called IgnoreSslErrors in the web.config
            if (Convert.ToBoolean("True"))
            {
                //Allow expired or untrusted certificate. Used for test servers that use self-signed certs.
                return true;
            }
            else
            {
                return policyErrors == SslPolicyErrors.None;
            }
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdbEsessionKeyNotEmpty = new System.Windows.Forms.RadioButton();
            this.rdbEsessionKeyEmpty = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbCertNotEmpty = new System.Windows.Forms.RadioButton();
            this.rdbCertEmpty = new System.Windows.Forms.RadioButton();
            this.dtpck_TimeSent = new System.Windows.Forms.DateTimePicker();
            this.lblTimeSent = new System.Windows.Forms.Label();
            this.txtMessageName = new System.Windows.Forms.TextBox();
            this.lblMessageName = new System.Windows.Forms.Label();
            this.txtExchangeVersion = new System.Windows.Forms.TextBox();
            this.lblExchangeVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCertID = new System.Windows.Forms.Label();
            this.txtSubscriberID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessageID = new System.Windows.Forms.TextBox();
            this.cmdCall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOriginalSafeTirFile = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.dtpck_TimeSent);
            this.groupBox1.Controls.Add(this.lblTimeSent);
            this.groupBox1.Controls.Add(this.txtMessageName);
            this.groupBox1.Controls.Add(this.lblMessageName);
            this.groupBox1.Controls.Add(this.txtExchangeVersion);
            this.groupBox1.Controls.Add(this.lblExchangeVer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCertID);
            this.groupBox1.Controls.Add(this.txtSubscriberID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnSelectFile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMessageID);
            this.groupBox1.Controls.Add(this.cmdCall);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOriginalSafeTirFile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 328);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdbEsessionKeyNotEmpty);
            this.groupBox4.Controls.Add(this.rdbEsessionKeyEmpty);
            this.groupBox4.Location = new System.Drawing.Point(120, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 30);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            // 
            // rdbEsessionKeyNotEmpty
            // 
            this.rdbEsessionKeyNotEmpty.AutoSize = true;
            this.rdbEsessionKeyNotEmpty.Checked = true;
            this.rdbEsessionKeyNotEmpty.Location = new System.Drawing.Point(6, 8);
            this.rdbEsessionKeyNotEmpty.Name = "rdbEsessionKeyNotEmpty";
            this.rdbEsessionKeyNotEmpty.Size = new System.Drawing.Size(74, 17);
            this.rdbEsessionKeyNotEmpty.TabIndex = 16;
            this.rdbEsessionKeyNotEmpty.TabStop = true;
            this.rdbEsessionKeyNotEmpty.Text = "Not Empty";
            this.rdbEsessionKeyNotEmpty.UseVisualStyleBackColor = true;
            // 
            // rdbEsessionKeyEmpty
            // 
            this.rdbEsessionKeyEmpty.AutoSize = true;
            this.rdbEsessionKeyEmpty.Location = new System.Drawing.Point(154, 9);
            this.rdbEsessionKeyEmpty.Name = "rdbEsessionKeyEmpty";
            this.rdbEsessionKeyEmpty.Size = new System.Drawing.Size(54, 17);
            this.rdbEsessionKeyEmpty.TabIndex = 17;
            this.rdbEsessionKeyEmpty.Text = "Empty";
            this.rdbEsessionKeyEmpty.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbCertNotEmpty);
            this.groupBox3.Controls.Add(this.rdbCertEmpty);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(120, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 30);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            // 
            // rdbCertNotEmpty
            // 
            this.rdbCertNotEmpty.AutoSize = true;
            this.rdbCertNotEmpty.Checked = true;
            this.rdbCertNotEmpty.Location = new System.Drawing.Point(6, 9);
            this.rdbCertNotEmpty.Name = "rdbCertNotEmpty";
            this.rdbCertNotEmpty.Size = new System.Drawing.Size(74, 17);
            this.rdbCertNotEmpty.TabIndex = 14;
            this.rdbCertNotEmpty.TabStop = true;
            this.rdbCertNotEmpty.Text = "Not Empty";
            this.rdbCertNotEmpty.UseVisualStyleBackColor = true;
            // 
            // rdbCertEmpty
            // 
            this.rdbCertEmpty.AutoSize = true;
            this.rdbCertEmpty.Location = new System.Drawing.Point(154, 8);
            this.rdbCertEmpty.Name = "rdbCertEmpty";
            this.rdbCertEmpty.Size = new System.Drawing.Size(54, 17);
            this.rdbCertEmpty.TabIndex = 15;
            this.rdbCertEmpty.Text = "Empty";
            this.rdbCertEmpty.UseVisualStyleBackColor = true;
            // 
            // dtpck_TimeSent
            // 
            this.dtpck_TimeSent.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpck_TimeSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpck_TimeSent.Location = new System.Drawing.Point(120, 256);
            this.dtpck_TimeSent.Name = "dtpck_TimeSent";
            this.dtpck_TimeSent.Size = new System.Drawing.Size(312, 20);
            this.dtpck_TimeSent.TabIndex = 24;
            // 
            // lblTimeSent
            // 
            this.lblTimeSent.Location = new System.Drawing.Point(16, 256);
            this.lblTimeSent.Name = "lblTimeSent";
            this.lblTimeSent.Size = new System.Drawing.Size(88, 16);
            this.lblTimeSent.TabIndex = 23;
            this.lblTimeSent.Text = "Time Sent";
            // 
            // txtMessageName
            // 
            this.txtMessageName.Location = new System.Drawing.Point(120, 218);
            this.txtMessageName.Name = "txtMessageName";
            this.txtMessageName.Size = new System.Drawing.Size(312, 20);
            this.txtMessageName.TabIndex = 22;
            this.txtMessageName.Text = "TIRPreDeclaration";
            // 
            // lblMessageName
            // 
            this.lblMessageName.Location = new System.Drawing.Point(16, 218);
            this.lblMessageName.Name = "lblMessageName";
            this.lblMessageName.Size = new System.Drawing.Size(88, 16);
            this.lblMessageName.TabIndex = 21;
            this.lblMessageName.Text = "Message Name";
            // 
            // txtExchangeVersion
            // 
            this.txtExchangeVersion.Location = new System.Drawing.Point(120, 181);
            this.txtExchangeVersion.Name = "txtExchangeVersion";
            this.txtExchangeVersion.Size = new System.Drawing.Size(312, 20);
            this.txtExchangeVersion.TabIndex = 20;
            this.txtExchangeVersion.Text = "1.0.0";
            // 
            // lblExchangeVer
            // 
            this.lblExchangeVer.Location = new System.Drawing.Point(16, 181);
            this.lblExchangeVer.Name = "lblExchangeVer";
            this.lblExchangeVer.Size = new System.Drawing.Size(88, 16);
            this.lblExchangeVer.TabIndex = 19;
            this.lblExchangeVer.Text = "Exchange ver.";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Esession Key";
            // 
            // lblCertID
            // 
            this.lblCertID.Location = new System.Drawing.Point(16, 113);
            this.lblCertID.Name = "lblCertID";
            this.lblCertID.Size = new System.Drawing.Size(88, 16);
            this.lblCertID.TabIndex = 13;
            this.lblCertID.Text = "Certificate ID";
            // 
            // txtSubscriberID
            // 
            this.txtSubscriberID.Location = new System.Drawing.Point(120, 75);
            this.txtSubscriberID.Name = "txtSubscriberID";
            this.txtSubscriberID.Size = new System.Drawing.Size(312, 20);
            this.txtSubscriberID.TabIndex = 12;
            this.txtSubscriberID.Text = "BLRCUS";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Subscriber ID";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(440, 16);
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
            this.label2.Text = "Message ID";
            // 
            // txtMessageID
            // 
            this.txtMessageID.Location = new System.Drawing.Point(120, 48);
            this.txtMessageID.MaxLength = 300;
            this.txtMessageID.Name = "txtMessageID";
            this.txtMessageID.Size = new System.Drawing.Size(312, 20);
            this.txtMessageID.TabIndex = 6;
            this.txtMessageID.Text = "TIREPDG2B1";
            // 
            // cmdCall
            // 
            this.cmdCall.Location = new System.Drawing.Point(496, 16);
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
            this.label1.Text = "IE15 File";
            // 
            // txtOriginalSafeTirFile
            // 
            this.txtOriginalSafeTirFile.Location = new System.Drawing.Point(120, 16);
            this.txtOriginalSafeTirFile.MaxLength = 256;
            this.txtOriginalSafeTirFile.Name = "txtOriginalSafeTirFile";
            this.txtOriginalSafeTirFile.Size = new System.Drawing.Size(312, 20);
            this.txtOriginalSafeTirFile.TabIndex = 3;
            this.txtOriginalSafeTirFile.Text = "o:\\bin\\UploadMessage.xml";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOutPut);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 317);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtOutPut
            // 
            this.txtOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutPut.Location = new System.Drawing.Point(3, 16);
            this.txtOutPut.Multiline = true;
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutPut.Size = new System.Drawing.Size(602, 298);
            this.txtOutPut.TabIndex = 0;
            this.txtOutPut.Text = "-";
            // 
            // TIREPDG2BTest
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 645);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TIREPDG2BTest";
            this.Text = "TIREPDG2B WS Tester";
            this.Load += new System.EventHandler(this.TCHQTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdCall_Click(object sender, System.EventArgs e)
		{
            TIREPDG2B.TIREPDG2BUploadParams su = new TIREPDG2B.TIREPDG2BUploadParams();
            byte [] Emptybyte = new byte[0];
			//read encsession key file
			try
			{

				string fileName =txtOriginalSafeTirFile.Text+".3DesEncKey.bin";
                if (rdbEsessionKeyNotEmpty.Checked == true)
                {
                    su.ESessionKey = GetFileContents(fileName);
                }
                else
                {
                    su.ESessionKey = Emptybyte;
                }
				su.SubscriberMessageID = txtMessageID.Text;

                if (rdbCertNotEmpty.Checked == true)
                {

                    su.CertificateID = GetFileString(txtOriginalSafeTirFile.Text + ".ThumbPrint.txt");
                }
                else
                {
                    su.CertificateID = "";
                }
				fileName = txtOriginalSafeTirFile.Text+".encrypted.bin";
				su.MessageContent = GetFileContents(fileName);
				//tq.SubscriberID = "FCS";
				su.SubscriberID = txtSubscriberID.Text.Trim();
                su.MessageName = txtMessageName.Text; //"TIRPreDeclaration";
                su.TimeSent = dtpck_TimeSent.Value;// .ToString("yyyy-MM-ddTHH:mm.ms");
                su.InformationExchangeVersion = txtExchangeVersion.Text; //"1.0.0";
				
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

			TIREPDG2B.TIREPDG2BService uploadClass= new TIREPDG2B.TIREPDG2BService();
			TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceInfo , uploadClass.Url  );

            TIREPDG2B.TIREPDG2BUploadAck tr;
			//WSST.SafeTIRUploadAck  tr ;
			try
			{
                uploadClass.Timeout = 10000000;
				tr= uploadClass.TIREPDG2B(su);
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
            if (tr.SubscriberMessageID != null)
            {
                txtOutPut.Text += "\r\n The Message ID is :" + tr.SubscriberMessageID.ToString();
            }
			
			if (tr.ReturnCode !=2)
			{
			
				txtOutPut.Text += "\r\n Completed with errors" ;
                txtOutPut.Text += "\r\n Return code Reason = "+tr.ReturnCodeReason.ToString();
				return;
			}
			


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
			txtOriginalSafeTirFile.Text= TestParent.m_MesseageFileName;
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
				txtOriginalSafeTirFile.Text = openFileDialog1.FileName;
			}

		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
		
		}
	}


}
