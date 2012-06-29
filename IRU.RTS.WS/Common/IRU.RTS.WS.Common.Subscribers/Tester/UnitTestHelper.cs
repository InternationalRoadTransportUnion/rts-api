using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IRU.RTS.WS.Common.Subscribers.Tester
{
    public class UnitTestHelper
    {
        public static System.Reflection.Assembly TestAssembly
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly();
            }
        }

        public static Stream GetTestStream(string testfile)
        {
            return TestAssembly.GetManifestResourceStream("IRU.RTS.WS.Common.Subscribers.Tester." + testfile);
        }

        public static String GetTestString(string testfile)
        {
            using (Stream stmOut = GetTestStream(testfile))
            using (TextReader trOut = new StreamReader(stmOut, true))
            {
                return trOut.ReadToEnd();
            }
        }

        public static byte[] GetTestBytes(string testfile)
        {
            using (Stream stmOut = GetTestStream(testfile))
            {
                byte[] abRes = new byte[stmOut.Length];
                stmOut.Read(abRes, 0, abRes.Length);
                return abRes;
            }
        }
    }
}
