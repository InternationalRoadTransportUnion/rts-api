namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReceivedTIROperationTerminationType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class TIROperationTerminationType
    {
        
        private string tIRCarnetNumberField;
        
        private byte voletPageNumberField;
        
        private Customs customsField;
        
        private string customsOfficeField;
        
        private string customsLedgerEntryReferenceField;
        
        private System.DateTime customsLedgerEntryDateField;
        
        private string certificateOfTerminationReferenceField;
        
        private System.DateTime certificateOfTerminationDateField;
        
        private bool isFinalField;
        
        private byte sequenceNumberField;
        
        private bool sequenceNumberFieldSpecified;
        
        private bool isWithReservationField;
        
        private string customsCommentField;
        
        private uint packageCountField;
        
        private bool packageCountFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://rts.iru.org/model/carnet-1", DataType="token", Order=0)]
        public virtual string TIRCarnetNumber
        {
            get
            {
                return this.tIRCarnetNumberField;
            }
            set
            {
                this.tIRCarnetNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://rts.iru.org/model/carnet-1", Order=1)]
        public virtual byte VoletPageNumber
        {
            get
            {
                return this.voletPageNumberField;
            }
            set
            {
                this.voletPageNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.iru.org/model/tir-actor-1", Order=2)]
        public virtual Customs Customs
        {
            get
            {
                return this.customsField;
            }
            set
            {
                this.customsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public virtual string CustomsOffice
        {
            get
            {
                return this.customsOfficeField;
            }
            set
            {
                this.customsOfficeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public virtual string CustomsLedgerEntryReference
        {
            get
            {
                return this.customsLedgerEntryReferenceField;
            }
            set
            {
                this.customsLedgerEntryReferenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public virtual System.DateTime CustomsLedgerEntryDate
        {
            get
            {
                return this.customsLedgerEntryDateField;
            }
            set
            {
                this.customsLedgerEntryDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public virtual string CertificateOfTerminationReference
        {
            get
            {
                return this.certificateOfTerminationReferenceField;
            }
            set
            {
                this.certificateOfTerminationReferenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public virtual System.DateTime CertificateOfTerminationDate
        {
            get
            {
                return this.certificateOfTerminationDateField;
            }
            set
            {
                this.certificateOfTerminationDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public virtual bool IsFinal
        {
            get
            {
                return this.isFinalField;
            }
            set
            {
                this.isFinalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public virtual byte SequenceNumber
        {
            get
            {
                return this.sequenceNumberField;
            }
            set
            {
                this.sequenceNumberField = value;
                this.SequenceNumberSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool SequenceNumberSpecified
        {
            get
            {
                return this.sequenceNumberFieldSpecified;
            }
            set
            {
                this.sequenceNumberFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public virtual bool IsWithReservation
        {
            get
            {
                return this.isWithReservationField;
            }
            set
            {
                this.isWithReservationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public virtual string CustomsComment
        {
            get
            {
                return this.customsCommentField;
            }
            set
            {
                this.customsCommentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public virtual uint PackageCount
        {
            get
            {
                return this.packageCountField;
            }
            set
            {
                this.packageCountField = value;
                this.PackageCountSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool PackageCountSpecified
        {
            get
            {
                return this.packageCountFieldSpecified;
            }
            set
            {
                this.packageCountFieldSpecified = value;
            }
        }
    }
}
