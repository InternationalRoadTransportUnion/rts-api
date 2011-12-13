namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class TIROperationTerminationsType
    {
        
        private object[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        private uint countField;
        
        private bool countFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CancelledTIROperationTermination", typeof(TIROperationTerminationType), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("NewTIROperationTermination", typeof(TIROperationTerminationType), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("UpdatedTIROperationTermination", typeof(UpdatedTIROperationTerminationType), Order=0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public virtual object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order=1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual uint count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
                this.countSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool countSpecified
        {
            get
            {
                return this.countFieldSpecified;
            }
            set
            {
                this.countFieldSpecified = value;
            }
        }
    }
}
