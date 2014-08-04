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
	public class ModifySubscriber : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtSubscriber;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtVerifyPassword;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ComboBox cmbSubscribers;
		private System.Windows.Forms.TextBox txtSubscriberSearch;
		private System.Windows.Forms.Button btnChangeServices;
		private System.Windows.Forms.GroupBox gpSelectSubscriber;
		private System.Windows.Forms.GroupBox gpSubscriberDetails;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtCopyToAddress;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModifySubscriber()
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
			this.gpSubscriberDetails = new System.Windows.Forms.GroupBox();
			this.txtCopyToAddress = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.txtVerifyPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtSubscriber = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtSubscriberSearch = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gpSelectSubscriber = new System.Windows.Forms.GroupBox();
			this.btnSelect = new System.Windows.Forms.Button();
			this.cmbSubscribers = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnChangeServices = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.gpSubscriberDetails.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gpSelectSubscriber.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpSubscriberDetails
			// 
			this.gpSubscriberDetails.Controls.Add(this.txtCopyToAddress);
			this.gpSubscriberDetails.Controls.Add(this.label4);
			this.gpSubscriberDetails.Controls.Add(this.label7);
			this.gpSubscriberDetails.Controls.Add(this.txtDescription);
			this.gpSubscriberDetails.Controls.Add(this.txtVerifyPassword);
			this.gpSubscriberDetails.Controls.Add(this.txtPassword);
			this.gpSubscriberDetails.Controls.Add(this.txtSubscriber);
			this.gpSubscriberDetails.Controls.Add(this.label3);
			this.gpSubscriberDetails.Controls.Add(this.label2);
			this.gpSubscriberDetails.Controls.Add(this.label1);
			this.gpSubscriberDetails.Location = new System.Drawing.Point(8, 104);
			this.gpSubscriberDetails.Name = "gpSubscriberDetails";
			this.gpSubscriberDetails.Size = new System.Drawing.Size(600, 224);
			this.gpSubscriberDetails.TabIndex = 2;
			this.gpSubscriberDetails.TabStop = false;
			// 
			// txtCopyToAddress
			// 
			this.txtCopyToAddress.Location = new System.Drawing.Point(160, 192);
			this.txtCopyToAddress.MaxLength = 255;
			this.txtCopyToAddress.Name = "txtCopyToAddress";
			this.txtCopyToAddress.Size = new System.Drawing.Size(416, 20);
			this.txtCopyToAddress.TabIndex = 9;
			this.txtCopyToAddress.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 18);
			this.label4.TabIndex = 8;
			this.label4.Text = "Receive Copy To URL";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 24);
			this.label7.TabIndex = 7;
			this.label7.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.AcceptsReturn = true;
			this.txtDescription.Location = new System.Drawing.Point(160, 120);
			this.txtDescription.MaxLength = 255;
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(416, 56);
			this.txtDescription.TabIndex = 3;
			this.txtDescription.Text = "";
			// 
			// txtVerifyPassword
			// 
			this.txtVerifyPassword.Location = new System.Drawing.Point(160, 88);
			this.txtVerifyPassword.MaxLength = 255;
			this.txtVerifyPassword.Name = "txtVerifyPassword";
			this.txtVerifyPassword.PasswordChar = '*';
			this.txtVerifyPassword.Size = new System.Drawing.Size(416, 20);
			this.txtVerifyPassword.TabIndex = 2;
			this.txtVerifyPassword.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(160, 56);
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
			this.label3.Location = new System.Drawing.Point(24, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Verify Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Subscriber ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(480, 376);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(128, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "&Update";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnSearch);
			this.groupBox3.Controls.Add(this.txtSubscriberSearch);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(600, 40);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
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
			// txtSubscriberSearch
			// 
			this.txtSubscriberSearch.Location = new System.Drawing.Point(160, 13);
			this.txtSubscriberSearch.MaxLength = 255;
			this.txtSubscriberSearch.Name = "txtSubscriberSearch";
			this.txtSubscriberSearch.Size = new System.Drawing.Size(264, 20);
			this.txtSubscriberSearch.TabIndex = 1;
			this.txtSubscriberSearch.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Search Subscriber";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpSelectSubscriber
			// 
			this.gpSelectSubscriber.Controls.Add(this.btnSelect);
			this.gpSelectSubscriber.Controls.Add(this.cmbSubscribers);
			this.gpSelectSubscriber.Controls.Add(this.label6);
			this.gpSelectSubscriber.Location = new System.Drawing.Point(8, 56);
			this.gpSelectSubscriber.Name = "gpSelectSubscriber";
			this.gpSelectSubscriber.Size = new System.Drawing.Size(600, 40);
			this.gpSelectSubscriber.TabIndex = 1;
			this.gpSelectSubscriber.TabStop = false;
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
			// cmbSubscribers
			// 
			this.cmbSubscribers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSubscribers.Location = new System.Drawing.Point(160, 11);
			this.cmbSubscribers.Name = "cmbSubscribers";
			this.cmbSubscribers.Size = new System.Drawing.Size(264, 21);
			this.cmbSubscribers.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Select Subscriber";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(344, 376);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 23);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnChangeServices
			// 
			this.btnChangeServices.Location = new System.Drawing.Point(8, 344);
			this.btnChangeServices.Name = "btnChangeServices";
			this.btnChangeServices.Size = new System.Drawing.Size(128, 23);
			this.btnChangeServices.TabIndex = 4;
			this.btnChangeServices.Text = "Change Services";
			this.btnChangeServices.Click += new System.EventHandler(this.btnChangeServices_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ModifySubscriber
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 408);
			this.Controls.Add(this.btnChangeServices);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.gpSelectSubscriber);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gpSubscriberDetails);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ModifySubscriber";
			this.Text = "Modify / Delete Subscriber";
			this.Load += new System.EventHandler(this.ModifySubscriber_Load);
			this.gpSubscriberDetails.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.gpSelectSubscriber.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnChangeServices_Click(object sender, System.EventArgs e)
		{
			ModifySubscriberServices fModServices = new ModifySubscriberServices();
			fModServices.txtSubscriberID.Text=txtSubscriber.Text;
			fModServices.ShowDialog(this);
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			cmbSubscribers.Items.Clear();
			//disable detail groups and buttons

			
			gpSelectSubscriber.Enabled=false;
			SetNoSubscriberSelected();
			//

			txtSubscriberSearch.Text=txtSubscriberSearch.Text.Trim();
			string sSubsSelectSQL;
			SqlCommand sSQLSearchCmd = new SqlCommand();
			sSQLSearchCmd.CommandType=CommandType.Text;

			if (txtSubscriberSearch.Text=="") //all users
			{
                sSubsSelectSQL = "SELECT SUBSCRIBER_ID FROM dbo.WS_SUBSCRIBER ORDER BY SUBSCRIBER_ID";
				sSQLSearchCmd.CommandText=sSubsSelectSQL;
				
			}
			else
			{
                sSubsSelectSQL = "SELECT SUBSCRIBER_ID FROM dbo.WS_SUBSCRIBER " + 
					" WHERE SUBSCRIBER_ID LIKE @SUBSCRIBER_ID ORDER BY SUBSCRIBER_ID";
				sSQLSearchCmd.CommandText=sSubsSelectSQL;
				SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
				sSubsParam.Value=txtSubscriberSearch.Text+"%" ;
				sSQLSearchCmd.Parameters.Add(sSubsParam);
			}
			

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr=null;
			try
			{
			
				dbSubs.ConnectToDB();

				sdr = dbSubs.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				while(sdr.Read())
				{
					cmbSubscribers.Items.Add(sdr.GetString(0));
				}
				
				if (cmbSubscribers.Items.Count!=0) 
				{
					gpSelectSubscriber.Enabled=true;
					cmbSubscribers.SelectedIndex=0;

				
				}
				else //no users matching criteria
				{
					gpSelectSubscriber.Enabled=false;
					MessageBox.Show("No Subscriber matching criteria","Admin Client");
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

		private void SetNoSubscriberSelected()
		{
			txtPassword.Text="";
			txtSubscriber.Text="";
			
			txtVerifyPassword.Text="";
			txtDescription.Text="";
			txtCopyToAddress.Text="";
			
			gpSubscriberDetails.Enabled=false;
			btnChangeServices.Enabled=false;
			btnSave.Enabled=false;
			btnDelete.Enabled=false;
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			
			#region Connect to DB and getData

			SqlCommand sSQLSearchCmd = new SqlCommand();
			sSQLSearchCmd.CommandType=CommandType.Text;
            string sSubsSelectSQL = "SELECT SUBSCRIBER_ID,SUBSCRIBER_PASSWORD,SUBSCRIBER_DESCRIPTION,COPY_TO_ADDRESS FROM dbo.WS_SUBSCRIBER LEFT OUTER JOIN dbo.COPY_TO_URLS ON SUBSCRIBER_ID = COPY_TO_ID " + 
				" WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID";
			sSQLSearchCmd.CommandText=sSubsSelectSQL;

			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value= cmbSubscribers.Text;
			sSQLSearchCmd.Parameters.Add(sSubsParam);
			

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr=null;
			string sDBSubsID, sDBPassword,sDBSubsDesc, sDBSubsCopyToAdd;
			try
			{
			
				dbSubs.ConnectToDB();

				sdr = dbSubs.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleRow);
				if(sdr.Read()==true)
				{
					
					sDBSubsID = sdr.GetString(0);
					sDBPassword = sdr.IsDBNull(1)?"": sdr.GetString(1); ; //encrypted
					sDBSubsDesc= sdr.GetString(2);
					sDBSubsCopyToAdd= sdr.IsDBNull(3)?"": sdr.GetString(3); 
				}
				else
				{
					gpSelectSubscriber.Enabled=false;
					MessageBox.Show("No Subscriber matching criteria","Admin Client");
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

			
			txtSubscriber.Text=sDBSubsID;
			txtSubscriber.ReadOnly=true;
			txtPassword.Text=CommonHelpers.CommonDecryptData(sDBPassword);
			txtVerifyPassword.Text=txtPassword.Text;
			txtDescription.Text=sDBSubsDesc;
			txtCopyToAddress.Text=sDBSubsCopyToAdd;
			


			gpSubscriberDetails.Enabled=true;
			btnDelete.Enabled=true;
			btnSave.Enabled=true;
			btnChangeServices.Enabled=true;
			#endregion

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			#region Validations
			
			txtPassword.Text=			txtPassword.Text.Trim();
			txtCopyToAddress.Text=txtCopyToAddress.Text.Trim();
			txtDescription.Text=txtDescription.Text.Trim();

			


			
			


			if (txtPassword.Text.Trim() !=txtVerifyPassword.Text.Trim()) 
			{
				errorProvider1.SetError(txtVerifyPassword,"Passwords should match");
				txtVerifyPassword.SelectAll();
				txtVerifyPassword.Focus();
				return;
			}
			errorProvider1.SetError(txtVerifyPassword,"");
			
			
			

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
		
			errorProvider1.SetError(txtCopyToAddress,"");
			


			
			
			#endregion


			#region connect to DB and Save

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

            string sUpdateSQL = "UPDATE dbo.WS_SUBSCRIBER "
				+ " SET  SUBSCRIBER_PASSWORD=@SUBSCRIBER_PASSWORD, SUBSCRIBER_DESCRIPTION=@SUBSCRIBER_DESCRIPTION,  LAST_UPDATE_USERID=@LAST_UPDATE_USERID, LAST_UPDATE_TIME=getdate() " +
				" WHERE SUBSCRIBER_ID=@SUBSCRIBER_ID";


            string sDeleteCopyToSQL = "DELETE dbo.COPY_TO_URLS WHERE COPY_TO_ID=@COPY_TO_ID";

            string sInsertCopyToSQL = "INSERT INTO dbo.COPY_TO_URLS "
				+ " (COPY_TO_ID, COPY_TO_ADDRESS,  LAST_UPDATE_USERID, LAST_UPDATE_TIME) " +
				" VALUES " +
				"(@COPY_TO_ID, @COPY_TO_ADDRESS, @LAST_UPDATE_USERID, getdate())";


			SqlCommand sqlUpdateCmd = new SqlCommand(sUpdateSQL);
			SqlCommand sqlDeleteCopyToCmd = new SqlCommand(sDeleteCopyToSQL);
			SqlCommand sqlInsertCopyToCmd = new SqlCommand(sInsertCopyToSQL);



			#region set parameters
			// set parameter

			

			SqlParameter sSubsPWDParam = new SqlParameter("@SUBSCRIBER_PASSWORD",SqlDbType.NVarChar);
			if (txtPassword.Text=="")
			{
				sSubsPWDParam.Value=DBNull.Value;
			}
			else
			{
				sSubsPWDParam.Value=CommonHelpers.CommonEncryptData(txtPassword.Text);
			}
			sqlUpdateCmd.Parameters.Add(sSubsPWDParam);


			
			SqlParameter sSubsDescription = new SqlParameter("@SUBSCRIBER_DESCRIPTION",SqlDbType.NVarChar);
			sSubsDescription.Value=txtDescription.Text.Trim();
			sqlUpdateCmd.Parameters.Add(sSubsDescription);
			
			
			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlUpdateCmd.Parameters.Add(sLastUserParam);
			
			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriber.Text.Trim();
			sqlUpdateCmd.Parameters.Add(sSubsParam);


			//delete from copyto table any way


			SqlParameter sCopyToIDParamDel = new SqlParameter("@COPY_TO_ID",SqlDbType.NVarChar);
			sCopyToIDParamDel.Value=txtSubscriber.Text.Trim();
			sqlDeleteCopyToCmd.Parameters.Add(sCopyToIDParamDel);


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
				int nRowsAffected;
				nRowsAffected= dbSubs.ExecuteNonQuery(sqlUpdateCmd);

				if (nRowsAffected==0)
				{
					MessageBox.Show("User not updated successfully. Pls. retry", "Admin Client", MessageBoxButtons.OK,MessageBoxIcon.Warning);
					dbSubs.RollbackTransaction();
					return;		
				};
	


				dbSubs.ExecuteNonQuery(sqlDeleteCopyToCmd);

				if (txtCopyToAddress.Text!="")
				{
					dbSubs.ExecuteNonQuery(sqlInsertCopyToCmd);
				}
				dbSubs.CommitTransaction();

				MessageBox.Show("Subscriber Updated successfully", "Admin Client");

				

			}
			catch (SqlException exSQL)
			{
				dbSubs.RollbackTransaction();
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
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
			
			#region check commands

            string sCheckCopyToSQL = "SELECT COUNT(1) FROM dbo.WS_SUBSCRIBER_SERVICES " +
				" WHERE COPY_TO_ID= @SUBSCRIBER_ID";
			SqlCommand sqlCheckCopyToCmd = new SqlCommand(sCheckCopyToSQL);

            string sCheckKeysSQL = "SELECT COUNT(1) FROM dbo.SUBSCRIBER_ENCRYPTION_KEYS " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID";
			SqlCommand sqlCheckKeysCmd = new SqlCommand(sCheckKeysSQL);




			#endregion

			#region Del commands

            string sDelCopyToURLSQL = "DELETE dbo.COPY_TO_URLS " +
				" WHERE COPY_TO_ID= @SUBSCRIBER_ID";
			SqlCommand sqlDeleteCopyToCmd = new SqlCommand(sDelCopyToURLSQL);


            string sDelMethodsSQL = "DELETE dbo.WS_SUBSCRIBER_SERVICE_METHOD " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID";
			SqlCommand sqlDeleteMethodsCmd = new SqlCommand(sDelMethodsSQL);

            string sDelServicesSQL = "DELETE dbo.WS_SUBSCRIBER_SERVICES " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID";
			SqlCommand sqlDeleteServicesCmd = new SqlCommand(sDelServicesSQL);


            string sDelSubsSQL = "DELETE dbo.WS_SUBSCRIBER " +
				" WHERE SUBSCRIBER_ID= @SUBSCRIBER_ID";
			SqlCommand sqlDeleteSubsCmd = new SqlCommand(sDelSubsSQL);


			#endregion


			#region set parameters
			// set parameter

//check commands

			SqlParameter sCopyCheckParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sCopyCheckParam.Value=txtSubscriber.Text;
			sqlCheckCopyToCmd.Parameters.Add(sCopyCheckParam);


			SqlParameter sKeyCheckParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sKeyCheckParam.Value=txtSubscriber.Text;
			sqlCheckKeysCmd.Parameters.Add(sKeyCheckParam);

			//delete commands


			SqlParameter sSubsCopyToParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsCopyToParam.Value=txtSubscriber.Text;
			sqlDeleteCopyToCmd.Parameters.Add(sSubsCopyToParam);


			SqlParameter sSubsMethodParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsMethodParam.Value=txtSubscriber.Text;
			sqlDeleteMethodsCmd.Parameters.Add(sSubsMethodParam);

			SqlParameter sSubsServicesParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsServicesParam.Value=txtSubscriber.Text;
			sqlDeleteServicesCmd.Parameters.Add(sSubsServicesParam);

			SqlParameter sSubsParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sSubsParam.Value=txtSubscriber.Text;
			sqlDeleteSubsCmd.Parameters.Add(sSubsParam);

			
			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
			
				#region do checks

				int nCopyCount= (int)dbSubs.ExecuteScaler(sqlCheckCopyToCmd);

				if (nCopyCount>0)
				{
				
					MessageBox.Show("Subscriber is receiving Copies, cannot delete.", "Admin Client");
					return;

				}

				int nKeyCount= (int)dbSubs.ExecuteScaler(sqlCheckKeysCmd);
				if (nKeyCount>0)
				{
				
					MessageBox.Show("Subscriber has encryption keys assigned, cannot delete.", "Admin Client");
					return;

				}

				#endregion
				
				
				
				dbSubs.BeginTransaction();

				dbSubs.ExecuteNonQuery(sqlDeleteCopyToCmd);
				dbSubs.ExecuteNonQuery(sqlDeleteMethodsCmd);
				dbSubs.ExecuteNonQuery(sqlDeleteServicesCmd);
				dbSubs.ExecuteNonQuery(sqlDeleteSubsCmd);
				
				dbSubs.CommitTransaction();
				MessageBox.Show("Subscriber Deleted successfully", "Admin Client");

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

		private void ModifySubscriber_Load(object sender, System.EventArgs e)
		{
			#region set controls
	
			gpSelectSubscriber.Enabled=false;
			gpSubscriberDetails.Enabled=false;
			btnDelete.Enabled=false;
			btnSave.Enabled=false;
			btnChangeServices.Enabled=false;
			#endregion

		}
	}
}
