using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for DiagnosticReports.
	/// </summary>
	public class PerformanceReports : System.Windows.Forms.Form
	{

		#region data structures

		#region one bucket
		class Bucket
		{
			public DateTime Begin; //left inclusive- type 
			public DateTime End;
			public int TranCounter;
			public int MeanCompletionTime;
			public int[] AStepMeanTimeTable;  //sparse array stores mean times in each step
			public int[] AStepCountTable; //sparse array stores count in each step 


			public void AddTransaction(int CompletionSeconds)
			{
				MeanCompletionTime = (MeanCompletionTime*TranCounter + CompletionSeconds)/++TranCounter;
			}

			public void AddStepTime(int Step, int StepTimeSeconds)
			{
			
					AStepMeanTimeTable[Step] = (AStepMeanTimeTable[Step]*AStepCountTable[Step] + StepTimeSeconds)/++(AStepCountTable[Step]);
			
			}
				
				
			
			

			public Bucket ( DateTime BeginTime, string Duration, int MaxStep)
			{
				Begin=BeginTime;
				switch (Duration)
				{
					case "D":
						End=BeginTime.AddDays(1);
						break;

					case "H":
						End=BeginTime.AddHours(1);
						break;

					case "M":
						End=BeginTime.AddMinutes(1);
						break;

					case "S":

						End=BeginTime.AddSeconds(1);
						break;

				
				}
				
				AStepMeanTimeTable = new int[MaxStep+1]; //+1 allows us to access member by step no. else we would have to do step -1
				AStepCountTable= new int[MaxStep+1];
			
			}

			public Bucket ( DateTime BeginTime, DateTime EndTime, int MaxStep)
			{
			
						Begin = BeginTime;
						End=EndTime;
						AStepMeanTimeTable = new int[MaxStep+1];
						AStepCountTable= new int[MaxStep+1];
			}
		
		
		}
		#endregion

		class BucketLoad
		{
			public ArrayList ABuckets;
			int CurrentBucket =-1; //no buckets created at start
			int MaxStep;
			string Duration;
			DateTime BeginRange, EndRange;
			

			public BucketLoad(DateTime Begin, DateTime End, string Grouping, int LastStep)
			{
				Duration = Grouping;
				
				MaxStep= LastStep;
				
				BeginRange= Begin;
				EndRange=End;

				ABuckets = new ArrayList();
				
				
				CurrentBucket=-1;
			}
			

			private void AddStepTime(DateTime CreationTime, int Step, int StepTimeSeconds)
			{
				
					((Bucket)ABuckets[CurrentBucket]).AddStepTime(Step,StepTimeSeconds);
				
			}
		
			
			private void EnsureBucket(DateTime CreationTime)
			{
				if (CurrentBucket==-1) //first bucket not created yet
				{
				
					if (Duration=="N")
					{
						ABuckets.Add(  new Bucket(CreationTime,EndRange,MaxStep));
					}
					else
					{
						ABuckets.Add( new Bucket(CreationTime,Duration,MaxStep));
					}
					CurrentBucket=0;
					return;				
				}


				if (CreationTime< ((Bucket)ABuckets[CurrentBucket]).End)
				{
						
					return;
				}
				else
				{
				
					//calculate the boundry conditions for new bucket
					TimeSpan tGap = CreationTime - ((Bucket)ABuckets[CurrentBucket]).End;
					DateTime newBeginTime =DateTime.MinValue;

					switch (Duration)
					{
						case "D":
						 
							newBeginTime =((Bucket)ABuckets[CurrentBucket]).End.AddDays(  tGap.TotalDays);
							break;

						case "H":
							newBeginTime =((Bucket)ABuckets[CurrentBucket]).End.AddHours(  tGap.TotalHours);
							break;

						case "M":
							newBeginTime =((Bucket)ABuckets[CurrentBucket]).End.AddMinutes(  tGap.TotalMinutes);
							break;

						case "S":

							newBeginTime =((Bucket)ABuckets[CurrentBucket]).End.AddSeconds(  tGap.TotalSeconds);
							break;
				
					}



					ABuckets.Add( new Bucket(newBeginTime,Duration,MaxStep));
					++CurrentBucket;
				}
			}


			int PreviousStep=int.MaxValue; //intial value set to max so it is set correctly as records are in increasing order of step per id

			DateTime PreviousStepEndTime = DateTime.MinValue;

			public  void ProcessRecord(DateTime CreationTime,DateTime CompletionTime, int Step, DateTime StepEndTime )
			{
				EnsureBucket(CreationTime);

				if (Step < PreviousStep) //ID changed since data is sorted by id and step add a new transaction
				{
					((Bucket)ABuckets[CurrentBucket]).AddTransaction(	(int)((TimeSpan)(CompletionTime-CreationTime)).TotalSeconds);
					
					
				}	
				else
				{
				((Bucket)ABuckets[CurrentBucket]).AddStepTime ( Step, (int)((TimeSpan)(StepEndTime- PreviousStepEndTime)).TotalSeconds);
					
				
				}
				PreviousStep=Step;
				PreviousStepEndTime=StepEndTime;
				
				
			}
			

		}
		
		#endregion

		private System.Windows.Forms.GroupBox gpService;
		private System.Windows.Forms.Button btnSvcSelect;
		private System.Windows.Forms.ComboBox cmbServices;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox gpRepParameters;
		private System.Windows.Forms.Button btnExecuteReport;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtBeginTime;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtEndTime;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cmbStep;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtSenderIP;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ComboBox cmbSubscribers;
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbEndStep;
		private System.Windows.Forms.GroupBox gpGranularity;
		private System.Windows.Forms.RadioButton rdoDay;
		private System.Windows.Forms.RadioButton rdoHour;
		private System.Windows.Forms.RadioButton rdoMinute;
		private System.Windows.Forms.RadioButton rdoSecond;
		private System.Windows.Forms.RadioButton rdoNone;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtNoOfRows;
		private System.Windows.Forms.ListView lstReport;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PerformanceReports()
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
			this.gpService = new System.Windows.Forms.GroupBox();
			this.btnSvcSelect = new System.Windows.Forms.Button();
			this.cmbServices = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.gpRepParameters = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.gpGranularity = new System.Windows.Forms.GroupBox();
			this.rdoDay = new System.Windows.Forms.RadioButton();
			this.rdoHour = new System.Windows.Forms.RadioButton();
			this.rdoMinute = new System.Windows.Forms.RadioButton();
			this.rdoSecond = new System.Windows.Forms.RadioButton();
			this.rdoNone = new System.Windows.Forms.RadioButton();
			this.cmbSubscribers = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cmbStep = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtBeginTime = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtEndTime = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtSenderIP = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbEndStep = new System.Windows.Forms.ComboBox();
			this.btnExecuteReport = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.sfd1 = new System.Windows.Forms.SaveFileDialog();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.txtNoOfRows = new System.Windows.Forms.TextBox();
			this.lstReport = new System.Windows.Forms.ListView();
			this.gpService.SuspendLayout();
			this.gpRepParameters.SuspendLayout();
			this.gpGranularity.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpService
			// 
			this.gpService.Controls.Add(this.btnSvcSelect);
			this.gpService.Controls.Add(this.cmbServices);
			this.gpService.Controls.Add(this.label6);
			this.gpService.Location = new System.Drawing.Point(8, 8);
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
			// gpRepParameters
			// 
			this.gpRepParameters.Controls.Add(this.label8);
			this.gpRepParameters.Controls.Add(this.label5);
			this.gpRepParameters.Controls.Add(this.gpGranularity);
			this.gpRepParameters.Controls.Add(this.cmbSubscribers);
			this.gpRepParameters.Controls.Add(this.label10);
			this.gpRepParameters.Controls.Add(this.cmbStep);
			this.gpRepParameters.Controls.Add(this.label9);
			this.gpRepParameters.Controls.Add(this.label7);
			this.gpRepParameters.Controls.Add(this.txtBeginTime);
			this.gpRepParameters.Controls.Add(this.label2);
			this.gpRepParameters.Controls.Add(this.label3);
			this.gpRepParameters.Controls.Add(this.txtEndTime);
			this.gpRepParameters.Controls.Add(this.label12);
			this.gpRepParameters.Controls.Add(this.txtSenderIP);
			this.gpRepParameters.Controls.Add(this.label13);
			this.gpRepParameters.Controls.Add(this.label1);
			this.gpRepParameters.Controls.Add(this.cmbEndStep);
			this.gpRepParameters.Location = new System.Drawing.Point(8, 64);
			this.gpRepParameters.Name = "gpRepParameters";
			this.gpRepParameters.Size = new System.Drawing.Size(608, 272);
			this.gpRepParameters.TabIndex = 1;
			this.gpRepParameters.TabStop = false;
			this.gpRepParameters.Text = "Report Parameters";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(360, 128);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(216, 16);
			this.label8.TabIndex = 11;
			this.label8.Text = "Use value \"NOW\"  to refer to current time";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(360, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(208, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Enter time in YYYY-MM-DD 24hh:mm:ss format.";
			// 
			// gpGranularity
			// 
			this.gpGranularity.Controls.Add(this.rdoDay);
			this.gpGranularity.Controls.Add(this.rdoHour);
			this.gpGranularity.Controls.Add(this.rdoMinute);
			this.gpGranularity.Controls.Add(this.rdoSecond);
			this.gpGranularity.Controls.Add(this.rdoNone);
			this.gpGranularity.Location = new System.Drawing.Point(16, 200);
			this.gpGranularity.Name = "gpGranularity";
			this.gpGranularity.Size = new System.Drawing.Size(576, 56);
			this.gpGranularity.TabIndex = 9;
			this.gpGranularity.TabStop = false;
			this.gpGranularity.Text = "Grouping Granularity";
			// 
			// rdoDay
			// 
			this.rdoDay.Location = new System.Drawing.Point(140, 24);
			this.rdoDay.Name = "rdoDay";
			this.rdoDay.Size = new System.Drawing.Size(64, 24);
			this.rdoDay.TabIndex = 0;
			this.rdoDay.Text = "Day";
			// 
			// rdoHour
			// 
			this.rdoHour.Location = new System.Drawing.Point(252, 24);
			this.rdoHour.Name = "rdoHour";
			this.rdoHour.Size = new System.Drawing.Size(64, 24);
			this.rdoHour.TabIndex = 0;
			this.rdoHour.Text = "Hour";
			// 
			// rdoMinute
			// 
			this.rdoMinute.Location = new System.Drawing.Point(364, 24);
			this.rdoMinute.Name = "rdoMinute";
			this.rdoMinute.Size = new System.Drawing.Size(64, 24);
			this.rdoMinute.TabIndex = 0;
			this.rdoMinute.Text = "Minute";
			// 
			// rdoSecond
			// 
			this.rdoSecond.Location = new System.Drawing.Point(476, 24);
			this.rdoSecond.Name = "rdoSecond";
			this.rdoSecond.Size = new System.Drawing.Size(64, 24);
			this.rdoSecond.TabIndex = 0;
			this.rdoSecond.Text = "Second";
			// 
			// rdoNone
			// 
			this.rdoNone.Location = new System.Drawing.Point(36, 24);
			this.rdoNone.Name = "rdoNone";
			this.rdoNone.Size = new System.Drawing.Size(64, 24);
			this.rdoNone.TabIndex = 0;
			this.rdoNone.Text = "None";
			// 
			// cmbSubscribers
			// 
			this.cmbSubscribers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSubscribers.Location = new System.Drawing.Point(152, 138);
			this.cmbSubscribers.Name = "cmbSubscribers";
			this.cmbSubscribers.Size = new System.Drawing.Size(192, 21);
			this.cmbSubscribers.TabIndex = 8;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 80);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 24);
			this.label10.TabIndex = 6;
			this.label10.Text = "Begin Step";
			// 
			// cmbStep
			// 
			this.cmbStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStep.Location = new System.Drawing.Point(152, 80);
			this.cmbStep.Name = "cmbStep";
			this.cmbStep.Size = new System.Drawing.Size(184, 21);
			this.cmbStep.TabIndex = 5;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(360, 48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 4;
			this.label9.Text = "Note:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(360, 80);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(208, 16);
			this.label7.TabIndex = 3;
			this.label7.Text = "Use value \"ALL\" for WildCard Search";
			// 
			// txtBeginTime
			// 
			this.txtBeginTime.Location = new System.Drawing.Point(152, 24);
			this.txtBeginTime.MaxLength = 20;
			this.txtBeginTime.Name = "txtBeginTime";
			this.txtBeginTime.Size = new System.Drawing.Size(184, 20);
			this.txtBeginTime.TabIndex = 2;
			this.txtBeginTime.Text = "ALL";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "Begin Time ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 24);
			this.label3.TabIndex = 1;
			this.label3.Text = "End Time ";
			// 
			// txtEndTime
			// 
			this.txtEndTime.Location = new System.Drawing.Point(152, 52);
			this.txtEndTime.MaxLength = 20;
			this.txtEndTime.Name = "txtEndTime";
			this.txtEndTime.Size = new System.Drawing.Size(184, 20);
			this.txtEndTime.TabIndex = 3;
			this.txtEndTime.Text = "NOW";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 138);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(128, 24);
			this.label12.TabIndex = 1;
			this.label12.Text = "SubscriberID";
			// 
			// txtSenderIP
			// 
			this.txtSenderIP.Location = new System.Drawing.Point(152, 167);
			this.txtSenderIP.MaxLength = 15;
			this.txtSenderIP.Name = "txtSenderIP";
			this.txtSenderIP.Size = new System.Drawing.Size(184, 20);
			this.txtSenderIP.TabIndex = 7;
			this.txtSenderIP.Text = "ALL";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 167);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128, 24);
			this.label13.TabIndex = 1;
			this.label13.Text = "Sender IP Address";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 109);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "End Step";
			// 
			// cmbEndStep
			// 
			this.cmbEndStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbEndStep.Location = new System.Drawing.Point(152, 109);
			this.cmbEndStep.Name = "cmbEndStep";
			this.cmbEndStep.Size = new System.Drawing.Size(184, 21);
			this.cmbEndStep.TabIndex = 5;
			// 
			// btnExecuteReport
			// 
			this.btnExecuteReport.Location = new System.Drawing.Point(496, 344);
			this.btnExecuteReport.Name = "btnExecuteReport";
			this.btnExecuteReport.Size = new System.Drawing.Size(120, 24);
			this.btnExecuteReport.TabIndex = 2;
			this.btnExecuteReport.Text = "Execute Report";
			this.btnExecuteReport.Click += new System.EventHandler(this.btnExecuteReport_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(8, 344);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(136, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// sfd1
			// 
			this.sfd1.Filter = "CSV Files|*.csv";
			this.sfd1.Title = "Select File to save report.";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// txtNoOfRows
			// 
			this.txtNoOfRows.Location = new System.Drawing.Point(192, 344);
			this.txtNoOfRows.Name = "txtNoOfRows";
			this.txtNoOfRows.ReadOnly = true;
			this.txtNoOfRows.Size = new System.Drawing.Size(256, 20);
			this.txtNoOfRows.TabIndex = 4;
			this.txtNoOfRows.Text = "";
			// 
			// lstReport
			// 
			this.lstReport.Location = new System.Drawing.Point(16, 384);
			this.lstReport.Name = "lstReport";
			this.lstReport.Size = new System.Drawing.Size(600, 160);
			this.lstReport.TabIndex = 5;
			this.lstReport.View = System.Windows.Forms.View.Details;
			// 
			// PerformanceReports
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 567);
			this.Controls.Add(this.lstReport);
			this.Controls.Add(this.txtNoOfRows);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnExecuteReport);
			this.Controls.Add(this.gpRepParameters);
			this.Controls.Add(this.gpService);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "PerformanceReports";
			this.Text = "Performance Reports";
			this.Load += new System.EventHandler(this.DiagnosticReports_Load);
			this.gpService.ResumeLayout(false);
			this.gpRepParameters.ResumeLayout(false);
			this.gpGranularity.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DiagnosticReports_Load(object sender, System.EventArgs e)
		{
			PopulateServices(cmbServices);
			FillSubscriberCombo();
		}

		private void PopulateServices(ComboBox CmbServices)
		{
			CmbServices.Items.Clear();
			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				CmbServices.Items.Add("TCHQ");
				
			}
			else
			{
				CmbServices.Items.Add("WSST");
				CmbServices.Items.Add("WSCscc");
			
			}
			CmbServices.SelectedIndex=0;
		
		}

		private void cmbServices_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			gpRepParameters.Enabled=false;
			btnExecuteReport.Enabled=false;
		}

		
		
	
		CommonDBHelper m_dbHelper ;

		private void btnSvcSelect_Click(object sender, System.EventArgs e)
		{
			cmbStep.Items.Clear();
			cmbEndStep.Items.Clear();
			rdoNone.Checked=true;
			
			
			#region Setcontrols

			
			#region select which DB

			string sConnectString =null;

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
				if (cmbServices.Text=="TCHQ")
				{
					sConnectString=frmMain.HTConnectionStrings["TCHQDB"].ToString();
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

			m_dbHelper= new CommonDBHelper(sConnectString);
			#endregion

			
			cmbStep.Items.Clear();

			#endregion


			#region fill up combos
			



			#region Fill Step combo
			string sStepSelect = "SELECT STEP, STEP_DESCRIPTION FROM STEP_DEFINITIONS ORDER BY STEP";

			SqlCommand sSQLSearchCmd = new SqlCommand();

			sSQLSearchCmd.CommandText=sStepSelect;
			
			IDataReader sdr=null;
			cmbStep.Items.Clear();
			cmbEndStep.Items.Clear();
			
			try
			{
			
				m_dbHelper.ConnectToDB();

				sdr = m_dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				
				while(sdr.Read())
				{
					cmbStep.Items.Add(new StepCodeDescriptionMap(sdr.GetInt32(0),sdr.GetString(1)));
					cmbEndStep.Items.Add(new StepCodeDescriptionMap(sdr.GetInt32(0),sdr.GetString(1)));
				}

				//SMD 2006-12-13
				if(cmbStep.Items.Count > 0)
				{
					cmbStep.SelectedIndex = 0; 
				}
				cmbEndStep.SelectedIndex = cmbEndStep.Items.Count-1; 
				//======================================================
				
			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				if (sdr!=null) sdr.Close();
				m_dbHelper.Close();
			}
			#endregion

			
			#endregion

			gpRepParameters.Enabled=true;
			btnExecuteReport.Enabled=true;

		}

		
		


		private void FillSubscriberCombo()
		{
			string sSubsSelectSQL;
			SqlCommand sSQLSearchCmd = new SqlCommand();
			sSQLSearchCmd.CommandType=CommandType.Text;

			
			sSubsSelectSQL = "SELECT SUBSCRIBER_ID FROM WS_SUBSCRIBER ORDER BY SUBSCRIBER_ID";
			sSQLSearchCmd.CommandText=sSubsSelectSQL;
				
			

			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);

			IDataReader sdr=null;
			try
			{
			
				dbSubs.ConnectToDB();

				cmbSubscribers.Items.Clear();
				

				sdr = dbSubs.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				while(sdr.Read())
				{
					cmbSubscribers.Items.Add(sdr.GetString(0));
				}

				cmbSubscribers.SelectedIndex=0;
			
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

		private void cmbStepResults_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		DateTime dtBegin, dtEnd;

		private void btnExecuteReport_Click(object sender, System.EventArgs e)
		{

			
			#region Validations


		
			
		

			#region begin time

			if (txtBeginTime.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtBeginTime,"Mandatory Field, Use \"ALL\" for wildcard search.");
				txtBeginTime.Focus();
				return;
			}

			if (txtBeginTime.Text.Trim() !="ALL") 
			{

				Regex regexTimeMatch = new Regex(@"\d{4}-[01]{1}\d{1}-[0123]{1}\d{1}\s[012]{1}\d{1}:[012345]{1}\d{1}:[012345]{1}\d{1}", 
					RegexOptions.Singleline);
				Match aMatch = regexTimeMatch.Match(txtBeginTime.Text);

				if (aMatch.Success)
				{
					if ( aMatch.Value!= txtBeginTime.Text)
					{
						errorProvider1.SetError(txtBeginTime,"Enter time in correct format");
						txtBeginTime.Text="";
						return;
					}
				}
				else
				{
					errorProvider1.SetError(txtBeginTime,"Enter time in correct format");
					txtBeginTime.Text="";
					return;
				}

				try
				{
					dtBegin = DateTime.Parse(txtBeginTime.Text);
				}
				catch (Exception exx)
				{
					errorProvider1.SetError(txtBeginTime,"Enter time in correct format");
					txtBeginTime.Text="";
					return;
				}
				
			}
			else
			{
				dtBegin= DateTime.MinValue; //place holder
			
			}

			errorProvider1.SetError(txtBeginTime,"");
			#endregion 



			#region end time

			if (txtEndTime.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtEndTime,"Mandatory Field, Use \"NOW\" for wildcard search.");
				txtEndTime.Focus();
				return;
			}

			if (txtEndTime.Text.Trim() !="NOW") 
			{

				Regex regexTimeMatch = new Regex(@"\d{4}-[01]{1}\d{1}-[0123]{1}\d{1}\s[012]{1}\d{1}:[012345]{1}\d{1}:[012345]{1}\d{1}", 
					RegexOptions.Singleline);
				Match aMatch = regexTimeMatch.Match(txtEndTime.Text);

				if (aMatch.Success)
				{
					if ( aMatch.Value!= txtEndTime.Text)
					{
						errorProvider1.SetError(txtEndTime,"Enter time in correct format");
						txtEndTime.Text="";
						return;
					}
				}
				else
				{
					errorProvider1.SetError(txtEndTime,"Enter time in correct format");
					txtEndTime.Text="";
					return;
				}

				try
				{
					dtEnd=DateTime.Parse(txtEndTime.Text);
				}
				catch (Exception exx)
				{
					errorProvider1.SetError(txtEndTime,"Enter time in correct format");
					txtEndTime.Text="";
					return;
				}
				
			}
			else
			{
				dtEnd= DateTime.Now;
			}

			errorProvider1.SetError(txtEndTime,"");


			if (dtEnd<=dtBegin)
			{
				errorProvider1.SetError(txtEndTime,"End Time should be more than begin time");
				txtEndTime.Text="";
				return;
			
			}

			#endregion 


			#region step
			if (cmbStep.Text.Trim() =="") 
			{
				errorProvider1.SetError(cmbStep,"Mandatory Field");
				cmbStep.Focus();
				return;
			}
			
				errorProvider1.SetError(cmbStep,"");
			
			if (cmbEndStep.Text.Trim() =="") 
			{
				errorProvider1.SetError(cmbEndStep,"Mandatory Field");
				cmbEndStep.Focus();
				return;
			}
			
			errorProvider1.SetError(cmbEndStep,"");


			if (((StepCodeDescriptionMap)cmbEndStep.SelectedItem).Step <=((StepCodeDescriptionMap)cmbStep.SelectedItem).Step)
			{
				errorProvider1.SetError(cmbEndStep,"End Step should be more than begin step");
				cmbEndStep.Focus();
				return;
			
			}


			#endregion 

			#endregion
			ExecuteReport();
		}

		private void ExecuteReport()
		{
			
			

			SqlCommand sRepCommand = null;


			switch (cmbServices.Text)
			{
				case "TCHQ":
			
					sRepCommand= PrepareTCHQQuery();

					break;

				case "WSST":
					
					
					
						sRepCommand= PrepareWSSTInternalQuery();
					
					break;

				case "WSCscc":

						sRepCommand= PrepareWSCsccQuery();
					break;

			

			
			}//switch
		

			DialogResult dr = sfd1.ShowDialog();

			if (dr==DialogResult.Cancel) return;

			
			
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

			m_dbHelper= new CommonDBHelper(sConnectString);
			#endregion


			#region process data

			

			string Grouping ="N";

			if (rdoNone.Checked)
				Grouping="N";

			if (rdoDay.Checked)
				Grouping="D";

			if (rdoHour.Checked)
				Grouping="H";

			if (rdoMinute.Checked)
				Grouping="M";

			if (rdoSecond.Checked)
				Grouping="S";


			BucketLoad ReportProcessor = new BucketLoad(dtBegin,dtEnd,Grouping,((StepCodeDescriptionMap)cmbEndStep.SelectedItem).Step);

			IDataReader repReader=null; 
			try
			{
				m_dbHelper.ConnectToDB();
				repReader =  m_dbHelper.GetDataReader(sRepCommand, CommandBehavior.SingleResult );
				
				int nRowCounter = 0;
								
				if (repReader.Read())
				{
					do
					{
						#region ProcessRow
						ReportProcessor.ProcessRecord(repReader.GetDateTime(0),repReader.GetDateTime(1),repReader.GetInt32(2),repReader.GetDateTime(3));

						

						nRowCounter++;
						#endregion
					}while ( repReader.Read());
				}
				else
				{
					MessageBox.Show(this, "No Data matching your criteria.", "Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
			
			
				if (repReader!=null)
				{
					repReader.Close();
				}
				m_dbHelper.Close();
			}


			#endregion

			#region

			//SMD 2006-12-13
			txtNoOfRows.Text = ReportProcessor.ABuckets.Count.ToString();
			if(MessageBox.Show("Total rows in the report : "+txtNoOfRows.Text +" \n Do you want to continue ?","Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)== DialogResult.No)
			{
				return;
			}
			//===========================================================================================================


			#endregion
					
			#region write out File

			Stream sRepStream;
			try
			{
				sRepStream = sfd1.OpenFile();	
			}
			catch (IOException  exx)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			
			}

			StreamWriter sRepWriter = new StreamWriter(sRepStream, System.Text.Encoding.Unicode);
			

			//SMD 2006-12-13
			lstReport.Items.Clear();
			lstReport.Columns.Clear();

			lstReport.Columns.Add("Window",50,HorizontalAlignment.Left); 
			lstReport.Columns.Add("Total_Transactions",50,HorizontalAlignment.Left);
			lstReport.Columns.Add("Mean_Transaction_Time",50,HorizontalAlignment.Left);
			//=======================================================================================

			int[] aActualSteps;
			try
			{
				
				#region headers
				sRepWriter.Write("Window,Total_Transactions,Mean_Transaction_Time,");

				//write steps out from the combo

				int nBeginIndex= cmbStep.SelectedIndex ; 

				int nEndIndex= cmbEndStep.SelectedIndex;

				aActualSteps=new int[nEndIndex-nBeginIndex]; // as we dont print the first step we dont need nEndIndex-nBeginIndex + 1


				for (int nStepCntr= nBeginIndex+1, nActualStepCounter=0; nStepCntr<=nEndIndex;nStepCntr++, nActualStepCounter++)
				{
					sRepWriter.Write(((StepCodeDescriptionMap)cmbStep.Items[nStepCntr]).StepDescription)	;

					//SMD 2006-12-13
					lstReport.Columns.Add(((StepCodeDescriptionMap)cmbStep.Items[nStepCntr]).StepDescription,50,HorizontalAlignment.Left);
					//==============================================================

					aActualSteps[nActualStepCounter]= ((StepCodeDescriptionMap)cmbStep.Items[nStepCntr]).Step;
					if (nStepCntr<nEndIndex)
					{
						sRepWriter.Write(",");
					}
				
				}

				sRepWriter.Write("\r\n");

					#endregion

				#region Rows

				foreach (Bucket oneBucket in ReportProcessor.ABuckets )
				{
					//SMD 2006-12-13
					ListViewItem itm = new ListViewItem(oneBucket.Begin.ToString("yyyy-MM-dd HH:mm:ss") + " - " + oneBucket.End.ToString("yyyy-MM-dd HH:mm:ss"));
                    itm.SubItems.Add(oneBucket.TranCounter.ToString());
					itm.SubItems.Add(oneBucket.MeanCompletionTime.ToString());
					//==================================================================

					sRepWriter.Write(oneBucket.Begin.ToString("yyyy-MM-dd HH:mm:ss") + " - " + oneBucket.End.ToString("yyyy-MM-dd HH:mm:ss"));

					sRepWriter.Write(",");

					sRepWriter.Write(oneBucket.TranCounter);

					sRepWriter.Write(",");

					sRepWriter.Write(oneBucket.MeanCompletionTime);

					sRepWriter.Write(",");

					int nMaxStepToPrint = aActualSteps.Length;

					for(int nStepCntr=0; nStepCntr< nMaxStepToPrint; nStepCntr++)
					{
						sRepWriter.Write(oneBucket.AStepMeanTimeTable[aActualSteps[nStepCntr]]);

						//SMD 2006-12-13
						itm.SubItems.Add(oneBucket.AStepMeanTimeTable[aActualSteps[nStepCntr]].ToString());
						//==================================================

							if (nStepCntr< nMaxStepToPrint-1)
							sRepWriter.Write(",");
					}
					lstReport.Items.Add(itm);

					sRepWriter.Write("\r\n");
				}
				#endregion


			}
				finally
			{
				sRepWriter.Flush();
				sRepWriter.Close();
			
			}
			MessageBox.Show(this, "Report Created Successfully", "Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Information);
			return;

			#endregion

		}


		
		SqlCommand PrepareTCHQQuery()
		{
			
			SqlCommand sqlReportCmd = new SqlCommand();

			SqlParameter sParam = null;
			string sWhereSQL="";

			string sReportSQL=
				"SELECT  WIL.ROW_CREATED_TIME, WIL.COMPLETION_TIME,  TCHQ_STEP, WSEQ.LAST_UPDATE_TIME FROM     " + 
				"TCHQ_REQUEST_LOG WIL WITH (NOLOCK) INNER JOIN TCHQ_SEQUENCE WSEQ  WITH (NOLOCK) ON  WIL.TCHQ_QUERY_ID = WSEQ.TCHQ_QUERY_ID  " + 
				"WHERE " ;
	
			sWhereSQL += "AND SENDER_ID = @SENDER_ID " +
				"AND ROW_CREATED_TIME<=@ROW_CREATED_TIME_END " ;

			sWhereSQL += "AND TCHQ_STEP>=@TCHQ_STEP_START AND TCHQ_STEP<=@TCHQ_STEP_END ";

			#region optional params
			if (txtSenderIP.Text!="ALL")
			{
				sWhereSQL +="AND SENDER_TCP_IP_ADDRESS LIKE @O_SENDER_TCP_IP_ADDRESS " ;
				sParam = new SqlParameter("@O_SENDER_TCP_IP_ADDRESS",SqlDbType.NVarChar);
				sParam.Value=txtSenderIP.Text + "%";

				sqlReportCmd.Parameters.Add(sParam);
			}

			if (txtBeginTime.Text!="ALL")
			{
				sWhereSQL +="AND ROW_CREATED_TIME >= @O_ROW_CREATED_TIME_START " ;
				sParam = new SqlParameter("@O_ROW_CREATED_TIME_START",SqlDbType.DateTime);
				sParam.Value= DateTime.Parse(txtBeginTime.Text) ;

				sqlReportCmd.Parameters.Add(sParam);
			}
			#endregion 

			sWhereSQL +=" AND WIL.COMPLETION_TIME IS NOT NULL  ORDER BY WIL.ROW_CREATED_TIME, WIL.TCHQ_QUERY_ID, TCHQ_STEP ";

			#region mandatory params

			sParam = new SqlParameter("@TCHQ_STEP_START",SqlDbType.Int);
			sParam.Value= ((StepCodeDescriptionMap)cmbStep.SelectedItem).Step;
			sqlReportCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@TCHQ_STEP_END",SqlDbType.Int);
			sParam.Value= ((StepCodeDescriptionMap)cmbEndStep.SelectedItem).Step;
			sqlReportCmd.Parameters.Add(sParam);
						
			
			sParam = new SqlParameter("@SENDER_ID",SqlDbType.NVarChar);
			sParam.Value=cmbSubscribers.Text;

			sqlReportCmd.Parameters.Add(sParam);			
			
			
			sParam = new SqlParameter("@ROW_CREATED_TIME_END",SqlDbType.DateTime);
				
			if (txtEndTime.Text=="NOW")
			{
				sParam.Value=DateTime.Now;
			}
			else
			{
				sParam.Value=DateTime.Parse(txtEndTime.Text);
			}
				
			sqlReportCmd.Parameters.Add(sParam);
			
			
			#endregion


			//get rid of leading and
			sWhereSQL= sWhereSQL.Substring(4);

			sqlReportCmd.CommandText=  sReportSQL + sWhereSQL;

			return sqlReportCmd;
		
		}



			


			SqlCommand PrepareWSSTInternalQuery()
			{
			
				SqlCommand sqlReportCmd = new SqlCommand();

				SqlParameter sParam = null;
				string sWhereSQL="";

			string sReportSQL=
				"SELECT      WIL.ROW_CREATED_TIME, WIL.COMPLETION_TIME,  WSST_STEP, WSEQ.LAST_UPDATE_TIME FROM     " + 
				"WSST_INTERNAL_LOG WIL WITH (NOLOCK) INNER JOIN WSST_SEQUENCE WSEQ  WITH (NOLOCK) ON  WIL.SAFETIR_MESSAGE_IN_ID = WSEQ.SAFETIR_MESSAGE_IN_ID  " + 
"WHERE " ;
				

sWhereSQL +="AND SUBSCRIBER_ID = @SUBSCRIBER_ID " +

"AND ROW_CREATED_TIME<=@ROW_CREATED_TIME_END " ;

sWhereSQL += "AND WSST_STEP>=@WSST_STEP_START AND WSST_STEP<=@WSST_STEP_END ";


				#region optional params
				if (txtSenderIP.Text!="ALL")
				{
					sWhereSQL +="AND SENDER_TCP_IP_ADDRESS LIKE @O_SENDER_TCP_IP_ADDRESS " ;
					sParam = new SqlParameter("@O_SENDER_TCP_IP_ADDRESS",SqlDbType.NVarChar);
					sParam.Value=txtSenderIP.Text + "%";

					sqlReportCmd.Parameters.Add(sParam);
				}

				if (txtBeginTime.Text!="ALL")
				{
					sWhereSQL +="AND ROW_CREATED_TIME >= @O_ROW_CREATED_TIME_START " ;
					sParam = new SqlParameter("@O_ROW_CREATED_TIME_START",SqlDbType.DateTime);
					sParam.Value= DateTime.Parse(txtBeginTime.Text) ;

					sqlReportCmd.Parameters.Add(sParam);
				}
				#endregion 

sWhereSQL +=" AND WIL.COMPLETION_TIME IS NOT NULL  ORDER BY WIL.ROW_CREATED_TIME, WIL.SAFETIR_MESSAGE_IN_ID, WSST_STEP "; 

				#region mandatory params

				sParam = new SqlParameter("@WSST_STEP_START",SqlDbType.Int);
				sParam.Value= ((StepCodeDescriptionMap)cmbStep.SelectedItem).Step;
				sqlReportCmd.Parameters.Add(sParam);

				sParam = new SqlParameter("@WSST_STEP_END",SqlDbType.Int);
				sParam.Value= ((StepCodeDescriptionMap)cmbEndStep.SelectedItem).Step;
				sqlReportCmd.Parameters.Add(sParam);
		
			
				sParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
				sParam.Value=cmbSubscribers.Text;

				sqlReportCmd.Parameters.Add(sParam);			
			
			
				sParam = new SqlParameter("@ROW_CREATED_TIME_END",SqlDbType.DateTime);
				
				if (txtEndTime.Text=="NOW")
				{
					sParam.Value=DateTime.Now;
				}
				else
				{
					sParam.Value=DateTime.Parse(txtEndTime.Text);
				}
				
				sqlReportCmd.Parameters.Add(sParam);
			
			
		#endregion


				//get rid of leading and
					sWhereSQL= sWhereSQL.Substring(4);

				sqlReportCmd.CommandText=  sReportSQL + sWhereSQL;

				return sqlReportCmd;
		
			}



		
			
		SqlCommand PrepareWSCsccQuery()
		{
			
			SqlCommand sqlReportCmd = new SqlCommand();

			SqlParameter sParam = null;
			string sWhereSQL="";

			string sReportSQL=
				"SELECT  WIL.CREATION_TIME, WIL.COMPLETION_TIME,  COPY_STEP, WSEQ.LAST_UPDATE_TIME FROM     " + 
				"WSST_COPY_TO_LOG WIL WITH (NOLOCK) INNER JOIN WSST_COPY_TO_SEQUENCE WSEQ  WITH (NOLOCK) ON  WIL.SAFETIR_MESSAGE_IN_ID = WSEQ.SAFETIR_MESSAGE_IN_ID  " + 
				"WHERE " ;
	
			
		
			sWhereSQL +="AND SUBSCRIBER_ID = @SUBSCRIBER_ID " +

				"AND CREATION_TIME<=@ROW_CREATED_TIME_END " ;

			sWhereSQL += "AND COPY_STEP>=@COPY_STEP_START AND COPY_STEP<=@COPY_STEP_END ";


			#region optional params
			if (txtSenderIP.Text!="ALL")
			{
				sWhereSQL +="AND ORIGINAL_SENDER_TCP_IP_ADDRESS LIKE @O_SENDER_TCP_IP_ADDRESS " ;
				sParam = new SqlParameter("@O_SENDER_TCP_IP_ADDRESS",SqlDbType.NVarChar);
				sParam.Value=txtSenderIP.Text + "%";

				sqlReportCmd.Parameters.Add(sParam);
			}

			if (txtBeginTime.Text!="ALL")
			{
				sWhereSQL +="AND CREATION_TIME >= @O_ROW_CREATED_TIME_START " ;
				sParam = new SqlParameter("@O_ROW_CREATED_TIME_START",SqlDbType.DateTime);
				sParam.Value= DateTime.Parse(txtBeginTime.Text) ;

				sqlReportCmd.Parameters.Add(sParam);
			}
			#endregion 

			sWhereSQL +="AND WIL.COMPLETION_TIME IS NOT NULL ORDER BY WIL.CREATION_TIME, WIL.SAFETIR_MESSAGE_IN_ID, COPY_STEP ";

			#region mandatory params

			
			sParam = new SqlParameter("@COPY_STEP_START",SqlDbType.Int);
			sParam.Value= ((StepCodeDescriptionMap)cmbStep.SelectedItem).Step;
			sqlReportCmd.Parameters.Add(sParam);

			sParam = new SqlParameter("@COPY_STEP_END",SqlDbType.Int);
			sParam.Value= ((StepCodeDescriptionMap)cmbEndStep.SelectedItem).Step;
			sqlReportCmd.Parameters.Add(sParam);			
			
			sParam = new SqlParameter("@SUBSCRIBER_ID",SqlDbType.NVarChar);
			sParam.Value=cmbSubscribers.Text;

			sqlReportCmd.Parameters.Add(sParam);			
			
			
			sParam = new SqlParameter("@ROW_CREATED_TIME_END",SqlDbType.DateTime);
				
			if (txtEndTime.Text=="NOW")
			{
				sParam.Value=DateTime.Now;
			}
			else
			{
				sParam.Value=DateTime.Parse(txtEndTime.Text);
			}
				
			sqlReportCmd.Parameters.Add(sParam);
			
			
			#endregion


			//get rid of leading and
			sWhereSQL= sWhereSQL.Substring(4);

			sqlReportCmd.CommandText=  sReportSQL + sWhereSQL;

			return sqlReportCmd;
		
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}



		
		
	}
	


	
}
