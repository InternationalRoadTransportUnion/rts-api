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
        }
    }
}