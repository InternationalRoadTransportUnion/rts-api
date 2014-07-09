using System;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;


namespace IRU.RTS.AdminClient
{
	
	public class CommonDBHelper
	{

		private SqlTransaction m_sqlTransaction;
		private string m_connectionString;
		private SqlConnection m_sqlConnection;
		private bool m_IsConnected;
		private bool m_isTransacted;


		/// <summary>
		/// Stores the connection string in member variables and initialises other members.
		/// </summary>
		/// <param name="ConnectString">Connection string used by SQLConnection.Open</param>
		public CommonDBHelper(string ConnectString)
		{
			m_sqlConnection = new SqlConnection();
			m_connectionString = ConnectString;
			m_sqlConnection.ConnectionString=m_connectionString;
			m_IsConnected=false;
			m_isTransacted=false;
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
			SqlDataAdapter sda = new SqlDataAdapter(sc);

			
			sda.Fill( DataSetToFill,TableName);

			
		}
		
		
		public void FillDataSetTable(SqlCommand sqlDSCommand, DataSet DataSetToFill, string TableName)
		{
			
			sqlDSCommand.Connection=m_sqlConnection;
			
			SqlDataAdapter sda = new SqlDataAdapter(sqlDSCommand);

			sda.Fill(DataSetToFill,TableName);

			
		}

		public void FillDataSetTableWithSchema(SqlCommand sqlDSCommand, DataSet DataSetToFill, string TableName)
		{
			
			sqlDSCommand.Connection=m_sqlConnection;
			
			SqlDataAdapter sda = new SqlDataAdapter(sqlDSCommand);
			sda.MissingSchemaAction=MissingSchemaAction.AddWithKey;

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

			
			
			return sc.ExecuteScalar();
			

			
		}

		/// <summary>
		/// Executes the SqlStatment
		/// </summary>
		/// <param name="SQLStatement">query Result</param>
		/// <returns></returns>
		public object ExecuteScaler(IDbCommand CommandObject)
		{
			CommandObject.Connection = m_sqlConnection ;
			if (m_isTransacted)
			{
				CommandObject.Transaction = m_sqlTransaction;
			}
			else
			{
				
			}
			

			

			return CommandObject.ExecuteScalar();

			
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
		
	}	

}

