﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="TIREPDB2GServiceClassSoap", Namespace="http://www.iru.org")]
public partial class TIREPDB2GServiceClass : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback TIREPDB2GOperationCompleted;
    
    /// <remarks/>
    public TIREPDB2GServiceClass() {
        this.Url = "http://TCP-IP Address/rts/TIREPDB2G_ws/TIREPDB2Gupload.asmx";
    }
    
    /// <remarks/>
    public event TIREPDB2GCompletedEventHandler TIREPDB2GCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.iru.org/TIREPDB2G", RequestNamespace="http://www.iru.org", ResponseNamespace="http://www.iru.org", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public TIREPDB2GUploadAck TIREPDB2G(TIREPDB2GUploadParams su) {
        object[] results = this.Invoke("TIREPDB2G", new object[] {
                    su});
        return ((TIREPDB2GUploadAck)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginTIREPDB2G(TIREPDB2GUploadParams su, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("TIREPDB2G", new object[] {
                    su}, callback, asyncState);
    }
    
    /// <remarks/>
    public TIREPDB2GUploadAck EndTIREPDB2G(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((TIREPDB2GUploadAck)(results[0]));
    }
    
    /// <remarks/>
    public void TIREPDB2GAsync(TIREPDB2GUploadParams su) {
        this.TIREPDB2GAsync(su, null);
    }
    
    /// <remarks/>
    public void TIREPDB2GAsync(TIREPDB2GUploadParams su, object userState) {
        if ((this.TIREPDB2GOperationCompleted == null)) {
            this.TIREPDB2GOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTIREPDB2GOperationCompleted);
        }
        this.InvokeAsync("TIREPDB2G", new object[] {
                    su}, this.TIREPDB2GOperationCompleted, userState);
    }
    
    private void OnTIREPDB2GOperationCompleted(object arg) {
        if ((this.TIREPDB2GCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.TIREPDB2GCompleted(this, new TIREPDB2GCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.iru.org")]
public partial class TIREPDB2GUploadParams {
    
    private string subscriberIDField;
    
    private string certificateIDField;
    
    private byte[] eSessionKeyField;
    
    private string subscriberMessageIDField;
    
    private string informationExchangeVersionField;
    
    private string messageNameField;
    
    private System.DateTime timeSentField;
    
    private byte[] messageContentField;
    
    /// <remarks/>
    public string SubscriberID {
        get {
            return this.subscriberIDField;
        }
        set {
            this.subscriberIDField = value;
        }
    }
    
    /// <remarks/>
    public string CertificateID {
        get {
            return this.certificateIDField;
        }
        set {
            this.certificateIDField = value;
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
    public string SubscriberMessageID {
        get {
            return this.subscriberMessageIDField;
        }
        set {
            this.subscriberMessageIDField = value;
        }
    }
    
    /// <remarks/>
    public string InformationExchangeVersion {
        get {
            return this.informationExchangeVersionField;
        }
        set {
            this.informationExchangeVersionField = value;
        }
    }
    
    /// <remarks/>
    public string MessageName {
        get {
            return this.messageNameField;
        }
        set {
            this.messageNameField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime TimeSent {
        get {
            return this.timeSentField;
        }
        set {
            this.timeSentField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] MessageContent {
        get {
            return this.messageContentField;
        }
        set {
            this.messageContentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.iru.org")]
public partial class TIREPDB2GUploadAck {
    
    private string hostIDField;
    
    private string subscriberMessageIDField;
    
    private int returnCodeField;
    
    private int returnCodeReasonField;
    
    /// <remarks/>
    public string HostID {
        get {
            return this.hostIDField;
        }
        set {
            this.hostIDField = value;
        }
    }
    
    /// <remarks/>
    public string SubscriberMessageID {
        get {
            return this.subscriberMessageIDField;
        }
        set {
            this.subscriberMessageIDField = value;
        }
    }
    
    /// <remarks/>
    public int ReturnCode {
        get {
            return this.returnCodeField;
        }
        set {
            this.returnCodeField = value;
        }
    }
    
    /// <remarks/>
    public int ReturnCodeReason {
        get {
            return this.returnCodeReasonField;
        }
        set {
            this.returnCodeReasonField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
public delegate void TIREPDB2GCompletedEventHandler(object sender, TIREPDB2GCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class TIREPDB2GCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal TIREPDB2GCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public TIREPDB2GUploadAck Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((TIREPDB2GUploadAck)(this.results[0]));
        }
    }
}
