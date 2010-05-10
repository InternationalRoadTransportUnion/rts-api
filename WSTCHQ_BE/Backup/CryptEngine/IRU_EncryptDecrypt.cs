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
			/*
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
			success= IRU_InvokeCryptDll.InitializeCrypto();
			success = IRU_InvokeCryptDll.Encrypt(asciiString, cipher, ref length);
			success = IRU_InvokeCryptDll.UninitializeCrypto();
			
			// Fill the StringBuilder, cipherData with ASCII Chars
			cipherData.Append(ASCIIEncoding.Default.GetChars(cipher));

			s_CipheredTxt = cipherData.ToString();

			return s_CipheredTxt;
			*/
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

			// Perform the conversion from one encoding to the other.
			byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            
			// Convert the new byte[] into a char[] and then into a string.
			char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
			ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
			string asciiString = new string(asciiChars);

			// Call the Crypt functionality
			success = IRU_InvokeCryptDll.InitializeCrypto();
			success = IRU_InvokeCryptDll.Encrypt(asciiString, cipher, ref length);
			success = IRU_InvokeCryptDll.UninitializeCrypto();
			
			// Fill the StringBuilder, cipherData with ASCII Chars
			//cipherData.Append(ASCIIEncoding.Default.GetChars(cipher));
			//cipherData.Append(ASCIIEncoding.Default.GetString(cipher));
			cipherData.Append(Convert.ToBase64String(cipher));


			s_CipheredTxt = cipherData.ToString();
			int iLenEncr = s_CipheredTxt.Length ;

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
				success = IRU_InvokeCryptDll.InitializeCrypto();
				success  = IRU_InvokeCryptDll.EncryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = IRU_InvokeCryptDll.UninitializeCrypto();
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
				success= IRU_InvokeCryptDll.InitializeCrypto();
				success  = IRU_InvokeCryptDll.EncryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = IRU_InvokeCryptDll.UninitializeCrypto();
			}
			return n_CipherInt.ToString();
		}
		#endregion
		
		#region DecryptString
		public string DecryptString(string s_CipherText, uint length)
		{
			/*
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
			
//			s_CipherText = s_CipherText.Trim();
//			length = (uint)s_CipherText.Length;
//			try
//			{
//				for (int iLoop = 0; iLoop < (length); iLoop++)
//				{
//					cipher[iLoop] = Convert.ToByte(s_CipherText[iLoop]);
//				}
//			}
//			catch (Exception e)
//			{
//				s_DecryptedString = e.ToString().Trim();
//			}	
			/////////////////////////////////////////////////////////////////
			
			success= IRU_InvokeCryptDll.InitializeCrypto();
			success = IRU_InvokeCryptDll.Decrypt(decryptData, cipher, ref length);
			success = IRU_InvokeCryptDll.UninitializeCrypto();

			s_DecryptedString = decryptData.ToString();

			return s_DecryptedString;
			*/
			string s_DecryptedString  = "";
			byte[] cipher			  = null;
			bool success              = false;
			StringBuilder decryptData = null;

			Byte[] bytes = Convert.FromBase64String(s_CipherText);

			uint n_CipheredStringTextLen = (uint)bytes.Length;
			length = n_CipheredStringTextLen;

			cipher = new byte[length];
			decryptData = new StringBuilder((int)length);

			cipher = Convert.FromBase64String(s_CipherText);

			
			success = IRU_InvokeCryptDll.InitializeCrypto();
			success = IRU_InvokeCryptDll.Decrypt(decryptData, cipher, ref length);
			success = IRU_InvokeCryptDll.UninitializeCrypto();

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
				success= IRU_InvokeCryptDll.InitializeCrypto();
				success  = IRU_InvokeCryptDll.DecryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = IRU_InvokeCryptDll.UninitializeCrypto();
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
				success= IRU_InvokeCryptDll.InitializeCrypto();
				success  = IRU_InvokeCryptDll.DecryptNumFixedLength(ref n_DecryptedInt, ref n_CipherInt, ref len);
				success  = IRU_InvokeCryptDll.UninitializeCrypto();
			}
			return n_DecryptedInt.ToString();
		}
		#endregion
	}
}
