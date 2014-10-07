namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rts.iru.org/services/CarnetService-1")]
    public partial class stoppedCarnetsTypeTotal
    {
        
        private System.DateTime fromField;
        
        private bool fromFieldSpecified;
        
        private System.DateTime toField;
        
        private bool toFieldSpecified;
        
        private int countField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual System.DateTime from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
                this.fromSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool fromSpecified
        {
            get
            {
                return this.fromFieldSpecified;
            }
            set
            {
                this.fromFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual System.DateTime to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
                this.toSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool toSpecified
        {
            get
            {
                return this.toFieldSpecified;
            }
            set
            {
                this.toFieldSpecified = value;
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
    }
}
