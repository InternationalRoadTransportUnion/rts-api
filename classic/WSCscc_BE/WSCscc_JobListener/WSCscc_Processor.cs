using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient ;
using System.Data;
using System.IO;
using System.Text ;

using IRU;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;

using IRU.RTS;
using IRU.RTS.CryptoInterfaces;
using IRU.RTS.Crypto; 

namespace IRU.RTS.CopyToProcessor
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CopyToJobProcessor
	{

		#region DB Insert/Update string arrays
		//Step 1 - 5 - Initial log 
		private static string[] m_aStep1 = 
			new string[]{"SAFETIR_MESSAGE_IN_ID", "COPY_TO_ID", "SUBSCRIBER_ID", 
							"COPY_RESULT", "COPY_RESULT_DESCRIPTION", 
							"ORIGINAL_SENDER_TCP_IP_ADDRESS", "CREATION_TIME", 
							"LAST_STEP", "COMPLETION_TIME"};

		//Step 1 - 5 - Validate COPY_TO_ID
		private static string[] m_aStep1a = 
			new string[]{"COPY_TO_ADDRESS", 
							"LAST_STEP", "COMPLETION_TIME"};

		//Step 2 - 10 - Encrypt Message
		private static string[] m_aStep2 = 
			new string[]{"SESSION_KEY_USED_DECRYPTED_OUT", "SafeTIRUploadData", 
							"LAST_STEP", "COMPLETION_TIME"};

		//Step 3 - 15 - Encrypt Session Key
		private static string[] m_aStep3 = 
			new string[]{"ENCRYPTION_RESULT", "ENCRYPTION_RESULT_DESCRIPTION", 
							"ENCRYPTION_KEY_ID_USED", "SESSION_KEY_USED_ENCRYPTED_OUT", 
							"LAST_STEP", "COMPLETION_TIME"};

		private static string[] m_aStep4 = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};
		#endregion

		public CopyToJobProcessor()
		{

		}

		public void ProcessCopyToJob(string SubscriberID, string Copy_to_id, int SafeTIR_message_in_id, 
			string Original_Sender_TCP_Ip_Address, string SafeTIR_xml)
		{
			#region Variables
			WSST_COPY_TO_LOG_STRUCT  Wsst_Copy_To_Log_Data = new WSST_COPY_TO_LOG_STRUCT  ();
			WSST_COPY_TO_SEQUENCE_STRUCT Wsst_Sequence_Data = new WSST_COPY_TO_SEQUENCE_STRUCT();

			string subsdb_Password="", subsdb_SessionEncAlgo="", subsdb_HashAlgo="", 
				subsdb_CopyToID="", subsdb_CopyToAddress="" ; //To be fetched from Subscriber DB

			ICryptoOperations iCryptoOperations = null;

			WSCscc_CopyToDbHelper CopytoDB_helper = null;


			Hashtable htUpdateMsg ;

			byte[] a3DesSessionKey	= null;
			byte[] aEncMessage  = null;
			int keyStatus = 0;

			#endregion

			#region Create DB helper Instances
			IDBHelper dbHelperWSSTCOPYT = CopyToJobListener.m_dbHelperFactoryPlugin.GetDBHelper("WSST_COPYTODB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperSubscriber = CopyToJobListener.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB") ;//  null; //dbhelper for tchqdb

			CopytoDB_helper = new WSCscc_CopyToDbHelper(dbHelperWSSTCOPYT, Wsst_Copy_To_Log_Data, Wsst_Sequence_Data); 
			#endregion

			#region Step 1 - 5 - Initial Log & Validate Copy To Id 
			try
			{
				Wsst_Copy_To_Log_Data.COPY_TO_ID						=	Copy_to_id;
				Wsst_Copy_To_Log_Data.SUBSCRIBER_ID						=	SubscriberID;
				Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID				=	SafeTIR_message_in_id;
				Wsst_Copy_To_Log_Data.ORIGINAL_SENDER_TCP_IP_ADDRESS	=	Original_Sender_TCP_Ip_Address;
				Wsst_Copy_To_Log_Data.CREATION_TIME						=	DateTime.Now ;

				Wsst_Sequence_Data.SAFETIR_MESSAGE_IN_ID = Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID;

				Subscriber_DBHelper subsHelper = null; 
				dbHelperSubscriber.ConnectToDB();

				subsHelper = new Subscriber_DBHelper(dbHelperSubscriber);

				int subscriberReturnCode = subsHelper.AuthenticateQuerySender(Wsst_Copy_To_Log_Data.COPY_TO_ID ,
					out subsdb_Password,"WSCSCC",1, out subsdb_SessionEncAlgo,out subsdb_HashAlgo,
					out subsdb_CopyToID,out subsdb_CopyToAddress );

				if (subscriberReturnCode != 0)
				{
					Wsst_Sequence_Data.SetMembers(5, 
						2, "Copy_To_ID not Found: "+subscriberReturnCode.ToString() , 
						DateTime.Now );

					Wsst_Copy_To_Log_Data.COPY_RESULT = new NullableInt(2);

					throw new ApplicationException("Copy_To_ID not Found:" + subscriberReturnCode.ToString());
				}
				Wsst_Sequence_Data.SetMembers(5, 
					1 , null, 
					DateTime.Now );

				Wsst_Copy_To_Log_Data.COPY_TO_ADDRESS = subsdb_CopyToAddress; //"http://localhost/RTS/WSSTstc_WS/asmapCopy.asmx";

			}
			catch(ApplicationException ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

				Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
				Wsst_Copy_To_Log_Data.COMPLETION_TIME = new  NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   

			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

				Wsst_Sequence_Data.SetMembers(5, 
					2, " - "+ex.Message + " - " + ex.StackTrace , 
					DateTime.Now );

				Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
				Wsst_Copy_To_Log_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   
			}
			finally
			{
				dbHelperSubscriber.Close();
			}
	
			
			try
			{
				dbHelperWSSTCOPYT.ConnectToDB();
				dbHelperWSSTCOPYT.BeginTransaction();  
				CopytoDB_helper.LogSafeTIRfileContentsinInDB(m_aStep1);
				dbHelperWSSTCOPYT.CommitTransaction();  

				dbHelperWSSTCOPYT.BeginTransaction();  
				if(Wsst_Copy_To_Log_Data.LAST_STEP == null)
				{
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStep1a);
				}
				else
				{
					CopytoDB_helper.UpdateCopyToLogResultCode(null);
				}
				dbHelperWSSTCOPYT.CommitTransaction();  
			}
			catch(SqlException sqlEx)
			{
				dbHelperWSSTCOPYT.RollbackTransaction();

				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
				throw sqlEx ; //Back to FileListener
			}
			finally
			{
				dbHelperWSSTCOPYT.Close();
			}

			#endregion
	
			#region Step 2 - 10 - Encrypt Message
			if(Wsst_Copy_To_Log_Data.LAST_STEP == null)
			{
				try
				{
					iCryptoOperations = (ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations), CopyToJobListener.m_CryptoProviderEndpoint);

					htUpdateMsg = new Hashtable();
					htUpdateMsg["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
					byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(SafeTIR_xml);
					aEncMessage    = iCryptoOperations.Encrypt(aDocWithHash ,"3DES",ref htUpdateMsg);
					a3DesSessionKey = (byte[])htUpdateMsg["KEY"];

					Wsst_Copy_To_Log_Data.SESSION_KEY_USED_DECRYPTED_OUT = a3DesSessionKey;
					Wsst_Copy_To_Log_Data.SafeTIRUploadData = aEncMessage;

					Wsst_Sequence_Data.SetMembers(10, 
						1, null, 
						DateTime.Now );
				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Sequence_Data.SetMembers(10, 
						3, "Message Encryption Failed - "+ex.Message + " - " + ex.StackTrace , 
						DateTime.Now );

					Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
					Wsst_Copy_To_Log_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   

				}


				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStep2);
					dbHelperWSSTCOPYT.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}
			}
			#endregion

			#region Step 3 - 15 - Encrypt Sesion Key
			if(Wsst_Copy_To_Log_Data.LAST_STEP == null)
			{
				string encryptionKeyID="--";
				RSACryptoKey rKey = null;

				try
				{

					dbHelperSubscriber.ConnectToDB();
					int iKeyFetchResult =  KeyManager.AssignSubscriberKey(Copy_to_id,out rKey,out encryptionKeyID, dbHelperSubscriber);
					Wsst_Copy_To_Log_Data.ENCRYPTION_KEY_ID_USED =  encryptionKeyID;
				
					if(iKeyFetchResult ==1)
					{
						htUpdateMsg= new Hashtable();
						htUpdateMsg["EXPONENT"]= rKey.Exponent;
						htUpdateMsg["MODULUS"] = rKey.Modulus;
						System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));

						byte[] a3DesEncKey = null; ;
						a3DesEncKey  = iCryptoOperations.Encrypt(a3DesSessionKey,subsdb_SessionEncAlgo,ref htUpdateMsg);

						Wsst_Copy_To_Log_Data.SESSION_KEY_USED_ENCRYPTED_OUT = a3DesEncKey;
						Wsst_Copy_To_Log_Data.ENCRYPTION_RESULT = new NullableInt(1);
					}
					else
					{
						Wsst_Copy_To_Log_Data.ENCRYPTION_RESULT = new NullableInt(iKeyFetchResult);
						Wsst_Copy_To_Log_Data.ENCRYPTION_RESULT_DESCRIPTION = Copy_to_id + " Key fetch failed";

						Wsst_Sequence_Data.SetMembers(15, 
							iKeyFetchResult,Wsst_Copy_To_Log_Data.ENCRYPTION_RESULT_DESCRIPTION  , 
							DateTime.Now );

						throw new ApplicationException(Copy_to_id + " Key fetch failed");
					}

					Wsst_Sequence_Data.SetMembers(15, 
						1, null, 
						DateTime.Now );
				}
				catch(ApplicationException ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
					Wsst_Copy_To_Log_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   
				}
				catch(Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  

					Wsst_Sequence_Data.SetMembers(15, 
						3, "Session Key Encryption Failed - "+ex.Message + " - " + ex.StackTrace , 
						DateTime.Now );

					Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
					Wsst_Copy_To_Log_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   

				}
				finally
				{
					dbHelperSubscriber.Close();
				}


				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStep3);
					dbHelperWSSTCOPYT.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}
			}
			#endregion

			#region Step 4 - 20 - Create SafeTIR file for Copy Destination
			if(Wsst_Copy_To_Log_Data.LAST_STEP == null)
			{
				try
				{
					CreateFileForWSCscc(Wsst_Copy_To_Log_Data);
					Wsst_Sequence_Data.SetMembers(20, 
						1, null, 
						DateTime.Now );
				}
				catch(Exception ex)
				{
					Wsst_Sequence_Data.SetMembers(20, 
						2, ex.Message + " - " + ex.StackTrace , 
						DateTime.Now );

					Wsst_Copy_To_Log_Data.LAST_STEP = new NullableInt(Wsst_Sequence_Data.COPY_STEP) ;
					Wsst_Copy_To_Log_Data.COMPLETION_TIME = new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);   
				}


				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStep4);
					dbHelperWSSTCOPYT.CommitTransaction();  
				}
				catch(SqlException sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to JobListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}
				
			}
			#endregion
		}

		/*
SafeTIR_Message_In_ID	
ESessionKey	
EncryptedXML	
CopyToID	
CopyFromID	
TargetURL	
*/
		#region private methods
		private void CreateFileForWSCscc(WSST_COPY_TO_LOG_STRUCT Wsst_Copy_To_Log_Data)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID);
			sb.Append("\t");
			sb.Append(Wsst_Copy_To_Log_Data.ENCRYPTION_KEY_ID_USED); 
			sb.Append("\t");
			sb.Append(Convert.ToBase64String(Wsst_Copy_To_Log_Data.SESSION_KEY_USED_ENCRYPTED_OUT)); 
			sb.Append("\t");
			sb.Append(Convert.ToBase64String(Wsst_Copy_To_Log_Data.SafeTIRUploadData)); 
			sb.Append("\t");
			sb.Append(Wsst_Copy_To_Log_Data.COPY_TO_ID); 
			sb.Append("\t");
			sb.Append(Wsst_Copy_To_Log_Data.SUBSCRIBER_ID); 
			sb.Append("\t");
			sb.Append(Wsst_Copy_To_Log_Data.COPY_TO_ADDRESS); 
						
			string  FileName = Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString().Trim() + "_CopyTo_" + 
				Wsst_Copy_To_Log_Data.COPY_TO_ID.Trim()+ "_" +DateTime.Now.ToString("yyyyMMddHHmmss") +
				".cif";
			StreamWriter sw = null;
			try
			{
				sw = new StreamWriter(CopyToJobListener.m_TemporaryFolderPath + FileName, false , System.Text.Encoding.Unicode);
				sw.Write(sb.ToString());
			}
			finally
			{
				sw.Close();
			}
			File.Move(CopyToJobListener.m_TemporaryFolderPath + FileName, 
				CopyToJobListener.m_DoubleAgentDropPath + FileName);
			
		}

		#endregion
	}

}
