using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using IRU.RTS;
using System.Security.Cryptography;

using System.Configuration;
using IRU.RTS.Crypto;
using System.IO;
using System.Data.SqlClient;


namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for frmCertClient.
	/// </summary>
	public class frmCertClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpInput;
		private System.Windows.Forms.Button cmdGenerate;
		private System.Windows.Forms.GroupBox gpOutput;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboSubscriber;
		private System.Windows.Forms.TextBox txtIssuerEmail;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCertPath;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dtExpiryDate;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
        private DateTimePicker dtStartDate;
        private Label label5;
        private IContainer components;

		public frmCertClient()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            dtStartDate.MinDate = DateTime.Today;
            //dtStartDate.MaxDate = DateTime.Today.AddMonths(3);
            dtStartDate.Value = DateTime.Today;
            dtExpiryDate.MinDate = dtStartDate.Value;
			//dtExpiryDate.MinDate=DateTime.Today;
			//dtExpiryDate.MaxDate=DateTime.Now.AddMonths(12);
			dtExpiryDate.Value = DateTime.Now.AddMonths(6);
			PopulateSubsCombo();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.gpInput = new System.Windows.Forms.GroupBox();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.dtExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCertPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIssuerEmail = new System.Windows.Forms.TextBox();
            this.cboSubscriber = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.gpOutput = new System.Windows.Forms.GroupBox();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpInput.SuspendLayout();
            this.gpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpInput
            // 
            this.gpInput.Controls.Add(this.dtStartDate);
            this.gpInput.Controls.Add(this.label5);
            this.gpInput.Controls.Add(this.button1);
            this.gpInput.Controls.Add(this.cmdBrowse);
            this.gpInput.Controls.Add(this.dtExpiryDate);
            this.gpInput.Controls.Add(this.label4);
            this.gpInput.Controls.Add(this.txtCertPath);
            this.gpInput.Controls.Add(this.label3);
            this.gpInput.Controls.Add(this.label2);
            this.gpInput.Controls.Add(this.txtIssuerEmail);
            this.gpInput.Controls.Add(this.cboSubscriber);
            this.gpInput.Controls.Add(this.label1);
            this.gpInput.Controls.Add(this.cmdGenerate);
            this.gpInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpInput.Location = new System.Drawing.Point(0, 0);
            this.gpInput.Name = "gpInput";
            this.gpInput.Size = new System.Drawing.Size(720, 136);
            this.gpInput.TabIndex = 0;
            this.gpInput.TabStop = false;
            this.gpInput.Enter += new System.EventHandler(this.gpInput_Enter);
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(144, 59);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(184, 20);
            this.dtStartDate.TabIndex = 10;
            this.dtStartDate.ValueChanged += new System.EventHandler(this.dtStartDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "Start Date";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(528, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Cancel";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(456, 96);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(32, 23);
            this.cmdBrowse.TabIndex = 3;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // dtExpiryDate
            // 
            this.dtExpiryDate.Location = new System.Drawing.Point(488, 56);
            this.dtExpiryDate.Name = "dtExpiryDate";
            this.dtExpiryDate.Size = new System.Drawing.Size(184, 20);
            this.dtExpiryDate.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(384, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Expiry Date";
            // 
            // txtCertPath
            // 
            this.txtCertPath.Location = new System.Drawing.Point(144, 96);
            this.txtCertPath.MaxLength = 256;
            this.txtCertPath.Name = "txtCertPath";
            this.txtCertPath.ReadOnly = true;
            this.txtCertPath.Size = new System.Drawing.Size(304, 20);
            this.txtCertPath.TabIndex = 6;
            this.txtCertPath.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output Certificate Path";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(384, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Issuer Email";
            // 
            // txtIssuerEmail
            // 
            this.txtIssuerEmail.Location = new System.Drawing.Point(488, 24);
            this.txtIssuerEmail.MaxLength = 20;
            this.txtIssuerEmail.Name = "txtIssuerEmail";
            this.txtIssuerEmail.Size = new System.Drawing.Size(100, 20);
            this.txtIssuerEmail.TabIndex = 1;
            this.txtIssuerEmail.Text = "iru@iru.org";
            // 
            // cboSubscriber
            // 
            this.cboSubscriber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubscriber.Location = new System.Drawing.Point(144, 24);
            this.cboSubscriber.Name = "cboSubscriber";
            this.cboSubscriber.Size = new System.Drawing.Size(72, 21);
            this.cboSubscriber.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Distributed To  Subscriber";
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGenerate.Location = new System.Drawing.Point(632, 96);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerate.TabIndex = 4;
            this.cmdGenerate.Text = "&Generate";
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // gpOutput
            // 
            this.gpOutput.Controls.Add(this.txtOutPut);
            this.gpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpOutput.Location = new System.Drawing.Point(0, 136);
            this.gpOutput.Name = "gpOutput";
            this.gpOutput.Size = new System.Drawing.Size(720, 130);
            this.gpOutput.TabIndex = 1;
            this.gpOutput.TabStop = false;
            this.gpOutput.Text = "Command Output";
            // 
            // txtOutPut
            // 
            this.txtOutPut.AcceptsReturn = true;
            this.txtOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutPut.Location = new System.Drawing.Point(3, 16);
            this.txtOutPut.Multiline = true;
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.ReadOnly = true;
            this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutPut.Size = new System.Drawing.Size(714, 111);
            this.txtOutPut.TabIndex = 0;
            this.txtOutPut.TabStop = false;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select folder to save the Certificate file.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmCertClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(720, 266);
            this.Controls.Add(this.gpOutput);
            this.Controls.Add(this.gpInput);
            this.Name = "frmCertClient";
            this.Text = "Create New IRU Keys";
            this.Load += new System.EventHandler(this.frmCertClient_Load);
            this.gpInput.ResumeLayout(false);
            this.gpInput.PerformLayout();
            this.gpOutput.ResumeLayout(false);
            this.gpOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	

		private void cmdGenerate_Click(object sender, System.EventArgs e)
		{
		
			txtOutPut.Text="";

			string commandLine=null;

			string  outThumbPrint,standardOutPut=null;
			
			//Validate Parameters

			if (txtIssuerEmail.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtIssuerEmail,"Mandatory Field");
				return;
			}
			else
			{
				errorProvider1.SetError(txtIssuerEmail,"");
			}
			
		
			if (txtCertPath.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtCertPath,"Mandatory Field");
				return;
			}
			else
			{
				errorProvider1.SetError(txtCertPath,"");
			}
            //TimeSpan ts = (dtExpiryDate.Value.Date) - (dtStartDate.Value.Date);
            
            //if (ts.TotalDays > 365 )
            //{
            //    errorProvider1.SetError(dtExpiryDate, "Expiry Date cannot be more than 1 year from start date");
            //    return;
            //}
            //else
            //{
            //    errorProvider1.SetError(txtCertPath, "");
            //}
			
			
			CertGenerator cg = new CertGenerator();

			DateTime dtGenerated;
			string CertPath="";

			try
			{
				cg.GenerateCertificate(txtCertPath.Text,txtIssuerEmail.Text,cboSubscriber.Text, dtStartDate.Value, dtExpiryDate.Value, out standardOutPut, out commandLine, out dtGenerated, out CertPath);
			}
			catch (FileNotFoundException fex)
			{
				MessageBox.Show(this, fex.Message,"File not found",MessageBoxButtons.OK,MessageBoxIcon.Error);
				standardOutPut += "\r\n" + fex.Message;
				return;
			}
			catch (ApplicationException apex)
			{
				MessageBox.Show(this,apex.Message);
				standardOutPut += "\r\n" + apex.Message;
				//standardOutPut += "\r\n" + standardOutPut;

			
				return;
			}
			finally
			{
			
				txtOutPut.Text = "Executing Command Line : \r\n" + "makecert.exe " +commandLine;

				txtOutPut.Text += "\r\n" + standardOutPut;
				txtOutPut.SelectionStart = standardOutPut.Length-1;
				txtOutPut.SelectionLength = 1;

				txtOutPut.ScrollToCaret();
			}

			RSAParameters rsaParams;
			cg.ExtractKeys2(CertPath,out rsaParams , out outThumbPrint);


			InsertNewIRUKey(cboSubscriber.Text, dtExpiryDate.Value, dtGenerated, CertPath, rsaParams ,outThumbPrint, frmMain.UserID);


			MessageBox.Show(this,"Key Pair genererated");
			txtOutPut.Text += "\r\n" + "Certificate File:" + CertPath ;
			txtOutPut.Text += "\r\n" + "Keys inserted into database. Thumbprint:" + outThumbPrint ;
			txtOutPut.SelectionStart = standardOutPut.Length-1;
			txtOutPut.SelectionLength = 1;

			txtOutPut.ScrollToCaret();
			


		}

		

		private void InsertNewIRUKey(string SubscriberID, DateTime ExpiryDate, DateTime dtGenerated, string CertPath , RSAParameters rsaParams, string ThumbPrint, string LoggedInUser)
		{
			//SqlConnection sqlConnect = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["SubsDBConnString"]);

			//mandar 8-12-2005
			CommonDBHelper cdhSubscriber = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
			cdhSubscriber.ConnectToDB();


			try
			{
				//SqlTransaction sqlTran = sqlConnect.BeginTransaction();
				cdhSubscriber.BeginTransaction();

				string updateSQL = "UPDATE [IRU_ENCRYPTION_KEYS] SET CERT_IS_CURRENT = 0, LAST_UPDATE_TIME= GetDate(), LAST_UPDATE_USERID= @LAST_UPDATE_USERID WHERE DISTRIBUTED_TO = '" + SubscriberID + "' and CERT_IS_CURRENT = 1";


				string insertSQL = "INSERT INTO [IRU_ENCRYPTION_KEYS] " 
					+ "([ENCRYPTION_KEY_ID], [MODULUS], [EXPONENT], " + 
					"[D], [P] ,[Q] ,[DP],[DQ], [INVERSEQ]," +
					"[DISTRIBUTED_TO], [DISTRIBUTION_DATE], "+
					" [KEY_ACTIVE], [KEY_ACTIVE_REASON], [CERT_IS_CURRENT], " + 
					"[CERT_EXPIRY_DATE], [CERT_GENERATION_TIME], [CERT_BLOB], " 
					+ "[LAST_UPDATE_USERID], [LAST_UPDATE_TIME]) VALUES " + 
					"(@ENCRYPTION_KEY_ID, @MODULUS, @EXPONENT ," +
					" @D, @P ,@Q , @DP, @DQ, @INVERSEQ," +
					"@DISTRIBUTED_TO , NULL  ," + 
					" @KEY_ACTIVE, @KEY_ACTIVE_REASON,@CERT_IS_CURRENT," + 
					" @CERT_EXPIRY_DATE, @CERT_GENERATION_TIME, @CERT_BLOB," + 
					" @LAST_UPDATE_USERID,Getdate())";


				//SqlCommand dInsertCommand= new SqlCommand(insertSQL,sqlConnect, sqlTran);
				SqlCommand dInsertCommand= new SqlCommand(insertSQL);//,sqlConnect, sqlTran);

				dInsertCommand.Parameters.Add("@ENCRYPTION_KEY_ID",SqlDbType.VarChar,255);
				dInsertCommand.Parameters.Add("@MODULUS",SqlDbType.VarBinary,128);
				dInsertCommand.Parameters.Add("@EXPONENT",SqlDbType.VarBinary,3);
				dInsertCommand.Parameters.Add("@D",SqlDbType.VarBinary,128);
				dInsertCommand.Parameters.Add("@DP",SqlDbType.VarBinary,64);
				dInsertCommand.Parameters.Add("@DQ",SqlDbType.VarBinary,64);
				dInsertCommand.Parameters.Add("@INVERSEQ",SqlDbType.VarBinary,64);
				dInsertCommand.Parameters.Add("@P",SqlDbType.VarBinary,64);
				dInsertCommand.Parameters.Add("@Q",SqlDbType.VarBinary,64);
				dInsertCommand.Parameters.Add("@DISTRIBUTED_TO",SqlDbType.NVarChar ,255);
				dInsertCommand.Parameters.Add("@KEY_ACTIVE",SqlDbType.Bit,1);
				dInsertCommand.Parameters.Add("@KEY_ACTIVE_REASON",SqlDbType.NVarChar,500);
				dInsertCommand.Parameters.Add("@CERT_IS_CURRENT",SqlDbType.Bit,1);
				dInsertCommand.Parameters.Add("@CERT_EXPIRY_DATE",SqlDbType.DateTime);
				dInsertCommand.Parameters.Add("@CERT_GENERATION_TIME",SqlDbType.DateTime);
				dInsertCommand.Parameters.Add("@CERT_BLOB",SqlDbType.Image);
				dInsertCommand.Parameters.Add("@LAST_UPDATE_USERID",SqlDbType.NVarChar,50);

				dInsertCommand.Parameters["@ENCRYPTION_KEY_ID"].Value= ThumbPrint;

				dInsertCommand.Parameters["@MODULUS"].Value=rsaParams.Modulus;
				dInsertCommand.Parameters["@EXPONENT"].Value=rsaParams.Exponent;

				dInsertCommand.Parameters["@D"].Value=rsaParams.D;

				dInsertCommand.Parameters["@DP"].Value=rsaParams.DP;
				dInsertCommand.Parameters["@DQ"].Value=rsaParams.DQ;
				dInsertCommand.Parameters["@P"].Value=rsaParams.P;
				dInsertCommand.Parameters["@Q"].Value=rsaParams.Q;
				dInsertCommand.Parameters["@INVERSEQ"].Value=rsaParams.InverseQ;

				
				dInsertCommand.Parameters["@DISTRIBUTED_TO"].Value=SubscriberID;
				dInsertCommand.Parameters["@KEY_ACTIVE"].Value=1;
				dInsertCommand.Parameters["@KEY_ACTIVE_REASON"].Value="New Key Generated";

				
			
				dInsertCommand.Parameters["@CERT_IS_CURRENT"].Value=1;
				dInsertCommand.Parameters["@CERT_EXPIRY_DATE"].Value=ExpiryDate;
				dInsertCommand.Parameters["@CERT_GENERATION_TIME"].Value=dtGenerated;

				//read the certificate file contents
			
				FileStream fsCert = new FileStream(CertPath,FileMode.Open );
				byte[] certContents = new byte[fsCert.Length];

				try
				{
					fsCert.Read(certContents,0,certContents.Length);
				}
				finally
				{
					fsCert.Close();
				}
				dInsertCommand.Parameters["@CERT_BLOB"].Value=certContents;
				dInsertCommand.Parameters["@LAST_UPDATE_USERID"].Value=LoggedInUser;


				//prepare update

				//SqlCommand dUpdateKeyStatCommand = new SqlCommand(updateSQL,sqlConnect,sqlTran);

				SqlCommand dUpdateKeyStatCommand = new SqlCommand(updateSQL);

				dUpdateKeyStatCommand.Parameters.Add("@LAST_UPDATE_USERID",SqlDbType.NVarChar,50);
				dUpdateKeyStatCommand.Parameters["@LAST_UPDATE_USERID"].Value=LoggedInUser;


				int rowsAffected ;

				cdhSubscriber.ExecuteNonQuery(dUpdateKeyStatCommand);

				cdhSubscriber.ExecuteNonQuery(dInsertCommand);
				/*
				rowsAffected = dUpdateKeyStatCommand.ExecuteNonQuery();

				rowsAffected = dInsertCommand.ExecuteNonQuery();
				
				sqlTran.Commit();
				*/
				cdhSubscriber.CommitTransaction();

			}
			finally
			{
				cdhSubscriber.Close();
			}
		
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			folderBrowserDialog1.SelectedPath = Application.StartupPath;

			DialogResult dr = 	folderBrowserDialog1.ShowDialog(this);

			if (dr != DialogResult.OK)
				return;

			string sPath = folderBrowserDialog1.SelectedPath;

			//cant set the name now as exact time of generationis decided later when generate is clicked
			string certPath = sPath;// + "\\" + cboSubscriber.Text+DateTime.Now.ToString("yyyyMMddHHmmss")+".cer";
			txtCertPath.Text=certPath;
		}

		private void gpInput_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void frmCertClient_Load(object sender, System.EventArgs e)
		{
		/*
			SqlConnection sqlconn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["SubsDBConnString"]);
			int nRetryCount = 50, nMainCounter =100000;
			while (nMainCounter-->0)
			{

				while (nRetryCount-- > 0)
				{
					try
					{
						sqlconn.Open();
						break;
					}
					catch (Exception exx)

					{
						string s = exx.Message;
						System.Diagnostics.Debug.WriteLine(nRetryCount);
					}
					if (nRetryCount==0)
					{
						throw new ApplicationException("");
					}
					else
					{
						System.Threading.Thread.Sleep(1000);
					}
				}

				System.Threading.Thread.Sleep(10);
				sqlconn.Close();
				System.Diagnostics.Debug.WriteLine("Main Loop process" + nMainCounter.ToString());
				System.Threading.Thread.Sleep(50);

			}
			*/
		}

		private void PopulateSubsCombo()
		{
			CommonHelpers.PopulateSubsCombo(cboSubscriber);
			cboSubscriber.SelectedIndex=0;
		
		}

        private void dtStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtExpiryDate.MinDate = dtStartDate.Value;
        }
	}
}
