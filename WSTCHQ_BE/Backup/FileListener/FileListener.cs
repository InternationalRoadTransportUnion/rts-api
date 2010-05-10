using System;
using System.IO;
using System.Xml;
using IRU.CommonInterfaces;
using System.Threading;
using System.Collections;


namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// The FileSystemListener will poll for files in the file drop locations esp. those copied over by DoubleAgent. The Component is reusable and can be used in the WSSCscc Client,  WSST_Processor and the WSSCscc Processor to pick up delivered files.
	/// The Class uses a combination of FileSystemWatcher [dropped later do only scheduled scan] and active scanning to pick up files. It will use 
	/// It will deliver Files to an IActiveObject as an byte array using the enqueue method. The Configuration parameters are as follows.
	/// <list type="table">
	/// <item><term>DeliverToPlugin</term><description>Name of the IAciveObjectPlugin to which to deliver the file contents</description></item>
	/// <item><term>PollFolderLocation</term><description>The Directory to poll files for</description></item>
	/// <item><term>WorkingfolderLocation</term><description>The File is moved in the Working folder location so that it can be read </description></item>
	/// <item><term>ArchiveFolderLocation</term><description>After the file is read the file is moved to the Archive Folder Location</description></item>
	/// <item><term>FilePattern</term><description>the File extension to poll for e.g. *.c*</description></item>
	/// <item><term>ForceScanItnerval</term><description>Scan Interval in minute</description></item>
	/// </list>
	/// 
	/// A PluginManager can load multiple FileSystemListeners to poll for different folders and different file extensions and maybe deliver to different ActiveObjects
	/// </summary>
	public class FileSystemListener:IPlugIn,IRunnable
	{
		private IPlugInManager m_PluginManager;
		private string m_PluginName;
		private string m_DeliverToPlugin, m_PollFolderLocation, m_WorkingfolderLocation;//="g:\SafeTIRWorkingfolder";
		private string m_ArchiveFolderLocation, m_ErrorFolderLocation, m_FilePattern;
		private int m_ForceScanInterval; //in seconds
		private System.Threading.ManualResetEvent m_ShutDownEvent;

		private int m_MaxConcurrentProcessCount;// max files to be processed at one time

		private int m_CurrentProcessCount;// current number of files getting processed

		private IActiveObject m_TargetActiveObject; //where to send the file contents

		// FileSystemWatcher m_watcher;// mandar : removed as not very dependable,
		Thread m_ScheduledCheckThread;

		/// <summary>
		/// Do Nothing
		/// </summary>
		public FileSystemListener()
		{
			m_MaxConcurrentProcessCount = 0;
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members

		/// <summary>
		/// Called in by the plugin Manager. It stores the configuration parameters in internal variables
		/// </summary>
		/// <param name="pluginManager"></param>
		public void Configure(IPlugInManager pluginManager)
		{
			m_PluginManager = pluginManager;
///			< FileSystemListener DeliverToPlugin="WSST_Processor" PollFolderLocation="g:\SafeTirInDrop" WorkingfolderLocation="g:\SafeTIRWorkingfolder"
///			ArchiveFolderLocation="g:\SafeTIRUploadFileArchive" FilePattern="*.cif" ForceScanInterval="3"></FileSystemListener>

			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./FileSystemListener");


				m_DeliverToPlugin  = parameterNode.Attributes["DeliverToPlugin"].InnerXml;

				//dont fetch the plugin yet as the PM might not have loaded this plugin do it in the Start Method


				m_PollFolderLocation = parameterNode.Attributes["PollFolderLocation"].InnerXml;

				m_WorkingfolderLocation = parameterNode.Attributes["WorkingFolderLocation"].InnerXml;

				m_ArchiveFolderLocation = parameterNode.Attributes["ArchiveFolderLocation"].InnerXml;

				m_FilePattern = parameterNode.Attributes["FilePattern"].InnerXml;

				m_ForceScanInterval = int.Parse(parameterNode.Attributes["ForceScanInterval"].InnerXml);

				m_ErrorFolderLocation = parameterNode.Attributes["ErrorFolderLocation"].InnerXml;

				m_MaxConcurrentProcessCount=int.Parse(parameterNode.Attributes["MaxConcurrentProcessCount"].InnerXml);

			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure "
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure "
					+ e.Message);
				throw e;
			}


		}

		/// <summary>
		/// Calls Stop
		/// </summary>
		public void Unload()
		{
			Stop();
		}

		/// <summary>
		/// Name of the Plugin Set by the Plugin Manager
		/// </summary>
		public string PluginName
		{
			get
			{
		
				return m_PluginName;
			}
			set
			{
				m_PluginName= value;
			}
		}

		#endregion

		#region IRunnable Members

		/// <summary>
		/// Get reference to the IActiveObject using IPluginManager.GetPluginByName.
		/// Setup the Shutdown event (ManualResetEvent) and reset it
		/// Call the setupFileSystemListener function
		/// </summary>
		public void Start()
		{
			m_TargetActiveObject = (IActiveObject) m_PluginManager.GetPluginByName(m_DeliverToPlugin);
			m_ShutDownEvent = new ManualResetEvent(false);
			
			//moved this out to the main thread, if there is any exception it used to prevent the PM from loading

			//SetupFileSystemListener();

			ThreadStart ts = new ThreadStart(ScheduleScan);

			m_ScheduledCheckThread = new Thread(ts);

			m_ScheduledCheckThread.Name = m_PluginName + " thread";

			m_ScheduledCheckThread.Start();
		}


		/// <summary>
		/// Set the Shutdown event and disable FileSystemEventWatcher
		/// </summary>
		public void Stop()
		{
			m_ShutDownEvent.Set();

		}


		#endregion

		#region Implementation

		/// <summary>
		/// The SetupFileSystemListener does the following
		/// <list type="bullet">
		/// <item>Call FullScan function</item>
		/// <item>Register Callback for the FileSystemWatcher for the created event
		/// <code>
		///        FileSystemWatcher watcher = new FileSystemWatcher();
