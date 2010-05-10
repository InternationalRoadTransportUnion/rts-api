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
	/// Summary description for ModifyPeriodicReport.
	/// </summary>
	public class ModifyPeriodicReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpService;
		private System.Windows.Forms.ComboBox cmbServices;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox gpRepHeader;
		private System.Windows.Forms.TextBox txtReportID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtReportSQL;
		private System.Windows.Forms.TextBox txtReportDescription;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkDay2;
		private System.Windows.Forms.CheckBox chkDay3;
		private System.Windows.Forms.CheckBox chkDay1;
		private System.Windows.Forms.CheckBox chkDay5;
		private System.Windows.Forms.CheckBox chkDay4;
		private System.Windows.Forms.CheckBox chkDay6;
		private System.Windows.Forms.CheckBox chkDay7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ListBox lstSelectedTimes;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cmbAvailableTime;
		private System.Windows.Forms.Button btnAddTime;
		private System.Windows.Forms.Button btnRemoveTime;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btnSvcSelect;
		private System.Windows.Forms.GroupBox gpSelectReport;
		private System.Windows.Forms.ComboBox cmbReportIDs;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btnRepSelect;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.GroupBox gpSchedule;
		private System.Windows.Forms.Button btnDelete;


		private CheckBox[] m_aDayBoxes;

		DateTime m_dtLastExectime; //current report last exec time
		

		public ModifyPeriodicReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_aDayBoxes= new CheckBox[7];
			m_aDayBoxes[0] = chkDay1;
			m_aDayBoxes[1] = chkDay2;
			m_aDayBoxes[2] = chkDay3;
			m_aDayBoxes[3] = chkDay4;
			m_aDayBoxes[4] = chkDay5;
			m_aDayBoxes[5] = chkDay6;
            m_aDayBoxes[6] = chkDay7;
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
			this.gpRepHeader = new System.Windows.Forms.GroupBox();
			this.txtReportDescription = new System.Windows.Forms.TextBox();
			this.txtReportSQL = new System.Windows.Forms.TextBox();
			this.txtReportID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gpSchedule = new System.Windows.Forms.GroupBox();
			this.btnAddTime = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.cmbAvailableTime = new System.Windows.Forms.ComboBox();
			this.lstSelectedTimes = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.chkDay2 = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkDay3 = new System.Windows.Forms.CheckBox();
			this.chkDay1 = new System.Windows.Forms.CheckBox();
			this.chkDay5 = new System.Windows.Forms.CheckBox();
			this.chkDay4 = new System.Windows.Forms.CheckBox();
			this.chkDay6 = new System.Windows.Forms.CheckBox();
			this.chkDay7 = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.btnRemoveTime = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gpSelectReport = new System.Windows.Forms.GroupBox();
			this.btnRepSelect = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.cmbReportIDs = new System.Windows.Forms.ComboBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.gpService.SuspendLayout();
			this.gpRepHeader.SuspendLayout();
			this.gpSchedule.SuspendLayout();
			this.gpSelectReport.SuspendLayout();
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
			this.cmbServices.Location = new System.Drawing.Point(160, 16);
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
			// gpRepHeader
			// 
			this.gpRepHeader.Controls.Add(this.txtReportDescription);
			this.gpRepHeader.Controls.Add(this.txtReportSQL);
			this.gpRepHeader.Controls.Add(this.txtReportID);
			this.gpRepHeader.Controls.Add(this.label3);
			this.gpRepHeader.Controls.Add(this.label2);
			this.gpRepHeader.Controls.Add(this.label1);
			this.gpRepHeader.Location = new System.Drawing.Point(16, 128);
			this.gpRepHeader.Name = "gpRepHeader";
			this.gpRepHeader.Size = new System.Drawing.Size(600, 224);
			this.gpRepHeader.TabIndex = 2;
			this.gpRepHeader.TabStop = false;
			// 
			// txtReportDescription
			// 
			this.txtReportDescription.Location = new System.Drawing.Point(160, 160);
			this.txtReportDescription.MaxLength = 128;
			this.txtReportDescription.Multiline = true;
			this.txtReportDescription.Name = "txtReportDescription";
			this.txtReportDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtReportDescription.Size = new System.Drawing.Size(408, 48);
			this.txtReportDescription.TabIndex = 2;
			this.txtReportDescription.Text = "";
			// 
			// txtReportSQL
			// 
			this.txtReportSQL.Location = new System.Drawing.Point(160, 48);
			this.txtReportSQL.MaxLength = 3000;
			this.txtReportSQL.Multiline = true;
			this.txtReportSQL.Name = "txtReportSQL";
			this.txtReportSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtReportSQL.Size = new System.Drawing.Size(408, 96);
			this.txtReportSQL.TabIndex = 1;
			this.txtReportSQL.Text = "";
			// 
			// txtReportID
			// 
			this.txtReportID.Location = new System.Drawing.Point(160, 16);
			this.txtReportID.MaxLength = 50;
			this.txtReportID.Name = "txtReportID";
			this.txtReportID.ReadOnly = true;
			this.txtReportID.Size = new System.Drawing.Size(144, 20);
			this.txtReportID.TabIndex = 0;
			this.txtReportID.Text = "";
			this.toolTip1.SetToolTip(this.txtReportID, "Provide a positive integer.");
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 160);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 31);
			this.label3.TabIndex = 4;
			this.label3.Text = "Report Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 31);
			this.label2.TabIndex = 2;
			this.label2.Text = "Report SQL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Report ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpSchedule
			// 
			this.gpSchedule.Controls.Add(this.btnAddTime);
			this.gpSchedule.Controls.Add(this.label14);
			this.gpSchedule.Controls.Add(this.cmbAvailableTime);
			this.gpSchedule.Controls.Add(this.lstSelectedTimes);
			this.gpSchedule.Controls.Add(this.label13);
			this.gpSchedule.Controls.Add(this.label5);
			this.gpSchedule.Controls.Add(this.chkDay2);
			this.gpSchedule.Controls.Add(this.label4);
			this.gpSchedule.Controls.Add(this.chkDay3);
			this.gpSchedule.Controls.Add(this.chkDay1);
			this.gpSchedule.Controls.Add(this.chkDay5);
			this.gpSchedule.Controls.Add(this.chkDay4);
			this.gpSchedule.Controls.Add(this.chkDay6);
			this.gpSchedule.Controls.Add(this.chkDay7);
			this.gpSchedule.Controls.Add(this.label7);
			this.gpSchedule.Controls.Add(this.label8);
			this.gpSchedule.Controls.Add(this.label9);
			this.gpSchedule.Controls.Add(this.label10);
			this.gpSchedule.Controls.Add(this.label11);
			this.gpSchedule.Controls.Add(this.label12);
			this.gpSchedule.Controls.Add(this.btnRemoveTime);
			this.gpSchedule.Location = new System.Drawing.Point(16, 360);
			this.gpSchedule.Name = "gpSchedule";
			this.gpSchedule.Size = new System.Drawing.Size(600, 200);
			this.gpSchedule.TabIndex = 3;
			this.gpSchedule.TabStop = false;
			// 
			// btnAddTime
			// 
			this.btnAddTime.Location = new System.Drawing.Point(248, 96);
			this.btnAddTime.Name = "btnAddTime";
			this.btnAddTime.Size = new System.Drawing.Size(128, 20);
			this.btnAddTime.TabIndex = 8;
			this.btnAddTime.Text = "Add time >";
			this.btnAddTime.Click += new System.EventHandler(this.btnAddTime_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(16, 96);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(88, 24);
			this.label14.TabIndex = 6;
			this.label14.Text = "Available time";
			// 
			// cmbAvailableTime
			// 
			this.cmbAvailableTime.Location = new System.Drawing.Point(112, 96);
			this.cmbAvailableTime.Name = "cmbAvailableTime";
			this.cmbAvailableTime.Size = new System.Drawing.Size(120, 21);
			this.cmbAvailableTime.TabIndex = 7;
			// 
			// lstSelectedTimes
			// 
			this.lstSelectedTimes.Location = new System.Drawing.Point(376, 96);
			this.lstSelectedTimes.Name = "lstSelectedTimes";
			this.lstSelectedTimes.Size = new System.Drawing.Size(104, 95);
			this.lstSelectedTimes.TabIndex = 9;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(376, 72);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(160, 24);
			this.label13.TabIndex = 3;
			this.label13.Text = "Selected Time (24HH:mm)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(136, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 24);
			this.label5.TabIndex = 2;
			this.label5.Text = "Sun";
			// 
			// chkDay2
			// 
			this.chkDay2.Location = new System.Drawing.Point(184, 40);
			this.chkDay2.Name = "chkDay2";
			this.chkDay2.Size = new System.Drawing.Size(24, 24);
			this.chkDay2.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Day of the Week";
			// 
			// chkDay3
			// 
			this.chkDay3.Location = new System.Drawing.Point(224, 40);
			this.chkDay3.Name = "chkDay3";
			this.chkDay3.Size = new System.Drawing.Size(24, 24);
			this.chkDay3.TabIndex = 2;
			// 
			// chkDay1
			// 
			this.chkDay1.Location = new System.Drawing.Point(144, 40);
			this.chkDay1.Name = "chkDay1";
			this.chkDay1.Size = new System.Drawing.Size(24, 24);
			this.chkDay1.TabIndex = 0;
			this.chkDay1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// chkDay5
			// 
			this.chkDay5.Location = new System.Drawing.Point(304, 40);
			this.chkDay5.Name = "chkDay5";
			this.chkDay5.Size = new System.Drawing.Size(24, 24);
			this.chkDay5.TabIndex = 4;
			// 
			// chkDay4
			// 
			this.chkDay4.Location = new System.Drawing.Point(264, 40);
			this.chkDay4.Name = "chkDay4";
			this.chkDay4.Size = new System.Drawing.Size(24, 24);
			this.chkDay4.TabIndex = 3;
			// 
			// chkDay6
			// 
			this.chkDay6.Location = new System.Drawing.Point(344, 40);
			this.chkDay6.Name = "chkDay6";
			this.chkDay6.Size = new System.Drawing.Size(24, 24);
			this.chkDay6.TabIndex = 5;
			// 
			// chkDay7
			// 
			this.chkDay7.Location = new System.Drawing.Point(384, 40);
			this.chkDay7.Name = "chkDay7";
			this.chkDay7.Size = new System.Drawing.Size(24, 24);
			this.chkDay7.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(176, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 24);
			this.label7.TabIndex = 2;
			this.label7.Text = "Mon";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(216, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(32, 24);
			this.label8.TabIndex = 2;
			this.label8.Text = "Tue";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(256, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 24);
			this.label9.TabIndex = 2;
			this.label9.Text = "Wed";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(296, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(32, 24);
			this.label10.TabIndex = 2;
			this.label10.Text = "Thu";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(336, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(32, 24);
			this.label11.TabIndex = 2;
			this.label11.Text = "Fri";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(376, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(32, 24);
			this.label12.TabIndex = 2;
			this.label12.Text = "Sat";
			// 
			// btnRemoveTime
			// 
			this.btnRemoveTime.Location = new System.Drawing.Point(248, 136);
			this.btnRemoveTime.Name = "btnRemoveTime";
			this.btnRemoveTime.Size = new System.Drawing.Size(128, 20);
			this.btnRemoveTime.TabIndex = 10;
			this.btnRemoveTime.Text = "Remove time <";
			this.btnRemoveTime.Click += new System.EventHandler(this.btnRemoveTime_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(16, 576);
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
			// gpSelectReport
			// 
			this.gpSelectReport.Controls.Add(this.btnRepSelect);
			this.gpSelectReport.Controls.Add(this.label15);
			this.gpSelectReport.Controls.Add(this.cmbReportIDs);
			this.gpSelectReport.Location = new System.Drawing.Point(16, 72);
			this.gpSelectReport.Name = "gpSelectReport";
			this.gpSelectReport.Size = new System.Drawing.Size(600, 48);
			this.gpSelectReport.TabIndex = 1;
			this.gpSelectReport.TabStop = false;
			// 
			// btnRepSelect
			// 
			this.btnRepSelect.Location = new System.Drawing.Point(320, 16);
			this.btnRepSelect.Name = "btnRepSelect";
			this.btnRepSelect.Size = new System.Drawing.Size(128, 20);
			this.btnRepSelect.TabIndex = 1;
			this.btnRepSelect.Text = "Select";
			this.btnRepSelect.Click += new System.EventHandler(this.btnRepSelect_Click);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(24, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(120, 18);
			this.label15.TabIndex = 1;
			this.label15.Text = "Select Report ID";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmbReportIDs
			// 
			this.cmbReportIDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbReportIDs.Location = new System.Drawing.Point(160, 16);
			this.cmbReportIDs.Name = "cmbReportIDs";
			this.cmbReportIDs.Size = new System.Drawing.Size(144, 21);
			this.cmbReportIDs.TabIndex = 0;
			this.cmbReportIDs.SelectedIndexChanged += new System.EventHandler(this.cmbReportIDs_SelectedIndexChanged);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(344, 576);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(128, 20);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(488, 576);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(128, 20);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// ModifyPeriodicReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(624, 608);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.gpSelectReport);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gpSchedule);
			this.Controls.Add(this.gpRepHeader);
			this.Controls.Add(this.gpService);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ModifyPeriodicReport";
			this.Text = "ModifyPeriodicReport";
			this.Load += new System.EventHandler(this.ModifyPeriodicReport_Load);
			this.gpService.ResumeLayout(false);
			this.gpRepHeader.ResumeLayout(false);
			this.gpSchedule.ResumeLayout(false);
			this.gpSelectReport.ResumeLayout(false);
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
				CmbServices.Items.Add("WSST");
			}
			else
			{
				CmbServices.Items.Add("WSST");
				CmbServices.Items.Add("WSCscc");
			
			}
			CmbServices.SelectedIndex=0;
		
		}
		private void ModifyPeriodicReport_Load(object sender, System.EventArgs e)
		{
			#region setup controls
			chkDay1.Checked=false;
			chkDay2.Checked=false;
			chkDay3.Checked=false;
			chkDay4.Checked=false;
			chkDay5.Checked=false;
			chkDay6.Checked=false;
			chkDay7.Checked=false;
			PopulatehalfHourIntervals(cmbAvailableTime);
			PopulateServices(cmbServices);
			

			gpSelectReport.Enabled=false;
			SetNoReportSelected();

		

			#endregion

		}

		private void SetNoReportSelected()
		{
			txtReportID.Text="";
			txtReportSQL.Text="";
			
			txtReportDescription.Text="";
			lstSelectedTimes.Items.Clear();
			foreach (CheckBox chkDay in m_aDayBoxes)
			{ 
				chkDay.Checked=false;
			}
			
			gpRepHeader.Enabled=false;
			gpSchedule.Enabled=false;
			btnUpdate.Enabled=false;
			btnDelete.Enabled=false;
		}
		private void btnAddTime_Click(object sender, System.EventArgs e)
		{
			#region validate combo time

			Regex regexTimeMatch = new Regex(@"[012]\d:[012345]\d", 
				RegexOptions.Singleline);
			Match aMatch = regexTimeMatch.Match(cmbAvailableTime.Text);

			if (aMatch.Success)
			{
				if ( aMatch.Value!= cmbAvailableTime.Text)
				{
					MessageBox.Show("Enter time in format 24HH:mm e.g. 22:20");
					cmbAvailableTime.Text="";
					return;
				}
			}
			else
			{
				MessageBox.Show("Enter time in format 24HH:mm e.g. 22:20");
				cmbAvailableTime.Text="";
				return;
			}
			

				

			#endregion

			//Check if item exists
			if (lstSelectedTimes.FindStringExact(cmbAvailableTime.Text)<0)
			{

				AddSortedTime();
				
			}
		}

		private void AddSortedTime()
		{
			int nIndex=1;
			foreach (Object item in lstSelectedTimes.Items)
			{

				if (item.ToString().CompareTo(cmbAvailableTime.Text)>=0)
				{
					break;
				}		
				else
					nIndex++;
			}
			lstSelectedTimes.Items.Insert(nIndex-1,cmbAvailableTime.Text);
			
		}

	


		private void InsertReportToDB(CommonDBHelper dbHelper)
		{
			
			#region Validate
			
			txtReportID.Text=txtReportID.Text.Trim();
			txtReportSQL.Text=txtReportSQL.Text.Trim();
			txtReportDescription.Text=txtReportDescription.Text.Trim();

			if (txtReportID.Text=="")
			{
				errorProvider1.SetError(txtReportID,"Mandatory Field");
				return;
			}
			
			try
			{
				int nRepID;
				nRepID=int.Parse(txtReportID.Text);
				if (nRepID<1)
				{
					throw new ApplicationException("Positive integer required");
					}
			}
			catch (Exception exx)
			{
				errorProvider1.SetError(txtReportID,"Provide a valid positive integer");
				return;
			}
				errorProvider1.SetError(txtReportID,"");

			if (txtReportSQL.Text=="")
			{
				errorProvider1.SetError(txtReportSQL,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtReportSQL,"");

			if (txtReportDescription.Text=="")
			{
				errorProvider1.SetError(txtReportDescription,"Mandatory Field");
				return;
			}
			errorProvider1.SetError(txtReportDescription,"");

			bool bDayCheck=false;
			foreach (CheckBox chkDay in m_aDayBoxes)
			{
				if (chkDay.Checked==true)
				{	bDayCheck=true;
					break;
				}
			}
			if (bDayCheck==false)
			{
				errorProvider1.SetError(chkDay7,"Select atleast one day of the week");
				return;
			
			}
				errorProvider1.SetError(chkDay7,"");

			if (lstSelectedTimes.Items.Count==0)
			{
				errorProvider1.SetError(lstSelectedTimes,"Select atleast one time of day.");
				return;
			}
				errorProvider1.SetError(lstSelectedTimes,"");
			#endregion

		
			#region connect to DB and update

			

			string sInsertReportMainSQL= "INSERT INTO PERIODIC_REPORT (REPORT_ID, SERVICE_ID,LAST_EXECUTION_TIME,NEXT_SCHEDULE_ID, NEXT_EXECUTION_TIME, LAST_UPDATE_USERID,LAST_UPDATE_TIME) VALUES (@REPORT_ID, @SERVICE_ID,@LAST_EXECUTION_TIME,@NEXT_SCHEDULE_ID, @NEXT_EXECUTION_TIME, @LAST_UPDATE_USERID,getdate())";

			string sInsertReportSQLSQL = "INSERT INTO PERIODIC_REPORT_SQL (REPORT_ID, QUERY_SQL, QUERY_DESCRIPTION,LAST_UPDATE_USERID,LAST_UPDATE_TIME) VALUES (@REPORT_ID,@QUERY_SQL,@QUERY_DESCRIPTION,@LAST_UPDATE_USERID,getdate())";

			string sInsertReportScheduleSQL = "INSERT INTO PERIODIC_REPORT_SCHEDULE (REPORT_ID,SCHEDULE_ID,SCHEDULE_DAY,SCHEDULE_TIME,NEXT_SCHEDULE_ID,LAST_UPDATE_USERID,LAST_UPDATE_TIME) VALUES (@REPORT_ID,@SCHEDULE_ID,@SCHEDULE_DAY,@SCHEDULE_TIME,@NEXT_SCHEDULE_ID,@LAST_UPDATE_USERID,getdate())";

				SqlCommand  sInsertReportMainCmd = new SqlCommand(sInsertReportMainSQL);
			    SqlCommand sInsertReportSQLCmd= new SqlCommand(sInsertReportSQLSQL);
				SqlCommand sInsertReportScheduleCmd = new SqlCommand(sInsertReportScheduleSQL);

			//generate max schid for no of days x no of times

			int nDayCount=0;

			foreach (CheckBox chkDay in m_aDayBoxes)
			{
				if (chkDay.Checked==true)
				{
						nDayCount++;
					
				}
			}

			int nSchIDsMax= nDayCount* lstSelectedTimes.Items.Count;

			#region set Params

			
			SqlParameter sRepIDParam = new SqlParameter("@REPORT_ID",SqlDbType.Int);
			sRepIDParam.Value=txtReportID.Text;
			sInsertReportMainCmd.Parameters.Add(sRepIDParam);

			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sInsertReportMainCmd.Parameters.Add(sSvcParam);

			

			SqlParameter sLETimeParam = new SqlParameter("@LAST_EXECUTION_TIME",SqlDbType.DateTime);
			sLETimeParam.Value= m_dtLastExectime;
			sInsertReportMainCmd.Parameters.Add(sLETimeParam);

			SqlParameter sNextSchIDParam = new SqlParameter("@NEXT_SCHEDULE_ID",SqlDbType.Int);
			sNextSchIDParam.Value=1; //always 1
			sInsertReportMainCmd.Parameters.Add(sNextSchIDParam);

			SqlParameter sNETimeParam = new SqlParameter("@NEXT_EXECUTION_TIME",SqlDbType.DateTime);

			sInsertReportMainCmd.Parameters.Add(sNETimeParam);//set time while looping through times


			SqlParameter sLastUserParam = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam.Value=frmMain.UserID;
			sInsertReportMainCmd.Parameters.Add(sLastUserParam);


			// for SQL Insert
	
			SqlParameter sRepIDParam2 = new SqlParameter("@REPORT_ID",SqlDbType.Int);
			sRepIDParam2.Value=txtReportID.Text;
			sInsertReportSQLCmd.Parameters.Add(sRepIDParam2);

			SqlParameter sRepSQLParam = new SqlParameter("@QUERY_SQL",SqlDbType.NVarChar);
			sRepSQLParam.Value=txtReportSQL.Text;
			sInsertReportSQLCmd.Parameters.Add(sRepSQLParam);

			SqlParameter sRepDescParam = new SqlParameter("@QUERY_DESCRIPTION",SqlDbType.NVarChar);
			sRepDescParam.Value=txtReportDescription.Text;
			sInsertReportSQLCmd.Parameters.Add(sRepDescParam);

			SqlParameter sLastUserParam2 = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam2.Value=frmMain.UserID;
			sInsertReportSQLCmd.Parameters.Add(sLastUserParam2);

			//for schedule insert

			SqlParameter sRepIDParam3 = new SqlParameter("@REPORT_ID",SqlDbType.Int);
			sRepIDParam3.Value=txtReportID.Text;
			sInsertReportScheduleCmd.Parameters.Add(sRepIDParam3);

			SqlParameter sSchIDParam = new SqlParameter("@SCHEDULE_ID",SqlDbType.Int);
			//sSchIDParam.Value= set later in the loop
			sInsertReportScheduleCmd.Parameters.Add(sSchIDParam);

			SqlParameter sSchDayParam = new SqlParameter("@SCHEDULE_DAY",SqlDbType.Int);
			//sSchDayParam.Value= set later in the loop
			sInsertReportScheduleCmd.Parameters.Add(sSchDayParam);

			
			SqlParameter sSchTimeParam = new SqlParameter("@SCHEDULE_TIME",SqlDbType.NVarChar);
			//sSchTimeParam.Value= set later in the loop
			sInsertReportScheduleCmd.Parameters.Add(sSchTimeParam);

			SqlParameter sNextSchIDParam2 = new SqlParameter("@NEXT_SCHEDULE_ID",SqlDbType.Int);
			//sNextSchIDParam2.Value= set later in the loop
			sInsertReportScheduleCmd.Parameters.Add(sNextSchIDParam2);

			SqlParameter sLastUserParam3 = new SqlParameter("@LAST_UPDATE_USERID",SqlDbType.NVarChar);
			sLastUserParam3.Value=frmMain.UserID;
			sInsertReportScheduleCmd.Parameters.Add(sLastUserParam3);

			#endregion




			//transaction management in caller

				#region insert schedule
				int nSchIDCntr= 1;
				bool bNextSchIDToSetFound = false;
			    bool bFirstTimeToExecSet = false;

				int nNextSchIDToSet=1 ; //defaults to 1 
				DateTime dtFirstTimeToExec = DateTime.MinValue ;//run at next poll by default
				DateTime dtNextTimeToExec = DateTime.MinValue ;//run at next poll by default

				for (int nDayCntr = 1 ; nDayCntr<8 ; nDayCntr++)
				{
					if (m_aDayBoxes[nDayCntr-1].Checked==true)
					{
								//if day set loop through selected times and insert
								foreach (Object timeItem in lstSelectedTimes.Items)
						{


									#region first Time Set for next execution time from the UI
								
									if (bFirstTimeToExecSet==false)
									{
										dtFirstTimeToExec = CalculateNextTime(DateTime.Now, nDayCntr, (string)timeItem);

										bFirstTimeToExecSet=true;
									}
									#endregion


								//compare with current day
								if (bNextSchIDToSetFound==false)
								{
									if ((int)DateTime.Now.DayOfWeek+1 == nDayCntr)
									{
										if (DateTime.Now.ToString("HH:mm").CompareTo((string)timeItem) <=0)
										{
											bNextSchIDToSetFound=true;
											nNextSchIDToSet=nSchIDCntr;
											dtNextTimeToExec = CalculateNextTime(DateTime.Now, nDayCntr, (string)timeItem);

										}
						
									}
									if ( (int)DateTime.Now.DayOfWeek+1 < nDayCntr)
									{
											bNextSchIDToSetFound=true;
											nNextSchIDToSet=nSchIDCntr;
											dtNextTimeToExec = CalculateNextTime(DateTime.Now, nDayCntr, (string)timeItem);
											
									}

								}
						
							sInsertReportScheduleCmd.Parameters["@SCHEDULE_ID"].Value=nSchIDCntr;
							sInsertReportScheduleCmd.Parameters["@SCHEDULE_DAY"].Value=nDayCntr;
							sInsertReportScheduleCmd.Parameters["@SCHEDULE_TIME"].Value=(string)timeItem;
							if (nSchIDCntr==nSchIDsMax)
								sInsertReportScheduleCmd.Parameters["@NEXT_SCHEDULE_ID"].Value=1; //set back to one
							else
								sInsertReportScheduleCmd.Parameters["@NEXT_SCHEDULE_ID"].Value=++nSchIDCntr; //increment

							dbHelper.ExecuteNonQuery(sInsertReportScheduleCmd);
						}
					
					}

				
				}
					

					#endregion 

					#region insert master
			
			

			if (bNextSchIDToSetFound==false)
			{
				sInsertReportMainCmd.Parameters["@NEXT_SCHEDULE_ID"].Value=nNextSchIDToSet;
				sInsertReportMainCmd.Parameters["@NEXT_EXECUTION_TIME"].Value=dtFirstTimeToExec;
			}
			else
			{
				if (nNextSchIDToSet==nSchIDsMax) 
				{
					//set to 1
					sInsertReportMainCmd.Parameters["@NEXT_SCHEDULE_ID"].Value=1;
				}
				else
				{
					//set to 1
					sInsertReportMainCmd.Parameters["@NEXT_SCHEDULE_ID"].Value=nNextSchIDToSet+1;
				}
				
				sInsertReportMainCmd.Parameters["@NEXT_EXECUTION_TIME"].Value=dtNextTimeToExec;
			
			}
						dbHelper.ExecuteNonQuery(sInsertReportMainCmd);

			
			

			
					#endregion 


					#region insert sql
						dbHelper.ExecuteNonQuery(sInsertReportSQLCmd);
					#endregion 

			
					
			
			

			#endregion
		
		
		}

		private DateTime CalculateNextTime(DateTime LastTimeRun,  int NextDay, string NextTime)
		{
		
			DateTime dtNextRunTime;
			
			int loopDOW=-1;
			DateTime dtTomorrow = LastTimeRun;
			
			loopDOW= (int)dtTomorrow.DayOfWeek;

			while (loopDOW+1!=NextDay) //loop till we have our day could be the same day
			{
				dtTomorrow= dtTomorrow.AddDays(1); //jump to the next date and check DOW
				loopDOW= (int)dtTomorrow.DayOfWeek;
			}
			
			dtNextRunTime=DateTime.Parse(dtTomorrow.ToString("yyyy-MM-dd") + " " + NextTime) ;
			//In case the dtnextruntime is less than lastrun time then the user has probably selected only one day and time in the week to run this report so just add 7 days to dtNextRunTime and return
			if (dtNextRunTime <LastTimeRun) 
			{
				dtNextRunTime= dtNextRunTime.AddDays(7);
			}
				
			
			return dtNextRunTime;


		}

		private void btnRemoveTime_Click(object sender, System.EventArgs e)
		{
			if(lstSelectedTimes.SelectedItem!=null)
			 lstSelectedTimes.Items.Remove(lstSelectedTimes.SelectedItem);
		}

		private void btnSvcSelect_Click(object sender, System.EventArgs e)
		{
			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
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
				
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion

			string sSelectReportsSQL= "SELECT REPORT_ID FROM PERIODIC_REPORT WHERE SERVICE_ID=@SERVICE_ID";

		;
			SqlCommand sSQLSearchCmd = new SqlCommand();

			sSQLSearchCmd.CommandText=sSelectReportsSQL;
			SqlParameter sSvcParam = new SqlParameter("@SERVICE_ID",SqlDbType.NVarChar);
			sSvcParam.Value=cmbServices.Text;
			sSQLSearchCmd.Parameters.Add(sSvcParam);

		//	CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			cmbReportIDs.Items.Clear();

			IDataReader sdr=null;
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				while(sdr.Read())
				{
					cmbReportIDs.Items.Add(sdr.GetInt32(0).ToString());
				}
				
				if (cmbReportIDs.Items.Count!=0) 
				{
					gpSelectReport.Enabled=true;
					cmbReportIDs.SelectedIndex=0;

				
				}
				else //no users matching criteria
				{
					gpSelectReport.Enabled=false;
					MessageBox.Show("No Reports for this service.","Admin Client");
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
			SetNoReportSelected();




		}

		private void btnRepSelect_Click(object sender, System.EventArgs e)
		{
			SetNoReportSelected();

			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
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
				
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion


			#region Connect to DB and getData

			
			
			string sReportSelectSQL = "SELECT [LAST_EXECUTION_TIME] ,[LAST_UPDATE_USERID]       ,[LAST_UPDATE_TIME]  FROM PERIODIC_REPORT " + 
				" WHERE REPORT_ID = @REPORT_ID";
			

			string sReportSelectSQLSQL = "SELECT [QUERY_SQL]      ,[QUERY_DESCRIPTION] FROM [PERIODIC_REPORT_SQL] " + 
				" WHERE REPORT_ID = @REPORT_ID";


			string sReportSelectSchSQL = "SELECT [SCHEDULE_DAY]      ,[SCHEDULE_TIME] FROM [PERIODIC_REPORT_SCHEDULE] " +
				" WHERE REPORT_ID = @REPORT_ID ORDER BY SCHEDULE_ID";


			SqlCommand sSQLSearchCmd = new SqlCommand(sReportSelectSQL);
			SqlCommand sSQLReportSQLCmd = new SqlCommand(sReportSelectSQLSQL);
			SqlCommand sSQLReportSchCmd = new SqlCommand(sReportSelectSchSQL);


			#region Set params


			SqlParameter sRepIDParam = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam.Value= int.Parse(cmbReportIDs.Text);
			sSQLSearchCmd.Parameters.Add(sRepIDParam);

			SqlParameter sRepIDParam2 = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam2.Value= int.Parse(cmbReportIDs.Text);
			sSQLReportSQLCmd.Parameters.Add(sRepIDParam2);

			SqlParameter sRepIDParam3 = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam3.Value= int.Parse(cmbReportIDs.Text);
			sSQLReportSchCmd.Parameters.Add(sRepIDParam3);

			#endregion

			#region DataReader Master

			IDataReader sdr=null;
		

			
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleRow);
				if(sdr.Read()==true)
				{
					
					m_dtLastExectime = sdr.GetDateTime(0);
					txtReportID.Text=cmbReportIDs.Text;
		
				}	
				else
				{
					gpSelectReport.Enabled=false;
					MessageBox.Show("No Report matching criteria","Admin Client");
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


			
			#region DataReader Report SQL

			sdr=null;
			
			string sReportSQL, sReportDescription;
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLReportSQLCmd,CommandBehavior.SingleRow);
				if(sdr.Read()==true)
				{
					
					sReportSQL = sdr.GetString(0);
					sReportDescription = sdr.GetString(1);
					txtReportDescription.Text=sReportDescription;
					txtReportSQL.Text=sReportSQL;
				}	
				else
				{
					gpSelectReport.Enabled=false;
					MessageBox.Show("No Report matching criteria","Admin Client");
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

			
			#region DataReader Schedule

			sdr=null;
			
			

			int  nSchDay;
			string sSchTime;
			try
			{
			
				dbHelper.ConnectToDB();

				sdr = dbHelper.GetDataReader(sSQLReportSchCmd,CommandBehavior.Default);
				int nPreviousDay=-1; // used to prevent repeated processing for same day
				bool bAllTimesAdded = false;
				lstSelectedTimes.Items.Clear();
				while(sdr.Read())
				{
			
		
					nSchDay = sdr.GetInt32(0);
					sSchTime = sdr.GetString(1);

				

					if (nSchDay!= nPreviousDay)
					{
						if (nPreviousDay!=-1)//all time values added for a day so no need to add again
						{
							bAllTimesAdded=true;
						}
						nPreviousDay=nSchDay;
						m_aDayBoxes[nSchDay-1].Checked=true;

					}
					if (bAllTimesAdded==false ) //only need to add the times once that is for the first day
					{
						lstSelectedTimes.Items.Add(sSchTime);
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
				dbHelper.Close();
			}
			#endregion

			#endregion


			#region Clear and Populate controls
			//set control values

			



			gpRepHeader.Enabled=true;
			gpSchedule.Enabled=true;
			btnDelete.Enabled=true;
			btnUpdate.Enabled=true;
			
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
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
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
				
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion

			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction();
				DeletePeriodicReport(dbHelper,int.Parse( cmbReportIDs.Text));
			
				dbHelper.CommitTransaction();
				MessageBox.Show("Report deleted successfully","Admin Client");
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

		private void DeletePeriodicReport(CommonDBHelper dbHelper, int ReportID)
		{
		

			#region Connect to DB and getData

			
			
			string sReportDeleteSQL = "DELETE PERIODIC_REPORT " + 
				" WHERE REPORT_ID = @REPORT_ID";
			

			string sReportDeleteSQLSQL = "DELETE [PERIODIC_REPORT_SQL] " + 
				" WHERE REPORT_ID = @REPORT_ID";


			string sReportDeleteSchSQL = "DELETE [PERIODIC_REPORT_SCHEDULE] " +
				" WHERE REPORT_ID = @REPORT_ID";


			SqlCommand sSQLDelCmd = new SqlCommand(sReportDeleteSQL);
			SqlCommand sSQLReportSQLCmd = new SqlCommand(sReportDeleteSQLSQL);
			SqlCommand sSQLReportSchCmd = new SqlCommand(sReportDeleteSchSQL);


			#region Set params


			SqlParameter sRepIDParam = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam.Value= int.Parse(cmbReportIDs.Text);
			sSQLDelCmd.Parameters.Add(sRepIDParam);

			SqlParameter sRepIDParam2 = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam2.Value= int.Parse(cmbReportIDs.Text);
			sSQLReportSQLCmd.Parameters.Add(sRepIDParam2);

			SqlParameter sRepIDParam3 = new SqlParameter("@REPORT_ID",SqlDbType.NVarChar);
			sRepIDParam3.Value= int.Parse(cmbReportIDs.Text);
			sSQLReportSchCmd.Parameters.Add(sRepIDParam3);

			#endregion

			#region Delete
		

			

				//transaction management in caller
				dbHelper.ExecuteNonQuery( sSQLDelCmd);
				dbHelper.ExecuteNonQuery( sSQLReportSQLCmd);
				dbHelper.ExecuteNonQuery( sSQLReportSchCmd);

				
				

			
			
			
			#endregion

			#endregion


			

		
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			CommonDBHelper dbHelper;
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
				}
				else if (cmbServices.Text=="WSST")
				{
					sConnectString=frmMain.HTConnectionStrings["ExternalDB"].ToString();
				
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
				
			
			}

			dbHelper= new CommonDBHelper(sConnectString);
			#endregion

			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction();
				DeletePeriodicReport(dbHelper,int.Parse( cmbReportIDs.Text));
				InsertReportToDB(dbHelper);
				dbHelper.CommitTransaction();
				MessageBox.Show("Report updated successfully","Admin client");
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

		private void cmbReportIDs_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetNoReportSelected();
		}

	}
}
