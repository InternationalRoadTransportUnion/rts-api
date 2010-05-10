using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient ;
using System.Data;
using System.IO;
using IRU;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS;
using System.Text.RegularExpressions;
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

                string QuerySchemaPath = m_SchemaFilesPath + "ReconciliationQueryData.xsd";
                XMLValidationHelper.PopulateSchemas("http://www.iru.org/SafeTIRUpload", QuerySchemaPath);
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
                    dbHelperWSRQNewRequest.BeginTransaction();
                    WSRQNewRequestDbHelper.InsertRequest(wsrqdtl);
                    dbHelperWSRQNewRequest.CommitTransaction();
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
             /*   XmlDocument doc = new XmlDocument();
                XmlNodeList xl1;
                xl1 = null;
                doc.LoadXml(bFileContents);
                string aFileContents="";
                XmlNamespaceManager xns1 = new XmlNamespaceManager(doc.NameTable);
                xns1.AddNamespace("def", "http://www.iru.org/SafeTIRUpload");
                XmlNode querynode = doc.DocumentElement.SelectSingleNode("/def:SafeTIRReconciliation/def:Body/def:RequestRecord", xns1);
                for (int i = 0; i < querynode.Attributes.Count; i++)
                {
                    
                    wds1.RequestID   =  querynode.Attributes["RequestID"].Value;
                    DateTime date1 = new DateTime(0,DateTimeKind.Utc);
                    DateTime.TryParse(querynode.Attributes["RequestDate"].Value.ToString(),out date1);
                    wds1.RequestDate =  date1;
                    int j = 0;
                    int.TryParse(querynode.Attributes["RequestReminderNum"].Value.ToString(), out j);
                    wds1.RequestReminderNum = j;
                    j = 0;
                    int.TryParse(querynode.Attributes["RequestDataSource"].Value.ToString(), out j);
                    wds1.RequestDataSource = j;
                    wds1.TNO = querynode.Attributes["TNO"].Value;
                    wds1.ICC = querynode.Attributes["ICC"].Value;
                    date1 = new DateTime(0, DateTimeKind.Utc);
                    DateTime.TryParse(querynode.Attributes["DCL"].Value.ToString(), out date1);
                    wds1.DCL = date1;
                    wds1.CNL = querynode.Attributes["CNL"].Value;
                    wds1.COF = querynode.Attributes["COF"].Value;
                    date1 = new DateTime(0, DateTimeKind.Utc);
                    DateTime.TryParse(querynode.Attributes["DDI"].Value.ToString(), out date1);
                    wds1.DDI = date1;
                    wds1.RND = querynode.Attributes["RND"].Value;
                    wds1.PFD = querynode.Attributes["PFD"].Value;
                    wds1.CWR = querynode.Attributes["CWR"].Value;
                    j = 0;
                    int.TryParse(querynode.Attributes["VPN"].Value.ToString(), out j);
                    wds1.VPN = j;
                    wds1.COM = querynode.Attributes["COM"].Value;
                    wds1.RBC = querynode.Attributes["RBC"].Value;
                    j = 0;
                    int.TryParse(querynode.Attributes["PIC"].Value.ToString(), out j);
                    wds1.PIC = j;
                    wds1.RequestRemark = querynode.Attributes["RequestRemark"].Value;
                }*/
                 int pos1=-1,pos2=-1,len=-1;
                 string aFileContents;
                 pos1 = bFileContents.IndexOf("<RequestRecord",StringComparison.InvariantCultureIgnoreCase)+ 14  ;
                 pos2 = bFileContents.IndexOf("</Body>",StringComparison.InvariantCultureIgnoreCase) ;
                 len=pos2-pos1;
                  aFileContents = bFileContents.Substring(pos1,len);
                 string[] aFileContentsList = aFileContents.Split(new string[] { " " ,"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                 string aFileString,aFileContent;
                 aFileContent = "";
                 aFileString = "";
                 for (int i = 0; i < aFileContentsList.Length; i++)
                 {
                     if (aFileContentsList[i].Trim().Contains("RequestID="))
                     {
                         aFileString = "RequestID";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RequestDate="))
                     {
                         aFileString = "RequestDate";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RequestReminderNum="))
                     {
                         aFileString = "RequestReminderNum";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RequestDataSource="))
                     {
                         aFileString = "RequestDataSource";
                     }
                     else if (aFileContentsList[i].Trim().Contains("TNO="))
                     {
                         aFileString = "TNO";
                     }
                     else if (aFileContentsList[i].Trim().Contains("ICC="))
                     {
                         aFileString = "ICC";
                     }
                     else if (aFileContentsList[i].Trim().Contains("DCL="))
                     {
                         aFileString = "DCL";
                     }
                     else if (aFileContentsList[i].Trim().Contains("CNL="))
                     {
                         aFileString = "CNL";
                     }
                     else if (aFileContentsList[i].Trim().Contains("COF="))
                     {
                         aFileString = "COF";
                     }
                     else if (aFileContentsList[i].Trim().Contains("DDI="))
                     {
                         aFileString = "DDI";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RND="))
                     {
                         aFileString = "RND";
                     }
                     else if (aFileContentsList[i].Trim().Contains("PFD="))
                     {
                         aFileString = "PFD";
                     }
                     else if (aFileContentsList[i].Trim().Contains("CWR="))
                     {
                         aFileString = "CWR";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RBC="))
                     {
                         aFileString = "RBC";
                     }
                     else if (aFileContentsList[i].Trim().Contains("PIC="))
                     {
                         aFileString = "PIC";
                     }
                     else if (aFileContentsList[i].Trim().Contains("COM="))
                     {
                         aFileString = "COM";
                     }
                     else if (aFileContentsList[i].Trim().Contains("VPN="))
                     {
                         aFileString = "VPN";
                     }
                     else if (aFileContentsList[i].Trim().Contains("RequestRemark="))
                     {
                         aFileString = "RequestRemark";
                     }
                   
                     aFileContent = aFileContentsList[i].Trim();
                     aFileContent = aFileContent.Replace('"', ' ').Trim();
                     switch (aFileString)
                     {
                         case "RequestID":
                            // aFileContent = aFileContent.Replace("<", "");
                             //aFileContent = aFileContent.Replace("RequestRecord", "");
                            
                             aFileContent = aFileContent.Replace("RequestID=", "").Trim();

                             if (wds1.RequestID == null || wds1.RequestID == "")
                             {
                                 wds1.RequestID = aFileContent;
                             }
                             else
                             {
                                 wds1.RequestID = wds1.RequestID + " " + aFileContent;
                             }
                             break;
                         case "RequestDate":
                             aFileContent = aFileContent.Replace("RequestDate=", "").Trim();
                             DateTime date1 = new DateTime(0, DateTimeKind.Utc);
                             DateTime.TryParse(aFileContent, out date1);
                             wds1.RequestDate = date1;
                             break;
                         case "RequestReminderNum":
                             aFileContent = aFileContent.Replace("RequestReminderNum=", "").Trim();
                             int int1;
                             int.TryParse(aFileContent, out int1);
                             wds1.RequestReminderNum = int1;
                             break;
                         case "RequestDataSource":
                             aFileContent = aFileContent.Replace("RequestDataSource=", "").Trim();
                             int int2;
                             int.TryParse(aFileContent, out int2);
                             wds1.RequestDataSource = int2;
                             break;
                         case "TNO":
                             aFileContent = aFileContent.Replace("TNO=", "").Trim();
                     //        string s1 = enc.EncryptString(aFileContent);
                   //          int i1 = enc.EncryptInteger(12345678);
                             
                             if (wds1.TNO == null || wds1.TNO == "")
                             {
                                 wds1.TNO = aFileContent;
                             }
                             else
                             {
                                 wds1.TNO = wds1.TNO + " " + aFileContent;
                             }
                            
                             break;
                         case "ICC":
                             aFileContent = aFileContent.Replace("ICC=", "").Trim();
                             if (wds1.ICC == null || wds1.ICC == "")
                             {
                                 wds1.ICC = aFileContent;
                             }
                             else
                             {
                                 wds1.ICC = wds1.ICC + " " + aFileContent;
                             }
                             break;
                         case "DCL":
                             aFileContent = aFileContent.Replace("DCL=", "").Trim();
                             DateTime date2 = new DateTime(0,DateTimeKind.Utc);
                             DateTime.TryParse(aFileContent, out date2);
                             wds1.DCL = date2;
                             break;
                         case "CNL":
                             aFileContent = aFileContent.Replace("CNL=", "").Trim();
                             if (wds1.CNL == null || wds1.CNL == "")
                             {
                                 wds1.CNL = aFileContent;
                             }
                             else
                             {
                                 wds1.CNL = wds1.CNL + " " + aFileContent;
                             }
                            
                             break;
                         case "COF":
                             aFileContent = aFileContent.Replace("COF=", "").Trim();
                             if (wds1.COF == null || wds1.COF == "")
                             {
                                 wds1.COF = aFileContent;
                             }
                             else
                             {
                                 wds1.COF = wds1.COF + " " + aFileContent;
                             }
                            
                             break;
                         case "DDI":
                             aFileContent = aFileContent.Replace("DDI=", "").Trim();
                             DateTime date3 = new DateTime(0,DateTimeKind.Utc);
                             DateTime.TryParse(aFileContent, out date3);
                             wds1.DDI = date3;
                             break;
                         case "RND":
                             aFileContent = aFileContent.Replace("RND=", "").Trim();
                             if (wds1.RND == null || wds1.RND == "")
                             {
                                 wds1.RND = aFileContent;
                             }
                             else
                             {
                                 wds1.RND = wds1.RND + " " + aFileContent;
                             }
                            
                             break;
                         case "PFD":
                             aFileContent = aFileContent.Replace("PFD=", "").Trim();
                             if (wds1.PFD == null || wds1.PFD== "")
                             {
                                 wds1.PFD = aFileContent;
                             }
                             else
                             {
                                 wds1.PFD = wds1.PFD + " " + aFileContent;
                             }
                            
                             break;
                         case "CWR":
                             aFileContent = aFileContent.Replace("CWR=", "").Trim();
                             if (wds1.CWR == null || wds1.CWR == "")
                             {
                                 wds1.CWR = aFileContent;
                             }
                             else
                             {
                                 wds1.CWR = wds1.CWR + " " + aFileContent;
                             }
                            
                            
                             break;
                         case "RBC":
                             aFileContent = aFileContent.Replace("RBC=", "").Trim();
                             if (wds1.RBC == null || wds1.RBC == "")
                             {
                                 wds1.RBC = aFileContent;
                             }
                             else
                             {
                                 wds1.RBC = wds1.RBC + " " + aFileContent;
                             }
                            
                             break;
                         case "PIC":
                             aFileContent = aFileContent.Replace("PIC=", "").Trim();
                             int int4;
                             int.TryParse(aFileContent, out int4);
                             wds1.PIC = int4;
                             break;
                         case "COM":
                             aFileContent = aFileContent.Replace("COM=", "").Trim();
                             if (wds1.COM == null || wds1.COM == "")
                             {
                                 wds1.COM = aFileContent;
                             }
                             else
                             {
                                 wds1.COM = wds1.COM + " " + aFileContent;
                             }
                            
                             break;
                         case "VPN":
                             aFileContent = aFileContent.Replace("VPN=", "").Trim();
                             int int5;
                             int.TryParse(aFileContent, out int5);
                             wds1.VPN = int5;
                             break;
                         case "RequestRemark":
                             aFileContent = aFileContent.Replace("/>", "").Trim();
                             aFileContent = aFileContent.Replace("RequestRemark=", "").Trim();
                             if (wds1.RequestRemark == null || wds1.RequestRemark == "")
                             {
                                 wds1.RequestRemark = aFileContent;
                             }
                             else
                             {
                                 wds1.RequestRemark = wds1.RequestRemark + " " + aFileContent;
                             }
                             break;
                     }
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
