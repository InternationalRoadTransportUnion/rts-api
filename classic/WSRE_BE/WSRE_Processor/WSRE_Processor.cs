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
using System.Text.RegularExpressions;
using CIFCreation;
using IRU.RTS.Common.WCF;

namespace IRU.RTS.WSRE
{
	public class WSRE_Processor:IPlugIn,IRunnable,IActiveObject
	{

		#region Array strings for DB Insert In Update
		//-- Step 1 - 5 - Initial Log
		private static string[] m_aStep1 = 
			new string[]{"SUBSCRIBER_ID", "SAFETIR_MESSAGE_IN_ID", "SESSION_KEY_USED_ENCRYPTED_IN", 
							"SENDER_TCP_IP_ADDRESS", "SafeTIRReconData", "ROW_CREATED_TIME", "SENDER_MESSAGE_ID", "DECRYPTION_KEY_ID"};

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

		//-- Step 9 - 99 - Procedure Succeded

		private static string[] m_aStep9 = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};

			
		#endregion


		#region Declare Variables

		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 

		private string m_DispatchFileDropPath; 
		private string m_TemporaryFolderPath;

		private string m_CryptoProviderEndpoint;

		private IPlugInManager m_PluginManager;

		private string m_PluginName;

		private string m_SchemaFilesPath;


		#endregion

