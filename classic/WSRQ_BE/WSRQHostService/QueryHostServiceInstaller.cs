using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace WSRQHostService
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		private System.ServiceProcess.ServiceInstaller QueryHostServiceInstaller;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProjectInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

            this.BeforeInstall += new InstallEventHandler(ProjectInstaller_BeforeInstall);
            this.BeforeUninstall += new InstallEventHandler(ProjectInstaller_BeforeUninstall);
        }

        public void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.QueryHostServiceInstaller.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.QueryHostServiceInstaller.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.QueryHostServiceInstaller.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.QueryHostServiceInstaller.DisplayName);
        }

        public void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.QueryHostServiceInstaller.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.QueryHostServiceInstaller.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.QueryHostServiceInstaller.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.QueryHostServiceInstaller.DisplayName);
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


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			this.QueryHostServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstaller1
			// 
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			// 
			// QueryHostServiceInstaller
			// 
			this.QueryHostServiceInstaller.DisplayName = "WSRQ Query Host Service ";
			this.QueryHostServiceInstaller.ServiceName = "WSRQHostService";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
																					  this.serviceProcessInstaller1,
																					  this.QueryHostServiceInstaller});

		}
		#endregion
	}
}
