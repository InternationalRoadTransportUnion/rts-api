using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace RTSDotNETClient.TestClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new MainForm());
        }

        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception is RTSWebServiceException)
                MessageBox.Show("The web service returned an error.\n\nError " + e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (e.Exception is XsdValidationException)
                MessageBox.Show(e.Exception.Message, "Xsd Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(e.Exception.Message + "\n\nStack Trace:\n\n" + e.Exception.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static MainForm MainForm
        {
            get
            {
                return (MainForm) Application.OpenForms[0];
            }
        }

        public static bool LoadTestData
        {
            get
            {
                if (ConfigurationManager.AppSettings["LoadTestData"] == null)
                    return false;
                return bool.Parse(ConfigurationManager.AppSettings["LoadTestData"]);
            }
        }
    }
}
