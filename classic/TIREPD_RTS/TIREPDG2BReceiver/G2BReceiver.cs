using System;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using System.Data;
using System.Data.SqlClient;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;
using IRU.CryptEngine;
using System.Xml;
using System.Collections;
using IRU.RTS.Crypto;
using IRU.RTS.TIREPD;
using IRU.RTS.Common.WCF; 

namespace IRU.RTS.TIREPD
{
    public class G2BReceiver:MarshalByRefObject, IG2BReceiver
    {
        private string m_sG2B_Received_Id;
        public G2BReceiver()
        {
            m_sG2B_Received_Id = "G2B_Message_ID";
        }
        ///// <summary>
        ///// Updates the RESPONSE_RESULT and time columns. The WEb Service Layer will call back this method after calling 
        ///// processquery and checking if the client is still connected
        ///// </summary>
        ///// <param name="QueryId"> Query ID , key to row in log table </param>
        ///// <param name="dtResponseSent">Time in the WS layer</param>
        ///// <param name="ResponseResult">true=client was still connected, false= client was not connected </param>
        //[System.Runtime.Remoting.Messaging.OneWay]
        //public void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult)
        //{
        //    #region Get IDBHelper instances from Plugin Manager
        //    IDBHelper dbHelperG2B = G2B_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TIREPD_DB");//  null; //dbhelper for g2bdb
        //    G2B_DBHelper g2bDbHelper = new G2B_DBHelper(dbHelperG2B);
        //    #endregion

        //    try
        //    {
        //        dbHelperG2B.ConnectToDB();
        //        g2bDbHelper.LogRequestResponse(QueryId, dtResponseSent, ResponseResult);
        //    }
        //    catch (Exception ex)
        //    {
        //        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
        //    }
        //    finally
        //    {
        //        dbHelperG2B.Close();
        //    }


        //}


