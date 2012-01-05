using System.Xml.Serialization;

namespace RTSDotNETClient
{
    /// <summary>
    /// The return codes returned by the web services
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// Success
        /// </summary>
        [XmlEnum("2")]
        SUCCESS = 2,

        /// <summary>
        /// Any unclassified error
        /// </summary>
        [XmlEnum("1200")]
        UNCLASSIFIED_ERROR = 1200,
        
        /// <summary>
        /// No Message Tag Value
        /// </summary>
        [XmlEnum("1210")]
        MISSING_MESSAGE_TAG = 1210,
        
        /// <summary>
        /// No Certificate found corresponding to the MessageTag
        /// </summary>
        [XmlEnum("1211")]
        UNKNOWN_MESSAGE_TAG = 1211,
        
        /// <summary>
        /// Subscriber ID Missing or Bad formatted or not registered
        /// </summary>
        [XmlEnum("1212")]
        INVALID_SUBSCRIBER_ID = 1212,
        
        /// <summary>
        /// Session key missing
        /// </summary>
        [XmlEnum("1213")]
        MISSING_ESESSIONKEY = 1213,
        
        /// <summary>
        /// Encrypted data missing
        /// </summary>
        [XmlEnum("1214")]
        MISSING_PAYLOAD = 1214,
        
        /// <summary>
        /// Value is more than maximum length specified or Missing
        /// </summary>
        [XmlEnum("1222")]
        INVALID_MESSAGEID = 1222,
        
        /// <summary>
        /// Version is not valid
        /// </summary>
        [XmlEnum("1223")]
        INVALID_INFORMATION_EXCHANGE_VERSION = 1223,
        
        /// <summary>
        /// Cannot Decrypt the encrypted session key
        /// </summary>
        [XmlEnum("1230")]
        ESESSIONKEY_DECRYPTION = 1230,
        
        /// <summary>
        /// Error Decrypting the encrypted TIRCarnetHolderQuery
        /// </summary>
        [XmlEnum("1231")]
        PAYLOAD_DECRYPTION = 1231,
        
        /// <summary>
        /// Value is more than maximum length specified
        /// </summary>
        [XmlEnum("1232")]
        INVALID_QUERYID = 1232,
        
        /// <summary>
        /// Authorization Failure
        /// </summary>
        [XmlEnum("1233")]
        INVALID_PASSWORD = 1233,
        
        /// <summary>
        /// Query Time in wrong format or value missing
        /// </summary>
        [XmlEnum("1234")]
        INVALID_SENDTIME = 1234,
        
        /// <summary>
        /// Originator Code missing or has wrong format
        /// </summary>
        [XmlEnum("1236")]
        INVALID_ORIGINATOR = 1236,
        
        /// <summary>
        /// Origin Time in wrong format or value missing
        /// </summary>
        [XmlEnum("1237")]
        INVALID_ORIGINTIME = 1237,
        
        /// <summary>
        /// Query Type is not specified or has wrong value
        /// </summary>
        [XmlEnum("1239")]
        INVALID_QUERYTYPE = 1239,
        
        /// <summary>
        /// Query Reason is not specified or has wrong value
        /// </summary>
        [XmlEnum("1240")]
        INVALID_QUERYREASON = 1240,
        
        /// <summary>
        /// TIR Carnet number is not specified or wrong format
        /// </summary>
        [XmlEnum("1241")]
        INVALID_CARNETNUMBER = 1241,
        
        /// <summary>
        /// SenderID is not registered or missing
        /// </summary>
        [XmlEnum("1242")]
        INVALID_SENDERID = 1242,
        
        /// <summary>
        /// Service unavailable. Please try later.
        /// </summary>
        [XmlEnum("1250")]
        DATABASE_QUERY_TIMEOUT = 1250
    }

}
