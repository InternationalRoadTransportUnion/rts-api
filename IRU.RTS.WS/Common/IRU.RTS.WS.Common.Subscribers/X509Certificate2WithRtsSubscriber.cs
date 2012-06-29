using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Subscribers
{
    public class X509Certificate2WithRtsSubscriber: X509Certificate2
    {
        private string _subscriberId;

        public string SubscriberId
        {
            get { return _subscriberId; }
        }

        public X509Certificate2WithRtsSubscriber(string subscriberId, byte[] rawData)
            : base(rawData)
        {
            _subscriberId = subscriberId;
        }
    }

    static class X509Certificate2Extension
    {
        public static string SubscriberId(this X509Certificate2 certificate)
        {
            string res = null;

            if (certificate is X509Certificate2WithRtsSubscriber)
            {
                res = ((X509Certificate2WithRtsSubscriber)certificate).SubscriberId;
            }

            return res;
        }
    }
}
