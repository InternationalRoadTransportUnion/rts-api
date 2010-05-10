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
	public class DiagnosticReports : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpService;
		private System.Windows.Forms.Button btnSvcSelect;
		private System.Windows.Forms.ComboBox cmbServices;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox gpRepParameters;
		private System.Windows.Forms.Button btnExecuteReport;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSenderMessageID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBeginTime;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtEndTime;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSlowTransThreshold;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cmbStep;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtSenderIP;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ComboBox cmbStepResults;
		private System.Windows.Forms.ComboBox cmbSubscribers;
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.TextBox txtMessageID;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ListView lstReport;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DiagnosticReports()
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
			this.cmbSubscribers = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cmbStep = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSenderMessageID = new System.Windows.Forms.TextBox();
			this.txtBeginTime = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtEndTime = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtSlowTransThreshold = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbStepResults = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.txtSenderIP = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.txtMessageID = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.btnExecuteReport = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.sfd1 = new System.Windows.Forms.SaveFileDialog();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.lstReport = new System.Windows.Forms.ListView();
			this.gpService.SuspendLayout();
			this.gpRepParameters.SuspendLayout();
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
			this.gpRepParameters.Controls.Add(this.cmbSubscribers);
			this.gpRepParameters.Controls.Add(this.label10);
			this.gpRepParameters.Controls.Add(this.cmbStep);
			this.gpRepParameters.Controls.Add(this.label9);
			this.gpRepParameters.Controls.Add(this.label7);
			this.gpRepParameters.Controls.Add(this.label5);
			this.gpRepParameters.Controls.Add(this.label1);
			this.gpRepParameters.Controls.Add(this.txtSenderMessageID);
			this.gpRepParameters.Controls.Add(this.txtBeginTime);
			this.gpRepParameters.Controls.Add(this.label2);
			this.gpRepParameters.Controls.Add(this.label3);
			this.gpRepParameters.Controls.Add(this.txtEndTime);
			this.gpRepParameters.Controls.Add(this.label4);
			this.gpRepParameters.Controls.Add(this.txtSlowTransThreshold);
			this.gpRepParameters.Controls.Add(this.label8);
			this.gpRepParameters.Controls.Add(this.cmbStepResults);
			this.gpRepParameters.Controls.Add(this.label11);
			this.gpRepParameters.Controls.Add(this.label12);
			this.gpRepParameters.Controls.Add(this.txtSenderIP);
			this.gpRepParameters.Controls.Add(this.label13);
			this.gpRepParameters.Controls.Add(this.txtMessageID);
			this.gpRepParameters.Controls.Add(this.label14);
			this.gpRepParameters.Location = new System.Drawing.Point(8, 64);
			this.gpRepParameters.Name = "gpRepParameters";
			this.gpRepParameters.Size = new System.Drawing.Size(608, 296);
			this.gpRepParameters.TabIndex = 1;
			this.gpRepParameters.TabStop = false;
			this.gpRepParameters.Text = "Report Parameters";
			// 
			// cmbSubscribers
			// 
			this.cmbSubscribers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSubscribers.Location = new System.Drawing.Point(144, 266);
			this.cmbSubscribers.Name = "cmbSubscribers";
			this.cmbSubscribers.Size = new System.Drawing.Size(192, 21);
			this.cmbSubscribers.TabIndex = 8;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 181);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 18);
			this.label10.TabIndex = 6;
			this.label10.Text = "Step";
			// 
			// cmbStep
			// 
			this.cmbStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStep.Location = new System.Drawing.Point(144, 177);
			this.cmbStep.Name = "cmbStep";
			this.cmbStep.Size = new System.Drawing.Size(184, 21);
			this.cmbStep.TabIndex = 5;
			this.cmbStep.SelectedIndexChanged += new System.EventHandler(this.cmbStep_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(376, 96);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 4;
			this.label9.Text = "Note:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(376, 168);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(208, 16);
			this.label7.TabIndex = 3;
			this.label7.Text = "Use value \"ALL\" for WildCard Search";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(376, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(208, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Enter time in YYYY-MM-DD 24hh:mm:ss format.";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Sender Message ID ";
			// 
			// txtSenderMessageID
			// 
			this.txtSenderMessageID.Location = new System.Drawing.Point(144, 61);
			this.txtSenderMessageID.MaxLength = 255;
			this.txtSenderMessageID.Name = "txtSenderMessageID";
			this.txtSenderMessageID.Size = new System.Drawing.Size(184, 20);
			this.txtSenderMessageID.TabIndex = 1;
			this.txtSenderMessageID.Text = "ALL";
			// 
			// txtBeginTime
			// 
			this.txtBeginTime.Location = new System.Drawing.Point(144, 90);
			this.txtBeginTime.MaxLength = 20;
			this.txtBeginTime.Name = "txtBeginTime";
			this.txtBeginTime.Size = new System.Drawing.Size(184, 20);
			this.txtBeginTime.TabIndex = 2;
			this.txtBeginTime.Text = "ALL";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 19);
			this.label2.TabIndex = 1;
			this.label2.Text = "Begin Time ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 24);
			this.label3.TabIndex = 1;
			this.label3.Text = "End Time ";
			// 
			// txtEndTime
			// 
			this.txtEndTime.Location = new System.Drawing.Point(144, 119);
			this.txtEndTime.MaxLength = 20;
			this.txtEndTime.Name = "txtEndTime";
			this.txtEndTime.Size = new System.Drawing.Size(184, 20);
			this.txtEndTime.TabIndex = 3;
			this.txtEndTime.Text = "NOW";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 146);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 24);
			this.label4.TabIndex = 1;
			this.label4.Text = "Slow Transaction Threshhold (secs)";
			// 
			// txtSlowTransThreshold
			// 
			this.txtSlowTransThreshold.Location = new System.Drawing.Point(144, 148);
			this.txtSlowTransThreshold.MaxLength = 5;
			this.txtSlowTransThreshold.Name = "txtSlowTransThreshold";
			this.txtSlowTransThreshold.Size = new System.Drawing.Size(184, 20);
			this.txtSlowTransThreshold.TabIndex = 4;
			this.txtSlowTransThreshold.Text = "0";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(376, 144);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(216, 16);
			this.label8.TabIndex = 3;
			this.label8.Text = "Use value \"NOW\"  to refer to current time";
			// 
			// cmbStepResults
			// 
			this.cmbStepResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStepResults.Location = new System.Drawing.Point(144, 207);
			this.cmbStepResults.Name = "cmbStepResults";
			this.cmbStepResults.Size = new System.Drawing.Size(184, 21);
			this.cmbStepResults.TabIndex = 6;
			this.cmbStepResults.SelectedIndexChanged += new System.EventHandler(this.cmbStepResults_SelectedIndexChanged);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 212);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 24);
			this.label11.TabIndex = 6;
			this.label11.Text = "Step Result";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 270);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(128, 16);
			this.label12.TabIndex = 1;
			this.label12.Text = "SubscriberID";
			// 
			// txtSenderIP
			// 
			this.txtSenderIP.Location = new System.Drawing.Point(144, 237);
			this.txtSenderIP.MaxLength = 15;
			this.txtSenderIP.Name = "txtSenderIP";
			this.txtSenderIP.Size = new System.Drawing.Size(184, 20);
			this.txtSenderIP.TabIndex = 7;
			this.txtSenderIP.Text = "ALL";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 241);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128, 14);
			this.label13.TabIndex = 1;
			this.label13.Text = "Sender IP Address";
			// 
			// txtMessageID
			// 
			this.txtMessageID.Location = new System.Drawing.Point(144, 32);
			this.txtMessageID.MaxLength = 20;
			this.txtMessageID.Name = "txtMessageID";
			this.txtMessageID.Size = new System.Drawing.Size(184, 20);
			this.txtMessageID.TabIndex = 0;
			this.txtMessageID.Text = "ALL";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 34);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(128, 16);
			this.label14.TabIndex = 1;
			this.label14.Text = "Message ID ";
			// 
			// btnExecuteReport
			// 
			this.btnExecuteReport.Location = new System.Drawing.Point(496, 368);
			this.btnExecuteReport.Name = "btnExecuteReport";
			this.btnExecuteReport.Size = new System.Drawing.Size(120, 24);
			this.btnExecuteReport.TabIndex = 2;
			this.btnExecuteReport.Text = "Execute Report";
			this.btnExecuteReport.Click += new System.EventHandler(this.btnExecuteReport_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(8, 368);
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
			// lstReport
			// 
			this.lstReport.Location = new System.Drawing.Point(16, 400);
			this.lstReport.Name = "lstReport";
			this.lstReport.Size = new System.Drawing.Size(600, 160);
			this.lstReport.TabIndex = 6;
			this.lstReport.View = System.Windows.Forms.View.Details;
			// 
			// DiagnosticReports
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 599);
			this.Controls.Add(this.lstReport);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnExecuteReport);
			this.Controls.Add(this.gpRepParameters);
			this.Controls.Add(this.gpService);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "DiagnosticReports";
			this.Text = "DiagnosticReports";
			this.Load += new System.EventHandler(this.DiagnosticReports_Load);
			this.gpService.ResumeLayout(false);
			this.gpRepParameters.ResumeLayout(false);
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
				CmbServices.Items.Add("WSST");
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

		string m_StepDefinationTable;
		string m_StepDefinationTableSQL;
		
		string m_StepResultsDefinitionTable;
		string m_StepResultsDefinitionSQL;

	
		CommonDBHelper m_dbHelper ;

		private void btnSvcSelect_Click(object sender, System.EventArgs e)
		{
			cmbStep.Items.Clear();
			cmbStepResults.Items.Clear();
			
			#region Setcontrols

			
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

			
			cmbStep.Items.Clear();
			cmbStepResults.Items.Clear();
			#endregion


			#region fill up combos
			if((ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan") &&  (cmbServices.Text=="WSST"))
			{


		
				cmbStep.Items.Clear();
				cmbStep.Items.Add(new StepCodeDescriptionMap(-1,"ALL"));
				

				cmbStepResults.Items.Clear();
				cmbStepResults.Items.Add(new StepCodeDescriptionMap(-1,"ALL"));	
				cmbStepResults.Items.Add(new StepCodeDescriptionMap(2,"OK"));
				cmbStepResults.Items.Add(new StepCodeDescriptionMap(1200,"Error"));
				cmbStepResults.SelectedIndex=0;

				//threshold has no meaning

				txtSlowTransThreshold.Text="0";
				txtSlowTransThreshold.Enabled=false;

				gpRepParameters.Enabled=true;
				btnExecuteReport.Enabled=true;


				if(cmbStep.Items.Count > 0)
				{
					cmbStep.SelectedIndex = 0; 
				}
				cmbStepResults.SelectedIndex = 0;

				return;			
			}

			//enable could be disabled for WSST external
			txtSlowTransThreshold.Enabled=true;


			#region Fill Step combo
			string sStepSelect = "SELECT STEP, STEP_DESCRIPTION FROM STEP_DEFINITIONS ORDER BY STEP";

			SqlCommand sSQLSearchCmd = new SqlCommand();

			sSQLSearchCmd.CommandText=sStepSelect;
			
			IDataReader sdr=null;
			
			try
			{
			
				m_dbHelper.ConnectToDB();

				sdr = m_dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				cmbStep.Items.Add(new StepCodeDescriptionMap(-1,"ALL"));
				while(sdr.Read())
				{
					cmbStep.Items.Add(new StepCodeDescriptionMap(sdr.GetInt32(0),sdr.GetString(1)));
				}
	
			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + ex.Message + "\r\n Error At:" + ex.Source, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
				if (sdr!=null) sdr.Close();
				m_dbHelper.Close();

				//SMD 2006-12-13
				if(cmbStep.Items.Count > 0)
				{
					cmbStep.SelectedIndex = 0; 
				}
				cmbStepResults.SelectedIndex = 0;
				//======================================

			}
			#endregion

			
			#endregion

			gpRepParameters.Enabled=true;
			btnExecuteReport.Enabled=true;

		}

		private void FillStepResultCombo()
		{
		
			#region Fill step result
			string sStepREsultSelect = "SELECT  STEP_RESULT , STEP_RESULT_DESCRIPTION FROM STEP_RESULT_DEFINITIONS WHERE STEP=@STEP";

			SqlCommand sSQLSearchCmd = new SqlCommand();

			SqlParameter sParam = new SqlParameter("@STEP",SqlDbType.Int);
			sParam.Value= ((StepCodeDescriptionMap)cmbStep.SelectedItem).Step;
			sSQLSearchCmd.Parameters.Add(sParam);

			sSQLSearchCmd.CommandText=sStepREsultSelect;
			
			IDataReader sdr=null;
			
			try
			{
			
				m_dbHelper.ConnectToDB();

				sdr = m_dbHelper.GetDataReader(sSQLSearchCmd,CommandBehavior.SingleResult);
				cmbStepResults.Items.Add(new StepCodeDescriptionMap(-1,"ALL"));
				while(sdr.Read())
				{
					cmbStepResults.Items.Add(new StepCodeDescriptionMap(sdr.GetInt32(0),sdr.GetString(1)));
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
				m_dbHelper.Close();
			}




			#endregion


		}

		private void cmbStep_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cmbStepResults.Items.Clear();
			if (cmbStep.Text=="ALL")
			{
				
				cmbStepResults.Items.Add(new StepCodeDescriptionMap(-1,"ALL"));				return;
			}
			
			FillStepResultCombo();
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

		private void btnExecuteReport_Click(object sender, System.EventArgs e)
		{
			#region Validations


			if (txtMessageID.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtMessageID,"Mandatory Field, Use \"ALL\" for wildcard search.");
				txtMessageID.Focus();
				return;
			}
			
			errorProvider1.SetError(txtMessageID,"");
			
			if (txtSenderMessageID.Text.Trim() =="") 
			{
				errorProvider1.SetError(txtSenderMessageID,"Mandatory Field, Use \"ALL\" for wildcard search.");
				txtSenderMessageID.Focus();
				return;
			}
			
			errorProvider1.SetError(txtSenderMessageID,"");

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
					DateTime.Parse(txtBeginTime.Text);
				}
				catch (Exception exx)
				{
								errorProvider1.SetError(txtBeginTime,"Enter time in correct format");
					txtBeginTime.Text="";
					return;
				}
				
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
					DateTime.Parse(txtEndTime.Text);
				}
				catch (Exception exx)
				{
								errorProvider1.SetError(txtEndTime,"Enter time in correct format");
					txtEndTime.Text="";
					return;
				}
				
			}

			errorProvider1.SetError(txtEndTime,"");
			#endregion 



			if (cmbStep.Text.Trim() =="") 
			{
				errorProvider1.SetError(cmbStep,"Mandatory Field");
				cmbStep.Focus();
				return;
			}
			
				errorProvider1.SetError(cmbStep,"");
			

			if (cmbStepResults.Text.Trim() =="") 
			{
				errorProvider1.SetError(cmbStepResults,"Mandatory Field");
				cmbStep.Focus();
				return;
			}
			
			errorProvider1.SetError(cmbStepResults,"");
			

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
					if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
					{
						//there is no sequence table
						sRepCommand= PrepareWSSTExternalQuery();
					}
					else
					{
					
						sRepCommand= PrepareWSSTInternalQuery();
					}
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
			IDataReader repReader=null; 
			IDataReader repReaderCounter=null; 
			try
			{
				m_dbHelper.ConnectToDB();


				//SMD 2006-12-13
				repReaderCounter =  m_dbHelper.GetDataReader(sRepCommand, CommandBehavior.SingleResult );

				int iTotalNoOFRecs = 0;

				while(repReaderCounter.Read())
				{
					iTotalNoOFRecs++;
				}

				if(iTotalNoOFRecs > 0)
				{
					if(MessageBox.Show("Total rows in the report : "+iTotalNoOFRecs.ToString()+" \n Do you want to continue ?","Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)== DialogResult.No)
					{
						if (repReaderCounter!=null)
						{
							repReaderCounter.Close();
						}
						return;
					}
				}
				else
				{
					MessageBox.Show(this, "No Data matching your criteria.", "Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				if (repReaderCounter!=null)
				{
					repReaderCounter.Close();
				}

				//===========================================================================================================

				repReader =  m_dbHelper.GetDataReader(sRepCommand, CommandBehavior.SingleResult );

				bool bReadResult= repReader.Read();
				
				
				int nRowCounter = 0;
				#region DumpHeader

				//SMD 2006-12-13
				lstReport.Items.Clear();
				lstReport.Columns.Clear();
				//=============================

				for (int nFieldCounter=0; nFieldCounter< repReader.FieldCount; nFieldCounter++)
				{
					sRepWriter.Write(repReader.GetName(nFieldCounter));

					//SMD 2006-12-13
					lstReport.Columns.Add(repReader.GetName(nFieldCounter),50,HorizontalAlignment.Left); 
					//=======================================================

					if (nFieldCounter<repReader.FieldCount-1)
					{
						sRepWriter.Write(",");
					}
					
				}

				sRepWriter.Write("\r\n");
				#endregion
				
				if (bReadResult==true)
				{
					do
					{
						#region DumpRows

						//SMD 2006-12-13
						ListViewItem itm = null;
						if(repReader.FieldCount>0)
						{
							 itm = new ListViewItem(repReader[0].ToString());
						}
						//=======================================================

						for (int nFieldCounter=0; nFieldCounter< repReader.FieldCount; nFieldCounter++)
						{
							sRepWriter.Write(repReader[nFieldCounter]);
							//SMD 2006-12-13
							if(nFieldCounter>0)
							{
								itm.SubItems.Add(repReader[nFieldCounter].ToString());
							}
							//=======================================================

							if (nFieldCounter<repReader.FieldCount-1)
							{
								sRepWriter.Write(",");

							}
					
						}

						//SMD 2006-12-13
						lstReport.Items.Add(itm);
						//=======================================================

						sRepWriter.Write("\r\n");

						nRowCounter++;
						#endregion
					}while ( repReader.Read());
				}
				else
				{
					MessageBox.Show(this, "No Data matching your criteria.", "Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				MessageBox.Show(this, nRowCounter.ToString()+  " rows exported to file. " + sfd1.FileName, "Admin Client",MessageBoxButtons.OK,MessageBoxIcon.Information);
				


			}
			catch (SqlException exSQL)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			
			}
			finally
			{
				sRepWriter.Flush();
				sRepWriter.Close();
				if (repReader!=null)
				{
					repReader.Close();
				}
				m_dbHelper.Close();
			}


			#endregion

		}


		
		SqlCommand PrepareTCHQQuery()
		{
			
			SqlCommand sqlReportCmd = new SqlCommand();

			SqlParameter sParam;
			string sWhereSQL="";

			string sReportSQL=
				"SELECT WIL.TCHQ_QUERY_ID,  WIL.SENDER_QUERY_ID, TCHQ_STEP, WSEQ.TCHQ_STEP_RESULT, "+
				"REPLACE(CONVERT(NVARCHAR(4000),WSEQ.TCHQ_STEP_ERROR_DESC),',','_') as TCHQ_STEP_ERROR_DESC, CONVERT(nvarchar(19),WSEQ.LAST_UPDATE_TIME ,120) AS LAST_UPDATE_TIME FROM     " + 
				"TCHQ_REQUEST_LOG WIL WITH (NOLOCK) INNER JOIN TCHQ_SEQUENCE WSEQ  WITH (NOLOCK) ON  WIL.TCHQ_QUERY_ID = WSEQ.TCHQ_QUERY_ID  " + 
				"WHERE " ;
	
			#region subquery
			if (cmbStepResults.Text!="ALL") //if specific step result is not expected then no need to fileter the sequence table
			{
				if (cmbStep.Text !="ALL")
				{
					sWhereSQL +="AND WIL.TCHQ_QUERY_ID IN (SELECT TCHQ_QUERY_ID FROM TCHQ_SEQUENCE  WITH (NOLOCK) WHERE  " + " TCHQ_STEP=@O_TCHQ_STEP " ;

	
					sParam = new SqlParameter("@O_TCHQ_STEP",SqlDbType.Int);
					sParam.Value= ((StepCodeDescriptionMap) (cmbStep.SelectedItem)).Step;
					sqlReportCmd.Parameters.Add(sParam);

					if (cmbStepResults.Text!="ALL")
					{
						sWhereSQL +="AND TCHQ_STEP_RESULT=@O_TCHQ_STEP_RESULT " ;
							
						sParam = new SqlParameter("@O_TCHQ_STEP_RESULT",SqlDbType.Int);
						sParam.Value=  ((StepCodeDescriptionMap) (cmbStepResults.SelectedItem)).Step;
						sqlReportCmd.Parameters.Add(sParam);
					
					}
					if (txtMessageID.Text!="ALL")
					{
						sWhereSQL +="AND TCHQ_QUERY_ID = @O_TCHQ_QUERY_ID " ;
						sParam = new SqlParameter("@O_TCHQ_QUERY_ID",SqlDbType.Int);
						sParam.Value=int.Parse( txtMessageID.Text );
						sqlReportCmd.Parameters.Add(sParam);
					}

					sWhereSQL +=")"; //close sub query
				}

				
			}
			#endregion

			if (txtMessageID.Text!="ALL")
			{
				sWhereSQL +="AND WIL.TCHQ_QUERY_ID = @O_TCHQ_QUERY_ID_2 " ;
				sParam = new SqlParameter("@O_TCHQ_QUERY_ID_2",SqlDbType.Int);
				sParam.Value=int.Parse( txtMessageID.Text );
				sqlReportCmd.Parameters.Add(sParam);
			}


			sWhereSQL += "AND DATEDIFF(s,ROW_CREATED_TIME,COMPLETION_TIME)>=@THRESHOLD " +
				" AND SENDER_ID = @SENDER_ID " +

				"AND ROW_CREATED_TIME<=@ROW_CREATED_TIME_END " ;

			#region optional params

			if (txtSenderMessageID.Text!="ALL")
			{
				sWhereSQL +="AND WIL.SENDER_QUERY_ID = @O_SENDER_MESSAGEID_2 " ;
				sParam = new SqlParameter("@O_SENDER_MESSAGEID_2",SqlDbType.NVarChar);
				sParam.Value=txtSenderMessageID.Text ;
				sqlReportCmd.Parameters.Add(sParam);
			}

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

			sWhereSQL +="ORDER BY WIL.TCHQ_QUERY_ID, WSEQ.TCHQ_STEP ";

			#region mandatory params

			sParam = new SqlParameter("@THRESHOLD",SqlDbType.Int);
			sParam.Value=int.Parse( txtSlowTransThreshold.Text);

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

				SqlParameter sParam;
				string sWhereSQL="";

			string sReportSQL=
				"SELECT WIL.SAFETIR_MESSAGE_IN_ID,  WIL.SENDER_MESSAGE_ID, WSEQ.WSST_STEP, WSEQ.WSST_STEP_RESULT, "+
				"REPLACE(CONVERT(NVARCHAR(4000),WSEQ.WSST_STEP_ERROR_DESCRIPTION),',','_') as WSST_STEP_ERROR_DESCRIPTION, " +
				"CONVERT(nvarchar(19),WSEQ.LAST_UPDATE_TIME ,120) AS LAST_UPDATE_TIME FROM " + 
				"WSST_INTERNAL_LOG WIL WITH (NOLOCK) INNER JOIN WSST_SEQUENCE WSEQ  WITH (NOLOCK) ON  "+
				"WIL.SAFETIR_MESSAGE_IN_ID = WSEQ.SAFETIR_MESSAGE_IN_ID WHERE " ;
	
				#region subquery

				if (cmbStepResults.Text!="ALL") //if specific step result is not expected then no need to fileter the sequence table
				{
					if (cmbStep.Text !="ALL")
					{
						sWhereSQL +="AND WIL.SAFETIR_MESSAGE_IN_ID IN (SELECT SAFETIR_MESSAGE_IN_ID FROM WSST_SEQUENCE  WITH (NOLOCK) WHERE  " + " WSST_STEP=@O_WSST_STEP " ;

	
						sParam = new SqlParameter("@O_WSST_STEP",SqlDbType.Int);
						sParam.Value= ((StepCodeDescriptionMap) (cmbStep.SelectedItem)).Step;
						sqlReportCmd.Parameters.Add(sParam);

						if (cmbStepResults.Text!="ALL")
						{
							sWhereSQL +="AND WSST_STEP_RESULT=@O_WSST_STEP_RESULT " ;
							
							sParam = new SqlParameter("@O_WSST_STEP_RESULT",SqlDbType.Int);
							sParam.Value=  ((StepCodeDescriptionMap) (cmbStepResults.SelectedItem)).Step;
							sqlReportCmd.Parameters.Add(sParam);
					
						}
						if (txtMessageID.Text!="ALL")
						{
							sWhereSQL +="AND SAFETIR_MESSAGE_IN_ID = @O_SAFETIR_MESSAGE_IN_ID " ;
							sParam = new SqlParameter("@O_SAFETIR_MESSAGE_IN_ID",SqlDbType.Int);
							sParam.Value=int.Parse( txtMessageID.Text );
							sqlReportCmd.Parameters.Add(sParam);
						}

						sWhereSQL +=")"; //close sub query
					}

				
				}
				#endregion

				if (txtSenderMessageID.Text!="ALL")
				{
					sWhereSQL +="AND WIL.SENDER_MESSAGE_ID = @O_SENDER_MESSAGE_ID_2 " ;
					sParam = new SqlParameter("@O_SENDER_MESSAGE_ID_2",SqlDbType.NVarChar);
					sParam.Value=txtSenderMessageID.Text ;
					sqlReportCmd.Parameters.Add(sParam);
				}

				if (txtMessageID.Text!="ALL")
				{
					sWhereSQL +="AND WIL.SAFETIR_MESSAGE_IN_ID = @O_SAFETIR_MESSAGE_IN_ID_2 " ;
					sParam = new SqlParameter("@O_SAFETIR_MESSAGE_IN_ID_2",SqlDbType.Int);
					sParam.Value=int.Parse( txtMessageID.Text );
					sqlReportCmd.Parameters.Add(sParam);
				}


				sWhereSQL += "AND DATEDIFF(s,ROW_CREATED_TIME,COMPLETION_TIME)>=@THRESHOLD " +
				" AND SUBSCRIBER_ID = @SUBSCRIBER_ID " +
				"AND ROW_CREATED_TIME<=@ROW_CREATED_TIME_END " ;

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

				sWhereSQL +="ORDER BY WIL.SAFETIR_MESSAGE_IN_ID, WSEQ.WSST_STEP ";

				#region mandatory params

				sParam = new SqlParameter("@THRESHOLD",SqlDbType.Int);
				sParam.Value=int.Parse( txtSlowTransThreshold.Text);

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



			
		SqlCommand PrepareWSSTExternalQuery()
		{
			
			SqlCommand sqlReportCmd = new SqlCommand();

			SqlParameter sParam;
			string sWhereSQL="";

			string sReportSQL=
				"SELECT      WIL.SAFETIR_MESSAGE_IN_ID,  WIL.SENDER_MESSAGEID, RETURN_CODE, REPLACE(CONVERT(NVARCHAR(4000),RETURN_CODE_DESCRIPTION),',','_') as RETURN_CODE_DESCRIPTION, CONVERT(nvarchar(19),ROW_CREATED_TIME ,120) AS ROW_CREATED_TIME FROM     " + 
				"WSST_EXTERNAL_LOG WIL WITH (NOLOCK) " + 
				"WHERE " ;
	
			#region subquery

			if (cmbStepResults.Text!="ALL") //if specific step result is not expected then no need to fileter the sequence table
			{
				
					sWhereSQL +="AND RETURN_CODE =@O_WSST_STEP_RESULT " ;

					sParam = new SqlParameter("@O_WSST_STEP_RESULT",SqlDbType.Int);
					sParam.Value= ((StepCodeDescriptionMap) (cmbStepResults.SelectedItem)).Step;
					sqlReportCmd.Parameters.Add(sParam);
					
				

				
			}
			#endregion

			if (txtMessageID.Text!="ALL")
			{
				sWhereSQL +="AND WIL.SAFETIR_MESSAGE_IN_ID = @O_SAFETIR_MESSAGE_IN_ID_2 " ;
				sParam = new SqlParameter("@O_SAFETIR_MESSAGE_IN_ID_2",SqlDbType.Int);
				sParam.Value=int.Parse( txtMessageID.Text );
				sqlReportCmd.Parameters.Add(sParam);
			}


			sWhereSQL += "AND SUBSCRIBER_ID = @SUBSCRIBER_ID " +

				"AND ROW_CREATED_TIME<=@ROW_CREATED_TIME_END " ;

			#region optional params
			if (txtSenderMessageID.Text!="ALL")
			{
				sWhereSQL +="AND WIL.SENDER_MESSAGEID = @O_SENDER_MESSAGEID_2 " ;
				sParam = new SqlParameter("@O_SENDER_MESSAGEID_2",SqlDbType.NVarChar);
				sParam.Value=txtSenderMessageID.Text ;
				sqlReportCmd.Parameters.Add(sParam);
			}

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

			sWhereSQL +="ORDER BY WIL.SAFETIR_MESSAGE_IN_ID ";

			#region mandatory params

						
			
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

			SqlParameter sParam;
			string sWhereSQL="";

			string sReportSQL=
				"SELECT      WIL.SAFETIR_MESSAGE_IN_ID,  WSEQ.COPY_STEP,                      WSEQ.COPY_STEP_RESULT, REPLACE(CONVERT(NVARCHAR(4000),WSEQ.COPY_STEP_ERROR_DESCRIPTION),',','_') as COPY_STEP_ERROR_DESCRIPTION, CONVERT(nvarchar(19),WSEQ.LAST_UPDATE_TIME ,120) AS LAST_UPDATE_TIME FROM     " + 
				"WSST_COPY_TO_LOG WIL WITH (NOLOCK) INNER JOIN WSST_COPY_TO_SEQUENCE WSEQ  WITH (NOLOCK) ON  WIL.SAFETIR_MESSAGE_IN_ID = WSEQ.SAFETIR_MESSAGE_IN_ID  " + 
				"WHERE " ;
	
			#region subquery

			if (cmbStepResults.Text!="ALL") //if specific step result is not expected then no need to fileter the sequence table
			{
				if (cmbStep.Text !="ALL")
				{
					sWhereSQL +="AND WIL.SAFETIR_MESSAGE_IN_ID IN (SELECT SAFETIR_MESSAGE_IN_ID FROM WSST_COPY_TO_SEQUENCE  WITH (NOLOCK) WHERE  " + " COPY_STEP=@O_COPY_STEP " ;

	
					sParam = new SqlParameter("@O_COPY_STEP",SqlDbType.Int);
					sParam.Value= ((StepCodeDescriptionMap) (cmbStep.SelectedItem)).Step;
					sqlReportCmd.Parameters.Add(sParam);

					if (cmbStepResults.Text!="ALL")
					{
						sWhereSQL +="AND COPY_STEP_RESULT=@COPY_STEP_RESULT " ;
							
						sParam = new SqlParameter("@COPY_STEP_RESULT",SqlDbType.Int);
						sParam.Value=  ((StepCodeDescriptionMap) (cmbStepResults.SelectedItem)).Step;
						sqlReportCmd.Parameters.Add(sParam);
					
					}
					if (txtMessageID.Text!="ALL")
					{
						sWhereSQL +="AND SAFETIR_MESSAGE_IN_ID = @O_SAFETIR_MESSAGE_IN_ID " ;
						sParam = new SqlParameter("@O_SAFETIR_MESSAGE_IN_ID",SqlDbType.Int);
						sParam.Value=int.Parse( txtMessageID.Text );
						sqlReportCmd.Parameters.Add(sParam);
					}

					sWhereSQL +=")"; //close sub query
				}

				
			}
			#endregion

			if (txtMessageID.Text!="ALL")
			{
				sWhereSQL +="AND WIL.SAFETIR_MESSAGE_IN_ID = @O_SAFETIR_MESSAGE_IN_ID_2 " ;
				sParam = new SqlParameter("@O_SAFETIR_MESSAGE_IN_ID_2",SqlDbType.Int);
				sParam.Value=int.Parse( txtMessageID.Text );
				sqlReportCmd.Parameters.Add(sParam);
			}


			sWhereSQL += "AND DATEDIFF(s,CREATION_TIME,COMPLETION_TIME)>=@THRESHOLD " +
				" AND SUBSCRIBER_ID = @SUBSCRIBER_ID " +

				"AND CREATION_TIME<=@ROW_CREATED_TIME_END " ;

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

			sWhereSQL +="ORDER BY WIL.SAFETIR_MESSAGE_IN_ID, WSEQ.COPY_STEP ";

			#region mandatory params

			sParam = new SqlParameter("@THRESHOLD",SqlDbType.Int);
			sParam.Value=int.Parse( txtSlowTransThreshold.Text);

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
	

	class StepCodeDescriptionMap
	{
		public int Step;
		public string StepDescription;
		public StepCodeDescriptionMap(int StepParam, string DescParam)
		{
			Step = StepParam;
			StepDescription=DescParam;
		}
		public override string ToString()
		{
			if (Step!=-1)
				return Step.ToString() +'-'+ StepDescription;
			else
				return StepDescription;
				
		}
	}
	
}
