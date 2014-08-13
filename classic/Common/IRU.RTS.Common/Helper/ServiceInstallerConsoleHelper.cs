using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration.Install;

namespace IRU.RTS.Common.Helper
{
    public class ServiceInstallerConsoleHelper
    {
        private ServiceBase _service;
        private List<String> _arguments = new List<string>();
        private ServiceStartMode _defaultServiceStartMode = ServiceStartMode.Manual;
        private ServiceAccount _defaultServiceAccount = ServiceAccount.User;

        public ServiceInstallerConsoleHelper(ServiceBase service, string[] arguments)
        {            
            _service = service;
            _arguments.AddRange(arguments);

            foreach (string arg in _arguments.ToList())
            {
                string s = arg.Trim();

                if (s.Equals("/FORCEINTERACTIVE", StringComparison.InvariantCultureIgnoreCase) || s.Equals("/FI", StringComparison.InvariantCultureIgnoreCase))
                {
                    _arguments.Remove(arg);
                }
            }

            if (_arguments.Count == 0)
            {
                _arguments.Add("/DEFAULT");
            }
        }

        public static bool IsInteractive()
        {
            return Environment.UserInteractive || HasForceInteractiveFlag();
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
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t\t[/FI]");
                    System.Console.Out.WriteLine("\t\t[/FORCEINTERACTIVE]");
                    System.Console.Out.WriteLine("\t\t\tOptional flag to force the interactive mode");
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t/I[NSTALL]");
                    System.Console.Out.WriteLine("\t\tInstall the service");
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t\t[/FI]");
                    System.Console.Out.WriteLine("\t\t[/FORCEINTERACTIVE]");
                    System.Console.Out.WriteLine("\t\t\tOptional flag to force the interactive mode");
                    System.Console.Out.WriteLine("\t\t[/Account=<service_account>]");
                    System.Console.Out.WriteLine("\t\t\tService account:");
                    foreach (var v in Enum.GetValues(typeof(ServiceAccount)))
                    {
                        System.Console.Out.WriteLine("\t\t\t\t{0}{1}", v, v.Equals(_defaultServiceAccount) ? " (default)" : null);
                    }
                    System.Console.Out.WriteLine("\t\t[/username=<account_username>]");
                    System.Console.Out.WriteLine("\t\t\tOptional account username to run the service");
                    System.Console.Out.WriteLine("\t\t[/password=<account_password>]");
                    System.Console.Out.WriteLine("\t\t\tOptional account password to run the service");
                    System.Console.Out.WriteLine("\t\t[/StartType=<startup_type>]");
                    System.Console.Out.WriteLine("\t\t\tStartup type of the service:");
                    foreach (var v in Enum.GetValues(typeof(ServiceStartMode)))
                    {
                        System.Console.Out.WriteLine("\t\t\t\t{0}{1}", v, v.Equals(_defaultServiceStartMode) ? " (default)" : null);
                    }
                    System.Console.Out.WriteLine("\t\t[/ServiceName=<service_alternative_name>]");
                    System.Console.Out.WriteLine("\t\t\tOptional alternative name of the service to install");
                    System.Console.Out.WriteLine("\t\t\tmore than once on a same machine");
                    System.Console.Out.WriteLine("\t\t[/DisplayName=<display_alternative_name>]");
                    System.Console.Out.WriteLine("\t\t\tOptional alternative display name of the service");
                    System.Console.Out.WriteLine("\t\t\tto install more than once on a same machine");                                        
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t/U[NINSTALL]");
                    System.Console.Out.WriteLine("\t\tUninstall the service");
                    System.Console.Out.WriteLine();
                    System.Console.Out.WriteLine("\t\t[/FI]");
                    System.Console.Out.WriteLine("\t\t[/FORCEINTERACTIVE]");
                    System.Console.Out.WriteLine("\t\t\tOptional flag to force the interactive mode");
                    System.Console.Out.WriteLine("\t\t[/ServiceName=<service_alternative_name>]");
                    System.Console.Out.WriteLine("\t\t\tOptional alternative name of the service to uninstall");
                    System.Console.Out.WriteLine("\t\t\ta very specific instance");
                    System.Console.Out.WriteLine("\t\t[/DisplayName=<display_alternative_name>]");
                    System.Console.Out.WriteLine("\t\t\tOptional alternative display name of the service");
                    System.Console.Out.WriteLine("\t\t\tto uninstall a very specific instance");                                        

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
            installer.CommandLine = _arguments.GetRange(1, _arguments.Count - 1).ToArray();
            installer.BeforeInstall += new InstallEventHandler(installer_BeforeInstall);
            
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

        private void installer_BeforeInstall(object sender, InstallEventArgs e)
        {
            if (sender is AssemblyInstaller)
            {
                AssemblyInstaller installer = (AssemblyInstaller)sender;
                ServiceInstaller svcInst = null;
                ServiceProcessInstaller svcProcInst = null;
                Installer inst = installer.Installers.OfType<Installer>().FirstOrDefault();

                if (inst != null)
                {
                    svcInst = inst.Installers.OfType<ServiceInstaller>().FirstOrDefault();
                    svcProcInst = inst.Installers.OfType<ServiceProcessInstaller>().FirstOrDefault();
                }

                if (svcInst != null)
                {
                    if (string.IsNullOrEmpty(installer.Context.Parameters["StartType"]))
                    {
                        svcInst.StartType = _defaultServiceStartMode;
                    }
                    else
                    {
                        svcInst.StartType = (ServiceStartMode)Enum.Parse(typeof(ServiceStartMode), installer.Context.Parameters["StartType"], true);
                    }
                }

                if (svcProcInst != null)
                {
                    if (string.IsNullOrEmpty(installer.Context.Parameters["Account"]))
                    {
                        svcProcInst.Account = _defaultServiceAccount;
                    }
                    else
                    {
                        svcProcInst.Account = (ServiceAccount)Enum.Parse(typeof(ServiceAccount), installer.Context.Parameters["Account"], true);
                    }
                }
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

        private static bool HasForceInteractiveFlag()
        {
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                string s = arg.Trim();
                if (s.Equals("/FORCEINTERACTIVE", StringComparison.InvariantCultureIgnoreCase) || s.Equals("/FI", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}