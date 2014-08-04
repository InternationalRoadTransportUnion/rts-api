using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for ExportSubsDB.
	/// </summary>
	public class ExportSubsDB : System.Windows.Forms.Form
	{
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ProgressBar prgBar;
		private System.Windows.Forms.Label lblProgressStatus;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rdAllRecords;
		private System.Windows.Forms.RadioButton rdDateRange;
		private System.Windows.Forms.DateTimePicker dtPicker;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExportSubsDB()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			#region validate

			txtFilePath.Text=txtFilePath.Text.Trim();

			if (txtFilePath.Text=="")
			{
				errorProvider1.SetError(btnBrowse,"Select a file path to export");
			}
			errorProvider1.SetError(btnBrowse,"");
		

		
			#endregion
		
			
			try
			{
				this.Cursor=Cursors.WaitCursor;				 
				ExportSubsDBToFile();
			}
			catch (Exception exx)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message , "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			finally
			{
			this.Cursor=Cursors.Default;	
			}
		
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
			this.sfd1 = new System.Windows.Forms.SaveFileDialog();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dtPicker = new System.Windows.Forms.DateTimePicker();
			this.rdDateRange = new System.Windows.Forms.RadioButton();
			this.rdAllRecords = new System.Windows.Forms.RadioButton();
			this.lblProgressStatus = new System.Windows.Forms.Label();
			this.prgBar = new System.Windows.Forms.ProgressBar();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// sfd1
			// 
			this.sfd1.DefaultExt = "xml";
			this.sfd1.Filter = "XML Files|*.xml";
			this.sfd1.Title = "Select Path to export Subscirber DB XML";
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(104, 24);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.ReadOnly = true;
			this.txtFilePath.Size = new System.Drawing.Size(384, 20);
			this.txtFilePath.TabIndex = 0;
			this.txtFilePath.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Export Path";
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(432, 152);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(128, 24);
			this.btnExport.TabIndex = 2;
			this.btnExport.Text = "Export";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(16, 224);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(504, 24);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(40, 24);
			this.btnBrowse.TabIndex = 4;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.lblProgressStatus);
			this.groupBox1.Controls.Add(this.prgBar);
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtFilePath);
			this.groupBox1.Controls.Add(this.btnExport);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(576, 208);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dtPicker);
			this.groupBox2.Controls.Add(this.rdDateRange);
			this.groupBox2.Controls.Add(this.rdAllRecords);
			this.groupBox2.Location = new System.Drawing.Point(16, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(544, 72);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Record Fillter";
			// 
			// dtPicker
			// 
			this.dtPicker.CustomFormat = "yyyy-MM-dd";
			this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtPicker.Location = new System.Drawing.Point(168, 40);
			this.dtPicker.Name = "dtPicker";
			this.dtPicker.Size = new System.Drawing.Size(120, 20);
			this.dtPicker.TabIndex = 2;
			// 
			// rdDateRange
			// 
			this.rdDateRange.Location = new System.Drawing.Point(24, 40);
			this.rdDateRange.Name = "rdDateRange";
			this.rdDateRange.Size = new System.Drawing.Size(136, 24);
			this.rdDateRange.TabIndex = 1;
			this.rdDateRange.Text = "Changed Since Date";
			this.rdDateRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rdDateRange.CheckedChanged += new System.EventHandler(this.rdDateRange_CheckedChanged);
			// 
			// rdAllRecords
			// 
			this.rdAllRecords.Location = new System.Drawing.Point(24, 16);
			this.rdAllRecords.Name = "rdAllRecords";
			this.rdAllRecords.Size = new System.Drawing.Size(88, 16);
			this.rdAllRecords.TabIndex = 0;
			this.rdAllRecords.Text = "All Records";
			this.rdAllRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rdAllRecords.CheckedChanged += new System.EventHandler(this.rdDateRange_CheckedChanged);
			// 
			// lblProgressStatus
			// 
			this.lblProgressStatus.Location = new System.Drawing.Point(24, 176);
			this.lblProgressStatus.Name = "lblProgressStatus";
			this.lblProgressStatus.Size = new System.Drawing.Size(384, 16);
			this.lblProgressStatus.TabIndex = 6;
			// 
			// prgBar
			// 
			this.prgBar.Location = new System.Drawing.Point(16, 152);
			this.prgBar.Name = "prgBar";
			this.prgBar.Size = new System.Drawing.Size(384, 16);
			this.prgBar.TabIndex = 5;
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ExportSubsDB
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 262);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ExportSubsDB";
			this.Text = "ExportSubsDB";
			this.Load += new System.EventHandler(this.ExportSubsDB_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = sfd1.ShowDialog();
			if (dr==DialogResult.Cancel)
				return;
			txtFilePath.Text=sfd1.FileName;
			btnExport.Enabled=true;
			
			prgBar.Value=0;

			

			

			
			
		}

		private void ExportSubsDBToFile()
		{
			CommonDBHelper dbSubs = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
			prgBar.Value=0;
			lblProgressStatus.Text="";

            string[] aTableArray = new string[] { "dbo.WS_SUBSCRIBER", "dbo.WS_SUBSCRIBER_SERVICE_METHOD", "dbo.WS_SUBSCRIBER_SERVICES", "dbo.COPY_TO_URLS", "dbo.IRU_ENCRYPTION_KEYS", "dbo.RTS_RIGHTS_MASTER", "dbo.RTS_USER", "dbo.RTS_USER_RIGHTS", "dbo.SUBSCRIBER_ENCRYPTION_KEYS" };

			DataSet dsSubs = new DataSet("SubscriberDB" + DateTime.Now.ToString());
			int nExtractCount=0;
			
			//ignores the time part and sets it to 00:00hrs
			DateTime dtFromTime = DateTime.Parse(dtPicker.Text);


			foreach (string aTable in aTableArray)
			{
				dbSubs.ConnectToDB();
				SqlCommand sTableSelectCmd = new SqlCommand();
				if (rdAllRecords.Checked==true)
				{
					sTableSelectCmd.CommandText="SELECT * from " + aTable;
				}
				else
				{
				
					sTableSelectCmd.CommandText="SELECT * from " + aTable + " WHERE LAST_UPDATE_TIME >= @LAST_UPDATE_TIME";
					SqlParameter sParam = new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime);
					sParam.Value=dtFromTime;
					sTableSelectCmd.Parameters.Add(sParam);
				}
				sTableSelectCmd.CommandType=CommandType.Text;

				try
				{
					dbSubs.FillDataSetTableWithSchema(sTableSelectCmd,dsSubs,aTable);
				}	
				finally
				{
					dbSubs.Close();
				}
				lblProgressStatus.Text="Extracted Table " + aTable;
				nExtractCount++;
				prgBar.Value=prgBar.Maximum/aTableArray.Length * nExtractCount;
				Application.DoEvents();
			}

			if (File.Exists(txtFilePath.Text))
			{
				File.Delete(txtFilePath.Text);	
			
			}

				lblProgressStatus.Text="Exporting File";
				dsSubs.WriteXml(txtFilePath.Text,XmlWriteMode.WriteSchema);	
				lblProgressStatus.Text="Export Complete";

			MessageBox.Show("Export completed", "Admin Client");
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void rdDateRange_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdDateRange.Checked==true) 
			{
				dtPicker.Enabled=true;
			}
			else
			{
				dtPicker.Enabled=false;
			}

		}

		private void ExportSubsDB_Load(object sender, System.EventArgs e)
		{
			rdAllRecords.Checked=true;
			dtPicker.MaxDate=DateTime.Now;
			btnExport.Enabled=false;

		}

		
	}
}
