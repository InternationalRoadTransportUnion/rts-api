using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Reflection;

namespace RTSDotNETClient.TCHQ
{

    /// <summary>
    /// Type of the query
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// Carnet Holder
        /// </summary>
        [XmlEnum("1")]
        CarnetHolder = 1
    }

    /// <summary>
    /// Reason for the query
    /// </summary>
    public enum QueryReason
    {
        /// <summary>
        /// Entry
        /// </summary>
        [XmlEnum("1")]
        Entry = 1,

        /// <summary>
        /// Exit
        /// </summary>
        [XmlEnum("2")]
        Exit = 2,
        
        /// <summary>
        /// Termination
        /// </summary>
        [XmlEnum("3")]
        Termination = 3,
        
        /// <summary>
        /// Opening
        /// </summary>
        [XmlEnum("4")]
        Opening = 4,
        
        /// <summary>
        /// Other
        /// </summary>
        [XmlEnum("5")]
        Other = 5
    }

    /// <summary>
    /// The TIR Carnet Holder Query to be sent to the TCHQ Web Service
    /// </summary>
    [XmlRoot("Query", Namespace = "http://www.iru.org/TCHQuery")]
    public class Query : BaseQueryResponse
    {
        /// <summary>
        /// The xml schema definition file that defines the structure of the query
        /// </summary>
        public const string Xsd = "TCHQuery.xsd";
        
        /// <summary>
        /// The body of the query
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
        /// The identification of the sender.
        /// </summary>
        public string Sender{ get; set; }

        /// <summary>
        /// The date and time the query was sent to the WSTCHQ. Time in UTC. See ISO 8601.
        /// </summary>
        public DateTime SentTime{ get; set; }

        /// <summary>
        /// The identification of the customs post which generated the query.
        /// </summary>
        public string Originator{ get; set; }

        /// <summary>
        /// The date and time the Originator created the query. Time in UTC. See ISO 8601.
        /// </summary>
        public DateTime OriginTime{ get; set; }       

        /// <summary>
        /// Password of the sender
        /// </summary>
        public string Password{ get; set; }

        /// <summary>
        /// Type of the query
        /// </summary>
        [XmlElement("Query_Type")]
        public QueryType QueryType{ get; set; }

        /// <summary>
        /// Reason for the query
        /// </summary>
        [XmlElement("Query_Reason")]
        public QueryReason QueryReason{ get; set; }

        /// <summary>
        /// The Carnet Number
        /// </summary>
        [XmlElement("Carnet_Number")]
        public string CarnetNumber { get; set; }

    }
    
}
