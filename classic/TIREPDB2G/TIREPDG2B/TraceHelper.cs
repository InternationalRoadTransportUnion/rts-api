using System;
using System.Diagnostics;
namespace TraceHelper
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class TraceHelper
	{
		public static TraceSwitch EAITraceSwitch = 
			new TraceSwitch("General", "Entire Application");


		public static void TraceMessage(bool tSwitch, string sMessage)
		{
			string sMessageToWrite = DateTime.Now.ToString("\r\n dd/MMM/yyyy HH:mm:ss:fff tt") + " : " + sMessage ;
			Trace.WriteLineIf(tSwitch,sMessageToWrite);
		
		}

		public TraceHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
