namespace IRU.RTS.WS.VoucherService.Interface
{
    
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StoppedCarnetType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CarnetType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RegisteredTIRCarnetType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/carnet-1")]
    public partial class CarnetBaseType
    {
        
        private string tIRCarnetNumberField;
        
        private System.DateTime expiryDateField;
        
        private Association associationField;
        
        private HaulierType holderField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=1)]
        public virtual System.DateTime ExpiryDate
        {
            get
            {
                return this.expiryDateField;
            }
            set
            {
                this.expiryDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.iru.org/model/tir-actor-1", Order=2)]
        public virtual Association Association
        {
            get
            {
                return this.associationField;
            }
            set
            {
                this.associationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.iru.org/model/tir-actor-1", Order=3)]
        public virtual HaulierType Holder
        {
            get
            {
                return this.holderField;
            }
            set
            {
                this.holderField = value;
            }
        }
    }
}
