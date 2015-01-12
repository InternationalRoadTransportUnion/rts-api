using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient ;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Linq;
using IRU;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS;

//using IRU.CryptEngine;

namespace IRU.RTS.WSRQ
{
	/// <summary>
    /// Lata Created on Oct 10,2007 for uploading new requests file.
	/// </summary>
    /// 
      public class WSRQDetailsStruct
        {
            public string RequestID;
            public DateTime RequestDate;
            public int RequestReminderNum;
            public int RequestDataSource;
            public string TNO;
            public string ICC;
            public DateTime DCL;
            public string CNL;
            public string COF;
            public DateTime DDI;
            public string RND;
            public string PFD;
            public string TCO;
            public string CWR;
            public int VPN;
            public string COM;
            public string RBC;
            public int PIC;
            public string RequestRemark;
        };
	public class WSRQNewRequest_Processor:IPlugIn,IRunnable,IActiveObject
	{

		#region Declare Variables

		    internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 
//            private string m_DispatchFileDropPath; 
//		    private string m_TemporaryFolderPath;

		    //private string m_CryptoProviderEndpoint;//
		    private IPlugInManager m_PluginManager;
		    private string m_PluginName;
		    private string m_SchemaFilesPath;
            
 
	    #endregion

		public WSRQNewRequest_Processor()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members

