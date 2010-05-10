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
	public class ModifyUser : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.TextBox txtVerifySqlPassword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSqlPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSqlUserName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtVerifyPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtUserSearch;
		private System.Windows.Forms.ComboBox cmbUsers;
		private System.Windows.Forms.Button btnChangeUserRights;
		private System.Windows.Forms.GroupBox gpSelectUser;
		private System.Windows.Forms.GroupBox gpUserDetails;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.GroupBox gpSearch;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModifyUser()
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
			this.gpUserDetails = new System.Windows.Forms.GroupBox();
			this.txtVerifySqlPassword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSqlPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSqlUserName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtVerifyPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gpSearch = new System.Windows.Forms.GroupBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtUserSearch = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gpSelectUser = new System.Windows.Forms.GroupBox();
			this.btnSelect = new System.Windows.Forms.Button();
			this.cmbUsers = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnChangeUserRights = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.gpUserDetails.SuspendLayout();
			this.gpSearch.SuspendLayout();
			this.gpSelectUser.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpUserDetails
			// 
			this.gpUserDetails.Controls.Add(this.txtVerifySqlPassword);
			this.gpUserDetails.Controls.Add(this.label1);
			this.gpUserDetails.Controls.Add(this.txtSqlPassword);
			this.gpUserDetails.Controls.Add(this.label2);
			this.gpUserDetails.Controls.Add(this.txtSqlUserName);
			this.gpUserDetails.Controls.Add(this.label4);
			this.gpUserDetails.Controls.Add(this.txtVerifyPassword);
			this.gpUserDetails.Controls.Add(this.txtPassword);
			this.gpUserDetails.Controls.Add(this.txtUserID);
			this.gpUserDetails.Controls.Add(this.label3);
			this.gpUserDetails.Controls.Add(this.label7);
			this.gpUserDetails.Controls.Add(this.label8);
			this.gpUserDetails.Enabled = false;
			this.gpUserDetails.Location = new System.Drawing.Point(8, 104);
			this.gpUserDetails.Name = "gpUserDetails";
			this.gpUserDetails.Size = new System.Drawing.Size(600, 224);
			this.gpUserDetails.TabIndex = 2;
			this.gpUserDetails.TabStop = false;
			// 
			// txtVerifySqlPassword
			// 
			this.txtVerifySqlPassword.Location = new System.Drawing.Point(163, 184);
			this.txtVerifySqlPassword.MaxLength = 50;
			this.txtVerifySqlPassword.Name = "txtVerifySqlPassword";
			this.txtVerifySqlPassword.PasswordChar = '*';
			this.txtVerifySqlPassword.Size = new System.Drawing.Size(192, 20);
			this.txtVerifySqlPassword.TabIndex = 20;
			this.txtVerifySqlPassword.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(21, 184);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 19;
			this.label1.Text = "Verify Sql Password";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSqlPassword
			// 
			this.txtSqlPassword.Location = new System.Drawing.Point(163, 152);
			this.txtSqlPassword.MaxLength = 50;
			this.txtSqlPassword.Name = "txtSqlPassword";
			this.txtSqlPassword.PasswordChar = '*';
			this.txtSqlPassword.Size = new System.Drawing.Size(192, 20);
			this.txtSqlPassword.TabIndex = 18;
			this.txtSqlPassword.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(21, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 17;
			this.label2.Text = "Sql Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSqlUserName
			// 
			this.txtSqlUserName.Location = new System.Drawing.Point(163, 120);
			this.txtSqlUserName.MaxLength = 50;
			this.txtSqlUserName.Name = "txtSqlUserName";
			this.txtSqlUserName.Size = new System.Drawing.Size(192, 20);
			this.txtSqlUserName.TabIndex = 16;
			this.txtSqlUserName.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(21, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 18);
			this.label4.TabIndex = 15;
			this.label4.Text = "Sql User Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVerifyPassword
			// 
			this.txtVerifyPassword.Location = new System.Drawing.Point(163, 88);
			this.txtVerifyPassword.MaxLength = 50;
			this.txtVerifyPassword.Name = "txtVerifyPassword";
			this.txtVerifyPassword.PasswordChar = '*';
			this.txtVerifyPassword.Size = new System.Drawing.Size(416, 20);
			this.txtVerifyPassword.TabIndex = 13;
			this.txtVerifyPassword.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(163, 56);
			this.txtPassword.MaxLength = 50;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(416, 20);
			this.txtPassword.TabIndex = 11;
			this.txtPassword.Text = "";
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(163, 24);
			this.txtUserID.MaxLength = 50;
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(192, 20);
			this.txtUserID.TabIndex = 9;
			this.txtUserID.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(21, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Verify Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(21, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 18);
			this.label7.TabIndex = 12;
			this.label7.Text = "Password";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(21, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 18);
			this.label8.TabIndex = 10;
			this.label8.Text = "User ID";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSave
			// 
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(480, 376);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(128, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "&Update";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// gpSearch
			// 
			this.gpSearch.Controls.Add(this.btnSearch);
			this.gpSearch.Controls.Add(this.txtUserSearch);
			this.gpSearch.Controls.Add(this.label5);
			this.gpSearch.Location = new System.Drawing.Point(8, 8);
			this.gpSearch.Name = "gpSearch";
			this.gpSearch.Size = new System.Drawing.Size(600, 40);
			this.gpSearch.TabIndex = 0;
			this.gpSearch.TabStop = false;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(448, 12);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(128, 20);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "Go";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtUserSearch
			// 
			this.txtUserSearch.Location = new System.Drawing.Point(160, 13);
			this.txtUserSearch.Name = "txtUserSearch";
			this.txtUserSearch.Size = new System.Drawing.Size(264, 20);
			this.txtUserSearch.TabIndex = 1;
			this.txtUserSearch.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Search User";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpSelectUser
			// 
			this.gpSelectUser.Controls.Add(this.btnSelect);
			this.gpSelectUser.Controls.Add(this.cmbUsers);
			this.gpSelectUser.Controls.Add(this.label6);
			this.gpSelectUser.Enabled = false;
			this.gpSelectUser.Location = new System.Drawing.Point(8, 56);
			this.gpSelectUser.Name = "gpSelectUser";
			this.gpSelectUser.Size = new System.Drawing.Size(600, 40);
			this.gpSelectUser.TabIndex = 1;
			this.gpSelectUser.TabStop = false;
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(448, 12);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(128, 20);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// cmbUsers
			// 
			this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUsers.Location = new System.Drawing.Point(160, 11);
			this.cmbUsers.Name = "cmbUsers";
			this.cmbUsers.Size = new System.Drawing.Size(264, 21);
			this.cmbUsers.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Select User";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(344, 376);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 23);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnChangeUserRights
			// 
			this.btnChangeUserRights.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnChangeUserRights.Enabled = false;
			this.btnChangeUserRights.Location = new System.Drawing.Point(8, 344);
			this.btnChangeUserRights.Name = "btnChangeUserRights";
			this.btnChangeUserRights.Size = new System.Drawing.Size(128, 23);
			this.btnChangeUserRights.TabIndex = 3;
			this.btnChangeUserRights.Text = "Change User Rights";
			this.btnChangeUserRights.Click += new System.EventHandler(this.btnChangeUserRights_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ModifyUser
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 405);
			this.Controls.Add(this.btnChangeUserRights);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.gpSelectUser);
			this.Controls.Add(this.gpSearch);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gpUserDetails);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ModifyUser";
			this.Text = "Modify / Delete User";
			this.Load += new System.EventHandler(this.ModifyUser_Load);
			this.gpUserDetails.ResumeLayout(false);
			this.gpSearch.ResumeLayout(false);
			this.gpSelectUser.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnChangeUserRights_Click(object sender, System.EventArgs e)
		{
			
			AddModifyUserRights famodUserRights = new AddModifyUserRights();
			famodUserRights.txtUserID.Text=txtUserID.Text; 
			famodUserRights.ShowDialog(this);
			

		
		}

		private void ModifyUser_Load(object sender, System.EventArgs e)
		{
			
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			cmbUsers.Items.Clear();
			//disable detail groups and buttons

			
			gpSelectUser.Enabled=false;
			SetNoUserSelected();
			//

			txtUserSearch.Text=txtUserSearch.Text.Trim();
			string sUserSelectSQL;
			SqlCommand sSQLSearchCmd = new SqlCommand();
			sSQLSearchCmd.CommandType=CommandType.Text;

			if (txtUserSearch.Text=="") //all users
			{
				sUserSelectSQL = "SELECT RTS_USER_ID FROM RTS_USER";
				sSQLSearchCmd.CommandText=sUserSelectSQL;
				
			}
			else
			{
				sUserSelectSQL = "SELECT RTS_USER_ID FROM RTS_USER " + 
								 " WHERE RTS_USER_ID LIKE @RTS_USER_ID";
				sSQLSearchCmd.CommandText=sUserSelectSQL;
				SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
				sUserParam.Value=txtUserSearch.Text+"%" ;
				sSQLSearchCmd.Parameters.Add(sUserParam);
			}
			

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr=null;
			try
			{
			
				dbSubs.ConnectToDB();

				sdr = dbSubs.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				while(sdr.Read())
				{
					cmbUsers.Items.Add(sdr.GetString(0));
				}
				
				if (cmbUsers.Items.Count!=0) 
				{
					gpSelectUser.Enabled=true;
					cmbUsers.SelectedIndex=0;

				
				}
				else //no users matching criteria
				{
					gpSelectUser.Enabled=false;
					MessageBox.Show("No User matching criteria","Admin Client");
				}

			
			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				if (sdr!=null) sdr.Close();
				dbSubs.Close();
			}




		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			#region Connect to DB and getData

			SqlCommand sSQLSearchCmd = new SqlCommand();
			sSQLSearchCmd.CommandType=CommandType.Text;
			string sUserSelectSQL = "SELECT RTS_USER_ID,RTS_USER_PASSWORD,RTS_USER_SQL_LOGIN,RTS_USER_SQL_PASSWORD FROM RTS_USER " + 
				" WHERE RTS_USER_ID = @RTS_USER_ID";
			sSQLSearchCmd.CommandText=sUserSelectSQL;

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value= cmbUsers.Text;
			sSQLSearchCmd.Parameters.Add(sUserParam);
			

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr=null;
			string sDBUserID, sDBPassword,sDBSQLUserID, sDBSQLPassword;
			try
			{
			
				dbSubs.ConnectToDB();

				sdr = dbSubs.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleRow);
				if(sdr.Read()==true)
				{
					
					sDBUserID = sdr.GetString(0);
					sDBPassword = sdr.GetString(1); //encrypted
					sDBSQLUserID= sdr.GetString(2);
					sDBSQLPassword= sdr.GetString(3); //encrypted
				}
				else
				{
					gpSelectUser.Enabled=false;
					MessageBox.Show("No User matching criteria","Admin Client");
					return;
				}

			
			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				if (sdr!=null) sdr.Close();
				dbSubs.Close();
			}


			#endregion


			#region Clear and Populate controls
			//set control values

			
			txtUserID.Text=sDBUserID;
			txtUserID.ReadOnly=true;
			txtPassword.Text=CommonHelpers.CommonDecryptData(sDBPassword);
			txtVerifyPassword.Text=txtPassword.Text;
			txtSqlUserName.Text=sDBSQLUserID;
			txtSqlPassword.Text=CommonHelpers.CommonDecryptData(sDBSQLPassword);
			txtVerifySqlPassword.Text=txtSqlPassword.Text;


			gpUserDetails.Enabled=true;
			btnDelete.Enabled=true;
			btnSave.Enabled=true;
			btnChangeUserRights.Enabled=true;
			#endregion





		}

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
				
			string sUpdateSQL = "UPDATE RTS_USER "
				+ " SET RTS_USER_PASSWORD = @RTS_USER_PASSWORD, RTS_USER_SQL_LOGIN=@RTS_USER_SQL_LOGIN, RTS_USER_SQL_PASSWORD=@RTS_USER_SQL_PASSWORD, LAST_UPDATE_USERID=@LAST_UPDATE_USERID, LAST_UPDATE_TIME=getdate()" +
				" WHERE RTS_USER_ID= @RTS_USER_ID";

			SqlCommand sqlUpdateCmd = new SqlCommand(sUpdateSQL);


			#region set parameters
			// set parameter

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlUpdateCmd.Parameters.Add(sUserParam);

			SqlParameter sUserPWDParam = new SqlParameter("@RTS_USER_PASSWORD",SqlDbType.NVarChar);
			sUserPWDParam.Value=CommonHelpers.CommonEncryptData(txtPassword.Text.Trim());
			sqlUpdateCmd.Parameters.Add(sUserPWDParam);


			
			SqlParameter sSqlUserParam = new SqlParameter("@RTS_USER_SQL_LOGIN",SqlDbType.NVarChar);
			sSqlUserParam.Value=txtSqlUserName.Text.Trim();
			sqlUpdateCmd.Parameters.Add(sSqlUserParam);
			
			
			SqlParameter sSqlUserPWDParam = new SqlParameter("@RTS_USER_SQL_PASSWORD",SqlDbType.NVarChar);
			sSqlUserPWDParam.Value=CommonHelpers.CommonEncryptData(txtSqlPassword.Text.Trim());
			sqlUpdateCmd.Parameters.Add(sSqlUserPWDParam);

			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlUpdateCmd.Parameters.Add(sLastUserParam);
			
			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				int nRowsAffected;

				nRowsAffected= dbSubs.ExecuteNonQuery(sqlUpdateCmd);
			
				if (nRowsAffected!=0)
					MessageBox.Show("User updated successfully", "Admin Client");
				else
					MessageBox.Show("User not updated successfully. Pls. retry", "Admin Client", MessageBoxButtons.OK,MessageBoxIcon.Warning);

				
				

			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				dbSubs.Close();
			}



			#endregion

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			#region confirmation
			if (MessageBox.Show("Are you sure?","Admin Client",MessageBoxButtons.YesNo)==DialogResult.No)
				return;

			#endregion

			

			
			#region connect to DB and Delete rights and user

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
				
			string sDelRightsSQL = "DELETE RTS_USER_RIGHTS " +
							" WHERE RTS_USER_ID= @RTS_USER_ID";

			SqlCommand sqlDeleteRightsCmd = new SqlCommand(sDelRightsSQL);

			string sDelUserSQL = "DELETE RTS_USER " +
				" WHERE RTS_USER_ID= @RTS_USER_ID";

			SqlCommand sqlDeleteUserCmd = new SqlCommand(sDelUserSQL);

			#region set parameters
			// set parameter

			SqlParameter sUserRightParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserRightParam.Value=txtUserID.Text.Trim();
			sqlDeleteRightsCmd.Parameters.Add(sUserRightParam);

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlDeleteUserCmd.Parameters.Add(sUserParam);

			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.BeginTransaction();

				dbSubs.ExecuteNonQuery(sqlDeleteRightsCmd);
				dbSubs.ExecuteNonQuery(sqlDeleteUserCmd);
				
				dbSubs.CommitTransaction();
				MessageBox.Show("User Deleted successfully", "Admin Client");

				//call search click to reload
				btnSearch_Click(null,null);
					
				

			}
			catch (SqlException exSQL)
			{
				dbSubs.RollbackTransaction();
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{ //autorollback in case of error
				dbSubs.Close();
			}



			#endregion



			
		}

		private void SetNoUserSelected()
		{
			txtPassword.Text="";
			txtUserID.Text="";
			
			txtVerifyPassword.Text="";
			txtSqlUserName.Text="";
			txtSqlPassword.Text="";
			txtVerifySqlPassword.Text="";
			gpUserDetails.Enabled=false;
			btnChangeUserRights.Enabled=false;
			btnSave.Enabled=false;
			btnDelete.Enabled=false;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
