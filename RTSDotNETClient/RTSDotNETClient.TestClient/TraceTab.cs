using System.Diagnostics;
using System.Windows.Forms;

namespace RTSDotNETClient.TestClient
{
    public partial class TraceTab : UserControl
    {
        public TraceTab()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            tbTrace.Clear();
        }
        public void ScrollToBottom()
        {
            tbTrace.Focus();
            tbTrace.SelectionStart = tbTrace.Text.Length;
            tbTrace.SelectionLength = 0;
            tbTrace.ScrollToCaret();
        }

        public void InitTrace()
        {
            if (Global.TraceEnabled)
                Trace.Listeners.Add(new TextBoxTraceListener(tbTrace));
        }
    }
}
