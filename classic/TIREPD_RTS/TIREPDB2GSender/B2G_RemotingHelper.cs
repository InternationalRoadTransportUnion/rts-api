using System;
using System.Runtime.Remoting;
using IRU.RTS;
using System.Xml;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using IRU.RTS.TIREPD;
using IRU.RTS.Common.WCF; 

namespace IRU.RTS.TIREPD
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class B2G_RemotingHelper : IRU.CommonInterfaces.IRunnable, IRU.CommonInterfaces.IPlugIn
    {
        internal static IDBHelperFactory m_dbHelperFactoryPlugin;//="DBHelperFactory" 

        internal static IRU.CommonInterfaces.ICache m_InMemoryCachePlugin;//="InMemoryCache"

		private NetTcpServiceHost<B2GSender, IB2GSender> _serviceHost = null;

        //if port is 0 dont register a channel as some other plugin is expected to register the channel
        private int m_RemotingPort;//="4000"


        internal static string m_CryptoProviderEndpoint;//="tcp://server:Port/CryptoProvider.rem" 
        private string m_RemotingEndPoint;//="WSTCHQ_Processor.rem"

        private IPlugInManager m_PluginManager;

        private string m_PluginName;

        public static string m_SchemaFilesPath;

        public static string[] m_MessageNameArr;
        public static System.Collections.Hashtable m_hsCountryISO_Subsc_Msg_Info = new System.Collections.Hashtable();
        public static System.Collections.Hashtable m_hsCountryISO_WebPathList = new System.Collections.Hashtable();
        public static System.Collections.Hashtable m_hsCountryISO_AdditionalParams = new System.Collections.Hashtable();

        public B2G_RemotingHelper()
        {
            m_MessageNameArr = null;
            //
            // TODO: Add constructor logic here
            //
        }
        #region IRunnable Members

        public void Start()
        {
            //Register the processor class with Remoting system.
			
			try
			{
				_serviceHost = new NetTcpServiceHost<B2GSender, IB2GSender>(m_RemotingPort, m_RemotingEndPoint);
				_serviceHost.Open();
			}
			catch (Exception ex)
			{
				Stop();

				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
					"Can't start ServiceHost: "
					+ ex.Message);
				throw ex;
			} 
        }

        public void Stop()
        {
            // TODO:  Add TCHQ_RemotingHelper.Stop implementation
			if (_serviceHost != null)
			{
				_serviceHost.Dispose();
				_serviceHost = null;
			}
        }

        #endregion

        #region IPlugIn Members

        public void Configure(IRU.CommonInterfaces.IPlugInManager PluginManager)
        {
            // TODO:  Add B2G_RemotingHelper.Configure implementation

            m_PluginManager = PluginManager;
            XmlNode sectionNode = null; 
            try
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "PluginName:" + m_PluginName);
                sectionNode = m_PluginManager.GetConfigurationSection(
                    m_PluginName);

                XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
                    "./B2G_RemotingHelperSettings");

                string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
                    "DBHelperFactoryPlugin").InnerText;
                m_dbHelperFactoryPlugin = (IDBHelperFactory)m_PluginManager.GetPluginByName(sDBFactoryName);

                string sRemotingPort = XMLHelper.GetAttributeNode(parameterNode,
                    "RemotingPort").InnerText;
                m_RemotingPort = int.Parse(sRemotingPort);

                m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
                    "CryptoProviderEndpoint").InnerText;
                m_RemotingEndPoint = XMLHelper.GetAttributeNode(parameterNode,
                    "RemotingEndPoint").InnerText;

                m_SchemaFilesPath = XMLHelper.GetAttributeNode(parameterNode,
    "SchemaFilesPath").InnerText;

                string sCountryISO_SubscriberId_Info = XMLHelper.GetAttributeNode(parameterNode,
    "CountryISO_SubscriberId_Info").InnerText;

                string[] strListtemp = sCountryISO_SubscriberId_Info.Split(';');

                foreach (string str in strListtemp)
                {
                    if (str.Trim() != "")
                    {
                        string[] strTemp = str.Split(',');
                        m_hsCountryISO_Subsc_Msg_Info.Add(strTemp[0], strTemp);
                    }
                }
                string COUNTRYISO_WebPath_List = XMLHelper.GetAttributeNode(parameterNode,
    "COUNTRYISO_WebPath_List").InnerText;

                string[] strCOUNTRYISO_WebPath_Listtemp = COUNTRYISO_WebPath_List.Split(';');

                foreach (string str in strCOUNTRYISO_WebPath_Listtemp)
                {
                    if (str.Trim() != "")
                    {
                        string[] strTemp = str.Split(',');
                        m_hsCountryISO_WebPathList.Add(strTemp[0], strTemp);
                    }
                }

                #region Additional Country Parameters
                string sCountryISO_AdditionalParams = XMLHelper.GetAttributeNode(parameterNode, "CountryISO_AdditionalParams").InnerText;

                string[] asCountryISO_AddParams = sCountryISO_AdditionalParams.Split(';');

                foreach (string sParams in asCountryISO_AddParams)
                {
                    System.Collections.Specialized.NameValueCollection nvcParams = new System.Collections.Specialized.NameValueCollection();

                    string[] asTemp = sParams.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sParamVal in asTemp)
                    {
                        string[] asParamVal = sParamVal.Split(new char[] {'='}, 2);
                        if (asParamVal.Length ==  2)
                        {
                            nvcParams.Add(asParamVal[0], asParamVal[1]);
                        }
                    }

                    m_hsCountryISO_AdditionalParams.Add(asTemp[0], nvcParams);
                }
                #endregion
            }
            catch (ApplicationException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
                    "XMLNode not found in .Configure of B2G_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch (ArgumentNullException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "PluginName:"+m_PluginName );
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
                    "XMLNode not found in .Configure of B2G_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch (FormatException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "PluginName:" + m_PluginName);
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo,
                    "Invalid value while formating an XMLNode in .Configure of B2G_RemotingHelper"
                    + e.Message);
                throw e;
            }

            //register channels here so that more channels can be registered in config in other plugins
            if (m_RemotingPort == 0)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, " B2G_Remoting helper has not registered channels, expecting other plugin to register a remoting tcpchannel");

            }

        }

        #region Additional Country Parameters
        public static string GetAdditionalParameterValue(string countryISOCode2, string parameterName)
        {
            if (m_hsCountryISO_AdditionalParams[countryISOCode2] is System.Collections.Specialized.NameValueCollection)
            {
                System.Collections.Specialized.NameValueCollection nvcParams = (System.Collections.Specialized.NameValueCollection)m_hsCountryISO_AdditionalParams[countryISOCode2];
                return nvcParams[parameterName];
            }

            return null;
        }
        #endregion

        public void Unload()
        {
            // TODO:  Add TCHQ_RemotingHelper.Unload implementation
        }

        public string PluginName
        {
            get
            {

                return m_PluginName;
            }
            set
            {
                m_PluginName = value;

            }
        }

        #endregion
    }
}
