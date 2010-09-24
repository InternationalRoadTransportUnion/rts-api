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

namespace IRU.RTS.TIREPD
{
    [Serializable] 
    public class B2GSender : MarshalByRefObject, IB2GSender
    {
        private ICryptoOperations m_iCryptoOperations;

        private string m_sB2G_Sender_Id;
        public B2GSender()
        {
            m_sB2G_Sender_Id = "B2G_Message_ID";
        }
        public bool SendEPDFile(String sDocSend, string ISOCode, string LRN, out string sDocResponse)
        {
            //convert the receiving string to xml
            XmlDocument xDoc = new XmlDocument();
            XmlDocument xDocResponse = new XmlDocument();
            XmlDocument xDocResponse1 = new XmlDocument();
             string sErrMessage = "";
            try
            {
                xDocResponse.Load(B2G_RemotingHelper.m_SchemaFilesPath + "\\EPD_RTS_Response.xml");

                TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadParams su = new TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadParams();
                su = PrepareMessage(sDocSend, LRN, ISOCode, ref sErrMessage);

                xDoc.LoadXml(sDocSend);
                xDocResponse1 = xDoc;

                XmlNode node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/SubscriberID");
                node1.InnerText = su.SubscriberID;
                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/MessageId");
                node1.InnerText = su.SubscriberMessageID;

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/TimeSent");
                node1.InnerText = su.TimeSent.ToString("yyyy-MM-ddTHH:mm:ss");

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/InformationExchangeVersion");
                node1.InnerText = su.InformationExchangeVersion;

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/EncrypTedMessage");
                node1.InnerText =  System.Text.Encoding.Unicode.GetString(su.MessageContent);

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/CertificateId");
                node1.InnerText = su.CertificateID;

                TIREPDB2GSender.TIREPDB2GService.TIREPDB2GServiceClass uploadClass = new TIREPDB2GSender.TIREPDB2GService.TIREPDB2GServiceClass();
                TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadAck tr;
                uploadClass.Timeout = 10000000;
                tr = uploadClass.TIREPDB2G(su);

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/Response/HostId");
                node1.InnerText = tr.HostID;

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/Response/ResponseCode");
                node1.InnerText = tr.ReturnCode.ToString();

                node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/Response/SubscriberMessageId");
                node1.InnerText = tr.SubscriberMessageID;

            }
            catch(Exception ex) 
            {
                XmlNode node1 = xDocResponse.SelectSingleNode("EPDRTSReponse/Response/ErrorMessage");
                node1.InnerText = ex.Message + " - " + " - " + ex.StackTrace +" --sDocSend: "+  sDocSend;
                sDocResponse = xDocResponse.OuterXml;
                System.Diagnostics.EventLog.WriteEntry("TIREPDB2G", node1.InnerText, System.Diagnostics.EventLogEntryType.Error);
                return false;
             }
             sDocResponse = xDocResponse.OuterXml;
            return true;
        }
        public TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadParams PrepareMessage(string sXmlIE15, string LRN, string sISO,ref string sMessage)
        {
            int iResult = 1;
            #region Get IDBHelper instances from Plugin Manager
            IDBHelper dbHelperEPD_RTS = B2G_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TIREPD_DB");//  null; //dbhelper for tchqdb
            IDBHelper dbHelperSubscriber = B2G_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("SubscriberDB");//  null; //dbhelper for tchqdb
            #endregion

            #region initiliaze Variables
            TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadParams su = new TIREPDB2GSender.TIREPDB2GService.TIREPDB2GUploadParams();

            B2G_DBHelper tchqDbHelper = new B2G_DBHelper(dbHelperEPD_RTS);
            Subscriber_DBHelper subsDbHelper = new Subscriber_DBHelper(dbHelperSubscriber);
            if (!B2G_RemotingHelper.m_hsCountryISO_Subsc_Msg_Info.ContainsKey(sISO))
            {
                sMessage = "ISO Code:" + sISO + " not found in the config file.";
                throw new ApplicationException(sMessage);
            }
            string SubscriberID = ((string[])(B2G_RemotingHelper.m_hsCountryISO_Subsc_Msg_Info[sISO]))[1]; //"FCS";
            string sMessageName = ((string[])(B2G_RemotingHelper.m_hsCountryISO_Subsc_Msg_Info[sISO]))[2];//"TIRPreDeclaration";
            string sMessageExchangeVersion = ((string[])(B2G_RemotingHelper.m_hsCountryISO_Subsc_Msg_Info[sISO]))[3]; //"1.0.0";

            SubscriberDetailsStruct subscriberDetails = new SubscriberDetailsStruct();
            RSACryptoKey rKey = null;
            string encryptionKeyID="--";


            Hashtable htResponse;
            byte[] a3DesSessionKey = null;
            byte[] aEncResponse = null;

            #endregion

            try
            {
                m_iCryptoOperations = (ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations), B2G_RemotingHelper.m_CryptoProviderEndpoint);

                dbHelperSubscriber.ConnectToDB();
                iResult = KeyManager.AssignSubscriberKey(SubscriberID, out rKey, out encryptionKeyID, dbHelperSubscriber);
                if (iResult == 1)
                {
                    #region - Encrypt Query Response

                    htResponse = new Hashtable();
                    htResponse["IV"] = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
                    byte[] aDocWithHash = System.Text.Encoding.Unicode.GetBytes(sXmlIE15);
                    aEncResponse = m_iCryptoOperations.Encrypt(aDocWithHash, "3DES", ref htResponse);
                    a3DesSessionKey = (byte[])htResponse["KEY"];
                    #endregion

					htResponse = new Hashtable();
					htResponse["EXPONENT"]= rKey.Exponent;
					htResponse["MODULUS"] = rKey.Modulus;
					System.Diagnostics.Debug.WriteLine(" After Key Fetch " + DateTime.Now.ToString("HH:mm:ss:fff"));

					byte[] a3DesEncKey = null; ;
					a3DesEncKey  = m_iCryptoOperations.Encrypt(a3DesSessionKey,"RSA",ref htResponse);

                    #region Assign Values to the send Struct
                    su.SubscriberID = SubscriberID;
                    su.MessageName = sMessageName;
                    su.SubscriberMessageID = LRN;
                    su.TimeSent = DateTime.Now;
                    su.MessageContent = aEncResponse; 
					su.CertificateID = encryptionKeyID;
					su.ESessionKey = a3DesEncKey;
                    su.InformationExchangeVersion = sMessageExchangeVersion;
                    #endregion

                }
            }
            catch(Exception ex)
            {
                sMessage = ex.Message + " - " + ex.StackTrace; 
            }

            return su;
        }
    }
}
