using System;
using System.Collections.Generic;
using System.Text;

namespace IRU.RTS.TIREPD
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.iru.org")]
    public partial class TIREPDG2BUploadParams
    {

        private string subscriberIDField;

        private string certificateIDField;

        private byte[] eSessionKeyField;

        private string subscriberMessageIDField;

        private string informationExchangeVersionField;

        private string messageNameField;

        private System.DateTime timeSentField;

        private byte[] messageContentField;

        /// <remarks/>
        public string SubscriberID
        {
            get
            {
                return this.subscriberIDField;
            }
            set
            {
                this.subscriberIDField = value;
            }
        }

        /// <remarks/>
        public string CertificateID
        {
            get
            {
                return this.certificateIDField;
            }
            set
            {
                this.certificateIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] ESessionKey
        {
            get
            {
                return this.eSessionKeyField;
            }
            set
            {
                this.eSessionKeyField = value;
            }
        }

        /// <remarks/>
        public string SubscriberMessageID
        {
            get
            {
                return this.subscriberMessageIDField;
            }
            set
            {
                this.subscriberMessageIDField = value;
            }
        }

        /// <remarks/>
        public string InformationExchangeVersion
        {
            get
            {
                return this.informationExchangeVersionField;
            }
            set
            {
                this.informationExchangeVersionField = value;
            }
        }

        /// <remarks/>
        public string MessageName
        {
            get
            {
                return this.messageNameField;
            }
            set
            {
                this.messageNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime TimeSent
        {
            get
            {
                return this.timeSentField;
            }
            set
            {
                this.timeSentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] MessageContent
        {
            get
            {
                return this.messageContentField;
            }
            set
            {
                this.messageContentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.iru.org")]
    public partial class TIREPDG2BUploadAck
    {

        private string hostIDField;

        private string subscriberMessageIDField;

        private int returnCodeField;

        private int returnCodeReasonField;

        /// <remarks/>
        public string HostID
        {
            get
            {
                return this.hostIDField;
            }
            set
            {
                this.hostIDField = value;
            }
        }

        /// <remarks/>
        public string SubscriberMessageID
        {
            get
            {
                return this.subscriberMessageIDField;
            }
            set
            {
                this.subscriberMessageIDField = value;
            }
        }

        /// <remarks/>
        public int ReturnCode
        {
            get
            {
                return this.returnCodeField;
            }
            set
            {
                this.returnCodeField = value;
            }
        }

        /// <remarks/>
        public int ReturnCodeReason
        {
            get
            {
                return this.returnCodeReasonField;
            }
            set
            {
                this.returnCodeReasonField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.iru.org")]
    public partial class TIREPDB2GUploadParams
    {

        private string subscriberIDField;

        private string certificateIDField;

        private byte[] eSessionKeyField;

        private string subscriberMessageIDField;

        private string informationExchangeVersionField;

        private string messageNameField;

        private System.DateTime timeSentField;

        private byte[] messageContentField;

        /// <remarks/>
        public string SubscriberID
        {
            get
            {
                return this.subscriberIDField;
            }
            set
            {
                this.subscriberIDField = value;
            }
        }

        /// <remarks/>
        public string CertificateID
        {
            get
            {
                return this.certificateIDField;
            }
            set
            {
                this.certificateIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] ESessionKey
        {
            get
            {
                return this.eSessionKeyField;
            }
            set
            {
                this.eSessionKeyField = value;
            }
        }

        /// <remarks/>
        public string SubscriberMessageID
        {
            get
            {
                return this.subscriberMessageIDField;
            }
            set
            {
                this.subscriberMessageIDField = value;
            }
        }

        /// <remarks/>
        public string InformationExchangeVersion
        {
            get
            {
                return this.informationExchangeVersionField;
            }
            set
            {
                this.informationExchangeVersionField = value;
            }
        }

        /// <remarks/>
        public string MessageName
        {
            get
            {
                return this.messageNameField;
            }
            set
            {
                this.messageNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime TimeSent
        {
            get
            {
                return this.timeSentField;
            }
            set
            {
                this.timeSentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] MessageContent
        {
            get
            {
                return this.messageContentField;
            }
            set
            {
                this.messageContentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.iru.org")]
    public partial class TIREPDB2GUploadAck
    {

        private string hostIDField;

        private string subscriberMessageIDField;

        private int returnCodeField;

        private int returnCodeReasonField;

        /// <remarks/>
        public string HostID
        {
            get
            {
                return this.hostIDField;
            }
            set
            {
                this.hostIDField = value;
            }
        }

        /// <remarks/>
        public string SubscriberMessageID
        {
            get
            {
                return this.subscriberMessageIDField;
            }
            set
            {
                this.subscriberMessageIDField = value;
            }
        }

        /// <remarks/>
        public int ReturnCode
        {
            get
            {
                return this.returnCodeField;
            }
            set
            {
                this.returnCodeField = value;
            }
        }

        /// <remarks/>
        public int ReturnCodeReason
        {
            get
            {
                return this.returnCodeReasonField;
            }
            set
            {
                this.returnCodeReasonField = value;
            }
        }
    }


}
