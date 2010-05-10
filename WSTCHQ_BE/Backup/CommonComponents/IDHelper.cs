using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for IDHelper.
	/// </summary>
	public class IDHelper
	{
		
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="DBHelper"></param>
		public IDHelper()
		{
			
		}

		/// <summary>
		/// Will genererate a new ID for given Purpose. 
		/// The Method will use the DBHelper class to do the following pseudo TSQL.
		/// 
		/// <code>
		/// Begin Transaction
		/// Declare @newID numeric
		/// Update RTS_ID set @newID= ID_Value + 1, ID_Value = ID_Value + 1 where  ID_Purpose = @IdPurpose
		/// Select @newID
		/// Commit Transaction
		/// 
		/// </code>
		/// 
		/// </summary>
		/// 
		/// <param name="IdPurpose">String which matches with the ID_Purpose value </param>
		/// <param name="DBName">Name of DB matching with the config file</param>
		/// <returns>The new number generated as long</returns>
		/// <exception cref="ApplicationException">In case  the row is missing the newID value will be null and the class will throw an ApplicationException</exception>
		public static long GenerateID(string IdPurpose, IDBHelper IDDBHelper)
		{

			string sCommand = "Declare @IdPurpose nvarchar(50)";

			sCommand +="\n set @IdPurpose = '" + IdPurpose + "'";
			sCommand +="\n set nocount on";
			sCommand +="\n Begin Transaction";
			sCommand +="\n Declare @newID numeric";
			sCommand +="\n Update RTS_IDS set @newID = ID_Value + 1, ID_Value = ID_Value + 1 where  ID_Purpose = @IdPurpose";
			sCommand +="\n Select @newID as NewGenID";
			sCommand +="\n Commit Transaction";

			object returned=null ;
			try
			{
				IDDBHelper.ConnectToDB();
				returned = IDDBHelper.ExecuteScaler(sCommand);
				
				//long result = (long)
				

			}
			finally
			{
				IDDBHelper.Close();
			}

			return long.Parse( returned.ToString());







		}


		

	}



}
