using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IRU.RTS.WS.Common.Model;
using IRU.RTS.WS.Common.Business;
using IRU.RTS.WS.Common.Data;

namespace IRU.RTS.WS.Common.Data.Current
{
    public class SQLCurrentQuery : SQLQuery, IDisposable
    {
        public SQLCurrentQuery(): base(Properties.Settings.Default.CurrentDB, Properties.Settings.Default.SQLCommandTimeout)
        {            
        }

        public SQLCurrentQuery(string connectionString): base(connectionString, -1)
        {
        }

        public SQLCurrentQuery(string connectionString, int commandTimeout): base(connectionString, commandTimeout)
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
            using (SqlCommand scmd = GetSqlCommand(SQLCommandHelper.GetSQLCommandString("Queries.GetInvalidatedCarnets.sql")))
            {
                scmd.Parameters.Add("MinTIRCarnetNumber", System.Data.SqlDbType.Int).Value = minTIRCarnetNumber;
                scmd.Parameters.Add("DateFrom", System.Data.SqlDbType.DateTime).Value = from;
                scmd.Parameters.Add("DateTo", System.Data.SqlDbType.DateTime).Value = to;
                scmd.Parameters.Add("Offset", System.Data.SqlDbType.Int).Value = offset;
                scmd.Parameters.Add("Count", System.Data.SqlDbType.Int).Value = count;

                using (SqlDataReader sdr = scmd.ExecuteReader())
                {
                    resultStoppedCarnets.Total.From = from;
                    resultStoppedCarnets.Total.To = to;
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
