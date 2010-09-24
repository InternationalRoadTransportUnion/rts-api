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
        }
    }
}