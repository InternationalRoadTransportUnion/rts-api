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

namespace IRU.RTS.TIREPD
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class G2B_RemotingHelper : IRU.CommonInterfaces.IRunnable, IRU.CommonInterfaces.IPlugIn
    {
        internal static IDBHelperFactory m_dbHelperFactoryPlugin;//="DBHelperFactory" 

        internal static IRU.CommonInterfaces.ICache m_InMemoryCachePlugin;//="InMemoryCache" 

        //if port is 0 dont register a channel as some other plugin is expected to register the channel
        private int m_RemotingPort;//="4000"


        internal static string m_CryptoProviderEndpoint;//="tcp://server:Port/CryptoProvider.rem" 
        private string m_RemotingEndPoint;//="WSTCHQ_Processor.rem"

        private IPlugInManager m_PluginManager;

        private string m_PluginName;

        private string m_SchemaFilesPath;

        public static string[] m_MessageNameArr;
        
        public static System.Collections.Hashtable m_hsCountryISO_INMessagePath = new System.Collections.Hashtable();

        public G2B_RemotingHelper()
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

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(G2BReceiver), m_RemotingEndPoint, WellKnownObjectMode.SingleCall);

//IE14    http://tempuri.org/XMLSchema.xsd
//IE15    http://www.iru.org/TIREPD
//IE29    http://tempuri.org/XMLSchema.xsd
//IE16    http://tempuri.org/XMLSchema.xsd
//IE28    http://tempuri.org/XMLSchema.xsd
//IE928   ttp://tempuri.org/XMLSchema.xsd


            //

            //Read the schema files into XMLHelper
            //string QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE014.xsd";
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE015.xsd";
            //XMLValidationHelper.PopulateSchemas("http://www.iru.org/TIREPD", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE016.xsd";
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE028.xsd";
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE029.xsd";
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE928.xsd";
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);

            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE928.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE917.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE028.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE016.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE060.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE055.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE029.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE051.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE009.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE004.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);
            //QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE005.xsd"; 
            //XMLValidationHelper.PopulateSchemas("http://tempuri.org/XMLSchema.xsd", QuerySchemaPath);				

            // per Mail from Matthieu
            // But we do not read all schemas in the folder only the ones specified below.
            string QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE014.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE014");
            QuerySchemaPath = m_SchemaFilesPath + "\\TIREPD_IE015.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE015");

            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE928.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE928");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE917.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE917");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE028.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE028");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE016.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE016");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE060.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE060");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE055.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE055");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE029.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE029");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE051.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE051");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE009.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE009");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE004.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE004");
            QuerySchemaPath = m_SchemaFilesPath + " \\TIREPD_IE005.xsd";
            EPDXMLValidationHelper.PopulateSchemas(null, QuerySchemaPath, "IE005");				

        }

        public void Stop()
        {
            // TODO:  Add TCHQ_RemotingHelper.Stop implementation
        }

        #endregion

        #region IPlugIn Members

        public void Configure(IRU.CommonInterfaces.IPlugInManager PluginManager)
        {
            // TODO:  Add G2B_RemotingHelper.Configure implementation

            m_PluginManager = PluginManager;
            try
            {
                XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
                    m_PluginName);

                XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
                    "./G2B_RemotingHelperSettings");

                string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
                    "DBHelperFactoryPlugin").InnerText;
                m_dbHelperFactoryPlugin = (IDBHelperFactory)m_PluginManager.GetPluginByName(sDBFactoryName);

                //string sCacheName = XMLHelper.GetAttributeNode(parameterNode,
                //    "InMemoryCachePlugin").InnerText;

                //m_InMemoryCachePlugin = (ICache)m_PluginManager.GetPluginByName(sCacheName);

                string sRemotingPort = XMLHelper.GetAttributeNode(parameterNode,
                    "RemotingPort").InnerText;
                m_RemotingPort = int.Parse(sRemotingPort);

                m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
                    "CryptoProviderEndpoint").InnerText;
                m_RemotingEndPoint = XMLHelper.GetAttributeNode(parameterNode,
                    "RemotingEndPoint").InnerText;

                m_SchemaFilesPath = XMLHelper.GetAttributeNode(parameterNode,
                    "SchemaFilesPath").InnerText;

                string sMessageNamesList = XMLHelper.GetAttributeNode(parameterNode,
                "MessageNamesList").InnerText;
                m_MessageNameArr = sMessageNamesList.Split(',');

                string [] sCtryIsoInMessagePath = XMLHelper.GetAttributeNode(parameterNode,
                "CountryISO_INMessagePath").InnerText.Split(';');
                foreach (string str in sCtryIsoInMessagePath)
                {
                    if (str.Trim().Length > 0)
                    {
                        string[] ctryPath = str.Split(',');
                        if (ctryPath.Length == 3)
                        {
                            m_hsCountryISO_INMessagePath.Add(ctryPath[1], ctryPath);
                        }
                    }
                }

            }
            catch (ApplicationException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
                    "XMLNode not found in .Configure of G2B_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch (ArgumentNullException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
                    "XMLNode not found in .Configure of G2B_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch (FormatException e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo,
                    "Invalid value while formating an XMLNode in .Configure of G2B_RemotingHelper"
                    + e.Message);
                throw e;
            }

            //register channels here so that more channels can be registered in config in other plugins
            if (m_RemotingPort != 0)
            {
                BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
                provider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                // Creating the IDictionary to set the port on the channel instance.
                System.Collections.IDictionary props = new System.Collections.Hashtable();
                props["port"] = m_RemotingPort;

                //TcpChannel chan = new TcpChannel(m_RemotingPort);
                TcpChannel chan = new TcpChannel(props, null, provider);
                ChannelServices.RegisterChannel(chan,false);
            }
            else
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, " G2B_Remoting helper has not registered channels, expecting other plugin to register a remoting tcpchannel");

            }

        }

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
