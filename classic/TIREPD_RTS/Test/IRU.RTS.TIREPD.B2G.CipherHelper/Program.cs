using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IRU.RTS.TIREPD.B2G.CipherHelper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
