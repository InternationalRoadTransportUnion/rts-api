namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class ReconciliationRequestReplyType
    {
        
        private string idField;
        
        private ReconciliationRequestReplyTypeReplyType replyTypeField;
        
        private TIROperationTerminationType correctedTIROperationTerminationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual ReconciliationRequestReplyTypeReplyType ReplyType
        {
            get
            {
                return this.replyTypeField;
            }
            set
            {
                this.replyTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public virtual TIROperationTerminationType CorrectedTIROperationTermination
        {
            get
            {
                return this.correctedTIROperationTerminationField;
            }
            set
            {
                this.correctedTIROperationTerminationField = value;
            }
        }
    }
}
