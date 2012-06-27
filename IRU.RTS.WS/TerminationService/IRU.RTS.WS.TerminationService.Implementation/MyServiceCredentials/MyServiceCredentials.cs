using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.IdentityModel.Selectors;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyServiceCredentials : ServiceCredentials
    {
        //X509Certificate2 additionalCertificate;
        bool useSubscriberCertificateDatabase;

        public MyServiceCredentials()
        {
        }

        protected MyServiceCredentials(MyServiceCredentials other)
            : base(other)
        {
            this.useSubscriberCertificateDatabase = other.useSubscriberCertificateDatabase;
        }

        //public X509Certificate2 AdditionalCertificate
        //{
        //    get
        //    {
        //        return this.additionalCertificate;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("value");
        //        }
        //        this.additionalCertificate = value;
        //    }
        //}

        public bool UseSubscriberCertificateDatabase
        {
            get
            {
                return this.useSubscriberCertificateDatabase;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.useSubscriberCertificateDatabase = value;
            }
        }

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            return new MyServiceCredentialsSecurityTokenManager(this);
        }

        protected override ServiceCredentials CloneCore()
        {
            return new MyServiceCredentials(this);
        }
    }
}
