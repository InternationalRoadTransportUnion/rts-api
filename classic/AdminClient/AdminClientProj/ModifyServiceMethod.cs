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
	public class ModifyServiceMethod : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtMethodID;
		private System.Windows.Forms.TextBox txtMethodDesc;
		internal System.Windows.Forms.TextBox txtServiceID;
		internal System.Windows.Forms.TextBox txtSubscriberID;
		private System.Windows.Forms.Button btnNewMethod;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gpMethodDetails;
		private System.Windows.Forms.GroupBox gpSelectMethod;
		private System.Windows.Forms.ComboBox cmbMethods;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModifyServiceMethod()
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
			this.gpMethodDetails = new System.Windows.Forms.GroupBox();
			this.txtMethodDesc = new System.Windows.Forms.TextBox();
			this.txtMethodID = new System.Windows.Forms.TextBox();
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtSubscriberID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtServiceID = new System.Windows.Forms.TextBox();
			this.gpSelectMethod = new System.Windows.Forms.GroupBox();
			this.cmbMethods = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnNewMethod = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.gpMethodDetails.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gpSelectMethod.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpMethodDetails
			// 
			this.gpMethodDetails.Controls.Add(this.txtMethodDesc);
			this.gpMethodDetails.Controls.Add(this.txtMethodID);
			this.gpMethodDetails.Controls.Add(this.chkActive);
			this.gpMethodDetails.Controls.Add(this.label8);
			this.gpMethodDetails.Controls.Add(this.label3);
			this.gpMethodDetails.Controls.Add(this.label2);
			this.gpMethodDetails.Location = new System.Drawing.Point(8, 136);
			this.gpMethodDetails.Name = "gpMethodDetails";
			this.gpMethodDetails.Size = new System.Drawing.Size(600, 176);
			this.gpMethodDetails.TabIndex = 2;
			this.gpMethodDetails.TabStop = false;
			// 
			// txtMethodDesc
			// 
			this.txtMethodDesc.AcceptsReturn = true;
			this.txtMethodDesc.Location = new System.Drawing.Point(160, 56);
			this.txtMethodDesc.MaxLength = 255;
			this.txtMethodDesc.Multiline = true;
			this.txtMethodDesc.Name = "txtMethodDesc";
			this.txtMethodDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMethodDesc.Size = new System.Drawing.Size(416, 64);
			this.txtMethodDesc.TabIndex = 1;
			this.txtMethodDesc.Text = "";
			// 
			// txtMethodID
			// 
			this.txtMethodID.Location = new System.Drawing.Point(160, 24);
			this.txtMethodID.Name = "txtMethodID";
			this.txtMethodID.Size = new System.Drawing.Size(144, 20);
			this.txtMethodID.TabIndex = 0;
			this.txtMethodID.Text = "";
			// 
			// chkActive
			// 
			this.chkActive.Location = new System.Drawing.Point(160, 136);
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size(24, 24);
			this.chkActive.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(24, 136);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 18);
			this.label8.TabIndex = 9;
			this.label8.Text = "Active";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Method Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Method ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtSubscriberID);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.txtServiceID);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(600, 72);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// txtSubscriberID
			// 
			this.txtSubscriberID.Location = new System.Drawing.Point(160, 12);
			this.txtSubscriberID.Name = "txtSubscriberID";
			this.txtSubscriberID.ReadOnly = true;
			this.txtSubscriberID.Size = new System.Drawing.Size(416, 20);
			this.txtSubscriberID.TabIndex = 0;
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
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Service ID";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtServiceID
			// 
			this.txtServiceID.Location = new System.Drawing.Point(160, 40);
			this.txtServiceID.Name = "txtServiceID";
			this.txtServiceID.ReadOnly = true;
			this.txtServiceID.Size = new System.Drawing.Size(144, 20);
			this.txtServiceID.TabIndex = 1;
			this.txtServiceID.Text = "";
			// 
			// gpSelectMethod
			// 
			this.gpSelectMethod.Controls.Add(this.cmbMethods);
			this.gpSelectMethod.Controls.Add(this.label1);
			this.gpSelectMethod.Controls.Add(this.btnNewMethod);
			this.gpSelectMethod.Controls.Add(this.btnSelect);
			this.gpSelectMethod.Location = new System.Drawing.Point(8, 88);
			this.gpSelectMethod.Name = "gpSelectMethod";
			this.gpSelectMethod.Size = new System.Drawing.Size(600, 40);
			this.gpSelectMethod.TabIndex = 1;
			this.gpSelectMethod.TabStop = false;
			// 
			// cmbMethods
			// 
			this.cmbMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMethods.Location = new System.Drawing.Point(160, 11);
			this.cmbMethods.Name = "cmbMethods";
			this.cmbMethods.Size = new System.Drawing.Size(144, 21);
			this.cmbMethods.TabIndex = 0;
			this.cmbMethods.SelectedIndexChanged += new System.EventHandler(this.cmbMethods_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Select Method ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnNewMethod
			// 
			this.btnNewMethod.Location = new System.Drawing.Point(448, 12);
			this.btnNewMethod.Name = "btnNewMethod";
			this.btnNewMethod.Size = new System.Drawing.Size(128, 20);
			this.btnNewMethod.TabIndex = 2;
			this.btnNewMethod.Text = "New Method";
			this.btnNewMethod.Click += new System.EventHandler(this.btnNewMethod_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(312, 12);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(128, 20);
			this.btnSelect.TabIndex = 1;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(336, 328);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(128, 23);
			this.btnAdd.TabIndex = 4;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(200, 328);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(0, 328);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(472, 328);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(128, 23);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "&Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ModifyServiceMethod
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 368);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.gpSelectMethod);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.gpMethodDetails);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ModifyServiceMethod";
			this.Text = "Modify Service Method";
			this.Load += new System.EventHandler(this.ModifyServiceMethod_Load);
			this.gpMethodDetails.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.gpSelectMethod.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void ModifyServiceMethod_Load(object sender, System.EventArgs e)
		{

			#region setup controls
			txtMethodID.Text="";
			chkActive.Checked=false;
			txtMethodDesc.Text="";

			gpMethodDetails.Enabled=false;
			gpSelectMethod.Enabled=true;
			btnDelete.Enabled=false;
			btnAdd.Enabled=false;
			btnUpdate.Enabled=false;
			#endregion
			

			#region Get All service methods

		
			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);

			/*
			 
SELECT [SUBSCRIBER_ID]
      ,[SERVICE_ID]
      ,[METHOD_ID]
      ,[ACTIVE]
      ,[LAST_UPDATE_USER]
      ,[LAST_UPDATE_TIME]
      ,[METHOD_DESCRIPTION]
  FROM [WS_SUBSCRIBER_DB].[dbo].[WS_SUBSCRIBER_SERVICE_METHOD]

			 */
            string sMethodsSQL = "SELECT [METHOD_ID] FROM dbo.[WS_SUBSCRIBER_SERVICE_METHOD] WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID";
			
			SqlCommand sqlMethodsCmd = new SqlCommand(sMethodsSQL);

			// set parameter

			//TODO: Get rid of duplicate new SQLPArameter
			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text;
			sqlMethodsCmd.Parameters.Add(sSubsParam);


			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=txtServiceID.Text;
			sqlMethodsCmd.Parameters.Add(sSvcParam);

			IDataReader sdr;
			sdr = null;
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlMethodsCmd,CommandBehavior.SingleResult);
				cmbMethods.Items.Clear();
				
				while (sdr.Read())
				{
					int nDBMethodID;
					nDBMethodID= sdr.GetInt32(0);
					cmbMethods.Items.Add(nDBMethodID);
				}
				if (cmbMethods.Items.Count==0)
				{
					btnSelect.Enabled=false;
				}
				else
				{
					btnSelect.Enabled=true;
					cmbMethods.SelectedIndex=0;
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


		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			
			#region Set controls
			txtMethodID.Enabled=false;
			
			gpMethodDetails.Enabled=true;
			btnDelete.Enabled=true;
			btnAdd.Enabled=false;
			
			btnUpdate.Enabled=true;
			#endregion

			#region Get service Data

		
			
			/*
		
SELECT [SUBSCRIBER_ID]
      ,[SERVICE_ID]
      ,[METHOD_ID]
      ,[ACTIVE]
      ,[LAST_UPDATE_USER]
      ,[LAST_UPDATE_TIME]
      ,[METHOD_DESCRIPTION]
  FROM [WS_SUBSCRIBER_DB].[dbo].[WS_SUBSCRIBER_SERVICE_METHOD]
			 */
			string sServicesSQL = "SELECT [METHOD_DESCRIPTION], [ACTIVE]  " +
                 " FROM dbo.[WS_SUBSCRIBER_SERVICE_METHOD] WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID AND METHOD_ID = @METHOD_ID";
			
			SqlCommand sqlSvcMethodsCmd = new SqlCommand(sServicesSQL );

			// set parameter

			SqlParameter sSubsParam = new SqlParameter();
			sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text;
			sqlSvcMethodsCmd.Parameters.Add(sSubsParam);

			SqlParameter sSvcParam = new SqlParameter();
			sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=txtServiceID.Text;
			sqlSvcMethodsCmd.Parameters.Add(sSvcParam);

			SqlParameter sMthParam = new SqlParameter();
			sMthParam = new SqlParameter("@METHOD_ID",SqlDbType.Int);
			sMthParam.Value=int.Parse( cmbMethods.Text);
			sqlSvcMethodsCmd.Parameters.Add(sMthParam);
			
			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr = null;
			string sDBDEscription =null;
						
			bool bDBActive=false;

			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlSvcMethodsCmd,CommandBehavior.SingleResult);
								
				while (sdr.Read())
				{
			
					sDBDEscription =sdr.IsDBNull(0)?"":sdr.GetString(0);
					bDBActive=sdr.GetBoolean(1);
					
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
			 txtMethodID.Text=cmbMethods.Text;

			txtMethodDesc.Text=sDBDEscription;
			
			chkActive.Checked=bDBActive;
	
			
			#endregion

		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			
			txtMethodDesc.Text=txtMethodDesc.Text.Trim();
			
			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sUpdateSQL = "UPDATE dbo.WS_SUBSCRIBER_SERVICE_METHOD "
				+ " SET METHOD_DESCRIPTION = @METHOD_DESCRIPTION, ACTIVE=@ACTIVE, LAST_UPDATE_USERID=@LAST_UPDATE_USERID, LAST_UPDATE_TIME=getdate()" +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID AND SERVICE_ID= @SERVICE_ID AND METHOD_ID= @METHOD_ID";

			SqlCommand sqlUpdateCmd = new SqlCommand(sUpdateSQL);


			#region set parameters
			// set parameter
			

			SqlParameter sDescParam = new SqlParameter("@METHOD_DESCRIPTION",SqlDbType.NVarChar);
			if (txtMethodDesc.Text=="")
			{
				sDescParam.Value=DBNull.Value;
			}
			else
			{
				sDescParam.Value=txtMethodDesc.Text;
			}
			
			sqlUpdateCmd.Parameters.Add(sDescParam);


			SqlParameter sActiveParam = new SqlParameter("@ACTIVE",SqlDbType.Bit);
			sActiveParam.Value= chkActive.Checked;
			sqlUpdateCmd.Parameters.Add(sActiveParam);



			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text;
			sqlUpdateCmd.Parameters.Add(sSubsParam);

			
			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=txtServiceID.Text;
			sqlUpdateCmd.Parameters.Add(sSvcParam);

			SqlParameter sMthParam = new SqlParameter("@METHOD_ID",SqlDbType.Int);
			sMthParam.Value= int.Parse(cmbMethods.Text);
			sqlUpdateCmd.Parameters.Add(sMthParam);
			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlUpdateCmd.Parameters.Add(sLastUserParam);
			
			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				int nRowsAffected;
				nRowsAffected = dbSubs.ExecuteNonQuery(sqlUpdateCmd);
				
				if (nRowsAffected!=0)
				MessageBox.Show("Subscriber service method updated successfully", "Admin Client");
				else
				MessageBox.Show("Subscriber service method not updated successfully. Pls. retry", "Admin Client", MessageBoxButtons.OK,MessageBoxIcon.Warning);
				
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

		private void btnNewMethod_Click(object sender, System.EventArgs e)
		{
			
			#region Set controls
			txtMethodID.Enabled=true;
			txtMethodID.Text="";
			txtMethodDesc.Text="";
			gpMethodDetails.Enabled=true;

			btnUpdate.Enabled=false;
			btnAdd.Enabled=true;
			chkActive.Checked=false;
			btnDelete.Enabled=false;
			
			#endregion


			
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{

			txtMethodID.Text=txtMethodID.Text.Trim();
			txtMethodDesc.Text=txtMethodDesc.Text.Trim();
			#region Validations
			if (txtMethodID.Text =="") 
			{
				errorProvider1.SetError(txtMethodID,"Mandatory Field");
				txtMethodID.Focus();
				return;
			}
			errorProvider1.SetError(txtMethodID,"");

			try
			{
					int.Parse(txtMethodID.Text ) ;
			 }
		catch (Exception exx)
			{
				errorProvider1.SetError(txtMethodID,"Should be a integer");
				txtMethodID.Focus();
				return;

			}
			errorProvider1.SetError(txtMethodID,"");

			
			#endregion

			
			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sInsSvcMthSQL = "INSERT dbo.WS_SUBSCRIBER_SERVICE_METHOD "
				+ " ( SUBSCRIBER_ID, SERVICE_ID, METHOD_ID, METHOD_DESCRIPTION, ACTIVE,LAST_UPDATE_USERID, LAST_UPDATE_TIME) VALUES( @SUBSCRIBER_ID,@SERVICE_ID,@METHOD_ID,   @METHOD_DESCRIPTION, @ACTIVE,  @LAST_UPDATE_USERID,  getdate())";
	

			SqlCommand sqlInsSvcMethodsCmd = new SqlCommand(sInsSvcMthSQL);


			#region set parameters
			// set parameter

			
			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriberID.Text;
			sqlInsSvcMethodsCmd.Parameters.Add(sSubsParam);

			


			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=txtServiceID.Text;
			sqlInsSvcMethodsCmd.Parameters.Add(sSvcParam);


			SqlParameter sMthIDParam= new SqlParameter("@METHOD_ID",SqlDbType.Int);
			sMthIDParam.Value=int.Parse(txtMethodID.Text);
			sqlInsSvcMethodsCmd.Parameters.Add(sMthIDParam);


			SqlParameter sDescParam = new SqlParameter("@METHOD_DESCRIPTION",SqlDbType.NVarChar);
			if (txtMethodDesc.Text=="")
			{
				sDescParam.Value=DBNull.Value;
			}
			else
			{
				sDescParam.Value=txtMethodDesc.Text;
			}
			
			sqlInsSvcMethodsCmd.Parameters.Add(sDescParam);



			SqlParameter sActiveParam = new SqlParameter("@ACTIVE",SqlDbType.Bit);
			sActiveParam.Value= chkActive.Checked;
			sqlInsSvcMethodsCmd.Parameters.Add(sActiveParam);


			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlInsSvcMethodsCmd.Parameters.Add(sLastUserParam);
			
			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.ExecuteNonQuery(sqlInsSvcMethodsCmd);
				
				MessageBox.Show("Subscriber services Method added successfully", "Admin Client");

				ModifyServiceMethod_Load(null,null);// refresh
				
			}
			catch(SqlException exx)
			{
			
				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("Service Method already configured.", "Admin Client");

					//prepare for next user
					txtMethodID.Focus();
					txtMethodID.SelectAll();
					
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
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID AND SERVICE_ID=@SERVICE_ID AND METHOD_ID=@METHOD_ID";
			SqlCommand sqlDeleteMethodsCmd = new SqlCommand(sDelMethodsSQL);

			


			#endregion


			#region set parameters
			// set parameter

			//delete commands



			SqlParameter sSubsMethodParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsMethodParam.Value=txtSubscriberID.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sSubsMethodParam);

			SqlParameter sServicesMethodParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sServicesMethodParam.Value=txtServiceID.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sServicesMethodParam);

			SqlParameter sMthIDMethodParam = new SqlParameter("@METHOD_ID",SqlDbType.NVarChar);
			sMthIDMethodParam.Value=cmbMethods.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sMthIDMethodParam);


			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				
				dbSubs.ExecuteNonQuery(sqlDeleteMethodsCmd);
				
				MessageBox.Show("Subscriber Service Method Deleted successfully", "Admin Client");

				//call search click to reload
				ModifyServiceMethod_Load(null,null);//refresh
					
				

			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			finally
			{ //autorollback in case of error
				dbSubs.Close();
			}



			#endregion


		}

		private void cmbMethods_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			gpMethodDetails.Enabled=false;
			btnDelete.Enabled=false;
			btnUpdate.Enabled=false;

		}
	}
}
