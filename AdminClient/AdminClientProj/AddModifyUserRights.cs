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
	public class AddModifyUserRights : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.ListView lstUserPermissions;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnChk;
		internal System.Windows.Forms.TextBox txtUserID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddModifyUserRights()
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
			this.lstUserPermissions = new System.Windows.Forms.ListView();
			this.columnChk = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lstUserPermissions);
			this.groupBox1.Location = new System.Drawing.Point(8, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 320);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// lstUserPermissions
			// 
			this.lstUserPermissions.CheckBoxes = true;
			this.lstUserPermissions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnChk,
																								 this.columnHeader1,
																								 this.columnHeader2});
			this.lstUserPermissions.FullRowSelect = true;
			this.lstUserPermissions.GridLines = true;
			this.lstUserPermissions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstUserPermissions.LabelWrap = false;
			this.lstUserPermissions.Location = new System.Drawing.Point(8, 16);
			this.lstUserPermissions.Name = "lstUserPermissions";
			this.lstUserPermissions.Size = new System.Drawing.Size(584, 296);
			this.lstUserPermissions.TabIndex = 0;
			this.lstUserPermissions.View = System.Windows.Forms.View.Details;
			this.lstUserPermissions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstUserPermissions_ItemCheck);
			// 
			// columnChk
			// 
			this.columnChk.Text = "";
			this.columnChk.Width = 20;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Permission Code";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Permission Description";
			this.columnHeader2.Width = 450;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(8, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtUserID);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(600, 40);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(160, 13);
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.ReadOnly = true;
			this.txtUserID.Size = new System.Drawing.Size(264, 20);
			this.txtUserID.TabIndex = 1;
			this.txtUserID.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "UserID";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(480, 376);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(128, 23);
			this.btnUpdate.TabIndex = 2;
			this.btnUpdate.Text = "&Save Permissions";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// AddModifyUserRights
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(616, 405);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnUpdate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddModifyUserRights";
			this.Text = "User Permissions";
			this.Load += new System.EventHandler(this.AddModifyUserRights_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void AddModifyUserRights_Load(object sender, System.EventArgs e)
		{
			lstUserPermissions.Items.Clear();
			lstUserPermissions.LabelEdit=false;
			lstUserPermissions.FullRowSelect=false;
			

			#region Get All rights from rights master order by id and left joined on rights_user

		
			CommonDBHelper dbSubsDB = new CommonDBHelper((string) frmMain.HTConnectionStrings["SubscriberDB"]);


            string sRightsSQL = "SELECT RM.RTS_RIGHT, RM.RTS_RIGHT_DESCRIPTION,RU.RTS_USER_ID FROM dbo.RTS_RIGHTS_MASTER RM LEFT OUTER JOIN ( SELECT RTS_USER_ID, RTS_RIGHT FROM dbo.RTS_USER_RIGHTS WHERE RTS_USER_ID=@RTS_USER_ID ) RU ON RM.RTS_RIGHT=RU.RTS_RIGHT " + 
" ORDER BY RM.RTS_RIGHT";

 
			SqlCommand sqlRights = new SqlCommand(sRightsSQL);

			// set parameter

			SqlParameter sUserParam = new SqlParameter();
			sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlRights.Parameters.Add(sUserParam);


			IDataReader sdr;
			sdr = null;
			try
			{
				dbSubsDB.ConnectToDB();

				sdr = dbSubsDB.GetDataReader(sqlRights,CommandBehavior.SingleResult);
				
				while (sdr.Read())
				{
					int rightID;
					string sRightDescription, sUserID;

					rightID= sdr.GetInt32(0);
					sRightDescription= sdr.GetString(1);
					sUserID= sdr.IsDBNull(2)?null:  sdr.GetString(2);

					ListViewItem lvi = new ListViewItem(new string[]{"",rightID.ToString(),sRightDescription});
				
					lstUserPermissions.Items.Add(lvi);
					if (sUserID!=null)
					{
						lvi.Checked=true;
					}
					
					
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

			btnUpdate.Enabled=false;


		}

		private void lstUserPermissions_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			btnUpdate.Enabled=true;
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			btnUpdate.Enabled=false;

			#region connect db and begin transaction
			
			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
				
			string sDelRightsSQL = "DELETE dbo.RTS_USER_RIGHTS " +
				" WHERE RTS_USER_ID= @RTS_USER_ID";

			SqlCommand sqlDeleteRightsCmd = new SqlCommand(sDelRightsSQL);

			string sInsertRightsSQL = "INSERT dbo.RTS_USER_RIGHTS "+ " (RTS_USER_ID, RTS_RIGHT,  LAST_UPDATE_USERID, LAST_UPDATE_TIME) " +
				" VALUES " +
				"(@RTS_USER_ID, @RTS_RIGHT, @LAST_UPDATE_USERID, getdate())";

			SqlCommand sqlInsertRightCmd = new SqlCommand(sInsertRightsSQL);



			#region set parameters
			// set parameter

			SqlParameter sUserRightParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserRightParam.Value=txtUserID.Text.Trim();
			sqlDeleteRightsCmd.Parameters.Add(sUserRightParam);

			//set the userid and lastupdate id for the insert command
			//later in the loop just change the rightid

			SqlParameter sUserParam = new SqlParameter("@RTS_USER_ID",SqlDbType.NVarChar);
			sUserParam.Value=txtUserID.Text.Trim();
			sqlInsertRightCmd.Parameters.Add(sUserParam);


			SqlParameter sRightParam= new SqlParameter("@RTS_RIGHT",SqlDbType.Int);
			sqlInsertRightCmd.Parameters.Add(sRightParam); //value set in Loop

			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sqlInsertRightCmd.Parameters.Add(sLastUserParam);


			#endregion


			try
			{
			
				dbSubs.ConnectToDB();
				dbSubs.BeginTransaction();

				dbSubs.ExecuteNonQuery(sqlDeleteRightsCmd);

				foreach (ListViewItem lvi in lstUserPermissions.Items)
				{
					
					//if checked
					if (lvi.Checked==true)
					{

						//1th item contains the id
						sqlInsertRightCmd.Parameters["@RTS_RIGHT"].Value= int.Parse( lvi.SubItems[1].Text);
						dbSubs.ExecuteNonQuery(sqlInsertRightCmd);
					}
				}
				dbSubs.CommitTransaction();
				MessageBox.Show("Rights Updated successfully", "Admin Client");
				this.Close();

					
				

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

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
