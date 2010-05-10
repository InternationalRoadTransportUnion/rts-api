using System;
using System.Collections.Generic;
using System.Text;



namespace IRU.RTS.WSWSRQ
{
    //fields which map to the WSRQ_LOG table.
    public class WSRQLogQueryData
    {
        public double Exchange_ID;
        public string decryptionKeyID; //MessageTag - Key ID Used to encrypt by FCS
        public byte[] encryptedQueryParams;
        public String senderTCPAddress;
        public Int32 queryParamValidResult;
        public Int32 decryptionResult;
        public String decryptionResultDesc;
        public String decryptedQueryParamXML;
        public byte[] encryptedSessionKeyIn;
        public byte[] decryptedSessionKeyIn;
        //public Byte[]		decryptionKeyUsed;
        //public Byte[]		signatureKeyUsed;
        public Boolean validQueryXML;
        public String invalidQueryXMLReason;
        public String senderID;
        public string Information_Exchange_Version;
        public DateTime originTime;
        public String senderPassword;
        public Int32 queryType;
        public String Sender_MessageID;
        public Int32 senderAuthenticated;
        public int returnCode;
        public Int32 queryResultCode;
        public int No_Of_Requests_Sent;
        public int responseEncryptionResult;
        public String responseEncryptionResultDesc;
        public byte[] decryptedSessionKeyOut;
        public byte[] encryptedSessionKeyOut;
        public String sessionKeyEncryptionKeyIDUsed;
        public DateTime rowCreationTime;
        public int lastStep;

    }

    public class WSRQLogRequestStruct
    {
        public long NumberOfRecords;
        public Array RequestRecords;
    }


    public class WSRQLogRequestRecordsStruct
    {
	    public string		RequestId ;//( 14 characters)
	    public DateTime	RequestDate;
	    public int RequestReminderNum; //+ve integer
	    public int	RequestDataSource;
	    public string TNO;
        public string ICC;
        public string DCL;
        public string CNL;
        public string COF;
        public string DDI;
        public string	RND;
        public string	PFD;
        public string	CWR;
        public string	VPN; //2,4,6,8,10,12,14,16,18 or 20
        public string	COM; 
        public string	RBC; //CR,CNR,VR,VNR
        public int		PIC; // non negative integer
        public string	RequestRemark;
        
     }

    public class WSRQXMLRequestRecordsStruct
    {
        public string RequestRecordString;     

    }


     public struct  ReconciliationQueryData
	 {	
	    public DateTime SentTime;
		public string Password;		
		public int QueryType;
		public string Sender_Document_Version;		
	 }

     public class SubscriberDetailsStruct
     {
        public String subscriberID;
        public String password;
        public String HashAlgo;
        public String SessionKeyAlgo;
        public String CopyToId;
        public String CopyToAddress;
     }

    public class KeyInformationStruct
    {
        public string sServiceID;
        public string sEncryptKeyID;
        public long lKeyIndexUsed;
        public byte[] byEncryption;
        public byte[] bySignature;
        public string sSubscriberID;
        public int iKeyStatus;
    }


  
}