        public TIREPDG2BUploadAck ProcessReceivedFile(TIREPDG2BUploadParams G2BUploadParams, string SenderIP, out long G2BMessageId)
        {
            bool continueChecking = true;
            G2BLogRequestStruct g2bRequestLogData = new G2BLogRequestStruct();
            g2bRequestLogData.returnCode = 1200; //if nothing else is sent.
            SubscriberDetailsStruct subscriberDetails = new SubscriberDetailsStruct();
            RSACryptoKey sessionDecrKey = null;
            IDBHelper dbHelperG2B = null;
            IDBHelper dbHelperSubscriber = null;
            G2B_DBHelper g2bDbHelper = null;
            Subscriber_DBHelper subsDbHelper = null;
            int keyStatus = 0;
            G2BMessageId = 0;

			using (NetTcpClient<ICryptoOperations> client = new NetTcpClient<ICryptoOperations>(G2B_RemotingHelper.m_CryptoProviderEndpoint))
			{
				ICryptoOperations m_iCryptoOperations = client.GetProxy();

				try
				{
					#region Get IDBHelper instances from Plugin Manager
					dbHelperG2B = G2B_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TIREPD_DB");//  null; //dbhelper for g2bdb
					dbHelperSubscriber = G2B_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB");//  null; //dbhelper for g2bdb
					#endregion

					#region a. Generate New ID
					long newID = IDHelper.GenerateID(m_sG2B_Received_Id, dbHelperG2B);
					G2BMessageId = newID;
					#endregion

					#region Initialize / create Variables

					g2bDbHelper = new G2B_DBHelper(dbHelperG2B);
					subsDbHelper = new Subscriber_DBHelper(dbHelperSubscriber);


					#endregion

					#region Populate g2bRequestLogData with available data as on step 5
					//b. Insert a row in G2B_REQUEST_LOG
					g2bRequestLogData.G2B_QueryID = newID;
					g2bRequestLogData.senderQueryID = G2BUploadParams.SubscriberMessageID;
					g2bRequestLogData.decryptionKeyID = G2BUploadParams.CertificateID;
					g2bRequestLogData.senderID = G2BUploadParams.SubscriberID;
					g2bRequestLogData.encryptedQueryParams = G2BUploadParams.MessageContent;
					g2bRequestLogData.encryptedSessionKeyIn = G2BUploadParams.ESessionKey;
					g2bRequestLogData.senderTCPAddress = SenderIP;
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					if (G2BUploadParams.SubscriberMessageID != null)
					{
						if (G2BUploadParams.SubscriberMessageID.Length > 255 || G2BUploadParams.SubscriberMessageID.Trim() == "")
						{
							g2bRequestLogData.senderQueryID = "";
							g2bRequestLogData.returnCode = 1214;//1222;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
						}
					}
					else
					{
						g2bRequestLogData.senderQueryID = "";
						g2bRequestLogData.returnCode = 1214;//1222;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
					}
					#endregion
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					g2bRequestLogData.senderQueryID = "";
					g2bRequestLogData.returnCode = 1214;//1222;
					g2bRequestLogData.lastStep = 5;
					continueChecking = false;
				}


				#region Step 5 - Initial Log Request & Validate Params
				if (continueChecking)
				{
					if (G2BUploadParams.CertificateID == null || G2BUploadParams.CertificateID.Trim() == "")
					{
						g2bRequestLogData.returnCode = 1211;//1210;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.decryptionKeyID = null;
					}
				}

				if (continueChecking)
				{
					//Get Keys for Decryption using CertificateID
					try
					{

						dbHelperSubscriber.ConnectToDB();
						keyStatus = KeyManager.GetIRUKeyDetails(G2BUploadParams.CertificateID, G2BUploadParams.SubscriberID, out sessionDecrKey, dbHelperSubscriber);
					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
						throw;
					}
					finally
					{
						dbHelperSubscriber.Close();
					}

					if (keyStatus == 3)
					{
						g2bRequestLogData.returnCode = 1211;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "KeyStatus for Subscriber " + G2BUploadParams.SubscriberID + " - " + G2BUploadParams.CertificateID + " = " + keyStatus.ToString());
						//TODO:Update G2BLOG & Sequence with Key Status 1200
						//return new TIRHolderResponse(); // return to FCS the respose struct
					}
					else if (keyStatus == 9)
					{
						g2bRequestLogData.returnCode = 1211;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
					}

				}
				if (continueChecking)
				{
					if ((G2BUploadParams.SubscriberID == null || G2BUploadParams.SubscriberID.Trim() == ""))
					{
						g2bRequestLogData.returnCode = 1210;//1212;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.senderID = null;
					}
				}
				if (continueChecking)
				{

					subscriberDetails.subscriberID = G2BUploadParams.SubscriberID;
					int iAuthenticateQuerySender = 0;
					try
					{
						dbHelperSubscriber.ConnectToDB();
						iAuthenticateQuerySender = subsDbHelper.AuthenticateQuerySender(subscriberDetails.subscriberID,
							out subscriberDetails.password, "G2B", 1, out subscriberDetails.SessionKeyAlgo,
							out subscriberDetails.HashAlgo, out subscriberDetails.CopyToId, out subscriberDetails.CopyToAddress);
					}
					catch (Exception ex)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
						throw;
					}
					finally
					{
						dbHelperSubscriber.Close();
					}
					//Also it is checked here for Invalid Method instead of Step 30
					if (iAuthenticateQuerySender != 0)
					{
						g2bRequestLogData.returnCode = 1210;//iAuthenticateQuerySender;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
					}
				}

				if (continueChecking)
				{
					if (G2BUploadParams.ESessionKey == null)
					{
						g2bRequestLogData.returnCode = 1213;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.encryptedSessionKeyIn = null;
					}
					else
					{
						if (G2BUploadParams.ESessionKey.Length == 0)
						{
							g2bRequestLogData.returnCode = 1213;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
							//g2bRequestLogData.encryptedSessionKeyIn = null;
						}
					}
				}
				if (continueChecking)
				{
					if (G2BUploadParams.InformationExchangeVersion == null)
					{
						g2bRequestLogData.returnCode = 1215;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.encryptedSessionKeyIn = null;
					}
					else
					{
						if (G2BUploadParams.InformationExchangeVersion.Length == 0)
						{
							g2bRequestLogData.returnCode = 1215;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
							//g2bRequestLogData.encryptedSessionKeyIn = null;
						}
					}
				}

				if (continueChecking)
				{
					if (G2BUploadParams.MessageName != null)
					{
						if (G2BUploadParams.MessageName.Length == 0)
						{
							g2bRequestLogData.returnCode = 1216;//1214;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
							//g2bRequestLogData.encryptedQueryParams = null;

						}
						else
						{
							continueChecking = false;
							foreach (string strMsgName in G2B_RemotingHelper.m_MessageNameArr)
							{
								if (G2BUploadParams.MessageName == strMsgName)
								{
									continueChecking = true;
									break;
								}
							}
							if (continueChecking == false)
							{
								g2bRequestLogData.returnCode = 1216;//1214;
								g2bRequestLogData.lastStep = 5;

							}
						}
					}
					else
					{
						g2bRequestLogData.returnCode = 1216;//1214;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
					}
				}
				if (continueChecking)
				{
					if (G2BUploadParams.TimeSent == null)
					{
						g2bRequestLogData.returnCode = 1217;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.encryptedSessionKeyIn = null;
					}
					else
					{
						if (G2BUploadParams.TimeSent == DateTime.MinValue)
						{
							g2bRequestLogData.returnCode = 1217;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
							//g2bRequestLogData.encryptedSessionKeyIn = null;
						}
					}
				}

