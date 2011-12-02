namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rts.iru.org/services/CarnetService-1")]
    public partial class stoppedCarnetsTypeStoppedCarnets
    {
        
        private System.Collections.Generic.List<StoppedCarnetType> stoppedCarnetField;
        
        private int offsetField;
        
        private int countField;
        
        private bool endReachedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StoppedCarnet", Order=0)]
        public virtual System.Collections.Generic.List<StoppedCarnetType> StoppedCarnet
        {
            get
            {
                return this.stoppedCarnetField;
            }
            set
            {
                this.stoppedCarnetField = value;
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
