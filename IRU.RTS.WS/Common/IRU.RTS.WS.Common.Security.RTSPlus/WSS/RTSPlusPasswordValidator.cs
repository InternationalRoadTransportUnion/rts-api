using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.Security;
using IRU.Common.WCF.Security.WSS.PasswordDigest;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;

namespace IRU.RTS.WS.Common.Security.RTSPlus.WSS
{
    public class RTSPlusPasswordValidator : DefaultPasswordValidator, IPasswordValidator
    {
        private string _password;
        private string _soapAction;
        private string _serviceName;        
        private string _methodName;
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
            OperationContext.Current.IncomingMessageProperties.Add("RTS_SUBSCRIBER_ID", userName);

            _password = null;
            _soapAction = String.Empty;            
            _serviceName = String.Empty;
            _methodName = String.Empty;
            _methodId = 0;            

            OperationContext oc = OperationContext.Current;
            if (oc != null)
            {                
                if (oc.IncomingMessageHeaders != null)
                {
                    _soapAction = oc.IncomingMessageHeaders.Action;

                    Regex rx = new Regex("^http://(([^/]+)/)*(.*)$");
                    Match m = rx.Match(_soapAction);

                    if (m.Success)
                    {
                        _serviceName = m.Groups[Math.Max(m.Groups.Count - 2, 0)].Value;
                        _methodName = m.Groups[m.Groups.Count - 1].Value;

                        switch (_serviceName)
                        {
                            case "CarnetService-1":
                                switch (_methodName)
                                {
                                    case "queryCarnet":
                                        _methodId = 1;
                                        break;
                                    case "getStoppedCarnets":
                                        _methodId = 2;
                                        break;
                                }
                                break;
                        }
                    }
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
                    throw new SecurityException(String.Format("Calling action [{0}] is not allowed by IRU for the subscriber [{1}].", _soapAction, userName));
                }
            }

            return bRes;
        }

        #endregion
    }
}