///		watcher.Path = args[1];
///				watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite 														  | NotifyFilters.FileName | NotifyFilters.DirectoryName;
		
		///watcher.Filter = "*.c*"

		/// Add event handlers.
		
		///watcher.Created += new FileSystemEventHandler(OnCreated);
		/// // Begin watching.
		///watcher.EnableRaisingEvents = true;

		/// 
		/// </code>
		/// </item>
		/// <item> Start the thread for timely scanning by setting up the ScheduleScan</item>
		/// </list>
		/// </summary>
		private void SetupFileSystemListener()
		{
			FullScan(m_WorkingfolderLocation); // catch up with files existing in the Working Folder folder

			FullScan(m_PollFolderLocation ); // catch up with files existing in the drop folder
/*
			m_watcher = new FileSystemWatcher();
			m_watcher.Path = m_PollFolderLocation;
			m_watcher.NotifyFilter = NotifyFilters.CreationTime;														  
		
			m_watcher.Filter = m_FilePattern;
*/

			/// Add event handlers.
		/*
			m_watcher.Created += new FileSystemEventHandler(OnCreated);
			m_watcher.Changed += new FileSystemEventHandler(OnCreated);
			
			// Begin watching.
			EnableWatcher();
			*/
		
		}


		/// <summary>
		/// ThreadFunction to do a periodic scan
		/// <list type="bullet">
		/// <item>Start a While Loop and wait (WaitHandle.WaitOne) on the Shutdown event for the ForceScanInterval amount of time as timeout</item>
		/// <item>In case Wait breaks for shutdown event being set then break out of the while loop</item>
		/// <item>Else Disable the FileSystem watcher event raising by <c>watcher.EnableRaisingEvents = false;</c></item>
		/// <item>Call FullScan Method</item>
		/// <item>Re-enable the FilesystemWathcerEvent raising</item>
		/// </list>
		/// </summary>
		private void ScheduleScan()
		{

			//mandar - added cleanup of the working folder- moved from start() method
			try
			{
				SetupFileSystemListener();
			}
			catch( Exception ex)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"File Watcher thread exception swallowed ." + ex.Message + "\r\n" +  ex.StackTrace);
			}
			finally
			{
			
			
			}
			while (true)
			{
				//wait on event convert into milliseconds
				bool bDidFire;
				bDidFire= m_ShutDownEvent.WaitOne( m_ForceScanInterval * 1000,false);

				if (bDidFire)
					break; //get out of the loop

				#region Main Processing
				try
				{
					//DisableWatcher();
					lock (this)  // so that this code does not run when a call back is being processed in filewatcher
					{
						FullScan(m_PollFolderLocation);
					}
				}
				catch (Exception ex)
				{
					//do nothing
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"File Watcher thread exception swallowed ." + ex.Message + "\r\n" +  ex.StackTrace);
				}
				finally
				{
					//start watching again
					//EnableWatcher();
				
				}

				#endregion
			
			}
		
		}

		
//////		/// <summary>
//////		/// Callback function for the Created Event.
//////		/// <list type="bullet">
//////		/// <item>Get the File Name and call ProcessFile by passing in the File name</item>
//////		/// </list>
//////		/// </summary>
//////		/// <param name="source">Ignore this param</param>
//////		/// <param name="e">Contains the name of the File etc.</param>
//////		private  void OnCreated(object source, FileSystemEventArgs e)
//////		{
//////		
//////			lock (this) // prevents the scheduled thread from overallping on this call
//////			{
//////				switch (e.ChangeType )
//////				{
//////						case WatcherChangeTypes.Created:
//////						string filePath = e.FullPath;
//////						//ProcessFile(filePath);
//////							try
//////							{
//////								ProcessFile(filePath);
//////							}
//////							catch (Exception ioex)
//////							{
//////								string file = e.Name ;
//////								File.Move(m_WorkingfolderLocation+"\\"+file, m_ErrorFolderLocation+"\\"+file); 	
//////								Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"File Watcher full scan exception swallowed ." + ioex.Message + "\r\n" +  ioex.StackTrace);
//////							}
//////
//////						break;
//////						;
//////					default:
//////			
//////						break;
//////				}
//////			}
//////					
//////		
//////		}
//////

		delegate void ProcessFileDel(string FileName);

		/// <summary>
		/// Uses the DirectoryInfo and FileInfo class to get all the files in the PollFolderLocation which are pending to be delivered and pass it on to the ProcessFile Method
		/// <code>
		/// DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
		//// Create an array representing the files in the current directory.
