using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for AddSubscriber.
	/// </summary>
	public class AddSubscriber : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtSubscriber;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtVerifyPassword;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCopyToAddress;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtDescription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddSubscriber()
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
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtVerifyPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtSubscriber = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtCopyToAddress = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtDescription);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtVerifyPassword);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtSubscriber);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 208);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// txtDescription
			// 
			this.txtDescription.AcceptsReturn = true;
			this.txtDescription.Location = new System.Drawing.Point(160, 128);
			this.txtDescription.MaxLength = 255;
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(416, 56);
			this.txtDescription.TabIndex = 3;
			this.txtDescription.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 24);
			this.label5.TabIndex = 3;
			this.label5.Text = "Description";
			// 
			// txtVerifyPassword
			// 
			this.txtVerifyPassword.Location = new System.Drawing.Point(160, 96);
			this.txtVerifyPassword.MaxLength = 255;
			this.txtVerifyPassword.Name = "txtVerifyPassword";
			this.txtVerifyPassword.PasswordChar = '*';
			this.txtVerifyPassword.Size = new System.Drawing.Size(416, 20);
			this.txtVerifyPassword.TabIndex = 2;
			this.txtVerifyPassword.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(160, 64);
			this.txtPassword.MaxLength = 255;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(416, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Text = "";
			// 
			// txtSubscriber
			// 
			this.txtSubscriber.Location = new System.Drawing.Point(160, 24);
			this.txtSubscriber.MaxLength = 255;
			this.txtSubscriber.Name = "txtSubscriber";
			this.txtSubscriber.Size = new System.Drawing.Size(416, 20);
			this.txtSubscriber.TabIndex = 0;
			this.txtSubscriber.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Verify Password";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Subscriber ID";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtCopyToAddress);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(8, 224);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(600, 64);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			// 
			// txtCopyToAddress
			// 
			this.txtCopyToAddress.Location = new System.Drawing.Point(160, 26);
			this.txtCopyToAddress.MaxLength = 400;
			this.txtCopyToAddress.Name = "txtCopyToAddress";
			this.txtCopyToAddress.Size = new System.Drawing.Size(416, 20);
			this.txtCopyToAddress.TabIndex = 0;
			this.txtCopyToAddress.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 18);
			this.label4.TabIndex = 1;
			this.label4.Text = "Receive Copy To URL";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(480, 304);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(128, 23);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Sa&ve";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 304);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// AddSubscriber
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 336);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddSubscriber";
			this.Text = "Add New Subscriber";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
		
			#region Validations
			txtSubscriber.Text=			txtSubscriber.Text.Trim();
			txtPassword.Text=			txtPassword.Text.Trim();
			txtCopyToAddress.Text=txtCopyToAddress.Text.Trim();
			txtDescription.Text=txtDescription.Text.Trim();

			if (txtSubscriber.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtSubscriber,"Mandatory Field");
				txtSubscriber.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtSubscriber,"");
			}


		


			if (txtPassword.Text.Trim() !=txtVerifyPassword.Text.Trim()) 
			{
				errorProvider1.SetError(txtVerifyPassword,"Passwords should match");
				txtVerifyPassword.SelectAll();
				txtVerifyPassword.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtVerifyPassword,"");
			}
			
			

			if (txtCopyToAddress.Text !="") 
			{
				if (txtCopyToAddress.Text.IndexOf("http://")==-1)
				{
				
					if (txtCopyToAddress.Text.IndexOf("https://")==-1)
					{
						errorProvider1.SetError(txtCopyToAddress,"Address should start with http(s)://");
						txtCopyToAddress.Focus();
						return;	
					}
				
				}
				
			}
			else
			{
				errorProvider1.SetError(txtCopyToAddress,"");
			}


			
			
			#endregion


			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sInsertSQL = "INSERT INTO dbo.WS_SUBSCRIBER "
				+ " (SUBSCRIBER_ID, SUBSCRIBER_PASSWORD, SUBSCRIBER_DESCRIPTION,  LAST_UPDATE_USERID, LAST_UPDATE_TIME) " +
				" VALUES " +
				"(@SUBSCRIBER_ID, @SUBSCRIBER_PASSWORD, @SUBSCRIBER_DESCRIPTION, @LAST_UPDATE_USERID, getdate())";

            string sInsertCopyToSQL = "INSERT INTO dbo.COPY_TO_URLS "
				+ " (COPY_TO_ID, COPY_TO_ADDRESS,  LAST_UPDATE_USERID, LAST_UPDATE_TIME) " +
				" VALUES " +
				"(@COPY_TO_ID, @COPY_TO_ADDRESS, @LAST_UPDATE_USERID, getdate())";


			SqlCommand sqlInsertCmd = new SqlCommand(sInsertSQL);
			SqlCommand sqlInsertCopyToCmd = new SqlCommand(sInsertCopyToSQL);



			#region set parameters
			// set parameter

			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriber.Text.Trim();
			sqlInsertCmd.Parameters.Add(sSubsParam);

			SqlParameter sSubsPWDParam = new SqlParameter("@SUBSCRIBER_PASSWORD",SqlDbType.NVarChar);
			if (txtPassword.Text=="")
			{
				sSubsPWDParam.Value=DBNull.Value;
			}
			else
			{
				sSubsPWDParam.Value=CommonHelpers.CommonEncryptData(txtPassword.Text);
			}
			sqlInsertCmd.Parameters.Add(sSubsPWDParam);


			
			SqlParameter sSubsDescription = new SqlParameter("@SUBSCRIBER_DESCRIPTION",SqlDbType.NVarChar);
			sSubsDescription.Value=txtDescription.Text.Trim();
			sqlInsertCmd.Parameters.Add(sSubsDescription);
			
			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlInsertCmd.Parameters.Add(sLastUserParam);
			
			
			//inser copytourl only of specified
			if (txtCopyToAddress.Text!="")
			{
				SqlParameter sCopyToIDParam = new SqlParameter("@COPY_TO_ID",SqlDbType.NVarChar);
				sCopyToIDParam.Value=txtSubscriber.Text.Trim();
				sqlInsertCopyToCmd.Parameters.Add(sCopyToIDParam);

				SqlParameter sSubsCopyToAddress = new SqlParameter("@COPY_TO_ADDRESS",SqlDbType.NVarChar);
				
				sSubsCopyToAddress.Value=txtCopyToAddress.Text;
				
				sqlInsertCopyToCmd.Parameters.Add(sSubsCopyToAddress);

				SqlParameter sLastUserParam2 = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
				sLastUserParam2.Value=frmMain.UserID;
				sqlInsertCopyToCmd.Parameters.Add(sLastUserParam2);
			
			}
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.BeginTransaction();
				dbSubs.ExecuteNonQuery(sqlInsertCmd);

				if (txtCopyToAddress.Text!="")
				{
					dbSubs.ExecuteNonQuery(sqlInsertCopyToCmd);
				}
				dbSubs.CommitTransaction();

				MessageBox.Show("Subscriber added successfully", "Admin Client");

				//prepare for next subscriber
				txtSubscriber.Text="";
				txtPassword.Text="";
				txtVerifyPassword.Text="";
				txtDescription.Text="";
				txtCopyToAddress.Text="";
				txtSubscriber.Focus();

			}
			catch(SqlException exx)
			{
				dbSubs.RollbackTransaction();
			
				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("Subscriber already exists.", "Admin Client");

					//prepare for next user
					txtSubscriber.Text="";
					txtPassword.Text="";
					txtVerifyPassword.Text="";
					txtDescription.Text="";
					txtCopyToAddress.Text="";
					txtSubscriber.Focus();
				}
				else
			
				{
						MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message + "\r\n SQL Error No:" + exx.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
				}
			
			}

			finally
			{
				dbSubs.Close();
			}



			#endregion
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
