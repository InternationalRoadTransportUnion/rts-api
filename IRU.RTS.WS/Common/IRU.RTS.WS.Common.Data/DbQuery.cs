using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace IRU.RTS.WS.Common.Data
{
    public class DbQuery: IDisposable
    {
        protected DbConnection _scon = null;
        protected int _scmdTimeout;
        protected DbCommandHelper _sqlCmdHlp;

        public virtual DbCommandHelper SQLCommandHelper
        {
            get { return _sqlCmdHlp; }
        }

        public DbQuery(DbConnection dbConnection)
            : this(dbConnection, -1)
        {
        }

        public DbQuery(DbConnection dbConnection, int commandTimeout)
        {
            _scon = dbConnection;
            _scmdTimeout = commandTimeout;
            AfterConstruction();
        }

        public virtual void AfterConstruction()
        {
            _sqlCmdHlp = new DbCommandHelper(this);
        }

        protected virtual DbCommand GetDbCommand(string cmdText)
        {
            DbCommand scmd = _scon.CreateCommand();
            scmd.CommandText = cmdText;
            if (_scmdTimeout > -1)
                scmd.CommandTimeout = _scmdTimeout;
            if (_scon.State == System.Data.ConnectionState.Broken)
                _scon.Close();
            if (_scon.State == System.Data.ConnectionState.Closed)
                _scon.Open();

            return scmd;
        }

        protected virtual object GetValue<T>(object value)
        {
            object retVal = value;

            if (typeof(T) == typeof(String))
            {
                retVal = Convert.ToString(value);
            }
            else if (typeof(T) == typeof(int))
            {
                retVal = Convert.ToInt32(value);
            }
            
            return retVal;
        }

        protected T GetValue<T>(DbDataReader dbDataReader, string columnName)
        {
            object oVal = dbDataReader[columnName];

            if (oVal == DBNull.Value)
            {
                oVal = default(T);
            }
            else
            {
                if ((oVal.GetType() == typeof(DateTime)) && (((DateTime)oVal).Kind == DateTimeKind.Unspecified))
                {
                    oVal = DateTime.SpecifyKind((DateTime)oVal, DateTimeKind.Local);
                }

                if (oVal.GetType() != typeof(T))
                {
                    oVal = GetValue<T>(oVal);
                }
            }

            return (T)oVal;
        }

        protected void AddParameter(DbCommand dbCommand, string parameterName, System.Data.DbType dbType, object value)
        {
            DbParameter dbp = dbCommand.CreateParameter();
            dbp.ParameterName = parameterName;
            dbp.DbType = dbType;
            dbp.Value = value;
            dbCommand.Parameters.Add(dbp);
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (_scon.State == System.Data.ConnectionState.Open)
                _scon.Close();

            _scon.Dispose();
        }

        #endregion
    }
}
