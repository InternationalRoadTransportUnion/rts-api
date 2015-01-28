using System;
using IRU.CommonInterfaces;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;


namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// DBHelperFactory is class factory to generate a DBHelper instance
	/// ...
	/// 
	/// </summary>
	public class DBHelperFactory:IPlugIn,IDBHelperFactory
	{
		internal Hashtable m_connectionHash;
		internal Hashtable m_schemaHash;
		private IPlugInManager m_PluginManager;
		private string m_PluginName;
		
		#region IPlugIn Members
		/// <summary>
		/// Implements IPlugin.Configure. Will access the pluginmanager to read the config file XML Fragment. The XML Section is loaded in XMLDOM and  name value pairs corresponding to DB name and connection string is loaded into a static HashTable. It will also use the CryptoHelper to decrypt the settings.
		/// Sample Section
		/// <Section name="DBHelperFactory">
		///		<ConnectionStrings>
		///		<DB>
		///		<Name> CuteWiseDB</Name>
		///		<!-- Base64 encoded -->
		///		<ConnectionString>bb4499ds0028740010038749020029....</ConnectionString>
		///		</DB>
		///		<DB>
		///		<Name> RTS_Users</Name>
		///		<ConnectionString>hdjjakksasdasdbb4499ds0028740010038749020029....</ConnectionString>
		///		</DB>
		///		</ConnectionStrings>		
		///
		///		</Section>
		///		The Decryption pseudoCode:
		///		<list type="bullet">
		///		<item>
		///		<description>Read Value of ConnectionString</description>
		///		</item>
		///		<item>
		///		<description>Decode the Base64 string to get byte to get array of Bytes [using System.Convert.FromBase64String]</description>
		///		</item>
		///		<item>
		///		<description>Pass the bytes and hardcoded key [Needs to be finalised by IRU team, on how to use existing CryptoLibrary] to CryptoHelper to decrypt and get back decrypted bytes.</description>
		///		</item>
		///		<item>
		///		<description>Use System.Text.UnicodeEncoding get back the unencrypted Unicode String</description>
		///		</item>
		///		
		///		</list>
		///		
		/// </summary>
		/// <param name="pluginManager">Reference to the Hosting PluginManager</param>
		public void Configure(IPlugInManager PluginManager)
		{
			m_PluginManager = PluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./ConnectionStrings");


				XmlNodeList xDBList = parameterNode.SelectNodes("./DB");


				//loop through this list and populate hashtable

				foreach (XmlNode xDBNode in xDBList)
				{
					string dbName = xDBNode.Attributes["Name"].InnerText;
					
					string connectionString = xDBNode.SelectSingleNode("./ConnectionString").InnerXml;				
					m_connectionHash.Add(dbName,connectionString);

					XmlNode xnSchemaName = xDBNode.SelectSingleNode("./SchemaName");
					string schemaName = xnSchemaName != null ? xnSchemaName.InnerXml : "dbo";
					m_schemaHash.Add(dbName, schemaName);
				}


			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of DBHelperFactory"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of DBHelperFactory"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of DBHelperFactory"
					+ e.Message);
				throw e;
			}

		}
		/// <summary>
		/// Do nothing
		/// </summary>
		public void Unload()
		{
			m_connectionHash = null;
			m_schemaHash = null;
		}
		/// <summary>
		/// Set to DBHelper by PluginManager, this name has to be used as other Plugins will refer to this by "DBHelper"
		/// </summary>
		public string PluginName
		{
			get
			{
				// TODO:  Add DBHelper.PluginName getter implementation
				return m_PluginName;
			}
			set
			{
				m_PluginName = value;
				// TODO:  Add DBHelper.PluginName setter implementation
			}
		}

		#endregion

		public DBHelperFactory()
		{
			m_connectionHash = new Hashtable();
			m_connectionHash = Hashtable.Synchronized(m_connectionHash);

			m_schemaHash = new Hashtable();
			m_schemaHash = Hashtable.Synchronized(m_schemaHash);
		}

		/// <summary>
		/// Is the actual Factory method which creates a new instance of DBHelper class. It passes on the Connection string corresponding to DBName as parameter to the constructor of DBHelper.
		/// </summary>
		/// <param name="DBName">Value corresponding to the name valu pair in the Configuration file</param>
		/// <returns>A new instance of DBHelper</returns>
		/// <exception cref="ApplicationException">In case teh DBName does not correspond to value stored in the config gile</exception>
		public IDBHelper GetDBHelper(string DBName)
		{
			string sConnectionString = null;
			string sSchemaName = null;
			try
			{
				sConnectionString = (string)m_connectionHash[DBName];
				sSchemaName = (string)m_schemaHash[DBName];
			}
			catch
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"Unable to fetch connection string for DB " + DBName);
				throw;
			}

			DBHelper dbhToReturn = new DBHelper(sConnectionString, sSchemaName);
		
			return dbhToReturn;
		}
	}
