using System;


namespace IRU.RTS.WSEGIS
{
    public class TIRCarnetHolderQueryParamsStruct
    {
        public byte[] Signature;
        public string KeyId;

        public string Sender;
        public DateTime SentTime;
        public string Originator;
        public DateTime OriginTime;
        public string Password;
        public int	Query_Type;
        public string Carnet_Number; //Digits & Number
    }

    public class EGISLogRequestStruct
    {
        public double       EGIS_QueryID;
        public string       decryptionKeyID; //MessageTag - Key ID Used to encrypt by FCS
        public byte []      encryptedQueryParams;
        public String       senderTCPAddress;
        public Int32        queryParamValidResult;
        public Int32        decryptionResult;
        public String       decryptionResultDesc;
        public String       decryptedQueryParamXML;
        public byte []      encryptedSessionKeyIn;
        public byte []      decryptedSessionKeyIn;
        //public Byte[]     decryptionKeyUsed;
        //public Byte[]     signatureKeyUsed;
        public Boolean      validQueryXML;
        public String       invalidQueryXMLReason;
        public String       senderID;
        public String       originatorID;
        public DateTime     originTime;
        public String       senderPassword;
        public Int32        queryType; 
        //public Int32      queryReason;
        public String       tirCarnetNumber;
        public String       senderQueryID; 
        public Int32        senderAuthenticated; 
        public Int32        queryResultCode;
        public String       holderID;
        public object       validityDate;	//to store null value
        public String       assocShortName;
        public object       numberOfTerminations; // to store null values
        public object       voucherNumber; // to store null values
        public DateTime     rowCreationTime;
        public String       sessionKeyEncryptionKeyIDUsed;
        public byte []      encryptedSessionKeyOut;
        public byte []      decryptedSessionKeyOut;
        public int          responseEncryptionResult;
        public String       responseEncryptionResultDesc;
        public int          returnCode;
        public int          lastStep;
        public String       decryptedQueryResultXML;
    }

    public class SubscriberDetailsStruct
    {
        public String	subscriberID;
        public String	password;
        public String	HashAlgo;
        public String	SessionKeyAlgo;
        public String	CopyToId;
        public String	CopyToAddress;
    }

    public class KeyInformationStruct
    {
        public string sServiceID;
        public string sEncryptKeyID;
        public long lKeyIndexUsed;
        public byte [] byEncryption;
        public byte [] bySignature;
        public string sSubscriberID;
        public int iKeyStatus; 
    }
}