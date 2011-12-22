using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Reflection;

namespace RTSDotNETClient.WSRQ
{

    public enum QueryType
    {
        [XmlEnum("1")]
        AllNewRequests = 1
    }

    [XmlRoot("ReconciliationQuery", Namespace = "http://www.iru.org/SafeTIRReconciliation")]
    public class Query : BaseQueryResponse
    {                
        public Body Body = new Body();

        public Query()
        {
            Xsd = "SafeTIRReconciliation.xsd";
        }
    }

    public class Body
    {
        public DateTime SentTime { get; set; }
        public string Password { get; set; }
        public QueryType QueryType { get; set; }
    }

}
