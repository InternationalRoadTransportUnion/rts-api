using System;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections;

namespace IRU.CommonInterfaces
{

	/// <summary>
	/// This is the interface for plugin manager implementaion.
	/// It will be used by the host EXE to load the plugins at the start of service/application.
	/// The implementation will be responsible of loading the config file.
	/// </summary>
	public interface IPlugInManager
	{
		/// <summary>
		/// Absolute Path to the Config file.
		/// Set by the host process.
		/// </summary>
        
		string ConfigFile
		{
			set;
			get;
		}
		
		/// <summary>
		/// Returns the SectionNode to a plugin from the Config XML File
		/// </summary>
		/// <param name="sectionName">String corresponding to the name attribute of a Section</param>
		/// 
		/// <returns>Corresponding XML Fragment from the config File</returns>
		XmlNode GetConfigurationSection(string sectionName);

		/// <summary>
		/// The requested instance of the Plugin will be fetched from the internal Hashtables. The user/ casts the returned object to expected Interface or Class
		/// </summary>
		/// <param name="pluginName">Name of the plugin to retreive</param>
		/// <returns>Corresponding XML Fragment from the config File</returns>
		object GetPluginByName(string pluginName);

		/// <summary>
		/// Loads the plugin assemblies which are configured in the config file
		/// </summary>
		void LoadPlugins();

		/// <summary>
		/// Unloads all the loaded plugins. By calling Unload of IPlugin interface
		/// </summary>
		void Unload();

	}
 
	/// <summary>
	/// Any component which does internal processing using independent threads will have to implement IRunnable this will allow the host/plugin manager to control the starting and stopping of the component
	/// </summary>
	public interface IRunnable
	{
		/// <summary>
		///  Starts the processing Thread/s etc.
		/// </summary>
		void Start();

		/// <summary>
		/// Stops the Processing Thread/s, unregisters remoting etc.
		/// </summary>
        
		void Stop();
		
	
	}

	/// <summary>
	/// All of the plugins will implement this interface.
	/// Eg Parsers, Cache, Completeness Checker
	/// </summary>
	public interface IPlugIn
	{
		/// <summary>
		/// Used by PluginManager to configure the plugin. The IPluginManger will pass in "this" so the plugin has reference to the Plugin Manager. The implementation will use this referencet to request the Manager its configuration section and then use it to configure itself. The Plugin is not expected to start any processing threads in this method
		/// </summary>
		/// <param name="pluginManager"> </param>
		void Configure(IPlugInManager pluginManager);

		/// <summary>
		/// Name of the Plugin in the config file. Set by the Plugin Manager
		/// </summary>
		string PluginName
		{
			set;
			get;
		}

		/// <summary>
		/// Used by PluginManager to stop / unload instance
		/// </summary>
		void Unload();


       
	}


	/// <summary>
	/// ICache is interface for the File Cache implementation. Allows the client to access files from the in-memory cache as a XMLDOM or a string
	/// </summary>
	
	public interface ICache
	{
		/// <summary>
		/// The implementation will return the XML file loaded in memory as an XMLDocument
		/// </summary>
		/// <param name="FileName">Name of the File to fetch</param>
		/// <returns>File contents loaded in XmlDOM</returns>
		/// <exception cref="FileNotFoundException">Will throw a FileNotFoundException if File has not been loaded in the cache</exception>
		XmlDocument GetXMLDomFromCache(string FileName);

		/// <summary>
		/// The implementation will return the XML File loaded in memory as an string
		/// </summary>
		/// <param name="sFileName">Name of the File to fetch</param>
		/// <returns>File contents as a string</returns>
		/// <exception cref="FileNotFoundException">Will throw a FileNotFoundException if File has not been loaded in the cache</exception>
		string GetStringFromCache(string sFileName);
	}

	/// <summary>
	/// This Interface will be used by implementors of the (variation of ) ActiveObject design pattern. A client will 	call the enqueue method to pass of job objects into the Active object. The implementation will enqueu into a synchronised in-memory queue. The implementation as well as the client will be responsible for dequeing this object and synchronising the object access.
	/// </summary>
	public interface IActiveObject
	{
		/// <summary>
		/// Enque the the object into the queue
		/// </summary>
		/// <param name="objToEnqueue">This object will be enqueued</param>
		void Enqueue(Object objToEnqueue);

		/// <summary>
		/// Enque the the object to queue. In case the implementation implements multiple queues to receive jobs the client can specify the queue name which should be well known
		/// </summary>
		/// <param name="objToEnqueue">The object to enqueu</param>
		/// <param name="queueName">Name of the queue to enqueue the object into</param>
		void Enqueue(Object objToEnqueue, string queueName);

		
	}

	/// <summary>
	/// Exposes typical database helper utilitity functions
	/// </summary>
	public interface IDBHelper
	{

		/// <summary>
		/// Opens the connection on the member Connection variable
		/// </summary>
		void ConnectToDB();


