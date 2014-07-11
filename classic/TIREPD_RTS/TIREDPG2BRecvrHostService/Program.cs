using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Configuration.Install;

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

    class ServiceInstallerConsoleHelper
    {
        private ServiceBase _service;
        private List<String> _arguments = new List<string>();

        public ServiceInstallerConsoleHelper(ServiceBase service, string[] arguments)
        {
            _service = service;
            _arguments.AddRange(arguments);

            if (_arguments.Count == 0)
            {
                _arguments.Add("/HELP");
            }
        }

        public void Execute()
        {
            switch (_arguments[0].ToUpperInvariant().Trim())
            {
                case "/HELP":
                case "/H":
                case "/?":
                    System.Console.Out.WriteLine("HELP:");
                    System.Console.Out.WriteLine("=====");
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t/H[ELP]");
                    System.Console.Out.WriteLine("\t/?");
                    System.Console.Out.WriteLine("\t\tShow this help");
                    System.Console.Out.WriteLine("\t/I[NSTALL] [/username=<account_username>] [/password=<account_password>]");
                    System.Console.Out.WriteLine("\t\tInstall the service");
                    System.Console.Out.WriteLine("\t\t/username and /password are optionnal parameters");
                    System.Console.Out.WriteLine("\t/U[NINSTALL]");
                    System.Console.Out.WriteLine("\t\tUninstall the service");

                    break;
                case "/INSTALL":
                case "/I":
                    System.Console.Out.WriteLine("INSTALLATION...");

                    InstallService();

                    break;
                case "/UNINSTALL":
                case "/U":
                    System.Console.Out.WriteLine("UNINSTALLATION...");

                    UninstallService();

                    break;
                default:
                    System.Console.Out.WriteLine("UNSUPPORTED...");
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t/H[ELP]");
                    System.Console.Out.WriteLine("\t/?");
                    System.Console.Out.WriteLine("\t\tShow the help");

                    break;
            }
        }

        private AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(
                _service.GetType().Assembly, null);
            installer.UseNewContext = true;            
            installer.CommandLine = _arguments.GetRange(1, _arguments.Count-1).ToArray();

            return installer;
        }

        private void InstallService()
        {
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        Console.Error.WriteLine(ex.StackTrace);

                        try
                        {
                            installer.Rollback(state);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void UninstallService()
        {
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        Console.Error.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}