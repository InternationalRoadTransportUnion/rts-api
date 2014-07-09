namespace IRU.RTS.WS.VoucherService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/services/VoucherService-1")]
    public partial class QueriedVoucherType
    {
        
        private string voucherNumberField;
        
        private RegistrationStatusType voucherRegistrationStatusField;
        
        private RegisteredTIRCarnetType registeredTIRCarnetField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual string VoucherNumber
        {
            get
            {
                return this.voucherNumberField;
            }
            set
            {
                this.voucherNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual RegistrationStatusType VoucherRegistrationStatus
        {
            get
            {
                return this.voucherRegistrationStatusField;
            }
            set
            {
                this.voucherRegistrationStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public virtual RegisteredTIRCarnetType RegisteredTIRCarnet
        {
            get
            {
                return this.registeredTIRCarnetField;
            }
            set
            {
                this.registeredTIRCarnetField = value;
            }
        }
    }
}
