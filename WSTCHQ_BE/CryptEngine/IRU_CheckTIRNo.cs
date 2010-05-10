using System;
using System.Collections;
using System.Data;
using System.IO;

namespace IRU.CryptEngine
{
	/// <summary>
	/// CheckTIRNo - Ensure that the tuser has inserted a correct TIR Carnet Number.
	/// </summary>
	public class IRU_CheckTIRNo
	{
		#region CheckForCheckChar
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CheckForCheckChar>
		/// CheckForCheckChar Checks ot see if the Carnet No has 2 characters at the start, or where it is a Carnet betwee 14000001 and 25000000.
		/// </CheckForCheckChar>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static string CheckForCheckChar(string sTIR_Carnet_No)
		{
			char c1 = ((sTIR_Carnet_No).Trim().ToUpper())[0];
			char c2 = ((sTIR_Carnet_No).Trim().ToUpper())[1];
			string s_CheckCharType = "";

			if ((Char.IsLetter(c1)) && (Char.IsLetter(c2)))
			{
				if ((c1 == 'I')||(c1 == 'L')||(c1 == 'O')||(c2 == 'I')||(c1 == 'L')||(c1 == 'O'))
				{
					s_CheckCharType = "INVALID";
				}
				else
				{
					s_CheckCharType = "ALPHA";
				}
			}
			else
			{
				if (Char.IsDigit(c1))
				{
					try
					{
						Int64.Parse(sTIR_Carnet_No.Trim());
						s_CheckCharType = "NUMERIC";
					}
					catch (Exception ParseExcep)
					{
						if (ParseExcep.Message.Trim() != "")
						{
							s_CheckCharType = "INVALID";
						}
					}
				}
			}
			return s_CheckCharType;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion

		#region CheckChar
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CheckChar>
		/// CheckChar - Checks to see if the Carnet No's 2 characters are valid or not.
		/// </CheckChar>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static string CheckChar(Int64 lTIR_Carnet_No)
		{
			string sCheckChar   = "";
			Int64 lCarnetMod_23 = 0;
			int nCheckLetter    = 0;
			char c				= 'A';

			if (lTIR_Carnet_No < 25000000)
			{
				sCheckChar = "";
			}
			else 
			{
				lCarnetMod_23 = (lTIR_Carnet_No % 23);
				nCheckLetter = (int)((decimal)(c) + ((3 * lCarnetMod_23 + 17) % 26));
				
				if (lCarnetMod_23 < 12)
				{
					sCheckChar = (((char)(nCheckLetter)).ToString() + 'X');
				}
				else
				{
					sCheckChar = ('X' + ((char)(nCheckLetter)).ToString());
				}
			}
			return sCheckChar;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion

		#region CheckForValidCarnetNo
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <CheckForValidCarnetNo>
		/// CheckForValidCarnetNo - Checks the validity of the Carnet No.
		/// </CheckForValidCarnetNo>
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static bool CheckForValidCarnetNo(string sTIR_Carnet_No)
		{
			bool bValidCarnetNo = false;

			if (CheckForCheckChar(sTIR_Carnet_No) == "NUMERIC")
			{
				if (Int64.Parse(sTIR_Carnet_No) < 1 )
				{
					//THE TIR CARNET NUMBER IS INCORRECT!
					bValidCarnetNo = false;
				}
				if ((Int64.Parse(sTIR_Carnet_No) > 0 ) && (Int64.Parse(sTIR_Carnet_No) < 14000001))
				{
					//No information available - The TIR Carnet was issued before the Recommendation of 20 October 1995 was adopted. 
					//Only TIR Carnet Numbers above 14 000 000 can be consulted!
					bValidCarnetNo = false;
				}
				if (Int64.Parse(sTIR_Carnet_No) >= 25000000)
				{
					//TIR CARNET NUMBERS 25, 000 000 AND ABOVE HAVE TWO CHARACTERS IN FRONT OF THE NUMBER! THE USER MUST CAPTURE THE TEN CHARACTERS!";
					bValidCarnetNo = false;
				}
				if ((Int64.Parse(sTIR_Carnet_No) >= 14000001) && (Int64.Parse(sTIR_Carnet_No) < 25000000))
				{
					//Valid TIR Number!
					bValidCarnetNo = true;
				}
			}

			if (CheckForCheckChar(sTIR_Carnet_No) == "ALPHA")
			{
				string sCheckCharVal = CheckChar(Int64.Parse(sTIR_Carnet_No.Substring(2)));

				if (sCheckCharVal.ToUpper() == (sTIR_Carnet_No.Substring(0, 2)))
				{
					//Valid TIR Number!
					bValidCarnetNo = true;
				}
				else 
				{
					//THE TIR CARNET NUMBER IS INCORRECT!
					bValidCarnetNo = false;
				}
			}
			if (CheckForCheckChar(sTIR_Carnet_No) == "INVALID")
			{
				//THE TIR CARNET NUMBER IS INCORRECT!
				bValidCarnetNo = false;
			}
			return bValidCarnetNo;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}