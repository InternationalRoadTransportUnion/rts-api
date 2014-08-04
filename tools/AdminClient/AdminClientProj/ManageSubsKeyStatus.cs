using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for ManageSubsKeys.
	/// </summary>
	public class ManageSubsKeys : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpSelectSubscriber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboSubscriber;
		private System.Windows.Forms.Button dtnSelectSubscriber;
		private System.Windows.Forms.GroupBox gpCertificates;
		private System.Windows.Forms.ListView lvwCertificates;
		private System.Windows.Forms.GroupBox gpButtons;
		private System.Windows.Forms.Button btnMarkActive;
		private System.Windows.Forms.Button btnMarkInactive;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.ColumnHeader col_ACTIVE_STATUS;
		private System.Windows.Forms.ColumnHeader col_SUBSCRIBER_ID;
		private System.Windows.Forms.ColumnHeader col_CERT_RECEIVED_DATE;
		private System.Windows.Forms.ColumnHeader col_ThumbPrint;
		private System.Windows.Forms.ColumnHeader col_ActiveStatusReason;
		private System.Windows.Forms.ColumnHeader col_LastUpdatedBy;
		private System.Windows.Forms.ColumnHeader col_LastUpdatedDate;

		private System.Windows.Forms.SaveFileDialog sfd;
        private ColumnHeader col_CERT_EXPIRY_DATE;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ManageSubsKeys()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

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
            this.gpSelectSubscriber = new System.Windows.Forms.GroupBox();
            this.dtnSelectSubscriber = new System.Windows.Forms.Button();
            this.cboSubscriber = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpCertificates = new System.Windows.Forms.GroupBox();
            this.lvwCertificates = new System.Windows.Forms.ListView();
            this.col_ThumbPrint = new System.Windows.Forms.ColumnHeader();
            this.col_ACTIVE_STATUS = new System.Windows.Forms.ColumnHeader();
            this.col_SUBSCRIBER_ID = new System.Windows.Forms.ColumnHeader();
            this.col_CERT_RECEIVED_DATE = new System.Windows.Forms.ColumnHeader();
            this.col_ActiveStatusReason = new System.Windows.Forms.ColumnHeader();
            this.col_LastUpdatedBy = new System.Windows.Forms.ColumnHeader();
            this.col_LastUpdatedDate = new System.Windows.Forms.ColumnHeader();
            this.gpButtons = new System.Windows.Forms.GroupBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnMarkInactive = new System.Windows.Forms.Button();
            this.btnMarkActive = new System.Windows.Forms.Button();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.col_CERT_EXPIRY_DATE = new System.Windows.Forms.ColumnHeader();
            this.gpSelectSubscriber.SuspendLayout();
            this.gpCertificates.SuspendLayout();
            this.gpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpSelectSubscriber
            // 
            this.gpSelectSubscriber.Controls.Add(this.dtnSelectSubscriber);
            this.gpSelectSubscriber.Controls.Add(this.cboSubscriber);
            this.gpSelectSubscriber.Controls.Add(this.label1);
            this.gpSelectSubscriber.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpSelectSubscriber.Location = new System.Drawing.Point(0, 0);
            this.gpSelectSubscriber.Name = "gpSelectSubscriber";
            this.gpSelectSubscriber.Size = new System.Drawing.Size(800, 56);
            this.gpSelectSubscriber.TabIndex = 0;
            this.gpSelectSubscriber.TabStop = false;
            // 
            // dtnSelectSubscriber
            // 
            this.dtnSelectSubscriber.Location = new System.Drawing.Point(272, 16);
            this.dtnSelectSubscriber.Name = "dtnSelectSubscriber";
            this.dtnSelectSubscriber.Size = new System.Drawing.Size(112, 24);
            this.dtnSelectSubscriber.TabIndex = 2;
            this.dtnSelectSubscriber.Text = "Select";
            this.dtnSelectSubscriber.Click += new System.EventHandler(this.dtnSelectSubscriber_Click);
            // 
            // cboSubscriber
            // 
            this.cboSubscriber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubscriber.Location = new System.Drawing.Point(144, 16);
            this.cboSubscriber.Name = "cboSubscriber";
            this.cboSubscriber.Size = new System.Drawing.Size(112, 21);
            this.cboSubscriber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subscriber ";
            // 
            // gpCertificates
            // 
            this.gpCertificates.Controls.Add(this.lvwCertificates);
            this.gpCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpCertificates.Location = new System.Drawing.Point(0, 56);
            this.gpCertificates.Name = "gpCertificates";
            this.gpCertificates.Size = new System.Drawing.Size(800, 294);
            this.gpCertificates.TabIndex = 1;
            this.gpCertificates.TabStop = false;
            // 
            // lvwCertificates
            // 
            this.lvwCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwCertificates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_ThumbPrint,
            this.col_ACTIVE_STATUS,
            this.col_SUBSCRIBER_ID,
            this.col_CERT_RECEIVED_DATE,
            this.col_CERT_EXPIRY_DATE,
            this.col_ActiveStatusReason,
            this.col_LastUpdatedBy,
            this.col_LastUpdatedDate});
            this.lvwCertificates.FullRowSelect = true;
            this.lvwCertificates.Location = new System.Drawing.Point(3, 19);
            this.lvwCertificates.MultiSelect = false;
            this.lvwCertificates.Name = "lvwCertificates";
            this.lvwCertificates.Size = new System.Drawing.Size(794, 197);
            this.lvwCertificates.TabIndex = 0;
            this.lvwCertificates.UseCompatibleStateImageBehavior = false;
            this.lvwCertificates.View = System.Windows.Forms.View.Details;
            this.lvwCertificates.SelectedIndexChanged += new System.EventHandler(this.lvwCertificates_SelectedIndexChanged);
            // 
            // col_ThumbPrint
            // 
            this.col_ThumbPrint.Text = "Thumb Print";
            this.col_ThumbPrint.Width = 250;
            // 
            // col_ACTIVE_STATUS
            // 
            this.col_ACTIVE_STATUS.Text = "Active Status";
            this.col_ACTIVE_STATUS.Width = 80;
            // 
            // col_SUBSCRIBER_ID
            // 
            this.col_SUBSCRIBER_ID.Text = "Distributed To";
            this.col_SUBSCRIBER_ID.Width = 117;
            // 
            // col_CERT_RECEIVED_DATE
            // 
            this.col_CERT_RECEIVED_DATE.Text = "Distribution Date";
            this.col_CERT_RECEIVED_DATE.Width = 126;
            // 
            // col_ActiveStatusReason
            // 
            this.col_ActiveStatusReason.Text = "Status reason";
            this.col_ActiveStatusReason.Width = 119;
            // 
            // col_LastUpdatedBy
            // 
            this.col_LastUpdatedBy.Text = "Last Updated By";
            this.col_LastUpdatedBy.Width = 100;
            // 
            // col_LastUpdatedDate
            // 
            this.col_LastUpdatedDate.Text = "Last Updated Date";
            this.col_LastUpdatedDate.Width = 120;
            // 
            // gpButtons
            // 
            this.gpButtons.Controls.Add(this.Cancel);
            this.gpButtons.Controls.Add(this.btnExport);
            this.gpButtons.Controls.Add(this.btnMarkInactive);
            this.gpButtons.Controls.Add(this.btnMarkActive);
            this.gpButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpButtons.Location = new System.Drawing.Point(0, 270);
            this.gpButtons.Name = "gpButtons";
            this.gpButtons.Size = new System.Drawing.Size(800, 80);
            this.gpButtons.TabIndex = 2;
            this.gpButtons.TabStop = false;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(16, 40);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(112, 24);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(640, 40);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 24);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export .Cer";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnMarkInactive
            // 
            this.btnMarkInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkInactive.Enabled = false;
            this.btnMarkInactive.Location = new System.Drawing.Point(504, 40);
            this.btnMarkInactive.Name = "btnMarkInactive";
            this.btnMarkInactive.Size = new System.Drawing.Size(112, 24);
            this.btnMarkInactive.TabIndex = 1;
            this.btnMarkInactive.Text = "Mark Inactive";
            this.btnMarkInactive.Click += new System.EventHandler(this.btnMarkInactive_Click);
            // 
            // btnMarkActive
            // 
            this.btnMarkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkActive.Enabled = false;
            this.btnMarkActive.Location = new System.Drawing.Point(352, 40);
            this.btnMarkActive.Name = "btnMarkActive";
            this.btnMarkActive.Size = new System.Drawing.Size(112, 24);
            this.btnMarkActive.TabIndex = 0;
            this.btnMarkActive.Text = "Mark Active";
            this.btnMarkActive.Click += new System.EventHandler(this.btnMarkActive_Click);
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "cer";
            this.sfd.Title = "Select Folder and Certificate File Name";
            // 
            // col_CERT_EXPIRY_DATE
            // 
            this.col_CERT_EXPIRY_DATE.Text = "Expiration Date";
            this.col_CERT_EXPIRY_DATE.Width = 120;
            // 
            // ManageSubsKeys
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(800, 350);
            this.Controls.Add(this.gpButtons);
            this.Controls.Add(this.gpCertificates);
            this.Controls.Add(this.gpSelectSubscriber);
            this.Name = "ManageSubsKeys";
            this.Text = "Manage Subscriber  Keys";
            this.Load += new System.EventHandler(this.ManageSubsKeys_Load);
            this.gpSelectSubscriber.ResumeLayout(false);
            this.gpCertificates.ResumeLayout(false);
            this.gpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void PopulateSubsCombo()
		{
			CommonHelpers.PopulateSubsCombo(cboSubscriber);
			if(cboSubscriber.Items.Count > 0)
			{
				cboSubscriber.SelectedIndex=0;
			}
		
		}

		private DataSet m_KeysDataSet;

		private DataSet getKeysDS()
		{
			string sCertSelect = "SELECT [ENCRYPTION_KEY_ID],[SUBSCRIBER_ID],[CERT_RECEIVED_DATE] ,[KEY_ACTIVE],[KEY_ACTIVE_REASON],[CERT_EXPIRY_DATE]  ,[CERT_BLOB],[LAST_UPDATE_USERID],[LAST_UPDATE_TIME] ";

            sCertSelect += " FROM dbo.[SUBSCRIBER_ENCRYPTION_KEYS]"; 

			sCertSelect+=	 " WHERE SUBSCRIBER_ID = @SUBSCRIBER_ID ORDER BY CERT_RECEIVED_DATE";
			SqlCommand sCertCommand = new SqlCommand(sCertSelect);
			sCertCommand.Parameters.Add(new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar,255));
			sCertCommand.Parameters["@SUBSCRIBER_ID"].Value=cboSubscriber.Text;
			
			CommonDBHelper subsConn = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			DataSet dsKeys = null;
			try
			{
				dsKeys = new DataSet();
				subsConn.FillDataSetTable(sCertCommand,dsKeys,"Keys");
			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return null;
			}
			finally
			{
				subsConn.Close();
			}

			return dsKeys;
		}
		private void dtnSelectSubscriber_Click(object sender, System.EventArgs e)
		{
			PopulateCertificates();		
			UpdateButtonStatus();

 
		}

		private void PopulateCertificates()
		{
			m_KeysDataSet= getKeysDS();
			if (m_KeysDataSet==null) //DB error occurred
			{
				return;
			}


			#region populate listview

			lvwCertificates.Items.Clear();
			foreach (DataRow dr in m_KeysDataSet.Tables["Keys"].Rows)
			{
                ListViewItem lItem = new ListViewItem(new string[] { dr["ENCRYPTION_KEY_ID"].ToString(), dr["KEY_ACTIVE"].ToString(), dr["SUBSCRIBER_ID"].ToString(), dr["CERT_RECEIVED_DATE"].ToString(), dr["CERT_EXPIRY_DATE"].ToString(), dr["KEY_ACTIVE_REASON"].ToString(), dr["LAST_UPDATE_USERID"].ToString(), dr["LAST_UPDATE_TIME"].ToString() });
				lvwCertificates.Items.Add(lItem);			
			}

			
			#endregion

		
		}

		private void lvwCertificates_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			UpdateButtonStatus();		
		}


		private void UpdateButtonStatus()
		{
			if (lvwCertificates.SelectedItems.Count==0) 
			{
				btnMarkInactive.Enabled=false;
				btnMarkActive.Enabled=false;
				btnExport.Enabled=false;
				return;
			
			};
			ListViewItem lSelected = lvwCertificates.SelectedItems[0];
			string thumbPrint = lSelected.SubItems[0].Text;
			DataRow[] drSelected= m_KeysDataSet.Tables["Keys"].Select("ENCRYPTION_KEY_ID ='" + thumbPrint + "'");
			if ((bool)drSelected[0]["KEY_ACTIVE"])
			{
				btnMarkInactive.Enabled=true;
			
			}
			else
			{
				btnMarkInactive.Enabled=false;
			}
			btnMarkActive.Enabled=!btnMarkInactive.Enabled;
			btnExport.Enabled=true;

		
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			if (lvwCertificates.SelectedItems.Count==0) return;

			DialogResult dr = sfd.ShowDialog();
			if (dr==DialogResult.Cancel)
				return;

			
			ListViewItem lSelected = lvwCertificates.SelectedItems[0];

			string thumbPrint = lSelected.SubItems[0].Text;
			DataRow[] drSelected= m_KeysDataSet.Tables["Keys"].Select("ENCRYPTION_KEY_ID ='" + thumbPrint + "'");
			byte[] aDataExport = (byte[])drSelected[0]["CERT_BLOB"];

			FileStream fs  = new FileStream(sfd.FileName,FileMode.CreateNew,FileAccess.Write,FileShare.None);
			try
			{
				fs.Write(aDataExport,0,aDataExport.Length);
			}
			finally
			{
				fs.Close();
			}
		}

		private void btnMarkActive_Click(object sender, System.EventArgs e)
		{
			if (lvwCertificates.SelectedItems.Count==0) return;

		

			
			ListViewItem lSelected = lvwCertificates.SelectedItems[0];

			string thumbPrint = lSelected.SubItems[0].Text;
			ChangeKeyStatus(thumbPrint,1);
			//update the list
			
			PopulateCertificates();
			UpdateButtonStatus();


					
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="NewStatus">1 Active, 2 inactive</param>
		private void ChangeKeyStatus(string ThumpPrint, int NewStatus)
		{
			DataRow[] drSelected= m_KeysDataSet.Tables["Keys"].Select("ENCRYPTION_KEY_ID ='" + ThumpPrint + "'");

			DateTime expDate = (DateTime)drSelected[0]["CERT_EXPIRY_DATE"];

            if ((NewStatus == 1) && (expDate < DateTime.Now))
			{
				MessageBox.Show("Certificate already expired. Cannot continue");
				return;
			}
            string sUpdate = "UPDATE dbo.SUBSCRIBER_ENCRYPTION_KEYS set KEY_ACTIVE = @KEY_ACTIVE , LAST_UPDATE_TIME = @LAST_UPDATE_TIME, LAST_UPDATE_USERID=@LAST_UPDATE_USERID WHERE ENCRYPTION_KEY_ID= @ENCRYPTION_KEY_ID";
			SqlCommand sCertCommand = new SqlCommand(sUpdate);

			sCertCommand.Parameters.Add(new SqlParameter("@KEY_ACTIVE",SqlDbType.Bit));
			sCertCommand.Parameters["@KEY_ACTIVE"].Value=NewStatus;
			
			sCertCommand.Parameters.Add(new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime));
			sCertCommand.Parameters["@LAST_UPDATE_TIME"].Value=DateTime.Now;
			
			sCertCommand.Parameters.Add(new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar));
			sCertCommand.Parameters["@LAST_UPDATE_USERID"].Value=frmMain.UserID;

			sCertCommand.Parameters.Add(new SqlParameter("@ENCRYPTION_KEY_ID",SqlDbType.NVarChar,255));
			sCertCommand.Parameters["@ENCRYPTION_KEY_ID"].Value=ThumpPrint;
			
			CommonDBHelper subsConn = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			try
			{
				subsConn.ConnectToDB();
				int nRowsAffected=subsConn.ExecuteNonQuery(sCertCommand);
				if (nRowsAffected!=1)
				{
					MessageBox.Show("Unable to Update, Refresh data to continue.");
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
				subsConn.Close();
			}


		}
		private void btnMarkInactive_Click(object sender, System.EventArgs e)
		{
			if (lvwCertificates.SelectedItems.Count==0) return;

		

			
			ListViewItem lSelected = lvwCertificates.SelectedItems[0];

			string thumbPrint = lSelected.SubItems[0].Text;
			ChangeKeyStatus(thumbPrint,0);
			//update the list
			
			PopulateCertificates();
			UpdateButtonStatus();

		
		}

		private void ManageSubsKeys_Load(object sender, System.EventArgs e)
		{
		UpdateButtonStatus();
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
