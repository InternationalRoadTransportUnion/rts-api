using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using IRU.RTS.WS.Common.Business;
using IRU.RTS.WS.Common.Data;

namespace IRU.RTS.WS.Common.Data.Current
{
    public class DbCurrentQuery : DbQuery, IDisposable
    {
        public DbCurrentQuery(DbConnection dbConnection)
            : base(dbConnection, -1)
        {
        }

        public DbCurrentQuery(DbConnection dbConnection, int commandTimeout)
            : base(dbConnection, commandTimeout)
        {
        }

        protected override object GetValue<T>(object value)
        {
            object retVal = base.GetValue<T>(value);

            if (typeof(T) == typeof(TIRCarnet))
            {
                retVal = new TIRCarnet(Convert.ToString(value));
            }
            else if (typeof(T) == typeof(CarnetInvalidationStatus))
            {
                retVal = Convert.ToInt32(value).AsCarnetInvalidationStatus();
            }

            return retVal;
        }

        public void GetInvalidatedCarnets(DateTime from, DateTime to, int minTIRCarnetNumber, int offset, uint count, DbDataReaderExecutedDelegate dbDataReaderExecuted)
        {
            if (dbDataReaderExecuted == null)
                return;

            using (DbCommand scmd = GetDbCommand(SQLCommandHelper.GetSQLCommandString("Queries.GetInvalidatedCarnets.sql")))
            {
                AddParameter(scmd, "MinTIRCarnetNumber", System.Data.DbType.Int32, minTIRCarnetNumber);
                AddParameter(scmd, "DateFrom", System.Data.DbType.DateTime, from);
                AddParameter(scmd, "DateTo", System.Data.DbType.DateTime, to);
                AddParameter(scmd, "Offset", System.Data.DbType.Int32, offset);
                AddParameter(scmd, "Count", System.Data.DbType.Int32, count);

                using (DbDataReader dr = scmd.ExecuteReader())
                {
                    dbDataReaderExecuted(this, new DbDataReaderEventArgs(dr));
                }
            }
        }
    }
}
