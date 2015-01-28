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
using IRU.RTS.Common.WCF;

namespace IRU.RTS.WSEGIS
{
	/// <summary>
	/// Summary description for EGIS_QueryProcessor.
	/// </summary>
	public class EGIS_QueryProcessor : MarshalByRefObject, IEGISProcessor
	{
		private ICryptoOperations m_iCryptoOperations;

		private string m_sEGIS_Query_Id;

		private static string GetBlankIfNull(object Obj)
		{
			if (Obj == null)
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
			if (DateTimeobj == null)
			{
				return "";
			}
			else
			{
				//return XmlConvert.ToString((DateTime)DateTimeobj); 
				return ((DateTime)DateTimeobj).ToString("yyyy-MM-dd");
			}
		}

		public EGIS_QueryProcessor()
		{
			m_sEGIS_Query_Id = "EGIS_Query_ID";
		}

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
			IDBHelper dbHelperEGIS = EGIS_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("EGISDB");//  null; //dbhelper for EGISdb
			EGIS_DBHelper EGISDbHelper = new EGIS_DBHelper(dbHelperEGIS);
			#endregion

			try
			{
				dbHelperEGIS.ConnectToDB();
				EGISDbHelper.LogRequestResponse(QueryId, dtResponseSent, ResponseResult);
			}
			catch (Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
			}
			finally
			{
				dbHelperEGIS.Close();
			}


		}

