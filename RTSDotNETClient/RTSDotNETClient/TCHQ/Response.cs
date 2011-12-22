using System;
using System.Xml;
using System.Xml.Serialization;

namespace RTSDotNETClient.TCHQ
{

    public enum ResponseResult
    {
        [XmlEnum("1")]
        IssueDataAvailable = 1,
        [XmlEnum("2")]
        NoIssueDataAvailable = 2,
        [XmlEnum("3")]
        CarnetHasBeenInvalidatedByTheIRU = 3,
        [XmlEnum("4")]
        CarnetIsNotInCirculation = 4,
        [XmlEnum("5")]
        CarnetNumberIsIncorrect = 5
    }

    [XmlRoot("QueryResponse", Namespace = "http://www.iru.org/TCHQResponse")]
    public class Response : BaseQueryResponse
    {                
        public ResponseBody Body = new ResponseBody();

        public Response()
        {
            Xsd = "TCHQResponse.xsd";
        }
    }

    public class ResponseBody
    {
        public string Sender{ get; set; }
        public string Originator{ get; set; }
        public DateTime ResponseTime{ get; set; }        
        public ResponseResult Result{ get; set; }        
        [XmlElement("Carnet_Number")]
        public string CarnetNumber { get; set; }
        public string HolderID { get; set; }
        public DateTime ValidityDate { get; set; }
        public string Association { get; set; }
        public int NumTerminations { get; set; }        
    }
    
}
