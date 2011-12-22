using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RTSDotNETClient.WSRE
{

    public enum RequestReplyType
    {
        [XmlEnum("0")]
        NotSpecified = 0,
        [XmlEnum("1")]
        DataSentWithThisReplyIsTheCorrectData=1,
        [XmlEnum("2")]
        DataSentBeforeShouldBeDeleted=2,
        [XmlEnum("3")]
        NoTerminationDataAvailable=3
    }

    [XmlRoot("SafeTIR", Namespace = "http://www.iru.org/SafeTIRUpload")]
    public class Query : BaseQueryResponse
    {
        public Query() 
        {
            Xsd = "SafeTIRUpload.xsd";
        }
        public Body Body = new Body();

    }

    public class Body
    {
        public int NumberOfRecords { get; set; }
        public List<RequestReplyRecord> RequestReplyRecords { get; set; }
    }

    public class RequestReplyRecord
    {
        [XmlAttribute("RequestID")]
        public string RequestID { get; set; }
        [XmlAttribute("RequestReplyType")]
        public RequestReplyType RequestReplyType { get; set; }
        [XmlAttribute("TNO")]
        public string TNO { get; set; }
        [XmlAttribute("ICC")]
        public string ICC { get; set; }
        [XmlAttribute("DCL", DataType = "date")]
        public DateTime DCL { get; set; }
        [XmlAttribute("CNL")]
        public string CNL { get; set; }
        [XmlAttribute("COF")]
        public string COF { get; set; }
        [XmlAttribute("DDI", DataType = "date")]
        public DateTime DDI { get; set; }
        [XmlAttribute("RND")]
        public string RND { get; set; }
        [XmlAttribute("PFD")]
        public PFD PFD { get; set; }
        [XmlAttribute("CWR")]
        public CWR CWR { get; set; }
        [XmlAttribute("VPN")]
        public int VPN { get; set; }
        [XmlAttribute("COM")]
        public string COM { get; set; }
        [XmlAttribute("RBC")]
        public RBC RBC { get; set; }
        [XmlAttribute("PIC")]
        public int PIC { get; set; }
    }

}
