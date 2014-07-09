using System;
using System.Data;
using System.Data.SqlClient ;

using System.Text;

using System.Reflection;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Uses .NET reflection to generate and execute commands to update DB tables given array of column names and a structure
	/// </summary>
	public class StructToTable
	{
		public StructToTable()
		{
			
		}
		public SqlCommand GetTableInsertCommand(object DataStruct, string TableName, string [] AFieldNames)
		{
			

			#region Generate INSERT
			string insertStatement = GenerateInsert(TableName, AFieldNames);


			SqlCommand sqlInsertCommand = new SqlCommand();

			sqlInsertCommand.CommandType = CommandType.Text;
			sqlInsertCommand.CommandText = insertStatement;
				
			#endregion
	
			PopulateParameters(DataStruct,sqlInsertCommand,AFieldNames);

			return sqlInsertCommand;
		}


		private void PopulateParameters (object DataStruct, SqlCommand SqlCommandToPopulate, string [] AFieldNames)
		{
		
			
			#region CreateParams and Set Value
			Type structType = DataStruct.GetType();

			//object aMember = structType.GetMember(AFieldNames[0]);
			
			int fieldCounter =0;

			object objVal; //value of the field cast as object
			//AFieldNames.Length
			for (fieldCounter =0 ; fieldCounter< AFieldNames.Length;fieldCounter++)
			{
				FieldInfo aMember = structType.GetField(AFieldNames[fieldCounter]);

				
				
				objVal = System.DBNull.Value; // initiate as dbnull
				
				object objFieldValue = aMember.GetValue(DataStruct);
				#region Switch
				switch (aMember.FieldType.ToString())
				{
					case  ("IRU.RTS.CommonComponents.NullableInt"):
					{
						NullableInt nValOut;
							
						if (objFieldValue!=null) 
						{
							nValOut=(NullableInt) objFieldValue;
							objVal=nValOut.Value;
						}
						//System.Windows.Forms.MessageBox.Show(nIntOut.Value.ToString());
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Int);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
					}
						break;
					
					case  ("System.Int32"):
					{
						int nVal;
						nVal=(int) objFieldValue;
						objVal=nVal;
						//System.Windows.Forms.MessageBox.Show(nIntOut.Value.ToString());
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Int);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
					}
						break;

					case  ("IRU.RTS.CommonComponents.NullableDouble" ):
					{
						NullableDouble nValOut;
							
						if (objFieldValue!=null) 
						{
							nValOut=(NullableDouble) objFieldValue;
							objVal=nValOut.Value;
						}
						
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Decimal);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}
						break;
					case("System.Double"):
					{
						double nVal;
						nVal=(double)objFieldValue;
						objVal=nVal;
						//SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],nVal);
						//System.Windows.Forms.MessageBox.Show(nIntOut.Value.ToString());
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Decimal);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}

						break;				
						#region datetime
					case  ("IRU.RTS.CommonComponents.NullableDateTime"):
					{
						NullableDateTime  nValOut;
							
						
						if (objFieldValue!=null) 
						{
							nValOut=(NullableDateTime) objFieldValue;
							objVal=nValOut.Value;
						}
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.DateTime);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}
						break;
					case("System.DateTime"):
					{
						DateTime nVal;
						nVal=(DateTime) objFieldValue;
						objVal = nVal;
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.DateTime);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
						
					}
						break;		
						#endregion

						#region bytearray
					case("System.Byte[]"):
					{
						byte[] nVal;
						nVal=(byte[]) objFieldValue;
						if(nVal!=null)
							objVal = nVal;
						//System.Windows.Forms.MessageBox.Show(nIntOut.Value.ToString());
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Image);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}
						break;		
						#endregion

						#region string
					case("System.String"):
					{
						string nVal;
						nVal=(string) objFieldValue;
						if(nVal!=null)
							objVal = nVal;
						//System.Windows.Forms.MessageBox.Show(nIntOut.Value.ToString());
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.NVarChar);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}
						break;
		
						#endregion

						#region nullableBool
					case  ("IRU.RTS.CommonComponents.NullableBool"):
					{
						NullableBool  nValOut;
						if (objFieldValue!=null) 
						{
							nValOut=(NullableBool) objFieldValue;
							objVal=nValOut.Value;
							if ((bool)objVal ==true)
							{
								objVal=1;
							}
							else
							{
								objVal=0;
							}
						}
							
							
								
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Bit);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
								
							
					}
						break;
					case("System.Boolean"):
					{
						bool nVal;
						nVal=(bool) objFieldValue;

						if (nVal==true)
						{
							objVal=1;
						}
						else
						{
							objVal=0;
						}
						SqlCommandToPopulate.Parameters.Add("@" + AFieldNames[fieldCounter],SqlDbType.Bit);
						SqlCommandToPopulate.Parameters["@" + AFieldNames[fieldCounter]].Value= objVal;
						
					}
						break;	


						#endregion



				}
				#endregion switch
		
			}
			
	
			#endregion

	
		}
		private string GenerateInsert( string TableName, string [] AFieldNames)
		{
			StringBuilder insertString = new StringBuilder();
			insertString.Append( "INSERT INTO "); 
			insertString.Append( TableName );
			insertString.Append( "(" );


			#region Values
			int fieldCtr = 0;
			StringBuilder valString = new StringBuilder(); 

			for (fieldCtr =0 ;fieldCtr< AFieldNames.Length; fieldCtr++)
			{
                
				insertString.Append(AFieldNames[fieldCtr]);
				if(fieldCtr < (AFieldNames.Length-1))
				{
					insertString.Append(",");

				}
                valString.Append("@" + AFieldNames[fieldCtr]);
              	if(fieldCtr < (AFieldNames.Length-1))
				{

                    valString.Append(",");

				}
		
			
			}

			
			insertString.Append( ") VALUES (");
			insertString.Append( valString.ToString() );

			insertString.Append( ")");



			#endregion

			return insertString.ToString();
			


		}


		private string GenerateUpdate( string TableName, string [] AFieldNames, string [] AKeyColumns)
		{
			StringBuilder updateString = new StringBuilder();
			updateString.Append( "UPDATE "); 
			updateString.Append( TableName );
			updateString.Append( " Set " );


			#region Values
			int fieldCtr = 0;
			

			for (fieldCtr =0 ;fieldCtr< AFieldNames.Length; fieldCtr++)
			{	
				updateString.Append(AFieldNames[fieldCtr]);
				updateString.Append(" = @" + AFieldNames[fieldCtr]);
				if(fieldCtr < (AFieldNames.Length-1))
				{
					updateString.Append(",");

				}
				
			}

			
			updateString.Append( " WHERE ");
			for (fieldCtr =0 ;fieldCtr< AKeyColumns.Length; fieldCtr++)
			{	
				updateString.Append(AKeyColumns[fieldCtr]);
				updateString.Append(" = @" + AKeyColumns[fieldCtr]);
				if(fieldCtr < (AKeyColumns.Length-1))
				{
					updateString.Append(",");

				}
				
			}

			



			#endregion

			return updateString.ToString();
			


		}


		
		public SqlCommand GetTableUpdateCommand(object DataStruct, string TableName, string [] AFieldNames, string [] AKeyColumns)
		{
			
			#region Generate UPDATE
			string updateStatement = GenerateUpdate(TableName, AFieldNames,  AKeyColumns);
			//System.Windows.Forms.MessageBox.Show(insertStatement);

			SqlCommand sqlUpdateCommand = new SqlCommand();

			sqlUpdateCommand.CommandType = CommandType.Text;
			sqlUpdateCommand.CommandText = updateStatement;
				
			#endregion
			PopulateParameters(DataStruct,sqlUpdateCommand,AFieldNames);
			PopulateParameters(DataStruct,sqlUpdateCommand,AKeyColumns);
			return sqlUpdateCommand;
		}
	}

	#region NullableClasses
	public class NullableBase
	{
//////		protected bool m_Null;
//////		public NullableBase()
//////		{
//////			m_Null= true;
//////		
//////		}
//////		public bool IsNull
//////		{
//////			get
//////			{
//////				return m_Null;
//////			}
//////			set
//////			{
//////				m_Null = value;
//////	
//////			}
//////		}
	}

	public class NullableInt :NullableBase 
	{
		int m_Value;
		public NullableInt(int Data)
		{
			m_Value=Data;
		}
		public int Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				 
			}
		}

		
	}
 
	public class NullableLong :NullableBase 
	{
		long m_Value;
		public NullableLong(long Data)
		{
			m_Value=Data;
		}
		public long Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				 
			}
		}
	}

	public class NullableDateTime :NullableBase 
	{
		DateTime m_Value;
		public NullableDateTime(DateTime Data)
		{
			m_Value=Data;
		}
		public DateTime Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				 
			}
		}
	}

	public class NullableDouble:NullableBase 
	{
		double  m_Value;
		public NullableDouble(double Data)
		{
			m_Value=Data;
		}
		public double Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				 
			}
		}
	}

	public class NullableBool:NullableBase 
	{
		bool m_Value;
		public NullableBool(bool Data)
		{
			m_Value=Data;
		}
		public bool Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				 
			}
		}
	}
	#endregion NullableClasses

}
