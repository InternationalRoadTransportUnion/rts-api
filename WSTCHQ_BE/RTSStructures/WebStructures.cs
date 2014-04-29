using System;
using System.Runtime.Serialization;

namespace IRU.RTS
{
	/// <summary>
	/// This Structure which holds the Response From the TIR Carnet Query 
	/// Processing Engine called by the Web Service Method
	/// public string WSTCHQ_MessageTag;
	/// <summary>
	/// Holds the Encrpyted Query Result Sent back to the Web Service Client
	/// Data Type is Array of Bytes.
	/// public string WSTCHQ_MessageTag contains the DB Key which identifies the Encryption Key used from the Database.
	/// public byte [] TIRCarnetHolderResponseParams contains the byte array containing the Encrypted Output of the Query Fired.
	/// public int ReturnCode will contain 2 if the OK else 1200 if anything went wrong during the Query Processing
	/// public string Query_ID will contain the Query ID which will allow the query initiator to track the Query. This is Optional.
	/// </summary>
	[Serializable]
	public struct TIRHolderResponse
	{		
		public string MessageTag;	
		public byte [] ESessionKey;
		public byte [] TIRCarnetHolderResponseParams;		
		public int    ReturnCode;		
		public string Query_ID;
	}


	/// <summary>
	/// This Structure holds query parameters for the TIR Carnet Query 
	/// which are sent to the Query Processing Engine called by the Web Service Method.
	/// public string WSCTHQ_MessageTag contains the DB Key which identifies the Encryption Key used from the Database
	/// public byte [] TIRCarnetHolderQueryParams will contain the byte array containing the Encrpyted Parameters for Query.
	/// public string Query_ID will contain the Query ID which will uniquely identify the query being Fired. This is Optional.
	/// </summary>
	[Serializable]
	public struct  TIRHolderQuery
	{			
		public string SubscriberID;
		public string MessageTag;	
		public byte [] ESessionKey;
		public byte [] TIRCarnetHolderQueryParams;		
		public string Query_ID;
	}


	[Serializable]
	public struct  SafeTIRUploadParams
	{
		public string SubscriberID;
		public string MessageTag;
		public byte[] ESessionKey;
		public byte[] safeTIRUploadData;
		public string CopyToID;
		public string Sender_MessageID;
	}

	[Serializable]
	public struct SafeTIRUploadAck
	{
		public string Version;
		public string Sender;
		public DateTime ResponseTime;
		public int    ReturnCode;
		public string Sender_MessageID;
	}


	[Serializable]
	public struct  SafeTIRUploadCopy
	{
		public string MessageTag;
		public byte[] ESessionKey;
		public byte[] safeTIRUploadData;
		public string CopyToID;
		public string CopyFromID;
		public string Sender_MessageID;
	}

	[Serializable]
	public struct SafeTIRUploadCopyAck
	{
		public int ReturnCode;
	}



	/// <summary>
	/// This holds the data for the subscriber of the Services
	/// It will contain four parameters which are as follows:
	/// public string subscriberID will contain the Subscribers ID
	/// public string subscriberPassword will contain the Subscribers Password
	/// public string subscriberDescription will Contain the Subscirbers Description
	/// </summary>		
	/// 	
	public struct Subscriber
	{
		public string subscriberID;
		public string subscriberPassword;
		public string subscriberDescription;
	}

	/// <summary>
	/// This holds the data for the subscriber of the Services
	/// It will contain four parameters which are as follows:
	/// public string subscriberID will contain the Subscribers ID
	/// public int serviceID will contain the Service ID which the Subscriber can use
	/// public int active will Contain the status whether the subscriber can use this Service or not.
	/// </summary>		
	public struct SubscriberServices
	{
		public string subscriberID;
		public int serviceID;
		public int active;
	}
	/// <summary>
	/// This holds the data for the subscriber of the Services
	/// It will contain four parameters which are as follows:
	/// public string subscriberID will contain the Subscribers ID
	/// public int serviceID  will contain the Service ID which the Subscriber can use
	/// public int methodID will contain the Method which the subscriber can use from the service identified by the above mentioned service ID.
	/// public int active will contain the status whether the subscriber can use the method of the service identified by the Service ID and the method id mentioned above.
	/// </summary>		
	public struct SubscriberServiceMethod
	{
		public string subscriberID;
		public int serviceID;
		public int methodID;
		public int active;
	}

    
    /// <summary>
    /// Lata changed for WSRQ on Sept05,2007
    /// </summary>
    [Serializable]
    public struct ReconciliationQuery
    {	
	    public string MessageTag;
		public string SubscriberID;
        public byte[] ESessionKey;
		public byte [] ReconciliationQueryData;		
		public string Sender_MessageID;
		public string Information_Exchange_Version;
	}

    /// <summary>
    ///Lata changed for WSRQ on Sept05,2007
    /// </summary>
    [Serializable]
    public struct ReconciliationResponse
    {
	    public string Sender; 
	    public string MessageTag;
	    public byte[] ESessionKey;
	    public byte[] ReconciliationRequestData;
	    public string Sender_MessageID;
	    public int    ReturnCode;
    }


    /// <summary>
    ///Lata changed for WSRE on Sept05,2007
    /// </summary>
    [Serializable]
    public struct SafeTIRReconParams//ReconciliationReply //SafeTIRReconParams
    {
        public string SubscriberID;
	    public string MessageTag;
	    public byte[] ESessionKey;
	    public byte[] SafeTIRReconData;
 	    public string Sender_MessageID;
 	    public string Information_Exchange_Version;
    }


    ///// <summary>
    /////Lata changed for WSRE on Sept05,2007
    ///// </summary>
    //[Serializable]
    //public struct ReconciliationReplyAck
    //{
    //    public DateTime SentTime;
    //    public DateTime ResponseTime;
    //    public int ReturnCode;
    //    public string MessageID;
    //}


    /// <summary>
    /// VoucherQuery - TVQR method #1: query
    /// </summary>
    [Serializable]
    public struct VoucherQueryType
    {
        public string SubscriberID;
        public string MessageTag;
        public byte[] ESessionKey;
        public byte[] VoucherQueryParams;
        public string Query_ID;
    }

    /// <summary>
    /// VoucherQuery - TVQR method #1: response
    /// </summary>
    [Serializable]
    public struct VoucherQueryResponseType
    {
        public string MessageTag;
        public byte[] ESessionKey;
        public byte[] VoucherQueryResponseParams;
        public int ReturnCode;
        public string Query_ID;
    }

    /// <summary>
    /// VoucherRegistration - TVQR method #2: query
    /// </summary>
    [Serializable]
    public struct VoucherRegistrationType
    {
        public string SubscriberID;
        public string MessageTag;
        public byte[] ESessionKey;
        public byte[] VoucherRegistrationParams;
        public string Sender_MessageID;
    }

    /// <summary>
    /// VoucherRegistration - TVQR method #2: response
    /// </summary>
    [Serializable]
    public struct VoucherRegistrationResponseType
    {
        public string MessageTag;
        public byte[] ESessionKey;
        public byte[] VoucherRegistrationResponseParams;
        public int ReturnCode;
        public string Sender_MessageID;
    }
}

