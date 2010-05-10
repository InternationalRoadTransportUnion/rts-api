using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		#region Members
		string sDBUserID, sDBPassword,sDBSQLUserID, sDBSQLPassword;
		#endregion


		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblVersion;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Login()
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
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(44, 104);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(120, 24);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "Login";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(220, 104);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(96, 16);
			this.txtUserID.MaxLength = 50;
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(224, 20);
			this.txtUserID.TabIndex = 0;
			this.txtUserID.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(96, 48);
			this.txtPassword.MaxLength = 50;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(224, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "User ID";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 24);
			this.label2.TabIndex = 5;
			this.label2.Text = "Password";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(8, 136);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(152, 24);
			this.lblVersion.TabIndex = 6;
			this.lblVersion.Text = "-";
			// 
			// Login
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(392, 158);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUserID);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLogin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Login";
			this.Text = "Login";
			this.Load += new System.EventHandler(this.Login_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Login_Load(object sender, System.EventArgs e)
		{
			lblVersion.Text= ConfigurationSettings.AppSettings["Location"].ToString() + " V:" +  Application.ProductVersion;
			

			
		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Login());
			
		}

		
		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			



			#region validations
			if (txtUserID.Text.Trim()=="")
			{
				MessageBox.Show("Provide valid UserID");
				txtUserID.Focus();
				return;
			}
			if (txtPassword.Text.Trim()=="")
			{
				MessageBox.Show("Provide valid Password. Empty Password not allowed");
				txtPassword.Focus();
				return;
			}
			#endregion


			#region Connect to DB and verify uid password
			CommonDBHelper dbInitialDB = new CommonDBHelper(CommonHelpers.CommonDecryptData(System.Configuration.ConfigurationSettings.AppSettings["InitialConnectString"].ToString()));


			string sValidateSQL = "SELECT " +
				"RTS_USER_ID, RTS_USER_PASSWORD, RTS_USER_SQL_LOGIN, RTS_USER_SQL_PASSWORD "+ 
				" FROM RTS_USER " +
				" WHERE RTS_USER_ID =@RTS_USER_ID";

 
			SqlCommand sqlSelect = new SqlCommand(sValidateSQL);

			// set parameter

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlSelect.Parameters.Add(sUserParam);


			IDataReader sdr = null;
			try
			{
				dbInitialDB.ConnectToDB();

				sdr = dbInitialDB.GetDataReader(sqlSelect,CommandBehavior.SingleRow);
				if (!sdr.Read())
				{
					MessageBox.Show( "Invalid UserID or Password","Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					txtUserID.Focus();
					txtUserID.SelectAll();
				
					txtPassword.Text="";

					return;
				}
				

				sDBUserID = sdr.GetString(0);
				sDBPassword = sdr.GetString(1);
				sDBSQLUserID= sdr.GetString(2);
				sDBSQLPassword= sdr.GetString(3); //encrypted

			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				if (sdr!=null) sdr.Close();
				dbInitialDB.Close();
			}
	

			if (txtPassword.Text!= CommonHelpers.CommonDecryptData(sDBPassword))
			{
				
				MessageBox.Show( "Invalid UserID or Password","Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				
				txtUserID.Focus();
				txtUserID.SelectAll();
				
				txtPassword.Text="";

				return;
			}

			#endregion

			#region Prepare Connection Strings
			Hashtable htConnectionStrings = PrepareConnectionStrings(sDBSQLUserID,CommonHelpers.CommonDecryptData(sDBSQLPassword));
			#endregion

			#region GetRights from subscriber db
			ArrayList alUserRights = null;


			CommonDBHelper dbSubsDB = new CommonDBHelper((string)htConnectionStrings["SubscriberDB"]);


			string sRightsSQL = "SELECT " +
				"RTS_RIGHT "+ 
				" FROM RTS_USER_RIGHTS " +
				" WHERE RTS_USER_ID =@RTS_USER_ID";

 
			SqlCommand sqlRights = new SqlCommand(sRightsSQL);

			// set parameter

			sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlRights.Parameters.Add(sUserParam);


			sdr = null;
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlRights,CommandBehavior.SingleResult);
				alUserRights = new ArrayList();
				while (sdr.Read())
				{
					alUserRights.Add(sdr.GetInt32(0));
				}

				if(alUserRights.Count==0)
				{
					MessageBox.Show( "User has no rights.","Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					txtUserID.Focus();
					txtUserID.SelectAll();
				
					txtPassword.Text="";

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
				dbSubsDB.Close();
			}
	
			#endregion


			#region Load main form and set rights

			
			frmMain.HTConnectionStrings = htConnectionStrings;

			frmMain.UserID=txtUserID.Text.Trim();

			frmMain.UserRights=(int[])alUserRights.ToArray(typeof(System.Int32));
			
			frmMain frmNewMain = new frmMain();

			this.Visible=false;
			frmNewMain.ShowDialog();
			this.Close();

			
			#endregion


		}
	

		private Hashtable  PrepareConnectionStrings(string SQLUser, string SQLPassword)
		{
			/*
			 * <add key=SubscriberDBName" value="WS_Subscriber_db"></add>
<add key="CopyToDBName" value="WSST_Copy_To_db"></add>
<add key="InternalDBName" value="WSST_Internal_db"></add>
<add key="ExternalDBName" value="WS_External_db"></add>
<add key="TCHQDBName" 
			 * 
			 */
			Hashtable htConnects = new Hashtable();
			
				htConnects.Add( "SubscriberDB",PrepareConnectString(ConfigurationSettings.AppSettings["SubscriberServerName"].ToString(),ConfigurationSettings.AppSettings["SubscriberDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));

//Load values based on location of install

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="Intranet")
			{

			


				htConnects.Add( "CopyToDB",PrepareConnectString(ConfigurationSettings.AppSettings["CopyToServerName"].ToString(),ConfigurationSettings.AppSettings["CopyToDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));

				htConnects.Add( "InternalDB",PrepareConnectString(ConfigurationSettings.AppSettings["InternalServerName"].ToString(),ConfigurationSettings.AppSettings["InternalDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));

			}
			else
			{
				htConnects.Add( "ExternalDB",PrepareConnectString(ConfigurationSettings.AppSettings["ExternalServerName"].ToString(),ConfigurationSettings.AppSettings["ExternalDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));

				htConnects.Add( "TCHQDB",PrepareConnectString(ConfigurationSettings.AppSettings["TCHQServerName"].ToString(),ConfigurationSettings.AppSettings["TCHQDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));
                htConnects.Add("WSRQDB", PrepareConnectString(ConfigurationSettings.AppSettings["WSRQServerName"].ToString(), ConfigurationSettings.AppSettings["WSRQDBName"].ToString(), sDBSQLUserID, sDBSQLPassword));

			}
			return htConnects;
		}

		private string PrepareConnectString(string ServerName, string DBName, string DBUser, string DBPassword)
		{

			//Application Name=AdminClient;Server=Doctor43\I0;Database=WS_Subscriber_db;uid=rtsuser;pwd=rtsuser;Connect Timeout=30"
			return "Application Name=AdminClient;Server="+ServerName+";Database=" + DBName+
				";uid="+DBUser+";pwd="+DBPassword+";Connect Timeout=30;";
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

	}
}
