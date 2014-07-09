namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rts.iru.org/model/termination-1")]
    public partial class MissingTIROperationTerminationType
    {
        
        private string tIRCarnetNumberField;
        
        private byte voletPageNumberField;
        
        private Customs customsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://rts.iru.org/model/carnet-1", DataType="token", Order=0)]
        public virtual string TIRCarnetNumber
        {
            get
            {
                return this.tIRCarnetNumberField;
            }
            set
            {
                this.tIRCarnetNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://rts.iru.org/model/carnet-1", Order=1)]
        public virtual byte VoletPageNumber
        {
            get
            {
                return this.voletPageNumberField;
            }
            set
            {
                this.voletPageNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.iru.org/model/tir-actor-1", Order=2)]
        public virtual Customs Customs
        {
            get
            {
                return this.customsField;
            }
            set
            {
                this.customsField = value;
            }
        }
    }
}
