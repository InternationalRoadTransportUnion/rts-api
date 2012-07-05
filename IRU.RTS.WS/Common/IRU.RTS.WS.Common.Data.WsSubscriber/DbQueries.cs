using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IRU.Common.EnterpriseLibrary.Data;

namespace IRU.RTS.WS.Common.Data.WsSubscriber
{
    public class DbQueries: IDisposable
    {
        public void GetSubscriberEncryptionKeysBySubscriber(string subscriberId, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetSubscriberEncryptionKeysBySubscriber.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetServiceMethodsBySubscriberAndService(string subscriberId, string serviceId, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetServiceMethodsBySubscriberAndService.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId);
            db.AddInParameter(dbc, "ServiceId", System.Data.DbType.String, serviceId);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetAllIRUEncryptionsKeys(bool onlyActive, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllIRUEncryptionsKeys.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "OnlyActive", System.Data.DbType.Boolean, onlyActive);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetAllSubscriberEncryptionKeys(bool onlyActive, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllSubscriberEncryptionKeys.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "OnlyActive", System.Data.DbType.Boolean, onlyActive);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetAllRtsplusSignatureKeys(bool onlyActive, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllRtsplusSignatureKeys.sql");            
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "OnlyActive", System.Data.DbType.Boolean, onlyActive);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void InsertRtsplusSignatureKeyForSubscriber(string userId, string subscriberId, DateTime validFrom, DateTime validTo, string thumbprint, byte[] certBlob, string privateKeyXml)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.InsertRtsplusSignatureKeyForSubscriber.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "UserId", System.Data.DbType.String, userId);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId);
            db.AddInParameter(dbc, "ValidFrom", System.Data.DbType.DateTime, validFrom);
            db.AddInParameter(dbc, "ValidTo", System.Data.DbType.DateTime, validTo);
            db.AddInParameter(dbc, "Thumbprint", System.Data.DbType.String, thumbprint);
            db.AddInParameter(dbc, "CertBlob", System.Data.DbType.Binary, certBlob);
            db.AddInParameter(dbc, "PrivateKey", System.Data.DbType.Xml, privateKeyXml);
            db.AddInParameter(dbc, "KeyActive", System.Data.DbType.Boolean, false);

            db.ExecuteNonQuery(dbc);
        }

        public void ActivateRtsplusSignatureKeyForSubscriber(string userId, string subscriberId, string thumbprint, bool serverCertificate)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.ActivateRtsplusSignatureKeyForSubscriber.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "UserId", System.Data.DbType.String, userId);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId);
            db.AddInParameter(dbc, "Thumbprint", System.Data.DbType.String, thumbprint);
            db.AddInParameter(dbc, "ServerCertificate", System.Data.DbType.Boolean, serverCertificate);

            db.ExecuteNonQuery(dbc);
        }

        public void DeactivateRtsplusSignatureKeyForSubscriber(string userId, string subscriberId, string thumbprint, bool serverCertificate)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.DeactivateRtsplusSignatureKeyForSubscriber.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "UserId", System.Data.DbType.String, userId);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId);
            db.AddInParameter(dbc, "Thumbprint", System.Data.DbType.String, thumbprint);
            db.AddInParameter(dbc, "ServerCertificate", System.Data.DbType.Boolean, serverCertificate);

            db.ExecuteNonQuery(dbc);
        }

        #region IDisposable Members

        public void Dispose()
        {
            //
        }

        #endregion
    }
}