		public void Configure(IPlugInManager pluginManager)
		{
			m_PluginManager = pluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./WSRQNewRequest_ProcessorSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;

				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				//m_TemporaryFolderPath =XMLHelper.GetAttributeNode(parameterNode,
				//	"TemporaryFolderPath").InnerText;

				//if (m_TemporaryFolderPath.LastIndexOf("\\")!= m_TemporaryFolderPath.Length-1)
				//	m_TemporaryFolderPath+="\\";

				//m_DispatchFileDropPath = XMLHelper.GetAttributeNode(parameterNode,
				//	"DispatchFileDropPath").InnerText;

				//if (m_DispatchFileDropPath.LastIndexOf("\\")!= m_DispatchFileDropPath.Length-1)
				//	m_DispatchFileDropPath+="\\";

				//m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
				//	"CryptoProviderEndpoint").InnerText;

				//Read the schema files into XMLHelper
				m_SchemaFilesPath = XMLHelper.GetAttributeNode(parameterNode,
					"SchemaFilesPath").InnerText;

				if (m_SchemaFilesPath.LastIndexOf("\\")!= m_SchemaFilesPath.Length-1)
					m_SchemaFilesPath+="\\";

                string QuerySchemaPath = m_SchemaFilesPath + "SafeTIRReconciliation.xsd";
                XMLValidationHelper.PopulateSchemas("http://www.iru.org/SafeTIRReconciliation", QuerySchemaPath, new string[] { null, "WSRQNewRequest" });
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSRQNewRequest_Processor"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
                    "XMLNode not found in .Configure of WSRQNewRequest_Processor"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of WSRE_Processor"
					+ e.Message);
				throw e;
			}

		}

		public void Unload()
		{
			// TODO:  Add WSRQNewRequest_Processor.Unload implementation
		}

		public bool Enqueue(object objFileContents)
		{
			//TODO: This method is called by the FileSystemListener through the IActiveObject.Enqueue. 
			//Call the ProcessUploadJob Method. 
			return true;
		}

		public string PluginName
		{
			get
			{
				return m_PluginName;
			}
			set
			{
				m_PluginName=value;
			}
		}

		#endregion


		#region IRunnable Members

		public void Start()
		{
			// TODO:  Add WSRQNewRequest_Processor.Start implementation
		}

		public void Stop()
		{
            // TODO:  Add WSRQNewRequest_Processor.Stop implementation
		}

		#endregion

		#region IActiveObject Members

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue, string queueName)
		{
            // TODO:  Add WSRQNewRequest_Processor.IRU.CommonInterfaces.IActiveObject.Enqueue implementation
		}

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue)
		{
			ProcessUploadJob(objToEnqueue); 
		}

		#endregion

		#region ProcessUploadJob

		private void ProcessUploadJob(object objFileContents)
		{
            try
            {
                #region Variables
                WSRQDetailsStruct wsrqdtl = new WSRQDetailsStruct();
                string sFailureReason;
                #endregion

                #region Extract File Contents and Name from object

                Hashtable htFileContents = (Hashtable)objFileContents;
                string baFileName = htFileContents["FileName"].ToString();
                byte[] baFileContents = (byte[])htFileContents["FileContents"];

                #endregion

                
                #region Create DB helper Instances
                IDBHelper dbHelperWSRQNewRequest = m_dbHelperFactoryPlugin.GetDBHelper("WSRQDB");//  null; //dbhelper for tchqdb
                WSRQNewRequest_DBHelper WSRQNewRequestDbHelper = new WSRQNewRequest_DBHelper(dbHelperWSRQNewRequest);
                #endregion


                #region Split String
                string sFileContents = System.Text.Encoding.Unicode.GetString(baFileContents);
                sFileContents = sFileContents.Trim();
                #endregion

                #region validate xsd
                try
                {
                    XMLValidationHelper xvh = new XMLValidationHelper();
                    if (!xvh.ValidateXML(sFileContents, out sFailureReason))
                    {
                        throw new ApplicationException(sFailureReason);
                    }
                }
                catch (Exception ex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                    throw ex;
                
                }
                #endregion

                #region insert into wsrq_details
                wsrqdtl = FillWSRQDetailsStruct(sFileContents);
                try
                {
                    dbHelperWSRQNewRequest.ConnectToDB();
					try
					{
						dbHelperWSRQNewRequest.BeginTransaction();
						WSRQNewRequestDbHelper.InsertRequest(wsrqdtl);
						dbHelperWSRQNewRequest.CommitTransaction();
					}
					finally
					{
						dbHelperWSRQNewRequest.Close();
					}
                }
                catch (IOException oe)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, oe.Message + " - " + oe.StackTrace);
                    throw oe;
                }
                catch (SqlException sqlex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, sqlex.Message + " - " + sqlex.StackTrace);
                    throw sqlex;
                }
                catch (Exception ex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                    throw ex;
                }
                #endregion

              /*  #region Move file to Dispatch folder
                    strFile = CreateDispatchedFile(baFileName, sFileContents);
                #endregion*/
            }
            catch (IOException ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                throw ex;
                
            }

        }
        #endregion

        #region private methods
        private WSRQDetailsStruct FillWSRQDetailsStruct(string bFileContents)
        {
            WSRQDetailsStruct wds1 = new WSRQDetailsStruct();
            //IRU_EncryptDecrypt enc = new IRU_EncryptDecrypt();
            
            try
            {
				XDocument xdDoc = XDocument.Parse(bFileContents);
				XElement xeRoot = xdDoc.Root;
				XElement xeRequestRecord = xeRoot.Descendants().Where(xe => xe.Name.LocalName == "RequestRecord").FirstOrDefault();
				if (xeRequestRecord == null)
				{
					throw new NullReferenceException("<RequestRecord> node not found!");
				}

				XAttribute xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RequestID").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.RequestID = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RequestDate").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.RequestDate = (DateTime)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RequestReminderNum").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.RequestReminderNum = (int)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RequestDataSource").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.RequestDataSource = (int)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "TNO").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.TNO = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "ICC").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.ICC = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "DCL").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.DCL = (DateTime)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "CNL").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.CNL = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "COF").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.COF = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "DDI").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.DDI = (DateTime)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RND").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.RND = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "PFD").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.PFD = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "TCO").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.TCO = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "CWR").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.CWR = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "VPN").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.VPN = (int)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "COM").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.COM = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RBC").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.RBC = ((string)xaValue).Trim();
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "PIC").FirstOrDefault();
				if (xaValue != null)
				{
					wds1.PIC = (int)xaValue;
				}

				xaValue = xeRequestRecord.Attributes().Where(xa => xa.Name.LocalName == "RequestRemark").FirstOrDefault();
				if (xaValue != null && !String.IsNullOrEmpty(xaValue.Value))
				{
					wds1.RequestRemark = ((string)xaValue).Trim();
				}
            }
            catch(IOException ex)
            {
                throw ex;
            }
            return wds1;

        }


        /*  private string CreateDispatchedFile(string filename2,string aFileContents)
          {


              string FileName =filename2.Substring(0,filename2.LastIndexOf("."))+ "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".req";
              StreamWriter sw1 = null;
              try
              {
                  //XMLValidationHelper.ChangeEncoding(filename2, "UNICODE");
                  string file1 = m_TemporaryFolderPath + FileName;
                  sw1= new StreamWriter(file1,false,System.Text.UnicodeEncoding.Unicode);
                  sw1.Write(aFileContents);
              }
              catch (SqlException sqlEx)
              {
                  throw sqlEx;
              }

              finally
              {
                  sw1.Close();
              }
              File.Move(m_TemporaryFolderPath + FileName, m_DispatchFileDropPath + FileName);

              return FileName;
          }*/
        #endregion







    }
}
