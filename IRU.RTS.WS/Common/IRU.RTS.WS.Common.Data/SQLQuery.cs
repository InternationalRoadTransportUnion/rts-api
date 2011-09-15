using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace IRU.RTS.WS.Common.Data
{
    public class SQLQuery: IDisposable
    {
        protected SqlConnection _scon = null;
        protected int _scmdTimeout;
        protected SQLCommandHelper _sqlCmdHlp;

        public virtual SQLCommandHelper SQLCommandHelper
        {
            get { return _sqlCmdHlp; }
        }

        public SQLQuery(string connectionString): this(connectionString, -1)
        {
        }

        public SQLQuery(string connectionString, int commandTimeout)
        {
            _scon = new SqlConnection(connectionString);
            _scmdTimeout = commandTimeout;
            AfterConstruction();
        }

        public virtual void AfterConstruction()
        {
            _sqlCmdHlp = new SQLCommandHelper(this);
        }

        protected virtual SqlCommand GetSqlCommand(string cmdText)
        {
            SqlCommand scmd = new SqlCommand(cmdText, _scon);
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

        protected T GetValue<T>(SqlDataReader sqlDataReader, string columnName)
        {
            object oVal = sqlDataReader[columnName];

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
