using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Subscribers
{
    public class RTSServerCertGetter : ICertGetter
    {
        #region ICertGetter Members

        public void GetCertificates(ref X509Certificate2Collection certCollection)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
