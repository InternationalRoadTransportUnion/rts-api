using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace EGISHostService
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		private System.ServiceProcess.ServiceInstaller EGISHostServiceInstaller;
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
                this.EGISHostServiceInstaller.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.EGISHostServiceInstaller.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.EGISHostServiceInstaller.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.EGISHostServiceInstaller.DisplayName);
        }

        public void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.EGISHostServiceInstaller.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.EGISHostServiceInstaller.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.EGISHostServiceInstaller.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.EGISHostServiceInstaller.DisplayName);
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
            this.EGISHostServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // EGISHostServiceInstaller
            // 
            this.EGISHostServiceInstaller.DisplayName = "WSEGIS Host Service ";
            this.EGISHostServiceInstaller.ServiceName = "EGISHostService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.EGISHostServiceInstaller});

		}
		#endregion
	}
}
