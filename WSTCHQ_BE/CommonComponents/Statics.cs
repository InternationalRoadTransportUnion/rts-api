using System;
using System.Diagnostics;

namespace IRU.RTS.CommonComponents
{

	
	

	/// <summary>
	/// Summary description for Statics.
	/// </summary>
	public class Statics
	{
		/// <summary>
		/// IRUTraceSwitch used to trace information using System.Diagnostics.Trace
		/// </summary>
		public static TraceSwitch IRUTraceSwitch = new TraceSwitch("General", "Entire Application");
		
		public Statics()
		{
			//
			// TODO: Add constructor logic here
			//
			//IRUTraceSwitch.Level=TraceLevel.Verbose;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="condition"></param>
		/// <param name="message"></param>
		public static void IRUTrace(object Sender,bool condition,string message)
		{
			string logMessage = string.Format("{0},{1} , [ {2}/{3} ],[ {4} ]",
				Sender.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff tt"),
                System.Threading.Thread.CurrentThread.Name, System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), message);
			Trace.WriteLineIf(condition ,logMessage);
		}

		
		/// <summary>
		/// Formats Error string for tracing purpose
		/// </summary>
		/// <param name="funcName">Function Name in which error has occured</param>
		/// <param name="errDescription">Description of Error</param>
		/// <param name="errMessage">Error Message as given by Exception caught</param>
		/// <param name="errStack">Error stack as in Exception caught</param>
		/// <returns>Formatted Error String</returns>
		public static string FormatErrorString(string funcName,string errDescription,string errMessage,string errStack)
		{
			string strError ;
			strError = "[Function: " + funcName + " ] \n";
			strError += "[Description: " + errDescription + " ] \n";
			strError += "[Error: " + errMessage + " ] \n";
			strError += "[Stack: " + errStack + " ] \n";
			return strError ;
		}

		/// <summary>
		/// Formats Error string for tracing purpose
		/// </summary>
		/// <param name="funcName">Function Name in which error has occured</param>
		/// <param name="errDescription">Description of Error</param>
		/// <returns>Formatted Error String</returns>
		public static string FormatErrorString(string funcName,string errDescription)
		{
			string strError ;
			strError = "[Function: " + funcName + " ] \n";
			strError += "[Description: " + errDescription + " ] \n";
			return strError ;	
		}

	}
}