				if (continueChecking)
				{
					if (G2BUploadParams.MessageContent == null)
					{
						g2bRequestLogData.returnCode = 1218;//1214;
						g2bRequestLogData.lastStep = 5;
						continueChecking = false;
						//g2bRequestLogData.encryptedQueryParams = null;
					}
					else
					{
						if (G2BUploadParams.MessageContent.Length == 0)
						{
							g2bRequestLogData.returnCode = 1218;//1214;
							g2bRequestLogData.lastStep = 5;
							continueChecking = false;
							//g2bRequestLogData.encryptedQueryParams = null;

						}
					}
				}

				try
				{
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperG2B.ConnectToDB();
					dbHelperG2B.BeginTransaction();
					if (continueChecking)
					{
						g2bDbHelper.LogRequestS5(g2bRequestLogData, false);
						g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 5, g2bRequestLogData.returnCode, "", g2bRequestLogData.rowCreationTime);
					}
					else
					{
						g2bDbHelper.LogRequestS5(g2bRequestLogData, true);
						g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 5, g2bRequestLogData.returnCode, "", g2bRequestLogData.rowCreationTime);
					}
					//Insert step in Sequence table
					dbHelperG2B.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperG2B.Close();
				}
				if (!continueChecking)
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}
				#endregion

				#region  Step 10 - Session Key Decryption

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

					g2bRequestLogData.decryptedSessionKeyIn = null;

					try
					{
						decrSessionKeyIn = m_iCryptoOperations.Decrypt(G2BUploadParams.ESessionKey, subscriberDetails.SessionKeyAlgo, hashForSessionKey);

						g2bRequestLogData.decryptedSessionKeyIn = decrSessionKeyIn;
					}
					catch (Exception ex)
					{
						iStep10Result = 8;
						g2bRequestLogData.returnCode = 1301;//1230;
						g2bRequestLogData.lastStep = 10;

						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

						iStep10ResultDesc = ex.Message + " -> " + ex.StackTrace;
						continueChecking = false;
					}
				}
				else
				{
					iStep10Result = 7;
					g2bRequestLogData.returnCode = 1301;//1230;
					g2bRequestLogData.lastStep = 10;
					continueChecking = false;
				}

				try
				{
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperG2B.ConnectToDB();
					dbHelperG2B.BeginTransaction();
					if (iStep10Result == 1)
					{
						g2bDbHelper.LogRequestS10(g2bRequestLogData, false);
					}
					else
					{
						g2bDbHelper.LogRequestS10(g2bRequestLogData, true);
					}
					//Insert step in Sequence table
					g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 10, iStep10Result, iStep10ResultDesc, g2bRequestLogData.rowCreationTime);
					dbHelperG2B.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperG2B.Close();
				}

				if (!continueChecking)
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}
				#endregion

				#region Step 15 - do Decryption & Get Decrypted Data.

				int iStep15Result = 1;
				string sStep15ResultDesc = "";
				//This is to be used 
				Hashtable hashDecryptParams = new Hashtable();

				byte[] byIV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };

				hashDecryptParams["KEY"] = decrSessionKeyIn;
				hashDecryptParams["IV"] = byIV;

				try
				{
					byte[] baDecryptedQueryParamXML =
						m_iCryptoOperations.Decrypt(g2bRequestLogData.encryptedQueryParams,
						"3DES", hashDecryptParams);

					iStep15Result = 1;

					//g2bRequestLogData.decryptedQueryParamXML = Convert.ToBase64String(decryptedQueryParamXML);
					g2bRequestLogData.decryptedQueryParamXML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);
					g2bRequestLogData.decryptionResult = 1;

				}
				catch (Exception ex)
				{
					iStep15Result = 8;
					g2bRequestLogData.returnCode = 1301;//1231;
					g2bRequestLogData.lastStep = 15;

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					sStep15ResultDesc = ex.Message + " -> " + ex.StackTrace;

					g2bRequestLogData.decryptedQueryParamXML = null;
					g2bRequestLogData.decryptionResult = 8;
					continueChecking = false;
				}
				g2bRequestLogData.decryptionResultDesc = sStep15ResultDesc;
				g2bRequestLogData.rowCreationTime = DateTime.Now;

				try
				{
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperG2B.ConnectToDB();
					dbHelperG2B.BeginTransaction();
					if (iStep15Result == 1)
					{
						g2bDbHelper.LogRequestS15(g2bRequestLogData, false);
					}
					else
					{
						g2bDbHelper.LogRequestS15(g2bRequestLogData, true);
					}
					//Insert step in Sequence table
					g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 15, iStep15Result, sStep15ResultDesc, g2bRequestLogData.rowCreationTime);
					dbHelperG2B.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperG2B.Close();
				}

				if (!continueChecking)
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}

				//
				//			//TODO:forNowJust do the following
				//			#region this code needs to be replaced with Actual Decryption Code
				//			g2bRequestLogData.decryptedQueryParamXML = DecryptXML(decryptedQueryParamXML);
				//			g2bRequestLogData.decryptionResult = 1;
				//			g2bRequestLogData.decryptionResultDesc = "";
				//			#endregion


				#endregion

				#region Step 20 - do Validate Hash - no longer used so commented
				//int iStep20Result = 1;
				//string sStep20ResultDesc = "";

				//try
				//{
				//    /* //replaced by regexhelper
				//    int iHashStart = g2bRequestLogData.decryptedQueryParamXML.IndexOf("<Hash>");

				//    if(iHashStart >0)
				//    {
				//        iHashStart += 6;
				//    }
				//    else
				//    {
				//        throw new ApplicationException("No Hash found");
				//    }
				//    int iHashEnd = g2bRequestLogData.decryptedQueryParamXML.IndexOf("</Hash>");
				//    int iHashLength = iHashEnd - iHashStart; 
				//    string sHash =  g2bRequestLogData.decryptedQueryParamXML.Substring(iHashStart,iHashLength);  
				//    */

				//    string sHash = RegExHelper.ExtractHASH(g2bRequestLogData.decryptedQueryParamXML);//.Substring(iHashStart,iHashLength);  

				//    if (sHash.Trim() == "")
				//    {
				//        throw new ApplicationException("No Hash found");
				//    }


				//    byte[] baHash = Convert.FromBase64String(sHash);

				//    /* //replaced by regexhelper
				//    int iBodyStart = g2bRequestLogData.decryptedQueryParamXML.IndexOf("<Body>");
				//    if(iBodyStart > 0)
				//    {
				//        iBodyStart += 6;
				//    }
				//    else
				//    {
				//        throw new ApplicationException("No Body Node found");
				//    }
				//    int iBodyEnd = g2bRequestLogData.decryptedQueryParamXML.IndexOf("</Body>");
				//    int iBodyLength = iBodyEnd - iBodyStart ; 
				//    string sBody =  g2bRequestLogData.decryptedQueryParamXML.Substring(iBodyStart,iBodyLength );  
				//    */

				//    string sBody = RegExHelper.ExtractBODYContents(g2bRequestLogData.decryptedQueryParamXML);//.Substring(iBodyStart,iBodyLength );  

				//    if (sBody.Trim() == "")
				//    {
				//        throw new ApplicationException("No Body Node found");
				//    }



				//    byte[] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);


				//    if (!m_iCryptoOperations.VerifyHash(baBody, subscriberDetails.HashAlgo, null, baHash))
				//    {
				//        g2bRequestLogData.returnCode = 1200;
				//        g2bRequestLogData.lastStep = 20;
				//        iStep20Result = 7;
				//        sStep20ResultDesc = "Hash Verification Failed";
				//        continueChecking = false;
				//    }
				//}
				//catch (Exception ex)
				//{
				//    g2bRequestLogData.returnCode = 1200;
				//    g2bRequestLogData.lastStep = 20;
				//    iStep20Result = 8;
				//    sStep20ResultDesc = ex.Message + " - " + ex.StackTrace;
				//    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

				//    continueChecking = false;
				//}


				//try
				//{
				//    g2bRequestLogData.rowCreationTime = DateTime.Now;
				//    dbHelperG2B.ConnectToDB();
				//    dbHelperG2B.BeginTransaction();
				//    if (iStep20Result == 1)
				//    {
				//        g2bDbHelper.LogRequestS20(g2bRequestLogData, false);
				//    }
				//    else
				//    {
				//        g2bDbHelper.LogRequestS20(g2bRequestLogData, true);
				//    }
				//    //Insert step in Sequence table
				//    g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 20, iStep20Result, sStep20ResultDesc, g2bRequestLogData.rowCreationTime);
				//    dbHelperG2B.CommitTransaction();
				//}
				//catch (Exception ex)
				//{
				//    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
				//    continueChecking = false;
				//}
				//finally
				//{
				//    dbHelperG2B.Close();
				//}

				//if (!continueChecking)
				//{
				//    TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
				//    ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
				//    ack.ReturnCode = g2bRequestLogData.returnCode;
				//    return ack;
				//}

				#endregion

				#region Step 25 - Validate against request XSD
				int iStep25Result = 1;
				string sStep25ResultDesc = "";
				try
				{
					EPDXMLValidationHelper xvh = new EPDXMLValidationHelper();
					if (!xvh.ValidateXML(g2bRequestLogData.decryptedQueryParamXML, out g2bRequestLogData.invalidQueryXMLReason))
					{
						//iStep25Result = GetTCHQueryXMLErrorCode(g2bRequestLogData.invalidQueryXMLReason, out g2bRequestLogData.invalidQueryXMLReason);
						iStep25Result = 1302;
						throw new ApplicationException(g2bRequestLogData.invalidQueryXMLReason);
					}
				}
				catch (Exception ex)
				{
					g2bRequestLogData.returnCode = iStep25Result;
					g2bRequestLogData.lastStep = 25;
					sStep25ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

					continueChecking = false;
				}
				try
				{
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperG2B.ConnectToDB();
					dbHelperG2B.BeginTransaction();
					if (iStep25Result == 1)
					{
						g2bDbHelper.LogRequestS25(g2bRequestLogData, false);
					}
					else
					{
						g2bDbHelper.LogRequestS25(g2bRequestLogData, true);
					}
					//Insert step in Sequence table
					g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 25, iStep25Result, sStep25ResultDesc, g2bRequestLogData.rowCreationTime);
					dbHelperG2B.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperG2B.Close();
				}

				if (!continueChecking)
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}


				#endregion

				#region Step 99 - Confirm End of step & return result

				try
				{
					XmlDocument xdoc = new XmlDocument();
					xdoc.LoadXml(g2bRequestLogData.decryptedQueryParamXML);
					CreateFileInFolder(xdoc, G2BUploadParams.SubscriberID);
					g2bRequestLogData.rowCreationTime = DateTime.Now;
					g2bRequestLogData.lastStep = 99;
					g2bRequestLogData.returnCode = 2;
					dbHelperG2B.ConnectToDB();
					dbHelperG2B.BeginTransaction();
					//Insert step in Sequence table
					g2bDbHelper.LogRequestS99(g2bRequestLogData);
					g2bDbHelper.LogSequenceStep(g2bRequestLogData.G2B_QueryID, 99, 2, "", g2bRequestLogData.rowCreationTime);
					dbHelperG2B.CommitTransaction();


				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperG2B.Close();
				}

				if (!continueChecking)
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}
				else
				{
					TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
					ack.HostID = G2BUploadParams.SubscriberID;
					ack.SubscriberMessageID = G2BUploadParams.SubscriberMessageID;
					ack.ReturnCode = g2bRequestLogData.returnCode;
					return ack;
				}

				#endregion


				//TIREPDG2BUploadAck ack1 = new TIREPDG2BUploadAck();

				//return ack1;
			}
        }

        private bool CreateFileInFolder(XmlDocument xdoc, string subscriberId)
        {
            bool bFoundPath = false;
            bFoundPath = G2B_RemotingHelper.m_hsCountryISO_INMessagePath.Contains(subscriberId);
            Random rnd;
            if (bFoundPath)
            {
                try
                {
                    DateTime dtNow = DateTime.Now;
                    rnd = new Random(dtNow.Millisecond);
                    string sPathName = subscriberId + "_"
                    + dtNow.Year.ToString().PadLeft(4, '0')
                    + dtNow.Month.ToString().PadLeft(2, '0')
                    + dtNow.Day.ToString().PadLeft(2, '0')
                    + "_"
                    + dtNow.Hour.ToString().PadLeft(2, '0')
                    + dtNow.Minute.ToString().PadLeft(2, '0')
                    + dtNow.Second.ToString().PadLeft(2, '0')
                    + dtNow.Millisecond.ToString().PadLeft(3, '0')
                    + "_"
                    + rnd.Next(99999).ToString().PadLeft(5, '0')
                    + ".xml";

                    xdoc.Save(((string[])G2B_RemotingHelper.m_hsCountryISO_INMessagePath[subscriberId])[2] + @"\\" + sPathName);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }
}