		/// <summary>
		/// Opens  a Database transaction against the SQLConnection and stores the reference in member variables
		/// </summary>
		void  BeginTransaction();

		/// <summary>
		/// In case the member SQLTransaction variable is not null calls Rollback
		/// </summary>
		void  RollbackTransaction();

		/// <summary>
		/// In case the member SQLTransaction variable is not null calls Commit
		/// </summary>
		void  CommitTransaction();


		/// <summary>
		/// Fires the provided SQL against the Connection using an SQLDataAdapter . Creates a new DataSet and fills a table of the name specified in TableName. In case the SQL Transaction object is not NULL it will be used while executing the SQL
		/// </summary>
		/// <param name="SQLStatement">SQL to execute</param>
		/// <param name="TableName">Name of the DataTable to fill in the DataSet</param>
		/// <returns>new DataSet</returns>
		DataSet GetDataSet(string SQLStatement, string TableName)
			;
		/// <summary>
		/// Fires the provided SQL against the Connection using an SQLDataAdapter . USes the DataSet  DataSettoFill and fills a table of the name specified in TableName. In case the SQL Transaction object is not NULL it will be used while executing the SQL
		/// </summary>
		/// <param name="SQLStatement">SQL to execute</param>
		/// <param name="TableName">Name of the DataTable to fill in the DataSet</param>
		/// <param name="DataSetToFill">The dataset will be passed in by the client</param>
		
		void FillDataSetTable(string SQLStatement, DataSet DataSetToFill, string TableName);

		/// <summary>
		/// Fills a table in a dataset, accepts a sqlcommand
		/// </summary>
		/// <param name="SQLCommand">select command</param>
		/// <param name="DataSetToFill"></param>
		/// <param name="TableName"></param>
		void FillDataSetTable(IDbCommand SQLCommand, DataSet DataSetToFill, string TableName);


		  /// <summary>
		  /// Executes the SQL and returns the DataReader
		  /// </summary>
		  /// <param name="SQLStatement">Select statement to execute</param>
		  /// <param name="Behaviour">Command Behaviour</param>
		  /// <returns>IDataReader</returns>
		IDataReader GetDataReader( string SQLStatement, System.Data.CommandBehavior Behaviour);
	
	
		/// <summary>
		/// Executes the Command and returns the DataReader
		/// </summary>
		/// <param name="CommandObject">Select Command Object</param>
		/// <param name="Behaviour">Command Behaviour</param>
		/// <returns>IDataReader</returns>
		IDataReader GetDataReader( IDbCommand CommandObject, System.Data.CommandBehavior Behaviour);
	

		/// <summary>
		/// Executes the query and returns the count of rows affected
		/// </summary>
		/// <param name="SQLStatement"></param>
		/// <returns>count of rows affected</returns>
		int ExecuteNonQuery( string SQLStatement);


		/// <summary>
		/// Executes the Command Object. Used to pass in Isert/Updates which require to update binary data.
		/// </summary>
		/// <param name="CommandObject">The SQL Command object on which the execute will be fired.</param>
		/// <returns>count of rows affected</returns>
		int ExecuteNonQuery( IDbCommand CommandObject);




		/// <summary>
		/// Executes the ExecuteScaler method and used to retrieve single value
		/// </summary>
		/// <param name="SQLStatement"></param>
		/// <returns></returns>
		object ExecuteScaler(string SQLStatement);

        /// <summary>
        /// Executes the ExecuteScaler method and used to retrieve single value
        /// </summary>
        /// <param name="CommandObject">The SQL Command object on which the execute will be fired.</param>
        /// <returns></returns>
        object ExecuteScaler(IDbCommand CommandObject);

		/// <summary>
		/// IDisposable implementation. Close the Connection
		/// </summary>
		/// <param name="IsDisposing">Not Used</param>
		void Dispose( bool IsDisposing);

		/// <summary>
		/// Close DB connection
		/// </summary>
		void Close();

		
	}

	/// <summary>
	/// Interface wraps behaviour of a notification handler  
	/// </summary>
	public interface INotificationHandler
	{
		/// <summary>
		/// Implementation sends out the notification
		/// </summary>
		/// <param name="NotificationMessage">String containing the notification message </param>
		/// <param name="NotoficationParameters">Hashtable containing Parameters required to notify. e.g. SMTPAddress, SMTPHost, SMTPSubject etc.</param>
		 void Notify(string NotificationMessage, Hashtable NotoficationParameters);
	
	
	}

	/// <summary>
	/// Implemented by DBHelper Factory class which creates DB helpers for particular Database
	/// </summary>
	public interface IDBHelperFactory
	{
		/// <summary>
		/// Creates a new DBhelper and returns the IDBHelper
		/// </summary>
		/// <param name="DBName">Name of the DBHelper</param>
		/// <returns>DBHelper's IDBHelper</returns>
		IDBHelper GetDBHelper(string DBName);
	
	}

	
}
