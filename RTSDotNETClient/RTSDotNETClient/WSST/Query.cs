using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RTSDotNETClient.WSST
{

    public enum UPG
    {
        [XmlEnum("N")]
        New,
        [XmlEnum("CancelDelete")]
        CancelDelete
    }

    public enum UploadType
    {
        [XmlEnum("1")]
        DataUpload=1
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
        public string Version{ get; set; }
        public string SubscriberID { get; set; }
        public string Password{ get; set; }
        public UploadType UploadType { get; set; }       
        public int TCN{ get; set; }        
        public DateTime SentTime { get; set; }
        [XmlElement("Sender_MessageID")]
        public string SenderMessageID { get; set; }
        public List<Record> SafeTIRRecords { get; set; }
    }

    public class Record
    { 
        [XmlAttribute("TNO")]
        public string TNO { get; set; }
        [XmlAttribute("ICC")]
        public string ICC {get;set;}
        [XmlAttribute("DCL", DataType = "date")]
        public DateTime DCL {get;set;}
        [XmlAttribute("CNL")]
        public string CNL { get; set; }
        [XmlAttribute("COF")]
        public string COF { get; set; }
        [XmlAttribute("DDI", DataType="date")]
        public DateTime DDI { get; set; }
        [XmlAttribute("RND")]
        public string RND {get;set;}
        [XmlAttribute("PFD")]
        public PFD PFD {get;set;}
        [XmlAttribute("CWR")]
        public CWR CWR {get;set;}
        [XmlAttribute("VPN")]
        public int VPN {get;set;}
        [XmlAttribute("COM")]
        public string COM {get;set;}
        [XmlAttribute("RBC")]
        public RBC RBC { get; set; }
        [XmlAttribute("UPG")]
        public UPG UPG {get;set;}
        [XmlAttribute("PIC")]
        public int PIC { get; set; }
    }

}
