using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IRU.Common.EnterpriseLibrary.Data;

namespace IRU.RTS.WS.Common.Data.Current
{
    public class DbQueries: IDisposable
    {
        public void GetInvalidatedCarnets(DateTime from, DateTime to, int minTIRCarnetNumber, int offset, uint count, DataReaderExecutedDelegate dataReaderExecuted)
        {
            Database db = DatabaseFactory.CreateDatabase("Current");

            string sSql = db.GetSqlStringFromResource("Queries.GetInvalidatedCarnets.sql");
            DbCommand dbc = db.GetSqlStringCommand(sSql);
            db.AddInParameter(dbc, "MinTIRCarnetNumber", System.Data.DbType.Int32, minTIRCarnetNumber);
            db.AddInParameter(dbc, "DateFrom", System.Data.DbType.DateTime, from);
            db.AddInParameter(dbc, "DateTo", System.Data.DbType.DateTime, to);
            db.AddInParameter(dbc, "Offset", System.Data.DbType.Int32, offset);
            db.AddInParameter(dbc, "Count", System.Data.DbType.Int32, count);

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
