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
	public class ModifySubscriberServices : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ComboBox cmbServices;
		private System.Windows.Forms.Button btnNewService;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnChangeMethods;
		internal System.Windows.Forms.TextBox txtSubscriberID;
		private System.Windows.Forms.ComboBox cmbHashAlgo;
		private System.Windows.Forms.TextBox txtService;
		private System.Windows.Forms.GroupBox gpServiceDetails;
		private System.Windows.Forms.GroupBox gpService;
		private System.Windows.Forms.ComboBox cmbSessionEncAlgo;
		private System.Windows.Forms.ComboBox cmbCopyToID;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModifySubscriberServices()
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
			this.gpServiceDetails = new System.Windows.Forms.GroupBox();
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.cmbCopyToID = new System.Windows.Forms.ComboBox();
			this.cmbSessionEncAlgo = new System.Windows.Forms.ComboBox();
			this.cmbHashAlgo = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtService = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtSubscriberID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gpService = new System.Windows.Forms.GroupBox();
			this.btnNewService = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.cmbServices = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnChangeMethods = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.gpServiceDetails.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gpService.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpServiceDetails
			// 
			this.gpServiceDetails.Controls.Add(this.chkActive);
			this.gpServiceDetails.Controls.Add(this.cmbCopyToID);
			this.gpServiceDetails.Controls.Add(this.cmbSessionEncAlgo);
			this.gpServiceDetails.Controls.Add(this.cmbHashAlgo);
			this.gpServiceDetails.Controls.Add(this.label8);
			this.gpServiceDetails.Controls.Add(this.label9);
			this.gpServiceDetails.Controls.Add(this.txtService);
			this.gpServiceDetails.Controls.Add(this.label3);
			this.gpServiceDetails.Controls.Add(this.label2);
			this.gpServiceDetails.Controls.Add(this.label1);
			this.gpServiceDetails.Location = new System.Drawing.Point(8, 104);
			this.gpServiceDetails.Name = "gpServiceDetails";
			this.gpServiceDetails.Size = new System.Drawing.Size(600, 208);
			this.gpServiceDetails.TabIndex = 2;
			this.gpServiceDetails.TabStop = false;
			// 
			// chkActive
			// 
			this.chkActive.Location = new System.Drawing.Point(160, 176);
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size(24, 24);
			this.chkActive.TabIndex = 4;
			// 
			// cmbCopyToID
			// 
			this.cmbCopyToID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCopyToID.Location = new System.Drawing.Point(160, 136);
			this.cmbCopyToID.Name = "cmbCopyToID";
			this.cmbCopyToID.Size = new System.Drawing.Size(416, 21);
			this.cmbCopyToID.TabIndex = 3;
			// 
			// cmbSessionEncAlgo
			// 
			this.cmbSessionEncAlgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSessionEncAlgo.Items.AddRange(new object[] {
																   "RSA"});
			this.cmbSessionEncAlgo.Location = new System.Drawing.Point(160, 96);
			this.cmbSessionEncAlgo.Name = "cmbSessionEncAlgo";
			this.cmbSessionEncAlgo.Size = new System.Drawing.Size(144, 21);
			this.cmbSessionEncAlgo.TabIndex = 2;
			// 
			// cmbHashAlgo
			// 
			this.cmbHashAlgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHashAlgo.Items.AddRange(new object[] {
															 "SHA1"});
			this.cmbHashAlgo.Location = new System.Drawing.Point(160, 56);
			this.cmbHashAlgo.Name = "cmbHashAlgo";
			this.cmbHashAlgo.Size = new System.Drawing.Size(144, 21);
			this.cmbHashAlgo.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(24, 169);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 31);
			this.label8.TabIndex = 9;
			this.label8.Text = "Active";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(24, 129);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 31);
			this.label9.TabIndex = 8;
			this.label9.Text = "Send Copy To ID";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtService
			// 
			this.txtService.Location = new System.Drawing.Point(160, 16);
			this.txtService.MaxLength = 50;
			this.txtService.Name = "txtService";
			this.txtService.Size = new System.Drawing.Size(144, 20);
			this.txtService.TabIndex = 0;
			this.txtService.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 31);
			this.label3.TabIndex = 4;
			this.label3.Text = "Session Key Encryption Algo";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 31);
			this.label2.TabIndex = 2;
			this.label2.Text = "Hash Algo";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Service ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(480, 352);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(128, 23);
			this.btnUpdate.TabIndex = 6;
			this.btnUpdate.Text = "&Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 352);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtSubscriberID);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(600, 40);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// txtSubscriberID
			// 
			this.txtSubscriberID.Location = new System.Drawing.Point(160, 12);
			this.txtSubscriberID.Name = "txtSubscriberID";
			this.txtSubscriberID.ReadOnly = true;
			this.txtSubscriberID.Size = new System.Drawing.Size(416, 20);
			this.txtSubscriberID.TabIndex = 1;
			this.txtSubscriberID.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Subscriber ID";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpService
			// 
			this.gpService.Controls.Add(this.btnNewService);
			this.gpService.Controls.Add(this.btnSelect);
			this.gpService.Controls.Add(this.cmbServices);
			this.gpService.Controls.Add(this.label6);
			this.gpService.Location = new System.Drawing.Point(8, 56);
			this.gpService.Name = "gpService";
			this.gpService.Size = new System.Drawing.Size(600, 40);
			this.gpService.TabIndex = 1;
			this.gpService.TabStop = false;
			// 
			// btnNewService
			// 
			this.btnNewService.Location = new System.Drawing.Point(448, 11);
			this.btnNewService.Name = "btnNewService";
			this.btnNewService.Size = new System.Drawing.Size(128, 20);
			this.btnNewService.TabIndex = 3;
			this.btnNewService.Text = "New Service";
			this.btnNewService.Click += new System.EventHandler(this.btnNewService_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(312, 12);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(128, 20);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// cmbServices
			// 
			this.cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbServices.Location = new System.Drawing.Point(160, 11);
			this.cmbServices.Name = "cmbServices";
			this.cmbServices.Size = new System.Drawing.Size(144, 21);
			this.cmbServices.TabIndex = 1;
			this.cmbServices.SelectedIndexChanged += new System.EventHandler(this.cmbServices_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Select Service ID";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(208, 352);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 23);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(344, 352);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(128, 23);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnChangeMethods
			// 
			this.btnChangeMethods.Location = new System.Drawing.Point(8, 320);
			this.btnChangeMethods.Name = "btnChangeMethods";
			this.btnChangeMethods.Size = new System.Drawing.Size(128, 23);
			this.btnChangeMethods.TabIndex = 3;
			this.btnChangeMethods.Text = "Change Methods";
			this.btnChangeMethods.Click += new System.EventHandler(this.btnChangeMethods_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ModifySubscriberServices
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 381);
			this.Controls.Add(this.btnChangeMethods);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.gpService);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gpServiceDetails);
			this.Controls.Add(this.btnUpdate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ModifySubscriberServices";
			this.Text = "Modify / Delete Subscriber Services";
			this.Load += new System.EventHandler(this.ModifySubscriberServices_Load);
			this.gpServiceDetails.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.gpService.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnChangeMethods_Click(object sender, System.EventArgs e)
		{
			ModifyServiceMethod fModMethods = new ModifyServiceMethod();
			fModMethods.txtServiceID.Text=txtService.Text;
			fModMethods.txtSubscriberID.Text=txtSubscriberID.Text;
			fModMethods.ShowDialog(this);
		}

		private void ModifySubscriberServices_Load(object sender, System.EventArgs e)
		{
	

			#region Get All service 

		
			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);

/*
SELECT [SUBSCRIBER_ID]
      ,[SERVICE_ID]
      ,[ACTIVE]
      ,[SESSIONKEY_ENC_ALGO]
      ,[HASH_ALGO]
      ,[COPY_TO_ID]
      ,[LAST_UPDATE_USERID]
      ,[LAST_UPDATE_TIME]
  FROM [WS_SUBSCRIBER_DB].[dbo].[WS_SUBSCRIBER_SERVICES]";

 */
            string sServicesSQL = "SELECT [SERVICE_ID] FROM dbo.[WS_SUBSCRIBER_SERVICES] WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID";
			
			SqlCommand sqlServicesCmd = new SqlCommand(sServicesSQL );

			// set parameter

			SqlParameter sSubsParam = new SqlParameter();
			sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text.Trim();
			sqlServicesCmd.Parameters.Add(sSubsParam);


			IDataReader sdr;
			sdr = null;
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlServicesCmd,CommandBehavior.SingleResult);
				cmbServices.Items.Clear();
				
				while (sdr.Read())
				{
					string ServiceID;
					ServiceID= sdr.GetString(0);
					cmbServices.Items.Add(ServiceID);
				}
				if (cmbServices.Items.Count==0)
				{
					btnSelect.Enabled=false;
				}
				else
				{
					btnSelect.Enabled=true;
					cmbServices.SelectedIndex=0;
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

			#region Set controls
			gpService.Enabled=true;
			gpServiceDetails.Enabled=false;
			txtService.Text="";
			cmbCopyToID.Text="";
			cmbHashAlgo.Text="";
			cmbSessionEncAlgo.Text="";
			chkActive.Checked=false;
			btnDelete.Enabled=false;
			btnAdd.Enabled=false;
			btnChangeMethods.Enabled=false;
			btnUpdate.Enabled=false;
			#endregion
			
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			#region Set controls
			txtService.Enabled=false;
			
			gpServiceDetails.Enabled=true;
			btnDelete.Enabled=true;
			btnAdd.Enabled=false;
			btnChangeMethods.Enabled=true;
			btnUpdate.Enabled=true;
			#endregion


			#region get all copy to recipients

			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);

            string sCopySQL = "SELECT [COPY_TO_ID] FROM dbo.[COPY_TO_URLS] ORDER BY COPY_TO_ID";
			
			SqlCommand sqlCopyToCmd = new SqlCommand(sCopySQL);

			
			IDataReader sdr;
			sdr = null;
			string sDBMasterCopyToID=null ;
			
			cmbCopyToID.Items.Clear();
			//add a blank option
			cmbCopyToID.Items.Add("");
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlCopyToCmd,CommandBehavior.SingleResult);
								
				while (sdr.Read())
				{
					sDBMasterCopyToID=sdr.GetString(0);
					cmbCopyToID.Items.Add(sDBMasterCopyToID);

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

			#region Get service Data

		
			
			/*
			SELECT [SUBSCRIBER_ID]
				  ,[SERVICE_ID]
				  ,[ACTIVE]
				  ,[SESSIONKEY_ENC_ALGO]
				  ,[HASH_ALGO]
				  ,[COPY_TO_ID]
				  ,[LAST_UPDATE_USERID]
				  ,[LAST_UPDATE_TIME]
			  FROM [WS_SUBSCRIBER_DB].[dbo].[WS_SUBSCRIBER_SERVICES]";

			 */
            string sServicesSQL = "SELECT [HASH_ALGO], [SESSIONKEY_ENC_ALGO] 			  ,[COPY_TO_ID], [ACTIVE]  FROM dbo.[WS_SUBSCRIBER_SERVICES] WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID";
			
			SqlCommand sqlServicesCmd = new SqlCommand(sServicesSQL );

			// set parameter

			SqlParameter sSubsParam = new SqlParameter();
			sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text.Trim();
			sqlServicesCmd.Parameters.Add(sSubsParam);

			SqlParameter sSvcParam = new SqlParameter();
			sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sqlServicesCmd.Parameters.Add(sSvcParam);

			
			sdr = null;
			string sDBHashAlgo=null, sDBEncAlgo=null, sDBCopytoID=null;
			
			bool bDBActive=false;

			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlServicesCmd,CommandBehavior.SingleResult);
								
				while (sdr.Read())
				{
			
					sDBHashAlgo= sdr.GetString(0);
					sDBEncAlgo= sdr.GetString(1);
					sDBCopytoID=sdr.IsDBNull(2)?"":sdr.GetString(2);
					bDBActive=sdr.GetBoolean(3);
					
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
			txtService.Text=cmbServices.Text;
			cmbHashAlgo.Text=sDBHashAlgo;
			cmbSessionEncAlgo.Text=sDBEncAlgo;
			chkActive.Checked=bDBActive;
	
			cmbCopyToID.Text=sDBCopytoID;
			#endregion


		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{

			
			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sUpdateSQL = "UPDATE dbo.WS_SUBSCRIBER_SERVICES "
				+ " SET SESSIONKEY_ENC_ALGO = @SESSIONKEY_ENC_ALGO, HASH_ALGO=@HASH_ALGO, COPY_TO_ID=@COPY_TO_ID, ACTIVE=@ACTIVE, LAST_UPDATE_USERID=@LAST_UPDATE_USERID, LAST_UPDATE_TIME=getdate()" +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID AND SERVICE_ID= @SERVICE_ID";

			SqlCommand sqlUpdateCmd = new SqlCommand(sUpdateSQL);


			#region set parameters
			// set parameter
			SqlParameter sSessKeyEncParam = new SqlParameter("@SESSIONKEY_ENC_ALGO",SqlDbType.NVarChar);
			sSessKeyEncParam.Value=cmbSessionEncAlgo.Text;
			sqlUpdateCmd.Parameters.Add(sSessKeyEncParam);


			SqlParameter sHashAlgoParam = new SqlParameter("@HASH_ALGO",SqlDbType.NVarChar);
			sHashAlgoParam.Value=cmbHashAlgo.Text;
			sqlUpdateCmd.Parameters.Add(sHashAlgoParam);

			SqlParameter sCopytoIDParam = new SqlParameter("@COPY_TO_ID",SqlDbType.NVarChar);
			if (cmbCopyToID.Text=="")
			{
				sCopytoIDParam.Value=DBNull.Value;
			}
			else
			{
				sCopytoIDParam.Value=cmbCopyToID.Text;
			}
			
			sqlUpdateCmd.Parameters.Add(sCopytoIDParam);


			SqlParameter sActiveParam = new SqlParameter("@ACTIVE",SqlDbType.Bit);
			sActiveParam.Value= chkActive.Checked;
			sqlUpdateCmd.Parameters.Add(sActiveParam);



			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text.Trim();
			sqlUpdateCmd.Parameters.Add(sSubsParam);

			
			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sqlUpdateCmd.Parameters.Add(sSvcParam);

			
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
					MessageBox.Show("Subscriber service  updated successfully", "Admin Client");
				else
					MessageBox.Show("Subscriber service not updated successfully. Pls. retry", "Admin Client", MessageBoxButtons.OK,MessageBoxIcon.Warning);
				
				
				btnSelect_Click(null,null); //force a refresh
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

		private void btnNewService_Click(object sender, System.EventArgs e)
		{
			
			#region Set controls
			txtService.Enabled=true;
			cmbHashAlgo.Text="";
			cmbSessionEncAlgo.Text="";

			gpServiceDetails.Enabled=true;
			btnDelete.Enabled=false;
			btnAdd.Enabled=true;
			btnChangeMethods.Enabled=false;
			btnUpdate.Enabled=false;
			#endregion


			#region get all copy to recipients

			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);

            string sCopySQL = "SELECT [COPY_TO_ID] FROM dbo.[COPY_TO_URLS] ORDER BY COPY_TO_ID";
			
			SqlCommand sqlCopyToCmd = new SqlCommand(sCopySQL);

			
			IDataReader sdr;
			sdr = null;
			string sDBMasterCopyToID=null ;
			
			cmbCopyToID.Items.Clear();
			//add a blank option
			cmbCopyToID.Items.Add("");
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlCopyToCmd,CommandBehavior.SingleResult);
								
				while (sdr.Read())
				{
					sDBMasterCopyToID=sdr.GetString(0);
					cmbCopyToID.Items.Add(sDBMasterCopyToID);

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

			#region Set controls

			txtService.Text="";
			cmbHashAlgo.Text="";
			cmbSessionEncAlgo.Text="";
			chkActive.Checked=false;
	
			cmbCopyToID.Text="";
			#endregion

		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			
			#region Validations
			if (txtService.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtService,"Mandatory Field");
				txtService.Focus();
				return;
			}
			errorProvider1.SetError(txtService,"");
			

			if (cmbHashAlgo.Text.Trim() =="") 
			{
				errorProvider1.SetError(cmbHashAlgo,"Mandatory Field");
				cmbHashAlgo.Focus();
				return;
			}
			errorProvider1.SetError(cmbHashAlgo,"");
			


			if (cmbSessionEncAlgo.Text.Trim()=="") 
			{
			errorProvider1.SetError(cmbSessionEncAlgo,"Mandatory Field");
			cmbSessionEncAlgo.Focus();
			return;
			}
			
			errorProvider1.SetError(cmbSessionEncAlgo,"");
			
			
			
			
			#endregion

			
			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sInsSvcSQL = "INSERT dbo.WS_SUBSCRIBER_SERVICES "
				+ " ( SUBSCRIBER_ID, SERVICE_ID, SESSIONKEY_ENC_ALGO,HASH_ALGO, COPY_TO_ID,ACTIVE,LAST_UPDATE_USERID, LAST_UPDATE_TIME) VALUES( @SUBSCRIBER_ID,@SERVICE_ID,   @SESSIONKEY_ENC_ALGO,  @HASH_ALGO,  @COPY_TO_ID,  @ACTIVE,  @LAST_UPDATE_USERID,  getdate())";
	

			SqlCommand sqlInsertSvcCmd = new SqlCommand(sInsSvcSQL);


			#region set parameters
			// set parameter

			
			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text.Trim();
			sqlInsertSvcCmd.Parameters.Add(sSubsParam);

			
			txtService.Text = txtService.Text.Trim();

			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=txtService.Text;
			sqlInsertSvcCmd.Parameters.Add(sSvcParam);


			SqlParameter sSessKeyEncParam = new SqlParameter("@SESSIONKEY_ENC_ALGO",SqlDbType.NVarChar);
			sSessKeyEncParam.Value=cmbSessionEncAlgo.Text;
			sqlInsertSvcCmd.Parameters.Add(sSessKeyEncParam);


			SqlParameter sHashAlgoParam = new SqlParameter("@HASH_ALGO",SqlDbType.NVarChar);
			sHashAlgoParam.Value=cmbHashAlgo.Text;
			sqlInsertSvcCmd.Parameters.Add(sHashAlgoParam);

			SqlParameter sCopytoIDParam = new SqlParameter("@COPY_TO_ID",SqlDbType.NVarChar);
			if (cmbCopyToID.Text=="")
			{
				sCopytoIDParam.Value=DBNull.Value;
			}
			else
			{
				sCopytoIDParam.Value=cmbCopyToID.Text;
			}
			
			sqlInsertSvcCmd.Parameters.Add(sCopytoIDParam);


			SqlParameter sActiveParam = new SqlParameter("@ACTIVE",SqlDbType.Bit);
			sActiveParam.Value= chkActive.Checked;
			sqlInsertSvcCmd.Parameters.Add(sActiveParam);


			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlInsertSvcCmd.Parameters.Add(sLastUserParam);
			
			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.ExecuteNonQuery(sqlInsertSvcCmd);
				
				MessageBox.Show("Subscriber services added successfully", "Admin Client");

				ModifySubscriberServices_Load(null,null); //force refresh
				
			}
			catch(SqlException exx)
			{
			
				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("Service already configured.", "Admin Client");

					//prepare for next user
					txtService.Focus();
					txtService.SelectAll();
					
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			#region confirmation
			if (MessageBox.Show("Are you sure?","Admin Client",MessageBoxButtons.YesNo)==DialogResult.No)
				return;

			#endregion

			

			
			#region connect to DB and Delete rights and user

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			#region Del commands
            string sDelMethodsSQL = "DELETE dbo.WS_SUBSCRIBER_SERVICE_METHOD " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID";
			SqlCommand sqlDeleteMethodsCmd = new SqlCommand(sDelMethodsSQL);

            string sDelServicesSQL = "DELETE dbo.WS_SUBSCRIBER_SERVICES " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID";
			SqlCommand sqlDeleteServicesCmd = new SqlCommand(sDelServicesSQL);



			#endregion


			#region set parameters
			// set parameter

			//delete commands



			SqlParameter sSubsMethodParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsMethodParam.Value=txtSubscriberID.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sSubsMethodParam);

			SqlParameter sServicesMethodParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sServicesMethodParam.Value=cmbServices.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sServicesMethodParam);


			SqlParameter sSubsServicesParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsServicesParam.Value=txtSubscriberID.Text;
			sqlDeleteServicesCmd.Parameters.Add(sSubsServicesParam);

			SqlParameter sServicesParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sServicesParam.Value=cmbServices.Text;
			sqlDeleteServicesCmd.Parameters.Add(sServicesParam);

			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				
				
				dbSubs.BeginTransaction();

				dbSubs.ExecuteNonQuery(sqlDeleteMethodsCmd);
				dbSubs.ExecuteNonQuery(sqlDeleteServicesCmd);

				
				dbSubs.CommitTransaction();
				MessageBox.Show("Subscriber Service Deleted successfully", "Admin Client");

				//call search click to reload
				ModifySubscriberServices_Load(null,null);//refresh
					
				

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

		private void cmbServices_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			gpServiceDetails.Enabled=false;
			btnUpdate.Enabled=false;
			btnDelete.Enabled=false;
		}
	}
}
