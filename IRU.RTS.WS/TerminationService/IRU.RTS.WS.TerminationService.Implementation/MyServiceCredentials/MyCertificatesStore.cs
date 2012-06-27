using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyCertificatesStore
    {
        // CERT_SIGN_SEVER_CERT Thumbprint:
        //  90 a9 fa 8b 31 18 da 54 0d bb b5 c3 cf 52 5f bb 5e 81 40 d5

        // CERT_SIGN_CLIENT_CERT Thumbprint:
        //  fd bd b9 e6 d2 81 b8 f4 96 f9 03 02 75 77 7f b6 eb 0b f9 1d
        // IRU.Common.Messaging from@somewhere.com (test) Thumbprint:
        //  e0 ca ea 17 8d f2 1d b2 0a 48 29 6b db 3a 9e 74 de 7d b8 1e

        string[] myClientsCertificatesThumprints = new string[] { 
            "FDBDB9E6D281B8F496F9030275777FB6EB0BF91D", 
            "E0CAEA178DF21DB20A48296BDB3A9E74DE7DB81E" };

        public string[] ClientsCertificatesThumprints()
        {
            return myClientsCertificatesThumprints;
        }

        public string ServiceCertificateThumprint()
        {
            return "90A9FA8B3118DA540DBBB5C3CF525FBB5E8140D5";
        }
    }
}
