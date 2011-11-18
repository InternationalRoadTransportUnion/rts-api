using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace IRU.RTS.WS.Common.Data
{
    public class DbCommandHelper
    {
        private Assembly _callerAssembly;

        public DbCommandHelper(object caller)
        {
            _callerAssembly = Assembly.GetAssembly(caller.GetType());
        }

        public Stream GetSQLCommandStream(string sqlCommandResourceName)
        {
            Stream retVal = _callerAssembly.GetManifestResourceStream(sqlCommandResourceName);
            if (retVal == null)
                retVal = _callerAssembly.GetManifestResourceStream(_callerAssembly.GetName().Name + "." + sqlCommandResourceName);

            return retVal;
        }

        public String GetSQLCommandString(string sqlCommandResourceName)
        {
            using (Stream stm = GetSQLCommandStream(sqlCommandResourceName))
            {
                if (stm == null)
                    return null;
                using (TextReader tr = new StreamReader(stm))
                {
                    return tr.ReadToEnd();
                }
            }
        }
    }
}
