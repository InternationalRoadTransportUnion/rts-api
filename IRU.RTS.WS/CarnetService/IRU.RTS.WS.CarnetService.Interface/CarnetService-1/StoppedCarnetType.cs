namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/carnet-1")]
    public partial class StoppedCarnetType : CarnetBaseType
    {
        
        private System.DateTime declarationDateField;
        
        private System.DateTime invalidationDateField;
        
        private InvalidationStatusType invalidationStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=0)]
        public virtual System.DateTime DeclarationDate
        {
            get
            {
                return this.declarationDateField;
            }
            set
            {
                this.declarationDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual System.DateTime InvalidationDate
        {
            get
            {
                return this.invalidationDateField;
            }
            set
            {
                this.invalidationDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public virtual InvalidationStatusType InvalidationStatus
        {
            get
            {
                return this.invalidationStatusField;
            }
            set
            {
                this.invalidationStatusField = value;
            }
        }
    }
}
