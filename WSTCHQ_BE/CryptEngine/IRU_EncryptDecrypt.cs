using System;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;

namespace IRU.CryptEngine
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Data Layer Class for Decrypting Encrypted Data.
	/// </summary>
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public class IRU_EncryptDecrypt
	{
		#region EncryptString
		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////// Call this method to encrypt a C# (Unicode) String ///////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////
		public string EncryptString(string s_PlainTxt)
		{
            return IRU.Common.Crypto.Cipher.EngineHelper.CryptStringToBase64(s_PlainTxt, null);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		
		#region EncryptInteger
		public int EncryptInteger(int n_DecryptedInt)
		{
            return (int) IRU.Common.Crypto.Cipher.EngineHelper.CryptInteger((UInt32) n_DecryptedInt);
		}
		#endregion

		#region EncryptWord
		public string EncryptWord(int n_DecryptedInt)
		{
            return IRU.Common.Crypto.Cipher.EngineHelper.CryptInteger((UInt32)n_DecryptedInt).ToString();
		}
		#endregion
		
		#region DecryptString
		public string DecryptString(string s_CipherText, uint length)
		{
            return IRU.Common.Crypto.Cipher.EngineHelper.DecryptBase64ToString(s_CipherText.Substring(0, (int)length), null);
		}
		#endregion

		#region DecryptInteger
		public string DecryptInteger(int n_CipherInt)
		{
            return IRU.Common.Crypto.Cipher.EngineHelper.DecryptInteger((UInt32) n_CipherInt).ToString();
		}
		#endregion

		#region DecryptWord
		public string DecryptWord(int n_CipherInt)
		{
            if (n_CipherInt < UInt16.MaxValue)
                return IRU.Common.Crypto.Cipher.EngineHelper.DecryptWord((UInt16)n_CipherInt).ToString();
            else
                return DecryptInteger(n_CipherInt);
		}
		#endregion
	}
}
