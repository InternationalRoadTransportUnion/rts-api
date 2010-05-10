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
	/// Summary description for AddPeriodicReport.
	/// </summary>
	public class AddPeriodicReport : System.Windows.Forms.Form
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
		private System.Windows.Forms.GroupBox gpSchedule;
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
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cmbAvailableTime;
		private System.Windows.Forms.Button btnAddTime;
		private System.Windows.Forms.Button btnRemoveTime;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;


		private CheckBox[] m_aDayBoxes;

		public AddPeriodicReport()
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
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gpService.SuspendLayout();
			this.gpRepHeader.SuspendLayout();
			this.gpSchedule.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpService
			// 
			this.gpService.Controls.Add(this.cmbServices);
			this.gpService.Controls.Add(this.label6);
			this.gpService.Location = new System.Drawing.Point(16, 16);
			this.gpService.Name = "gpService";
			this.gpService.Size = new System.Drawing.Size(600, 40);
			this.gpService.TabIndex = 0;
			this.gpService.TabStop = false;
			// 
			// cmbServices
			// 
			this.cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbServices.Location = new System.Drawing.Point(160, 11);
			this.cmbServices.Name = "cmbServices";
			this.cmbServices.Size = new System.Drawing.Size(144, 21);
			this.cmbServices.TabIndex = 1;
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
			this.gpRepHeader.Location = new System.Drawing.Point(16, 64);
			this.gpRepHeader.Name = "gpRepHeader";
			this.gpRepHeader.Size = new System.Drawing.Size(600, 232);
			this.gpRepHeader.TabIndex = 1;
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
			this.gpSchedule.Location = new System.Drawing.Point(16, 304);
			this.gpSchedule.Name = "gpSchedule";
			this.gpSchedule.Size = new System.Drawing.Size(600, 216);
			this.gpSchedule.TabIndex = 2;
			this.gpSchedule.TabStop = false;
			// 
			// btnAddTime
			// 
			this.btnAddTime.Location = new System.Drawing.Point(240, 96);
			this.btnAddTime.Name = "btnAddTime";
			this.btnAddTime.Size = new System.Drawing.Size(96, 24);
			this.btnAddTime.TabIndex = 8;
			this.btnAddTime.Text = "Add time >";
			this.btnAddTime.Click += new System.EventHandler(this.btnAddTime_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 96);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(88, 24);
			this.label14.TabIndex = 6;
			this.label14.Text = "Available time";
			// 
			// cmbAvailableTime
			// 
			this.cmbAvailableTime.Location = new System.Drawing.Point(104, 96);
			this.cmbAvailableTime.Name = "cmbAvailableTime";
			this.cmbAvailableTime.Size = new System.Drawing.Size(120, 21);
			this.cmbAvailableTime.TabIndex = 7;
			// 
			// lstSelectedTimes
			// 
			this.lstSelectedTimes.Location = new System.Drawing.Point(368, 96);
			this.lstSelectedTimes.Name = "lstSelectedTimes";
			this.lstSelectedTimes.Size = new System.Drawing.Size(104, 95);
			this.lstSelectedTimes.TabIndex = 9;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(368, 72);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(160, 24);
			this.label13.TabIndex = 3;
			this.label13.Text = "Selected Time (24HH:mm)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(128, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 24);
			this.label5.TabIndex = 2;
			this.label5.Text = "Sun";
			// 
			// chkDay2
			// 
			this.chkDay2.Location = new System.Drawing.Point(176, 40);
			this.chkDay2.Name = "chkDay2";
			this.chkDay2.Size = new System.Drawing.Size(24, 24);
			this.chkDay2.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Day of the Week";
			// 
			// chkDay3
			// 
			this.chkDay3.Location = new System.Drawing.Point(216, 40);
			this.chkDay3.Name = "chkDay3";
			this.chkDay3.Size = new System.Drawing.Size(24, 24);
			this.chkDay3.TabIndex = 2;
			// 
			// chkDay1
			// 
			this.chkDay1.Location = new System.Drawing.Point(136, 40);
			this.chkDay1.Name = "chkDay1";
			this.chkDay1.Size = new System.Drawing.Size(24, 24);
			this.chkDay1.TabIndex = 0;
			this.chkDay1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// chkDay5
			// 
			this.chkDay5.Location = new System.Drawing.Point(296, 40);
			this.chkDay5.Name = "chkDay5";
			this.chkDay5.Size = new System.Drawing.Size(24, 24);
			this.chkDay5.TabIndex = 4;
			// 
			// chkDay4
			// 
			this.chkDay4.Location = new System.Drawing.Point(256, 40);
			this.chkDay4.Name = "chkDay4";
			this.chkDay4.Size = new System.Drawing.Size(24, 24);
			this.chkDay4.TabIndex = 3;
			// 
			// chkDay6
			// 
			this.chkDay6.Location = new System.Drawing.Point(336, 40);
			this.chkDay6.Name = "chkDay6";
			this.chkDay6.Size = new System.Drawing.Size(24, 24);
			this.chkDay6.TabIndex = 5;
			// 
			// chkDay7
			// 
			this.chkDay7.Location = new System.Drawing.Point(376, 40);
			this.chkDay7.Name = "chkDay7";
			this.chkDay7.Size = new System.Drawing.Size(24, 24);
			this.chkDay7.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(168, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 24);
			this.label7.TabIndex = 2;
			this.label7.Text = "Mon";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(208, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(32, 24);
			this.label8.TabIndex = 2;
			this.label8.Text = "Tue";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(248, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 24);
			this.label9.TabIndex = 2;
			this.label9.Text = "Wed";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(288, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(32, 24);
			this.label10.TabIndex = 2;
			this.label10.Text = "Thu";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(328, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(32, 24);
			this.label11.TabIndex = 2;
			this.label11.Text = "Fri";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(368, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(32, 24);
			this.label12.TabIndex = 2;
			this.label12.Text = "Sat";
			// 
			// btnRemoveTime
			// 
			this.btnRemoveTime.Location = new System.Drawing.Point(240, 136);
			this.btnRemoveTime.Name = "btnRemoveTime";
			this.btnRemoveTime.Size = new System.Drawing.Size(96, 24);
			this.btnRemoveTime.TabIndex = 10;
			this.btnRemoveTime.Text = "Remove time <";
			this.btnRemoveTime.Click += new System.EventHandler(this.btnRemoveTime_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(488, 528);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(128, 24);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(16, 528);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// AddPeriodicReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(624, 584);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.gpSchedule);
			this.Controls.Add(this.gpRepHeader);
			this.Controls.Add(this.gpService);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddPeriodicReport";
			this.Text = "AddPeriodicReport";
			this.Load += new System.EventHandler(this.AddPeriodicReport_Load);
			this.gpService.ResumeLayout(false);
			this.gpRepHeader.ResumeLayout(false);
			this.gpSchedule.ResumeLayout(false);
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
		private void AddPeriodicReport_Load(object sender, System.EventArgs e)
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
			lstSelectedTimes.Items.Clear();
			

			#endregion

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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			InsertReportToDB();
		}


		private void InsertReportToDB()
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


			string sInsertReportMainSQL= "INSERT INTO PERIODIC_REPORT (REPORT_ID, SERVICE_ID,LAST_EXECUTION_TIME,NEXT_SCHEDULE_ID,NEXT_EXECUTION_TIME,LAST_UPDATE_USERID,LAST_UPDATE_TIME) VALUES (@REPORT_ID, @SERVICE_ID,@LAST_EXECUTION_TIME,@NEXT_SCHEDULE_ID,@NEXT_EXECUTION_TIME,@LAST_UPDATE_USERID,getdate())";

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
			
			sLETimeParam.Value= new DateTime(2001,1,1);
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




			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction();

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

			
					dbHelper.CommitTransaction();
				MessageBox.Show("Report added successfully.","Admin client");
				
				//reset controls
				txtReportID.Text="";
				txtReportID.Focus();
			}
			catch(SqlException exx)
			{
				dbHelper.RollbackTransaction();

				if (exx.Number ==2627) //PK failure
				{
					MessageBox.Show("Report already exists. Change ReportID", "Admin Client");

					//prepare for next user
					txtReportID.SelectAll();
					
					txtReportID.Focus();
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

		private void btnRemoveTime_Click(object sender, System.EventArgs e)
		{
			if(lstSelectedTimes.SelectedItem!=null)
			 lstSelectedTimes.Items.Remove(lstSelectedTimes.SelectedItem);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
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
			
			//In case the dtnextruntime is less than lastrun time then the user has probably selected only one day and time in the week to run this report so just add 7 days to LastTimeRun and return
			if (dtNextRunTime <LastTimeRun) 
			{
				dtNextRunTime= dtNextRunTime.AddDays(7);
			}
			
				
			
			return dtNextRunTime;


		}

	
	}
}
