namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rts.iru.org/model/carnet-1")]
    public partial class CarnetTypeTIROperationTerminations
    {
        
        private uint countField;
        
        private bool hasFinalField;
        
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
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual bool hasFinal
        {
            get
            {
                return this.hasFinalField;
            }
            set
            {
                this.hasFinalField = value;
            }
        }
    }
}
