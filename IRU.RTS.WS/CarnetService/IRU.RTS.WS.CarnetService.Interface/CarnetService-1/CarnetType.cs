namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/carnet-1")]
    public partial class CarnetType : CarnetBaseType
    {
        
        private CarnetStatusType statusField;
        
        private CarnetTypeTIROperationTerminations tIROperationTerminationsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual CarnetStatusType Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual CarnetTypeTIROperationTerminations TIROperationTerminations
        {
            get
            {
                return this.tIROperationTerminationsField;
            }
            set
            {
                this.tIROperationTerminationsField = value;
            }
        }
    }
}
