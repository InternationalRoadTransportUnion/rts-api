using System;
using System.Data;
using System.Collections;
using IRU.CommonInterfaces;
using IRU.CryptEngine;
using IRU.RTS.CommonComponents;
using System.Data.SqlClient ;
using System.Text ;
using System.Reflection;
using System.IO;

namespace IRU.RTS.WSTCHQ
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// CWQuerys is called to retrieve the CW Carnet Data. This data is returned in a Decrypted Hash Table.
	/// </summary>
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class CWQuerys
	{
		#region Globals
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CWQuerys Globals>
		/// CWQuerys Globals are in this section.  This includes constants for the "Result" status of the Cute-Wise Database query.
		/// </CWQuerys Globals>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private IDBHelper m_idbHelper;

		protected const int n_CWStatusStateID_5 = 1;
		protected const int n_CWStatusStateID_0 = 2;
		protected const int n_CWStoppCarnetNumb = 3;
		protected const int n_CWStatusStateID_X = 4;
		protected const int n_InvalidCarnetNumb = 5;
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		
		#region CWQuerys Constructor
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CWQuerys Constructor>
		/// CWQuerys Constructor logic below.
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public CWQuerys(IDBHelper idbHelper)
		{
			m_idbHelper = idbHelper;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion

		#region GetTIRCarnetQueryData
		
        /// <summary>
		/// CWQuerys GetTIRCarnetQueryData - This is call to get the TIR Carnet Data.
		/// </summary>
		/// <param name="sTIR_Carnet_No"></param>
		/// <returns></returns>
        public Hashtable GetTIRCarnetQueryData(string sTIR_Carnet_No)
        {
            
            Int64 lTIR_Carnet_No = 0;
            Hashtable ht = new Hashtable();
            string sResult = "";

            try
            {
                if (IRU_CheckTIRNo.CheckForValidCarnetNo(sTIR_Carnet_No))
                {
                    if (IRU_CheckTIRNo.CheckForCheckChar(sTIR_Carnet_No) == "NUMERIC")
                        lTIR_Carnet_No = int.Parse(sTIR_Carnet_No.Trim());                    
                    else if (IRU_CheckTIRNo.CheckForCheckChar(sTIR_Carnet_No) == "ALPHA")                    
                        lTIR_Carnet_No = int.Parse((sTIR_Carnet_No.Trim().Substring(2)).ToString());                    

                    if (lTIR_Carnet_No != 0)
                    {                        
                        string query;
                        using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TCHQ_Processor.GetCarnetStatus.sql")))
                        {
                            query = reader.ReadToEnd();
                        }

                        SqlCommand cmd = new SqlCommand(query);
                        cmd.CommandTimeout = 500;
                        cmd.Parameters.Add("@TIRNumber", SqlDbType.Int).Value = lTIR_Carnet_No;
                        m_idbHelper.ConnectToDB();
                        IDataReader dr = m_idbHelper.GetDataReader(cmd, CommandBehavior.SingleRow);

                        dr.Read(); 
                        sResult = dr["RESULT"].ToString().Trim();

                        if (sResult.Trim() != "")
                        {
                            switch (sResult)
                            {
                                case "1":                                    
                                    ht.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
                                    ht.Add("Assoc_Short_Name", dr["AssociationShortName"].ToString().Trim());
                                    ht.Add("Validity_Date", (DateTime)dr["ExpiryDate"]);
                                    ht.Add("No_Of_Terminations", dr["NoOfDischarges"].ToString().Trim());
                                    ht.Add("Query_Result_Code", sResult);
                                    ht.Add("Holder_ID", dr["I_HolderID"].ToString().Trim());
                                    break;                                    
                                case "2":                                    
                                    ht.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
                                    ht.Add("Assoc_Short_Name", dr["AssociationShortName"].ToString().Trim());
                                    ht.Add("Validity_Date", null);
                                    ht.Add("No_Of_Terminations", dr["NoOfDischarges"].ToString().Trim());
                                    ht.Add("Query_Result_Code", sResult);
                                    ht.Add("Holder_ID", null);
                                    break;                                    
                                case "3":
                                case "4":
                                case "5":
                                    ht.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
                                    ht.Add("Assoc_Short_Name", null);
                                    ht.Add("Validity_Date", null);
                                    ht.Add("No_Of_Terminations", null);
                                    ht.Add("Query_Result_Code", sResult);
                                    ht.Add("Holder_ID", null);
                                    break;                                    
                            }
                        }                       
                    }
                }
                else
                {
                    ht.Add("Carnet_Number", sTIR_Carnet_No);
                    ht.Add("Assoc_Short_Name", null);
                    ht.Add("Validity_Date", null);
                    ht.Add("No_Of_Terminations", null);
                    ht.Add("Query_Result_Code", "5");
                    ht.Add("Holder_ID", null);
                }                
            }
            catch (Exception e)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning,
                    "Invalid value whilst Retreiving the CW Carnets Data in TCHQ_Processor.CWQuerys.cs"
                    + e.Message);
                throw;
            }
            return ht;
        }

		/// <summary>        
		/// CWQuerys GetTIRCarnetQueryData - This is call to get the TIR Carnet Data.
		/// </summary>
		/// <param name="sTIR_Carnet_No"></param>
		/// <returns></returns>
        [Obsolete]
		public Hashtable GetTIRCarnetQueryDataCuteWise(string sTIR_Carnet_No)
		{
			IRU_EncryptDecrypt iru_EncryptDecrypt = new IRU_EncryptDecrypt();

			Int64 lTIR_Carnet_No = 0;
			Hashtable hashEncryptedResult = new Hashtable();
			Hashtable hashDecryptedResult = new Hashtable();

			string sQuerySelFromCWDB = "";
			//string sCWDataTableName	 = "CWDataTable";
			string sResult = "";

			//DataSet cwDS = new DataSet();
			//DataRow dr;

			try
			{
				if (IRU_CheckTIRNo.CheckForValidCarnetNo(sTIR_Carnet_No))
				{
					if (IRU_CheckTIRNo.CheckForCheckChar(sTIR_Carnet_No) == "NUMERIC")
					{
						lTIR_Carnet_No = iru_EncryptDecrypt.EncryptInteger(int.Parse(sTIR_Carnet_No.Trim()));
					}
					if (IRU_CheckTIRNo.CheckForCheckChar(sTIR_Carnet_No) == "ALPHA")
					{
						lTIR_Carnet_No = iru_EncryptDecrypt.EncryptInteger(int.Parse((sTIR_Carnet_No.Trim().Substring(2)).ToString()));
					}

					if (lTIR_Carnet_No != 0)
					{
						#region <CW Query> This is where we will perform the SQL Query on the CW Database
						#region PMC Code commented - 2005-09-29
						
						//						sQuerySelFromCWDB  = "IF( " +
						//							"(SELECT COUNT(*) FROM [CWStopped] " +
						//							"WHERE TIRNumber = " + lTIR_Carnet_No  + " " + //*1006674407*/ 1007559781) " +
						//							"> 0) " +
						//							"SELECT " + n_CWStoppCarnetNumb.ToString().Trim() + " AS RESULT FROM CWCarnets " +
						//																	
						//							"ELSE " +
						//																	
						//							"IF( " +
						//							"(SELECT CWStatus.StateID FROM CWStatus WHERE " +
						//							"CWStatus.TIRNumber = " + lTIR_Carnet_No  + ") = -26223) " +
						//										
						//							"SELECT CWCarnets.TIRNumber, CWCarnets.I_HolderID, CWCarnets.ExpiryDate, RefAssociations.AssociationShortName, " + 
						//							"(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = 1007559781)) AS NoOfDischarges, " +
						//							"CWStatus.StateID, (SELECT " + n_CWStatusStateID_5.ToString().Trim() + ") AS RESULT " +
						//							"FROM CWDischarges INNER JOIN " +
						//							"CWCarnets ON CWDischarges.TIRNumber = CWCarnets.TIRNumber INNER JOIN " +
						//							"RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN " +
						//							"CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber " +
						//							"WHERE (CWCarnets.TIRNumber = " + lTIR_Carnet_No  + ") " +
						//								
						//							"ELSE " +
						//
						//							"IF( " +
						//							"(SELECT CWStatus.StateID FROM CWStatus WHERE " +
						//							"CWStatus.TIRNumber = " + lTIR_Carnet_No  + ") = -26220) " +
						//
						//							"SELECT CWCarnets.TIRNumber, RefAssociations.AssociationShortName, " + 
						//							"(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = " + lTIR_Carnet_No  + ")) AS NoOfDischarges, " +
						//							"CWStatus.StateID, (SELECT " + n_CWStatusStateID_0.ToString().Trim() + " AS RESULT " +
						//							"FROM CWDischarges INNER JOIN " +
						//							"CWCarnets ON CWDischarges.TIRNumber = CWCarnets.TIRNumber INNER JOIN " +
						//							"RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN " +
						//							"CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber " +
						//							"WHERE (CWCarnets.TIRNumber = " + lTIR_Carnet_No  + ") " +
						//																			
						//							"ELSE " +
						//							"SELECT " + n_CWStatusStateID_X.ToString().Trim() + " AS RESULT";

						//						sQuerySelFromCWDB  = "IF (EXISTS (SELECT Top 1 1 FROM [CWStopped] WHERE TIRNumber = @TIR_NUMBER)) " +
						//						"SELECT 3 AS RESULT " +
						//						"ELSE  " +
						//						"IF((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIR_NUMBER) = -26223)  " +
						//						"SELECT CWCarnets.TIRNumber,  " +
						//						"CWCarnets.I_HolderID,  " +
						//						"CWCarnets.ExpiryDate,  " +
						//						"RefAssociations.AssociationShortName,  " +
						//						"(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIR_NUMBER)) AS NoOfDischarges,  " +
						//						"CWStatus.StateID,  " +
						//						"1 AS RESULT FROM CWCarnets INNER JOIN  " +
						//						"RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN  " +
						//						"CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber  " +
						//						"WHERE (CWCarnets.TIRNumber = @TIR_NUMBER)  " +
						//						"ELSE  " +
						//						"IF( (SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIR_NUMBER) = -26220)  " +
						//						"SELECT CWCarnets.TIRNumber,  " +
						//						"RefAssociations.AssociationShortName,  " +
						//						"(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIR_NUMBER)) AS NoOfDischarges,  " +
						//						"CWStatus.StateID,  " +
						//						"2 AS RESULT  " +
						//						"FROM CWCarnets INNER JOIN  " +
						//						"RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN  " +
						//						"CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber  " +
						//						"WHERE (CWCarnets.TIRNumber = @TIR_NUMBER)  " +
						//						"ELSE  " +
						//						"SELECT 4 AS RESULT " ;
						#endregion
						//sQuerySelFromCWDB  = "

						#region Code Commented after reporting a bug for value 5 - not in CWCarnets
						/* 
						StringBuilder strBuider = 
							new StringBuilder("IF (EXISTS (SELECT Top 1 1 FROM [CWStopped] WHERE TIRNumber = @TIRNumber)) ");
						strBuider.Append("	BEGIN "); //-- Stopped carnet 
						strBuider.Append("		SELECT @TIRNumber AS TIRNumber, 3 AS RESULT ");
						strBuider.Append("	END "); //-- Stopped carnet 
						strBuider.Append("ELSE ");
						strBuider.Append("	BEGIN ");
						strBuider.Append("	IF((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26223) ");
						strBuider.Append("		BEGIN  "); //-- status = 5 
						strBuider.Append("			SELECT CWCarnets.TIRNumber, 1 AS RESULT, ");
						strBuider.Append("			CWCarnets.I_HolderID, ");
						strBuider.Append("			CWCarnets.ExpiryDate, ");
						strBuider.Append("			RefAssociations.AssociationShortName, ");
						strBuider.Append("			(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges, ");
						strBuider.Append("			CWStatus.StateID FROM CWCarnets INNER JOIN ");
						strBuider.Append("			RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN ");
						strBuider.Append("			CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber ");
						strBuider.Append("			WHERE (CWCarnets.TIRNumber = @TIRNumber) ");
						strBuider.Append("		END "); //-- status = 5
						strBuider.Append("	ELSE ");
						strBuider.Append("		IF( (SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26220) ");
						strBuider.Append("			BEGIN "); //-- status = 0
						strBuider.Append("				SELECT CWCarnets.TIRNumber, 2 AS RESULT, ");
						strBuider.Append("				RefAssociations.AssociationShortName, ");
						strBuider.Append("				(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges, ");
						strBuider.Append("				CWStatus.StateID");
						strBuider.Append("				FROM CWCarnets INNER JOIN ");
						strBuider.Append("				RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN ");
						strBuider.Append("				CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber ");
						strBuider.Append("				WHERE (CWCarnets.TIRNumber = @TIRNumber) ");
						strBuider.Append("			END "); //-- status = 0
						strBuider.Append("		ELSE ");
						strBuider.Append("			BEGIN");
						strBuider.Append("				SELECT @TIRNumber AS TIRNumber, 4 AS RESULT");
						strBuider.Append("			END");
						strBuider.Append("	END");
						*/
						#endregion

						#region Code commented for CR of Query to hide problems with CW Database
						/*
						StringBuilder strBuider = 
						new StringBuilder("IF (EXISTS (SELECT Top 1 1 FROM [CWCarnets] WHERE TIRNumber = @TIRNumber)) ");
						strBuider.Append("	BEGIN "); //-- Stopped carnet 
						strBuider.Append(" IF (EXISTS (SELECT Top 1 1 FROM [CWStopped] WHERE TIRNumber = @TIRNumber)) ");
						strBuider.Append("	BEGIN "); //-- Stopped carnet 
						strBuider.Append("		SELECT @TIRNumber AS TIRNumber, 3 AS RESULT ");
						strBuider.Append("	END "); //-- Stopped carnet 
						strBuider.Append("ELSE ");
						strBuider.Append("	BEGIN ");
						strBuider.Append("	IF((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26223) ");
						strBuider.Append("		BEGIN  "); //-- status = 5 
						strBuider.Append("			SELECT CWCarnets.TIRNumber, 1 AS RESULT, ");
						strBuider.Append("			CWCarnets.I_HolderID, ");
						strBuider.Append("			CWCarnets.ExpiryDate, ");
						strBuider.Append("			RefAssociations.AssociationShortName, ");
						strBuider.Append("			(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges, ");
						strBuider.Append("			CWStatus.StateID FROM CWCarnets INNER JOIN ");
						strBuider.Append("			RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN ");
						strBuider.Append("			CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber ");
						strBuider.Append("			WHERE (CWCarnets.TIRNumber = @TIRNumber) ");
						strBuider.Append("		END "); //-- status = 5
						strBuider.Append("	ELSE ");
						strBuider.Append("		IF( (SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26220) ");
						strBuider.Append("			BEGIN "); //-- status = 0
						strBuider.Append("				SELECT CWCarnets.TIRNumber, 2 AS RESULT, ");
						strBuider.Append("				RefAssociations.AssociationShortName, ");
						strBuider.Append("				(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges, ");
						strBuider.Append("				CWStatus.StateID");
						strBuider.Append("				FROM CWCarnets INNER JOIN ");
						strBuider.Append("				RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID INNER JOIN ");
						strBuider.Append("				CWStatus ON CWCarnets.TIRNumber = CWStatus.TIRNumber ");
						strBuider.Append("				WHERE (CWCarnets.TIRNumber = @TIRNumber) ");
						strBuider.Append("			END "); //-- status = 0
						strBuider.Append("		ELSE ");
						strBuider.Append("			BEGIN");
						strBuider.Append("				SELECT @TIRNumber AS TIRNumber, 4 AS RESULT");
						strBuider.Append("			END");
						strBuider.Append("	END");
						strBuider.Append("	END");
						strBuider.Append("	ELSE ");
						strBuider.Append("	BEGIN");
						strBuider.Append("		SELECT @TIRNumber AS TIRNumber, 5 AS RESULT");
						strBuider.Append("	END");
						*/
						#endregion
			
						StringBuilder strBuilder = 
						new StringBuilder("IF (EXISTS (SELECT Top 1 1 FROM [CWStopped] WHERE TIRNumber = @TIRNumber)) ");
						strBuilder.Append("BEGIN ");
						strBuilder.Append("	SELECT @TIRNumber AS TIRNumber, 3 AS RESULT  ");
						strBuilder.Append("END ");
						strBuilder.Append("ELSE ");
						strBuilder.Append("BEGIN ");
						strBuilder.Append("	IF (NOT EXISTS (SELECT Top 1 1 FROM [CWCarnets] WHERE TIRNumber = @TIRNumber)) 	 ");
						strBuilder.Append("	BEGIN  ");
						strBuilder.Append("		SELECT @TIRNumber AS TIRNumber, 5 AS RESULT	 ");
						strBuilder.Append("	END  ");
						strBuilder.Append("	ELSE  ");
						strBuilder.Append("	BEGIN  ");
						strBuilder.Append("		IF(((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26220) OR ");
						strBuilder.Append("	           ((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26165) OR ");
						strBuilder.Append("	           ((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26223) OR ");
						strBuilder.Append("	           ((SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber) = -26211) OR ");
						strBuilder.Append("	           (NOT EXISTS (SELECT CWStatus.StateID FROM CWStatus WHERE CWStatus.TIRNumber = @TIRNumber))) ");
						strBuilder.Append("		BEGIN ");
						strBuilder.Append("			IF ((SELECT CWCarnets.IssueDate FROM CWCarnets WHERE CWCarnets.TIRNumber = @TIRNumber) is not null) ");
						strBuilder.Append("			BEGIN ");
						strBuilder.Append("				SELECT CWCarnets.TIRNumber, 1 AS RESULT,  ");
						strBuilder.Append("				CWCarnets.I_HolderID,  ");
						strBuilder.Append("				CWCarnets.ExpiryDate,  ");
						strBuilder.Append("				RefAssociations.AssociationShortName,  ");
						strBuilder.Append("				(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges ");
						strBuilder.Append("				FROM CWCarnets INNER JOIN  ");
						strBuilder.Append("				RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID  ");
						strBuilder.Append("				WHERE (CWCarnets.TIRNumber = @TIRNumber)  ");
						strBuilder.Append("			END ");
						strBuilder.Append("			ELSE ");
						strBuilder.Append("			BEGIN ");
						strBuilder.Append("				IF((SELECT CWCarnets.BulletinDate FROM CWCarnets WHERE CWCarnets.TIRNumber = @TIRNumber) is not null) ");
						strBuilder.Append("				BEGIN  ");
						strBuilder.Append("					SELECT CWCarnets.TIRNumber, 2 AS RESULT,  ");
						strBuilder.Append("					RefAssociations.AssociationShortName,  ");
						strBuilder.Append("					(SELECT COUNT(*) FROM CWDischarges WHERE (CWDischarges.TIRNumber = @TIRNumber)) AS NoOfDischarges ");
						strBuilder.Append("					FROM CWCarnets INNER JOIN  ");
						strBuilder.Append("					RefAssociations ON CWCarnets.AssociationID = RefAssociations.AssociationID   ");
						strBuilder.Append("					WHERE (CWCarnets.TIRNumber = @TIRNumber)  ");
						strBuilder.Append("				END ");
						strBuilder.Append("				ELSE ");
						strBuilder.Append("	                        BEGIN ");
						strBuilder.Append("					SELECT @TIRNumber AS TIRNumber, 4 AS RESULT    ");
						strBuilder.Append("				END ");
						strBuilder.Append("			END  ");
						strBuilder.Append("		END  ");
						strBuilder.Append("		ELSE ");
						strBuilder.Append("		BEGIN  ");
						strBuilder.Append("			SELECT @TIRNumber AS TIRNumber, 4 AS RESULT  ");
						strBuilder.Append("		END ");
						strBuilder.Append("	END ");
						strBuilder.Append("END ");


						sQuerySelFromCWDB  = strBuilder.ToString(); 
			
						SqlCommand cmd = new SqlCommand(sQuerySelFromCWDB);
						cmd.CommandTimeout = 500;
						cmd.Parameters.Add("@TIRNumber", SqlDbType.Int).Value = lTIR_Carnet_No;

						m_idbHelper.ConnectToDB();

						IDataReader dr = m_idbHelper.GetDataReader(cmd, CommandBehavior.SingleRow); 
 
						//dr = cwDS.Tables[0].Rows[0];

						//sResult = dr["RESULT"].ToString().Trim();

						dr.Read(); // expecting atleast one row
						sResult = dr["RESULT"].ToString().Trim();

						if (sResult.Trim() != "")
						{
							switch(sResult)
							{
								case "1":
								{
									// Fill The HashTable
									hashEncryptedResult.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
									hashEncryptedResult.Add("Assoc_Short_Name", dr["AssociationShortName"].ToString().Trim());
									hashEncryptedResult.Add("Validity_Date", (DateTime)dr["ExpiryDate"]);
									hashEncryptedResult.Add("No_Of_Terminations", dr["NoOfDischarges"].ToString().Trim());
									hashEncryptedResult.Add("Query_Result_Code", sResult);
									hashEncryptedResult.Add("Holder_ID", dr["I_HolderID"].ToString().Trim());
									//hashEncryptedResult.Add("State_ID", dr["StateID"].ToString().Trim());
									break;
								}
								case "2":
								{
									// Fill The HashTable
									hashEncryptedResult.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
									hashEncryptedResult.Add("Assoc_Short_Name", dr["AssociationShortName"].ToString().Trim());
									hashEncryptedResult.Add("Validity_Date", null);
									hashEncryptedResult.Add("No_Of_Terminations", dr["NoOfDischarges"].ToString().Trim());
									hashEncryptedResult.Add("Query_Result_Code", sResult);
									hashEncryptedResult.Add("Holder_ID", null);
									//hashEncryptedResult.Add("State_ID", dr["StateID"].ToString().Trim());
									break;
								}
								case "3":
								{
									// Fill The HashTableb
									hashEncryptedResult.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
									hashEncryptedResult.Add("Assoc_Short_Name", null);
									hashEncryptedResult.Add("Validity_Date", null);
									hashEncryptedResult.Add("No_Of_Terminations", null);
									hashEncryptedResult.Add("Query_Result_Code", sResult);
									hashEncryptedResult.Add("Holder_ID", null);
									//hashEncryptedResult.Add("State_ID", dr["StateID"].ToString().Trim());
									break;
								}
								case "4":
								{
									// Fill The HashTable
									hashEncryptedResult.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
									hashEncryptedResult.Add("Assoc_Short_Name", null);
									hashEncryptedResult.Add("Validity_Date", null);
									hashEncryptedResult.Add("No_Of_Terminations", null);
									hashEncryptedResult.Add("Query_Result_Code", sResult);
									hashEncryptedResult.Add("Holder_ID", null);
									//hashEncryptedResult.Add("State_ID", dr["StateID"].ToString().Trim());
									break;
								}
								case "5":
								{
									// Fill The HashTable
									hashEncryptedResult.Add("Carnet_Number", dr["TIRNumber"].ToString().Trim());
									hashEncryptedResult.Add("Assoc_Short_Name", null);
									hashEncryptedResult.Add("Validity_Date", null);
									hashEncryptedResult.Add("No_Of_Terminations", null);
									hashEncryptedResult.Add("Query_Result_Code", sResult);
									hashEncryptedResult.Add("Holder_ID", null);
									//hashEncryptedResult.Add("State_ID", dr["StateID"].ToString().Trim());
									break;
								}
							}
						}
						#endregion
					}
				}
				else
				{
					hashEncryptedResult.Add("Carnet_Number",sTIR_Carnet_No  );
					hashEncryptedResult.Add("Assoc_Short_Name", null);
					hashEncryptedResult.Add("Validity_Date", null);
					hashEncryptedResult.Add("No_Of_Terminations", null);
					hashEncryptedResult.Add("Query_Result_Code", "5");
					hashEncryptedResult.Add("Holder_ID", null);
				}
				if (hashEncryptedResult.Count > 0)
				{
					if (sResult.Trim() != "")
					{
						hashDecryptedResult = DecrpytCuteWISEResult(hashEncryptedResult, sResult);
					}
					else
					{
						hashDecryptedResult = hashEncryptedResult;
					}
				}
				else
				{
					hashDecryptedResult = hashEncryptedResult;
				}
			}

			catch (Exception e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning  ,
					"Invalid value whilst Retreiving the CW Carnets Data in TCHQ_Processor.CWQuerys.cs"
					+ e.Message);
				throw e;
			}

			return hashDecryptedResult;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion

		#region GetTIRCarnetQueryData
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CWQuerys DecrpytCuteWISEResult>
		/// CWQuerys DecrpytCuteWISEResult - Called by GetTIRCarnetQueryData to decrypt the CW Carnet data Hashtable.
		/// </CWQuerys DecrpytCuteWISEResult>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private Hashtable DecrpytCuteWISEResult(Hashtable hashEncryptedResult, string sResult)
		{
			IRU_EncryptDecrypt iru_EncryptDecrypt = new IRU_EncryptDecrypt();
			
			Hashtable hashDecryptedResult = new Hashtable();

			if (hashEncryptedResult.Count > 0)
			{
				foreach (string sKey in hashEncryptedResult.Keys) 
				{
					try
					{
						if (sKey.Trim() == "Carnet_Number")
						{
							if ( ((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")))
							{
								string sTIR_Carnet_No	= ((iru_EncryptDecrypt.DecryptInteger(int.Parse(hashEncryptedResult[sKey].ToString().Trim()))).ToString());
								string sTIR_CheckChars  = IRU_CheckTIRNo.CheckChar(Int64.Parse(sTIR_Carnet_No));

								hashDecryptedResult.Add(sKey.Trim(), sTIR_CheckChars + sTIR_Carnet_No);
							}
							else
							{
								hashDecryptedResult.Add(sKey.Trim(), "");
							}
						}
						if (sKey.Trim() == "Assoc_Short_Name")
						{
							if ( ((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")))
							{
								string sAssName = iru_EncryptDecrypt.DecryptString((hashEncryptedResult[sKey].ToString().Trim()), ((uint)(hashEncryptedResult[sKey].ToString().Length)));
								hashDecryptedResult.Add(sKey.Trim(), sAssName);
							}
							else
							{
								hashDecryptedResult.Add(sKey.Trim(), "");
							}
						}
						if (sKey.Trim() == "Validity_Date")
						{
							hashDecryptedResult.Add(sKey.Trim(), hashEncryptedResult[sKey]);

//							if (((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")))
//							{
//								hashDecryptedResult.Add(sKey.Trim(), (hashEncryptedResult[sKey].ToString().Trim()));
//							}
//							else
//							{
//								hashDecryptedResult.Add(sKey.Trim(), "");
//							}
						}

						if (sKey.Trim() == "No_Of_Terminations")
						{
//							if (((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")) )
//							{
//								hashDecryptedResult.Add(sKey.Trim(), (hashEncryptedResult[sKey].ToString().Trim()));
//							}
//							else
//							{
//								hashDecryptedResult.Add(sKey.Trim(), "");
//							}
							hashDecryptedResult.Add(sKey.Trim(), hashEncryptedResult[sKey]);
						}

						if (sKey.Trim() == "Query_Result_Code")
						{
							hashDecryptedResult.Add(sKey.Trim(), hashEncryptedResult[sKey]);
//							if (((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")) )
//							{
//								hashDecryptedResult.Add(sKey.Trim(), (hashEncryptedResult[sKey].ToString().Trim()));
//							}
//							else
//							{
//								hashDecryptedResult.Add(sKey.Trim(), "");
//							}
						}

						if (sKey.Trim() == "Holder_ID")
						{
							if (((hashEncryptedResult[sKey]) != null) && (((hashEncryptedResult[sKey].ToString().Trim()) != "")) )
							{
								string sHolderID = iru_EncryptDecrypt.DecryptString((hashEncryptedResult[sKey].ToString()), ((uint)(hashEncryptedResult[sKey].ToString().Length)));
								hashDecryptedResult.Add(sKey.Trim(), sHolderID);
							}
							else
							{
								hashDecryptedResult.Add(sKey.Trim(), "");
							}
						}
//						if (sKey.Trim() == "State_ID")
//						{
//							if ((((hashEncryptedResult[sKey].ToString().Trim()) != "")) || ((hashEncryptedResult[sKey].ToString().Trim()) != null))
//							{
//								string sStateID = iru_EncryptDecrypt.DecryptWord(int.Parse(hashEncryptedResult[sKey].ToString().Trim()));
//								hashDecryptedResult.Add(sKey.Trim(), sStateID);
//							}
//							else
//							{
//								hashDecryptedResult.Add(sKey.Trim(), "");
//							}
//						}
					}
					catch (Exception e)
					{
						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning ,
							"Invalid value while decrypting the CW Carnets Data in TCHQ_Processor.CWQuerys.cs"
							+ e.Message);
						throw e;
					}
				}
			}

			return hashDecryptedResult;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}
