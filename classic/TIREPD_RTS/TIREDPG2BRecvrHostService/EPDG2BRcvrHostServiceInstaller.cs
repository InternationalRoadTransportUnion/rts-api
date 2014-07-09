using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace TIREDPG2BRecvrHostService
{
    [RunInstaller(true)]
    public partial class EPDG2BRcvrHostServiceInstaller : Installer
    {

        public EPDG2BRcvrHostServiceInstaller()
        {
            InitializeComponent();
            this.BeforeInstall += new InstallEventHandler(ProjectInstaller_BeforeInstall);
            this.BeforeUninstall += new InstallEventHandler(ProjectInstaller_BeforeUninstall);
        }

        public void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.B2GRecvrServiceInstaller1.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.B2GRecvrServiceInstaller1.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.B2GRecvrServiceInstaller1.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.B2GRecvrServiceInstaller1.DisplayName);
        }

        public void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Context.Parameters["ServiceName"]))
                this.B2GRecvrServiceInstaller1.ServiceName = this.Context.Parameters["ServiceName"];
            Console.WriteLine("ServiceName=" + this.B2GRecvrServiceInstaller1.ServiceName);
            if (!string.IsNullOrEmpty(this.Context.Parameters["DisplayName"]))
                this.B2GRecvrServiceInstaller1.DisplayName = this.Context.Parameters["DisplayName"];
            Console.WriteLine("DisplayName=" + this.B2GRecvrServiceInstaller1.DisplayName);
        }
    }
}