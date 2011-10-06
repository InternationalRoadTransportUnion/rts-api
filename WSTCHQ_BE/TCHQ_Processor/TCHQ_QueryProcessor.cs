using System;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using System.Data;
using System.Data.SqlClient ;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces; 
using IRU.CryptEngine ;
using System.Xml;
using System.Collections;
using IRU.RTS.Crypto;


namespace IRU.RTS.WSTCHQ
{
	/// <summary>
	/// Summary description for TCHQ_QueryProcessor.
	/// </summary>
	public class TCHQ_QueryProcessor:MarshalByRefObject, ITCHQProcessor
	{
		private ICryptoOperations m_iCryptoOperations;

		private string m_sTCHQ_Query_Id;

//		private SqlCommand m_cmdTCHQSeqTbl;
//		private string m_subscriberPassword;

		//private string m_sEncryptKeyTblName;

		private static string GetBlankIfNull(object Obj)
		{
			if(Obj == null)
			{
				return "";
			}
			else
			{
				return Obj.ToString();
			}
		}

		private static string GetBlankDateIfNull(object DateTimeobj)
		{
			if(DateTimeobj == null)
			{
				return "";
			}
			else
			{
				//return XmlConvert.ToString((DateTime)DateTimeobj); 
				return ((DateTime)DateTimeobj).ToString("yyyy-MM-dd"); 
			}
		}

		public TCHQ_QueryProcessor()
		{
			m_sTCHQ_Query_Id = "TCHQ_Query_ID";
		}

//////		public void SetEncryptKeyTableName(string EncryptKeyTableName)
//////		{
//////			m_sEncryptKeyTblName = EncryptKeyTableName;
//////		}


		/// <summary>
		/// Updates the RESPONSE_RESULT and time columns. The WEb Service Layer will call back this method after calling 
		/// processquery and checking if the client is still connected
		/// </summary>
		/// <param name="QueryId"> Query ID , key to row in log table </param>
		/// <param name="dtResponseSent">Time in the WS layer</param>
		/// <param name="ResponseResult">true=client was still connected, false= client was not connected </param>
		[System.Runtime.Remoting.Messaging.OneWay] 
		public void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult)
		{
			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperTCHQ = TCHQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TCHQDB") ;//  null; //dbhelper for tchqdb
			TCHQ_DBHelper tchqDbHelper = new TCHQ_DBHelper(dbHelperTCHQ); 
			#endregion

			try
			{
				dbHelperTCHQ.ConnectToDB();	
				tchqDbHelper.LogRequestResponse(QueryId, dtResponseSent, ResponseResult);
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
			}
			finally
			{
				dbHelperTCHQ.Close();
			}
			

		}

		public TIRHolderResponse  ProcessQuery(TIRHolderQuery tirHolderQueryData, string SenderIP, out long IRUQueryId)
		{
			

			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperTCHQ = TCHQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TCHQDB") ;//  null; //dbhelper for tchqdb
			IDBHelper dbHelperSubscriber = TCHQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB") ;//  null; //dbhelper for tchqdb
			#endregion

			#region a. Generate New ID
			long newID = IDHelper.GenerateID(m_sTCHQ_Query_Id,dbHelperTCHQ);
			IRUQueryId = newID;
			#endregion

			#region Initialize / create Variables 

			TCHQ_DBHelper tchqDbHelper = new TCHQ_DBHelper(dbHelperTCHQ); 
			Subscriber_DBHelper subsDbHelper = new Subscriber_DBHelper(dbHelperSubscriber); 

			bool continueChecking = true;
			TCHQLogRequestStruct tchqRequestLogData = new TCHQLogRequestStruct ();
			SubscriberDetailsStruct subscriberDetails = new SubscriberDetailsStruct();
			RSACryptoKey sessionDecrKey=null;
			int keyStatus = 0;

			#endregion

			#region Populate tchqRequestLogData with available data as on step 5 
			//b. Insert a row in TCHQ_REQUEST_LOG
			tchqRequestLogData.TCHQ_QueryID = newID;
			tchqRequestLogData.senderQueryID = tirHolderQueryData.Query_ID ;
			tchqRequestLogData.decryptionKeyID = tirHolderQueryData.MessageTag ; 
			tchqRequestLogData.senderID = tirHolderQueryData.SubscriberID ;
			tchqRequestLogData.encryptedQueryParams = tirHolderQueryData.TIRCarnetHolderQueryParams ;
			tchqRequestLogData.encryptedSessionKeyIn = tirHolderQueryData.ESessionKey ;
			tchqRequestLogData.senderTCPAddress = SenderIP;
			tchqRequestLogData.rowCreationTime = DateTime.Now ;

			if(tirHolderQueryData.Query_ID.Length > 20)
			{
				tchqRequestLogData.senderQueryID = "";
				tchqRequestLogData.returnCode = 1222;
				tchqRequestLogData.lastStep = 5; 
				continueChecking = false;
			}
			#endregion

			#region Step 5 - Initial Log Request & Validate Params 

			if(continueChecking)
			{
				if(tirHolderQueryData.MessageTag  == null || tirHolderQueryData.MessageTag.Trim() == "")
				{
					tchqRequestLogData.returnCode = 1210;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
					//tchqRequestLogData.decryptionKeyID = null;
				}
			}

			if(continueChecking)
			{
				//Get Keys for Decryption using MessageTag
				try
				{

					dbHelperSubscriber.ConnectToDB();
					keyStatus = KeyManager.GetIRUKeyDetails(tirHolderQueryData.MessageTag,tirHolderQueryData.SubscriberID , out sessionDecrKey,dbHelperSubscriber);
				}
				finally
				{
					dbHelperSubscriber.Close(); 
				}
				if(keyStatus ==3)
				{
					tchqRequestLogData.returnCode = 1211;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "KeyStatus for Subscriber "  + tirHolderQueryData.SubscriberID + " - " + tirHolderQueryData.MessageTag + " = "+keyStatus.ToString());
					//TODO:Update TCHQLOG & Sequence with Key Status 1200
					//return new TIRHolderResponse(); // return to FCS the respose struct
				}
				else if(keyStatus == 9)
				{
					tchqRequestLogData.returnCode = 1211;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
				}

			}
			if(continueChecking)
			{
				if((tirHolderQueryData.SubscriberID == null || tirHolderQueryData.SubscriberID.Trim() == ""))
				{
					tchqRequestLogData.returnCode = 1212;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
					//tchqRequestLogData.senderID = null;
				}
			}
			if(continueChecking)
			{
				
				subscriberDetails.subscriberID =  tirHolderQueryData.SubscriberID;
				int iAuthenticateQuerySender = 0;
				try
				{
					dbHelperSubscriber.ConnectToDB();
					iAuthenticateQuerySender  = subsDbHelper.AuthenticateQuerySender(subscriberDetails.subscriberID , 
						out subscriberDetails.password, "TCHQ", 1, out subscriberDetails.SessionKeyAlgo,   
						out subscriberDetails.HashAlgo, out subscriberDetails.CopyToId, out subscriberDetails.CopyToAddress );
				}
				finally
				{
					dbHelperSubscriber.Close();
				}
				//Also it is checked here for Invalid Method instead of Step 30
				if(	iAuthenticateQuerySender  !=	0)
				{
					tchqRequestLogData.returnCode = iAuthenticateQuerySender ;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
				}
			}

