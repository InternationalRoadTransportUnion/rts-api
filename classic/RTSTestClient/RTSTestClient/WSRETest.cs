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

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQTest.
	/// </summary>
	public class WSRETest : System.Windows.Forms.Form
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WSRETest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			txtOriginalSafeTirFile.Text= TestParent.m_MesseageFileName;
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
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(608, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtSubscriberID
            // 
            this.txtSubscriberID.Location = new System.Drawing.Point(120, 80);
            this.txtSubscriberID.Name = "txtSubscriberID";
            this.txtSubscriberID.Size = new System.Drawing.Size(312, 20);
            this.txtSubscriberID.TabIndex = 12;
            this.txtSubscriberID.Text = "FCS";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 83);
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
            this.txtMessageID.Text = "FCSQ1";
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
            this.label1.Text = "WSRE File";
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
            this.groupBox2.Location = new System.Drawing.Point(0, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 237);
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
            this.txtOutPut.Size = new System.Drawing.Size(602, 218);
            this.txtOutPut.TabIndex = 0;
            this.txtOutPut.Text = "-";
            // 
            // WSRETest
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 381);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WSRETest";
            this.Text = "WSRE WS Tester";
            this.Load += new System.EventHandler(this.WSRETest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdCall_Click(object sender, System.EventArgs e)
		{
             WSRE.SafeTIRReconParams su = new WSRE.SafeTIRReconParams();
             string fileName = String.Empty;
			//read encsession key file
			try
			{

				fileName =txtOriginalSafeTirFile.Text+".3DesEncKey.bin";
				su.ESessionKey = GetFileContents(fileName);
            }
            catch (IOException ioex)
			{
			//	MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				txtOutPut.Text+= "\r\n  IO Exception: Check log file for details: " + ioex.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
			//	return;
			}
            try
            {
                su.MessageTag=GetFileString( txtOriginalSafeTirFile.Text+".ThumbPrint.txt");
            }
            catch (IOException ioex)
			{
			//	MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				txtOutPut.Text+= "\r\n  IO Exception: Check log file for details: " + ioex.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
			//	return;
			}

            try
            {
				fileName = txtOriginalSafeTirFile.Text+".encrypted.bin";
				su.SafeTIRReconData = GetFileContents(fileName);
            }
            catch(IOException ioex)
            {
             
			//	MessageBox.Show("IO Exception: Check log file for details: " + ioex.Message);
				txtOutPut.Text+= "\r\n  IO Exception: Check log file for details: " + ioex.Message;
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, ioex.Message + "\r\n" +ioex.Source + "\r\n" + ioex.StackTrace				);
			//	return;
			}
				//tq.SubscriberID = "FCS";
                su.Sender_MessageID = txtMessageID.Text.Trim();
				su.SubscriberID = txtSubscriberID.Text.Trim();
                su.Information_Exchange_Version = "2.0.0";
				//su.CopyToID= txtCopyToID.Text;
				
			
						
			txtOutPut.Text+= "\r\n Files read successfully";
			txtOutPut.Text+= "\r\n Before calling Web Service";
			Application.DoEvents();

			//WSRE.WSREService  wsreservice = new WSRE.WSREService();
            WSRE.SafeTirUpload wsreservice = new WSRE.SafeTirUpload();
            TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceInfo, wsreservice.Url);

            WSRE.SafeTIRUploadAck rra;
			try
			{
                rra = wsreservice.WSRE(su);
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

			txtOutPut.Text += "\r\n The return code is :" + rra.ReturnCode.ToString();
			txtOutPut.Text += "\r\n The Message ID is :" + rra.Sender_MessageID.ToString();
			
			
			if (rra.ReturnCode !=2)
			{
			
				txtOutPut.Text += "\r\n Completed with errors";
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

		private void WSRETest_Load(object sender, System.EventArgs e)
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
