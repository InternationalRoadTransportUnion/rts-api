using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using IRU.RTS.WS.Common.Data.RTSPlusLog;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.IdentityModel.Policy;

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

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            DbLoggerInformation dli = new DbLoggerInformation();
            dli.SubscriberId = (string)LogOperationContext.Current["RTS_SUBSCRIBER_ID"];
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            dli.RequestIpAddress = endpoint.Address;

            if (request != null)
            {
                MessageBuffer mbRequest = request.CreateBufferedCopy(Int32.MaxValue);
                Message mCopyRequest = mbRequest.CreateMessage();

                dli.RequestAction = mCopyRequest.Headers.Action;
                dli.Request = mCopyRequest.ToString();

                request = mCopyRequest;
            }

            return dli;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            DbLoggerInformation dli = (DbLoggerInformation)correlationState;
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