		public WSRE_Processor()
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
					"./WSRE_ProcessorSettings");

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

				string QuerySchemaPath = m_SchemaFilesPath + "WSRE.xsd";
				XMLValidationHelper.PopulateSchemas("http://www.iru.org/SafeTIRUpload",QuerySchemaPath);
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSRE_Processor"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSRE_Processor"
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
			// TODO:  Add WSRE_Processor.Unload implementation
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
			// TODO:  Add WSRE_Processor.Start implementation
		}

		public void Stop()
		{
			// TODO:  Add WSRE_Processor.Stop implementation
		}

		#endregion

		#region IActiveObject Members

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue, string queueName)
		{
			// TODO:  Add WSRE_Processor.IRU.CommonInterfaces.IActiveObject.Enqueue implementation
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
			WSRE_INTERNAL_LOG_STRUCT WSRE_Internal_Log_Struct_Data = new WSRE_INTERNAL_LOG_STRUCT();
			WSRE_SEQUENCE_STRUCT WSRE_Sequence_Struct_Data = new WSRE_SEQUENCE_STRUCT();

			string subsdb_Password="", subsdb_SessionEncAlgo="", subsdb_HashAlgo="", subsdb_CopyToID="", subsdb_CopyToAddress="" ; //To be fetched from Subscriber DB
		
			#endregion

			using (NetTcpClient<ICryptoOperations> client = new NetTcpClient<ICryptoOperations>(m_CryptoProviderEndpoint))
			{
				ICryptoOperations iCryptoOperations = client.GetProxy();

				#region Extract File Contents from object
				Hashtable htFileContents = (Hashtable)objFileContents;
				byte[] baFileContents = (byte[])htFileContents["FileContents"];
				#endregion
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "ExtractedFiles");
				#region Create DB helper Instances
				IDBHelper dbHelperWSREINTERNAL = m_dbHelperFactoryPlugin.GetDBHelper("WSRE_Internal_DB");//  null; //dbhelper for tchqdb
				IDBHelper dbHelperSubscriber = m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB");//  null; //dbhelper for tchqdb
				#endregion

				#region Split String
				string sFileContents = System.Text.Encoding.Unicode.GetString(baFileContents);
				string[] aFileContentsList = sFileContents.Split('\t');
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SplitString");
				RSACryptoKey sessionDecrKey = null;
				#endregion

				#region CleanForPrevious Failure
				InternalDBHelper intDBhelper = null;
				try
				{
					WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID = Int32.Parse(aFileContentsList[0].Trim().ToString());
					WSRE_Sequence_Struct_Data.SAFETIR_MESSAGE_IN_ID = WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID;

					intDBhelper = new InternalDBHelper(dbHelperWSREINTERNAL, WSRE_Internal_Log_Struct_Data, WSRE_Sequence_Struct_Data);
					int iRowsDeleted;

					intDBhelper.CleanInternalDBForFailure(out iRowsDeleted/*,  WSRE_Internal_Log_Struct_Data*/);

				}
				catch (Exception ex)
				{
					string msg = ex.Message + " _ " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, msg);
					throw ex; //Back to FileListener
				}
				#endregion

				#region Convert the Uploadeddata to byte array
				WSRE_Internal_Log_Struct_Data.SafeTIRReconData = Convert.FromBase64String(aFileContentsList[5]);
				#endregion


				#region -- Step 1 - 5 - Initial Log

				try
				{
					WSRE_Internal_Log_Struct_Data.ROW_CREATED_TIME = DateTime.Now;
					WSRE_Internal_Log_Struct_Data.SUBSCRIBER_ID = aFileContentsList[1];
					WSRE_Internal_Log_Struct_Data.DECRYPTION_KEY_ID = aFileContentsList[2];

					WSRE_Internal_Log_Struct_Data.SESSION_KEY_USED_ENCRYPTED_IN = Convert.FromBase64String(aFileContentsList[3]);
					WSRE_Internal_Log_Struct_Data.SENDER_TCP_IP_ADDRESS = aFileContentsList[4];
					WSRE_Internal_Log_Struct_Data.SENDER_MESSAGE_ID = aFileContentsList[6];
					WSRE_Internal_Log_Struct_Data.copy_to_id = aFileContentsList[7];


					WSRE_Sequence_Struct_Data.SetMembers(5, 1, null, DateTime.Now);
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

					WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(5);
					WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

					WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
						1200, ex.Message + " - " + ex.StackTrace,
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);

				}

				try
				{
					dbHelperWSREINTERNAL.ConnectToDB();
					dbHelperWSREINTERNAL.BeginTransaction();
					intDBhelper.LogSafeTIRfileContentsinInDB(m_aStep1);
					dbHelperWSREINTERNAL.CommitTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "InsertData");
				}
				catch (Exception sqlEx)
				{
					dbHelperWSREINTERNAL.RollbackTransaction();
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
					throw sqlEx; //Back to FileListener
				}
				finally
				{
					dbHelperWSREINTERNAL.Close();
				}
				#endregion

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 2 - 10 - Session Key Decryption
					int keyStatus = 0;

					try
					{
						dbHelperSubscriber.ConnectToDB();

						#region Get IRU Keydetails
						keyStatus = KeyManager.GetIRUKeyDetails(WSRE_Internal_Log_Struct_Data.DECRYPTION_KEY_ID,
							WSRE_Internal_Log_Struct_Data.SUBSCRIBER_ID, out sessionDecrKey, dbHelperSubscriber);

						if (keyStatus == 3 || keyStatus == 7 || keyStatus == 9)
						{
							WSRE_Sequence_Struct_Data.SetMembers(10,
								keyStatus, null,
								DateTime.Now);

							throw new ApplicationException("Invalid Key Status :" + keyStatus.ToString());
						}
						Subscriber_DBHelper subsHelper = null;
						subsHelper = new Subscriber_DBHelper(dbHelperSubscriber);

						int subscriberReturnCode = subsHelper.AuthenticateQuerySender(WSRE_Internal_Log_Struct_Data.SUBSCRIBER_ID,
							out subsdb_Password, "WSRE", 1, out subsdb_SessionEncAlgo, out subsdb_HashAlgo, out subsdb_CopyToID, out subsdb_CopyToAddress);

						if (subscriberReturnCode != 0)
						{

							WSRE_Sequence_Struct_Data.SetMembers(10,
								4/*subscriberReturnCode*/ , null,
								DateTime.Now);

							throw new ApplicationException("AuthenticateQuerySender from DB failed :" + subscriberReturnCode.ToString());
						}
						#endregion

						#region Session Key Decryption
						byte[] decrSessionKeyIn = null;

						Hashtable hashForSessionKey = new Hashtable();
						hashForSessionKey["MODULUS"] = sessionDecrKey.Modulus;
						hashForSessionKey["EXPONENT"] = sessionDecrKey.Exponent;
						hashForSessionKey["D"] = sessionDecrKey.D;
						hashForSessionKey["P"] = sessionDecrKey.P;
						hashForSessionKey["Q"] = sessionDecrKey.Q;
						hashForSessionKey["DP"] = sessionDecrKey.DP;
						hashForSessionKey["DQ"] = sessionDecrKey.DQ;
						hashForSessionKey["INVERSEQ"] = sessionDecrKey.INVERSEQ;

						try
						{
							decrSessionKeyIn = iCryptoOperations.Decrypt(WSRE_Internal_Log_Struct_Data.SESSION_KEY_USED_ENCRYPTED_IN,
								subsdb_SessionEncAlgo, hashForSessionKey);

							WSRE_Internal_Log_Struct_Data.SESSION_KEY_USED_DECRYPTED_IN = decrSessionKeyIn;

							WSRE_Sequence_Struct_Data.SetMembers(10, keyStatus, null, DateTime.Now);
						}
						catch (Exception ex)
						{
							WSRE_Sequence_Struct_Data.SetMembers(10, 8, ex.Message + " - " + ex.StackTrace, DateTime.Now);

							Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
							throw new ApplicationException(ex.Message + " - " + ex.StackTrace);
						}

						#endregion

					}
					catch (ApplicationException ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(WSRE_Sequence_Struct_Data.LAST_UPDATE_TIME);

						WSRE_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);


					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(10);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

						WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
							8, ex.Message + " - " + ex.StackTrace,
							WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);
					}
					finally
					{
						dbHelperSubscriber.Close();
					}
					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep2);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();

						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}
					#endregion
				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 3 - 15 - Do Decryption & get Decrypted Data

					Hashtable hashDecryptParams = new Hashtable();

					byte[] byIV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };

					hashDecryptParams["KEY"] = WSRE_Internal_Log_Struct_Data.SESSION_KEY_USED_DECRYPTED_IN;
					hashDecryptParams["IV"] = byIV;

					try
					{
						byte[] baDecryptedQueryParamXML =
							iCryptoOperations.Decrypt(WSRE_Internal_Log_Struct_Data.SafeTIRReconData,
							"3DES", hashDecryptParams);

						WSRE_Internal_Log_Struct_Data.SAFETIR_XML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);

						WSRE_Sequence_Struct_Data.SetMembers(15, 1, null, DateTime.Now);
					}
					catch (Exception ex)
					{
						WSRE_Sequence_Struct_Data.SetMembers(15, 8, ex.Message + " - " + ex.StackTrace, DateTime.Now);
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(WSRE_Sequence_Struct_Data.LAST_UPDATE_TIME);
					}
					WSRE_Internal_Log_Struct_Data.DECRYPTION_RESULT = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);

					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep3);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}
					#endregion

				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 4 - 20 - Do Validate Hash


					try
					{
						string sHash = RegExHelper.ExtractHASH(WSRE_Internal_Log_Struct_Data.SAFETIR_XML);

						if (sHash.Trim() == "")
						{
							WSRE_Sequence_Struct_Data.SetMembers(20, 7, "Hash Missing / Unable to extract Hash", DateTime.Now);
							throw new ApplicationException("No Hash found");
						}

						byte[] baHash = Convert.FromBase64String(sHash);

						string sBody = RegExHelper.ExtractBODYContents(WSRE_Internal_Log_Struct_Data.SAFETIR_XML);//.Substring(iBodyStart,iBodyLength );  
						Regex r = new Regex(@"\s+");
						sBody = sBody.Trim();
						sBody = r.Replace(sBody, " ");

						if (sBody.Trim() == "")
						{
							WSRE_Sequence_Struct_Data.SetMembers(20, 7, "Body Missing / Unable to extract Body", DateTime.Now);
							throw new ApplicationException("No Body Node found");
						}



						byte[] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);


						if (!iCryptoOperations.VerifyHash(baBody, subsdb_HashAlgo, null, baHash))
						{
							WSRE_Sequence_Struct_Data.SetMembers(20, 7, "Verify Hash Failed", DateTime.Now);
							throw new ApplicationException("Hash Verification Failed");
						}


						WSRE_Sequence_Struct_Data.SetMembers(20, 1, null, DateTime.Now);

					}
					catch (ApplicationException ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(WSRE_Sequence_Struct_Data.LAST_UPDATE_TIME);

					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(20);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

						WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
							8, ex.Message + " - " + ex.StackTrace,
							WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);
					}

					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep4);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}

					#endregion
				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 5 - 25 - Validate uploaded message against XSD.

					try
					{
						XMLValidationHelper xvh = new XMLValidationHelper();
						if (!xvh.ValidateXML(WSRE_Internal_Log_Struct_Data.SAFETIR_XML, out WSRE_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON))
						{
							WSRE_Sequence_Struct_Data.SetMembers(25, 2, WSRE_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON, DateTime.Now);
							WSRE_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);

							throw new ApplicationException(WSRE_Internal_Log_Struct_Data.SAFETIR_XML_INVALID_REASON);
						}
						WSRE_Sequence_Struct_Data.SetMembers(25, 1, null, DateTime.Now);
						WSRE_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);
					}
					catch (ApplicationException ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(WSRE_Sequence_Struct_Data.LAST_UPDATE_TIME);

					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(25);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

						WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
							2, ex.Message + " - " + ex.StackTrace,
							WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);

						WSRE_Internal_Log_Struct_Data.SAFETIR_XML_VALID = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);
					}


					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep5);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}

					#endregion
				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 6 - 30 - Authorize User


					try
					{
						//	XmlDocument xd = new XmlDocument();
						if (WSRE_Internal_Log_Struct_Data.SUBSCRIBER_ID.Trim() == null)
						{
							WSRE_Sequence_Struct_Data.SetMembers(30, 2, "Sender Verification Failed: Sender Node Missing", DateTime.Now);
							throw new ApplicationException(WSRE_Sequence_Struct_Data.WSRE_STEP_ERROR_DESCRIPTION);
						}
						else
						{
							//if(node.InnerText.Trim().ToUpper() != WSRE_Internal_Log_Struct_Data.SUBSCRIBER_ID.Trim().ToUpper())
							//{
							//    WSRE_Sequence_Struct_Data.SetMembers(30, 2, "Sender Verification Failed: Subscriber-Sender Node Mismatch ", DateTime.Now);    
							//    throw new ApplicationException(WSRE_Sequence_Struct_Data.WSRE_STEP_ERROR_DESCRIPTION);
							//}
						}

						//node = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:Password",xns);
						/*if(node == null)
						{
							if(subsdb_Password == null || subsdb_Password.Trim() == "")
							{
								//Password is valid - there is no password or password node also might not be present
							}
							else
							{	
								WSRE_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
								throw new ApplicationException(WSRE_Sequence_Struct_Data.WSRE_STEP_ERROR_DESCRIPTION);
							}
						}
						string password = node.InnerText ;
							if(subsdb_Password == null || subsdb_Password.Trim() == "")
							{
								if(password.Trim() == "")
								{

									//PAssword is valid - there is no password or password node also might no be present
								}
								else
								{
									WSRE_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
									throw new ApplicationException(WSRE_Sequence_Struct_Data.WSRE_STEP_ERROR_DESCRIPTION);
								}
							}
							else if(password.Trim() == subsdb_Password)
							{
								//PAssword is valid - there is no password or password node also might no be present
							}
							else
							{
								WSRE_Sequence_Struct_Data.SetMembers(30, 3, "Password Verification Failed", DateTime.Now);    
								throw new ApplicationException(WSRE_Sequence_Struct_Data.WSRE_STEP_ERROR_DESCRIPTION);
							}
						}*/


						WSRE_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(1);
						WSRE_Sequence_Struct_Data.SetMembers(30, WSRE_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED.Value, null, DateTime.Now);
					}
					catch (ApplicationException ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
						WSRE_Internal_Log_Struct_Data.SUBSCRIBER_AUTHENTICATED = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP_RESULT);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(WSRE_Sequence_Struct_Data.WSRE_STEP);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(WSRE_Sequence_Struct_Data.LAST_UPDATE_TIME);

					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(30);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

						WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
							1200, ex.Message + " - " + ex.StackTrace,
							WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);
					}

					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep6);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}

					#endregion
				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 7 - 35 - CreateCIFFile

					try
					{

						WSRE_Internal_Log_Struct_Data.CIF_FILENAME = CreateCIFDispatchFile(WSRE_Internal_Log_Struct_Data);
						WSRE_Sequence_Struct_Data.SetMembers(35, 1, null, DateTime.Now);
					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);

						WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(35);
						WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);

						WSRE_Sequence_Struct_Data.SetMembers(WSRE_Internal_Log_Struct_Data.LAST_STEP.Value,
							2, ex.Message + " - " + ex.StackTrace,
							WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);
					}

					WSRE_Sequence_Struct_Data.SetMembers(35, 1, null, DateTime.Now);
					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep7);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}


					#endregion
				}

				if (WSRE_Internal_Log_Struct_Data.LAST_STEP == null)
				{
					#region -- Step 8 - 99 - Procedure Succeded

					WSRE_Internal_Log_Struct_Data.COMPLETION_TIME = new NullableDateTime(DateTime.Now);
					WSRE_Internal_Log_Struct_Data.LAST_STEP = new NullableInt(99);

					WSRE_Sequence_Struct_Data.SetMembers(99, 2, null, WSRE_Internal_Log_Struct_Data.COMPLETION_TIME.Value);

					try
					{
						dbHelperWSREINTERNAL.ConnectToDB();
						dbHelperWSREINTERNAL.BeginTransaction();
						intDBhelper.UpdateInternalLogReturnCode(m_aStep9);
						dbHelperWSREINTERNAL.CommitTransaction();
					}
					catch (SqlException sqlEx)
					{
						dbHelperWSREINTERNAL.RollbackTransaction();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" + WSRE_Internal_Log_Struct_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace);
						throw sqlEx; //Back to FileListener
					}
					finally
					{
						dbHelperWSREINTERNAL.Close();
					}


					#endregion
				}
			}
		}	

		#region private methods
		private string CreateCIFDispatchFile(WSRE_INTERNAL_LOG_STRUCT UploadStruct)
		{


            string FileName = UploadStruct.SENDER_TCP_IP_ADDRESS.Trim() + "_" +  UploadStruct.SAFETIR_MESSAGE_IN_ID.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".CIF_03";
				
			StreamWriter sw=null;
            try
            {

                string xml1 = UploadStruct.SAFETIR_XML;

                int index1 = 0;
                index1 = xml1.IndexOf("<Body>", StringComparison.InvariantCultureIgnoreCase);
                xml1 = xml1.Insert(index1 + 6, "<SubscriberID>" + UploadStruct.SUBSCRIBER_ID + "</SubscriberID>");

                XMLValidationHelper.ChangeEncoding(ref xml1, "UNICODE");

                string file1 = m_TemporaryFolderPath + FileName;


                sw = new StreamWriter(file1, false, System.Text.Encoding.Unicode);
                sw.Write(xml1);


                //sw.Write(UploadStruct.SAFETIR_XML);


            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch(Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "99 CreateCIFDispatchFile :" +
                    UploadStruct.SAFETIR_XML.ToString() + " - " + ex.Message + " - " + ex.StackTrace + " - ");

                throw ex;
            }
             
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            try
            {
                CIFCreate cif1 = new CIFCreate();
                string FileName1 = cif1.CIFCreateFile(m_TemporaryFolderPath, FileName, m_DispatchFileDropPath,m_SchemaFilesPath, 3);
                if (File.Exists(m_TemporaryFolderPath + FileName))
                    File.Delete(m_TemporaryFolderPath + FileName);
                return FileName1;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }

			
		}
		#endregion

		#endregion

	}
}
