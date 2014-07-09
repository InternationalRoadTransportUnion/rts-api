using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using IRU.Common.Config.PluginConfigCtl;


namespace IRU.Common.ConfigEditorUI
{
	/// <summary>
	/// Summary description for frmHost.
	/// </summary>
	public class frmHost : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpMain;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
        private IRU.Common.Config.PluginConfigCtl.ConfigTabHost ucConfigTab;
        private IContainer components;

		public frmHost()
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
				if (components != null) 
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
            this.gpMain = new System.Windows.Forms.GroupBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.ucConfigTab = new IRU.Common.Config.PluginConfigCtl.ConfigTabHost();
            this.gpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpMain
            // 
            this.gpMain.Controls.Add(this.ucConfigTab);
            this.gpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpMain.Location = new System.Drawing.Point(0, 0);
            this.gpMain.Name = "gpMain";
            this.gpMain.Size = new System.Drawing.Size(672, 343);
            this.gpMain.TabIndex = 1;
            this.gpMain.TabStop = false;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
            this.menuItem1.Text = "&File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "&Open";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "&Save";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "E&xit";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // ucConfigTab
            // 
            this.ucConfigTab.ConfigFilePath = null;
            this.ucConfigTab.Context = null;
            this.ucConfigTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucConfigTab.EditTemplateFilePath = null;
            this.ucConfigTab.Location = new System.Drawing.Point(3, 16);
            this.ucConfigTab.Name = "ucConfigTab";
            this.ucConfigTab.Size = new System.Drawing.Size(666, 324);
            this.ucConfigTab.TabIndex = 0;
            // 
            // frmHost
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(672, 343);
            this.Controls.Add(this.gpMain);
            this.Menu = this.mainMenu1;
            this.Name = "frmHost";
            this.Text = "Plugins Config Editor";
            this.gpMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmHost());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			SelectFiles sf = new SelectFiles();
			sf.ConfigFilePath=ucConfigTab.ConfigFilePath;
			sf.TemplateFilePath=ucConfigTab.EditTemplateFilePath;
			if (sf.ShowDialog(this)==DialogResult.Cancel)
				return;
		
////			ucConfigTab.ConfigFilePath=Application.StartupPath + "\\Copy of WSTCHQ_Config.xml";
////			ucConfigTab.EditTemplateFilePath=Application.StartupPath + "\\" + "sampleTemplate.xml";


			ucConfigTab.ConfigFilePath=sf.ConfigFilePath;
			ucConfigTab.EditTemplateFilePath=sf.TemplateFilePath;
			

			StringDictionary context = new StringDictionary();
			ucConfigTab.Context = context;
			context.Add("target",@"c:\RTS2\");
			ucConfigTab.DisplayFileEditor();

		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			ucConfigTab.SaveFile();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
