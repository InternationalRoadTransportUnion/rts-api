using System;
using System.Xml;
using System.Xml.Serialization;

namespace RTSDotNETClient.WSRQ
{

    /// <summary>
    /// Type of the query
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// All new requests
        /// </summary>
        [XmlEnum("1")]
        AllNewRequests = 1
    }

    /// <summary>
    /// Query to be sent to the WSRQ Web Service to download the pending reconciliation requests
    /// </summary>
    [XmlRoot("ReconciliationQuery", Namespace = "http://www.iru.org/SafeTIRReconciliation")]
    public class Query : BaseQueryResponse
    {
        /// <summary>
        /// The xml schema definition file that defines the structure of the query
        /// </summary>
        public const string Xsd = "SafeTIRReconciliation.xsd";

        /// <summary>
        /// Customs Authorities Internal Value, not used by the IRU
        /// </summary>
        [XmlAttribute("Sender_Document_Version")]
        public string SenderDocumentVersion { get; set; }

        /// <summary>
        /// The query body
        /// </summary>
        public Body Body = new Body();        

        /// <summary>
        /// The default constructor
        /// </summary>
        public Query()
        {
            this.xsd = Xsd;
        }
    }

    /// <summary>
    /// The query body
    /// </summary>
    public class Body
    {
        /// <summary>
        /// The date and time the query was sent. Time in UTC. See ISO 8601
        /// </summary>
        public DateTime SentTime { get; set; }

        /// <summary>
        /// Password of the sender
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Type of the query
        /// </summary>
        public QueryType QueryType { get; set; }
    }

}
