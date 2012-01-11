using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RTSDotNETClient.WSST
{

    /// <summary>
    /// UPG
    /// </summary>
    public enum UPG
    {
        /// <summary>
        /// New
        /// </summary>
        [XmlEnum("N")]
        New,

        /// <summary>
        /// Cancel / Delete
        /// </summary>
        [XmlEnum("CancelDelete")]
        CancelDelete
    }

    /// <summary>
    /// Type of the Upload
    /// </summary>
    public enum UploadType
    {
        /// <summary>
        /// SafeTIR Data Upload
        /// </summary>
        [XmlEnum("1")]
        DataUpload=1
    }

    /// <summary>
    /// The query object to be sent to the WSST web service
    /// </summary>
    [XmlRoot("SafeTIR", Namespace = "http://www.iru.org/SafeTIRUpload")]
    public class Query : BaseQueryResponse
    {
        /// <summary>
        /// The xml schema definition file that defines the structure of the query
        /// </summary>
        public const string Xsd = "SafeTIRUpload.xsd";

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
    /// The body of the query
    /// </summary>
    public class Body
    {
        /// <summary>
        /// Version of the business structure of the Upload.
        /// </summary>
        public string Version{ get; set; }

        /// <summary>
        /// The subscriber ID
        /// </summary>
        public string SubscriberID { get; set; }

        /// <summary>
        /// Password of the subscriber (optional)
        /// </summary>
        public string Password{ get; set; }

        /// <summary>
        /// Type of the upload
        /// </summary>
        public UploadType UploadType { get; set; } 
      
        /// <summary>
        /// Total Number of Carnet records sent in this Upload batch
        /// </summary>
        public int TCN{ get; set; }        

        /// <summary>
        /// The date and time this message has been sent.
        /// </summary>
        public DateTime SentTime { get; set; }

        /// <summary>
        /// ID value set by Sender
        /// </summary>
        [XmlElement("Sender_MessageID")]
        public string SenderMessageID { get; set; }

        /// <summary>
        /// The set of Carnet Records Uploaded
        /// </summary>
        public List<Record> SafeTIRRecords { get; set; }
    }

    /// <summary>
    /// One Carnet Record
    /// </summary>
    public class Record
    { 
        /// <summary>
        /// TIR Carnet Reference Number
        /// </summary>
        [XmlAttribute("TNO")]
        public string TNO { get; set; }

        /// <summary>
        /// ISO3 code of the country of termination
        /// </summary>
        [XmlAttribute("ICC")]
        public string ICC {get;set;}
        
        /// <summary>
        /// Date in Customs Ledger (Termination)
        /// </summary>
        [XmlAttribute("DCL", DataType = "date")]
        public DateTime DCL {get;set;}
        
        /// <summary>
        /// Record Number in Customs Ledger (Termination)
        /// </summary>
        [XmlAttribute("CNL")]
        public string CNL { get; set; }
        
        /// <summary>
        /// Name or Number of Customs Office
        /// </summary>
        [XmlAttribute("COF")]
        public string COF { get; set; }
        
        /// <summary>
        /// Date of Discharge
        /// </summary>
        [XmlAttribute("DDI", DataType="date")]
        public DateTime DDI { get; set; }
        
        /// <summary>
        /// Reference Number of Discharge
        /// </summary>
        [XmlAttribute("RND")]
        public string RND {get;set;}
        
        /// <summary>
        /// Final or Partial Discharge
        /// </summary>
        [XmlAttribute("PFD")]
        public string PFD {get;set;}
        
        /// <summary>
        /// Discharge with or without Reservation. 
        /// <remarks>If Discharge is without reservation, the string "OK" will be used; if with Reservation, one character string, "R", will be used.</remarks>
        /// </summary>
        [XmlAttribute("CWR")]
        public CWR CWR {get;set;}
        
        /// <summary>
        /// Volet Page Number. This field contains the page number of the volet pertaining to the discharge and must be an even number in the range 2 to 20 (inclusive).
        /// </summary>
        [XmlAttribute("VPN")]
        public int VPN {get;set;}
        
        /// <summary>
        /// A comment
        /// </summary>
        [XmlAttribute("COM")]
        public string COM {get;set;}
        
        /// <summary>
        /// Carnet / Volet retained or not by customs
        /// </summary>
        [XmlAttribute("RBC")]
        public RBC RBC { get; set; }

        /// <summary>
        /// The previous data supplied by Customs for this TIR number was a mistake and needs to be canceled. All the mandatory fields for the record must be the same for the record being canceled and the record doing the canceling.
        /// <remarks>This delimiter can be used to update an existing record. When using such a mechanism, the new record (without UPG) should be present just after the record (with UPG value = "C") doing the canceling in the same message.</remarks>
        /// </summary>
        [XmlAttribute("UPG")]
        public UPG UPG {get;set;}
        
        /// <summary>
        /// Number of packages discharged
        /// </summary>
        [XmlAttribute("PIC")]
        public int PIC { get; set; }
    }

}
