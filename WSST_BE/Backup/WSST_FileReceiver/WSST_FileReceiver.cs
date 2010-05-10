using System;
using System.IO;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using System.Data.SqlClient;
using IRU.CommonInterfaces;


namespace IRU.RTS.WSST
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WSST_FileReceiver:MarshalByRefObject, IWSSTFileReceiver
	{
		private const string  M_WSST_QUERY_ID ="SAFETIR_MESSAGE_IN_ID";
		private long m_External_Message_ID;
		private WSST_EXTERNAL_LOG_STRUCT m_WSST_EXTERNAL_LOG_STRUCT;

		public WSST_FileReceiver()
		{
		
		}
		#region IWSSTFileReceiver Members

		public SafeTIRUploadAck ProcessReceivedFile(SafeTIRUploadParams TIRUploadParams, string SenderIP)
		{

			
			m_WSST_EXTERNAL_LOG_STRUCT = new WSST_EXTERNAL_LOG_STRUCT();

			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperWSST = WSSTFR_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("WSSTExternalDB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperSubscriberDB = WSSTFR_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB") ;//  null; //dbhelper for tchqdb
			
			ExternalDBHelper extDbHelper = new ExternalDBHelper(dbHelperWSST,m_WSST_EXTERNAL_LOG_STRUCT);
			
			#endregion
			
			#region Generate New ID
			Int32  newID = (Int32)IDHelper.GenerateID(M_WSST_QUERY_ID ,dbHelperWSST);
			m_External_Message_ID = newID;
			#endregion

			#region CreateReturnParam
			SafeTIRUploadAck safeAck = new SafeTIRUploadAck();
			safeAck.Sender_MessageID= TIRUploadParams.Sender_MessageID;
			safeAck.ResponseTime=DateTime.Now;
			safeAck.ReturnCode=2; // everything ok by default
			safeAck.Sender="IRU" ;//TODO : Constant
				

			
			#endregion
			

			#region Populate Struct
				m_WSST_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID = newID;
				m_WSST_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS=SenderIP;
				//m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE=1200; 
			
				m_WSST_EXTERNAL_LOG_STRUCT.SESSION_KEY_USED_ENCRYPTED_IN= TIRUploadParams.ESessionKey;
				m_WSST_EXTERNAL_LOG_STRUCT.ENCRYPTION_KEY_ID= TIRUploadParams.MessageTag;
				m_WSST_EXTERNAL_LOG_STRUCT.COPY_TO_ID=TIRUploadParams.CopyToID;
				m_WSST_EXTERNAL_LOG_STRUCT.SafeTIRUploadData =TIRUploadParams.safeTIRUploadData;
				m_WSST_EXTERNAL_LOG_STRUCT.SUBSCRIBER_ID =TIRUploadParams.SubscriberID;
				m_WSST_EXTERNAL_LOG_STRUCT.ROW_CREATED_TIME=DateTime.Now;
				// take 255 characters
				if (TIRUploadParams.Sender_MessageID.Length >255)
					m_WSST_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID=TIRUploadParams.Sender_MessageID.Substring(0,255);
				else
					m_WSST_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID=TIRUploadParams.Sender_MessageID;

			#endregion

			#region InsertRecord

			try
			{
				dbHelperWSST.ConnectToDB();
				extDbHelper.LogSafeTIRfileContentsinInDB();
				

			}
			catch (Exception exp)
			{
				safeAck.ReturnCode=1200;
			
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,exp.Message + "\r\n" + exp.StackTrace);
			
			}
			finally
			{
				dbHelperWSST.Close();

			}
			#endregion

			if (safeAck.ReturnCode==2) //everything ok
			{

				#region validate parameters
				/*
				Value	Applied To	Message
				2		Success
				Values representing error:
				1200		Any unclassified error
		
				1210	Message Tag	No Message Tag Value
				1212	SubscriberID	Subscriber ID Missing or Bad formatted or not registered
				1213	ESessionKey	Session key missing
				1214	SafeTIRUploadData, TIRCarnetHolderQueryParams	Encrypted data missing
				1220	CopyToID	Unregistered value in CopytoID
				1222	Sender_MessageID	Value is more than maximum length specified or Missing
				*/		
	
				
				Subscriber_DBHelper subsHelper = null; 
				try
				{
					if ( TIRUploadParams.MessageTag==null)
					{
						safeAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Missing: 1210");
					}
				
					if ( TIRUploadParams.MessageTag.Trim()=="")
					{
						safeAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Missing: 1210");
					}
					
					if ( TIRUploadParams.MessageTag.Length >50)
					{
						safeAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Length exceeds limit: 1210");
					}
					if ( TIRUploadParams.ESessionKey==null)
					{
						safeAck.ReturnCode=1213;
						throw new ApplicationException("ESessionKey Missing: 1213");
					}

					if ( TIRUploadParams.safeTIRUploadData == null)
					{
						safeAck.ReturnCode=1214;
						throw new ApplicationException("Upload Data Missing: 1214");
					}
					if ( TIRUploadParams.Sender_MessageID == null)
					{
						safeAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Missing: 1222");
					}
					if ( TIRUploadParams.Sender_MessageID.Length == 0)
					{
						safeAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Zero Length: 1222");
					}
					if ( TIRUploadParams.Sender_MessageID.Length > 255)
					{
						safeAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Exceeds 255 Length: 1222");
					}

					subsHelper = new Subscriber_DBHelper(dbHelperSubscriberDB);
				
					string sPassword, sSessionEncAlgo, sHashAlgo, sCopyToID, sCopyToAddress ;

				
					try
					{
						dbHelperSubscriberDB.ConnectToDB();

						int subscriberReturnCode = subsHelper.AuthenticateQuerySender(TIRUploadParams.SubscriberID,
							out sPassword,"WSST",1, out sSessionEncAlgo,out sHashAlgo,out sCopyToID, out sCopyToAddress );
				
						if (subscriberReturnCode != 0)
						{
							safeAck.ReturnCode=subscriberReturnCode;
							throw new ApplicationException("Subscriber ID error " + subscriberReturnCode.ToString());
						}
						#region copytoID validation
						
						if (TIRUploadParams.CopyToID != null)
						{
							if (TIRUploadParams.CopyToID.Trim() !="")
							{
								if (sCopyToID == null)
								{
									safeAck.ReturnCode=1220;
									throw new ApplicationException("Copy To ID error " + safeAck.ReturnCode);
								}
								else if (sCopyToID!= TIRUploadParams.CopyToID)
								{
									
										safeAck.ReturnCode=1220;
										throw new ApplicationException("Copy To ID error " + safeAck.ReturnCode);
									
								}
							}
						}
						#endregion

					}
					finally
					{
						dbHelperSubscriberDB.Close();
					}

					
				}

				catch (ApplicationException apex)
				{
					m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=apex.Message + " - " + apex.StackTrace ; 
				}
				catch (SqlException sqlEx)
				{
					safeAck.ReturnCode=1200;
					m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=sqlEx.Message + " - " + sqlEx.StackTrace ; 
				}
				catch (Exception sqlEx)
				{
					safeAck.ReturnCode=1200; //all success
					m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=sqlEx.Message + " - " + sqlEx.StackTrace ; 
				}
			
			

				#endregion

			}
			if (safeAck.ReturnCode==2) //everything ok
			{
				#region Generate File

				try
				{
					CreateSafeTIRDropFile();
				}
				catch (Exception fex) //file io exception expected
				{
					safeAck.ReturnCode=1200;
					m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=fex.Message+" \n-"+fex.StackTrace ; 
				}


				#endregion
			}

			

			m_WSST_EXTERNAL_LOG_STRUCT.RETURN_CODE=new NullableInt(safeAck.ReturnCode);

			#region Update with Return_code and description

			try
			{
				dbHelperWSST.ConnectToDB();
				extDbHelper.UpdateExternalLogReturnCode();
			}
			catch (Exception exp)
			{
				safeAck.ReturnCode=1200;
				
				
				
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,exp.Message + "\r\n" + exp.StackTrace);
				
			}
			finally
			{
				dbHelperWSST.Close();

			}
			#endregion


			
			
			safeAck.Sender_MessageID=TIRUploadParams.Sender_MessageID;

			safeAck.ResponseTime=DateTime.Now;

			return safeAck;
		}

		#endregion

		private void CreateSafeTIRDropFile()
		{
			/*
SafeTIR_Message_IN_ID
SubscriberID
MessageTag
SenderIP Address
SafeTIRUploadData
Sender_MessageID
CopyToID
EOF (eof is not required)*/
			#region ComputeFileNAme
		string sFileName= m_WSST_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS  + "_" + m_WSST_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID +"_" +
				DateTime.Now.ToString("yyMMddHHmm")+ ".SafeTIR";
	
				
		#endregion

			#region create File
			StreamWriter sw = null;
			try
			{
				sw = new StreamWriter(WSSTFR_RemotingHelper.m_TemporaryFolderPath + sFileName,false,System.Text.Encoding.Unicode);
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID.ToString().Trim());
				sw.Write("\t");
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.SUBSCRIBER_ID);
				sw.Write("\t");
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.ENCRYPTION_KEY_ID);
				sw.Write("\t");
				sw.Write(Convert.ToBase64String(m_WSST_EXTERNAL_LOG_STRUCT.SESSION_KEY_USED_ENCRYPTED_IN));
				sw.Write("\t");
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS);
				sw.Write("\t");
				sw.Write(Convert.ToBase64String(m_WSST_EXTERNAL_LOG_STRUCT.SafeTIRUploadData));
				sw.Write("\t");
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID);
				sw.Write("\t");
				sw.Write(m_WSST_EXTERNAL_LOG_STRUCT.COPY_TO_ID);
				
			}
			finally
			{
				sw.Close();
			
			}
#endregion create File
			#region Copy File
			
			File.Copy(WSSTFR_RemotingHelper.m_TemporaryFolderPath + sFileName,
						WSSTFR_RemotingHelper.m_DoubleAgentDropPath + sFileName);

						
			

			#endregion create File
 		}

	}
}
