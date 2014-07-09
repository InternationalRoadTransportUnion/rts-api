﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5472
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5472.
// 
#pragma warning disable 1591

namespace WSCscc_Client.WSSTstc {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SafeTIRUploadCopyServiceSoap", Namespace="http://www.iru.org")]
    public partial class SafeTIRUploadCopyService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback WSSTstcOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SafeTIRUploadCopyService() {
            this.Url = "http://localhost/RTS/WSSTstc_WS/asmapCopy.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event WSSTstcCompletedEventHandler WSSTstcCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.iru.org/WSSTstc", RequestNamespace="http://www.iru.org", ResponseNamespace="http://www.iru.org", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SafeTIRUploadCopyAck WSSTstc(SafeTIRUploadCopy su) {
            object[] results = this.Invoke("WSSTstc", new object[] {
                        su});
            return ((SafeTIRUploadCopyAck)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginWSSTstc(SafeTIRUploadCopy su, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("WSSTstc", new object[] {
                        su}, callback, asyncState);
        }
        
        /// <remarks/>
        public SafeTIRUploadCopyAck EndWSSTstc(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((SafeTIRUploadCopyAck)(results[0]));
        }
        
        /// <remarks/>
        public void WSSTstcAsync(SafeTIRUploadCopy su) {
            this.WSSTstcAsync(su, null);
        }
        
        /// <remarks/>
        public void WSSTstcAsync(SafeTIRUploadCopy su, object userState) {
            if ((this.WSSTstcOperationCompleted == null)) {
                this.WSSTstcOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWSSTstcOperationCompleted);
            }
            this.InvokeAsync("WSSTstc", new object[] {
                        su}, this.WSSTstcOperationCompleted, userState);
        }
        
        private void OnWSSTstcOperationCompleted(object arg) {
            if ((this.WSSTstcCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WSSTstcCompleted(this, new WSSTstcCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.iru.org")]
    public partial class SafeTIRUploadCopy {
        
        private string messageTagField;
        
        private byte[] eSessionKeyField;
        
        private byte[] safeTIRUploadDataField;
        
        private string copyToIDField;
        
        private string copyFromIDField;
        
        private string sender_MessageIDField;
        
        /// <remarks/>
        public string MessageTag {
            get {
                return this.messageTagField;
            }
            set {
                this.messageTagField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] ESessionKey {
            get {
                return this.eSessionKeyField;
            }
            set {
                this.eSessionKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] safeTIRUploadData {
            get {
                return this.safeTIRUploadDataField;
            }
            set {
                this.safeTIRUploadDataField = value;
            }
        }
        
        /// <remarks/>
        public string CopyToID {
            get {
                return this.copyToIDField;
            }
            set {
                this.copyToIDField = value;
            }
        }
        
        /// <remarks/>
        public string CopyFromID {
            get {
                return this.copyFromIDField;
            }
            set {
                this.copyFromIDField = value;
            }
        }
        
        /// <remarks/>
        public string Sender_MessageID {
            get {
                return this.sender_MessageIDField;
            }
            set {
                this.sender_MessageIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5476")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.iru.org")]
    public partial class SafeTIRUploadCopyAck {
        
        private int returnCodeField;
        
        /// <remarks/>
        public int ReturnCode {
            get {
                return this.returnCodeField;
            }
            set {
                this.returnCodeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void WSSTstcCompletedEventHandler(object sender, WSSTstcCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class WSSTstcCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal WSSTstcCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SafeTIRUploadCopyAck Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SafeTIRUploadCopyAck)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591