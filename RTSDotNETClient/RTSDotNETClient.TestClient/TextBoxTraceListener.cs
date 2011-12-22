using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RTSDotNETClient.TestClient
{
    public class TextBoxTraceListener : TraceListener
    {
        private TextBox textbox;
        private StringSendDelegate invokeWrite;

        public TextBoxTraceListener(TextBox target)
        {
            this.textbox = target;
            invokeWrite = new StringSendDelegate(SendString);
        }

        public override void Write(string message)
        {
            textbox.Invoke(invokeWrite, new object[] { message });
        }

        public override void WriteLine(string message)
        {
            textbox.Invoke(invokeWrite, new object[] { message + Environment.NewLine });
        }

        private delegate void StringSendDelegate(string message);
        private void SendString(string message)
        {
            textbox.AppendText(message);
        }
    }
}
