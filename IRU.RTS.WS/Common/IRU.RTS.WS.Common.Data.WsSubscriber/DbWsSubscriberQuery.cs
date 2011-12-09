using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace IRU.RTS.WS.Common.Data.WsSubscriber
{
    public class DbWsSubscriberQuery : DbQuery
    {
        public DbWsSubscriberQuery(DbConnection dbConnection)
            : base(dbConnection, -1)
        {
        }

        public DbWsSubscriberQuery(DbConnection dbConnection, int commandTimeout)
            : base(dbConnection, commandTimeout)
        {
        }

        public void GetSubscriberEncryptionKeysBySubscriber(string subscriberId, DbDataReaderExecutedDelegate dbDataReaderExecuted)
        {
            if (dbDataReaderExecuted == null)
                return;

            using (DbCommand scmd = GetDbCommand(SQLCommandHelper.GetSQLCommandString("Queries.GetSubscriberEncryptionKeysBySubscriber.sql")))
            {
                AddParameter(scmd, "SubscriberId", System.Data.DbType.String, subscriberId);

                using (DbDataReader dr = scmd.ExecuteReader())
                {
                    dbDataReaderExecuted(this, new DbDataReaderEventArgs(dr));
                }
            }
        }

        public void GetServiceMethodsBySubscriberAndService(string subscriberId, string serviceId, DbDataReaderExecutedDelegate dbDataReaderExecuted)
        {
            if (dbDataReaderExecuted == null)
                return;

            using (DbCommand scmd = GetDbCommand(SQLCommandHelper.GetSQLCommandString("Queries.GetServiceMethodsBySubscriberAndService.sql")))
            {
                AddParameter(scmd, "SubscriberId", System.Data.DbType.String, subscriberId);
                AddParameter(scmd, "ServiceId", System.Data.DbType.String, serviceId);

                using (DbDataReader dr = scmd.ExecuteReader())
                {
                    dbDataReaderExecuted(this, new DbDataReaderEventArgs(dr));
                }
            }
        }
    }
}
