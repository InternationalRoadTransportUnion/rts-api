using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using RTSSetup;

namespace RTSSetup
{
	/// <summary>
	/// Summary description for RTSSetup.
	/// </summary>
	public class RTSSetup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.LinkLabel lnkWSSTProcessor;
		private System.Windows.Forms.LinkLabel lnkWSCsccJobListener;
		private System.Windows.Forms.LinkLabel lnkInternalAlertProcessor;
		private System.Windows.Forms.LinkLabel lnkCryptoHost;
		private System.Windows.Forms.LinkLabel lnkCryptoHost2;
		private System.Windows.Forms.LinkLabel lnkWSSTFileReceiver;
		private System.Windows.Forms.LinkLabel lnkWSCsccClient;
		private System.Windows.Forms.LinkLabel lnkExternalAlertProcessor;
		private System.Windows.Forms.LinkLabel lnkWSSTWebService;
		private System.Windows.Forms.LinkLabel lnkTCHQWebService;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkWSE;
		private System.Windows.Forms.LinkLabel lnkAdminClient;
		private System.Windows.Forms.LinkLabel lnkTCHQ;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel4;
		
		
		
		
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RTSSetup()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTSSetup));
            this.cmdClose = new System.Windows.Forms.Button();
            this.lnkWSSTProcessor = new System.Windows.Forms.LinkLabel();
            this.lnkWSCsccJobListener = new System.Windows.Forms.LinkLabel();
            this.lnkInternalAlertProcessor = new System.Windows.Forms.LinkLabel();
            this.lnkCryptoHost = new System.Windows.Forms.LinkLabel();
            this.lnkCryptoHost2 = new System.Windows.Forms.LinkLabel();
            this.lnkWSSTFileReceiver = new System.Windows.Forms.LinkLabel();
            this.lnkWSCsccClient = new System.Windows.Forms.LinkLabel();
            this.lnkExternalAlertProcessor = new System.Windows.Forms.LinkLabel();
            this.lnkWSSTWebService = new System.Windows.Forms.LinkLabel();
            this.lnkTCHQWebService = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkWSE = new System.Windows.Forms.LinkLabel();
            this.lnkAdminClient = new System.Windows.Forms.LinkLabel();
            this.lnkTCHQ = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(632, 280);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 32);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click_1);
            // 
            // lnkWSSTProcessor
            // 
            this.lnkWSSTProcessor.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSSTProcessor.Location = new System.Drawing.Point(16, 149);
            this.lnkWSSTProcessor.Name = "lnkWSSTProcessor";
            this.lnkWSSTProcessor.Size = new System.Drawing.Size(120, 16);
            this.lnkWSSTProcessor.TabIndex = 7;
            this.lnkWSSTProcessor.TabStop = true;
            this.lnkWSSTProcessor.Tag = "WSSTProcessor.msi";
            this.lnkWSSTProcessor.Text = "2. WSSTProcessor";
            this.lnkWSSTProcessor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkWSCsccJobListener
            // 
            this.lnkWSCsccJobListener.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSCsccJobListener.Location = new System.Drawing.Point(16, 178);
            this.lnkWSCsccJobListener.Name = "lnkWSCsccJobListener";
            this.lnkWSCsccJobListener.Size = new System.Drawing.Size(120, 16);
            this.lnkWSCsccJobListener.TabIndex = 7;
            this.lnkWSCsccJobListener.TabStop = true;
            this.lnkWSCsccJobListener.Tag = "WSCsccJobListener.msi";
            this.lnkWSCsccJobListener.Text = "3. WSCsccJobListener";
            this.lnkWSCsccJobListener.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkInternalAlertProcessor
            // 
            this.lnkInternalAlertProcessor.BackColor = System.Drawing.Color.Transparent;
            this.lnkInternalAlertProcessor.Location = new System.Drawing.Point(16, 207);
            this.lnkInternalAlertProcessor.Name = "lnkInternalAlertProcessor";
            this.lnkInternalAlertProcessor.Size = new System.Drawing.Size(128, 16);
            this.lnkInternalAlertProcessor.TabIndex = 7;
            this.lnkInternalAlertProcessor.TabStop = true;
            this.lnkInternalAlertProcessor.Tag = "InternalAlertProcessor.msi";
            this.lnkInternalAlertProcessor.Text = "4. InternalAlertProcessor";
            this.lnkInternalAlertProcessor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkCryptoHost
            // 
            this.lnkCryptoHost.BackColor = System.Drawing.Color.Transparent;
            this.lnkCryptoHost.Location = new System.Drawing.Point(368, 120);
            this.lnkCryptoHost.Name = "lnkCryptoHost";
            this.lnkCryptoHost.Size = new System.Drawing.Size(120, 16);
            this.lnkCryptoHost.TabIndex = 7;
            this.lnkCryptoHost.TabStop = true;
            this.lnkCryptoHost.Tag = "CryptoHost.msi";
            this.lnkCryptoHost.Text = "1. CryptoHost";
            this.lnkCryptoHost.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkCryptoHost2
            // 
            this.lnkCryptoHost2.BackColor = System.Drawing.Color.Transparent;
            this.lnkCryptoHost2.Location = new System.Drawing.Point(16, 120);
            this.lnkCryptoHost2.Name = "lnkCryptoHost2";
            this.lnkCryptoHost2.Size = new System.Drawing.Size(120, 16);
            this.lnkCryptoHost2.TabIndex = 7;
            this.lnkCryptoHost2.TabStop = true;
            this.lnkCryptoHost2.Tag = "CryptoHost.msi";
            this.lnkCryptoHost2.Text = "1. CryptoHost";
            this.lnkCryptoHost2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkWSSTFileReceiver
            // 
            this.lnkWSSTFileReceiver.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSSTFileReceiver.Location = new System.Drawing.Point(368, 146);
            this.lnkWSSTFileReceiver.Name = "lnkWSSTFileReceiver";
            this.lnkWSSTFileReceiver.Size = new System.Drawing.Size(120, 16);
            this.lnkWSSTFileReceiver.TabIndex = 7;
            this.lnkWSSTFileReceiver.TabStop = true;
            this.lnkWSSTFileReceiver.Tag = "WSSTFileReceiver.msi";
            this.lnkWSSTFileReceiver.Text = "2. WSSTFileReceiver";
            this.lnkWSSTFileReceiver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkWSCsccClient
            // 
            this.lnkWSCsccClient.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSCsccClient.Location = new System.Drawing.Point(368, 227);
            this.lnkWSCsccClient.Name = "lnkWSCsccClient";
            this.lnkWSCsccClient.Size = new System.Drawing.Size(120, 16);
            this.lnkWSCsccClient.TabIndex = 7;
            this.lnkWSCsccClient.TabStop = true;
            this.lnkWSCsccClient.Tag = "WSCsccClient.msi";
            this.lnkWSCsccClient.Text = "5. WSCsccClient";
            this.lnkWSCsccClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkExternalAlertProcessor
            // 
            this.lnkExternalAlertProcessor.BackColor = System.Drawing.Color.Transparent;
            this.lnkExternalAlertProcessor.Location = new System.Drawing.Point(368, 198);
            this.lnkExternalAlertProcessor.Name = "lnkExternalAlertProcessor";
            this.lnkExternalAlertProcessor.Size = new System.Drawing.Size(144, 16);
            this.lnkExternalAlertProcessor.TabIndex = 7;
            this.lnkExternalAlertProcessor.TabStop = true;
            this.lnkExternalAlertProcessor.Tag = "ExternalAlertProcessor.msi";
            this.lnkExternalAlertProcessor.Text = "4. ExternalAlertProcessor";
            this.lnkExternalAlertProcessor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkWSSTWebService
            // 
            this.lnkWSSTWebService.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSSTWebService.Location = new System.Drawing.Point(656, 152);
            this.lnkWSSTWebService.Name = "lnkWSSTWebService";
            this.lnkWSSTWebService.Size = new System.Drawing.Size(120, 16);
            this.lnkWSSTWebService.TabIndex = 7;
            this.lnkWSSTWebService.TabStop = true;
            this.lnkWSSTWebService.Tag = "WSST_WEB.msi";
            this.lnkWSSTWebService.Text = "WSST Web Service";
            this.lnkWSSTWebService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkTCHQWebService
            // 
            this.lnkTCHQWebService.BackColor = System.Drawing.Color.Transparent;
            this.lnkTCHQWebService.Location = new System.Drawing.Point(656, 120);
            this.lnkTCHQWebService.Name = "lnkTCHQWebService";
            this.lnkTCHQWebService.Size = new System.Drawing.Size(120, 16);
            this.lnkTCHQWebService.TabIndex = 7;
            this.lnkTCHQWebService.TabStop = true;
            this.lnkTCHQWebService.Tag = "TCHQ_WEB.msi";
            this.lnkTCHQWebService.Text = "TCHQ Web Service";
            this.lnkTCHQWebService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(152, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Admin Client:";
            // 
            // lnkWSE
            // 
            this.lnkWSE.BackColor = System.Drawing.Color.Transparent;
            this.lnkWSE.Location = new System.Drawing.Point(152, 288);
            this.lnkWSE.Name = "lnkWSE";
            this.lnkWSE.Size = new System.Drawing.Size(184, 16);
            this.lnkWSE.TabIndex = 9;
            this.lnkWSE.TabStop = true;
            this.lnkWSE.Tag = "Microsoft WSE 2.0 SP2 Runtime.msi";
            this.lnkWSE.Text = "1. Microsoft WSE SP2";
            this.lnkWSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkAdminClient
            // 
            this.lnkAdminClient.BackColor = System.Drawing.Color.Transparent;
            this.lnkAdminClient.Location = new System.Drawing.Point(152, 312);
            this.lnkAdminClient.Name = "lnkAdminClient";
            this.lnkAdminClient.Size = new System.Drawing.Size(184, 16);
            this.lnkAdminClient.TabIndex = 9;
            this.lnkAdminClient.TabStop = true;
            this.lnkAdminClient.Tag = "AdminClient.msi";
            this.lnkAdminClient.Text = "2. Admin Client";
            this.lnkAdminClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // lnkTCHQ
            // 
            this.lnkTCHQ.BackColor = System.Drawing.Color.Transparent;
            this.lnkTCHQ.Location = new System.Drawing.Point(368, 172);
            this.lnkTCHQ.Name = "lnkTCHQ";
            this.lnkTCHQ.Size = new System.Drawing.Size(120, 16);
            this.lnkTCHQ.TabIndex = 7;
            this.lnkTCHQ.TabStop = true;
            this.lnkTCHQ.Tag = "TCHQ.msi";
            this.lnkTCHQ.Text = "3. TCHQ";
            this.lnkTCHQ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Location = new System.Drawing.Point(656, 178);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(120, 16);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "WSRQ_WEB.msi";
            this.linkLabel1.Text = "WSRQ Web Service";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.Location = new System.Drawing.Point(368, 256);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(120, 16);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "WSRQ.msi";
            this.linkLabel2.Text = "6. WSRQ";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Location = new System.Drawing.Point(368, 280);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(120, 16);
            this.linkLabel3.TabIndex = 12;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "WSREFileReceiver.msi";
            this.linkLabel3.Text = "7. WSREFileReceiver";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // linkLabel4
            // 
            this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel4.Location = new System.Drawing.Point(16, 239);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(120, 16);
            this.linkLabel4.TabIndex = 13;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Tag = "WSREProcessor.msi";
            this.linkLabel4.Text = "5. WSREProcessor";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstall_Click);
            // 
            // RTSSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(794, 344);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lnkWSE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkWSSTProcessor);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lnkWSCsccJobListener);
            this.Controls.Add(this.lnkInternalAlertProcessor);
            this.Controls.Add(this.lnkCryptoHost);
            this.Controls.Add(this.lnkCryptoHost2);
            this.Controls.Add(this.lnkWSSTFileReceiver);
            this.Controls.Add(this.lnkWSCsccClient);
            this.Controls.Add(this.lnkExternalAlertProcessor);
            this.Controls.Add(this.lnkWSSTWebService);
            this.Controls.Add(this.lnkTCHQWebService);
            this.Controls.Add(this.lnkAdminClient);
            this.Controls.Add(this.lnkTCHQ);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RTSSetup";
            this.Text = "RTS Setup";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RTSSetup_Closing);
            this.Load += new System.EventHandler(this.RTSSetup_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void RTSSetup_Load(object sender, System.EventArgs e)
		{
			

		}

		public static void Main()
		{
			Application.Run(new RTSSetup());
		}

	
////
////		System.Diagnostics.Process.Start(@"M:\mcs-client\code\IRU\RTS\InstallationMSIs\ExternalAlertProcessor\Debug\ExternalAlertProcessor.msi");
	

		System.Diagnostics.Process currentInstall;
		private void StartInstallation(string MSIName)
		{
			#region Exited Event is not getting hooked
////			if (currentInstall!=null)
////			{
////				MessageBox.Show(this,"Installation is in progress, cannot start new install.");
////				return;
////			
////			}
			#endregion
		currentInstall = System.Diagnostics.Process.Start(MSIName);
////		currentInstall.Exited+=new EventHandler(currentInstall_Exited);
		
		
		}
		#region Event is not getting hooked so this code is commented

////		private void currentInstall_Exited(object sender, EventArgs e)
////		{
////			MessageBox.Show("Exit code " + currentInstall.ExitCode.ToString());
////			currentInstall.Dispose();
////			currentInstall= null;
////		}

		#endregion

		private void lnkInstall_Click(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			StartInstallation(((LinkLabel)sender).Tag.ToString());
		}

		private void cmdClose_Click_1(object sender, System.EventArgs e)
		{
			Application.Exit();
		
		}

		private void RTSSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
//////			if( currentInstall!=null)
//////			{
//////					if (MessageBox.Show("Install is in progress do you want to close this application?","RTS Setup",MessageBoxButtons.YesNo)==DialogResult.No)
//////			 {
//////					e.Cancel=true;
//////						return;
//////				}
//////
//////			}
			e.Cancel=false;
		}

	}
}
