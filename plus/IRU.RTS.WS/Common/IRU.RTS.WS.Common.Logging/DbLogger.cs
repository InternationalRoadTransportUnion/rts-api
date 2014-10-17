﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.IdentityModel.Policy;
using IRU.RTS.WS.Common.Data.RTSPlusLog;

namespace IRU.RTS.WS.Common.Logging
{
    public class DbLoggerInformation
    {
        public delegate void SaveToDatabaseDelegate();

        public string SubscriberId = null;
        public DateTime RequestDateTime = DateTime.Now;
        public string RequestIpAddress = null;
        public string RequestAction = null;
        public string Request = null;
        public DateTime? ReplyDateTime = null;
        public string ReplyAction = null;
        public string Reply = null;

        public void SaveToDatabase()
        {
            using (DbQueries dbq = new DbQueries())
            {
                dbq.InsertWsCall(SubscriberId, RequestDateTime, RequestIpAddress, RequestAction, Request, ReplyDateTime, ReplyAction, Reply);
            }
        }
    }

    public class DbLoggerBehaviourExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(DbLoggerBehaviour); }
        }

        protected override object CreateBehavior()
        {
            return new DbLoggerBehaviour();
        }
    }

    public class DbLoggerBehaviour : IEndpointBehavior, IDispatchMessageInspector
    {
        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        private DbLoggerInformation CreateDbLoggerInformation()
        {
            DbLoggerInformation dli = new DbLoggerInformation();
            object oSubscriberId;
            OperationContext.Current.IncomingMessageProperties.TryGetValue("RTS_SUBSCRIBER_ID", out oSubscriberId);
            dli.SubscriberId = (string)oSubscriberId;
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            dli.RequestIpAddress = endpoint.Address;

            return dli;
        }


        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            DbLoggerInformation dli = CreateDbLoggerInformation();

            if (request != null)
            {
                MessageBuffer mbRequest = request.CreateBufferedCopy(Int32.MaxValue);
                Message mCopyRequest = mbRequest.CreateMessage();

                dli.RequestAction = mCopyRequest.Headers.Action;
                dli.Request = mCopyRequest.ToString();

                if (WebOperationContext.Current != null)
                {
                    IncomingWebRequestContext inWebReqCtx = WebOperationContext.Current.IncomingRequest;
                    if (inWebReqCtx != null)
                    {
                        string sAddressFromProxy = (inWebReqCtx.Headers["X-Forwarded-For"] ?? String.Empty).Split(new char[] { ',' }).FirstOrDefault();
                        if (!String.IsNullOrEmpty(sAddressFromProxy))
                        {
                            dli.RequestIpAddress = sAddressFromProxy;
                        }
                    }
                }

                request = mCopyRequest;
            }

            return dli;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            DbLoggerInformation dli = (DbLoggerInformation)correlationState;

            if ((dli == null) &&                 
                (OperationContext.Current.RequestContext != null) && 
                (OperationContext.Current.RequestContext.RequestMessage != null))
            {
                dli = CreateDbLoggerInformation();
                dli.RequestAction = OperationContext.Current.RequestContext.RequestMessage.Headers.Action;
                dli.Request = OperationContext.Current.RequestContext.RequestMessage.ToString();
            }

            if (dli == null)
                return;

            dli.ReplyDateTime = DateTime.Now;

            if (reply != null)
            {
                MessageBuffer mbReply = reply.CreateBufferedCopy(Int32.MaxValue);
                Message mCopyReply = mbReply.CreateMessage();

                dli.ReplyAction = mCopyReply.Headers.Action;
                dli.Reply = mCopyReply.ToString();
                
                reply = mCopyReply;
            }

            DbLoggerInformation.SaveToDatabaseDelegate asyncSaveToDb = new DbLoggerInformation.SaveToDatabaseDelegate(dli.SaveToDatabase);
            asyncSaveToDb.BeginInvoke(null, null);

            return;
        }

        #endregion
    }
}
