using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using IRU.RTS.Crypto;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.IO;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for frmTempUploadSubsCert.
	/// </summary>
	public class frmTempUploadSubsCert : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtCerFilePath;
		private System.Windows.Forms.OpenFileDialog ofDCertFile;
		private System.Windows.Forms.ComboBox cboSubs;
		private System.Windows.Forms.ErrorProvider erprovider;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdUpload;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.TextBox txtOutPut;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dtpReceivedDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmTempUploadSubsCert()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			dtpReceivedDate.MaxDate=DateTime.Now;

			PopulateSubsCombo();

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
			this.txtOutPut = new System.Windows.Forms.TextBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdUpload = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.cboSubs = new System.Windows.Forms.ComboBox();
			this.txtCerFilePath = new System.Windows.Forms.TextBox();
			this.ofDCertFile = new System.Windows.Forms.OpenFileDialog();
			this.erprovider = new System.Windows.Forms.ErrorProvider();
			this.dtpReceivedDate = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.dtpReceivedDate);
			this.groupBox1.Controls.Add(this.txtOutPut);
			this.groupBox1.Controls.Add(this.cmdCancel);
			this.groupBox1.Controls.Add(this.cmdUpload);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmdBrowse);
			this.groupBox1.Controls.Add(this.cboSubs);
			this.groupBox1.Controls.Add(this.txtCerFilePath);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(568, 406);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			
			// 
			// txtOutPut
			// 
			this.txtOutPut.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtOutPut.Location = new System.Drawing.Point(3, 163);
			this.txtOutPut.Multiline = true;
			this.txtOutPut.Name = "txtOutPut";
			this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutPut.Size = new System.Drawing.Size(562, 240);
			this.txtOutPut.TabIndex = 7;
			this.txtOutPut.Text = "-";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(440, 120);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(72, 24);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdUpload
			// 
			this.cmdUpload.Location = new System.Drawing.Point(352, 120);
			this.cmdUpload.Name = "cmdUpload";
			this.cmdUpload.Size = new System.Drawing.Size(72, 24);
			this.cmdUpload.TabIndex = 5;
			this.cmdUpload.Text = "Upload";
			this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "Certificate Path";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 3;
			this.label1.Text = "Subscriber";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Location = new System.Drawing.Point(424, 64);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(64, 24);
			this.cmdBrowse.TabIndex = 2;
			this.cmdBrowse.Text = "Browse..";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cboSubs
			// 
			this.cboSubs.Items.AddRange(new object[] {
														 "FCS"});
			this.cboSubs.Location = new System.Drawing.Point(96, 24);
			this.cboSubs.Name = "cboSubs";
			this.cboSubs.Size = new System.Drawing.Size(120, 21);
			this.cboSubs.TabIndex = 1;
			this.cboSubs.Text = "FCS";
			// 
			// txtCerFilePath
			// 
			this.txtCerFilePath.Location = new System.Drawing.Point(96, 64);
			this.txtCerFilePath.Name = "txtCerFilePath";
			this.txtCerFilePath.Size = new System.Drawing.Size(320, 20);
			this.txtCerFilePath.TabIndex = 0;
			this.txtCerFilePath.Text = "";
			// 
			// erprovider
			// 
			this.erprovider.ContainerControl = this;
			// 
			// dtpReceivedDate
			// 
			this.dtpReceivedDate.Location = new System.Drawing.Point(96, 96);
			this.dtpReceivedDate.Name = "dtpReceivedDate";
			this.dtpReceivedDate.Size = new System.Drawing.Size(160, 20);
			this.dtpReceivedDate.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 32);
			this.label3.TabIndex = 9;
			this.label3.Text = "Certificate Received Date";
			// 
			// frmTempUploadSubsCert
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 406);
			this.Controls.Add(this.groupBox1);
			this.Name = "frmTempUploadSubsCert";
			this.Text = "Upload Subscriber Cert (Temp Form)";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			ofDCertFile.Filter = "Certificate File (*.cer)|*.cer";
			ofDCertFile.Title="Select Subscriber .cer Certificate File";
			ofDCertFile.ValidateNames=true;
			ofDCertFile.Multiselect=false;
			
			

			DialogResult dr = 	ofDCertFile.ShowDialog(this);

			if (dr != DialogResult.OK)
				return;

			string sPath = ofDCertFile.FileName;

			
			txtCerFilePath.Text=sPath;
		}

		private void cmdUpload_Click(object sender, System.EventArgs e)
		{
			#region validate params

			if (txtCerFilePath.Text.Trim()=="")
			{
				erprovider.SetIconAlignment(txtCerFilePath,ErrorIconAlignment.MiddleLeft);
				erprovider.SetError(txtCerFilePath,"Select File to Upload");
			
				return;
			}

			if (cboSubs.Text.Trim()=="")
			{
				erprovider.SetError(cboSubs,"Select Subscriber");
			
				return;
			}


			#endregion


			CertGenerator cg = new CertGenerator();
			RSAParameters rsaParams;
			string thumbPrint;
			string parseMessage = "-";
			DateTime expiryDate=DateTime.Now;
			if (cg.ExtractKeysFromSubscriberCertFile3(txtCerFilePath.Text,out rsaParams, out thumbPrint, out parseMessage, out expiryDate)==false)
			{
				MessageBox.Show("Extract Keys failed");
				return;
			}
			
			txtOutPut.Text=parseMessage;
			InsertNewSubscriberKey(cboSubs.Text,expiryDate, dtpReceivedDate.Value, txtCerFilePath.Text,rsaParams,thumbPrint,frmMain.UserID);
			txtOutPut.Text+= "\r\n Thumbprint is " + thumbPrint;
			txtOutPut.Text+= "\r\n Key Import Succeeded";
			MessageBox.Show("Key Import Succeeded");

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void InsertNewSubscriberKey(string SubscriberID, DateTime ExpiryDate, DateTime CertReceivedDate, string CertPath , RSAParameters rsaParams, string ThumbPrint, string LoggedInUser)
		{
			/*
			SqlConnection sqlConnect = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["SubsDBConnString"]);
			sqlConnect.Open();
			*/

			//mandar 8-12-2005
			CommonDBHelper cdhSubscriber = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
			cdhSubscriber.ConnectToDB();


			try
			{
				//SqlTransaction sqlTran = sqlConnect.BeginTransaction();
				cdhSubscriber.BeginTransaction();

				//block previous certificates
				//string updateSQL = "UPDATE [SUBSCRIBER_ENCRYPTION_KEYS] SET  LAST_UPDATE_TIME= GetDate(), LAST_UPDATE_USERID= @LAST_UPDATE_USERID WHERE SUBSCRIBER_ID = '" + SubscriberID + "'";


				string insertSQL = "INSERT INTO [SUBSCRIBER_ENCRYPTION_KEYS] " 
					+ "([SUBSCRIBER_ID],[ENCRYPTION_KEY_ID], [MODULUS], [EXPONENT], " + 
					"  [CERT_BLOB],[CERT_RECEIVED_USERID],[CERT_RECEIVED_DATE], "+
					"  [CERT_EXPIRY_DATE], " + 
					"  [KEY_ACTIVE], [KEY_ACTIVE_REASON], " + 
					 " [LAST_UPDATE_USERID], [LAST_UPDATE_TIME]) VALUES " + 
					"( @SUBSCRIBER_ID,@ENCRYPTION_KEY_ID, @MODULUS, @EXPONENT ," +
					" @CERT_BLOB, @CERT_RECEIVED_USERID, @CERT_RECEIVED_DATE  ," + 
					" @CERT_EXPIRY_DATE," + 
					" @KEY_ACTIVE, @KEY_ACTIVE_REASON," +
					" @LAST_UPDATE_USERID,Getdate())";


				SqlCommand dInsertCommand= new SqlCommand(insertSQL);//,sqlConnect, sqlTran);

				dInsertCommand.Parameters.Add("@SUBSCRIBER_ID",SqlDbType.NVarChar ,255);
				dInsertCommand.Parameters.Add("@ENCRYPTION_KEY_ID",SqlDbType.VarChar,255);
				dInsertCommand.Parameters.Add("@MODULUS",SqlDbType.VarBinary,128);
				dInsertCommand.Parameters.Add("@EXPONENT",SqlDbType.VarBinary,3);
				dInsertCommand.Parameters.Add("@CERT_BLOB",SqlDbType.Image);
				dInsertCommand.Parameters.Add("@CERT_RECEIVED_USERID",SqlDbType.NVarChar,50);
				dInsertCommand.Parameters.Add("@CERT_RECEIVED_DATE",SqlDbType.DateTime);
				dInsertCommand.Parameters.Add("@CERT_EXPIRY_DATE",SqlDbType.DateTime);
				dInsertCommand.Parameters.Add("@KEY_ACTIVE",SqlDbType.Bit,1);
				dInsertCommand.Parameters.Add("@KEY_ACTIVE_REASON",SqlDbType.NVarChar,500);
						
				dInsertCommand.Parameters.Add("@LAST_UPDATE_USERID",SqlDbType.NVarChar,50);

				//set values

				dInsertCommand.Parameters["@SUBSCRIBER_ID"].Value=SubscriberID;
				dInsertCommand.Parameters["@ENCRYPTION_KEY_ID"].Value= ThumbPrint;
				dInsertCommand.Parameters["@MODULUS"].Value=rsaParams.Modulus;
				dInsertCommand.Parameters["@EXPONENT"].Value=rsaParams.Exponent;

			
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

				dInsertCommand.Parameters["@CERT_RECEIVED_USERID"].Value= LoggedInUser;
				dInsertCommand.Parameters["@CERT_RECEIVED_DATE"].Value= CertReceivedDate;

				dInsertCommand.Parameters["@CERT_EXPIRY_DATE"].Value=ExpiryDate;
				
			
			
				dInsertCommand.Parameters["@KEY_ACTIVE"].Value=1;
				dInsertCommand.Parameters["@KEY_ACTIVE_REASON"].Value="New Key Generated";
	

				dInsertCommand.Parameters["@LAST_UPDATE_USERID"].Value=LoggedInUser;


				//prepare update
/*
				SqlCommand dUpdateKeyStatCommand = new SqlCommand(updateSQL,sqlConnect,sqlTran);

				dUpdateKeyStatCommand.Parameters.Add("@LAST_UPDATE_USERID",SqlDbType.NVarChar,50);
				dUpdateKeyStatCommand.Parameters["@LAST_UPDATE_USERID"].Value=LoggedInUser;


				int rowsAffected ;
				rowsAffected = dUpdateKeyStatCommand.ExecuteNonQuery();
*/
				int rowsAffected ;
				//rowsAffected = dInsertCommand.ExecuteNonQuery();
				rowsAffected = cdhSubscriber.ExecuteNonQuery(dInsertCommand);
				cdhSubscriber.CommitTransaction();

			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				//sqlConnect.Close();
				cdhSubscriber.Close();
			}
		
		}

		private void PopulateSubsCombo()
		{
			CommonHelpers.PopulateSubsCombo(cboSubs);
			if(cboSubs.Items.Count >0)
			{
				cboSubs.SelectedIndex=0;
			}
		
		}
		
	}
}
