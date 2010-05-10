using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using IRU.RTS;
using IRU.RTS.CommonComponents;

namespace Test_FileMover
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtConfigFile;
		private System.Windows.Forms.Button cmdLoad;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
			this.cmdLoad = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtConfigFile
			// 
			this.txtConfigFile.Location = new System.Drawing.Point(104, 24);
			this.txtConfigFile.Name = "txtConfigFile";
			this.txtConfigFile.Size = new System.Drawing.Size(296, 20);
			this.txtConfigFile.TabIndex = 0;
			this.txtConfigFile.Text = "Test_fileMover.xml";
			// 
			// cmdLoad
			// 
			this.cmdLoad.Location = new System.Drawing.Point(416, 24);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.Size = new System.Drawing.Size(104, 24);
			this.cmdLoad.TabIndex = 1;
			this.cmdLoad.Text = "Load";
			this.cmdLoad.Click += new System.EventHandler(this.Load_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Path to Config File";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 190);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdLoad);
			this.Controls.Add(this.txtConfigFile);
			this.Name = "Form1";
			this.Text = "Main Form";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		PlugInManager m_pluginManager;
		private void Load_Click(object sender, System.EventArgs e)
		{
		
			m_pluginManager = new PlugInManager();
			m_pluginManager.ConfigFile=txtConfigFile.Text;
			m_pluginManager.LoadPlugins();
		}
	}
}
