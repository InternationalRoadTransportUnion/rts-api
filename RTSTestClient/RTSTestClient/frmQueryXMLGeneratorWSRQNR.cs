using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for frmQueryXMLGenerator.
	/// </summary>
	public class frmQueryXMLGeneratorWSRQNR : System.Windows.Forms.Form
    {
        #region variables
        private IContainer components;

		private bool bTemplateFileMissing;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.SaveFileDialog sfd; 

		private DataSet dsXML;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.TextBox RequestID;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox CNL;
		private System.Windows.Forms.TextBox DCL;
        private System.Windows.Forms.TextBox ICC;
		private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.OpenFileDialog ofd;
        private Label label10;
        private TextBox COF;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label18;
        private Label label17;
        private TextBox PIC;
        private TextBox RBC;
        private TextBox COM;
        private TextBox VPN;
        private TextBox CWR;
        private TextBox PFD;
        private TextBox RND;
        private TextBox DDI;
        private TextBox RequestDate;
        private Label label1;
        private Label label4;
        private TextBox TNO;
        private int addrow;
        private TextBox RequestReminderNum;
        private Label label5;
        private TextBox RequestDataSource;
        private Label label3;
        private Label lblSave;
      

		private string m_LoadedFileName;
    #endregion variables

		public frmQueryXMLGeneratorWSRQNR()
		{
			
			// Required for Windows Form Designer support
			//
            addrow = 0;
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//Load the template file in DataSet
			
			bTemplateFileMissing=true;
			string sAppPath = Application.ExecutablePath;
			sAppPath= Path.GetDirectoryName(sAppPath);

            string sQueryTemplate = sAppPath + "\\" + "WSRQNRDataTemplate.req";

			if (!File.Exists(sQueryTemplate))
			{
                MessageBox.Show("WSRQNRDataTemplate.req file not found in the application folder \r\n: " + Application.StartupPath);
				bTemplateFileMissing=true;
			}
			menuItem1_Click(null,null);// call the new message click;
			
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.RequestReminderNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RequestDataSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RequestDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PIC = new System.Windows.Forms.TextBox();
            this.RBC = new System.Windows.Forms.TextBox();
            this.COM = new System.Windows.Forms.TextBox();
            this.VPN = new System.Windows.Forms.TextBox();
            this.CWR = new System.Windows.Forms.TextBox();
            this.PFD = new System.Windows.Forms.TextBox();
            this.RND = new System.Windows.Forms.TextBox();
            this.DDI = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.COF = new System.Windows.Forms.TextBox();
            this.RequestID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CNL = new System.Windows.Forms.TextBox();
            this.DCL = new System.Windows.Forms.TextBox();
            this.ICC = new System.Windows.Forms.TextBox();
            this.TNO = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.lblSave = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 380);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "-";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(458, 349);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(362, 349);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "New Query";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Load File";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.RequestReminderNum);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.RequestDataSource);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.RequestDate);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.PIC);
            this.pnlMain.Controls.Add(this.RBC);
            this.pnlMain.Controls.Add(this.COM);
            this.pnlMain.Controls.Add(this.VPN);
            this.pnlMain.Controls.Add(this.CWR);
            this.pnlMain.Controls.Add(this.PFD);
            this.pnlMain.Controls.Add(this.RND);
            this.pnlMain.Controls.Add(this.DDI);
            this.pnlMain.Controls.Add(this.label18);
            this.pnlMain.Controls.Add(this.label17);
            this.pnlMain.Controls.Add(this.label16);
            this.pnlMain.Controls.Add(this.label15);
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.label13);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.COF);
            this.pnlMain.Controls.Add(this.RequestID);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.CNL);
            this.pnlMain.Controls.Add(this.DCL);
            this.pnlMain.Controls.Add(this.ICC);
            this.pnlMain.Controls.Add(this.TNO);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(946, 314);
            this.pnlMain.TabIndex = 20;
            // 
            // RequestReminderNum
            // 
            this.RequestReminderNum.Location = new System.Drawing.Point(177, 111);
            this.RequestReminderNum.Name = "RequestReminderNum";
            this.RequestReminderNum.Size = new System.Drawing.Size(184, 20);
            this.RequestReminderNum.TabIndex = 60;
            this.RequestReminderNum.Text = "textbox";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 23);
            this.label5.TabIndex = 59;
            this.label5.Text = "RequestReminderNum";
            // 
            // RequestDataSource
            // 
            this.RequestDataSource.Location = new System.Drawing.Point(177, 81);
            this.RequestDataSource.Name = "RequestDataSource";
            this.RequestDataSource.Size = new System.Drawing.Size(184, 20);
            this.RequestDataSource.TabIndex = 58;
            this.RequestDataSource.Text = "textbox";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 23);
            this.label3.TabIndex = 57;
            this.label3.Text = "RequestDataSource";
            // 
            // RequestDate
            // 
            this.RequestDate.Location = new System.Drawing.Point(177, 47);
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.Size = new System.Drawing.Size(184, 20);
            this.RequestDate.TabIndex = 56;
            this.RequestDate.Text = "textbox";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 55;
            this.label1.Text = "RequestDate";
            // 
            // PIC
            // 
            this.PIC.Location = new System.Drawing.Point(565, 250);
            this.PIC.Name = "PIC";
            this.PIC.Size = new System.Drawing.Size(184, 20);
            this.PIC.TabIndex = 53;
            this.PIC.Text = "textBox1";
            // 
            // RBC
            // 
            this.RBC.Location = new System.Drawing.Point(565, 216);
            this.RBC.Name = "RBC";
            this.RBC.Size = new System.Drawing.Size(184, 20);
            this.RBC.TabIndex = 52;
            this.RBC.Text = "textBox1";
            // 
            // COM
            // 
            this.COM.Location = new System.Drawing.Point(565, 180);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(184, 20);
            this.COM.TabIndex = 51;
            this.COM.Text = "textBox1";
            // 
            // VPN
            // 
            this.VPN.Location = new System.Drawing.Point(565, 150);
            this.VPN.Name = "VPN";
            this.VPN.Size = new System.Drawing.Size(184, 20);
            this.VPN.TabIndex = 50;
            this.VPN.Text = "textBox1";
            // 
            // CWR
            // 
            this.CWR.Location = new System.Drawing.Point(565, 117);
            this.CWR.Name = "CWR";
            this.CWR.Size = new System.Drawing.Size(184, 20);
            this.CWR.TabIndex = 49;
            this.CWR.Text = "textBox1";
            // 
            // PFD
            // 
            this.PFD.Location = new System.Drawing.Point(565, 82);
            this.PFD.Name = "PFD";
            this.PFD.Size = new System.Drawing.Size(184, 20);
            this.PFD.TabIndex = 48;
            this.PFD.Text = "textBox1";
            // 
            // RND
            // 
            this.RND.Location = new System.Drawing.Point(565, 47);
            this.RND.Name = "RND";
            this.RND.Size = new System.Drawing.Size(184, 20);
            this.RND.TabIndex = 47;
            this.RND.Text = "textBox1";
            // 
            // DDI
            // 
            this.DDI.Location = new System.Drawing.Point(565, 13);
            this.DDI.Name = "DDI";
            this.DDI.Size = new System.Drawing.Size(184, 20);
            this.DDI.TabIndex = 46;
            this.DDI.Text = "textBox1";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(461, 256);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 16);
            this.label18.TabIndex = 44;
            this.label18.Text = "PIC";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(461, 219);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 16);
            this.label17.TabIndex = 43;
            this.label17.Text = "RBC";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(461, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 16);
            this.label16.TabIndex = 42;
            this.label16.Text = "VPN";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(461, 183);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 41;
            this.label15.Text = "COM";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(461, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 40;
            this.label14.Text = "CWR";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(461, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 39;
            this.label13.Text = "PFD";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(461, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 38;
            this.label12.Text = "RND";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(461, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 37;
            this.label11.Text = "DDI";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 287);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 36;
            this.label10.Text = "COF";
            // 
            // COF
            // 
            this.COF.Location = new System.Drawing.Point(177, 291);
            this.COF.Name = "COF";
            this.COF.Size = new System.Drawing.Size(184, 20);
            this.COF.TabIndex = 35;
            this.COF.Text = "textBox1";
            // 
            // RequestID
            // 
            this.RequestID.Location = new System.Drawing.Point(177, 12);
            this.RequestID.Name = "RequestID";
            this.RequestID.Size = new System.Drawing.Size(184, 20);
            this.RequestID.TabIndex = 1;
            this.RequestID.Text = "textBox1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 33;
            this.label8.Text = "CNL";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 19);
            this.label7.TabIndex = 32;
            this.label7.Text = "DCL";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 31;
            this.label6.Text = "ICC";
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label4.Location = new System.Drawing.Point(3, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 23);
            this.label4.TabIndex = 29;
            this.label4.Text = "TNO";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "RequestID";
            // 
            // CNL
            // 
            this.CNL.Location = new System.Drawing.Point(177, 256);
            this.CNL.Name = "CNL";
            this.CNL.Size = new System.Drawing.Size(184, 20);
            this.CNL.TabIndex = 7;
            this.CNL.Text = "textBox1";
            // 
            // DCL
            // 
            this.DCL.Location = new System.Drawing.Point(177, 216);
            this.DCL.Name = "DCL";
            this.DCL.Size = new System.Drawing.Size(184, 20);
            this.DCL.TabIndex = 6;
            this.DCL.Text = "textBox1";
            // 
            // ICC
            // 
            this.ICC.Location = new System.Drawing.Point(177, 180);
            this.ICC.Name = "ICC";
            this.ICC.Size = new System.Drawing.Size(184, 20);
            this.ICC.TabIndex = 5;
            this.ICC.Text = "textBox1";
            // 
            // TNO
            // 
            this.TNO.Location = new System.Drawing.Point(177, 146);
            this.TNO.Name = "TNO";
            this.TNO.Size = new System.Drawing.Size(184, 20);
            this.TNO.TabIndex = 3;
            this.TNO.Text = "textBox1";
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "xml";
            this.ofd.Title = "Select Query XML file to Open";
            // 
            // lblSave
            // 
            this.lblSave.Location = new System.Drawing.Point(49, 354);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(224, 23);
            this.lblSave.TabIndex = 57;
            this.lblSave.Text = "This record has been saved sucessfully!";
            this.lblSave.Visible = false;
            // 
            // frmQueryXMLGeneratorWSRQNR
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(946, 533);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmQueryXMLGeneratorWSRQNR";
            this.Text = "Query XML File  Generator";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmQueryXMLGenerator_Closing);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdLoadTemplate_Click(object sender, System.EventArgs e)
		{
			
		
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			dsXML = new DataSet();

			bTemplateFileMissing=true;
			string sAppPath = Application.ExecutablePath;
			sAppPath= Path.GetDirectoryName(sAppPath);
            string sQueryTemplate = sAppPath + "\\" + "WSRQNRDataTemplate.req";
            			
			dsXML.ReadXml(sQueryTemplate,XmlReadMode.InferSchema);
			 
			SetDataBindings();
			QueryFileName = "NewWSRQNRQueryMessage.req";
            cmdSave.Visible = true;
            lblSave.Visible = false;
            
		}

		private void SetDataBindings()
		{
		        ClearBindings();
                
                RequestID.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RequestID");
                RequestDate.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RequestDate");
                RequestDataSource.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RequestDataSource");
                RequestReminderNum.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RequestReminderNum");
                TNO.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "TNO");
                ICC.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "ICC");
                DCL.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "DCL");
                CNL.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "CNL");
                COF.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "COF");
                DDI.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "DDI");
                RND.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RND");
                PFD.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "PFD");
                CWR.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "CWR");
                VPN.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "VPN");
                COM.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "COM");
                RBC.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "RBC");
                PIC.DataBindings.Add("Text", dsXML.Tables["RequestRecord"], "PIC");
                
            
        }

		private void ClearBindings()
		{
			try
			{
                int bindrow = addrow;
				//clear bindings may cause exception
                if (addrow > 0)
                    bindrow = addrow - 1;
                RequestID.DataBindings.RemoveAt(bindrow);
                RequestDate.DataBindings.RemoveAt(bindrow);
                RequestDataSource.DataBindings.RemoveAt(bindrow);
                RequestReminderNum.DataBindings.RemoveAt(bindrow);
                TNO.DataBindings.RemoveAt(bindrow);
                ICC.DataBindings.RemoveAt(bindrow);
                DCL.DataBindings.RemoveAt(bindrow);
                CNL.DataBindings.RemoveAt(bindrow);
                COF.DataBindings.RemoveAt(bindrow);
                DDI.DataBindings.RemoveAt(bindrow);
                RND.DataBindings.RemoveAt(bindrow);
                PFD.DataBindings.RemoveAt(bindrow);
                CWR.DataBindings.RemoveAt(bindrow);
                VPN.DataBindings.RemoveAt(bindrow);
                COM.DataBindings.RemoveAt(bindrow);
                RBC.DataBindings.RemoveAt(bindrow);
                PIC.DataBindings.RemoveAt(bindrow);
               

			}
			catch (Exception bex)
			{
				//swallowed 
			}
			

		
		
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
           sfd.Filter="REQ Files (*.req)|*.req";
			sfd.Title="Select Folder and enter File Name to save the message REQ file";
			sfd.ValidateNames=true;

			sfd.DefaultExt= ".req";
            sfd.FileName = QueryFileName;
            DialogResult dr = sfd.ShowDialog(this);
                if (dr != DialogResult.OK)
                    return;
            string fileName = sfd.FileName;
            
            dsXML.WriteXml(fileName,XmlWriteMode.IgnoreSchema);
			//get rid of the opening xml version 
			StreamReader sr = null;// = new StreamReader(fileName);

			string sData;
			try
			{
				sr = new StreamReader(fileName);
				sData = sr.ReadToEnd();
			}
			finally
			{
				sr.Close();
			}
			
			//get rid of processing instructions
			sData = sData.Replace("<?xml version=\"1.0\" standalone=\"yes\"?>\r\n","");

			StreamWriter sw = null;
			try
			{
				sw = new StreamWriter(fileName,false,System.Text.Encoding.Unicode);
				sw.Write(sData);
			}
			finally
			{
				sw.Close();
			}
			
			QueryFileName= fileName;
            
            this.cmdSave.Visible = false;
            this.lblSave.Visible = true;
            ClearBindings();


		}

		private string QueryFileName
		{
			get
			{
				return m_LoadedFileName;
			}

			set
			{
				m_LoadedFileName =value;
				this.Text= "Query XML File " + m_LoadedFileName;	
			
			}
		
		
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{

			//load file
			ofd.Filter="REQ Files (*.req)|*.req";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			dsXML= new DataSet();
            dsXML.ReadXml(ofd.FileName,XmlReadMode.InferSchema);
            
			QueryFileName = ofd.FileName;
            this.lblSave.Visible = false;



		
			SetDataBindings();

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
            if (addrow > 0)
            {
                dsXML.Tables["RequestRecord"].Rows.RemoveAt(addrow);
                //cmdSave.Click = true;
                 


            }
			this.Close();
		}

		private void frmQueryXMLGenerator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			TestParent.m_MesseageFileName=QueryFileName;
		}

        private void cmdAdd_Click(object sender, EventArgs e)
        {
           // ClearBindings();
            RequestID.Text = "";
            RequestDataSource.Text = "";
            RequestDate.Text = "";
            RequestReminderNum.Text = "";
            CNL.Text = "";
            DCL.Text = "";
            ICC.Text = "";
            PIC.Text = "";
            RBC.Text = "";
            COM.Text = "";
            COF.Text = "";
            VPN.Text = "";
            CWR.Text = "";
            PFD.Text = "";
            RND.Text = "";
            DDI.Text = "";
            RequestDate.Text = "";
            TNO.Text = "";
            this.lblSave.Visible = false;
            DataRow dr = dsXML.Tables["RequestRecord"].NewRow();
            dsXML.Tables["RequestRecord"].Rows.Add(dr);
            addrow = addrow+1;
            cmdSave.Visible = true;
        }

		
	}
}
