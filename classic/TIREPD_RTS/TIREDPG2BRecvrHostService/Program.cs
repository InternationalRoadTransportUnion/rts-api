using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Configuration.Install;
using IRU.RTS.Common.Helper;

namespace TIREDPG2BRecvrHostService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new TIREPDG2BRcvrHostService() };

            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                ServiceInstallerConsoleHelper svcInstConsHlp = new ServiceInstallerConsoleHelper(ServicesToRun[0], args);
                svcInstConsHlp.Execute();
            }
        }
    }
}