///		FileInfo[] fi = di.GetFiles(pattern);

		/// </code>
		/// 
		/// </summary>
		private void FullScan(string FolderPath)
		{
			//
//			DirectoryInfo di = new DirectoryInfo(FolderPath);
//			//Create an array representing the files in the current directory.
//			FileInfo[] fiList = di.GetFiles(m_FilePattern);
			int availableCount;

			lock (this)
			{
				availableCount = m_MaxConcurrentProcessCount-m_CurrentProcessCount;
			
			}
			
			if (availableCount<=0)
			{	Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"Max concurrent count reached . Current count is " + m_CurrentProcessCount);
				return;
			}

			DirectoryInfo di = new DirectoryInfo(FolderPath);
			//Create an array representing the files in the current directory.
			FileInfo[] fiList = di.GetFiles(m_FilePattern);
			

			foreach (FileInfo fii in fiList)
			{
				if (availableCount--==0)
					break;

				string fileName = fii.FullName;
				ProcessFileDel pfd = new ProcessFileDel(ProcessFile);

				pfd.BeginInvoke(fileName,null,null); //begin the call on a thread in the inbuilt thread pool
				//we dont bother when it ends
			

			}

		
		}

/*
		private void DisableWatcher()
		{
		
			lock (this) // this needs to be synched
			{
				m_watcher.EnableRaisingEvents=false;
			
			}
		
		}


		private void EnableWatcher()
		{
		
			lock (this) // this needs to be synched
			{
				m_watcher.EnableRaisingEvents=true;
			
			}
		
		}
*/

/// <summary>
/// This is the main FileProcessor.
///<list type="button">
///<item>Wrap a File.Move in an try catch to move the file to the WorkingFolder location</item>
///<item>Read the file into an Byte Array</item>
///<item>Close The FileStream</item>
///<item>Create a Hashtable and add three values 
///a: The Name of the file against 'FileName' key 
///b: byte array against eh 'FileContents' Key
///c: The name of the directory against the 'FolderKey'</item>
///<item>Enqueue the Hashtable to the Accepting reference to the IActiveObject</item>
///<item>Move the File to the Archive Folder</item>
///</list> 
/// </summary>
/// <param name="sFilePath"></param>
		private void ProcessFile(string sFilePath)
		{
			//extract the file name from path
			string fileName = Path.GetFileName( sFilePath);
			try
			{
				lock (this)
				{
					m_CurrentProcessCount++;
			
				}
				string destinationPath = m_WorkingfolderLocation + "\\" + fileName;

				if (sFilePath != destinationPath) //we are processing the working folder during startup
				{
					//try
					//{
						if (File.Exists (sFilePath))
						{
							File.Move(sFilePath,destinationPath);
						}
						else
						{
							return;
						}
					//}
					//catch(Exception ioex)
					//{
					//	Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning ,"1 File Watcher full scan exception swallowed ." + ioex.Message + "\r\n" +  ioex.StackTrace);
					//	return;
					//}
				}
			
				// PRocess file
				//read the file
				FileStream fs = new FileStream(destinationPath,FileMode.Open,FileAccess.Read);
				byte[] fileContents = null;
				try
				{
					fileContents = new byte[fs.Length];
					fs.Read(fileContents,0,fileContents.Length);
				}
				finally
				{
					fs.Close();
				}

				//'FileName' key b: byte array against eh 'FileContents' Key c: The name of the directory against the 'FolderKey' 
				Hashtable htEnqueued = new Hashtable();
				htEnqueued.Add("FileName",fileName);
				htEnqueued.Add("FileContents",fileContents);
				htEnqueued.Add("Folder",Path.GetDirectoryName(sFilePath));
				m_TargetActiveObject.Enqueue(htEnqueued);

				string archivePath =  m_ArchiveFolderLocation + "\\" + fileName;

				if (File.Exists (archivePath))
				{
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning, "About to delete and replace an existing Archive File " + archivePath);
					File.Delete(archivePath);
				
			
				}; //delete if it already exists
				File.Move(destinationPath,archivePath);
			}
			catch (Exception ioex)
			{
				string file = fileName ;
				File.Move(m_WorkingfolderLocation+"\\"+file, m_ErrorFolderLocation+"\\"+file); 	
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning ,"2 File Watcher full scan exception swallowed ." + ioex.Message + "\r\n" +  ioex.StackTrace);
			}
			finally
			{
				lock (this)
				{
					m_CurrentProcessCount--;
			
				}
			
			}
		
		}


		#endregion Implementation
	
	}
}
