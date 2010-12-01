using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PluginConfigEditCustomAction
{
	/// <summary>
	/// Summary description for ConfigEditor.
	/// </summary>
	public class ConfigEditor : System.Windows.Forms.Form
	{
		internal IRU.Common.Config.PluginConfigCtl.ConfigTabHost configTabHost1;
		private System.Windows.Forms.GroupBox gpControls;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigEditor()
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
            this.configTabHost1 = new IRU.Common.Config.PluginConfigCtl.ConfigTabHost();
            this.gpControls = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.gpControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // configTabHost1
            // 
            this.configTabHost1.AutoScroll = true;
            this.configTabHost1.ConfigFilePath = null;
            this.configTabHost1.Context = null;
            this.configTabHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configTabHost1.EditTemplateFilePath = null;
            this.configTabHost1.Location = new System.Drawing.Point(0, 0);
            this.configTabHost1.Name = "configTabHost1";
            this.configTabHost1.Size = new System.Drawing.Size(800, 488);
            this.configTabHost1.TabIndex = 0;
            // 
            // gpControls
            // 
            this.gpControls.Controls.Add(this.label1);
            this.gpControls.Controls.Add(this.cmdOK);
            this.gpControls.Controls.Add(this.cmdCancel);
            this.gpControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpControls.Location = new System.Drawing.Point(0, 433);
            this.gpControls.Name = "gpControls";
            this.gpControls.Size = new System.Drawing.Size(800, 55);
            this.gpControls.TabIndex = 1;
            this.gpControls.TabStop = false;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Note: Review Information in all the Tabs above";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(608, 16);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(72, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(704, 16);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 24);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ConfigEditor
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.gpControls);
            this.Controls.Add(this.configTabHost1);
            this.Name = "ConfigEditor";
            this.Text = "ConfigEditor";
            this.gpControls.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{

			if (MessageBox.Show(this,"Have you reviewed information in all the TABs above?","Installation",MessageBoxButtons.YesNo)== DialogResult.Yes )
			{
				configTabHost1.SaveFile();
				this.Close();
			}
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show(this,"This will close the editor without saving changes. You might have to edit the configuration file manually to get the product to function. Are you sure you want to Cancel?","Installation",MessageBoxButtons.YesNo)== DialogResult.Yes )
			this.Close();
		
		}
	}
}
