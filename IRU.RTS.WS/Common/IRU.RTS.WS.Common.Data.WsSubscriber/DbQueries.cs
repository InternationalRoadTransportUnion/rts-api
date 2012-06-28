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

        public void GetAllIRUEncryptionsKeys(DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllIRUEncryptionsKeys.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetAllSubscriberEncryptionKeys(DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllSubscriberEncryptionKeys.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        public void GetAllRtsplusSignatureKeys(DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("WsSubscriber");

            string sSql = db.GetSqlStringFromResource("Queries.GetAllRtsplusSignatureKeys.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);

            db.ExecuteReader(dataReaderExecuted, dbc);
        }

        #region IDisposable Members

        public void Dispose()
        {
            //
        }

        #endregion
    }
}
