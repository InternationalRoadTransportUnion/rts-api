namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rts.iru.org/services/TerminationService-1")]
    public partial class reconciliationRequestsTypeReconciliationRequests
    {
        
        private System.Collections.Generic.List<ReconciliationRequestType> reconciliationRequestField;
        
        private int offsetField;
        
        private int countField;
        
        private bool endReachedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReconciliationRequest", Namespace="http://rts.iru.org/model/termination-1", Order=0)]
        public virtual System.Collections.Generic.List<ReconciliationRequestType> ReconciliationRequest
        {
            get
            {
                return this.reconciliationRequestField;
            }
            set
            {
                this.reconciliationRequestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual int offset
        {
            get
            {
                return this.offsetField;
            }
            set
            {
                this.offsetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual int count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual bool endReached
        {
            get
            {
                return this.endReachedField;
            }
            set
            {
                this.endReachedField = value;
            }
        }
    }
}
