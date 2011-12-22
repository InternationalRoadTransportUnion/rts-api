using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RTSDotNETClient.WSRQ
{
    public enum RequestDataSource
    {
        [XmlEnum("1")]
        SafeTIRTransmission=1,
        [XmlEnum("2")]
        TirCarnet=2
    }

    [XmlRoot("SafeTIR", Namespace = "http://www.iru.org/SafeTIRReconciliation")]
    public class Response : BaseQueryResponse
    {
        public ResponseBody Body = new ResponseBody();

        public Response()
        {
            Xsd = "SafeTIRReconciliation.xsd";
        }
    }

    public class ResponseBody
    {
        public int NumberOfRecords { get; set; }
        public List<RequestRecord> RequestRecords { get; set; }
    }

    public class RequestRecord
    {
        [XmlAttribute("RequestID")]
        public string RequestID {get;set;}
        [XmlAttribute("RequestDate")]
        public DateTime RequestDate {get;set;}
        [XmlAttribute("RequestReminderNum")]
        public int  RequestReminderNum {get;set;}
        [XmlAttribute("RequestDataSource")]
        public int RequestDataSource{get;set;}
        [XmlAttribute("TNO")]
        public string TNO { get; set; }
        [XmlAttribute("ICC")]
        public string ICC { get; set; }
        [XmlAttribute("DCL")]
        public DateTime DCL { get; set; }
        [XmlAttribute("CNL")]
        public string CNL { get; set; }
        [XmlAttribute("COF")]
        public string COF { get; set; }
        [XmlAttribute("DDI")]
        public DateTime DDI { get; set; }
        [XmlAttribute("RND")]
        public string RND { get; set; }
        [XmlAttribute("PFD")]
        public PFD PFD { get; set; }
        [XmlAttribute("CWR")]
        public CWR CWR {get;set;}
        [XmlAttribute("VPN")]
        public int VPN { get; set; }
        [XmlAttribute("COM")]
        public string COM { get; set; }
        [XmlAttribute("RBC")]
        public RBC RBC { get; set; }
        [XmlAttribute("PIC")]
        public int PIC { get; set; }
        [XmlAttribute("RequestRemark")]
        public string RequestRemark { get; set; }
    }
}
