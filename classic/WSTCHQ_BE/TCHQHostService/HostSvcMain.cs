using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Reflection;
using System.IO;

namespace TCHQHostService
{
	public class HostSvcMain : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HostSvcMain()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new HostSvcMain(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new HostSvcMain() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "TCHQHostService";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		//Main Plugin Manager
		PlugInManager m_PluginManager;

		
		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
			try
			{
				Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			
				m_PluginManager = new PlugInManager();                
                m_PluginManager.ConfigFile= System.Configuration.ConfigurationSettings.AppSettings["ConfigXMLFile"];
				m_PluginManager.LoadPlugins();
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning, "Host Service " + this.ServiceName  + " completed loading all plugins.");
				System.Diagnostics.EventLog.WriteEntry(this.ServiceName,"Host Service " + this.ServiceName  + " completed loading all plugins.", 
					EventLogEntryType.Information);

			}
			catch (Exception Exx)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace);

				System.Diagnostics.EventLog.WriteEntry(this.ServiceName,
					"Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace,
					EventLogEntryType.Warning);
					throw Exx;
			}

		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			try
			{
				m_PluginManager.Unload();
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"Host Service " + this.ServiceName  + " unloaded all plugins." );
				System.Diagnostics.EventLog.WriteEntry(this.ServiceName,"Host Service " + this.ServiceName  + " unloaded all plugins." ,
					EventLogEntryType.Information);
			}
			catch (Exception Exx)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace);
				System.Diagnostics.EventLog.WriteEntry(this.ServiceName,"Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace,
					EventLogEntryType.Warning);

			}
		}
	}
}
