using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyServiceCredentials : ServiceCredentials
    {
        bool useSubscriberCertificateDatabase;

        public MyServiceCredentials()
        {
            // set default Service certificate
            // TODO: get it from SUBSCRIBER database

            // from fake Subcriber store (file implementation)
            MySubscribersCertificateStore mySubscribersCertificateStore = new MySubscribersCertificateStore();
            this.ServiceCertificate.Certificate = mySubscribersCertificateStore.GetCertificateFromStore("90A9FA8B3118DA540DBBB5C3CF525FBB5E8140D5");

            //// ELSE
            //// from Windows store...
            //this.ServiceCertificate.SetCertificate(
            //    StoreLocation.CurrentUser,
            //    StoreName.My,
            //    X509FindType.FindBySubjectName,
            //    "TEST_CERT_SIGN_SERVER_CERT");
        }

        protected MyServiceCredentials(MyServiceCredentials other)
            : base(other)
        {
            this.useSubscriberCertificateDatabase = other.useSubscriberCertificateDatabase;
        }

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
