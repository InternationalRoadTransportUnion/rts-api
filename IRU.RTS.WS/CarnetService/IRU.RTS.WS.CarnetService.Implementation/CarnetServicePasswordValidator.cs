using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security;
using System.Data.SqlClient;
using IRU.Common.WCF.Security.WSS.PasswordDigest;
using IRU.RTS.WS.Common.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;

namespace IRU.RTS.WS.CarnetService.Implementation
{
    public class CarnetServicePasswordValidator : DefaultPasswordValidator, IPasswordValidator
    {
        private string _password;
        private string _methodAction;
        private int _methodId;

        private void GetSubscriberEncryptionKeysBySubscriberExecuted(object sender, DbDataReaderEventArgs e)
        {
            DbQuery dq = (DbQuery)sender;

            while (e.DbDataReader.Read())
            {
                string sPassword = dq.GetValue<string>(e.DbDataReader, "ENCRYPTION_KEY_ID");
                bool bKeyActive = dq.GetValue<bool>(e.DbDataReader, "KEY_ACTIVE");
                DateTime dtExpiration = dq.GetValue<DateTime>(e.DbDataReader, "CERT_EXPIRY_DATE");

                if ((bKeyActive) && (dtExpiration.CompareTo(DateTime.Now) >= 0))
                    _password = sPassword;
            }
        }

        private void GetServiceMethodsBySubscriberAndServiceExecuted(object sender, DbDataReaderEventArgs e)
        {
            DbQuery dq = (DbQuery)sender;

            while (e.DbDataReader.Read())
            {
                bool bServiceActive = dq.GetValue<bool>(e.DbDataReader, "ACTIVE");
                bool bMethodActive = dq.GetValue<bool>(e.DbDataReader, "METHOD_ACTIVE");
                int iMethodId = dq.GetValue<int>(e.DbDataReader, "METHOD_ID");                
                
                if ((bServiceActive) && (bMethodActive) && (iMethodId == _methodId))
                    return;
            }

            _methodId = 0;
        }

        #region DefaultPasswordValidator Members

        public override string GetPassword(string userName)
        {
            _password = null;
            _methodId = 0;
            _methodAction = String.Empty;

            OperationContext oc = OperationContext.Current;            
            if ((oc != null) && (oc.IncomingMessageHeaders != null))
            {
                _methodAction = oc.IncomingMessageHeaders.Action;

                if (_methodAction.EndsWith("queryCarnet"))
                    _methodId = 1;
                if (_methodAction.EndsWith("getStoppedCarnets"))
                    _methodId = 2;
            }
           
            SqlConnection scWsSubscriber = new SqlConnection(Properties.Settings.Default.WsSubscriberDB);
            using (DbWsSubscriberQuery sq = new DbWsSubscriberQuery(scWsSubscriber, Properties.Settings.Default.SQLCommandTimeout))
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
