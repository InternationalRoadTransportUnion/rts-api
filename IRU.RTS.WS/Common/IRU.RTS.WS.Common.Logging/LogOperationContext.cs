using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace IRU.RTS.WS.Common.Logging
{
    public class LogOperationContext : IExtension<OperationContext>
    {
        private readonly IDictionary<string, object> items;

        private LogOperationContext()
        {
            items = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get
            {
                object oRes;
                items.TryGetValue(key, out oRes);
                return oRes;
            }

            set
            {
                items[key] = value;
            }
        }

        public static LogOperationContext Current
        {
            get
            {
                LogOperationContext context = OperationContext.Current.Extensions.Find<LogOperationContext>();
                if (context == null)
                {
                    context = new LogOperationContext();
                    OperationContext.Current.Extensions.Add(context);
                }
                return context;
            }
        }

        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
