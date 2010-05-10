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
using IRU.RTS.Crypto ;
using IRU.RTS.CryptoInterfaces;

namespace IRU.RTS.WSST
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WSST_Processor:IPlugIn,IRunnable,IActiveObject
	{

/*
DBHelperFactoryPlugin="DBHelperFactory"
InMemoryCachePlugin="InMemoryCache" 
DispatchFileDropPath="g:\DispatchPath" 
TemporaryFolderPath="g:\temp\Dispatch" 
WSSTInternalDBName =”WSST_INTERNAL_DB” 
SubscriberDBName=”SubscriberDB” 
CopyToDBName=”WSST_COPY_TO_DB” 
CryptoProviderEndpoint="tcp://server:Port/CryptoProvider.rem
*/

		#region Array strings for DB Insert In Update
		//-- Step 1 - 5 - Initial Log
		private static string[] m_aStep1 = 
			new string[]{"SUBSCRIBER_ID", "SAFETIR_MESSAGE_IN_ID", "SESSION_KEY_USED_ENCRYPTED_IN", 
							"SENDER_TCP_IP_ADDRESS", "SafeTIRUploadData", "ROW_CREATED_TIME", "SENDER_MESSAGE_ID", "DECRYPTION_KEY_ID"};

		//-- Step 2 - 10 - Session Key Decryption

		private static string[] m_aStep2 = 
			new string[]{"SUBSCRIBER_AUTHENTICATED", "SESSION_KEY_USED_DECRYPTED_IN", 
							"LAST_STEP", "COMPLETION_TIME"};

		//-- Step 3 - 15 - Do Decryption & get Decrypted Data

		private static string[] m_aStep3 = 
			new string[]{"DECRYPTION_RESULT", "DECRYPTION_RESULT_DESCRIPTION", "SAFETIR_XML", 
							"LAST_STEP", "COMPLETION_TIME"};

		//-- Step 4 - 20 - Do Validate Hash

		private static string[] m_aStep4 = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};

		//-- Step 5 - 25 - Validate uploaded message against XSD.

		private static string[] m_aStep5 = 
			new string[]{"SAFETIR_XML_VALID", "SAFETIR_XML_INVALID_REASON", "LAST_STEP", "COMPLETION_TIME"};

		//-- Step 6 - 30 - Authorize User

		private static string[] m_aStep6 = 
			new string[]{"SUBSCRIBER_AUTHENTICATED", "LAST_STEP", "COMPLETION_TIME"};

		//-- Step 7 - 35 - CreateCIFFile

		private static string[] m_aStep7 = 
			new string[]{"CIF_FILENAME", "LAST_STEP", "COMPLETION_TIME"};

		//-- Step 8 - 40 - Procedure Succeded

		private static string[] m_aStep8 = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};
		//Intentionally left null for step 8 where no need to update Internal log table

		//-- Step 9 - 99 - Procedure Succeded

		private static string[] m_aStep9 = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};

		//-- CopyToDB update array

		private static string[] m_aCopyToDB = 
			new string[]{"SAFETIR_MESSAGE_IN_ID", "COPY_TO_ID", "SAFETIR_XML", "SENDER_TCP_IP_ADDRESS", 
							"JOB_REQUEST_TIME", "JOB_STATUS","SUBSCRIBER_ID"};

	
		#endregion


		#region Declare Variables

		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 

		private string m_DispatchFileDropPath; 
		private string m_TemporaryFolderPath;

		//internal static IRU.CommonInterfaces.ICache  m_InMemoryCachePlugin;//="InMemoryCache" 

		private string m_CryptoProviderEndpoint;//

		private IPlugInManager m_PluginManager;

		private string m_PluginName;

		private string m_SchemaFilesPath;


		#endregion

		public WSST_Processor()
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
					"./WSST_ProcessorSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;

				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				m_TemporaryFolderPath =XMLHelper.GetAttributeNode(parameterNode,
					"TemporaryFolderPath").InnerText;

				if (m_TemporaryFolderPath.LastIndexOf("\\")!= m_TemporaryFolderPath.Length-1)
					m_TemporaryFolderPath+="\\";

				m_DispatchFileDropPath = XMLHelper.GetAttributeNode(parameterNode,
					"DispatchFileDropPath").InnerText;

				if (m_DispatchFileDropPath.LastIndexOf("\\")!= m_DispatchFileDropPath.Length-1)
					m_DispatchFileDropPath+="\\";

				m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
					"CryptoProviderEndpoint").InnerText;

				//Read the schema files into XMLHelper
				m_SchemaFilesPath = XMLHelper.GetAttributeNode(parameterNode,
					"SchemaFilesPath").InnerText;

				if (m_SchemaFilesPath.LastIndexOf("\\")!= m_SchemaFilesPath.Length-1)
					m_SchemaFilesPath+="\\";

				string QuerySchemaPath = m_SchemaFilesPath + "WSST.xsd";
				XMLValidationHelper.PopulateSchemas("http://www.iru.org/SafeTIRUpload",QuerySchemaPath);
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSST_Processor"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSST_Processor"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of WSST_Processor"
					+ e.Message);
				throw e;
			}

		}

		public void Unload()
		{
			// TODO:  Add WSST_Processor.Unload implementation
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
			// TODO:  Add WSST_Processor.Start implementation
		}

		public void Stop()
		{
			// TODO:  Add WSST_Processor.Stop implementation
		}

		#endregion

		#region IActiveObject Members

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue, string queueName)
		{
			// TODO:  Add WSST_Processor.IRU.CommonInterfaces.IActiveObject.Enqueue implementation
		}

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue)
		{
			ProcessUploadJob(objToEnqueue); 
		}

		#endregion

		#region ProcessUploadJob

		private void ProcessUploadJob(object objFileContents)
		{
			#region Variables
			WSST_INTERNAL_LOG_STRUCT Wsst_Internal_Log_Struct_Data = new WSST_INTERNAL_LOG_STRUCT();
			WSST_SEQUENCE_STRUCT Wsst_Sequence_Struct_Data = new WSST_SEQUENCE_STRUCT();

			string subsdb_Password="", subsdb_SessionEncAlgo="", subsdb_HashAlgo="", subsdb_CopyToID="", subsdb_CopyToAddress="" ; //To be fetched from Subscriber DB

			ICryptoOperations iCryptoOperations = null;
			
			#endregion

			#region Extract File Contents from object
			Hashtable htFileContents = (Hashtable)objFileContents ;
			byte[] baFileContents = (byte[])htFileContents["FileContents"];
			#endregion

			#region Create DB helper Instances
			IDBHelper dbHelperWSSTINTERNAL = m_dbHelperFactoryPlugin.GetDBHelper("WSST_Internal_DB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperWSSTCOPYT = m_dbHelperFactoryPlugin.GetDBHelper("WSST_COPYTODB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperSubscriber = m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB") ;//  null; //dbhelper for tchqdb
			#endregion

			#region Split String
			string sFileContents = System.Text.Encoding.Unicode.GetString(baFileContents);
			string [] aFileContentsList = sFileContents.Split('\t');

			RSACryptoKey sessionDecrKey=null;
			#endregion

			#region CleanForPrevious Failure
			InternalDBHelper intDBhelper =null;
			try
			{
				//Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID = double.Parse(aFileContentsList[0].Trim().ToString());
				Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID = Int32.Parse(aFileContentsList[0].Trim().ToString());
				Wsst_Sequence_Struct_Data.SAFETIR_MESSAGE_IN_ID = Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID;

				intDBhelper = new InternalDBHelper(dbHelperWSSTINTERNAL, Wsst_Internal_Log_Struct_Data, Wsst_Sequence_Struct_Data); 
				int iRowsDeleted;
				
				intDBhelper.CleanInternalDBForFailure(out iRowsDeleted/*,  Wsst_Internal_Log_Struct_Data*/);
			}
			catch(Exception ex)
			{
				string msg = ex.Message + " _ " + ex.StackTrace;
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,msg);
				throw ex; //Back to FileListener
			}
			#endregion

			#region Convert the Uploadeddata to byte array
			Wsst_Internal_Log_Struct_Data.SafeTIRUploadData = Convert.FromBase64String(aFileContentsList[5]); 
			#endregion


			#region -- Step 1 - 5 - Initial Log
			//"SUBSCRIBER_ID", "SAFETIR_MESSAGE_IN_ID", "SESSION_KEY_USED_ENCRYPTED_IN", 
			//"SENDER_TCP_IP_ADDRESS", "SafeTIRUploadData", "ROW_CREATED_TIME, 
			//SENDER_MESSAGE_ID"

			try
			{
				Wsst_Internal_Log_Struct_Data.ROW_CREATED_TIME = DateTime.Now ;
				Wsst_Internal_Log_Struct_Data.SUBSCRIBER_ID = aFileContentsList[1];
				Wsst_Internal_Log_Struct_Data.DECRYPTION_KEY_ID = aFileContentsList[2];

				Wsst_Internal_Log_Struct_Data.SESSION_KEY_USED_ENCRYPTED_IN = Convert.FromBase64String(aFileContentsList[3]);
				Wsst_Internal_Log_Struct_Data.SENDER_TCP_IP_ADDRESS = aFileContentsList[4];
				Wsst_Internal_Log_Struct_Data.SENDER_MESSAGE_ID = aFileContentsList[6];
				Wsst_Internal_Log_Struct_Data.copy_to_id =  aFileContentsList[7];


				Wsst_Sequence_Struct_Data.SetMembers(5,1,null,DateTime.Now); 
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

				Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(5);
				Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

				Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
					1200, ex.Message + " - " + ex.StackTrace, 
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );

			}

			try
			{
				dbHelperWSSTINTERNAL.ConnectToDB();
				dbHelperWSSTINTERNAL.BeginTransaction();  
				intDBhelper.LogSafeTIRfileContentsinInDB(m_aStep1);
				dbHelperWSSTINTERNAL.CommitTransaction();  
			}
			catch(Exception sqlEx)
			{
				dbHelperWSSTINTERNAL.RollbackTransaction();
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
				throw sqlEx ; //Back to FileListener
			}
			finally
			{
				dbHelperWSSTINTERNAL.Close();
			}
			#endregion

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 2 - 10 - Session Key Decryption
				int keyStatus = 0;
			
				try
				{
					dbHelperSubscriber.ConnectToDB();

					#region Get IRU Keydetails
					keyStatus = KeyManager.GetIRUKeyDetails(Wsst_Internal_Log_Struct_Data.DECRYPTION_KEY_ID ,
						Wsst_Internal_Log_Struct_Data.SUBSCRIBER_ID , out sessionDecrKey,dbHelperSubscriber);

					//if(keyStatus ==3 || keyStatus == 4 || keyStatus == 7 || keyStatus ==9 ) //there is no 4 status
					if(keyStatus ==3 || keyStatus == 7 || keyStatus ==9 )
					{
						Wsst_Sequence_Struct_Data.SetMembers(10 , 
							keyStatus , null, 
							DateTime.Now);

						throw new ApplicationException("Invalid Key Status :"+keyStatus.ToString()); 
					}
					Subscriber_DBHelper subsHelper = null; 
					subsHelper = new Subscriber_DBHelper(dbHelperSubscriber);

					int subscriberReturnCode = subsHelper.AuthenticateQuerySender(Wsst_Internal_Log_Struct_Data.SUBSCRIBER_ID,
						out subsdb_Password,"WSST",1, out subsdb_SessionEncAlgo,out subsdb_HashAlgo,out subsdb_CopyToID, out subsdb_CopyToAddress );
		
					if (subscriberReturnCode != 0)
					{

						Wsst_Sequence_Struct_Data.SetMembers(10, 
							4/*subscriberReturnCode*/ , null, 
							DateTime.Now );

						throw new ApplicationException("AuthenticateQuerySender from DB failed :" + subscriberReturnCode.ToString());
					}
					#endregion

					#region copytoID validation
						
					if (Wsst_Internal_Log_Struct_Data.copy_to_id != null)
					{
						if (Wsst_Internal_Log_Struct_Data.copy_to_id.Trim() !="")
						{
							if (subsdb_CopyToID == null)
							{
								Wsst_Sequence_Struct_Data.SetMembers(10, 
									1220 , null, 
									DateTime.Now );
								throw new ApplicationException("Copy To ID Null");
							}
							else if (subsdb_CopyToID!= Wsst_Internal_Log_Struct_Data.copy_to_id)
							{
								Wsst_Sequence_Struct_Data.SetMembers(10, 
									1220 , null, 
									DateTime.Now );
								throw new ApplicationException("Copy To ID Mismatch");
							
							}
						}
					}
					#endregion

					#region Session Key Decryption
					byte [] decrSessionKeyIn = null;

					iCryptoOperations = (ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations), 
						m_CryptoProviderEndpoint);

					Hashtable hashForSessionKey = new Hashtable();
					hashForSessionKey["MODULUS"] = sessionDecrKey.Modulus ;
					hashForSessionKey["EXPONENT"] = sessionDecrKey.Exponent ;
					hashForSessionKey["D"] = sessionDecrKey.D ;
					hashForSessionKey["P"] = sessionDecrKey.P ;
					hashForSessionKey["Q"] = sessionDecrKey.Q ;
					hashForSessionKey["DP"] = sessionDecrKey.DP ;
					hashForSessionKey["DQ"] = sessionDecrKey.DQ ;
					hashForSessionKey["INVERSEQ"] = sessionDecrKey.INVERSEQ ;
			
					try
					{
						decrSessionKeyIn = iCryptoOperations.Decrypt(Wsst_Internal_Log_Struct_Data.SESSION_KEY_USED_ENCRYPTED_IN, 
							subsdb_SessionEncAlgo , hashForSessionKey); 

						Wsst_Internal_Log_Struct_Data.SESSION_KEY_USED_DECRYPTED_IN =  decrSessionKeyIn;

						Wsst_Sequence_Struct_Data.SetMembers(10,keyStatus,null,DateTime.Now); 
					}
					catch(Exception ex)
					{
						Wsst_Sequence_Struct_Data.SetMembers(10, 8,ex.Message + " - " + ex.StackTrace, DateTime.Now) ;

						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
						throw new ApplicationException(ex.Message + " - " + ex.StackTrace);
					}

					#endregion

				}
				catch(ApplicationException ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(Wsst_Sequence_Struct_Data.LAST_UPDATE_TIME);

					Wsst_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT);


				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(10);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

					Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
						8, ex.Message + " - " + ex.StackTrace, 
						Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );
				}
				finally
				{
					dbHelperSubscriber.Close(); 
				}

				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep2);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 3 - 15 - Do Decryption & get Decrypted Data

				Hashtable hashDecryptParams = new Hashtable();
			
				byte[] byIV = new byte[]{0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};

				hashDecryptParams["KEY"] = Wsst_Internal_Log_Struct_Data.SESSION_KEY_USED_DECRYPTED_IN ;
				hashDecryptParams["IV"] = byIV;

				try
				{
					byte [] baDecryptedQueryParamXML =
						iCryptoOperations.Decrypt(Wsst_Internal_Log_Struct_Data.SafeTIRUploadData, 
						"3DES" , hashDecryptParams);  

					Wsst_Internal_Log_Struct_Data.SAFETIR_XML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);
   
					Wsst_Sequence_Struct_Data.SetMembers(15,1,null,DateTime.Now); 
				}
				catch(Exception ex)
				{
					Wsst_Sequence_Struct_Data.SetMembers(15, 8,ex.Message + " - " + ex.StackTrace, DateTime.Now) ;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Struct_Data.LAST_UPDATE_TIME);  
				}
				Wsst_Internal_Log_Struct_Data.DECRYPTION_RESULT = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT); 

				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep3);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 4 - 20 - Do Validate Hash


				try
				{
					string sHash = RegExHelper.ExtractHASH(Wsst_Internal_Log_Struct_Data.SAFETIR_XML);//.Substring(iHashStart,iHashLength);  

					if (sHash.Trim()=="")
					{
						Wsst_Sequence_Struct_Data.SetMembers(20,7,"Hash Missing / Unable to extract Hash",DateTime.Now); 
						throw new ApplicationException("No Hash found");
					}

					byte [] baHash = Convert.FromBase64String(sHash);

					string sBody = RegExHelper.ExtractBODYContents (Wsst_Internal_Log_Struct_Data.SAFETIR_XML);//.Substring(iBodyStart,iBodyLength );  
				
					if (sBody.Trim()=="")
					{
						Wsst_Sequence_Struct_Data.SetMembers(20,7,"Body Missing / Unable to extract Body",DateTime.Now); 
						throw new ApplicationException("No Body Node found");
					}
				
				
				
					byte [] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);

			
					if(!iCryptoOperations.VerifyHash(baBody,subsdb_HashAlgo, null, baHash))
					{
						Wsst_Sequence_Struct_Data.SetMembers(20,7,"Verify Hash Failed",DateTime.Now); 
						throw new ApplicationException("Hash Verification Failed");
					}


					Wsst_Sequence_Struct_Data.SetMembers(20,1,null,DateTime.Now); 

				}
				catch(ApplicationException ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(Wsst_Sequence_Struct_Data.LAST_UPDATE_TIME);

				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(20);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

					Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
						8, ex.Message + " - " + ex.StackTrace, 
						Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );
				}

				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep4);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
			
				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 5 - 25 - Validate uploaded message against XSD.

				try
				{
					XMLValidationHelper xvh = new XMLValidationHelper();
					if(!xvh.ValidateXML(Wsst_Internal_Log_Struct_Data.SAFETIR_XML , out Wsst_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON))
					{
						Wsst_Sequence_Struct_Data.SetMembers(25,2,Wsst_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON,DateTime.Now); 
						Wsst_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT);

						throw new ApplicationException(Wsst_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON);
					}
					Wsst_Sequence_Struct_Data.SetMembers(25,1,null,DateTime.Now); 
					Wsst_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT);
				}
				catch(ApplicationException ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(Wsst_Sequence_Struct_Data.LAST_UPDATE_TIME);

				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(25);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

					Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
						2, ex.Message + " - " + ex.StackTrace, 
						Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );

					Wsst_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT);
				}


				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep5);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
			

				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 6 - 30 - Authorize User


				try
				{
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(Wsst_Internal_Log_Struct_Data.SAFETIR_XML);

					XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
					xns.AddNamespace("def","http://www.iru.org/SafeTIRUpload");

					XmlNode node = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:SubscriberID",xns);
					if(node == null)
					{
						Wsst_Sequence_Struct_Data.SetMembers(30, 2, "Sender Verification Failed: Sender Node Missing", DateTime.Now);    
						throw new ApplicationException(Wsst_Sequence_Struct_Data.WSST_STEP_ERROR_DESCRIPTION);
					}
					else
					{
						if(node.InnerText.Trim().ToUpper() != Wsst_Internal_Log_Struct_Data.SUBSCRIBER_ID.Trim().ToUpper())
						{
							Wsst_Sequence_Struct_Data.SetMembers(30, 2, "Sender Verification Failed: Subscriber-Sender Node Mismatch ", DateTime.Now);    
							throw new ApplicationException(Wsst_Sequence_Struct_Data.WSST_STEP_ERROR_DESCRIPTION);
						}
					}

					node = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:Password",xns);
					if(node == null)
					{
						if(subsdb_Password == null || subsdb_Password.Trim() == "")
						{
							//Password is valid - there is no password or password node also might not be present
						}
						else
						{	
							Wsst_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
							throw new ApplicationException(Wsst_Sequence_Struct_Data.WSST_STEP_ERROR_DESCRIPTION);
						}
					}
					else
					{
						string password = node.InnerText ;
						if(subsdb_Password == null || subsdb_Password.Trim() == "")
						{
							if(password.Trim() == "")
							{

								//PAssword is valid - there is no password or password node also might no be present
							}
							else
							{
								Wsst_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
								throw new ApplicationException(Wsst_Sequence_Struct_Data.WSST_STEP_ERROR_DESCRIPTION);
							}
						}
						else if(password.Trim() == subsdb_Password)
						{
							//PAssword is valid - there is no password or password node also might no be present
						}
						else
						{
							Wsst_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
							throw new ApplicationException(Wsst_Sequence_Struct_Data.WSST_STEP_ERROR_DESCRIPTION);
						}
					}


					Wsst_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(1);
					Wsst_Sequence_Struct_Data.SetMembers(30,Wsst_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED.Value,null,DateTime.Now); 
				}
				catch(ApplicationException ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
					Wsst_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP_RESULT);

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Struct_Data.WSST_STEP);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(Wsst_Sequence_Struct_Data.LAST_UPDATE_TIME);

				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(30);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

					Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
						1200, ex.Message + " - " + ex.StackTrace, 
						Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );
				}

				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep6);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
			

				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 7 - 35 - CreateCIFFile

				try
				{
					Wsst_Internal_Log_Struct_Data.CIF_FILENAME = CreateCIFDispatchFile(Wsst_Internal_Log_Struct_Data);  
					Wsst_Sequence_Struct_Data.SetMembers(35,1,null,DateTime.Now); 
				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(35);
					Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

					Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
						2, ex.Message + " - " + ex.StackTrace, 
						Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );
				}

				Wsst_Sequence_Struct_Data.SetMembers(35,1,null,DateTime.Now); 
				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep7);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
			

				#endregion
			}

			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 8 - 40 - CopyTo Procedure


				if(Wsst_Internal_Log_Struct_Data.copy_to_id != null)
				{
					if(Wsst_Internal_Log_Struct_Data.copy_to_id.Trim() != "")
					{
						try
						{
							WSST_COPY_TO_JOB_STRUCT Wsst_Copy_To_Job_Data = new WSST_COPY_TO_JOB_STRUCT();
							Wsst_Copy_To_Job_Data.COPY_TO_ID = Wsst_Internal_Log_Struct_Data.copy_to_id ;
							Wsst_Copy_To_Job_Data.SENDER_TCP_IP_ADDRESS = Wsst_Internal_Log_Struct_Data.SENDER_TCP_IP_ADDRESS ;
							Wsst_Copy_To_Job_Data.SAFETIR_XML = Wsst_Internal_Log_Struct_Data.SAFETIR_XML;
							Wsst_Copy_To_Job_Data.JOB_REQUEST_TIME = DateTime.Now ;
							Wsst_Copy_To_Job_Data.JOB_STATUS = 0;
							Wsst_Copy_To_Job_Data.SAFETIR_MESSAGE_IN_ID = Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID ;
							Wsst_Copy_To_Job_Data.SUBSCRIBER_ID= Wsst_Internal_Log_Struct_Data.SUBSCRIBER_ID;

							dbHelperWSSTCOPYT.ConnectToDB();
							dbHelperSubscriber.ConnectToDB(); 

							if(AssignCopyTo( intDBhelper, dbHelperWSSTCOPYT, dbHelperSubscriber, Wsst_Copy_To_Job_Data))
							{
								Wsst_Sequence_Struct_Data.SetMembers(40,1,null,DateTime.Now); 
							}
							else
							{
								Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,"SAFETIR_MESSAGE_IN_ID :" + 
									Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " +  "AssignCopyTo failed..");  

								Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(40);
								Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

								Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
									3, " - probably COPY_TO_ID not found in Subscriber DB",		 
									Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );

							}
						}
						catch(Exception ex)
						{
							Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + 
								Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + ex.Message + " - " + ex.StackTrace );  

							Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(40);
							Wsst_Internal_Log_Struct_Data.COMPLETION_TIME =new NullableDateTime(DateTime.Now);

							Wsst_Sequence_Struct_Data.SetMembers(Wsst_Internal_Log_Struct_Data.LAST_STEP.Value  , 
								3, ex.Message + " - " + ex.StackTrace, 
								Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value );
						}

						try
						{
							dbHelperWSSTINTERNAL.ConnectToDB();
							intDBhelper.UpdateInternalLogReturnCode(m_aStep8);
							dbHelperWSSTINTERNAL.CommitTransaction();  
						}
						catch(SqlException sqlEx)
						{
							dbHelperWSSTINTERNAL.RollbackTransaction();
							Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
							throw sqlEx ; //Back to FileListener
						}
						finally
						{
							dbHelperWSSTINTERNAL.Close();
						}
					}
				}
	
			#endregion
			}
			if(Wsst_Internal_Log_Struct_Data.LAST_STEP == null)
			{
				#region -- Step 8 - 99 - Procedure Succeded

				Wsst_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now) ;
				Wsst_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(99);

				Wsst_Sequence_Struct_Data.SetMembers(99,2,null,Wsst_Internal_Log_Struct_Data.COMPLETION_TIME.Value ); 

				try
				{
					dbHelperWSSTINTERNAL.ConnectToDB();
					dbHelperWSSTINTERNAL.BeginTransaction();  
					intDBhelper.UpdateInternalLogReturnCode(m_aStep9);
					dbHelperWSSTINTERNAL.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTINTERNAL.Close();
				}
			

				#endregion
			}

		}	

		#region private methods
		private string CreateCIFDispatchFile(WSST_INTERNAL_LOG_STRUCT UploadStruct)
		{
			
			
			string  FileName = UploadStruct.SENDER_TCP_IP_ADDRESS.Trim() + "_" +
				UploadStruct.SAFETIR_MESSAGE_IN_ID + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
				".CIF_04";
			StreamWriter sw = null;
			try
			{
				XMLValidationHelper.ChangeEncoding(ref UploadStruct.SAFETIR_XML, "UNICODE");
				sw = new StreamWriter(m_TemporaryFolderPath+ FileName, false , System.Text.Encoding.Unicode);
				sw.Write(UploadStruct.SAFETIR_XML);
			}
			finally
			{
				sw.Close();
			}
			File.Move(m_TemporaryFolderPath+ FileName, m_DispatchFileDropPath + FileName);

			return FileName ;
		}

		private bool AssignCopyTo(InternalDBHelper intDBhelper, IDBHelper CopyToDBHelper, IDBHelper SubscriberDBHelper, WSST_COPY_TO_JOB_STRUCT CopyToDBData)
		{
			if(intDBhelper.CheckIfCopyToIDExists(SubscriberDBHelper, CopyToDBData))
			{
				intDBhelper.AssignCopyToJob(m_aCopyToDB, CopyToDBHelper, CopyToDBData); 
				return true;
			}
			else
				return false;
		}

		#endregion

		#endregion

	}
}
