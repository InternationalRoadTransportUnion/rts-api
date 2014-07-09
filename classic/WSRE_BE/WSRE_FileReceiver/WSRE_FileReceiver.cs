using System;
using System.IO;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using System.Data.SqlClient;
using IRU.CommonInterfaces;


namespace IRU.RTS.WSRE
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WSRE_FileReceiver:MarshalByRefObject, IWSREFileReceiver
	{
		private const string  M_WSRE_QUERY_ID ="SAFETIR_MESSAGE_IN_ID_WSRE";
		private long m_External_Message_ID;
		private WSRE_EXTERNAL_LOG_STRUCT m_WSRE_EXTERNAL_LOG_STRUCT;

		public  WSRE_FileReceiver()
		{
		
		}
		#region IWSREFileReceiver Members

        public SafeTIRUploadAck ProcessReceivedFile ( SafeTIRReconParams ReplyParams, string SenderIP)
		{

			m_WSRE_EXTERNAL_LOG_STRUCT = new WSRE_EXTERNAL_LOG_STRUCT();

			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperWSRE = WSREFR_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("WSREExternalDB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperSubscriberDB = WSREFR_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB") ;//  null; //dbhelper for tchqdb
			
			ExternalDBHelper extDbHelper = new ExternalDBHelper(dbHelperWSRE,m_WSRE_EXTERNAL_LOG_STRUCT);
			
			#endregion
			
			#region Generate New ID
			Int32  newID = (Int32)IDHelper.GenerateID(M_WSRE_QUERY_ID ,dbHelperWSRE);
			m_External_Message_ID = newID;
			#endregion

			#region CreateReturnParam
            SafeTIRUploadAck replyAck = new SafeTIRUploadAck();
            replyAck.Sender_MessageID = ReplyParams.Sender_MessageID;
            replyAck.ResponseTime = DateTime.Now;
            replyAck.ReturnCode = 2; // everything ok by default
             //replyAck.Sender = "IRU";//TODO : Constant
				

			
			#endregion
			

			#region Populate Struct
				m_WSRE_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID = newID;
				m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS=SenderIP;
				//m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE=1200; 
			
				m_WSRE_EXTERNAL_LOG_STRUCT.SESSION_KEY_USED_ENCRYPTED_IN= ReplyParams.ESessionKey;
                m_WSRE_EXTERNAL_LOG_STRUCT.ENCRYPTION_KEY_ID = ReplyParams.MessageTag;
                //m_WSRE_EXTERNAL_LOG_STRUCT.COPY_TO_ID = ReplyParams.CopyToID;
                m_WSRE_EXTERNAL_LOG_STRUCT.SafeTIRReconData = ReplyParams.SafeTIRReconData;
                m_WSRE_EXTERNAL_LOG_STRUCT.SUBSCRIBER_ID = ReplyParams.SubscriberID;
				m_WSRE_EXTERNAL_LOG_STRUCT.ROW_CREATED_TIME=DateTime.Now;
				// take 255 characters
                if (ReplyParams.Sender_MessageID.Length > 255)
                    m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID = ReplyParams.Sender_MessageID.Substring(0, 255);
				else
                    m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID = ReplyParams.Sender_MessageID;

			#endregion

			#region InsertRecord

			try
			{
				dbHelperWSRE.ConnectToDB();
				extDbHelper.UpdateExternalLogReturnCode();
                extDbHelper.LogSafeTIRfileContentsinInDB();

			}
			catch (Exception exp)
			{
				replyAck.ReturnCode=1200;
			
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,exp.Message + "\r\n" + exp.StackTrace);
			
			}
			finally
			{
				dbHelperWSRE.Close();

			}
			#endregion

			if (replyAck.ReturnCode==2) //everything ok
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
					if ( ReplyParams.MessageTag==null)
					{
						replyAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Missing: 1210");
					}
				
					if ( ReplyParams.MessageTag.Trim()=="")
					{
						replyAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Missing: 1210");
					}
					
					if ( ReplyParams.MessageTag.Length >50)
					{
						replyAck.ReturnCode=1210;
						throw new ApplicationException("MessageTag Length exceeds limit: 1210");
					}
					if ( ReplyParams.ESessionKey==null)
					{
						replyAck.ReturnCode=1213;
						throw new ApplicationException("ESessionKey Missing: 1213");
					}

					if ( ReplyParams.SafeTIRReconData == null)
					{
						replyAck.ReturnCode=1214;
						throw new ApplicationException("Upload Data Missing: 1214");
					}
					if ( ReplyParams.Sender_MessageID == null)
					{
						replyAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Missing: 1222");
					}
					if ( ReplyParams.Sender_MessageID.Length == 0)
					{
						replyAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Zero Length: 1222");
					}
					if ( ReplyParams.Sender_MessageID.Length > 255)
					{
						replyAck.ReturnCode=1222;
						throw new ApplicationException("Sender_MessageID Exceeds 255 Length: 1222");
					}

                    if (ReplyParams.Information_Exchange_Version != "2.0.0")
                    {
                         replyAck.ReturnCode = 1223;
                        throw new ApplicationException("Information Exchange version is invalid or null");
                    }

					subsHelper = new Subscriber_DBHelper(dbHelperSubscriberDB);
				
					string sPassword, sSessionEncAlgo, sHashAlgo, sCopyToID, sCopyToAddress ;

				
					try
					{
						dbHelperSubscriberDB.ConnectToDB();

						int subscriberReturnCode = subsHelper.AuthenticateQuerySender(ReplyParams.SubscriberID,
							out sPassword,"WSRE",1, out sSessionEncAlgo,out sHashAlgo,out sCopyToID, out sCopyToAddress );
				
						if (subscriberReturnCode != 0)
						{
							replyAck.ReturnCode=subscriberReturnCode;
							throw new ApplicationException("Subscriber ID error " + subscriberReturnCode.ToString());
						}
						/*#region copytoID validation
						
						if (ReplyParams.CopyToID != null)
						{
							if (ReplyParams.CopyToID.Trim() !="")
							{
								if (sCopyToID == null)
								{
									replyAck.ReturnCode=1220;
									throw new ApplicationException("Copy To ID error " + replyAck.ReturnCode);
								}
								else if (sCopyToID!= ReplyParams.CopyToID)
								{
									
										replyAck.ReturnCode=1220;
										throw new ApplicationException("Copy To ID error " + replyAck.ReturnCode);
									
								}
							}
						}
						#endregion*/

					}
					finally
					{
						dbHelperSubscriberDB.Close();
					}

					
				}

				catch (ApplicationException apex)
				{
					m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=apex.Message + " - " + apex.StackTrace ; 
				}
				catch (SqlException sqlEx)
				{
					replyAck.ReturnCode=1200;
					m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=sqlEx.Message + " - " + sqlEx.StackTrace ; 
				}
				catch (Exception sqlEx)
				{
					replyAck.ReturnCode=1200; //all success
					m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=sqlEx.Message + " - " + sqlEx.StackTrace ; 
				}
			
			

				#endregion

			}
			if (replyAck.ReturnCode==2) //everything ok
			{
				#region Generate File

				try
				{
					CreateSafeTIRDropFile();
				}
				catch (Exception fex) //file io exception expected
				{
					replyAck.ReturnCode=1200;
					m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE_DESCRIPTION=fex.Message+" \n-"+fex.StackTrace ; 
				}


				#endregion
			}

			

			m_WSRE_EXTERNAL_LOG_STRUCT.RETURN_CODE=new NullableInt(replyAck.ReturnCode);

			#region Update with Return_code and description

			try
			{
				dbHelperWSRE.ConnectToDB();
				extDbHelper.UpdateExternalLogReturnCode();
			}
			catch (Exception exp)
			{
				replyAck.ReturnCode=1200;
				
				
				
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,exp.Message + "\r\n" + exp.StackTrace);
				
			}
			finally
			{
				dbHelperWSRE.Close();

			}
			#endregion


			
			
			replyAck.Sender_MessageID=ReplyParams.Sender_MessageID;

			replyAck.ResponseTime=DateTime.Now;

			return replyAck;
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
		string sFileName= m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS  + "_" + m_WSRE_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID +"_" +
				DateTime.Now.ToString("yyMMddHHmm")+ ".Reply";
	
				
		#endregion

			#region create File
			StreamWriter sw = null;
			try
			{
                
				sw = new StreamWriter(WSREFR_RemotingHelper.m_TemporaryFolderPath + sFileName,false,System.Text.Encoding.Unicode);
				sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID.ToString().Trim());
				sw.Write("\t");
				sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.SUBSCRIBER_ID);
				sw.Write("\t");
                sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.ENCRYPTION_KEY_ID);
				sw.Write("\t");
				sw.Write(Convert.ToBase64String(m_WSRE_EXTERNAL_LOG_STRUCT.SESSION_KEY_USED_ENCRYPTED_IN));
				sw.Write("\t");
				sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_TCP_IP_ADDRESS);
				sw.Write("\t");
				sw.Write(Convert.ToBase64String(m_WSRE_EXTERNAL_LOG_STRUCT.SafeTIRReconData));
				sw.Write("\t");
				sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.SENDER_MESSAGEID);
				sw.Write("\t");
				//sw.Write(m_WSRE_EXTERNAL_LOG_STRUCT.COPY_TO_ID);
				
			}
			finally
			{
				sw.Close();
			
			}
#endregion create File
			#region Copy File
			
			File.Copy(WSREFR_RemotingHelper.m_TemporaryFolderPath + sFileName,
						WSREFR_RemotingHelper.m_DoubleAgentDropPath + sFileName);

						
			

			#endregion create File
 		}

	}
}
