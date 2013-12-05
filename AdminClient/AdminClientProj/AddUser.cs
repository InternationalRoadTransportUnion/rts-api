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
	public class AddUser : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtVerifyPassword;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtSqlUserName;
		private System.Windows.Forms.TextBox txtSqlPassword;
		private System.Windows.Forms.TextBox txtVerifySqlPassword;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddUser()
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
			this.txtVerifySqlPassword = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSqlPassword = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtSqlUserName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtVerifyPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtVerifySqlPassword);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txtSqlPassword);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtSqlUserName);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtVerifyPassword);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUserID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 224);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// txtVerifySqlPassword
			// 
			this.txtVerifySqlPassword.Location = new System.Drawing.Point(160, 184);
			this.txtVerifySqlPassword.MaxLength = 50;
			this.txtVerifySqlPassword.Name = "txtVerifySqlPassword";
			this.txtVerifySqlPassword.PasswordChar = '*';
			this.txtVerifySqlPassword.Size = new System.Drawing.Size(192, 20);
			this.txtVerifySqlPassword.TabIndex = 5;
			this.txtVerifySqlPassword.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(18, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 18);
			this.label6.TabIndex = 7;
			this.label6.Text = "Verify Sql Password";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSqlPassword
			// 
			this.txtSqlPassword.Location = new System.Drawing.Point(160, 152);
			this.txtSqlPassword.MaxLength = 50;
			this.txtSqlPassword.Name = "txtSqlPassword";
			this.txtSqlPassword.PasswordChar = '*';
			this.txtSqlPassword.Size = new System.Drawing.Size(192, 20);
			this.txtSqlPassword.TabIndex = 4;
			this.txtSqlPassword.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 5;
			this.label5.Text = "Sql Password";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSqlUserName
			// 
			this.txtSqlUserName.Location = new System.Drawing.Point(160, 120);
			this.txtSqlUserName.MaxLength = 50;
			this.txtSqlUserName.Name = "txtSqlUserName";
			this.txtSqlUserName.Size = new System.Drawing.Size(192, 20);
			this.txtSqlUserName.TabIndex = 3;
			this.txtSqlUserName.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 18);
			this.label4.TabIndex = 3;
			this.label4.Text = "Sql User Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVerifyPassword
			// 
			this.txtVerifyPassword.Location = new System.Drawing.Point(160, 87);
			this.txtVerifyPassword.MaxLength = 50;
			this.txtVerifyPassword.Name = "txtVerifyPassword";
			this.txtVerifyPassword.PasswordChar = '*';
			this.txtVerifyPassword.Size = new System.Drawing.Size(416, 20);
			this.txtVerifyPassword.TabIndex = 2;
			this.txtVerifyPassword.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(160, 55);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(416, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Text = "";
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(160, 23);
			this.txtUserID.MaxLength = 50;
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(192, 20);
			this.txtUserID.TabIndex = 0;
			this.txtUserID.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Verify Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "User ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(480, 264);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(128, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Sa&ve";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// AddUser
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 293);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddUser";
			this.Text = "Add New User";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
		
			#region Validations
			if (txtUserID.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtUserID,"Mandatory Field");
				txtUserID.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtUserID,"");
			}


			if (txtPassword.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtPassword,"Mandatory Field");
				txtPassword.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtPassword,"");
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
			
			if (txtSqlUserName.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtSqlUserName,"Mandatory Field");
				txtSqlUserName.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtSqlUserName,"");
			}


			if (txtSqlPassword.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtSqlPassword,"Mandatory Field");
				txtSqlPassword.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtSqlPassword,"");
			}
			
			if (txtSqlPassword.Text.Trim() !=txtVerifySqlPassword.Text.Trim()) 
			{
				errorProvider1.SetError(txtVerifySqlPassword,"Passwords should match");
				txtVerifySqlPassword.SelectAll();
				txtVerifySqlPassword.Focus();
				return;
			}
			else
			{
				errorProvider1.SetError(txtVerifySqlPassword,"");
			}
			
			#endregion


			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sInsertSQL = "INSERT INTO dbo.RTS_USER "
				+ " (RTS_USER_ID, RTS_USER_PASSWORD, RTS_USER_SQL_LOGIN, RTS_USER_SQL_PASSWORD, LAST_UPDATE_USERID, LAST_UPDATE_TIME) " +
				" VALUES " +
				"(@RTS_USER_ID, @RTS_USER_PASSWORD, @RTS_USER_SQL_LOGIN, @RTS_USER_SQL_PASSWORD, @LAST_UPDATE_USERID, getdate())";


			SqlCommand sqlInsertCmd = new SqlCommand(sInsertSQL);


			#region set parameters
			// set parameter

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlInsertCmd.Parameters.Add(sUserParam);

			SqlParameter sUserPWDParam = new SqlParameter("@RTS_USER_PASSWORD",SqlDbType.NVarChar);
			sUserPWDParam.Value=CommonHelpers.CommonEncryptData(txtPassword.Text.Trim());
			sqlInsertCmd.Parameters.Add(sUserPWDParam);


			
			SqlParameter sSqlUserParam = new SqlParameter("@RTS_USER_SQL_LOGIN",SqlDbType.NVarChar);
			sSqlUserParam.Value=txtSqlUserName.Text.Trim();
			sqlInsertCmd.Parameters.Add(sSqlUserParam);
			
			
			SqlParameter sSqlUserPWDParam = new SqlParameter("@RTS_USER_SQL_PASSWORD",SqlDbType.NVarChar);
			sSqlUserPWDParam.Value=CommonHelpers.CommonEncryptData(txtSqlPassword.Text.Trim());
			sqlInsertCmd.Parameters.Add(sSqlUserPWDParam);

			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlInsertCmd.Parameters.Add(sLastUserParam);
			
			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.ExecuteNonQuery(sqlInsertCmd);
				
				MessageBox.Show("User added successfully", "Admin Client");

				//prepare for next user
				txtUserID.Text="";
				txtPassword.Text="";
				txtVerifyPassword.Text="";
				txtUserID.Focus();

			}
			catch(SqlException exx)
			{
			
				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("User already exists.", "Admin Client");

					//prepare for next user
					txtUserID.Text="";
					txtPassword.Text="";
					txtUserID.Focus();
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
