namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rts.iru.org/model/termination-1")]
    public partial class ReconciliationRequestTypeOriginator
    {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Association", typeof(Association), Namespace="http://www.iru.org/model/tir-actor-1", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("IRU", typeof(IRU), Namespace="http://www.iru.org/model/tir-actor-1", Order=0)]
        public virtual object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
}
