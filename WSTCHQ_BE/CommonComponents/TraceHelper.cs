using System;
using System.Diagnostics;

namespace CommonComponents
{

	/// <summary>
	/// This class is directly referenced by users and used to Trace into Files using the .NET Tracing infrastructure System.Diagnostics.Trace.
	/// This requires the .Config file of the host EXE to have relevant entries to Enable tracing. Sample Config Section
	/// <code>
	/// <system.diagnostics>
	///<switches>
	///<add name="RTSTraceSwitch" value="1" />
	///</switches>
	///<trace autoflush="true" indentsize="4">
	///<listeners>
	///<add name="RTS" type="System.Diagnostics.TextWriterTraceListener" initializeData="c:\LogFile.log" />
	///</listeners>
	///</trace>
	///</system.diagnostics>
	/// 
	/// </code>
	/// </summary>
	public class TraceHelper
	{

		/// <summary>
		/// Do nothing
		/// </summary>
		public TraceHelper()
		{
			
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tracingSwitch"></param>
		/// <param name="MessageToTrace"></param>
		public void TraceToFile ( TraceSwitch tracingSwitch, string MessageToTrace)
		{
		
		}

	}
}
