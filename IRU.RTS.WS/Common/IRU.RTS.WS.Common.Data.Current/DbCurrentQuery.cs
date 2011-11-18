using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using IRU.RTS.WS.Common.Model;
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
            else if (typeof(T) == typeof(invalidationStatusType))
            {
                retVal = Convert.ToInt32(value).AsinvalidationStatusType();
            }

            return retVal;
        }

        public void GetInvalidatedCarnets(DateTime from, DateTime to, int minTIRCarnetNumber, int offset, int count, ref stoppedCarnetsType resultStoppedCarnets)
        {
            using (DbCommand scmd = GetDbCommand(SQLCommandHelper.GetSQLCommandString("Queries.GetInvalidatedCarnets.sql")))
            {
                AddParameter(scmd, "MinTIRCarnetNumber", System.Data.DbType.Int32, minTIRCarnetNumber);
                AddParameter(scmd, "DateFrom", System.Data.DbType.DateTime, from);
                AddParameter(scmd, "DateTo", System.Data.DbType.DateTime, to);
                AddParameter(scmd, "Offset", System.Data.DbType.Int32, offset);
                AddParameter(scmd, "Count", System.Data.DbType.Int32, count);

                using (DbDataReader sdr = scmd.ExecuteReader())
                {
                    resultStoppedCarnets.Total.Count = -1;
                    resultStoppedCarnets.StoppedCarnets.Offset = -1; resultStoppedCarnets.StoppedCarnets.Offset = (int)offset;
                    resultStoppedCarnets.StoppedCarnets.EndReached = true;

                    int iRec = 0;
                    while (sdr.Read())
                    {
                        iRec = GetValue<int>(sdr, "RowNumber");

                        stoppedCarnetType sc = new stoppedCarnetType();

                        sc.CarnetNumber = GetValue<TIRCarnet>(sdr, "Number").CarnetNumber;
                        sc.ExpiryDate = GetValue<DateTime>(sdr, "ExpiryDate");
                        sc.Association = GetValue<string>(sdr, "IssuingAssociation");
                        sc.Holder = GetValue<string>(sdr, "Holder");
                        sc.DeclarationDate = GetValue<DateTime>(sdr, "DateOfDeclaration");
                        sc.InvalidationDate = GetValue<DateTime>(sdr, "DateOfInvalidation");
                        sc.InvalidationStatus = (invalidationStatusType)(-1);
                        sc.InvalidationStatus = GetValue<invalidationStatusType>(sdr, "MotiveCode");                            

                        resultStoppedCarnets.StoppedCarnets.StoppedCarnet.Add(sc);
                    }
                    resultStoppedCarnets.StoppedCarnets.Count = resultStoppedCarnets.StoppedCarnets.StoppedCarnet.Count;                    

                    if ((sdr.NextResult()) && (sdr.Read()))
                    {
                        resultStoppedCarnets.Total.Count = GetValue<int>(sdr, "CountOfFoundStoppedCarnets");
                        resultStoppedCarnets.Total.From = GetValue<DateTime>(sdr, "MinOfDateOfInvalidation");
                        resultStoppedCarnets.Total.To = GetValue<DateTime>(sdr, "MaxOfDateOfInvalidation");
                        resultStoppedCarnets.StoppedCarnets.EndReached = (iRec == resultStoppedCarnets.Total.Count);
                    }
                }
            }
        }
    }
}
