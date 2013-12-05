namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class ReconciliationRequestRepliesType
    {
        
        private System.Collections.Generic.List<ReconciliationRequestReplyType> reconciliationRequestReplyField;
        
        private uint countField;
        
        private bool countFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReconciliationRequestReply", Order=0)]
        public virtual System.Collections.Generic.List<ReconciliationRequestReplyType> ReconciliationRequestReply
        {
            get
            {
                return this.reconciliationRequestReplyField;
            }
            set
            {
                this.reconciliationRequestReplyField = value;
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
