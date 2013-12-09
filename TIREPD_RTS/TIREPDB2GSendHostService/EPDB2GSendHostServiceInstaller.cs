using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace TIREPDB2GSendHostService
{
    [RunInstaller(true)]
    public partial class EPDB2GSendHostServiceInstaller : Installer
    {
        public EPDB2GSendHostServiceInstaller()
        {
            InitializeComponent();
            this.BeforeInstall += new InstallEventHandler(ProjectInstaller_BeforeInstall);
            this.BeforeUninstall += new InstallEventHandler(ProjectInstaller_BeforeUninstall);
        }

        public void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.B2GSendserviceInstaller1.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.B2GSendserviceInstaller1.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.B2GSendserviceInstaller1.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.B2GSendserviceInstaller1.DisplayName);
        }

        public void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.B2GSendserviceInstaller1.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.B2GSendserviceInstaller1.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.B2GSendserviceInstaller1.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.B2GSendserviceInstaller1.DisplayName);
        }
    }
}