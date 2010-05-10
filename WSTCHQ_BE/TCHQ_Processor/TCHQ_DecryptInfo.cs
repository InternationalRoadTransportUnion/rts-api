using System;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;

namespace TCHQ_Processor
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Data Layer Class for Decrypting Encrypted Data.
	/// </summary>
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public class TCHQ_DecryptInfo
	{
		#region DecryptString
		public string DecryptString(string s_CipherText, uint length)
		{
			string s_DecryptedString  = "";
			byte[] cipher			  = null;
			bool success              = false;
			StringBuilder decryptData = null;

			cipher = new byte[length];
			decryptData = new StringBuilder((int)length);

			cipher = ASCIIEncoding.Default.GetBytes(s_CipherText);
			/////////////////////////////////////////////////////////////////
			// This Does not work due to the fact that C# strings are UNICODE
			// So Use the above line "ASCIIEncoding.Default.GetBytes(string)"
			/*
			s_CipherText = s_CipherText.Trim();
			length = (uint)s_CipherText.Length;
			try
			{
				for (int iLoop = 0; iLoop < (length); iLoop++)
				{
					cipher[iLoop] = Convert.ToByte(s_CipherText[iLoop]);
				}
			}
			catch (Exception e)
			{
				s_DecryptedString = e.ToString().Trim();
			}*/		
			/////////////////////////////////////////////////////////////////
			
			success = TCHQ_InvokeCryptDll.InitializeCrypto();
			success = TCHQ_InvokeCryptDll.Decrypt(decryptData, cipher, ref length);
			success = TCHQ_InvokeCryptDll.UninitializeCrypto();

			s_DecryptedString = decryptData.ToString();

			return s_DecryptedString;
		}
		#endregion

		#region DecryptInteger
		public string DecryptInteger(int n_CipherInt)
		{
			bool success = false;
			int n_DecryptedInt = 0;
		
			unsafe
			{
				uint len = sizeof(int);
				success  = TCHQ_InvokeCryptDll.InitializeCrypto();
				success  = TCHQ_InvokeCryptDll.DecryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = TCHQ_InvokeCryptDll.UninitializeCrypto();
			}
			return n_DecryptedInt.ToString();
		}
		#endregion

		#region DecryptWord
		public string DecryptWord(int n_CipherInt)
		{
			bool success = false;
			int n_DecryptedInt = 0;
		
			unsafe
			{
				uint len = sizeof(short);
				success  = TCHQ_InvokeCryptDll.InitializeCrypto();
				success  = TCHQ_InvokeCryptDll.DecryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = TCHQ_InvokeCryptDll.UninitializeCrypto();
			}
			return n_DecryptedInt.ToString();
		}
		#endregion
	}
}
