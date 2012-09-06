using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security;
using IRU.Common.WCF.Security.WSS.PasswordDigest;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;
using IRU.RTS.WS.Common.Logging;

namespace IRU.RTS.WS.CarnetService.Implementation
{
    public class CarnetServicePasswordValidator : DefaultPasswordValidator, IPasswordValidator
    {
        private string _password;
        private string _methodAction;
        private int _methodId;

        private void GetSubscriberEncryptionKeysBySubscriberExecuted(object sender, DataReaderEventArgs e)
        {
            while (e.DataReader.Read())
            {
                string sPassword = e.DataReader.GetValue<string>("ENCRYPTION_KEY_ID");
                bool bKeyActive = e.DataReader.GetValue<bool>("KEY_ACTIVE");
                DateTime dtExpiration = e.DataReader.GetValue<DateTime>("CERT_EXPIRY_DATE");

                if ((bKeyActive) && (dtExpiration.CompareTo(DateTime.Now) >= 0))
                    _password = sPassword;
            }
        }

        private void GetServiceMethodsBySubscriberAndServiceExecuted(object sender, DataReaderEventArgs e)
        {
            while (e.DataReader.Read())
            {
                bool bServiceActive = e.DataReader.GetValue<bool>("ACTIVE");
                bool bMethodActive = e.DataReader.GetValue<bool>("METHOD_ACTIVE");
                int iMethodId = e.DataReader.GetValue<int>("METHOD_ID");                
                
                if ((bServiceActive) && (bMethodActive) && (iMethodId == _methodId))
                    return;
            }

            _methodId = 0;
        }

        #region DefaultPasswordValidator Members

        public override string GetPassword(string userName)
        {
            LogOperationContext.Current["RTS_SUBSCRIBER_ID"] = userName;

            _password = null;
            _methodId = 0;
            _methodAction = String.Empty;

            OperationContext oc = OperationContext.Current;
            if (oc != null)
            {                
                if (oc.IncomingMessageHeaders != null)
                {
                    _methodAction = oc.IncomingMessageHeaders.Action;

                    if (_methodAction.EndsWith("queryCarnet"))
                        _methodId = 1;
                    if (_methodAction.EndsWith("getStoppedCarnets"))
                        _methodId = 2;
                }
            }
           
            using (DbQueries sq = new DbQueries())
            {
                sq.GetSubscriberEncryptionKeysBySubscriber(userName, GetSubscriberEncryptionKeysBySubscriberExecuted);
                sq.GetServiceMethodsBySubscriberAndService(userName, "CarnetService-1", GetServiceMethodsBySubscriberAndServiceExecuted);
            }

            return _password;
        }

        public override bool AuthenticationPassed(string userName, string hashedPassword, string nonce, string created)
        {
            bool bRes = base.AuthenticationPassed(userName, hashedPassword, nonce, created);

            if (bRes)
            {
                if ((_password != null) && (_methodId == 0))
                {
                    throw new SecurityException(String.Format("Calls to the method [{0}] are not allowed by IRU for the subscriber [{1}].", _methodAction, userName));
                }
            }

            return bRes;
        }

        #endregion
    }
}
