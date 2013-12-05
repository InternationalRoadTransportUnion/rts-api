using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for ManageAlerts.
	/// </summary>
	public class ManageAlerts : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpService;
		private System.Windows.Forms.ComboBox cmbServices;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btnSvcSelect;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnDelete;


		
		private System.Windows.Forms.TextBox txtFaultType;
		private System.Windows.Forms.TextBox txtFaultDescription;
		private System.Windows.Forms.TextBox txtFaultSQL;
		private System.Windows.Forms.Button btnFaultSelect;
		private System.Windows.Forms.Button btnNewFault;
		private System.Windows.Forms.GroupBox gpFltHeader;
		private System.Windows.Forms.GroupBox gpExecutionDetails;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtThreshHoldQuantity;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtNoFaultDelay;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtFaultNoAlertDelay;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtAlertDelay;
		private System.Windows.Forms.TextBox txtLastCount;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtLastRuntime;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtNextRunTime;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ComboBox cmbFaultIDs;
		private System.Windows.Forms.GroupBox gpSelectFault;

		
		

		public ManageAlerts()
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
			this.components = new System.ComponentModel.Container();
			this.gpService = new System.Windows.Forms.GroupBox();
			this.btnSvcSelect = new System.Windows.Forms.Button();
			this.cmbServices = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.gpFltHeader = new System.Windows.Forms.GroupBox();
			this.txtThreshHoldQuantity = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFaultDescription = new System.Windows.Forms.TextBox();
			this.txtFaultSQL = new System.Windows.Forms.TextBox();
			this.txtFaultType = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtNoFaultDelay = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtFaultNoAlertDelay = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtAlertDelay = new System.Windows.Forms.TextBox();
			this.gpExecutionDetails = new System.Windows.Forms.GroupBox();
			this.txtNextRunTime = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtLastRuntime = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtLastCount = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gpSelectFault = new System.Windows.Forms.GroupBox();
			this.btnNewFault = new System.Windows.Forms.Button();
			this.btnFaultSelect = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.cmbFaultIDs = new System.Windows.Forms.ComboBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gpService.SuspendLayout();
			this.gpFltHeader.SuspendLayout();
			this.gpExecutionDetails.SuspendLayout();
			this.gpSelectFault.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpService
			// 
			this.gpService.Controls.Add(this.btnSvcSelect);
			this.gpService.Controls.Add(this.cmbServices);
			this.gpService.Controls.Add(this.label6);
			this.gpService.Location = new System.Drawing.Point(16, 16);
			this.gpService.Name = "gpService";
			this.gpService.Size = new System.Drawing.Size(600, 48);
			this.gpService.TabIndex = 0;
			this.gpService.TabStop = false;
			// 
			// btnSvcSelect
			// 
			this.btnSvcSelect.Location = new System.Drawing.Point(320, 16);
			this.btnSvcSelect.Name = "btnSvcSelect";
			this.btnSvcSelect.Size = new System.Drawing.Size(128, 20);
			this.btnSvcSelect.TabIndex = 1;
			this.btnSvcSelect.Text = "Select";
			this.btnSvcSelect.Click += new System.EventHandler(this.btnSvcSelect_Click);
			// 
			// cmbServices
			// 
			this.cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbServices.Location = new System.Drawing.Point(144, 16);
			this.cmbServices.Name = "cmbServices";
			this.cmbServices.Size = new System.Drawing.Size(144, 21);
			this.cmbServices.TabIndex = 0;
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
			// gpFltHeader
			// 
			this.gpFltHeader.Controls.Add(this.txtThreshHoldQuantity);
			this.gpFltHeader.Controls.Add(this.label4);
			this.gpFltHeader.Controls.Add(this.txtFaultDescription);
			this.gpFltHeader.Controls.Add(this.txtFaultSQL);
			this.gpFltHeader.Controls.Add(this.txtFaultType);
			this.gpFltHeader.Controls.Add(this.label3);
			this.gpFltHeader.Controls.Add(this.label2);
			this.gpFltHeader.Controls.Add(this.label1);
			this.gpFltHeader.Controls.Add(this.label5);
			this.gpFltHeader.Controls.Add(this.txtNoFaultDelay);
			this.gpFltHeader.Controls.Add(this.label7);
			this.gpFltHeader.Controls.Add(this.txtFaultNoAlertDelay);
			this.gpFltHeader.Controls.Add(this.label8);
			this.gpFltHeader.Controls.Add(this.txtAlertDelay);
			this.gpFltHeader.Location = new System.Drawing.Point(16, 112);
			this.gpFltHeader.Name = "gpFltHeader";
			this.gpFltHeader.Size = new System.Drawing.Size(600, 224);
			this.gpFltHeader.TabIndex = 2;
			this.gpFltHeader.TabStop = false;
			// 
			// txtThreshHoldQuantity
			// 
			this.txtThreshHoldQuantity.Location = new System.Drawing.Point(144, 152);
			this.txtThreshHoldQuantity.MaxLength = 10;
			this.txtThreshHoldQuantity.Name = "txtThreshHoldQuantity";
			this.txtThreshHoldQuantity.Size = new System.Drawing.Size(88, 20);
			this.txtThreshHoldQuantity.TabIndex = 3;
			this.txtThreshHoldQuantity.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 24);
			this.label4.TabIndex = 5;
			this.label4.Text = "Threshold Quantity";
			// 
			// txtFaultDescription
			// 
			this.txtFaultDescription.Location = new System.Drawing.Point(144, 48);
			this.txtFaultDescription.MaxLength = 50;
			this.txtFaultDescription.Name = "txtFaultDescription";
			this.txtFaultDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFaultDescription.Size = new System.Drawing.Size(408, 20);
			this.txtFaultDescription.TabIndex = 1;
			this.txtFaultDescription.Text = "";
			// 
			// txtFaultSQL
			// 
			this.txtFaultSQL.Location = new System.Drawing.Point(144, 80);
			this.txtFaultSQL.MaxLength = 3000;
			this.txtFaultSQL.Multiline = true;
			this.txtFaultSQL.Name = "txtFaultSQL";
			this.txtFaultSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFaultSQL.Size = new System.Drawing.Size(408, 56);
			this.txtFaultSQL.TabIndex = 2;
			this.txtFaultSQL.Text = "";
			// 
			// txtFaultType
			// 
			this.txtFaultType.Location = new System.Drawing.Point(144, 16);
			this.txtFaultType.MaxLength = 50;
			this.txtFaultType.Name = "txtFaultType";
			this.txtFaultType.ReadOnly = true;
			this.txtFaultType.Size = new System.Drawing.Size(144, 20);
			this.txtFaultType.TabIndex = 0;
			this.txtFaultType.Text = "";
			this.toolTip1.SetToolTip(this.txtFaultType, "Provide a positive integer.");
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 31);
			this.label3.TabIndex = 4;
			this.label3.Text = "Fault Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 31);
			this.label2.TabIndex = 2;
			this.label2.Text = "Fault SQL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fault Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 24);
			this.label5.TabIndex = 5;
			this.label5.Text = "No Fault Delay (mins)";
			// 
			// txtNoFaultDelay
			// 
			this.txtNoFaultDelay.Location = new System.Drawing.Point(144, 184);
			this.txtNoFaultDelay.MaxLength = 10;
			this.txtNoFaultDelay.Name = "txtNoFaultDelay";
			this.txtNoFaultDelay.Size = new System.Drawing.Size(88, 20);
			this.txtNoFaultDelay.TabIndex = 5;
			this.txtNoFaultDelay.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(272, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 24);
			this.label7.TabIndex = 5;
			this.label7.Text = "Faults No Alert Delay (mins)";
			// 
			// txtFaultNoAlertDelay
			// 
			this.txtFaultNoAlertDelay.Location = new System.Drawing.Point(416, 152);
			this.txtFaultNoAlertDelay.MaxLength = 10;
			this.txtFaultNoAlertDelay.Name = "txtFaultNoAlertDelay";
			this.txtFaultNoAlertDelay.Size = new System.Drawing.Size(88, 20);
			this.txtFaultNoAlertDelay.TabIndex = 4;
			this.txtFaultNoAlertDelay.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(272, 184);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 24);
			this.label8.TabIndex = 5;
			this.label8.Text = "Alert Delay (mins)";
			// 
			// txtAlertDelay
			// 
			this.txtAlertDelay.Location = new System.Drawing.Point(416, 184);
			this.txtAlertDelay.MaxLength = 10;
			this.txtAlertDelay.Name = "txtAlertDelay";
			this.txtAlertDelay.Size = new System.Drawing.Size(88, 20);
			this.txtAlertDelay.TabIndex = 6;
			this.txtAlertDelay.Text = "";
			// 
			// gpExecutionDetails
			// 
			this.gpExecutionDetails.Controls.Add(this.txtNextRunTime);
			this.gpExecutionDetails.Controls.Add(this.label11);
			this.gpExecutionDetails.Controls.Add(this.txtLastRuntime);
			this.gpExecutionDetails.Controls.Add(this.label10);
			this.gpExecutionDetails.Controls.Add(this.txtLastCount);
			this.gpExecutionDetails.Controls.Add(this.label9);
			this.gpExecutionDetails.Location = new System.Drawing.Point(16, 344);
			this.gpExecutionDetails.Name = "gpExecutionDetails";
			this.gpExecutionDetails.Size = new System.Drawing.Size(600, 88);
			this.gpExecutionDetails.TabIndex = 100;
			this.gpExecutionDetails.TabStop = false;
			this.gpExecutionDetails.Text = "Execution Details ";
			// 
			// txtNextRunTime
			// 
			this.txtNextRunTime.Location = new System.Drawing.Point(424, 16);
			this.txtNextRunTime.MaxLength = 10;
			this.txtNextRunTime.Name = "txtNextRunTime";
			this.txtNextRunTime.ReadOnly = true;
			this.txtNextRunTime.Size = new System.Drawing.Size(120, 20);
			this.txtNextRunTime.TabIndex = 12;
			this.txtNextRunTime.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(280, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(112, 24);
			this.label11.TabIndex = 11;
			this.label11.Text = "Next Run time";
			// 
			// txtLastRuntime
			// 
			this.txtLastRuntime.Location = new System.Drawing.Point(144, 48);
			this.txtLastRuntime.MaxLength = 10;
			this.txtLastRuntime.Name = "txtLastRuntime";
			this.txtLastRuntime.ReadOnly = true;
			this.txtLastRuntime.Size = new System.Drawing.Size(120, 20);
			this.txtLastRuntime.TabIndex = 10;
			this.txtLastRuntime.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 48);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 24);
			this.label10.TabIndex = 9;
			this.label10.Text = "Last Run time";
			// 
			// txtLastCount
			// 
			this.txtLastCount.Location = new System.Drawing.Point(144, 16);
			this.txtLastCount.MaxLength = 10;
			this.txtLastCount.Name = "txtLastCount";
			this.txtLastCount.ReadOnly = true;
			this.txtLastCount.Size = new System.Drawing.Size(88, 20);
			this.txtLastCount.TabIndex = 8;
			this.txtLastCount.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(112, 24);
			this.label9.TabIndex = 7;
			this.label9.Text = "Last Count";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(16, 440);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 20);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// gpSelectFault
			// 
			this.gpSelectFault.Controls.Add(this.btnNewFault);
			this.gpSelectFault.Controls.Add(this.btnFaultSelect);
			this.gpSelectFault.Controls.Add(this.label15);
			this.gpSelectFault.Controls.Add(this.cmbFaultIDs);
			this.gpSelectFault.Location = new System.Drawing.Point(16, 64);
			this.gpSelectFault.Name = "gpSelectFault";
			this.gpSelectFault.Size = new System.Drawing.Size(600, 48);
			this.gpSelectFault.TabIndex = 1;
			this.gpSelectFault.TabStop = false;
			// 
			// btnNewFault
			// 
			this.btnNewFault.Location = new System.Drawing.Point(464, 16);
			this.btnNewFault.Name = "btnNewFault";
			this.btnNewFault.Size = new System.Drawing.Size(128, 20);
			this.btnNewFault.TabIndex = 2;
			this.btnNewFault.Text = "New Alert";
			this.btnNewFault.Click += new System.EventHandler(this.btnNewFault_Click);
			// 
			// btnFaultSelect
			// 
			this.btnFaultSelect.Location = new System.Drawing.Point(320, 16);
			this.btnFaultSelect.Name = "btnFaultSelect";
			this.btnFaultSelect.Size = new System.Drawing.Size(128, 20);
			this.btnFaultSelect.TabIndex = 1;
			this.btnFaultSelect.Text = "Select";
			this.btnFaultSelect.Click += new System.EventHandler(this.btnRepSelect_Click);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(24, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(120, 18);
			this.label15.TabIndex = 1;
			this.label15.Text = "Select Fault/Alert";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmbFaultIDs
			// 
			this.cmbFaultIDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFaultIDs.Location = new System.Drawing.Point(144, 16);
			this.cmbFaultIDs.Name = "cmbFaultIDs";
			this.cmbFaultIDs.Size = new System.Drawing.Size(144, 21);
			this.cmbFaultIDs.TabIndex = 0;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(344, 440);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 20);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(488, 440);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(128, 20);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(200, 440);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(128, 20);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// ManageAlerts
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(624, 488);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gpFltHeader);
			this.Controls.Add(this.gpService);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.gpSelectFault);
			this.Controls.Add(this.gpExecutionDetails);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ManageAlerts";
			this.Text = "ManageAlerts";
			this.Load += new System.EventHandler(this.ManageAlerts_Load);
			this.gpService.ResumeLayout(false);
			this.gpFltHeader.ResumeLayout(false);
			this.gpExecutionDetails.ResumeLayout(false);
			this.gpSelectFault.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void PopulatehalfHourIntervals(ComboBox CmbTime)
		{
			CmbTime.Items.Clear();

			for (int hourCounter=0; hourCounter<24; hourCounter++)
			{
				CmbTime.Items.Add(hourCounter.ToString("00") +":"+"00");
				CmbTime.Items.Add(hourCounter.ToString("00") +":"+"30");

			
			}
		
		}

		private void PopulateServices(ComboBox CmbServices)
		{
			CmbServices.Items.Clear();
			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				CmbServices.Items.Add("TCHQ");
                CmbServices.Items.Add("WSRQ");
				CmbServices.Items.Add("WSST");
                CmbServices.Items.Add("WSRE");
			}
			else
			{
				CmbServices.Items.Add("WSST");
                CmbServices.Items.Add("WSRE");
				CmbServices.Items.Add("WSCscc");
			
			}
			CmbServices.SelectedIndex=0;
		
		}
		private void ManageAlerts_Load(object sender, System.EventArgs e)
		{
			#region setup controls
			
			PopulateServices(cmbServices);
			

			SetNoFaultSelected();
			btnNewFault.Enabled=true;
			btnFaultSelect.Enabled=false;
			cmbFaultIDs.Enabled=false;
			gpExecutionDetails.TabStop=false;

		

			#endregion

		}

		private void SetNoFaultSelected()
		{
			txtFaultType.Text="";
			txtFaultType.ReadOnly=true;
			txtFaultSQL.Text="";
			
			txtFaultDescription.Text="";
			txtFaultNoAlertDelay.Text="";
			txtAlertDelay.Text="";
				
			txtNoFaultDelay.Text="";
			txtLastRuntime.Text="";
			txtNextRunTime.Text="";
			txtThreshHoldQuantity.Text="";
				
			txtLastCount.Text="";
			
			
			gpFltHeader.Enabled=false;
			gpExecutionDetails.Enabled=false;
			btnUpdate.Enabled=false;
			btnDelete.Enabled=false;
			btnAdd.Enabled=false;
		}
		

		private void btnSvcSelect_Click(object sender, System.EventArgs e)
		{
			CommonDBHelper dbHelper;
			
			#region SetControls
			SetNoFaultSelected();
			btnFaultSelect.Enabled=false;
			cmbFaultIDs.Enabled=false;
			

			#endregion



			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
                else if (cmbServices.Text == "WSRQ")
                {
                    sConnectString = frmMain.HTConnectionStrings["WSRQDB"].ToString();
                }
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["ExternalDB"].ToString();

                }
					 
			}
			else
			{
				if (cmbServices.Text=="WSCscc")
				{
					sConnectString=frmMain.HTConnectionStrings["CopyToDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["InternalDB"].ToString();
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["InternalDB"].ToString();
                }
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion

            string sSelectFaultSQL = "SELECT FAULT_TYPE, ALERT_DESCRIPTION FROM dbo.ALERT_TRACK WHERE SERVICE_ID=@SERVICE_ID ORDER BY FAULT_TYPE";

		;
			SqlCommand sSQLSearchCmd = new SqlCommand();

			sSQLSearchCmd.CommandText=sSelectFaultSQL;
			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sSQLSearchCmd.Parameters.Add(sSvcParam);

			cmbFaultIDs.Items.Clear();

			IDataReader sdr=null;
			cmbFaultIDs.Items.Clear();
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				while(sdr.Read())
				{
					cmbFaultIDs.Items.Add(new FaultDescriptionListItem(sdr.GetInt32(0),sdr.GetString(1)));
				}
				
				if (cmbFaultIDs.Items.Count!=0) 
				{
					
					cmbFaultIDs.Enabled=true;
					cmbFaultIDs.SelectedIndex=0;
					btnFaultSelect.Enabled=true;
				}
				else //no faults matching criteria
				{
					gpFltHeader.Enabled=false;
					gpExecutionDetails.Enabled=false;
					gpSelectFault.Enabled=true;
					cmbFaultIDs.Enabled=false;
					btnFaultSelect.Enabled=false;


					MessageBox.Show("No Faults/alerts configured for this service. Click \"New Alert\" to add new alerts.","Admin Client");
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
				dbHelper.Close();
			}
			SetNoFaultSelected();




		}

		private void btnRepSelect_Click(object sender, System.EventArgs e)
		{
			SetNoFaultSelected();

			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
                else if (cmbServices.Text == "WSRQ")
                {
                    sConnectString = frmMain.HTConnectionStrings["WSRQDB"].ToString();
                }
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["ExternalDB"].ToString();

                }

					 
			}
			else
			{
				if (cmbServices.Text=="WSCscc")
				{
					sConnectString=frmMain.HTConnectionStrings["CopyToDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["InternalDB"].ToString();
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["InternalDB"].ToString();
                }
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion


			#region Connect to DB and getData



            string sFaultSelectSQL = "SELECT [THRESHOLD_QUANTITY]      ,[SQL_QUERY]       ,[NEXT_TIME_TO_RUN]      ,[LAST_COUNT]      ,[LAST_RUN_TIME]       ,[NO_FAULT_DELAY]      ,[FAULTS_NO_ALERT_DELAY]      ,[ALERT_DELAY]       ,[ALERT_DESCRIPTION]      ,[LAST_UPDATE_USERID]      ,[LAST_UPDATE_TIME]   FROM dbo.[ALERT_TRACK]" + 
				" WHERE SERVICE_ID = @SERVICE_ID and FAULT_TYPE = @FAULT_TYPE";
			

			

			SqlCommand sSQLSearchCmd = new SqlCommand(sFaultSelectSQL);
			

			#region Set params


			SqlParameter sFaultTypeParam = new SqlParameter("@FAULT_TYPE",SqlDbType.Int);
			sFaultTypeParam.Value= ((FaultDescriptionListItem)(cmbFaultIDs.SelectedItem)).FaultID;
			sSQLSearchCmd.Parameters.Add(sFaultTypeParam);

			SqlParameter sServiceIDParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sServiceIDParam.Value= cmbServices.Text;
			sSQLSearchCmd.Parameters.Add(sServiceIDParam);

			

			#endregion

			#region DataReader Master

			IDataReader sdr=null;
		

			
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleRow);
				if(sdr.Read()==true)
				{
					txtThreshHoldQuantity.Text=sdr.GetInt32(0).ToString();
					txtFaultSQL.Text = sdr.GetString(1);
					
					txtNextRunTime.Text= sdr.IsDBNull(2)?"":  sdr.GetDateTime(2).ToString();
					txtLastCount.Text= sdr.IsDBNull(3)?"":sdr.GetInt32(3).ToString();
					txtLastRuntime.Text=sdr.IsDBNull(4)?"":sdr.GetDateTime(4).ToString();
					
					txtNoFaultDelay.Text= sdr.GetInt32(5).ToString();
					txtFaultNoAlertDelay.Text= sdr.GetInt32(6).ToString();
					txtAlertDelay.Text= sdr.GetInt32(7).ToString();
					txtFaultDescription.Text=sdr.GetString(8);
					
				}	
				else
				{
					
					MessageBox.Show("No Fault matching criteria","Admin Client");
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
				dbHelper.Close();
			}
			#endregion


		

			#endregion


			#region setupControls
			txtFaultType.Text = ((FaultDescriptionListItem)(cmbFaultIDs.SelectedItem)).FaultID.ToString();
			gpFltHeader.Enabled=true;
			gpExecutionDetails.Enabled=true;
			btnDelete.Enabled=true;
			btnUpdate.Enabled=true;
			btnAdd.Enabled=false;

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

			
			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
                else if (cmbServices.Text == "WSRQ")
                {
                    sConnectString = frmMain.HTConnectionStrings["WSRQDB"].ToString();
                }
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["ExternalDB"].ToString();

                }
					 
			}
			else
			{
				if (cmbServices.Text=="WSCscc")
				{
					sConnectString=frmMain.HTConnectionStrings["CopyToDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["InternalDB"].ToString();
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["InternalDB"].ToString();
                }
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion

			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction();
				DeleteFaultAlert(dbHelper,((FaultDescriptionListItem) cmbFaultIDs.SelectedItem).FaultID);
			
				dbHelper.CommitTransaction();
				MessageBox.Show("Alert deleted successfully","Admin Client");
				btnSvcSelect_Click(null,null);//simulate click to refresh
			}
			catch (SqlException exSQL)
			{
				dbHelper.RollbackTransaction();
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				
				dbHelper.Close();
			}

		
		}

		private void DeleteFaultAlert(CommonDBHelper dbHelper, int ReportID)
		{
		

			#region Connect to DB and getData



            string sReportDeleteSQL = "DELETE dbo.ALERT_TRACK " + 
				" WHERE FAULT_TYPE = @FAULT_TYPE AND SERVICE_ID=@SERVICE_ID";
			

			SqlCommand sSQLDelCmd = new SqlCommand(sReportDeleteSQL);


			#region Set params


			SqlParameter sFaultIDParam = new SqlParameter("@FAULT_TYPE",SqlDbType.Int);
			sFaultIDParam.Value= ((FaultDescriptionListItem)(cmbFaultIDs.SelectedItem)).FaultID;
			sSQLDelCmd.Parameters.Add(sFaultIDParam);

			
			SqlParameter sSvcParam = new SqlParameter();
			sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sSQLDelCmd.Parameters.Add(sSvcParam);

			
			#endregion

			#region Delete
		

			
			
				//transaction management in caller
				dbHelper.ExecuteNonQuery( sSQLDelCmd);
			
			
			#endregion

			#endregion


			

		
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			#region Validate
			
			txtFaultType.Text=txtFaultType.Text.Trim();
			txtFaultSQL.Text=txtFaultSQL.Text.Trim();
			txtFaultDescription.Text=txtFaultDescription.Text.Trim();
			txtThreshHoldQuantity.Text=txtThreshHoldQuantity.Text.Trim();
			txtFaultNoAlertDelay.Text=txtFaultNoAlertDelay.Text.Trim();
			txtAlertDelay.Text=txtAlertDelay.Text.Trim();
			txtNoFaultDelay.Text=txtNoFaultDelay.Text.Trim();

			if (txtFaultType.Text=="")
			{
				errorProvider1.SetError(txtFaultType,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtFaultType.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtFaultType,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtFaultType,"");

			if (txtFaultSQL.Text=="")
			{
				errorProvider1.SetError(txtFaultSQL,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtFaultSQL,"");

			if (txtFaultDescription.Text=="")
			{
				errorProvider1.SetError(txtFaultDescription,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtFaultDescription,"");

			if (txtThreshHoldQuantity.Text=="")
			{
				errorProvider1.SetError(txtThreshHoldQuantity,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtThreshHoldQuantity.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtThreshHoldQuantity,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtThreshHoldQuantity,"");

			if (txtFaultNoAlertDelay.Text=="")
			{
				errorProvider1.SetError(txtFaultNoAlertDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtFaultNoAlertDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtFaultNoAlertDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtFaultNoAlertDelay,"");


			if (txtAlertDelay.Text=="")
			{
				errorProvider1.SetError(txtAlertDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtAlertDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtAlertDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtAlertDelay,"");

			if (txtNoFaultDelay.Text=="")
			{
				errorProvider1.SetError(txtNoFaultDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtNoFaultDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtNoFaultDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtNoFaultDelay,"");


			#endregion

		
			#region connect to DB and update

			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
                else if (cmbServices.Text == "WSRQ")
                {
                    sConnectString = frmMain.HTConnectionStrings["WSRQDB"].ToString();
                }
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["ExternalDB"].ToString();

                }
					 
			}
			else
			{
				if (cmbServices.Text=="WSCscc")
				{
					sConnectString=frmMain.HTConnectionStrings["CopyToDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["InternalDB"].ToString();
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["InternalDB"].ToString();
                }
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion


            string sUpdateFaultMainSQL = "UPDATE dbo.[ALERT_TRACK] SET [THRESHOLD_QUANTITY]=@THRESHOLD_QUANTITY,  [SQL_QUERY]=@SQL_QUERY    ,[NO_FAULT_DELAY]=@NO_FAULT_DELAY ,[FAULTS_NO_ALERT_DELAY]=@FAULTS_NO_ALERT_DELAY ,[ALERT_DELAY]=@ALERT_DELAY  ,[ALERT_DESCRIPTION]=@ALERT_DESCRIPTION ,[LAST_UPDATE_USERID]=@LAST_UPDATE_USERID ,[LAST_UPDATE_TIME]=getdate() " +
				" WHERE SERVICE_ID=@SERVICE_ID AND FAULT_TYPE=@FAULT_TYPE";

			
			SqlCommand  sUpdateFaultCmd = new SqlCommand(sUpdateFaultMainSQL);
			
			//generate max schid for no of days x no of times

			
			#region set Params

			SqlParameter sParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sParam.Value=cmbServices.Text;
			sUpdateFaultCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@FAULT_TYPE",SqlDbType.Int);
			sParam.Value= int.Parse (txtFaultType.Text);
			sUpdateFaultCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@THRESHOLD_QUANTITY",SqlDbType.Int);
			sParam.Value=int.Parse (txtThreshHoldQuantity.Text);
			sUpdateFaultCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@SQL_QUERY",SqlDbType.NVarChar);
			sParam.Value=txtFaultSQL.Text;
			sUpdateFaultCmd.Parameters.Add(sParam);
			
			sParam = new SqlParameter("@NO_FAULT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtNoFaultDelay.Text);
			sUpdateFaultCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@FAULTS_NO_ALERT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtFaultNoAlertDelay.Text);
			sUpdateFaultCmd.Parameters.Add(sParam);


			sParam = new SqlParameter("@ALERT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtAlertDelay.Text);
			sUpdateFaultCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@ALERT_DESCRIPTION",SqlDbType.NVarChar);
			sParam.Value=txtFaultDescription.Text;
			sUpdateFaultCmd.Parameters.Add(sParam);


			
			sParam= new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sParam.Value=frmMain.UserID;
			sUpdateFaultCmd.Parameters.Add(sParam);


			// for SQL Insert
	
			
			#endregion




			try
			{
				dbHelper.ConnectToDB();
				
				#region update master
				int nRowsAffected=
				
				nRowsAffected= dbHelper.ExecuteNonQuery(sUpdateFaultCmd);

				if (nRowsAffected==0)
				{
					MessageBox.Show("No Alert found to update. Refresh the screen by selecting a  Service again.","Admin client");
				}
				else
			MessageBox.Show("Alert Updated successfully.","Admin client");
				#endregion 

			
				
				
				
				//reset controls
				SetNoFaultSelected();
			}
			catch(SqlException exx)
			{
					MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message + "\r\n SQL Error No:" + exx.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
			
				
				
			}
			finally
			{
				dbHelper.Close();
			}


			#endregion
		
		

			
		}

		private void btnNewFault_Click(object sender, System.EventArgs e)
		{
			SetNoFaultSelected();
			txtFaultType.ReadOnly=false;
			txtFaultType.Focus();
			btnDelete.Enabled=false;
			btnUpdate.Enabled=false;
			btnAdd.Enabled=true;
			gpFltHeader.Enabled=true;
			gpExecutionDetails.Enabled=false;
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			
			#region Validate
			
			txtFaultType.Text=txtFaultType.Text.Trim();
			txtFaultSQL.Text=txtFaultSQL.Text.Trim();
			txtFaultDescription.Text=txtFaultDescription.Text.Trim();
			txtThreshHoldQuantity.Text=txtThreshHoldQuantity.Text.Trim();
			txtFaultNoAlertDelay.Text=txtFaultNoAlertDelay.Text.Trim();
			txtAlertDelay.Text=txtAlertDelay.Text.Trim();
			txtNoFaultDelay.Text=txtNoFaultDelay.Text.Trim();

			if (txtFaultType.Text=="")
			{
				errorProvider1.SetError(txtFaultType,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtFaultType.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtFaultType,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtFaultType,"");

			if (txtFaultSQL.Text=="")
			{
				errorProvider1.SetError(txtFaultSQL,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtFaultSQL,"");

			if (txtFaultDescription.Text=="")
			{
				errorProvider1.SetError(txtFaultDescription,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtFaultDescription,"");

			if (txtThreshHoldQuantity.Text=="")
			{
				errorProvider1.SetError(txtThreshHoldQuantity,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtThreshHoldQuantity.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtThreshHoldQuantity,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtThreshHoldQuantity,"");

			if (txtFaultNoAlertDelay.Text=="")
			{
				errorProvider1.SetError(txtFaultNoAlertDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtFaultNoAlertDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtFaultNoAlertDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtFaultNoAlertDelay,"");


			if (txtAlertDelay.Text=="")
			{
				errorProvider1.SetError(txtAlertDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtAlertDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtAlertDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtAlertDelay,"");

			if (txtNoFaultDelay.Text=="")
			{
				errorProvider1.SetError(txtNoFaultDelay,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtNoFaultDelay.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
				}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtNoFaultDelay,"Provide a valid positive integer");
				return;
			}
			errorProvider1.SetError(txtNoFaultDelay,"");


			#endregion

		
			#region connect to DB and update

			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
                else if (cmbServices.Text == "WSRQ")
                {
                    sConnectString = frmMain.HTConnectionStrings["WSRQDB"].ToString();
                }
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["ExternalDB"].ToString();

                }

					 
			}
			else
			{
				if (cmbServices.Text=="WSCscc")
				{
					sConnectString=frmMain.HTConnectionStrings["CopyToDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["InternalDB"].ToString();
				}
                else if (cmbServices.Text == "WSRE")
                {
                    sConnectString = frmMain.HTConnectionStrings["InternalDB"].ToString();
                }
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion


            string sInsertFaultMainSQL = "INSERT INTO dbo.[ALERT_TRACK] ([SERVICE_ID]  ,[FAULT_TYPE]  ,[THRESHOLD_QUANTITY]  ,[SQL_QUERY]  ,[NEXT_TIME_TO_RUN] ,[LAST_COUNT] ,[LAST_RUN_TIME]  ,[NO_FAULT_DELAY] ,[FAULTS_NO_ALERT_DELAY] ,[ALERT_DELAY]  ,[ALERT_DESCRIPTION] ,[LAST_UPDATE_USERID] ,[LAST_UPDATE_TIME]) " + 
" VALUES  (@SERVICE_ID  ,@FAULT_TYPE  ,@THRESHOLD_QUANTITY  ,@SQL_QUERY  ,null  ,null  ,null  ,@NO_FAULT_DELAY  ,@FAULTS_NO_ALERT_DELAY  ,@ALERT_DELAY  ,@ALERT_DESCRIPTION  ,@LAST_UPDATE_USERID  ,getdate())";

			
			SqlCommand  sInsertFaultMainCmd = new SqlCommand(sInsertFaultMainSQL);
			
			//generate max schid for no of days x no of times

			
			#region set Params

			SqlParameter sParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sParam.Value=cmbServices.Text;
			sInsertFaultMainCmd.Parameters.Add(sParam);

			 sParam = new SqlParameter("@FAULT_TYPE",SqlDbType.Int);
			sParam.Value= int.Parse (txtFaultType.Text);
			sInsertFaultMainCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@THRESHOLD_QUANTITY",SqlDbType.Int);
			sParam.Value=int.Parse (txtThreshHoldQuantity.Text);
			sInsertFaultMainCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@SQL_QUERY",SqlDbType.NVarChar);
			sParam.Value=txtFaultSQL.Text;
			sInsertFaultMainCmd.Parameters.Add(sParam);
			
			sParam = new SqlParameter("@NO_FAULT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtNoFaultDelay.Text);
			sInsertFaultMainCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@FAULTS_NO_ALERT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtFaultNoAlertDelay.Text);
			sInsertFaultMainCmd.Parameters.Add(sParam);


			sParam = new SqlParameter("@ALERT_DELAY",SqlDbType.Int);
			sParam.Value=int.Parse (txtAlertDelay.Text);
			sInsertFaultMainCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@ALERT_DESCRIPTION",SqlDbType.NVarChar);
			sParam.Value=txtFaultDescription.Text;
			sInsertFaultMainCmd.Parameters.Add(sParam);


			
			sParam= new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sParam.Value=frmMain.UserID;
			sInsertFaultMainCmd.Parameters.Add(sParam);


			// for SQL Insert
	
			
			#endregion




			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction();
				#region insert master
				
				dbHelper.ExecuteNonQuery(sInsertFaultMainCmd);
				#endregion 

			
				dbHelper.CommitTransaction();
				MessageBox.Show("Alert added successfully.","Admin client");
				
				//reset controls
				txtFaultType.Text="";
				txtFaultType.Focus();
			}
			catch(SqlException exx)
			{
			
				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("Fault already exists. Change Fault Type/Service", "Admin Client");

					//prepare for next user
					txtFaultType.SelectAll();
					
					txtFaultType.Focus();
					return;
				}
				else
				{
			
					MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message + "\r\n SQL Error No:" + exx.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
			
				
				}
			}
			finally
			{
				dbHelper.Close();
			}


			#endregion
		
		

		}



	}

	internal class FaultDescriptionListItem
	{
		public int FaultID;
		public string FaultDescription;

		public FaultDescriptionListItem(int FaultIDParam, string FaultDescriptionParam)
		{
			FaultID= FaultIDParam;
			FaultDescription= FaultDescriptionParam.Trim();
		}

		public override string ToString()
		{
			return FaultID.ToString() + "-" + FaultDescription;
		}

	
	}
}