		public EGISResponseType ProcessQuery(EGISQueryType EGISQuery, string SenderIP, out long IRUQueryId)
		{
			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperEGIS = EGIS_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("EGISDB");//  null; //dbhelper for egisdb
			IDBHelper dbHelperSubscriber = EGIS_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB");//  null; //dbhelper for subscriberdb
			#endregion

			#region a. Generate New ID
			long newID = IDHelper.GenerateID(m_sEGIS_Query_Id, dbHelperEGIS);
			IRUQueryId = newID;
			#endregion

			#region Initialize / create Variables

			EGIS_DBHelper egisDbHelper = new EGIS_DBHelper(dbHelperEGIS);
			Subscriber_DBHelper subsDbHelper = new Subscriber_DBHelper(dbHelperSubscriber);

			bool continueChecking = true;
			EGISLogRequestStruct egisRequestLogData = new EGISLogRequestStruct();
			SubscriberDetailsStruct subscriberDetails = new SubscriberDetailsStruct();
			RSACryptoKey sessionDecrKey = null;
			int keyStatus = 0;

			#endregion

			using (NetTcpClient<ICryptoOperations> client = new NetTcpClient<ICryptoOperations>(EGIS_RemotingHelper.m_CryptoProviderEndpoint))
			{
				ICryptoOperations m_iCryptoOperations = client.GetProxy();

				#region Populate egisRequestLogData with available data as on step 5
				//b. Insert a row in EGIS_REQUEST_LOG
				egisRequestLogData.EGIS_QueryID = newID;
				egisRequestLogData.senderQueryID = EGISQuery.Query_ID;
				egisRequestLogData.decryptionKeyID = EGISQuery.MessageTag;
				egisRequestLogData.senderID = EGISQuery.SubscriberID;
				egisRequestLogData.encryptedQueryParams = EGISQuery.EGISQueryParams;
				egisRequestLogData.encryptedSessionKeyIn = EGISQuery.ESessionKey;
				egisRequestLogData.senderTCPAddress = SenderIP;
				egisRequestLogData.rowCreationTime = DateTime.Now;

				if (EGISQuery.Query_ID.Length > 255)
				{
					egisRequestLogData.senderQueryID = "";
					egisRequestLogData.returnCode = 1232;
					egisRequestLogData.lastStep = 5;
					continueChecking = false;
				}
				#endregion

				#region Step 5 - Initial Log Request & Validate Params

				if (continueChecking)
				{
					if (EGISQuery.MessageTag == null || EGISQuery.MessageTag.Trim() == "")
					{
						egisRequestLogData.returnCode = 1210;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
						//egisRequestLogData.decryptionKeyID = null;
					}
				}

				if (continueChecking)
				{
					//Get Keys for Decryption using MessageTag
					try
					{

						dbHelperSubscriber.ConnectToDB();
						keyStatus = KeyManager.GetIRUKeyDetails(EGISQuery.MessageTag, EGISQuery.SubscriberID, out sessionDecrKey, dbHelperSubscriber);
					}
					finally
					{
						dbHelperSubscriber.Close();
					}
					if (keyStatus == 3)
					{
						egisRequestLogData.returnCode = 1211;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, "KeyStatus for Subscriber " + EGISQuery.SubscriberID + " - " + EGISQuery.MessageTag + " = " + keyStatus.ToString());
					}
					else if (keyStatus == 9)
					{
						egisRequestLogData.returnCode = 1211;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
					}

				}
				if (continueChecking)
				{
					if ((EGISQuery.SubscriberID == null || EGISQuery.SubscriberID.Trim() == ""))
					{
						egisRequestLogData.returnCode = 1212;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
					}
				}
				if (continueChecking)
				{

					subscriberDetails.subscriberID = EGISQuery.SubscriberID;
					int iAuthenticateQuerySender = 0;
					try
					{
						dbHelperSubscriber.ConnectToDB();
						iAuthenticateQuerySender = subsDbHelper.AuthenticateQuerySender(subscriberDetails.subscriberID,
							out subscriberDetails.password, "EGIS", 1, out subscriberDetails.SessionKeyAlgo,
							out subscriberDetails.HashAlgo, out subscriberDetails.CopyToId, out subscriberDetails.CopyToAddress);
					}
					finally
					{
						dbHelperSubscriber.Close();
					}
					//Also it is checked here for Invalid Method instead of Step 30
					if (iAuthenticateQuerySender != 0)
					{
						egisRequestLogData.returnCode = iAuthenticateQuerySender;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
					}
				}

				if (continueChecking)
				{
					if (EGISQuery.ESessionKey == null)
					{
						egisRequestLogData.returnCode = 1213;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
						//egisRequestLogData.encryptedSessionKeyIn = null;
					}
					else
					{
						if (EGISQuery.ESessionKey.Length == 0)
						{
							egisRequestLogData.returnCode = 1213;
							egisRequestLogData.lastStep = 5;
							continueChecking = false;
							//egisRequestLogData.encryptedSessionKeyIn = null;
						}
					}
				}

				if (continueChecking)
				{
					if (EGISQuery.EGISQueryParams == null)
					{
						egisRequestLogData.returnCode = 1214;
						egisRequestLogData.lastStep = 5;
						continueChecking = false;
						//egisRequestLogData.encryptedQueryParams = null;
					}
					else
					{
						if (EGISQuery.EGISQueryParams.Length == 0)
						{
							egisRequestLogData.returnCode = 1214;
							egisRequestLogData.lastStep = 5;
							continueChecking = false;
							//egisRequestLogData.encryptedQueryParams = null;

						}
					}
				}

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (continueChecking)
					{
						egisDbHelper.LogRequestS5(egisRequestLogData, false);
						egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 5, egisRequestLogData.returnCode, "", egisRequestLogData.rowCreationTime);
					}
					else
					{
						egisDbHelper.LogRequestS5(egisRequestLogData, true);
						egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 5, egisRequestLogData.returnCode, "", egisRequestLogData.rowCreationTime);
					}
					//Insert step in Sequence table
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}
				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
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

					egisRequestLogData.decryptedSessionKeyIn = null;

					try
					{
						decrSessionKeyIn = m_iCryptoOperations.Decrypt(EGISQuery.ESessionKey, subscriberDetails.SessionKeyAlgo, hashForSessionKey);

						egisRequestLogData.decryptedSessionKeyIn = decrSessionKeyIn;
					}
					catch (Exception ex)
					{
						iStep10Result = 8;
						egisRequestLogData.returnCode = 1230;
						egisRequestLogData.lastStep = 10;

						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

						iStep10ResultDesc = ex.Message + " -> " + ex.StackTrace;
						continueChecking = false;
					}
				}
				else
				{
					iStep10Result = 7;
					egisRequestLogData.returnCode = 1230;
					egisRequestLogData.lastStep = 10;
					continueChecking = false;
				}

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep10Result == 1)
					{
						egisDbHelper.LogRequestS10(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS10(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 10, iStep10Result, iStep10ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
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
						m_iCryptoOperations.Decrypt(egisRequestLogData.encryptedQueryParams,
						"3DES", hashDecryptParams);

					iStep15Result = 1;

					//egisRequestLogData.decryptedQueryParamXML = Convert.ToBase64String(decryptedQueryParamXML);
					egisRequestLogData.decryptedQueryParamXML = System.Text.Encoding.Unicode.GetString(baDecryptedQueryParamXML);
					egisRequestLogData.decryptionResult = 1;

				}
				catch (Exception ex)
				{
					iStep15Result = 8;
					egisRequestLogData.returnCode = 1231;
					egisRequestLogData.lastStep = 15;

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					sStep15ResultDesc = ex.Message + " -> " + ex.StackTrace;

					egisRequestLogData.decryptedQueryParamXML = null;
					egisRequestLogData.decryptionResult = 8;
					continueChecking = false;
				}
				egisRequestLogData.decryptionResultDesc = sStep15ResultDesc;
				egisRequestLogData.rowCreationTime = DateTime.Now;

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep15Result == 1)
					{
						egisDbHelper.LogRequestS15(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS15(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 15, iStep15Result, sStep15ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}

				#endregion

				#region Step 20 - do Validate Hash
				int iStep20Result = 1;
				string sStep20ResultDesc = "";

				try
				{
					string sHash = RegExHelper.ExtractHASH(egisRequestLogData.decryptedQueryParamXML);//.Substring(iHashStart,iHashLength);  

					if (sHash.Trim() == "")
					{
						throw new ApplicationException("No Hash found");
					}


					byte[] baHash = Convert.FromBase64String(sHash);

					/* //replaced by regexhelper
					int iBodyStart = egisRequestLogData.decryptedQueryParamXML.IndexOf("<Body>");
					if(iBodyStart > 0)
					{
						iBodyStart += 6;
					}
					else
					{
						throw new ApplicationException("No Body Node found");
					}
					int iBodyEnd = egisRequestLogData.decryptedQueryParamXML.IndexOf("</Body>");
					int iBodyLength = iBodyEnd - iBodyStart ; 
					string sBody =  egisRequestLogData.decryptedQueryParamXML.Substring(iBodyStart,iBodyLength );  
					*/

					string sBody = RegExHelper.ExtractBODYContents(egisRequestLogData.decryptedQueryParamXML);//.Substring(iBodyStart,iBodyLength );  

					if (sBody.Trim() == "")
					{
						throw new ApplicationException("No Body Node found");
					}



					byte[] baBody = System.Text.Encoding.Unicode.GetBytes(sBody);


					if (!m_iCryptoOperations.VerifyHash(baBody, subscriberDetails.HashAlgo, null, baHash))
					{
						egisRequestLogData.returnCode = 1200;
						egisRequestLogData.lastStep = 20;
						iStep20Result = 7;
						sStep20ResultDesc = "Hash Verification Failed";
						continueChecking = false;
					}
				}
				catch (Exception ex)
				{
					egisRequestLogData.returnCode = 1200;
					egisRequestLogData.lastStep = 20;
					iStep20Result = 8;
					sStep20ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

					continueChecking = false;
				}


				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep20Result == 1)
					{
						egisDbHelper.LogRequestS20(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS20(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 20, iStep20Result, sStep20ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}

				#endregion

				#region Step 25 - Validate against request XSD
				int iStep25Result = 1;
				string sStep25ResultDesc = "";
				try
				{
					XMLValidationHelper xvh = new XMLValidationHelper();
					if (!xvh.ValidateXML(egisRequestLogData.decryptedQueryParamXML, out egisRequestLogData.invalidQueryXMLReason))
					{
						iStep25Result = GetEGISQueryXMLErrorCode(egisRequestLogData.invalidQueryXMLReason, out egisRequestLogData.invalidQueryXMLReason);
						throw new ApplicationException(egisRequestLogData.invalidQueryXMLReason);
					}
				}
				catch (Exception ex)
				{
					egisRequestLogData.returnCode = iStep25Result;
					egisRequestLogData.lastStep = 25;
					sStep25ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);

					continueChecking = false;
				}
				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep25Result == 1)
					{
						egisDbHelper.LogRequestS25(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS25(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 25, iStep25Result, sStep25ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}


				#endregion

				#region Step 30 - Authorize User
				int iStep30Result = 1;
				string sStep30ResultDesc = "";
				try
				{
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(egisRequestLogData.decryptedQueryParamXML);

					XmlNamespaceManager xns = new XmlNamespaceManager(xd.NameTable);
					xns.AddNamespace("def", "http://rts.iru.org/egis");

					XmlNode nodeSender = xd.DocumentElement.SelectSingleNode("/def:EGISQuery/def:Body/def:Sender", xns);
					string BodySender = nodeSender.InnerText;
					if (subscriberDetails.subscriberID.Trim() != BodySender.Trim())
					{
						egisRequestLogData.returnCode = 1242;
						iStep30Result = 2;
						sStep30ResultDesc = "Sender Not Found or Not Same as SubscriberID";
						throw new ApplicationException(sStep30ResultDesc);
					}
					egisRequestLogData.senderID = BodySender;

					XmlNode node = xd.DocumentElement.SelectSingleNode("/def:EGISQuery/def:Body/def:Password", xns);
					if (node == null)
					{
						egisRequestLogData.senderPassword = "";
						if (subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
						{
							//Password is valid - there is no password or password node also might not be present
						}
						else
						{
							egisRequestLogData.returnCode = 1233;
							iStep30Result = 3;
							sStep30ResultDesc = "Password does not match";
							throw new ApplicationException("Password Verification Failed");
						}
					}
					else
					{
						string password = node.InnerText;
						egisRequestLogData.senderPassword = password;
						if (subscriberDetails.password == null || subscriberDetails.password.Trim() == "")
						{
							if (password.Trim() == "")
							{

								//PAssword is valid - there is no password or password node also might no be present
							}
							else
							{
								egisRequestLogData.returnCode = 1233;
								iStep30Result = 3;
								sStep30ResultDesc = "Password does not match";
								throw new ApplicationException("Password Verification Failed");
							}
						}
						else if (password.Trim() == subscriberDetails.password)
						{
							//PAssword is valid - there is no password or password node also might no be present
						}
						else
						{
							egisRequestLogData.returnCode = 1233;
							iStep30Result = 3;
							sStep30ResultDesc = "Password does not match";
							throw new ApplicationException("Password Verification Failed");
						}
					}
					node = xd.DocumentElement.SelectSingleNode("/def:EGISQuery/def:Body/def:Sender", xns);
					if (node == null)
					{
						egisRequestLogData.returnCode = 1242;
						iStep30Result = 2;
						sStep30ResultDesc = "Sender Not found";
						throw new ApplicationException("Sender Verification Failed");
					}
					else
					{
						if (node.InnerText.Trim().ToUpper() != egisRequestLogData.senderID.Trim().ToUpper())
						{
							egisRequestLogData.returnCode = 1242;
							iStep30Result = 2;
							sStep30ResultDesc = "Sender Not Valid";
							throw new ApplicationException("Sender Verification Failed");
						}
					}
					egisRequestLogData.senderAuthenticated = iStep30Result;
				}
				catch (ApplicationException ex)
				{
					egisRequestLogData.lastStep = 30;
					continueChecking = false;
				}
				catch (Exception ex)
				{
					if (iStep30Result == 1)
					{
						egisRequestLogData.returnCode = 1200;
						iStep30Result = 1200;
					}
					egisRequestLogData.lastStep = 30;
					sStep30ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep30Result == 1)
					{
						egisDbHelper.LogRequestS30(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS30(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 30, iStep30Result, sStep30ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}
				#endregion

				#region Step 35 - Get CW Query Result
				int iStep35Result = 1;
				string sStep35ResultDesc = "";
				Hashtable cwHashTable = null;
				XmlNode nodeResponseBody = null;
				XmlNamespaceManager xnsResponse = null;
				XmlDocument xdResponse = new XmlDocument();
				int iCWResultCode = 0;

				try
				{
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(egisRequestLogData.decryptedQueryParamXML);

					XmlNamespaceManager xns = new XmlNamespaceManager(xd.NameTable);
					xns.AddNamespace("def", "http://rts.iru.org/egis");

					XmlNode node = xd.DocumentElement.SelectSingleNode("/def:EGISQuery/def:Body", xns);
					egisRequestLogData.originatorID = node.SelectSingleNode("./def:Originator", xns).InnerText;
					egisRequestLogData.originTime = DateTime.Parse(node.SelectSingleNode("./def:OriginTime", xns).InnerText);
					egisRequestLogData.queryType = Convert.ToInt32(node.SelectSingleNode("./def:Query_Type", xns).InnerText);
					//egisRequestLogData.queryReason = Convert.ToInt32(node.SelectSingleNode("./def:Query_Reason", xns).InnerText);
					egisRequestLogData.tirCarnetNumber = node.SelectSingleNode("./def:Carnet_Number", xns).InnerText;

					//	<Sender>FCS</Sender>
					//	<SentTime>2004-05-19T13:54:50Z</SentTime>
					//	<Originator>Originator1</Originator>
					//	<OriginTime>2004-05-19T13:54:50Z</OriginTime>
					//	<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
					//	<Query_Type>1</Query_Type>
					//	<Carnet_Number>15042217</Carnet_Number>

					#region Query to Carneting Service

					try
					{
						EGIS_CarnetingServiceHelper carnetingSvcHelper = new EGIS_CarnetingServiceHelper(EGIS_RemotingHelper.m_CarnetingServiceEndpoint);
						cwHashTable = carnetingSvcHelper.GetCarnetDetailsAndSafeTIRData(egisRequestLogData.tirCarnetNumber, (egisRequestLogData.queryType & 0x2) == 0x2);
						iCWResultCode = int.Parse(cwHashTable["Query_Result_Code"].ToString());
					}
					catch (Exception e)
					{
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning,
							"Invalid value whilst retrieving the Data from the Carneting Service in EGIS_Processor.EGIS_CarnetingServiceHelper.cs"
							+ e.Message);
						throw e;
					}

					#endregion

					xdResponse = EGIS_RemotingHelper.m_InMemoryCachePlugin.GetXMLDomFromCache("QueryRespTemplate");
					xnsResponse = new XmlNamespaceManager(xdResponse.NameTable);
					xnsResponse.AddNamespace("tchq", "http://www.iru.org/TCHQResponse");
					xnsResponse.AddNamespace("safetir", "http://www.iru.org/SafeTIRUpload");
					xnsResponse.AddNamespace("egis", "http://rts.iru.org/egis");

					nodeResponseBody = xdResponse.DocumentElement.SelectSingleNode("/egis:EGISResponse/egis:Body", xnsResponse);

					nodeResponseBody.SelectSingleNode("./tchq:Sender", xnsResponse).InnerText = "IRU";
					nodeResponseBody.SelectSingleNode("./tchq:Originator", xnsResponse).InnerText = egisRequestLogData.originatorID;

					XmlNode tmpNode1;
					nodeResponseBody.SelectSingleNode("./tchq:ResponseTime", xnsResponse).InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");

					nodeResponseBody.SelectSingleNode("./tchq:Result", xnsResponse).InnerText = cwHashTable["Query_Result_Code"].ToString();
					nodeResponseBody.SelectSingleNode("./tchq:Carnet_Number", xnsResponse).InnerText = egisRequestLogData.tirCarnetNumber;

					nodeResponseBody.SelectSingleNode("./tchq:HolderID", xnsResponse).InnerText = GetBlankIfNull(cwHashTable["Holder_ID"]);
					if (nodeResponseBody.SelectSingleNode("./tchq:HolderID", xnsResponse).InnerText == "")
					{
						tmpNode1 = nodeResponseBody.SelectSingleNode("./tchq:HolderID", xnsResponse);
						nodeResponseBody.RemoveChild(tmpNode1);
					}

					nodeResponseBody.SelectSingleNode("./tchq:ValidityDate", xnsResponse).InnerText = GetBlankDateIfNull(cwHashTable["Validity_Date"]);
					if (nodeResponseBody.SelectSingleNode("./tchq:ValidityDate", xnsResponse).InnerText == "")
					{
						tmpNode1 = nodeResponseBody.SelectSingleNode("./tchq:ValidityDate", xnsResponse);
						nodeResponseBody.RemoveChild(tmpNode1);
					}

					nodeResponseBody.SelectSingleNode("./tchq:Association", xnsResponse).InnerText = GetBlankIfNull(cwHashTable["Assoc_Short_Name"]);
					if (nodeResponseBody.SelectSingleNode("./tchq:Association", xnsResponse).InnerText == "")
					{
						tmpNode1 = nodeResponseBody.SelectSingleNode("./tchq:Association", xnsResponse);
						nodeResponseBody.RemoveChild(tmpNode1);
					}
					nodeResponseBody.SelectSingleNode("./tchq:NumTerminations", xnsResponse).InnerText = GetBlankIfNull(cwHashTable["No_Of_Terminations"]);
					if (nodeResponseBody.SelectSingleNode("./tchq:NumTerminations", xnsResponse).InnerText == "")
					{
						tmpNode1 = nodeResponseBody.SelectSingleNode("./tchq:NumTerminations", xnsResponse);
						nodeResponseBody.RemoveChild(tmpNode1);
					}
					nodeResponseBody.SelectSingleNode("./tchq:Voucher_Number", xnsResponse).InnerText = GetBlankIfNull(cwHashTable["Voucher_Number"]);
					if (nodeResponseBody.SelectSingleNode("./tchq:Voucher_Number", xnsResponse).InnerText == "")
					{
						tmpNode1 = nodeResponseBody.SelectSingleNode("./tchq:Voucher_Number", xnsResponse);
						nodeResponseBody.RemoveChild(tmpNode1);
					}
					nodeResponseBody.SelectSingleNode("./egis:RequestedGuaranteeNumber", xnsResponse).InnerText = egisRequestLogData.tirCarnetNumber;

					egisRequestLogData.queryResultCode = Int32.Parse(cwHashTable["Query_Result_Code"].ToString());

					egisRequestLogData.holderID = GetBlankIfNull(cwHashTable["Holder_ID"]);
					egisRequestLogData.validityDate = cwHashTable["Validity_Date"];
					egisRequestLogData.assocShortName = GetBlankIfNull(cwHashTable["Assoc_Short_Name"]);
					egisRequestLogData.numberOfTerminations = cwHashTable["No_Of_Terminations"];
					egisRequestLogData.voucherNumber = cwHashTable["Voucher_Number"];


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
					//			<Voucher_Number>0001-2345-6789</Voucher_Number>


				}
				catch (Exception ex)
				{
					iStep35Result = 2;
					egisRequestLogData.lastStep = 35;
					egisRequestLogData.returnCode = 1200;
					sStep35ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep35Result == 1)
					{
						egisDbHelper.LogRequestS35(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS35(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 35, iStep35Result, sStep35ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}

				#endregion

				#region Step 40 - Hash & Encrypt Query Response.

				int iStep40Result = 1;
				string sStep40ResultDesc = "";
				Hashtable htResponse;
				byte[] outputHash = null;
				byte[] a3DesSessionKey = null;
				byte[] aEncResponse = null;


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

					string nsPrefix = RegExHelper.ExtractNameSpacePrefix(sDocwithHash, "http://rts.iru.org/egis");

					sDocwithHash = RegExHelper.SetHASH(sDocwithHash, nsPrefix, hashBodyValue);
					#endregion

					#region - Encrypt Query Response

					htResponse = new Hashtable();
					htResponse["IV"] = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
					byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sDocwithHash);
					aEncResponse = m_iCryptoOperations.Encrypt(aDocWithHash, "3DES", ref htResponse);
					a3DesSessionKey = (byte[])htResponse["KEY"];

					#endregion

					egisRequestLogData.decryptedSessionKeyOut = a3DesSessionKey;
					egisRequestLogData.responseEncryptionResult = 1;

				}
				catch (Exception ex)
				{
					iStep40Result = 8;
					egisRequestLogData.responseEncryptionResult = iStep40Result;
					egisRequestLogData.lastStep = 40;
					egisRequestLogData.returnCode = 1200;
					sStep40ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					egisRequestLogData.responseEncryptionResultDesc = sStep40ResultDesc;
					continueChecking = false;
				}

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep40Result == 1)
					{
						egisDbHelper.LogRequestS40(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS40(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 40, iStep40Result, sStep40ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}

				#endregion

				#region Step 45 - Encrypt Session Key

				int iStep45Result = 1;
				string sStep45ResultDesc = "";
				string encryptionKeyID = "--";
				RSACryptoKey rKey = null;
				//int nRetValue=-1;

				try
				{
					dbHelperSubscriber.ConnectToDB();
					iStep45Result = KeyManager.AssignSubscriberKey(egisRequestLogData.senderID, out rKey, out encryptionKeyID, dbHelperSubscriber);

					if (iStep45Result == 1)
					{
						htResponse = new Hashtable();
						htResponse["EXPONENT"] = rKey.Exponent;
						htResponse["MODULUS"] = rKey.Modulus;
						System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));

						byte[] a3DesEncKey = null; ;
						a3DesEncKey = m_iCryptoOperations.Encrypt(a3DesSessionKey, "RSA", ref htResponse);

						egisRequestLogData.sessionKeyEncryptionKeyIDUsed = encryptionKeyID;
						egisRequestLogData.encryptedSessionKeyOut = a3DesEncKey;
					}
					else
					{
						continueChecking = false;
						egisRequestLogData.responseEncryptionResult = iStep45Result;
						egisRequestLogData.lastStep = 45;
						egisRequestLogData.returnCode = 1200;
						egisRequestLogData.responseEncryptionResultDesc = "";
					}
				}
				catch (Exception ex)
				{
					iStep45Result = 8;
					egisRequestLogData.responseEncryptionResult = iStep45Result;
					egisRequestLogData.lastStep = 45;
					egisRequestLogData.returnCode = 1200;
					sStep45ResultDesc = ex.Message + " - " + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning, ex.Message + " - " + ex.StackTrace);
					egisRequestLogData.responseEncryptionResultDesc = sStep45ResultDesc;
					continueChecking = false;
				}
				finally
				{
					dbHelperSubscriber.Close();
				}

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					if (iStep45Result == 1)
					{
						egisDbHelper.LogRequestS45(egisRequestLogData, false);
					}
					else
					{
						egisDbHelper.LogRequestS45(egisRequestLogData, true);
					}
					//Insert step in Sequence table
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 45, iStep45Result, sStep45ResultDesc, egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}

				#endregion

				#region Step 99 - Confirm End of step & return result

				try
				{
					egisRequestLogData.rowCreationTime = DateTime.Now;
					egisRequestLogData.lastStep = 99;
					egisRequestLogData.returnCode = 2;
					dbHelperEGIS.ConnectToDB();
					dbHelperEGIS.BeginTransaction();
					//Insert step in Sequence table
					egisDbHelper.LogRequestS99(egisRequestLogData);
					egisDbHelper.LogSequenceStep(egisRequestLogData.EGIS_QueryID, 99, 2, "", egisRequestLogData.rowCreationTime);
					dbHelperEGIS.CommitTransaction();
				}
				catch (Exception ex)
				{
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace);
					continueChecking = false;
				}
				finally
				{
					dbHelperEGIS.Close();
				}

				if (!continueChecking)
				{
					EGISResponseType oEGISResponse = new EGISResponseType();
					oEGISResponse.Query_ID = EGISQuery.Query_ID;
					oEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oEGISResponse.MessageTag = "";
					oEGISResponse.EGISResponseParams = null;

					return oEGISResponse;
				}
				else
				{
					EGISResponseType oFinalEGISResponse = new EGISResponseType();

					oFinalEGISResponse.Query_ID = EGISQuery.Query_ID;
					oFinalEGISResponse.ReturnCode = egisRequestLogData.returnCode;
					oFinalEGISResponse.MessageTag = encryptionKeyID;
					oFinalEGISResponse.EGISResponseParams = aEncResponse;
					oFinalEGISResponse.ESessionKey = egisRequestLogData.encryptedSessionKeyOut;

					return oFinalEGISResponse;
				}

				#endregion

			}
		}

		#region Parse EGISQueryXMLErrorString
		private int GetEGISQueryXMLErrorCode(string sValidationResult, out string sNewValidationResult)
		{
			int iEGISQueryInvalidReasonNo = 0;
			string sErrNode = "";
			sNewValidationResult = "";
			if (sValidationResult.Trim().Length > 0)
			{
				string sFind = "EGISQuery:";
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

				iEGISQueryInvalidReasonNo = 1200;
				#region Sample XML
				//			<Query xmlns="http://rts.iru.org/egis">
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
				//				<Carnet_Number>15042217</Carnet_Number>
				//				</Body>
				//			</Query>
				#endregion

				if (sErrNode.Trim().ToUpper() == "ENVELOPE")
				{
					iEGISQueryInvalidReasonNo = 1200;
				}
				else if (sErrNode.Trim().ToUpper() == "SENDER")
				{
					iEGISQueryInvalidReasonNo = 1242;
				}
				else if (sErrNode.Trim().ToUpper() == "SENTTIME")
				{
					iEGISQueryInvalidReasonNo = 1234;
				}
				else if (sErrNode.Trim().ToUpper() == "ORIGINATOR")
				{
					iEGISQueryInvalidReasonNo = 1236;
				}
				else if (sErrNode.Trim().ToUpper() == "ORIGINTIME")
				{
					iEGISQueryInvalidReasonNo = 1237;
				}
				else if (sErrNode.Trim().ToUpper() == "PASSWORD")
				{
					iEGISQueryInvalidReasonNo = 1200;
				}
				else if (sErrNode.Trim().ToUpper() == "QUERY_TYPE")
				{
					iEGISQueryInvalidReasonNo = 1239;
				}
				//else if (sErrNode.Trim().ToUpper() == "QUERY_REASON")
				//{
				//	iEGISQueryInvalidReasonNo = 1240;
				//}
				else if (sErrNode.Trim().ToUpper() == "CARNET_NUMBER")
				{
					iEGISQueryInvalidReasonNo = 1241;
				}

				if (pos1 >= 0)
				{
					sNewValidationResult = sValidationResult + " (Node:" + sErrNode + " - ErrorCode:" + iEGISQueryInvalidReasonNo.ToString().Trim() + ")";
				}
			}
			return iEGISQueryInvalidReasonNo;
		}
		#endregion
	}
}