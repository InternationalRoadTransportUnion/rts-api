using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{


		#region Static Properties
		internal static string UserID="admin";
		internal static int[] UserRights;
		//internal static string  DBuser="rtsuser", DBPassword="rtsuser";

		internal static Hashtable HTConnectionStrings;

		//internal static string SubsDBConnectionString = @"Application Name=AdminClient;Server=Missouri\IRUAPPS;Database=WS_Subscriber_db;uid=rtsuser;pwd=realtime;Connect Timeout=30";
		

		#endregion

		#region Menu Hashtable
			Hashtable m_htMenuRights;
		#endregion

		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.MenuItem mnuACU;
		private System.Windows.Forms.MenuItem mnuACU_Add;
		private System.Windows.Forms.MenuItem mnuACU_Modify;
		private System.Windows.Forms.MenuItem mnuSubs;
		private System.Windows.Forms.MenuItem mnuSubs_Add;
		private System.Windows.Forms.MenuItem mnuSubs_Modify;
		private System.Windows.Forms.MenuItem mnuKM;
		private System.Windows.Forms.MenuItem mnuKM_GIK;
		private System.Windows.Forms.MenuItem mnuKM_MIKS;
		private System.Windows.Forms.MenuItem mnuKM_SKS;
		private System.Windows.Forms.MenuItem mnuKM_USC;
		private System.Windows.Forms.MenuItem mnuReports;
		private System.Windows.Forms.MenuItem mnuReports_VR;
		private System.Windows.Forms.MenuItem mnuReports_MR;
		private System.Windows.Forms.MenuItem mnuReports_MR_PR;
		private System.Windows.Forms.MenuItem mnuAlerts;
		private System.Windows.Forms.MenuItem mnuReports_MR_PR_Add;
		private System.Windows.Forms.MenuItem mnuReports_VR_PR;
		private System.Windows.Forms.MenuItem mnuReports_VR_DR;
		private System.Windows.Forms.MenuItem mnuSyncDB;
		private System.Windows.Forms.MenuItem mnuSyncDB_Export;
		private System.Windows.Forms.MenuItem mnuSyncDB_Import;
		private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.MenuItem mnuReports_MR_PR_Modify;
        private MenuItem mnuKM_RTS;
        private MenuItem mnuKM_RTSPLUS;
        private MenuItem mnuKM_RTSPLUS_MKS;
        private IContainer components;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//SubsDBConnectionString = System.Configuration.ConfigurationSettings.AppSettings["SubsDBConnString"].ToString();

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
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuACU = new System.Windows.Forms.MenuItem();
            this.mnuACU_Add = new System.Windows.Forms.MenuItem();
            this.mnuACU_Modify = new System.Windows.Forms.MenuItem();
            this.mnuSubs = new System.Windows.Forms.MenuItem();
            this.mnuSubs_Add = new System.Windows.Forms.MenuItem();
            this.mnuSubs_Modify = new System.Windows.Forms.MenuItem();
            this.mnuKM = new System.Windows.Forms.MenuItem();
            this.mnuKM_RTS = new System.Windows.Forms.MenuItem();
            this.mnuKM_GIK = new System.Windows.Forms.MenuItem();
            this.mnuKM_MIKS = new System.Windows.Forms.MenuItem();
            this.mnuKM_SKS = new System.Windows.Forms.MenuItem();
            this.mnuKM_USC = new System.Windows.Forms.MenuItem();
            this.mnuKM_RTSPLUS = new System.Windows.Forms.MenuItem();
            this.mnuKM_RTSPLUS_MKS = new System.Windows.Forms.MenuItem();
            this.mnuReports = new System.Windows.Forms.MenuItem();
            this.mnuReports_VR = new System.Windows.Forms.MenuItem();
            this.mnuReports_VR_PR = new System.Windows.Forms.MenuItem();
            this.mnuReports_VR_DR = new System.Windows.Forms.MenuItem();
            this.mnuReports_MR = new System.Windows.Forms.MenuItem();
            this.mnuReports_MR_PR = new System.Windows.Forms.MenuItem();
            this.mnuReports_MR_PR_Add = new System.Windows.Forms.MenuItem();
            this.mnuReports_MR_PR_Modify = new System.Windows.Forms.MenuItem();
            this.mnuAlerts = new System.Windows.Forms.MenuItem();
            this.mnuSyncDB = new System.Windows.Forms.MenuItem();
            this.mnuSyncDB_Export = new System.Windows.Forms.MenuItem();
            this.mnuSyncDB_Import = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuACU,
            this.mnuSubs,
            this.mnuKM,
            this.mnuReports,
            this.mnuAlerts,
            this.mnuSyncDB,
            this.mnuExit});
            // 
            // mnuACU
            // 
            this.mnuACU.Index = 0;
            this.mnuACU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuACU_Add,
            this.mnuACU_Modify});
            this.mnuACU.Text = "Admin Client Users";
            // 
            // mnuACU_Add
            // 
            this.mnuACU_Add.Index = 0;
            this.mnuACU_Add.Text = "Add";
            this.mnuACU_Add.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // mnuACU_Modify
            // 
            this.mnuACU_Modify.Index = 1;
            this.mnuACU_Modify.Text = "Modify";
            this.mnuACU_Modify.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // mnuSubs
            // 
            this.mnuSubs.Index = 1;
            this.mnuSubs.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSubs_Add,
            this.mnuSubs_Modify});
            this.mnuSubs.Text = "Subscribers";
            // 
            // mnuSubs_Add
            // 
            this.mnuSubs_Add.Index = 0;
            this.mnuSubs_Add.Text = "Add";
            this.mnuSubs_Add.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // mnuSubs_Modify
            // 
            this.mnuSubs_Modify.Index = 1;
            this.mnuSubs_Modify.Text = "Modify";
            this.mnuSubs_Modify.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // mnuKM
            // 
            this.mnuKM.Index = 2;
            this.mnuKM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuKM_RTS,
            this.mnuKM_RTSPLUS});
            this.mnuKM.Text = "Key Management";
            // 
            // mnuKM_RTS
            // 
            this.mnuKM_RTS.Index = 0;
            this.mnuKM_RTS.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuKM_GIK,
            this.mnuKM_MIKS,
            this.mnuKM_SKS,
            this.mnuKM_USC});
            this.mnuKM_RTS.Text = "RTS";
            // 
            // mnuKM_GIK
            // 
            this.mnuKM_GIK.Index = 0;
            this.mnuKM_GIK.Text = "Generate IRU Key";
            this.mnuKM_GIK.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // mnuKM_MIKS
            // 
            this.mnuKM_MIKS.Index = 1;
            this.mnuKM_MIKS.Text = "Manage IRU Key Status";
            this.mnuKM_MIKS.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // mnuKM_SKS
            // 
            this.mnuKM_SKS.Index = 2;
            this.mnuKM_SKS.Text = "Subscriber Key Status";
            this.mnuKM_SKS.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // mnuKM_USC
            // 
            this.mnuKM_USC.Index = 3;
            this.mnuKM_USC.Text = "(Temp) Upload Subs Cert";
            this.mnuKM_USC.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // mnuKM_RTSPLUS
            // 
            this.mnuKM_RTSPLUS.Index = 1;
            this.mnuKM_RTSPLUS.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuKM_RTSPLUS_MKS});
            this.mnuKM_RTSPLUS.Text = "RTS+";
            // 
            // mnuKM_RTSPLUS_MKS
            // 
            this.mnuKM_RTSPLUS_MKS.Index = 0;
            this.mnuKM_RTSPLUS_MKS.Text = "Manage Keystore";
            this.mnuKM_RTSPLUS_MKS.Click += new System.EventHandler(this.mnuKM_RTSPLUS_MKS_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.Index = 3;
            this.mnuReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReports_VR,
            this.mnuReports_MR});
            this.mnuReports.Text = "Reports";
            // 
            // mnuReports_VR
            // 
            this.mnuReports_VR.Index = 0;
            this.mnuReports_VR.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReports_VR_PR,
            this.mnuReports_VR_DR});
            this.mnuReports_VR.Text = "View Reports";
            // 
            // mnuReports_VR_PR
            // 
            this.mnuReports_VR_PR.Index = 0;
            this.mnuReports_VR_PR.Text = "Performance Reports";
            this.mnuReports_VR_PR.Click += new System.EventHandler(this.mnuReports_VR_PR_Click);
            // 
            // mnuReports_VR_DR
            // 
            this.mnuReports_VR_DR.Index = 1;
            this.mnuReports_VR_DR.Text = "Diagnostic Reports";
            this.mnuReports_VR_DR.Click += new System.EventHandler(this.mnuReports_VR_DR_Click);
            // 
            // mnuReports_MR
            // 
            this.mnuReports_MR.Index = 1;
            this.mnuReports_MR.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReports_MR_PR});
            this.mnuReports_MR.Text = "Manage Reports";
            // 
            // mnuReports_MR_PR
            // 
            this.mnuReports_MR_PR.Index = 0;
            this.mnuReports_MR_PR.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReports_MR_PR_Add,
            this.mnuReports_MR_PR_Modify});
            this.mnuReports_MR_PR.Text = "Periodic reports";
            // 
            // mnuReports_MR_PR_Add
            // 
            this.mnuReports_MR_PR_Add.Index = 0;
            this.mnuReports_MR_PR_Add.Text = "Add";
            this.mnuReports_MR_PR_Add.Click += new System.EventHandler(this.mnuReports_MR_PR_Add_Click);
            // 
            // mnuReports_MR_PR_Modify
            // 
            this.mnuReports_MR_PR_Modify.Index = 1;
            this.mnuReports_MR_PR_Modify.Text = "Modify";
            this.mnuReports_MR_PR_Modify.Click += new System.EventHandler(this.mnuReports_MR_PR_Modify_Click);
            // 
            // mnuAlerts
            // 
            this.mnuAlerts.Index = 4;
            this.mnuAlerts.Text = "Alerts";
            this.mnuAlerts.Click += new System.EventHandler(this.mnuAlerts_Click);
            // 
            // mnuSyncDB
            // 
            this.mnuSyncDB.Index = 5;
            this.mnuSyncDB.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSyncDB_Export,
            this.mnuSyncDB_Import});
            this.mnuSyncDB.Text = "SynchDB";
            // 
            // mnuSyncDB_Export
            // 
            this.mnuSyncDB_Export.Index = 0;
            this.mnuSyncDB_Export.Text = "Export Subscriber DB";
            this.mnuSyncDB_Export.Click += new System.EventHandler(this.mnuSyncDB_Export_Click);
            // 
            // mnuSyncDB_Import
            // 
            this.mnuSyncDB_Import.Index = 1;
            this.mnuSyncDB_Import.Text = "Import Subscriber DB";
            this.mnuSyncDB_Import.Click += new System.EventHandler(this.mnuSyncDB_Import_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 6;
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(768, 417);
            this.IsMdiContainer = true;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "RTS & RTS+ Admin Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.ResumeLayout(false);

		}
		#endregion

		private void menuItem5_Click(object sender, System.EventArgs e)
		{

			frmCertClient frmc = new frmCertClient();
			frmc.MdiParent=this;
			frmc.Show();

		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			
			Application.Exit();
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{

			if (UserRights==null)
			{
				MessageBox.Show("UserRights not set", "AdminClient");
				Application.Exit();

			}
			#region Setup Menu Security

			#region SetMenu Items Collection
				SetMenuCollection();
				//disable all menus
			foreach (object key in  m_htMenuRights.Keys)
			{
				((MenuItem)m_htMenuRights[key]).Enabled=false;
			}
				
			#endregion


			#region Enable Menus

			foreach (int rightID in UserRights)
			{
				((MenuItem)m_htMenuRights[rightID]).Enabled=true;
			}
			#endregion


			

			#region Drop Menus not valid in Location

			if(ConfigurationSettings.AppSettings["Location"].ToString()=="Intranet")
			{
			
				
				((MenuItem)m_htMenuRights[602]).Enabled=false; //import db

			}
			else if(ConfigurationSettings.AppSettings["Location"].ToString()=="BlueLan")
			{
			
				//disable top level menus
				((MenuItem)m_htMenuRights[100]).Enabled=false; //manage users
				((MenuItem)m_htMenuRights[200]).Enabled=false; //manage subscribers
				((MenuItem)m_htMenuRights[300]).Enabled=false; //Key Management
				((MenuItem)m_htMenuRights[601]).Enabled=false; //Export DB
 
				
			}
			else
			{
				MessageBox.Show("Invlid Location setting in configuration file. Application will exit");
				Application.Exit();
			
			}
			# endregion


			#region debug testing import db

			mnuSyncDB_Import.Enabled=true;
			#endregion
			#endregion




		}

		private void SetMenuCollection()
		{
			m_htMenuRights= new Hashtable();
			m_htMenuRights.Add(	100	,mnuACU	);
m_htMenuRights.Add(	101	,mnuACU_Add	);
m_htMenuRights.Add(	102	,mnuACU_Modify	);

m_htMenuRights.Add(	200	,mnuSubs	);
m_htMenuRights.Add(	201	,mnuSubs_Add	);
m_htMenuRights.Add(	202	,mnuSubs_Modify 	);

m_htMenuRights.Add(	300	,mnuKM	);
m_htMenuRights.Add(	301	,mnuKM_GIK	);
m_htMenuRights.Add(	302	,mnuKM_MIKS	);
m_htMenuRights.Add(	303	,mnuKM_SKS	);
m_htMenuRights.Add(304, mnuKM_USC);
m_htMenuRights.Add(305, mnuKM_RTSPLUS_MKS);

m_htMenuRights.Add(	400	,mnuReports	);
m_htMenuRights.Add(	401	,mnuReports_VR	);
m_htMenuRights.Add(	402	,mnuReports_VR_PR	);
m_htMenuRights.Add(	403	,mnuReports_VR_DR	);
m_htMenuRights.Add(	450	,mnuReports_MR	);
m_htMenuRights.Add(	451	,mnuReports_MR_PR	);
m_htMenuRights.Add(	452	,mnuReports_MR_PR_Add	);
			m_htMenuRights.Add(	453	,mnuReports_MR_PR_Modify);



m_htMenuRights.Add(	500	,mnuAlerts	);

m_htMenuRights.Add(	600	,mnuSyncDB	);
m_htMenuRights.Add(	601	,mnuSyncDB_Export	);
m_htMenuRights.Add(	602	,mnuSyncDB_Import	);


		

		
		
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Exit();
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			frmTempUploadSubsCert fUpload= new frmTempUploadSubsCert();
			fUpload.MdiParent = this;
			fUpload.Show();
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			AddSubscriber fAddSubscriber = new AddSubscriber();
			fAddSubscriber.MdiParent=this;
			fAddSubscriber.Show();
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			AddUser fAdduser = new AddUser();
			fAdduser.MdiParent=this;
			fAdduser.Show();
		
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			ModifySubscriber fModSubscriber = new ModifySubscriber();
			fModSubscriber.MdiParent=this;
			fModSubscriber.Show();

		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			ModifyUser fModUsers = new ModifyUser();
			fModUsers.MdiParent= this;
			fModUsers.Show();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			ManageIRUKeyStatus fIruKetStatus= new ManageIRUKeyStatus();
			fIruKetStatus.MdiParent=this;
			fIruKetStatus.Show();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			ManageSubsKeys fManageSubsKeys = new ManageSubsKeys();
			fManageSubsKeys.MdiParent=this;
			fManageSubsKeys.Show();
		}

		private void mnuReports_MR_PR_Add_Click(object sender, System.EventArgs e)
		{
			AddPeriodicReport fAddPeriodicReports= new AddPeriodicReport();
			fAddPeriodicReports.MdiParent=this;
			fAddPeriodicReports.Show();
		}

		private void mnuReports_MR_PR_Modify_Click(object sender, System.EventArgs e)
		{
			ModifyPeriodicReport fModiFyPeriodicReports= new ModifyPeriodicReport();
			fModiFyPeriodicReports.MdiParent=this;
			fModiFyPeriodicReports.Show();
		}

		private void mnuAlerts_Click(object sender, System.EventArgs e)
		{
			ManageAlerts fManageAlerts = new ManageAlerts();
			fManageAlerts.MdiParent=this;
			fManageAlerts.Show();
		}

		private void mnuSyncDB_Export_Click(object sender, System.EventArgs e)
		{
			ExportSubsDB fExportSubsDB = new ExportSubsDB();
			fExportSubsDB.MdiParent=this;
			fExportSubsDB.Show();
		}

		private void mnuSyncDB_Import_Click(object sender, System.EventArgs e)
		{
			ImportSubsDB fImportSubsDB = new ImportSubsDB();
			fImportSubsDB.MdiParent=this;
			fImportSubsDB.Show();
		}

		private void mnuReports_VR_DR_Click(object sender, System.EventArgs e)
		{
			DiagnosticReports fDiagRep = new DiagnosticReports();
			fDiagRep.MdiParent=this;
			fDiagRep.Show();
		}

		private void mnuReports_VR_PR_Click(object sender, System.EventArgs e)
		{
			PerformanceReports fPerfReports = new PerformanceReports();
			fPerfReports.MdiParent=this;
			fPerfReports.Show();
		
		}

        private void mnuKM_RTSPLUS_MKS_Click(object sender, EventArgs e)
        {
            frmRTSPlusKeystore fRTSPlusKeystore = new frmRTSPlusKeystore();
            fRTSPlusKeystore.MdiParent = this;
            fRTSPlusKeystore.Show();
        }
	}
}
