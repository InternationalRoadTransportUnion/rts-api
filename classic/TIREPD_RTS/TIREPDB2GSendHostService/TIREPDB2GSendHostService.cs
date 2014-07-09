using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using IRU.RTS.CommonComponents;
//using IRU.CommonInterfaces;
using IRU.RTS.TIREPD;

namespace TIREPDB2GSendHostService
{
    public partial class TIREPDB2GSendHostService : ServiceBase
    {
        PlugInManager m_PluginManager;

        public TIREPDB2GSendHostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                m_PluginManager = new PlugInManager();
                m_PluginManager.ConfigFile = System.Configuration.ConfigurationManager.AppSettings["ConfigXMLFile"];
                System.Diagnostics.EventLog.WriteEntry(this.ServiceName, "Host Service " + this.ServiceName + " m_PluginManager.ConfigFile =" + m_PluginManager.ConfigFile,
                    EventLogEntryType.Information);
                m_PluginManager.LoadPlugins();
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "Host Service " + this.ServiceName + " completed loading all plugins.");
                System.Diagnostics.EventLog.WriteEntry(this.ServiceName, "Host Service " + this.ServiceName + " completed loading all plugins.",
                    EventLogEntryType.Information);

            }
            catch (Exception Exx)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace);

                System.Diagnostics.EventLog.WriteEntry(this.ServiceName,
                    "Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace,
                    EventLogEntryType.Warning);
                throw Exx;
            }
        }

        protected override void OnStop()
        {
            try
            {
                m_PluginManager.Unload();
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "Host Service " + this.ServiceName + " unloaded all plugins.");
                System.Diagnostics.EventLog.WriteEntry(this.ServiceName, "Host Service " + this.ServiceName + " unloaded all plugins.",
                    EventLogEntryType.Information);
            }
            catch (Exception Exx)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace);
                System.Diagnostics.EventLog.WriteEntry(this.ServiceName, "Exception in Windows Service host :" + this.ServiceName + "\r\n" + Exx.Message + "\r\n" + Exx.StackTrace,
                    EventLogEntryType.Warning);

            }

        }
    }
}