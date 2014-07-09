namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/services/TerminationService-1")]
    public partial class reconciliationRequestsType
    {
        
        private reconciliationRequestsTypeTotal totalField;
        
        private reconciliationRequestsTypeReconciliationRequests reconciliationRequestsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual reconciliationRequestsTypeTotal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual reconciliationRequestsTypeReconciliationRequests reconciliationRequests
        {
            get
            {
                return this.reconciliationRequestsField;
            }
            set
            {
                this.reconciliationRequestsField = value;
            }
        }
    }
}
