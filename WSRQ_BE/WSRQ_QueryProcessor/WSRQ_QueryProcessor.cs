using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IRU.RTS;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS.Crypto;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IRU.RTS.WSWSRQ
{
    /// <summary> 
    /// Lata Created on September 18 ,2007 for r.econciliation request
    /// <summary> 
    public class WSRQ_QueryProcessor:MarshalByRefObject, IWSRQProcessor
    {
        private ICryptoOperations m_iCryptoOperations;
        private string m_sWSRQ_Query_Id;
        private IPlugInManager m;

        public WSRQ_QueryProcessor()
		{
			m_sWSRQ_Query_Id = "EXCHANGE_ID";
        }

        [System.Runtime.Remoting.Messaging.OneWay]
        public void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult)
        {
            #region Get IDBHelper instances from Plugin Manager
            IDBHelper dbHelperWSRQ = WSRQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("WSRQDB") ;//  null; //dbhelper for tchqdb
            WSRQ_DBHelper wsrqDbHelper = new WSRQ_DBHelper(dbHelperWSRQ); 
            #endregion

            try
            {
                dbHelperWSRQ.ConnectToDB();	
                wsrqDbHelper.LogRequestResponse(QueryId, dtResponseSent, ResponseResult);
            }
            catch(Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
            }
            finally
            {
                dbHelperWSRQ.Close();
            }
			

        }
        
		

        public ReconciliationResponse ProcessQuery(ReconciliationQuery reconciliationQuery, string SenderIP, out long IRUQueryId)
        {
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, Environment.NewLine + "1.)--------------- Calling processQuery-------------");
            #region Initialize / create Variables
            bool continueChecking = true;
            WSRQLogQueryData wsrqLogQueryData = new WSRQLogQueryData();
            IDBHelper dbHelperSubscriber = null;
            IDBHelper dbHelperWSRQ = null;
            WSRQ_DBHelper WSRQDbHelper = null;
            Subscriber_DBHelper subsDbHelper = null;
            ICryptoOperations m_iCryptoOperations = null;
            SubscriberDetailsStruct subscriberDetails = null;
            RSACryptoKey sessionDecrKey = null;
            long newID = 0;
            IRUQueryId = 0;
            string sICC = "";
            int keyStatus = 0, xint = 0;
            DateTime sentdate = new DateTime(0, DateTimeKind.Utc);
            #endregion
            #region Get IDBHelper instances from Plugin Manager
            try
            {
                dbHelperSubscriber = WSRQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB");//  null; //dbhelper for WSRQdb
                dbHelperWSRQ = WSRQ_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("WSRQDB");//  null; //dbhelper for WSRQdb
                #region a. Generate New ID
                newID = IDHelper.GenerateID("EXCHANGE_ID", dbHelperWSRQ);
                IRUQueryId = newID;
                #endregion
                #region Initialize / create Variables
                WSRQDbHelper = new WSRQ_DBHelper(dbHelperWSRQ);
                subsDbHelper = new Subscriber_DBHelper(dbHelperSubscriber);
                m_iCryptoOperations = (ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations), WSRQ_RemotingHelper.m_CryptoProviderEndpoint);
                subscriberDetails = new SubscriberDetailsStruct();
                #endregion

                #region Populate wsrqLogQueryData with available data as on step 5
                //b. Insert a row in WSRQ_REQUEST_LOG
                wsrqLogQueryData.Exchange_ID = newID;
                wsrqLogQueryData.decryptionKeyID = reconciliationQuery.MessageTag;
                wsrqLogQueryData.senderID = reconciliationQuery.SubscriberID;
                wsrqLogQueryData.encryptedQueryParams = reconciliationQuery.ReconciliationQueryData;
                wsrqLogQueryData.encryptedSessionKeyIn = reconciliationQuery.ESessionKey;
                wsrqLogQueryData.senderTCPAddress = SenderIP;
                wsrqLogQueryData.rowCreationTime = DateTime.Now;
                wsrqLogQueryData.Sender_MessageID = reconciliationQuery.Sender_MessageID;
                wsrqLogQueryData.returnCode = 2;//2:OK
                wsrqLogQueryData.Information_Exchange_Version = reconciliationQuery.Information_Exchange_Version;
                #endregion


            }
            catch (SqlException sqEx)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, sqEx.Message + " - " + sqEx.StackTrace + " - " + sqEx.ErrorCode.ToString());
                wsrqLogQueryData.returnCode = 1200;
                wsrqLogQueryData.lastStep = 0;
                continueChecking = false;
                ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                ReconciliationResponseData.MessageTag = "";
                ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;

                return ReconciliationResponseData;


            }
            catch (Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                wsrqLogQueryData.returnCode = 1200;
                wsrqLogQueryData.lastStep = 0;
                continueChecking = false;
            }
            #endregion

            #region Step 5 - Initial Log Request & Validate Params
            if (continueChecking)
            {
                if (reconciliationQuery.Information_Exchange_Version == null || reconciliationQuery.Information_Exchange_Version != "1.0.0")
                {
                    //1223:Version not valid
                    wsrqLogQueryData.returnCode = 1223;
                    wsrqLogQueryData.lastStep = 5;
                    continueChecking = false;
                }
            }
                
            //Message Tag validation   
            if (continueChecking)
             {
                    if (reconciliationQuery.MessageTag == null || reconciliationQuery.MessageTag.Trim() == "")
                    {
                        wsrqLogQueryData.returnCode = 1210;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                    }
             }
             //length of Sender_MessageID
             if (continueChecking)
             {
                 if (reconciliationQuery.Sender_MessageID.Trim().Length > 255)
                 {
                     wsrqLogQueryData.returnCode = 1232;
                     wsrqLogQueryData.lastStep = 5;
                     continueChecking = false;
                 }
             }

                if (continueChecking)
                {
                    //Get Keys for Decryption using MessageTag
                    try
                    {
                        dbHelperSubscriber.ConnectToDB();
                        keyStatus = KeyManager.GetIRUKeyDetails(reconciliationQuery.MessageTag, reconciliationQuery.SubscriberID, out sessionDecrKey, dbHelperSubscriber);
                    }
                    finally
                    {
                        dbHelperSubscriber.Close();
                    }
                    //1211:No Certificate found corresponding to the MessageTag
                    if (keyStatus == 3)
                    {
                        wsrqLogQueryData.returnCode = 1211;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "KeyStatus for Subscriber " + reconciliationQuery.SubscriberID + " - " + reconciliationQuery.MessageTag + " = " + keyStatus.ToString());
                        //TODO:Update TCHQLOG & Sequence with Key Status 1200
                        //return new TIRHolderResponse(); // return to FCS the respose struct
                    }
                    else if (keyStatus == 9)
                    {
                        wsrqLogQueryData.returnCode = 1211;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                    }

                }
                if (continueChecking)
                {
                    //1212 - Subscriber ID Missing or Bad formatted or not registered
                    if ((reconciliationQuery.SubscriberID == null || reconciliationQuery.SubscriberID.Trim() == ""))
                    {
                        wsrqLogQueryData.returnCode = 1212;
                        wsrqLogQueryData.senderAuthenticated = 1212;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                        

                    }
                    //Max length
                    else if (reconciliationQuery.SubscriberID.Trim().Length > 255)
                    {
                        wsrqLogQueryData.returnCode = 1212;
                        wsrqLogQueryData.senderAuthenticated = 1212;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                    }

                }
                if (continueChecking)
                {
                    subscriberDetails.subscriberID = reconciliationQuery.SubscriberID;
                    int iAuthenticateQuerySender = 0;
                    try
                    {
                        dbHelperSubscriber.ConnectToDB();
                        iAuthenticateQuerySender = subsDbHelper.AuthenticateQuerySender(subscriberDetails.subscriberID,
                            out subscriberDetails.password, "WSRQ", 1, out subscriberDetails.SessionKeyAlgo,
                            out subscriberDetails.HashAlgo, out subscriberDetails.CopyToId, out subscriberDetails.CopyToAddress, out sICC);
                    }
                    finally
                    {
                        dbHelperSubscriber.Close();
                    }
                    //Also it is checked here for Invalid Method instead of Step 30
                    if (iAuthenticateQuerySender != 0)
                    {
                        //iAuthenticateQuerySender = 1233; Invalid Service of Service Not Active
                        //iAuthenticateQuerySender = 1212;Service Method  for Subcriber is Not Active or it in Invalid or SubScriberID is not registered.
                        wsrqLogQueryData.returnCode = iAuthenticateQuerySender;
                        wsrqLogQueryData.senderAuthenticated = iAuthenticateQuerySender;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                        
                    }
                }

                if (continueChecking)
                { 
                    //1213:SessionKey Missing
                    if (reconciliationQuery.ESessionKey == null)
                    {
                        wsrqLogQueryData.returnCode = 1213;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                    }
                    else
                    {
                        if (reconciliationQuery.ESessionKey.Length == 0)
                        {
                            wsrqLogQueryData.returnCode = 1213;
                            wsrqLogQueryData.lastStep = 5;
                            continueChecking = false;
                        }
                    }
                }

                if (continueChecking)
                {
                    //1214:Encrypted Data Missing
                    if (reconciliationQuery.ReconciliationQueryData == null)
                    {
                        wsrqLogQueryData.returnCode = 1214;
                        wsrqLogQueryData.lastStep = 5;
                        continueChecking = false;
                    }
                    else
                    {
                        if (reconciliationQuery.ReconciliationQueryData.Length == 0)
                        {
                            wsrqLogQueryData.returnCode = 1214;
                            wsrqLogQueryData.lastStep = 5;
                            continueChecking = false;


                        }
                    }
                }

                try
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "2.)---------------Step5------------");

                    wsrqLogQueryData.rowCreationTime = DateTime.Now;
                    dbHelperWSRQ.ConnectToDB();
                    dbHelperWSRQ.BeginTransaction();
                    if (continueChecking)
                    {
                        WSRQDbHelper.LogRequestS5(wsrqLogQueryData, false);
                        WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 5, wsrqLogQueryData.returnCode, "", wsrqLogQueryData.rowCreationTime);
                    }
                    else
                    {
                        WSRQDbHelper.LogRequestS5(wsrqLogQueryData, true);
                        WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 5, wsrqLogQueryData.returnCode, "", wsrqLogQueryData.rowCreationTime);
                    }
                    dbHelperWSRQ.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                    continueChecking = false;
                }
                finally
                {
                    dbHelperWSRQ.Close();
                }
                if (!continueChecking)
                {
                    ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                    ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                    ReconciliationResponseData.MessageTag = "";
                    ReconciliationResponseData.ReconciliationRequestData = null;
                    ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                    ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
                    return ReconciliationResponseData;
                }
         #endregion

            #region  Step 10 - Session Key Decryption
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "3.)---------------Step10------------");

                ///In this section we do not have to check for 3 or 9 as if it was 3 or 9 it would not reach here.
                ///it will fail in sep 5 itself.
                int iStep10Result = 1;
                string iStep10ResultDesc = "";
                byte[] decrSessionKeyIn = null;

                if (keyStatus != 7)
                {
                    Hashtable hashForSessionKey = new Hashtable();
                    hashForSessionKey["MODULUS"] = sessionDecrKey.Modulus;
                    hashForSessionKey["EXPONENT"] = sessionDecrKey.Exponent;
                    hashForSessionKey["D"] = sessionDecrKey.D;
                    hashForSessionKey["P"] = sessionDecrKey.P;
                    hashForSessionKey["Q"] = sessionDecrKey.Q;
                    hashForSessionKey["DP"] = sessionDecrKey.DP;
                    hashForSessionKey["DQ"] = sessionDecrKey.DQ;
                    hashForSessionKey["INVERSEQ"] = sessionDecrKey.INVERSEQ;
                    wsrqLogQueryData.decryptedSessionKeyIn = null;

                    try
                    {
                        decrSessionKeyIn = m_iCryptoOperations.Decrypt(reconciliationQuery.ESessionKey, subscriberDetails.SessionKeyAlgo, hashForSessionKey);
                        wsrqLogQueryData.decryptedSessionKeyIn = decrSessionKeyIn;
                    }
                    catch (Exception ex)
                    {
                        //Decryption Failed: Exception is thrown while decrypting.
                        iStep10Result = 8;
                        wsrqLogQueryData.returnCode = 1230;
                        wsrqLogQueryData.lastStep = 10;

                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

                        iStep10ResultDesc = ex.Message + " -> " + ex.StackTrace;
                        continueChecking = false;
                    }
                }
                else
                {
                    //1230:Cannot Decrypt the encrypted session key
                    //7:SubScriberIDs do not match
                    iStep10Result = 7;
                    wsrqLogQueryData.returnCode = 1230;
                    wsrqLogQueryData.lastStep = 10;
                    continueChecking = false;
                }

                try
                {
                    wsrqLogQueryData.rowCreationTime = DateTime.Now;
                    dbHelperWSRQ.ConnectToDB();
                    dbHelperWSRQ.BeginTransaction();
                    if (iStep10Result == 1)
                    {
                        WSRQDbHelper.LogRequestS10(wsrqLogQueryData, false);
                    }
                    else
                    {
                        WSRQDbHelper.LogRequestS10(wsrqLogQueryData, true);
                    }
                    //Insert step in Sequence table
                    WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 10, iStep10Result, iStep10ResultDesc, wsrqLogQueryData.rowCreationTime);
                    dbHelperWSRQ.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                    continueChecking = false;
                }
                finally
                {
                    dbHelperWSRQ.Close();
                }

                if (!continueChecking)
                {
                    ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                    ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                    ReconciliationResponseData.MessageTag = "";
                    ReconciliationResponseData.ReconciliationRequestData = null;
                    ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                    ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;

                    return ReconciliationResponseData;
                }
                #endregion

            #region Step 15 - do Decryption & Get Decrypted Data.
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "4.)---------------Step15------------");

			int iStep15Result = 1;
			string sStep15ResultDesc = "";
			//This is to be used 
			Hashtable hashDecryptParams = new Hashtable();
			
			byte[] byIV = new byte[]{0x03,0x01,0x04,0x01,0x05,0x09,0x02,0x06};

			hashDecryptParams["KEY"] = decrSessionKeyIn;
			hashDecryptParams["IV"] = byIV;

			try
            {
				byte [] baDecryptedQueryParamXML = 	m_iCryptoOperations.Decrypt(wsrqLogQueryData.encryptedQueryParams,"3DES",hashDecryptParams);  
                iStep15Result = 1;//1:OK
                wsrqLogQueryData.decryptedQueryParamXML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);
				wsrqLogQueryData.decryptionResult = 1;
                wsrqLogQueryData.rowCreationTime = DateTime.Now;
                XmlDocument decryptedxml = new XmlDocument();
                decryptedxml.LoadXml(wsrqLogQueryData.decryptedQueryParamXML.ToString());
                XmlNamespaceManager xns1 = new XmlNamespaceManager(decryptedxml.NameTable);
                xns1.AddNamespace("def", "http://www.iru.org/SafeTIRReconciliation");
                XmlNode querynode = decryptedxml.DocumentElement.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:QueryType", xns1);
                if (querynode != null)
                {
                    int.TryParse(querynode.InnerText.ToString(), out xint);
                    wsrqLogQueryData.queryType = xint;
                }
                //1239:Invalid Query Type
                if (xint != 1)
                {
                    wsrqLogQueryData.returnCode = 1239;
                    wsrqLogQueryData.lastStep = 15;
                    continueChecking = false;
                    iStep15Result = 1239;//ask
                }
                querynode = decryptedxml.DocumentElement.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:Password", xns1);
                if (querynode != null)
                {
                    wsrqLogQueryData.senderPassword = querynode.InnerText.ToString();
                }
                querynode = decryptedxml.DocumentElement.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:SentTime", xns1);
                if (querynode != null)
                {
                    DateTime.TryParse(querynode.InnerText.ToString(), out sentdate);
                    wsrqLogQueryData.originTime = sentdate;
                }
                if (continueChecking)
                {
                    //1234:Invalid Sent Time
                    if (!(DateTime.TryParse(wsrqLogQueryData.originTime.ToString(), out sentdate)))
                    {
                        wsrqLogQueryData.returnCode = 1234;
                        wsrqLogQueryData.lastStep = 15;
                        continueChecking = false;
                        iStep15Result = 1234;//ask
                    }
                }
                wsrqLogQueryData.decryptionResultDesc = sStep15ResultDesc;       

   			}
			catch(Exception ex)
			{
                //1231:Error Decrypting the encrypted ReconciliationQueryData
				iStep15Result = 8;
				wsrqLogQueryData.returnCode = 1231;
				wsrqLogQueryData.lastStep = 15; 
					
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				sStep15ResultDesc = ex.Message + " -> " + ex.StackTrace ;

				wsrqLogQueryData.decryptedQueryParamXML = null;
				wsrqLogQueryData.decryptionResult = 8;
				continueChecking = false;
                wsrqLogQueryData.decryptionResultDesc = sStep15ResultDesc;       
			}
            
         	try
			{
				wsrqLogQueryData.rowCreationTime = DateTime.Now;
				dbHelperWSRQ.ConnectToDB();	
				dbHelperWSRQ.BeginTransaction();
				if(iStep15Result ==1)
				{
                    WSRQDbHelper.LogRequestS15(wsrqLogQueryData, false);
				}
				else
				{
                   WSRQDbHelper.LogRequestS15(wsrqLogQueryData, true);
				}
				//Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 15, iStep15Result, sStep15ResultDesc, wsrqLogQueryData.rowCreationTime);  
				dbHelperWSRQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperWSRQ.Close();
			}

			if(!continueChecking )
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
				ReconciliationResponseData.MessageTag = "";
				ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
                return ReconciliationResponseData ;
			}


			#endregion

            #region Step 20 - do Validate Hash
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "5.)---------------Step20------------");

			int iStep20Result = 1;
			string sStep20ResultDesc = "";

			try
			{				
				string sHash = RegExHelper.ExtractHASH(wsrqLogQueryData.decryptedQueryParamXML);//.Substring(iHashStart,iHashLength);  

				if (sHash.Trim()=="")
				{
                    wsrqLogQueryData.returnCode = 1200;
                    wsrqLogQueryData.lastStep = 20;
                    iStep20Result = 8;//no hash found
                    sStep20ResultDesc = "Hash not found";
                    continueChecking = false;
					//throw new ApplicationException("No Hash found");
				}
				byte [] baHash = Convert.FromBase64String(sHash);
                string sBody = RegExHelper.ExtractBODYContents (wsrqLogQueryData.decryptedQueryParamXML);//.Substring(iBodyStart,iBodyLength );  
				
				if (sBody.Trim()=="")
				{
                    wsrqLogQueryData.returnCode = 1200;
                    wsrqLogQueryData.lastStep = 20;
                    iStep20Result = 8;//no body found
                    sStep20ResultDesc = "Body not found";
                    continueChecking = false;
					//throw new ApplicationException("No Body Node found");
				}
                if (continueChecking)
                {
                    Regex r = new Regex(@"\s+");
                    sBody = r.Replace(sBody.Trim(), " ");
                    byte[] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);

                    if (!m_iCryptoOperations.VerifyHash(baBody, subscriberDetails.HashAlgo, null, baHash))
                    {
                        //1200:Any unclassified error.
                        wsrqLogQueryData.returnCode = 1200;
                        wsrqLogQueryData.lastStep = 20;
                        iStep20Result = 7;//Hash Mismatch
                        sStep20ResultDesc = "Hash Verification Failed";
                        continueChecking = false;
                    }
                }
                
			}
			catch(Exception ex)
			{
                //1200:Any unclassified error.
				wsrqLogQueryData.returnCode = 1200;
				wsrqLogQueryData.lastStep = 20; 
				iStep20Result = 8;//no body or hash node found
				sStep20ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  

				continueChecking = false;
			}


			try
			{
				wsrqLogQueryData.rowCreationTime = DateTime.Now;
				dbHelperWSRQ.ConnectToDB();	
				dbHelperWSRQ.BeginTransaction();
				if(iStep20Result ==1)
				{
                    WSRQDbHelper.LogRequestS20(wsrqLogQueryData, false);
				}
				else
				{
                    WSRQDbHelper.LogRequestS20(wsrqLogQueryData, true);
				}
				//Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 20, iStep20Result, sStep20ResultDesc, wsrqLogQueryData.rowCreationTime);  
				dbHelperWSRQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperWSRQ.Close();
			}

			if(!continueChecking )
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
				ReconciliationResponseData.MessageTag = "";
				ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
                return ReconciliationResponseData ;
			}

			#endregion

            #region Step 25 - Validate against request XSD
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "6.)---------------Step25------------");
            int iStep25Result = 1;//ok
			string sStep25ResultDesc = "";
            wsrqLogQueryData.validQueryXML = true;
			try
			{
              
                XMLValidationHelper.PopulateSchemas("http://www.iru.org/SafeTIRReconciliation", WSRQ_RemotingHelper.m_SchemaFilesPath + "WSRQueryData.xsd");
				XMLValidationHelper xvh = new XMLValidationHelper();
				if(!xvh.ValidateXML(wsrqLogQueryData.decryptedQueryParamXML, out wsrqLogQueryData.invalidQueryXMLReason))
				{
                    iStep25Result = GetWSRQueryXMLErrorCode(wsrqLogQueryData.invalidQueryXMLReason,out wsrqLogQueryData.invalidQueryXMLReason);
                    wsrqLogQueryData.validQueryXML = false;
                  throw new ApplicationException(wsrqLogQueryData.invalidQueryXMLReason);
				}
			}
			catch(Exception ex)
			{
				wsrqLogQueryData.returnCode = iStep25Result;
                wsrqLogQueryData.validQueryXML = false;
				wsrqLogQueryData.lastStep = 25; 
				sStep25ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
                continueChecking = false;
			}
			try
			{
				wsrqLogQueryData.rowCreationTime = DateTime.Now;
				dbHelperWSRQ.ConnectToDB();	
				dbHelperWSRQ.BeginTransaction();
				if(iStep25Result ==1)
				{
                   WSRQDbHelper.LogRequestS25(wsrqLogQueryData, false);
				}
				else
				{
                   WSRQDbHelper.LogRequestS25(wsrqLogQueryData, true);
				}
				//Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 25, iStep25Result, sStep25ResultDesc, wsrqLogQueryData.rowCreationTime);  
				dbHelperWSRQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperWSRQ.Close();
			}

			if(!continueChecking )
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
				ReconciliationResponseData.MessageTag = "";
				ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
				return ReconciliationResponseData;
			}
			
			
			#endregion


            #region Step 30 - Authorize User
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "7.)---------------Step30------------");
			int iStep30Result = 1;
			string sStep30ResultDesc = "";
			try
			{
			//	XmlDocument xd = new XmlDocument();
			//	xd.LoadXml(wsrqLogQueryData.decryptedQueryParamXML);

			//	XmlNamespaceManager xns = new XmlNamespaceManager( xd.NameTable);
			//	xns.AddNamespace("def","http://www.iru.org/WSRQuery");
             //   XmlNode node = xd.DocumentElement.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:Password",xns);
			//	if(node == null)
				//{
					if(wsrqLogQueryData.senderPassword == null  || wsrqLogQueryData.senderPassword == "")
                    {
					    if(subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
					    {
						    //Password is valid - there is no password or password node also might not be present
					    }
					    else
                        {	//1233	Password	Authorization Failure
						    wsrqLogQueryData.returnCode =  1233;
						    iStep30Result = 3;
						    sStep30ResultDesc = "Password does not match";
						    throw new ApplicationException("Password Verification Failed");
					    }
                    }
				    else
				    {
                        string password = wsrqLogQueryData.senderPassword;
					    if(subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
					    {
						    if(password.Trim() == "")
						    {

							    //PAssword is valid - there is no password or password node also might no be present
						    }
						    else
						    {
                                wsrqLogQueryData.returnCode =  1233;
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
						    wsrqLogQueryData.returnCode =  1233;
						    iStep30Result = 3;
						    sStep30ResultDesc = "Password does not match";
						    throw new ApplicationException("Password Verification Failed");
					    }
				    }
                
                
            }
			catch(ApplicationException ex)
			{	
				wsrqLogQueryData.lastStep = 30; 
				continueChecking = false;
                sStep30ResultDesc = ex.Message + " - " + ex.StackTrace;
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
                continueChecking = false;
			}
			catch(Exception ex)
			{
				if(iStep30Result  == 1)
				{
					wsrqLogQueryData.returnCode =  1200;
					iStep30Result =  1200;
				}
				wsrqLogQueryData.lastStep = 30; 
				sStep30ResultDesc = ex.Message + " - " + ex.StackTrace ;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			try
			{
				wsrqLogQueryData.rowCreationTime = DateTime.Now;
				dbHelperWSRQ.ConnectToDB();	
				dbHelperWSRQ.BeginTransaction();
				if(iStep30Result ==1)
				{
                    WSRQDbHelper.LogRequestS30(wsrqLogQueryData, false);
				}
				else
				{
                    WSRQDbHelper.LogRequestS30(wsrqLogQueryData, true);
				}
				//Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 30, iStep30Result, sStep30ResultDesc, wsrqLogQueryData.rowCreationTime);  
				dbHelperWSRQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperWSRQ.Close();
			}

			if(!continueChecking )
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
				ReconciliationResponseData.MessageTag = "";
				ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;

				return ReconciliationResponseData ;
			}
			#endregion


            #region Step 35 - Get Request Details for ReconciliationRequest
            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, "8.)---------------Step35------------");
            #region Variables
                int iStep35Result = 1;
                string sStep35ResultDesc = "";
                DataSet WSRQDS = new DataSet();
           
                XmlNode nodeResponseBody = null ;
                long lcnt;
                byte[] xmlarray=null;
                int eid, icnt = 0 ;
                string year, mon, day;
                eid = 0;
                XmlDocument xd1 = new XmlDocument();
            #endregion
                
                try
                {
                    #region Load the reconciliationrequest template 
                    
                        XmlDocument xd = new XmlDocument();
                        WSRQLogRequestStruct WLRS1 =  new WSRQLogRequestStruct();
                        xd=WSRQ_RemotingHelper.m_InMemoryCachePlugin.GetXMLDomFromCache("WSRQReconciliationDataTemplate");
                        xd1 = xd;
                        //FileStream fs = new FileStream(WSRQ_RemotingHelper.m_InMemoryCachePlugin.GetXMLDomFromCache("WSRQReconciliationDataTemplate.xml"), FileMode.Open, FileAccess.Read);
                        //xmlarray = new byte[fs.Length];
                        //fs.Read(xmlarray, 0, xmlarray.Length);
                        //fs.Close();
                        //string xmlstring = System.Text.Encoding.Unicode.GetString(xmlarray);
                        //xmlstring = xmlstring.Trim();
                        //xd.LoadXml(xmlstring);  
                        XmlNamespaceManager xns = new XmlNamespaceManager(xd.NameTable);
                        xns.AddNamespace("def", "http://www.iru.org/SafeTIRReconciliation");
                    #endregion

                    #region Query WSRQ_Detail to fetch all requests where exchangeID=0
                        WSRQDS = WSRQDbHelper.GetReconciliationRequestData(sICC);
                    #endregion

                    #region fill request data
                        long.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows.Count.ToString(),out lcnt);
                        int.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows.Count.ToString(),out icnt);
                        wsrqLogQueryData.No_Of_Requests_Sent = icnt;
                        WSRQLogRequestRecordsStruct[] WLRRS1 = new WSRQLogRequestRecordsStruct[lcnt]; 
                        WSRQXMLRequestRecordsStruct[] WXRRS1 = new WSRQXMLRequestRecordsStruct[lcnt];
                        WLRS1.NumberOfRecords = lcnt;
                        WLRS1.RequestRecords = WLRRS1;

                        for (int hcnt = 0; hcnt < WLRS1.NumberOfRecords; hcnt++)
                        {
                            WLRRS1[hcnt] = new WSRQLogRequestRecordsStruct();
                            WXRRS1[hcnt] = new WSRQXMLRequestRecordsStruct();
                            WLRRS1[hcnt].RequestId = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestID"].ToString();
                            DateTime reqdateresult = new DateTime(0,DateTimeKind.Utc);
                            DateTime.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestDate"].ToString(),out reqdateresult);
                            WLRRS1[hcnt].RequestDate        = reqdateresult;

                            int reqdatesource = 0;
                            int.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestDate"].ToString(), out reqdatesource);
                            WLRRS1[hcnt].RequestDataSource  = reqdatesource;

                            WLRRS1[hcnt].RequestRemark      = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestRemark"].ToString();

                            int reqremindernum = 0;
                            int.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestReminderNum"].ToString(), out reqremindernum);
                            WLRRS1[hcnt].RequestReminderNum = reqremindernum;

                            int reqDataSource = 0;
                            int.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RequestDataSource"].ToString(), out reqDataSource);
                            WLRRS1[hcnt].RequestDataSource = reqDataSource;

                            WLRRS1[hcnt].TNO  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["TNO"].ToString();
                            WLRRS1[hcnt].VPN  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["VPN"].ToString();
                            WLRRS1[hcnt].RND  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RND"].ToString();
                            WLRRS1[hcnt].RBC  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["RBC"].ToString();

                            int rPIC = 0;
                            int.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["PIC"].ToString(), out rPIC);
                            WLRRS1[hcnt].PIC = rPIC;
                                              
                            WLRRS1[hcnt].PFD  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["PFD"].ToString();
                            WLRRS1[hcnt].ICC  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["ICC"].ToString();

                            DateTime DDIresult = new DateTime(0, DateTimeKind.Utc);
                            DateTime.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["DDI"].ToString(), out DDIresult);
                            if (DDIresult.ToString() != "01/01/0001 12:00:00 AM" && DDIresult.ToString()!=null)
                                WLRRS1[hcnt].DDI = DDIresult.ToString();
                            else
                                WLRRS1[hcnt].DDI = "";

                            DateTime DCLresult = new DateTime(0, DateTimeKind.Utc);
                            DateTime.TryParse(WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["DCL"].ToString(), out DCLresult);
                            if(DCLresult.ToString() != "01/01/0001 12:00:00 AM" && DCLresult.ToString()!=null)
                                WLRRS1[hcnt].DCL  = DCLresult.ToString();
                            else
                                WLRRS1[hcnt].DCL = "";
                            WLRRS1[hcnt].CWR  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["CWR"].ToString();
                            WLRRS1[hcnt].COM  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["COM"].ToString();
                            WLRRS1[hcnt].COF  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["COF"].ToString();
                            WLRRS1[hcnt].CNL  = WSRQDS.Tables["WSRQ_DETAIL"].Rows[hcnt]["CNL"].ToString();
                           
                         }
                    #endregion

                    #region Update WSRQ_Detail with exchangeID
                        try
                        {
                            int.TryParse(wsrqLogQueryData.Exchange_ID.ToString(), out eid);
                            dbHelperWSRQ.ConnectToDB();
                            for (int hcnt = 0; hcnt < WLRS1.NumberOfRecords; hcnt++)
                            {
                                string RequestID = WLRRS1[hcnt].RequestId.ToString();
                                

                                dbHelperWSRQ.BeginTransaction();
                                WSRQDbHelper.UpdateWSRQDetails(RequestID, eid, sICC);
                                dbHelperWSRQ.CommitTransaction();
                            }
                        }
                        catch (Exception ex)
                        {
                            Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                            continueChecking = false;
                        }
                        finally
                        {
                            dbHelperWSRQ.Close();
                        }
                    #endregion

                    #region creation of xml string for Reconciliation requestdata
                        //nodeResponseBody = xd.DocumentElement.SelectSingleNode("/def:SafeTIRReconciliation/def:Body",xns);
                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, xd.InnerXml);
                        nodeResponseBody = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body", xns);
                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, nodeResponseBody.InnerXml);
                        XmlNode nodeNumberofRequestRecs = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:NumberOfRecords", xns);
                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo, nodeNumberofRequestRecs.InnerText);
                        nodeNumberofRequestRecs.InnerText = WLRS1.NumberOfRecords.ToString();
                        //nodeResponseBody.SelectSingleNode("NumberOfRecords", xns).InnerText = WLRS1.NumberOfRecords.ToString();
                        //nodeResponseBody = xd.DocumentElement.SelectSingleNode("/def:SafeTIRReconciliation/def:Body/def:RequestRecords", xns);
                        nodeResponseBody = xd.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:RequestRecords", xns);
                        
                       
                            for (int nodeattrcnt = 0; nodeattrcnt < WLRRS1.Length; nodeattrcnt++)
                            {

                                XmlElement childElement = xd.CreateElement("RequestRecord");

                                XmlAttribute attr = xd.CreateAttribute("RequestID",null);
                                attr.Value = WLRRS1[nodeattrcnt].RequestId.ToString();
                                childElement.Attributes.Append(attr);
                           
                                attr = xd.CreateAttribute("RequestDate",null);
                                DateTime d1 = new DateTime(0,DateTimeKind.Utc);
                                DateTime.TryParse(WLRRS1[nodeattrcnt].RequestDate.ToString(), out d1);
                                if (d1.Month.ToString().Length == 1)
                                    mon = "0" + d1.Month.ToString();
                                else
                                    mon = d1.Month.ToString();
                                if (d1.Day.ToString().Length == 1)
                                    day = "0" + d1.Day.ToString();
                                else
                                    day = d1.Day.ToString();
                                year = d1.Year.ToString();
                                attr.Value = year + "-" + mon + "-" + day;

                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("RequestDataSource", null);
                                attr.Value = WLRRS1[nodeattrcnt].RequestDataSource.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("RequestReminderNum", null);
                                attr.Value = WLRRS1[nodeattrcnt].RequestReminderNum.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("TNO", null);
                                attr.Value = WLRRS1[nodeattrcnt].TNO.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("ICC", null);
                                attr.Value = WLRRS1[nodeattrcnt].ICC.ToString();
                                childElement.Attributes.Append(attr);

                                //attr = xd.CreateAttribute("DCL", null);
                                DateTime d2= new DateTime();
                                DateTime.TryParse(WLRRS1[nodeattrcnt].DCL.ToString(), out d2);

                                if (d2.Month.ToString().Length == 1)
                                    mon = "0" + d2.Month.ToString();
                                else
                                    mon = d2.Month.ToString();
                                if (d2.Day.ToString().Length == 1)
                                    day = "0" + d2.Day.ToString();
                                else
                                    day = d2.Day.ToString();
                                year = d2.Year.ToString();

                                string sDCLValueTemp = year + "-" + mon + "-" + day;
                                // CR from Glen 2008-11-22
                                //attr.Value = year + "-" + mon + "-" + day;
                                //if (attr.Value == "1-01-01")
                                //attr.Value = "";
                                if (sDCLValueTemp == "1-01-01")
                                {
                                }
                                else
                                {
                                    attr = xd.CreateAttribute("DCL", null);
                                    attr.Value = sDCLValueTemp;
                                    childElement.Attributes.Append(attr);
                                }
                                attr = xd.CreateAttribute("CNL");
                                attr.Value = WLRRS1[nodeattrcnt].CNL.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("COF", null);
                                attr.Value = WLRRS1[nodeattrcnt].COF.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("DDI", null);
                                DateTime d3 = new DateTime();
                                DateTime.TryParse(WLRRS1[nodeattrcnt].DDI.ToString(), out d3);
                                if (d3.Month.ToString().Length == 1)
                                    mon = "0" + d3.Month.ToString();
                                else
                                    mon = d3.Month.ToString();
                                if (d3.Day.ToString().Length == 1)
                                    day = "0" + d3.Day.ToString();
                                else
                                    day = d3.Day.ToString();
                                year = d3.Year.ToString();
                                attr.Value = year + "-" + mon + "-" + day;
                                if (attr.Value == "1-01-01")
                                    attr.Value = "";
                                childElement.Attributes.Append(attr);
                                
                                attr = xd.CreateAttribute("RND", null);
                                attr.Value = WLRRS1[nodeattrcnt].RND.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("PFD", null);
                                attr.Value = WLRRS1[nodeattrcnt].PFD.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("CWR", null);
                                attr.Value = WLRRS1[nodeattrcnt].CWR.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("VPN", null);
                                attr.Value = WLRRS1[nodeattrcnt].VPN.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("COM", null);
                                attr.Value = WLRRS1[nodeattrcnt].COM.ToString();
                                childElement.Attributes.Append(attr);

                                //For Now we are not going to send this element as on 2008/06/19 till the BTOOLS request change is finalized. TKZ, GGA, SMD
                                //attr = xd.CreateAttribute("RBC", null);
                                //attr.Value = WLRRS1[nodeattrcnt].RBC.ToString();
                                //childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("PIC", null);
                                attr.Value = WLRRS1[nodeattrcnt].PIC.ToString();
                                childElement.Attributes.Append(attr);

                                attr = xd.CreateAttribute("RequestRemark", null);
                                attr.Value = WLRRS1[nodeattrcnt].RequestRemark.ToString();
                                childElement.Attributes.Append(attr);

                                nodeResponseBody.AppendChild(childElement); 

                                 
                            }
                            
                                             
                          
                      }
                 #endregion
             catch (Exception ex)
            {
                iStep35Result = 2;
                wsrqLogQueryData.lastStep = 35;
                wsrqLogQueryData.returnCode = 1200;
                sStep35ResultDesc = ex.Message + " - " + ex.StackTrace;
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
                continueChecking = false;
            }
            try
            {
                wsrqLogQueryData.rowCreationTime = DateTime.Now;
                dbHelperWSRQ.ConnectToDB();
                dbHelperWSRQ.BeginTransaction();
                if (iStep35Result == 1)
                {
                    WSRQDbHelper.LogRequestS35(wsrqLogQueryData, false);
                }
                else
                {
                    WSRQDbHelper.LogRequestS35(wsrqLogQueryData, true);
                }
                //Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 35, iStep35Result, sStep35ResultDesc, wsrqLogQueryData.rowCreationTime);
                dbHelperWSRQ.CommitTransaction();
            }
            catch (Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                continueChecking = false;
            }
            finally
            {
                dbHelperWSRQ.Close();
            }

            if (!continueChecking)
            {
                ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                ReconciliationResponseData.MessageTag = "";
                ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;

                return ReconciliationResponseData;
            }

            #endregion


            #region Step 40 - Hash & Encrypt Query Response.

            int iStep40Result = 1;
            string sStep40ResultDesc = "";
            Hashtable htResponse;
            byte[] outputHash = null;
            byte[] a3DesSessionKey = null;
            byte[] aEncResponse = null;

            string sDocwithHash = xd1.OuterXml.Replace("xmlns=\"\"","").Trim();
            string strResponseBodyContents = RegExHelper.ExtractBODYContents(sDocwithHash);

            if (strResponseBodyContents.Trim() == "")
            {
                throw new ApplicationException("No Body Node found");
            }

            try
            {
                
                #region Hash

                byte[] aBodyContents = System.Text.Encoding.Unicode.GetBytes(strResponseBodyContents);
                htResponse = new Hashtable();
                System.Diagnostics.Debug.WriteLine("Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
                outputHash = m_iCryptoOperations.Hash(aBodyContents, "SHA1", htResponse);
                System.Diagnostics.Debug.WriteLine(">>Hash " + DateTime.Now.ToString("HH:mm:ss:fff"));
                string hashBodyValue = Convert.ToBase64String(outputHash);
                string nsPrefix = RegExHelper.ExtractNameSpacePrefix(sDocwithHash, "http://www.iru.org/SafeTIRReconciliation");
                sDocwithHash = RegExHelper.SetHASH(sDocwithHash, nsPrefix, hashBodyValue);
                #endregion

                #region - Encrypt Query Response

                htResponse = new Hashtable();
                htResponse["IV"] = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
                byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
                aEncResponse = m_iCryptoOperations.Encrypt(aDocWithHash, "3DES", ref htResponse);
                a3DesSessionKey = (byte[])htResponse["KEY"];

                #endregion

                wsrqLogQueryData.decryptedSessionKeyOut = a3DesSessionKey;
                wsrqLogQueryData.responseEncryptionResult = 1;

            }
            catch (Exception ex)
            {
                iStep40Result = 8;
                wsrqLogQueryData.responseEncryptionResult = iStep40Result;
                wsrqLogQueryData.lastStep = 40;
                wsrqLogQueryData.returnCode = 1200;
                sStep40ResultDesc = ex.Message + " - " + ex.StackTrace;
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
                wsrqLogQueryData.responseEncryptionResultDesc = sStep40ResultDesc;
                continueChecking = false;
            }

            try
            {
                wsrqLogQueryData.rowCreationTime = DateTime.Now;
                dbHelperWSRQ.ConnectToDB();
                dbHelperWSRQ.BeginTransaction();
                if (iStep40Result == 1)
                {
                    WSRQDbHelper.LogRequestS40(wsrqLogQueryData, false);
                }
                else
                {
                    WSRQDbHelper.LogRequestS40(wsrqLogQueryData, true);
                }
                //Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 40, iStep40Result, sStep40ResultDesc, wsrqLogQueryData.rowCreationTime);
                dbHelperWSRQ.CommitTransaction();
            }
            catch (Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                continueChecking = false;
            }
            finally
            {
                dbHelperWSRQ.Close();
            }

            if (!continueChecking)
            {
                ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                ReconciliationResponseData.MessageTag = "";
                ReconciliationResponseData.ReconciliationRequestData = null; 
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;

                return ReconciliationResponseData;
            }

            #endregion


            #region Step 45 - Encrypt Session Key

            int iStep45Result = 1;
            string sStep45ResultDesc = "";
            string encryptionKeyID = "--";
            RSACryptoKey rKey = null;
            try
            {
                dbHelperSubscriber.ConnectToDB();
                iStep45Result = KeyManager.AssignSubscriberKey(wsrqLogQueryData.senderID, out rKey, out encryptionKeyID, dbHelperSubscriber);

                if (iStep45Result == 1)
                {
                    htResponse = new Hashtable();
                    htResponse["EXPONENT"] = rKey.Exponent;
                    htResponse["MODULUS"] = rKey.Modulus;
                    System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));

                    byte[] a3DesEncKey = null; ;
                    a3DesEncKey = m_iCryptoOperations.Encrypt(a3DesSessionKey, "RSA", ref htResponse);

                    wsrqLogQueryData.sessionKeyEncryptionKeyIDUsed = encryptionKeyID;
                    wsrqLogQueryData.encryptedSessionKeyOut = a3DesEncKey;
                }
                else
                {
                    continueChecking = false;
                    wsrqLogQueryData.responseEncryptionResult = iStep45Result;
                    wsrqLogQueryData.lastStep = 45;
                    wsrqLogQueryData.returnCode = 1200;
                    wsrqLogQueryData.responseEncryptionResultDesc = "";
                }
            }
            catch (Exception ex)
            {
                iStep45Result = 8;
                wsrqLogQueryData.responseEncryptionResult = iStep45Result;
                wsrqLogQueryData.lastStep = 45;
                wsrqLogQueryData.returnCode = 1200;
                sStep45ResultDesc = ex.Message + " - " + ex.StackTrace;
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
                wsrqLogQueryData.responseEncryptionResultDesc = sStep45ResultDesc;
                continueChecking = false;
            }
            finally
            {
                dbHelperSubscriber.Close();
            }

            try
            {
                wsrqLogQueryData.rowCreationTime = DateTime.Now;
                dbHelperWSRQ.ConnectToDB();
                dbHelperWSRQ.BeginTransaction();
                if (iStep45Result == 1)
                {
                    WSRQDbHelper.LogRequestS45(wsrqLogQueryData, false);
                }
                else
                {
                    
                    WSRQDbHelper.LogRequestS45(wsrqLogQueryData, true);
                }
                //Insert step in Sequence table
                WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 45, iStep45Result, sStep45ResultDesc, wsrqLogQueryData.rowCreationTime);
                dbHelperWSRQ.CommitTransaction();
            }
            catch (Exception ex)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
                continueChecking = false;
            }
            finally
            {
                dbHelperWSRQ.Close();
            }

            if (!continueChecking)
            {
                ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
                ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
                ReconciliationResponseData.MessageTag = "";
                ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
                return ReconciliationResponseData;
            }

            #endregion


            #region Step 99 - Confirm End of step & return result

			try
			{
				wsrqLogQueryData.rowCreationTime = DateTime.Now;
				wsrqLogQueryData.lastStep = 99;
				wsrqLogQueryData.returnCode = 2;
				dbHelperWSRQ.ConnectToDB();	
				dbHelperWSRQ.BeginTransaction();
				WSRQDbHelper.LogRequestS99(wsrqLogQueryData);
                //Insert step in Sequence table
				WSRQDbHelper.LogSequenceStep(wsrqLogQueryData.Exchange_ID, 99 , 2, "",wsrqLogQueryData.rowCreationTime);  
				dbHelperWSRQ.CommitTransaction();
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError , ex.Message + " - " + ex.StackTrace );  
				continueChecking = false;
			}
			finally
			{
				dbHelperWSRQ.Close();
			}

			if(!continueChecking )
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
				ReconciliationResponseData.MessageTag = "";
				ReconciliationResponseData.ReconciliationRequestData = null;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
                return ReconciliationResponseData;
			}
			else
			{
				ReconciliationResponse ReconciliationResponseData = new ReconciliationResponse();
				ReconciliationResponseData.ReturnCode = wsrqLogQueryData.returnCode;
    			ReconciliationResponseData.ESessionKey = wsrqLogQueryData.encryptedSessionKeyOut ;
                ReconciliationResponseData.MessageTag = encryptionKeyID ;
                ReconciliationResponseData.ReconciliationRequestData = aEncResponse;
                ReconciliationResponseData.Sender = wsrqLogQueryData.senderID;
                ReconciliationResponseData.Sender_MessageID = wsrqLogQueryData.Sender_MessageID;
              	return ReconciliationResponseData ;
			}
            
			#endregion

		

            //temp
              //  IRUQueryId = 0;
               // ReconciliationResponse rr = new ReconciliationResponse();
              //  return rr;

        }


        #region Parse WSRQQueryXMLErrorString
        private int GetWSRQueryXMLErrorCode(string sValidationResult, out string sNewValidationResult)
        {
            int iWSRQueryInvalidReasonNo = 0;
            string sErrNode = "";
            sNewValidationResult = "";
            if (sValidationResult.Trim().Length > 0)
            {
                string sFind = "SafeTIRReconciliation:";
                int pos1 = 0, pos2 = 0, iNoOfchars = 0;
                pos1 = sValidationResult.LastIndexOf(sFind);
                if (pos1 >= 0)
                {
                    pos2 = sValidationResult.IndexOf("'", pos1);
                    iNoOfchars = pos2 - pos1;
                    sErrNode = sValidationResult.Substring(pos1 + sFind.Length, iNoOfchars - sFind.Length);
                }
                else
                {
                    sNewValidationResult = sValidationResult;
                }

                iWSRQueryInvalidReasonNo = 1200;
                #region Sample XML
                //<ReconciliationQuery Sender_Document_Version="1.0.0" xmlns="http://www.iru.org/SafeTIRReconciliation">
                //<Body>
                //<SentTime>2004-05-19T13:54:50+04:00</SentTime>
                //<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
                //<QueryType>1</QueryType>
                //</Body>
                //</ReconciliationQuery>
                #endregion

                 if (sErrNode.Trim().ToUpper() == "ENVELOPE")
                {
                    iWSRQueryInvalidReasonNo = 1200;
                }
                else if (sErrNode.Trim().ToUpper() == "SENTTIME")
                {
                    iWSRQueryInvalidReasonNo = 1234;
                }
                else if (sErrNode.Trim().ToUpper() == "PASSWORD")
                {
                    iWSRQueryInvalidReasonNo = 1233;
                }
                else if (sErrNode.Trim().ToUpper() == "QUERY_TYPE")
                {
                    iWSRQueryInvalidReasonNo = 1239;
                }
                if (pos1 >= 0)
                {
                    sNewValidationResult = sValidationResult + " (Node:" + sErrNode + " - ErrorCode:" + iWSRQueryInvalidReasonNo.ToString().Trim() + ")";
                }
            }
            return iWSRQueryInvalidReasonNo;
        }
        #endregion



    }
        
}
