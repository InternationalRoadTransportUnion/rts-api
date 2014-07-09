using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using IRU.RTS.CommonComponents;

namespace GenTestHost
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtConfigFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button cmdUnload;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
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
			this.txtConfigFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.cmdUnload = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtConfigFile
			// 
			this.txtConfigFile.Location = new System.Drawing.Point(120, 32);
			this.txtConfigFile.Name = "txtConfigFile";
			this.txtConfigFile.Size = new System.Drawing.Size(408, 20);
			this.txtConfigFile.TabIndex = 0;
			this.txtConfigFile.Text = "WSTCHQ_SMD.xml";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Path to Config File";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(544, 32);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "Load";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdUnload
			// 
			this.cmdUnload.Location = new System.Drawing.Point(120, 64);
			this.cmdUnload.Name = "cmdUnload";
			this.cmdUnload.Size = new System.Drawing.Size(112, 32);
			this.cmdUnload.TabIndex = 3;
			this.cmdUnload.Text = "Unload";
			this.cmdUnload.Click += new System.EventHandler(this.cmdUnload_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(24, 112);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(208, 23);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "CommonComponents Tester";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(264, 104);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(288, 32);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Message Generator";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.Location = new System.Drawing.Point(264, 144);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(264, 32);
			this.linkLabel3.TabIndex = 6;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "TCHQ_Client";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 208);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(424, 32);
			this.label2.TabIndex = 7;
			this.label2.Text = "start with arguments : \"- ConfigFilePath\" e.g. -SMD_Config.xml ";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(648, 266);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.cmdUnload);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtConfigFile);
			this.Name = "frmMain";
			this.Text = "Test Host:";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		/// <summary>
		/// Global pm class
		/// </summary>
		PlugInManager pm;

		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				pm = new PlugInManager();
				pm.ConfigFile = txtConfigFile.Text;
				pm.LoadPlugins();
				button1.Enabled = false; 
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + " - " + ex.StackTrace ) ;
			}

		}

		private void cmdUnload_Click(object sender, System.EventArgs e)
		{
			if (pm !=null)
				pm.Unload();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CommonComponentsTest ct = new CommonComponentsTest();
			ct.ConfigFile  = txtConfigFile.Text;
			ct.Show();
		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			SampleMessageGenerator sg = new SampleMessageGenerator();
			sg.ConfigFile= txtConfigFile.Text;
			sg.Show();

		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			TCHQClient tc = new TCHQClient();
			tc.ConfigFile=txtConfigFile.Text;
			tc.Show();
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			string[] aparams = Environment.CommandLine.Split(new char[]{'-'});
			if (aparams.Length>1)
			{
				txtConfigFile.Text=aparams[1].Trim();
			
			}

		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(pm != null)
			{
				pm.Unload();
			}
		}
	}
}