/// <summary>
/// The DBHelper class provides utility functions to access the database. This class holds state (Connection object) between calls.
/// The Transaction variables are provided in case the class is not used in COM+ and the client requires explicit transaction control.

/// </summary>
	public class DBHelper:IDisposable,IDBHelper
	{

		private SqlTransaction m_sqlTransaction;
		private string m_connectionString;
		private SqlConnection m_sqlConnection;
		private string m_schemaName;
		private bool m_IsConnected;
		private bool m_isTransacted;


		/// <summary>
		/// Stores the connection string in member variables and initialises other members.
		/// </summary>
		/// <param name="ConnectString">Connection string used by SQLConnection.Open</param>
		/// <param name="SchemaName">Name of the Schema of DB</param>
		public DBHelper(string ConnectString, string SchemaName)
		{
			m_sqlConnection = new SqlConnection();
			m_connectionString = ConnectString;
			m_sqlConnection.ConnectionString=m_connectionString;
			m_schemaName = SchemaName;
			m_IsConnected=false;
			m_isTransacted=false;
		}

		/// <summary>
		/// Stores the connection string in member variables and initialises other members.
		/// </summary>
		/// <param name="ConnectString">Connection string used by SQLConnection.Open</param>
		public DBHelper(string ConnectString)
			: this(ConnectString, "dbo")
		{
		}

		/// <summary>
		/// Opens the connection on the member Connection variable
		/// </summary>
		public void ConnectToDB()
		{
			m_sqlConnection.Open();
			m_IsConnected=true;
			m_isTransacted=false;
		}


		/// <summary>
		/// Opens  a Database transaction against the SQLConnection and stores the reference in member variables
		/// </summary>
		public void  BeginTransaction()
		{
			m_isTransacted=false;
			if (m_IsConnected)
			{
				m_sqlTransaction= m_sqlConnection.BeginTransaction();
				m_isTransacted=true;
			}
		
		}

		/// <summary>
		/// In case the member SQLTransaction variable is not null calls Rollback
		/// </summary>
		public void  RollbackTransaction()
		{
			if (m_isTransacted)
			{
				m_sqlTransaction.Rollback();
				m_isTransacted=false;
			}
		}

		/// <summary>
		/// In case the member SQLTransaction variable is not null calls Commit
		/// </summary>
		public void  CommitTransaction()
		{
			if (m_isTransacted)
			{
				m_sqlTransaction.Commit();
				m_isTransacted=false;
			}
		}


		/// <summary>
		/// Fires the provided SQL against the Connection using an SQLDataAdapter . Creates a new DataSet and fills a table of the name specified in TableName. In case the SQL Transaction object is not NULL it will be used while executing the SQL
		/// </summary>
		/// <param name="SQLStatement">SQL to execute</param>
		/// <param name="TableName">Name of the DataTable to fill in the DataSet</param>
		/// <returns>new DataSet</returns>
		public DataSet GetDataSet(string SQLStatement, string TableName)
		{

			SqlDataAdapter sdAdapter = new SqlDataAdapter(SQLStatement,m_sqlConnection);
			DataSet ds = new DataSet();
			sdAdapter.Fill(ds,TableName);
			

		return ds;
		}

		/// <summary>
		/// Fires the provided SQL against the Connection using an SQLDataAdapter . USes the DataSet  DataSettoFill and fills a table of the name specified in TableName. In case the SQL Transaction object is not NULL it will be used while executing the SQL
		/// </summary>
		/// <param name="SQLStatement">SQL to execute</param>
		/// <param name="TableName">Name of the DataTable to fill in the DataSet</param>
		/// <param name="DataSetToFill">The dataset will be passed in by the client</param>
		
		public void FillDataSetTable(string SQLStatement, DataSet DataSetToFill, string TableName)
		{
			SqlCommand sc=null;

			if (m_isTransacted)
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection,m_sqlTransaction);
			}
			else
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection);
			}
			sc.CommandTimeout = 500;
			SqlDataAdapter sda = new SqlDataAdapter(sc);

			sda.Fill(DataSetToFill,TableName);

			
		}

		public void FillDataSetTable(IDbCommand sqlDSCommand, DataSet DataSetToFill, string TableName)
		{
			
			sqlDSCommand.Connection=m_sqlConnection;

			if (m_isTransacted)
			{
				sqlDSCommand.Transaction=m_sqlTransaction;
			}
			
			SqlDataAdapter sda = new SqlDataAdapter((SqlCommand) sqlDSCommand);

			sda.Fill(DataSetToFill,TableName);

			
		}

		/// <summary>
		/// Executes the SQL Statement on the connection and returns the DataReader
		/// </summary>
		/// <param name="SQLStatement">Select SQL Statement</param>
		/// <returns>SQLDataReader result</returns>
		public IDataReader GetDataReader( string SQLStatement, System.Data.CommandBehavior Behaviour)
		{
			SqlCommand sc=null;
			if (m_isTransacted)
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection,m_sqlTransaction);
			}
			else
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection);
			}
			

			
			sc.CommandTimeout = 500;
			SqlDataReader sdr = sc.ExecuteReader(Behaviour);
			

			return sdr;
		}



		/// <summary>
		/// Executes the SQLCommand on the connection and returns the DataReader
		/// </summary>
		/// <param name="CommandObject">Command object to execute</param>
		/// <returns>SQLDataReader result</returns>
		public IDataReader GetDataReader( IDbCommand CommandObject, CommandBehavior Behaviour )
		{
			CommandObject.Connection = m_sqlConnection ;
			if (m_isTransacted)
			{
				CommandObject.Transaction = m_sqlTransaction;
			}
			else
			{
				
			}
			

			return CommandObject.ExecuteReader(Behaviour);
			

			
		}




		
		/// <summary>
		/// Executes the SQLStatement and return rows affected
		/// </summary>
		/// <param name="SQLStatement">SQL stmt to execute</param>
		/// <returns>Rows Affected</returns>
		public int ExecuteNonQuery( string SQLStatement)
		{
			SqlCommand sc=null;
			if (m_isTransacted)
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection,m_sqlTransaction);
			}
			else
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection);
			}
			
			sc.CommandTimeout = 500;

			return sc.ExecuteNonQuery();
			

			
		}

		/// <summary>
		/// Executes the Command Object. Used to pass in Isert/Updates which require to update binary data.
		/// </summary>
		/// <param name="CommandObject">SQLCommand object to execute. This will have all the parameter objects set tot he requried values</param>
		/// <returns>count of rows affected</returns>
		
		public int ExecuteNonQuery( IDbCommand CommandObject)
		{
			CommandObject.Connection = m_sqlConnection ;
			if (m_isTransacted)
			{
				CommandObject.Transaction = m_sqlTransaction;
			}
			else
			{
				
			}

			CommandObject.CommandTimeout = 500;

			return CommandObject.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes the SqlStatment
		/// </summary>
		/// <param name="SQLStatement">query Result</param>
		/// <returns></returns>
		public object ExecuteScaler(string SQLStatement)
		{
			SqlCommand sc=null;
			if (m_isTransacted)
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection,m_sqlTransaction);
			}
			else
			{
				sc = new SqlCommand(SQLStatement,m_sqlConnection);
			}
			sc.CommandTimeout = 500;
			return sc.ExecuteScalar();
			

			
		}

        /// <summary>
        /// Executes the SqlStatment
        /// </summary>
        /// <param name="CommandObject">The command to execute</param>
        /// <returns></returns>
        public object ExecuteScaler(IDbCommand CommandObject)
        {
            CommandObject.Connection = m_sqlConnection;
            if (m_isTransacted)
            {
                CommandObject.Transaction = m_sqlTransaction;
            }
            else
            {

            }

            CommandObject.CommandTimeout = 500;

            return CommandObject.ExecuteScalar();
        }

		/// <summary>
		/// Returns the Name of the Schema used in DB
		/// </summary>
		/// <returns>Returns the Name of the Schema used in DB ("dbo" by default).</returns>
		public string SchemaName
		{
			get
			{
				return m_schemaName;
			}
		}

		/// <summary>
		/// Closes the connection if open
		/// </summary>
		/// <param name="IsDisposing">Ignored</param>
		public void Dispose( bool IsDisposing)
		{	
			Close();
		}


		public void Close()
		{
			if (m_sqlConnection.State != System.Data.ConnectionState.Closed)
			{
				m_sqlConnection.Close();
			}
			m_IsConnected=false;
		
		}
			#region IDisposable Members

		/// <summary>
		/// IDispose close the connection if open
		/// </summary>
			public void Dispose()
			{
				// TODO:  Add DBHelper.Dispose implementation
				Dispose(true);
			}

		#endregion
	}
}
