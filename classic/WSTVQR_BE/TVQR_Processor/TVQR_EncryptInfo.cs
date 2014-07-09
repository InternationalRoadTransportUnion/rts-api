using System;
using System.Data;
using System.Text;

namespace TVQR_Processor
{	
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Data Layer Class for Encrypting Decrypted Data.
	/// </summary>
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public class TVQR_EncryptInfo
	{
		#region EncryptString
		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////// Call this method to encrypt a C# (Unicode) String ///////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////
		public string EncryptString(string s_PlainTxt)
		{
			byte[] cipher			  = null;
			bool success			  = false;
			uint length				  = 0;
			StringBuilder cipherData  = null;
			string s_CipheredTxt	  = "";
			string unicodeString	  = "";

			length = (uint)s_PlainTxt.Length;
			cipher = new byte[length];
			cipherData = new StringBuilder((int)length);

			cipher		  = ASCIIEncoding.Default.GetBytes(s_PlainTxt);
			unicodeString = s_PlainTxt;

			// Create two different encodings.
			Encoding ascii   = Encoding.ASCII;
			Encoding unicode = Encoding.Unicode;

			// Convert the s_PlainTxt string into a byte[].
			byte[] unicodeBytes = unicode.GetBytes(unicodeString);
		//	byte[] unicodeBytes = unicode.GetBytes(UnicodeEncoding.Default.GetString(cipher));// unicodeString);



			// Perform the conversion from one encoding to the other.
			byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            
			// Convert the new byte[] into a char[] and then into a string.
			char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
			ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
			string asciiString = new string(asciiChars);

			// Call the Crypt functionality
			success = TVQR_InvokeCryptDll.InitializeCrypto();
			success = TVQR_InvokeCryptDll.Encrypt(asciiString, cipher, ref length);
			success = TVQR_InvokeCryptDll.UninitializeCrypto();
			
			// Fill the StringBuilder, cipherData with ASCII Chars
			cipherData.Append(ASCIIEncoding.Default.GetChars(cipher));

			s_CipheredTxt = cipherData.ToString();

			return s_CipheredTxt;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		
		#region EncryptInteger
		public int EncryptInteger(int n_DecryptedInt)
		{
			bool success = false;
			int n_CipherInt = 0;
		
			unsafe
			{
				uint len = sizeof(int);
				success  = TVQR_InvokeCryptDll.InitializeCrypto();
				success  = TVQR_InvokeCryptDll.EncryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = TVQR_InvokeCryptDll.UninitializeCrypto();
			}
			return int.Parse(n_CipherInt.ToString());
		}
		#endregion

		#region EncryptWord
		public string EncryptWord(int n_DecryptedInt)
		{
			bool success = false;
			int n_CipherInt = 0;
		
			unsafe
			{
				uint len = sizeof(short);
				success  = TVQR_InvokeCryptDll.InitializeCrypto();
				success  = TVQR_InvokeCryptDll.EncryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = TVQR_InvokeCryptDll.UninitializeCrypto();
			}
			return n_CipherInt.ToString();
		}
		#endregion
	}
}