			if(continueChecking)
			{
				if(tirHolderQueryData.ESessionKey  == null)
				{
					tchqRequestLogData.returnCode = 1213;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
					//tchqRequestLogData.encryptedSessionKeyIn = null;
				}
				else
				{
					if(tirHolderQueryData.ESessionKey.Length ==0)
					{
						tchqRequestLogData.returnCode = 1213;
						tchqRequestLogData.lastStep = 5; 
						continueChecking = false;
						//tchqRequestLogData.encryptedSessionKeyIn = null;
					}
				}
			}

			if(continueChecking)
			{
				if(tirHolderQueryData.TIRCarnetHolderQueryParams == null)
				{
					tchqRequestLogData.returnCode = 1214;
					tchqRequestLogData.lastStep = 5; 
					continueChecking = false;
					//tchqRequestLogData.encryptedQueryParams = null;
				}
				else
				{
					if(tirHolderQueryData.TIRCarnetHolderQueryParams.Length ==0)
					{
						tchqRequestLogData.returnCode = 1214;
						tchqRequestLogData.lastStep = 5; 
						continueChecking = false;
						//tchqRequestLogData.encryptedQueryParams = null;
						
					}
				}
			}

			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(continueChecking)
				{
					tchqDbHelper.LogRequestS5(tchqRequestLogData, false);
					tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 5 , tchqRequestLogData.returnCode,"" ,tchqRequestLogData.rowCreationTime);  
				}
				else
				{
					tchqDbHelper.LogRequestS5(tchqRequestLogData, true);
					tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 5 , tchqRequestLogData.returnCode, "",tchqRequestLogData.rowCreationTime);  
				}
				//Insert step in Sequence table
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}
			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}
			#endregion

			#region  Step 10 - Session Key Decryption

			///In this section we do not have to check for 3 or 9 as if it was 3 or 9 it would not reach here.
			///it will fail in sep 5 itself.
			int iStep10Result = 1;
			string iStep10ResultDesc = "";
			byte [] decrSessionKeyIn = null;

			if(keyStatus != 7)
			{
				m_iCryptoOperations = (ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations), TCHQ_RemotingHelper.m_CryptoProviderEndpoint);

				Hashtable hashForSessionKey = new Hashtable();
				hashForSessionKey["MODULUS"] = sessionDecrKey.Modulus ;
				hashForSessionKey["EXPONENT"] = sessionDecrKey.Exponent ;
				hashForSessionKey["D"] = sessionDecrKey.D ;
				hashForSessionKey["P"] = sessionDecrKey.P ;
				hashForSessionKey["Q"] = sessionDecrKey.Q ;
				hashForSessionKey["DP"] = sessionDecrKey.DP ;
				hashForSessionKey["DQ"] = sessionDecrKey.DQ ;
				hashForSessionKey["INVERSEQ"] = sessionDecrKey.INVERSEQ ;
			
				tchqRequestLogData.decryptedSessionKeyIn =  null;

				try
				{
					decrSessionKeyIn = m_iCryptoOperations.Decrypt(tirHolderQueryData.ESessionKey , subscriberDetails.SessionKeyAlgo, hashForSessionKey); 

					tchqRequestLogData.decryptedSessionKeyIn =  decrSessionKeyIn;
				}
				catch(Exception ex)
				{
					iStep10Result = 8;
					tchqRequestLogData.returnCode = 1230;
					tchqRequestLogData.lastStep = 10; 
					
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning , ex.Message + " - " + ex.StackTrace );  

					iStep10ResultDesc = ex.Message + " -> " + ex.StackTrace ;
					continueChecking = false;
				}
			}
			else
			{
				iStep10Result = 7;
				tchqRequestLogData.returnCode = 1230;
				tchqRequestLogData.lastStep = 10; 
				continueChecking = false;
			}
			
			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep10Result ==1)
				{
					tchqDbHelper.LogRequestS10(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS10(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 10 , iStep10Result, iStep10ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}
			
			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}
			#endregion

			#region Step 15 - do Decryption & Get Decrypted Data.

			int iStep15Result = 1;
			string sStep15ResultDesc = "";
			//This is to be used 
			Hashtable hashDecryptParams = new Hashtable();
			
			byte[] byIV = new byte[]{0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};

			hashDecryptParams["KEY"] = decrSessionKeyIn;
			hashDecryptParams["IV"] = byIV;

			try
			{
				byte [] baDecryptedQueryParamXML =
					m_iCryptoOperations.Decrypt(tchqRequestLogData.encryptedQueryParams, 
					"3DES" , hashDecryptParams);  

				iStep15Result = 1;

				//tchqRequestLogData.decryptedQueryParamXML = Convert.ToBase64String(decryptedQueryParamXML);
				tchqRequestLogData.decryptedQueryParamXML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);
				tchqRequestLogData.decryptionResult = 1;
   
			}
			catch(Exception ex)
			{
				iStep15Result = 8;
				tchqRequestLogData.returnCode = 1231;
				tchqRequestLogData.lastStep = 15; 
					
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				sStep15ResultDesc = ex.Message + " -> " + ex.StackTrace ;

				tchqRequestLogData.decryptedQueryParamXML = null;
				tchqRequestLogData.decryptionResult = 8;
				continueChecking = false;
			}
			tchqRequestLogData.decryptionResultDesc = sStep15ResultDesc;
			tchqRequestLogData.rowCreationTime = DateTime.Now;

			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep15Result ==1)
				{
					tchqDbHelper.LogRequestS15(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS15(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 15 , iStep15Result, sStep15ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}

			//
			//			//TODO:forNowJust do the following
			//			#region this code needs to be replaced with Actual Decryption Code
			//			tchqRequestLogData.decryptedQueryParamXML = DecryptXML(decryptedQueryParamXML);
			//			tchqRequestLogData.decryptionResult = 1;
			//			tchqRequestLogData.decryptionResultDesc = "";
			//			#endregion


			#endregion

			#region Step 20 - do Validate Hash
			int iStep20Result = 1;
			string sStep20ResultDesc = "";

			try
			{
				/* //replaced by regexhelper
				int iHashStart = tchqRequestLogData.decryptedQueryParamXML.IndexOf("<Hash>");

				if(iHashStart >0)
				{
					iHashStart += 6;
				}
				else
				{
					throw new ApplicationException("No Hash found");
				}
				int iHashEnd = tchqRequestLogData.decryptedQueryParamXML.IndexOf("</Hash>");
				int iHashLength = iHashEnd - iHashStart; 
				string sHash =  tchqRequestLogData.decryptedQueryParamXML.Substring(iHashStart,iHashLength);  
				*/
				
				string sHash = RegExHelper.ExtractHASH(tchqRequestLogData.decryptedQueryParamXML);//.Substring(iHashStart,iHashLength);  

				if (sHash.Trim()=="")
				{
					throw new ApplicationException("No Hash found");
				}
				

				byte [] baHash = Convert.FromBase64String(sHash);

				/* //replaced by regexhelper
				int iBodyStart = tchqRequestLogData.decryptedQueryParamXML.IndexOf("<Body>");
				if(iBodyStart > 0)
				{
					iBodyStart += 6;
				}
				else
				{
					throw new ApplicationException("No Body Node found");
				}
				int iBodyEnd = tchqRequestLogData.decryptedQueryParamXML.IndexOf("</Body>");
				int iBodyLength = iBodyEnd - iBodyStart ; 
				string sBody =  tchqRequestLogData.decryptedQueryParamXML.Substring(iBodyStart,iBodyLength );  
				*/

				string sBody = RegExHelper.ExtractBODYContents (tchqRequestLogData.decryptedQueryParamXML);//.Substring(iBodyStart,iBodyLength );  
				
				if (sBody.Trim()=="")
				{
					throw new ApplicationException("No Body Node found");
				}
				
				
				
				byte [] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);

			
				if(!m_iCryptoOperations.VerifyHash(baBody,subscriberDetails.HashAlgo, null, baHash))
				{
					tchqRequestLogData.returnCode = 1200;
					tchqRequestLogData.lastStep = 20; 
					iStep20Result = 7;
					sStep20ResultDesc = "Hash Verification Failed";
					continueChecking = false;
				}
			}
			catch(Exception ex)
			{
				tchqRequestLogData.returnCode = 1200;
				tchqRequestLogData.lastStep = 20; 
				iStep20Result = 8;
				sStep20ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  

				continueChecking = false;
			}


			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep20Result ==1)
				{
					tchqDbHelper.LogRequestS20(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS20(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 20 , iStep20Result, sStep20ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}

			#endregion

			#region Step 25 - Validate against request XSD
			int iStep25Result = 1;
			string sStep25ResultDesc = "";
			try
			{
				XMLValidationHelper xvh = new XMLValidationHelper();
				if(!xvh.ValidateXML(tchqRequestLogData.decryptedQueryParamXML, out tchqRequestLogData.invalidQueryXMLReason))
				{
					iStep25Result = GetTCHQueryXMLErrorCode(tchqRequestLogData.invalidQueryXMLReason,out tchqRequestLogData.invalidQueryXMLReason);
					throw new ApplicationException(tchqRequestLogData.invalidQueryXMLReason);
				}
			}
			catch(Exception ex)
			{
				tchqRequestLogData.returnCode = iStep25Result; 
				tchqRequestLogData.lastStep = 25; 
				sStep25ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  

				continueChecking = false;
			}
			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep25Result ==1)
				{
					tchqDbHelper.LogRequestS25(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS25(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 25 , iStep25Result, sStep25ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}
			
			
			#endregion
			
			#region Validate Signature - Not in Use Anymore
			/*
			//Extract Signature & KeyID 
			XmlDocument xmlQueryDoc = new XmlDocument();
			xmlQueryDoc.LoadXml(tchqRequestLogData.decryptedQueryParamXML);

			ParseXML(xmlQueryDoc, ref TIRCarnetHolderParamsData,1 );

			RSACryptoKey subscriberPublicKey;
			int iSubscriberKeyStatus;
			KeyManager.GetSubscriberKeyDetails(TIRCarnetHolderParamsData.Sender, 
				TIRCarnetHolderParamsData.KeyId, out subscriberPublicKey, out  iSubscriberKeyStatus);
	
			if(iSubscriberKeyStatus !=1)
			{
				//TODO:Update TCHQLOG & Sequence with Key Status 1200
				return new TIRHolderResponse();// return to FCS the respose struct
			}

			if(!ValidateSignature(ref tchqRequestLogData, 
				TIRCarnetHolderParamsData, 
				xmlQueryDoc, 
				subscriberPublicKey ))
			{
				//TODO:Update TCHQLOG & Sequence with Key Status 1200
				return new TIRHolderResponse();// return to FCS the respose struct
			}
			else
			{
				tchqDbHelper.LogRequestS20(tchqRequestLogData);
				tchqDbHelper.LogSequenceStep(newID, 15 , 1, DateTime.Now);  
			}
			*/
			#endregion

			#region Step 30 - Authorize User
			int iStep30Result = 1;
			string sStep30ResultDesc = "";
			try
			{
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(tchqRequestLogData.decryptedQueryParamXML);

				XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
				xns.AddNamespace("def","http://www.iru.org/TCHQuery");

				XmlNode nodeSender = xd.DocumentElement.SelectSingleNode("/def:Query/def:Body/def:Sender",xns);
				string BodySender = nodeSender.InnerText;
				if(subscriberDetails.subscriberID.Trim() != BodySender.Trim())
				{
					tchqRequestLogData.returnCode =  1242;
					iStep30Result = 2;
					sStep30ResultDesc = "Sender Not Found or Not Same as SubscriberID";
					throw new ApplicationException(sStep30ResultDesc);
				}
				tchqRequestLogData.senderID = BodySender;

				XmlNode node = xd.DocumentElement.SelectSingleNode("/def:Query/def:Body/def:Password",xns);
				if(node == null)
				{
					tchqRequestLogData.senderPassword = "";
					if(subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
					{
						//Password is valid - there is no password or password node also might not be present
					}
					else
					{	
						tchqRequestLogData.returnCode =  1233;
						iStep30Result = 3;
						sStep30ResultDesc = "Password does not match";
						throw new ApplicationException("Password Verification Failed");
					}
				}
				else
				{
					string password = node.InnerText ;
					tchqRequestLogData.senderPassword = password;
					if(subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
					{
						if(password.Trim() == "")
						{

							//PAssword is valid - there is no password or password node also might no be present
						}
						else
						{
							tchqRequestLogData.returnCode =  1233;
							iStep30Result = 3;
							sStep30ResultDesc = "Password does not match";
							throw new ApplicationException("Password Verification Failed");
						}
					}
					else if(password.Trim() == subscriberDetails.password)
					{
						//PAssword is valid - there is no password or password node also might no be present
					}
					else
					{
						tchqRequestLogData.returnCode =  1233;
						iStep30Result = 3;
						sStep30ResultDesc = "Password does not match";
						throw new ApplicationException("Password Verification Failed");
					}
				}
				node = xd.DocumentElement.SelectSingleNode("/def:Query/def:Body/def:Sender",xns);
				if(node == null)
				{
					tchqRequestLogData.returnCode =  1242;
					iStep30Result = 2;
					sStep30ResultDesc = "Sender Not found";
					throw new ApplicationException("Sender Verification Failed");
				}
				else
				{
					if(node.InnerText.Trim().ToUpper() != tchqRequestLogData.senderID.Trim().ToUpper())
					{
						tchqRequestLogData.returnCode =  1242;
						iStep30Result = 2;
						sStep30ResultDesc = "Sender Not Valid";
						throw new ApplicationException("Sender Verification Failed");
					}
				}
				tchqRequestLogData.senderAuthenticated = iStep30Result;
			}
			catch(ApplicationException ex)
			{	
				tchqRequestLogData.lastStep = 30; 
				continueChecking = false;
			}
			catch(Exception ex)
			{
				if(iStep30Result  == 1)
				{
					tchqRequestLogData.returnCode =  1200;
					iStep30Result =  1200;
				}
				tchqRequestLogData.lastStep = 30; 
				sStep30ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep30Result ==1)
				{
					tchqDbHelper.LogRequestS30(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS30(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 30 , iStep30Result, sStep30ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}
			#endregion

			#region Step 35 - Get CW Query Result
			int iStep35Result = 1;
			string sStep35ResultDesc = "";
			Hashtable cwHashTable = null;
			XmlNode nodeResponseBody = null ;
			XmlNamespaceManager xnsResponse = null;
			XmlDocument xdResponse = new XmlDocument();
			int iCWResultCode = 0;

			try
			{
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(tchqRequestLogData.decryptedQueryParamXML);

				XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
				xns.AddNamespace("def","http://www.iru.org/TCHQuery");
/*
				XmlNode node = xd.DocumentElement.SelectSingleNode("/def:Query/def:Body",xns);
				tchqRequestLogData.originatorID = node["Originator"].InnerText;
				tchqRequestLogData.originTime = DateTime.Parse(node["OriginTime"].InnerText);
				tchqRequestLogData.queryType = Convert.ToInt32(node["Query_Type"].InnerText); 
				tchqRequestLogData.queryReason = Convert.ToInt32(node["Query_Reason"].InnerText); 
				tchqRequestLogData.tirCarnetNumber = node["Carnet_Number"].InnerText;
				*/

				XmlNode node = xd.DocumentElement.SelectSingleNode("/def:Query/def:Body",xns);
				tchqRequestLogData.originatorID = node.SelectSingleNode("./def:Originator",xns).InnerText;
				tchqRequestLogData.originTime = DateTime.Parse(node.SelectSingleNode("./def:OriginTime",xns).InnerText);
				tchqRequestLogData.queryType = Convert.ToInt32(node.SelectSingleNode("./def:Query_Type",xns).InnerText); 
				tchqRequestLogData.queryReason = Convert.ToInt32(node.SelectSingleNode("./def:Query_Reason",xns).InnerText); 
				tchqRequestLogData.tirCarnetNumber = node.SelectSingleNode("./def:Carnet_Number",xns).InnerText;

//	<Sender>FCS</Sender>
//	<SentTime>2004-05-19T13:54:50Z</SentTime>
//	<Originator>Originator1</Originator>
//	<OriginTime>2004-05-19T13:54:50Z</OriginTime>
//	<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
//	<Query_Type>1</Query_Type>
//	<Query_Reason>1</Query_Reason>
//	<Carnet_Number>15042217</Carnet_Number>

				#region Get IDBHelper instances from Plugin Manager
				IDBHelper dbCurrent = TCHQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("CurrentDB"); //dbhelper for current
				#endregion

				CWQuerys cq = new CWQuerys(dbCurrent);
				cwHashTable = cq.GetTIRCarnetQueryData(tchqRequestLogData.tirCarnetNumber);

				iCWResultCode =  int.Parse(cwHashTable["Query_Result_Code"].ToString());
			
				xdResponse = TCHQ_RemotingHelper.m_InMemoryCachePlugin.GetXMLDomFromCache("QueryRespTemplate");
				xnsResponse = new XmlNamespaceManager( xdResponse.NameTable);
				xnsResponse.AddNamespace("def","http://www.iru.org/TCHQResponse");
	
				nodeResponseBody = xdResponse.DocumentElement.SelectSingleNode("/def:QueryResponse/def:Body",xnsResponse);
				/*
				nodeResponseBody["Sender"].InnerText = "IRU" ;
				nodeResponseBody["Originator"].InnerText = tchqRequestLogData.originatorID ;
				nodeResponseBody["ResponseTime"].InnerText = XmlConvert.ToString(DateTime.UtcNow);
				nodeResponseBody["Result"].InnerText = cwHashTable["Query_Result_Code"].ToString(); 
				nodeResponseBody["Carnet_Number"].InnerText = tchqRequestLogData.tirCarnetNumber ;
				nodeResponseBody["HolderID"].InnerText = GetBlankIfNull(cwHashTable["Holder_ID"]); 
				nodeResponseBody["ValidityDate"].InnerText = GetBlankDateIfNull(cwHashTable["Validity_Date"]); 
				nodeResponseBody["Association"].InnerText = GetBlankIfNull(cwHashTable["Assoc_Short_Name"]); 
				nodeResponseBody["NumTerminations"].InnerText = GetBlankIfNull(cwHashTable["No_Of_Terminations"]); 
				*/
				nodeResponseBody.SelectSingleNode("./def:Sender",xnsResponse).InnerText = "IRU" ;
				nodeResponseBody.SelectSingleNode("./def:Originator",xnsResponse).InnerText = tchqRequestLogData.originatorID ;
				//nodeResponseBody.SelectSingleNode("./def:ResponseTime",xnsResponse).InnerText = XmlConvert.ToString(DateTime.UtcNow);

				XmlNode tmpNode1, tmpNode2, tmpNode3, tmpNode4;
				nodeResponseBody.SelectSingleNode("./def:ResponseTime",xnsResponse).InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");

				nodeResponseBody.SelectSingleNode("./def:Result",xnsResponse).InnerText = cwHashTable["Query_Result_Code"].ToString(); 
				nodeResponseBody.SelectSingleNode("./def:Carnet_Number",xnsResponse).InnerText = tchqRequestLogData.tirCarnetNumber ;

				nodeResponseBody.SelectSingleNode("./def:HolderID",xnsResponse).InnerText = GetBlankIfNull(cwHashTable["Holder_ID"]); 
				if(nodeResponseBody.SelectSingleNode("./def:HolderID",xnsResponse).InnerText =="")
				{
					tmpNode1 = nodeResponseBody.SelectSingleNode("./def:HolderID",xnsResponse);
					nodeResponseBody.RemoveChild(tmpNode1); 
				}

				nodeResponseBody.SelectSingleNode("./def:ValidityDate",xnsResponse).InnerText = GetBlankDateIfNull(cwHashTable["Validity_Date"]); 
				if(nodeResponseBody.SelectSingleNode("./def:ValidityDate",xnsResponse).InnerText =="")
				{
					tmpNode1 = nodeResponseBody.SelectSingleNode("./def:ValidityDate",xnsResponse);
					nodeResponseBody.RemoveChild(tmpNode1); 
				}

				nodeResponseBody.SelectSingleNode("./def:Association",xnsResponse).InnerText = GetBlankIfNull(cwHashTable["Assoc_Short_Name"]); 
				if(nodeResponseBody.SelectSingleNode("./def:Association",xnsResponse).InnerText =="")
				{
					tmpNode1 = nodeResponseBody.SelectSingleNode("./def:Association",xnsResponse);
					nodeResponseBody.RemoveChild(tmpNode1); 
				}
				nodeResponseBody.SelectSingleNode("./def:NumTerminations",xnsResponse).InnerText = GetBlankIfNull(cwHashTable["No_Of_Terminations"]); 
				if(nodeResponseBody.SelectSingleNode("./def:NumTerminations",xnsResponse).InnerText =="")
				{
					tmpNode1 = nodeResponseBody.SelectSingleNode("./def:NumTerminations",xnsResponse);
					nodeResponseBody.RemoveChild(tmpNode1); 
				}

				tchqRequestLogData.queryResultCode =Int32.Parse(cwHashTable["Query_Result_Code"].ToString()); 

				tchqRequestLogData.holderID  = GetBlankIfNull(cwHashTable["Holder_ID"]); 
				tchqRequestLogData.validityDate = cwHashTable["Validity_Date"];
				tchqRequestLogData.assocShortName = GetBlankIfNull(cwHashTable["Assoc_Short_Name"]); 
				tchqRequestLogData.numberOfTerminations = cwHashTable["No_Of_Terminations"];



				//			Body Node - Elements
				//			<Sender>IRU</Sender>
				//			<Originator>Originator1</Originator>
				//			<ResponseTime>2004-05-19T14:05:50Z</ResponseTime>
				//			<Result>2</Result>
				//			<Carnet_Number>AB12345678</Carnet_Number>
				//			<HolderID>abcdefghijklmnop</HolderID>
				//			<ValidityDate>2004-05-25</ValidityDate>
				//			<Association>abcdefghijklmnop</Association>
				//			<NumTerminations>0</NumTerminations>




			}
			catch(Exception ex)
			{
				iStep35Result = 2;
				tchqRequestLogData.lastStep = 35; 
				tchqRequestLogData.returnCode = 1200;
				sStep35ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep35Result ==1)
				{
					tchqDbHelper.LogRequestS35(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS35(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 35 , iStep35Result, sStep35ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}

			#endregion

			#region Step 40 - Hash & Encrypt Query Response.

			int iStep40Result = 1;
			string sStep40ResultDesc = "";
			Hashtable htResponse ;
			byte[] outputHash =null; 
			byte[] a3DesSessionKey	= null;
			byte[] aEncResponse  = null;

			
			//replaced by string operation
			//string strResponseBodyContents = nodeResponseBody.InnerXml ;

			
			string sDocwithHash = xdResponse.OuterXml;

			//extract the body content.
/*
			 int iBodyStartR = sDocwithHash.IndexOf("<Body>");
			if(iBodyStartR > 0)
			{
				iBodyStartR += 6;
			}
			else
			{
				throw new ApplicationException("No Body Node found");
			}
			 int iBodyEndR = sDocwithHash.IndexOf("</Body>");
			 int iBodyLengthR = iBodyEndR - iBodyStartR ; 

			string strResponseBodyContents =  sDocwithHash.Substring(iBodyStartR,iBodyLengthR );  
*/			

			string strResponseBodyContents = RegExHelper.ExtractBODYContents(sDocwithHash);

			if (strResponseBodyContents.Trim()=="")
			{
				throw new ApplicationException("No Body Node found");
			}

			try
			{
				#region Hash

				byte[] aBodyContents = System.Text.Encoding.Unicode.GetBytes(strResponseBodyContents);
				htResponse = new Hashtable();
				System.Diagnostics.Debug.WriteLine("Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
				outputHash  = m_iCryptoOperations.Hash(aBodyContents,"SHA1", htResponse);
				System.Diagnostics.Debug.WriteLine(">>Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
				string hashBodyValue = Convert.ToBase64String(outputHash);
				
				//xdResponse.DocumentElement.SelectSingleNode("//def:Hash",xnsResponse).InnerText=hashBodyValue ;
				//string sDocwithHash = xdResponse.OuterXml;
				/*
				if (sDocwithHash.IndexOf("<Hash></Hash>") > 0 )
				{
					sDocwithHash = sDocwithHash.Replace("<Hash></Hash>","<Hash>" + hashBodyValue + "</Hash>");
				}
				else if (sDocwithHash.IndexOf("<Hash />") > 0)
				{
					sDocwithHash = sDocwithHash.Replace("<Hash />","<Hash>" + hashBodyValue + "</Hash>");
				}
				else if (sDocwithHash.IndexOf("<Hash/>") > 0)
				{
					sDocwithHash = sDocwithHash.Replace("<Hash/>","<Hash>" + hashBodyValue + "</Hash>");
				}
				else
				{
					throw new ApplicationException("Hash node not found in response template");
				}
				*/

				string nsPrefix = RegExHelper.ExtractNameSpacePrefix(sDocwithHash,"http://www.iru.org/TCHQResponse");

				sDocwithHash = RegExHelper.SetHASH(sDocwithHash,nsPrefix,hashBodyValue);
				#endregion

				#region - Encrypt Query Response
				
				htResponse = new Hashtable();
				htResponse["IV"]= new byte[] {0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};
				byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
				aEncResponse  = m_iCryptoOperations.Encrypt(aDocWithHash ,"3DES",ref htResponse);
				a3DesSessionKey = (byte[])htResponse["KEY"];

				#endregion

				tchqRequestLogData.decryptedSessionKeyOut = a3DesSessionKey ;
				tchqRequestLogData.responseEncryptionResult = 1;

			}
			catch(Exception ex)
			{
				iStep40Result = 8;
				tchqRequestLogData.responseEncryptionResult = iStep40Result;
				tchqRequestLogData.lastStep = 40; 
				tchqRequestLogData.returnCode = 1200;
				sStep40ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				tchqRequestLogData.responseEncryptionResultDesc = sStep40ResultDesc ;
				continueChecking = false;
			}

			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep40Result ==1)
				{
					tchqDbHelper.LogRequestS40(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS40(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 40 , iStep40Result, sStep40ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}

			#endregion

			#region Step 45 - Encrypt Session Key

			int iStep45Result = 1;
			string sStep45ResultDesc = "";
			string encryptionKeyID="--";
			RSACryptoKey rKey = null;
			//int nRetValue=-1;
	
			try
			{
				dbHelperSubscriber.ConnectToDB();
				iStep45Result =  KeyManager.AssignSubscriberKey(tchqRequestLogData.senderID,out rKey,out encryptionKeyID, dbHelperSubscriber);
				
				if(iStep45Result ==1)
				{
					htResponse = new Hashtable();
					htResponse["EXPONENT"]= rKey.Exponent;
					htResponse["MODULUS"] = rKey.Modulus;
					System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));

					byte[] a3DesEncKey = null; ;
					a3DesEncKey  = m_iCryptoOperations.Encrypt(a3DesSessionKey,"RSA",ref htResponse);

					tchqRequestLogData.sessionKeyEncryptionKeyIDUsed = encryptionKeyID;
					tchqRequestLogData.encryptedSessionKeyOut = a3DesEncKey;
				}
				else
				{
					continueChecking = false;
					tchqRequestLogData.responseEncryptionResult = iStep45Result;
					tchqRequestLogData.lastStep = 45; 
					tchqRequestLogData.returnCode = 1200;
					tchqRequestLogData.responseEncryptionResultDesc = "";
				}
			}
			catch(Exception ex)
			{
				iStep45Result = 8;
				tchqRequestLogData.responseEncryptionResult = iStep45Result;
				tchqRequestLogData.lastStep = 45; 
				tchqRequestLogData.returnCode = 1200;
				sStep45ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				tchqRequestLogData.responseEncryptionResultDesc = sStep45ResultDesc ;
				continueChecking = false;
			}
			finally
			{
				dbHelperSubscriber.Close();
			}

			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				if(iStep45Result ==1)
				{
					tchqDbHelper.LogRequestS45(tchqRequestLogData, false);
				}
				else
				{
					tchqDbHelper.LogRequestS45(tchqRequestLogData, true);
				}
				//Insert step in Sequence table
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 45 , iStep45Result, sStep45ResultDesc ,tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}

			#endregion

			#region Step 99 - Confirm End of step & return result

			try
			{
				tchqRequestLogData.rowCreationTime = DateTime.Now;
				tchqRequestLogData.lastStep = 99;
				tchqRequestLogData.returnCode = 2;
				dbHelperTCHQ.ConnectToDB();	
				dbHelperTCHQ.BeginTransaction();
				//Insert step in Sequence table
				tchqDbHelper.LogRequestS99(tchqRequestLogData);
				tchqDbHelper.LogSequenceStep(tchqRequestLogData.TCHQ_QueryID, 99 , 2, "",tchqRequestLogData.rowCreationTime);  
				dbHelperTCHQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperTCHQ.Close();
			}

			if(!continueChecking )
			{
				TIRHolderResponse tirHolderResponseData = new TIRHolderResponse();
				tirHolderResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderResponseData.MessageTag = "";
				tirHolderResponseData.TIRCarnetHolderResponseParams = null;

				return tirHolderResponseData ;
			}
			else
			{
				TIRHolderResponse tirHolderFinalResponseData = new TIRHolderResponse();

				tirHolderFinalResponseData.Query_ID = tirHolderQueryData.Query_ID ;
				tirHolderFinalResponseData.ReturnCode = tchqRequestLogData.returnCode;
				tirHolderFinalResponseData.MessageTag = encryptionKeyID ;
				tirHolderFinalResponseData.TIRCarnetHolderResponseParams = aEncResponse;
				tirHolderFinalResponseData.ESessionKey = tchqRequestLogData.encryptedSessionKeyOut ;

				return tirHolderFinalResponseData ;
			}

			#endregion

		}

		#region Parse TCHQQueryXMLErrorString 
		private int GetTCHQueryXMLErrorCode(string sValidationResult, out string sNewValidationResult)
		{
			int iTCHQueryInvalidReasonNo = 0;
			string sErrNode ="";
			sNewValidationResult = "";
			if(sValidationResult.Trim().Length >0)
			{
				string sFind = "TCHQuery:";
				int pos1=0, pos2=0,iNoOfchars =0;
				pos1 = sValidationResult.LastIndexOf(sFind);
				if(pos1>=0)
				{
					pos2 = sValidationResult.IndexOf("'",pos1);
					iNoOfchars = pos2-pos1;
					sErrNode = sValidationResult.Substring(pos1+sFind.Length,iNoOfchars-sFind.Length); 
				}
				else
				{
					sNewValidationResult = sValidationResult ;
				}

				iTCHQueryInvalidReasonNo = 1200;
				#region Sample XML
				//			<Query xmlns="http://www.iru.org/TCHQuery">
				//				<Envelope>
				//					<Hash>RABhAHQAYQA=</Hash>
				//				</Envelope>
				//				<Body>
				//				<Sender>FCS</Sender>
				//				<SentTime>2004-05-19T13:54:50Z</SentTime>
				//				<Originator>Originator1</Originator/>
				//				<OriginTime>2004-05-19T13:54:50Z</OriginTime>
				//				<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
				//				<Query_Type>1</Query_Type>
				//				<Query_Reason>1</Query_Reason>
				//				<Carnet_Number>15042217</Carnet_Number>
				//				</Body>
				//			</Query>
				#endregion

				if(sErrNode.Trim().ToUpper() == "ENVELOPE")
				{
					iTCHQueryInvalidReasonNo = 1200;
				}
				else if(sErrNode.Trim().ToUpper() == "SENDER")
				{
					iTCHQueryInvalidReasonNo = 1212;
				}
				else if(sErrNode.Trim().ToUpper() == "SENTTIME")
				{
					iTCHQueryInvalidReasonNo =1234;
				}
				else if(sErrNode.Trim().ToUpper() == "ORIGINATOR")
				{
					iTCHQueryInvalidReasonNo = 1236;
				}
				else if(sErrNode.Trim().ToUpper() == "ORIGINTIME")
				{
					iTCHQueryInvalidReasonNo = 1237;
				}
				else if(sErrNode.Trim().ToUpper() == "PASSWORD")
				{
					iTCHQueryInvalidReasonNo = 1200;
				}
				else if(sErrNode.Trim().ToUpper() == "QUERY_TYPE")
				{
					iTCHQueryInvalidReasonNo = 1239;
				}
				else if(sErrNode.Trim().ToUpper() == "QUERY_REASON")
				{
					iTCHQueryInvalidReasonNo = 1240;
				}
				else if(sErrNode.Trim().ToUpper() == "CARNET_NUMBER")
				{
					iTCHQueryInvalidReasonNo  =1241;
				}

				if(pos1>=0)
				{
					sNewValidationResult = sValidationResult + " (Node:" + sErrNode + " - ErrorCode:"+iTCHQueryInvalidReasonNo.ToString().Trim() + ")" ;
				}
			}
			return iTCHQueryInvalidReasonNo ;
		}
		#endregion

	}
}