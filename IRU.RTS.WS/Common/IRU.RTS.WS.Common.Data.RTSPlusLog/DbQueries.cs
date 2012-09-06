using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IRU.Common.EnterpriseLibrary.Data;

namespace IRU.RTS.WS.Common.Data.RTSPlusLog
{
    public class DbQueries : IDisposable
    {
        public void InsertWsCall(string subscriberId, DateTime requestDateTime, string requestIpAddress, string requestAction, string requestContent, DateTime? replyDateTime, string replyAction, string replyContent)
        {
            Database db = DatabaseFactory.CreateDatabase("RTSPlusLog");

            string sSql = db.GetSqlStringFromResource("Queries.InsertWsCall.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "SubscriberId", System.Data.DbType.String, subscriberId ?? String.Empty);
            db.AddInParameter(dbc, "RequestDateTime", System.Data.DbType.DateTime, requestDateTime);
            db.AddInParameter(dbc, "RequestIpAddress", System.Data.DbType.String, requestIpAddress ?? String.Empty);            
            db.AddInParameter(dbc, "RequestAction", System.Data.DbType.String, requestAction);
            db.AddInParameter(dbc, "RequestContent", System.Data.DbType.Xml, requestContent);
            db.AddInParameter(dbc, "ReplyDateTime", System.Data.DbType.DateTime, replyDateTime);
            db.AddInParameter(dbc, "ReplyAction", System.Data.DbType.String, replyAction);
            db.AddInParameter(dbc, "ReplyContent", System.Data.DbType.Xml, replyContent);
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
