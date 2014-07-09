namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class UpdatedTIROperationTerminationType
    {
        
        private TIROperationTerminationType cancelledTIROperationTerminationField;
        
        private TIROperationTerminationType newTIROperationTerminationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual TIROperationTerminationType CancelledTIROperationTermination
        {
            get
            {
                return this.cancelledTIROperationTerminationField;
            }
            set
            {
                this.cancelledTIROperationTerminationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual TIROperationTerminationType NewTIROperationTermination
        {
            get
            {
                return this.newTIROperationTerminationField;
            }
            set
            {
                this.newTIROperationTerminationField = value;
            }
        }
    }
}
