using System.Xml.Serialization;

namespace RTSDotNETClient
{
    public enum ReturnCode
    {
        [XmlEnum("2")]
        SUCCESS = 2,
        [XmlEnum("1200")]
        UNCLASSIFIED_ERROR = 1200,
        [XmlEnum("1210")]
        MISSING_MESSAGE_TAG = 1210,
        [XmlEnum("1211")]
        UNKNOWN_MESSAGE_TAG = 1211,
        [XmlEnum("1212")]
        INVALID_SUBSCRIBER_ID = 1212,
        [XmlEnum("1213")]
        MISSING_ESESSIONKEY = 1213,
        [XmlEnum("1214")]
        MISSING_PAYLOAD = 1214,
        [XmlEnum("1222")]
        INVALID_MESSAGEID = 1222,
        [XmlEnum("1223")]
        INVALID_INFORMATION_EXCHANGE_VERSION = 1223,
        [XmlEnum("1230")]
        ESESSIONKEY_DECRYPTION = 1230,
        [XmlEnum("1231")]
        PAYLOAD_DECRYPTION = 1231,
        [XmlEnum("1232")]
        INVALID_QUERYID = 1232,
        [XmlEnum("1233")]
        INVALID_PASSWORD = 1233,
        [XmlEnum("1234")]
        INVALID_SENDTIME = 1234,
        [XmlEnum("1236")]
        INVALID_ORIGINATOR = 1236,
        [XmlEnum("1237")]
        INVALID_ORIGINTIME = 1237,
        [XmlEnum("1239")]
        INVALID_QUERYTYPE = 1239,
        [XmlEnum("1240")]
        INVALID_QUERYREASON = 1240,
        [XmlEnum("1241")]
        INVALID_CARNETNUMBER = 1241,
        [XmlEnum("1242")]
        INVALID_SENDERID = 1242,
        [XmlEnum("1250")]
        DATABASE_QUERY_TIMEOUT = 1250
    }

}
