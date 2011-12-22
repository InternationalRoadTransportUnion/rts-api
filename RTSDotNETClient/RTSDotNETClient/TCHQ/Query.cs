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

    public enum QueryType
    {
        [XmlEnum("1")]
        CarnetHolder = 1
    }

    public enum QueryReason
    {
        [XmlEnum("1")]
        Entry = 1,
        [XmlEnum("2")]
        Exit = 2,
        [XmlEnum("3")]
        Termination = 3,
        [XmlEnum("4")]
        Opening = 4,
        [XmlEnum("5")]
        Other = 5
    }

    [XmlRoot("Query", Namespace = "http://www.iru.org/TCHQuery")]
    public class Query : BaseQueryResponse
    {
        public Query() 
        {
            Xsd = "TCHQuery.xsd";
        }
        public Body Body = new Body();

    }

    public class Body
    {
        public string Sender{ get; set; }
        public DateTime SentTime{ get; set; }
        public string Originator{ get; set; }
        public DateTime OriginTime{ get; set; }       
        public string Password{ get; set; }
        [XmlElement("Query_Type")]
        public QueryType QueryType{ get; set; }
        [XmlElement("Query_Reason")]
        public QueryReason QueryReason{ get; set; }
        [XmlElement("Carnet_Number")]
        public string CarnetNumber { get; set; }


    }
    
}
