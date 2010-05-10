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
	public class frmQueryXMLGeneratorWSRE : System.Windows.Forms.Form
    {
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
        private System.Windows.Forms.OpenFileDialog ofd;
        private int addrow;
        private DataGridView dataGridView1;
        private Panel pnlMain;
        private TextBox RequestReplyType;
        private Label label1;
        private TextBox PIC;
        private TextBox RBC;
        private TextBox COM;
        private TextBox VPN;
        private TextBox CWR;
        private TextBox PFD;
        private TextBox RND;
        private TextBox DDI;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private TextBox COF;
        private TextBox RequestID;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label4;
        private Label label2;
        private TextBox CNL;
        private TextBox DCL;
        private TextBox ICC;
        private TextBox TNO;
        private Button button1;
        private Button button2;
        private Label lblSave;
        private Button cmdAdd;
      

		private string m_LoadedFileName;

		public frmQueryXMLGeneratorWSRE()
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

            string sQueryTemplate = sAppPath + "\\" + "WSREDataTemplate.xml";

			if (!File.Exists(sQueryTemplate))
			{
                MessageBox.Show("WSREDataTemplate.xml file not found in the application folder \r\n: " + Application.StartupPath);
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
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblSave = new System.Windows.Forms.Label();
            this.RequestReplyType = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 429);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(971, 10);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "-";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(1644, 561);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(1540, 561);
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
            // ofd
            // 
            this.ofd.DefaultExt = "xml";
            this.ofd.Title = "Select Query XML file to Open";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.cmdAdd);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.button2);
            this.pnlMain.Controls.Add(this.lblSave);
            this.pnlMain.Controls.Add(this.cmdCancel);
            this.pnlMain.Controls.Add(this.cmdSave);
            this.pnlMain.Controls.Add(this.RequestReplyType);
            this.pnlMain.Controls.Add(this.dataGridView1);
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
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.MaximumSize = new System.Drawing.Size(1000, 800);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(965, 429);
            this.pnlMain.TabIndex = 59;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAdd.Location = new System.Drawing.Point(48, 355);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(185, 23);
            this.cmdAdd.TabIndex = 66;
            this.cmdAdd.Text = "Add Another Record";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(380, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 64;
            this.button1.Text = "Cancel";
            this.button1.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(274, 355);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 63;
            this.button2.Text = "Save";
            this.button2.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // lblSave
            // 
            this.lblSave.Location = new System.Drawing.Point(41, 381);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(224, 23);
            this.lblSave.TabIndex = 65;
            this.lblSave.Text = "This record has been saved sucessfully!";
            this.lblSave.Visible = false;
            // 
            // RequestReplyType
            // 
            this.RequestReplyType.Location = new System.Drawing.Point(165, 138);
            this.RequestReplyType.Name = "RequestReplyType";
            this.RequestReplyType.Size = new System.Drawing.Size(184, 20);
            this.RequestReplyType.TabIndex = 56;
            this.RequestReplyType.Text = "textbox";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataBindings.Add(new System.Windows.Forms.Binding("ReadOnly", global::RTSTestClient.Properties.Settings.Default, "SetReadOnly", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = global::RTSTestClient.Properties.Settings.Default.SetReadOnly;
            this.dataGridView1.Size = new System.Drawing.Size(1185, 89);
            this.dataGridView1.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 55;
            this.label1.Text = "RequestReplyType";
            // 
            // PIC
            // 
            this.PIC.Location = new System.Drawing.Point(473, 318);
            this.PIC.Name = "PIC";
            this.PIC.Size = new System.Drawing.Size(184, 20);
            this.PIC.TabIndex = 53;
            this.PIC.Text = "textBox1";
            // 
            // RBC
            // 
            this.RBC.Location = new System.Drawing.Point(473, 288);
            this.RBC.Name = "RBC";
            this.RBC.Size = new System.Drawing.Size(184, 20);
            this.RBC.TabIndex = 52;
            this.RBC.Text = "textBox1";
            // 
            // COM
            // 
            this.COM.Location = new System.Drawing.Point(473, 259);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(184, 20);
            this.COM.TabIndex = 51;
            this.COM.Text = "textBox1";
            // 
            // VPN
            // 
            this.VPN.Location = new System.Drawing.Point(473, 224);
            this.VPN.Name = "VPN";
            this.VPN.Size = new System.Drawing.Size(184, 20);
            this.VPN.TabIndex = 50;
            this.VPN.Text = "textBox1";
            // 
            // CWR
            // 
            this.CWR.Location = new System.Drawing.Point(473, 191);
            this.CWR.Name = "CWR";
            this.CWR.Size = new System.Drawing.Size(184, 20);
            this.CWR.TabIndex = 49;
            this.CWR.Text = "textBox1";
            // 
            // PFD
            // 
            this.PFD.Location = new System.Drawing.Point(473, 153);
            this.PFD.Name = "PFD";
            this.PFD.Size = new System.Drawing.Size(184, 20);
            this.PFD.TabIndex = 48;
            this.PFD.Text = "textBox1";
            // 
            // RND
            // 
            this.RND.Location = new System.Drawing.Point(473, 120);
            this.RND.Name = "RND";
            this.RND.Size = new System.Drawing.Size(184, 20);
            this.RND.TabIndex = 47;
            this.RND.Text = "textBox1";
            // 
            // DDI
            // 
            this.DDI.Location = new System.Drawing.Point(473, 94);
            this.DDI.Name = "DDI";
            this.DDI.Size = new System.Drawing.Size(184, 20);
            this.DDI.TabIndex = 46;
            this.DDI.Text = "textBox1";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(395, 318);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 16);
            this.label18.TabIndex = 44;
            this.label18.Text = "PIC";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(395, 288);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 16);
            this.label17.TabIndex = 43;
            this.label17.Text = "RBC";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(395, 228);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 16);
            this.label16.TabIndex = 42;
            this.label16.Text = "VPN";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(395, 263);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 41;
            this.label15.Text = "COM";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(395, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 40;
            this.label14.Text = "CWR";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(395, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 39;
            this.label13.Text = "PFD";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(395, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 38;
            this.label12.Text = "RND";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(395, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 37;
            this.label11.Text = "DDI";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(45, 301);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 36;
            this.label10.Text = "COF";
            // 
            // COF
            // 
            this.COF.Location = new System.Drawing.Point(165, 298);
            this.COF.Name = "COF";
            this.COF.Size = new System.Drawing.Size(184, 20);
            this.COF.TabIndex = 35;
            this.COF.Text = "textBox1";
            // 
            // RequestID
            // 
            this.RequestID.Location = new System.Drawing.Point(165, 112);
            this.RequestID.Name = "RequestID";
            this.RequestID.Size = new System.Drawing.Size(184, 20);
            this.RequestID.TabIndex = 1;
            this.RequestID.Text = "textBox1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(45, 270);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 33;
            this.label8.Text = "CNL";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(45, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 19);
            this.label7.TabIndex = 32;
            this.label7.Text = "DCL";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(45, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 31;
            this.label6.Text = "ICC";
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label4.Location = new System.Drawing.Point(45, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 23);
            this.label4.TabIndex = 29;
            this.label4.Text = "TNO";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "RequestID";
            // 
            // CNL
            // 
            this.CNL.Location = new System.Drawing.Point(165, 267);
            this.CNL.Name = "CNL";
            this.CNL.Size = new System.Drawing.Size(184, 20);
            this.CNL.TabIndex = 7;
            this.CNL.Text = "textBox1";
            // 
            // DCL
            // 
            this.DCL.Location = new System.Drawing.Point(165, 238);
            this.DCL.Name = "DCL";
            this.DCL.Size = new System.Drawing.Size(184, 20);
            this.DCL.TabIndex = 6;
            this.DCL.Text = "textBox1";
            // 
            // ICC
            // 
            this.ICC.Location = new System.Drawing.Point(165, 204);
            this.ICC.Name = "ICC";
            this.ICC.Size = new System.Drawing.Size(184, 20);
            this.ICC.TabIndex = 5;
            this.ICC.Text = "textBox1";
            // 
            // TNO
            // 
            this.TNO.Location = new System.Drawing.Point(165, 169);
            this.TNO.Name = "TNO";
            this.TNO.Size = new System.Drawing.Size(184, 20);
            this.TNO.TabIndex = 3;
            this.TNO.Text = "textBox1";
            // 
            // frmQueryXMLGeneratorWSRE
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(971, 439);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmQueryXMLGeneratorWSRE";
            this.Text = "Query XML File  Generator";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmQueryXMLGenerator_Closing);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
            string sQueryTemplate = sAppPath + "\\" + "WSREDataTemplate.xml";

			//dsXML.ReadXml("QueryTemplate.xml",XmlReadMode.InferSchema);
			dsXML.ReadXml(sQueryTemplate,XmlReadMode.InferSchema);
			 
			SetDataBindings();
			QueryFileName = "NewWSREQueryMessage.xml";
		}

		private void SetDataBindings()
		{
		        ClearBindings();
                     
                RequestID.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "RequestID");
                RequestReplyType.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "RequestReplyType");
                TNO.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "TNO");
                ICC.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "ICC");
                DCL.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "DCL");
                CNL.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "CNL");
                COF.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "COF");
                DDI.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "DDI");
                RND.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "RND");
                PFD.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "PFD");
                CWR.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "CWR");
                VPN.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "VPN");
                COM.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "COM");
                RBC.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "RBC");
                PIC.DataBindings.Add("Text", dsXML.Tables["RequestReplyRecord"], "PIC");
                /*for (int hcnt = 0; hcnt < dsXML.Tables["RequestReplyRecords"].Rows.Count - 1; hcnt++)
                {
                    addrow = hcnt;
                    ClearBindings();
                }*/
                dataGridView1.DataSource = dsXML.Tables["RequestReplyRecord"];
               
            
        }

		private void ClearBindings()
		{
			try
			{
                int bindrow = addrow;
				//clear bindings may cause exception
                if (addrow > 0)
                    bindrow = addrow - 1;
                //SubScriberID.DataBindings.RemoveAt(bindrow);
                //Sender_MesssageID.DataBindings.RemoveAt(bindrow);
                RequestID.DataBindings.RemoveAt(bindrow);
                RequestReplyType.DataBindings.RemoveAt(bindrow);
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
                int recnum=0;
                int.TryParse(dsXML.Tables["Body"].Rows[0]["NumberofRecords"].ToString(),out recnum);
                if (recnum >= 1)
                    dsXML.Tables["Body"].Rows[0]["NumberofRecords"] = recnum;

			}
			catch (Exception bex)
			{
				//swallowed 
			}
			

		
		
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
            int  recnum;
            recnum=0;
			sfd.Filter="XML Files (*.xml)|*.xml";
			sfd.Title="Select Folder and enter File Name to save the message XML file";
			sfd.ValidateNames=true;

			sfd.DefaultExt= ".xml";
			sfd.FileName=QueryFileName;
            if (addrow == 0)
            {
                DialogResult dr = sfd.ShowDialog(this);
                if (dr != DialogResult.OK)
                    return;
            }
            else
            {
                dsXML.Tables["RequestReplyRecord"].Rows[addrow].BeginEdit();
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["RequestReplyRecords_Id"] = 0;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["RequestID"] = RequestID.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["RequestReplyType"] = RequestReplyType.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["CNL"] = CNL.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["DCL"] = DCL.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["ICC"] = ICC.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["PIC"] = PIC.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["RBC"] = RBC.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["COM"] = COM.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["COF"] = COF.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["VPN"] = VPN.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["CWR"] = CWR.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["PFD"] = PFD.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["RND"] = RND.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["DDI"] = DDI.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow]["TNO"] = TNO.Text;
                dsXML.Tables["RequestReplyRecord"].Rows[addrow].EndEdit();
                
                int.TryParse(dsXML.Tables["Body"].Rows[0]["NumberofRecords"].ToString(),out recnum);
                if (recnum >= 1)
                {
                     dsXML.Tables["Body"].Rows[0]["NumberofRecords"] = recnum+1;
                    
                }
                //dsXML.Tables["Body"].Rows[0]["Sender_MessageID"]=Sender_MesssageID.Text;
                //dsXML.Tables["Body"].Rows[0]["SubscriberID"]= SubScriberID.Text;
                
            }
            string fileName = sfd.FileName;

	        
			dsXML.WriteXml(fileName,XmlWriteMode.IgnoreSchema );
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
				sw = new StreamWriter(fileName,false ,System.Text.Encoding.Unicode);
				sw.Write(sData);
			}
			finally
			{
				sw.Close();
			}
			
			QueryFileName= fileName;
            
            this.cmdAdd.Visible = true;
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
			ofd.Filter="XML Files (*.xml)|*.xml";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			dsXML= new DataSet();
			dsXML.ReadXml(ofd.FileName,XmlReadMode.InferSchema);
			QueryFileName = ofd.FileName;
            this.lblSave.Visible = false;
            //addrow = dsXML.Tables["RequestReplyRecord"].Rows.Count;



		
			SetDataBindings();

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
            if (addrow > 0)
            {
                dsXML.Tables["RequestReplyRecord"].Rows.RemoveAt(addrow);
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
            ClearBindings();
            RequestID.Text = "";
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
            RequestReplyType.Text = "";
            TNO.Text = "";
            this.lblSave.Visible = false;
            DataRow dr = dsXML.Tables["RequestReplyRecord"].NewRow();
            dsXML.Tables["RequestReplyRecord"].Rows.Add(dr);
            addrow = addrow+1;
            cmdAdd.Visible = false;
            cmdSave.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

		
	}
}
