using System;
using System.Text;
using System.Runtime.InteropServices;

namespace IRU.CryptEngine
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Data Layer Class for Loading the Crypto DLL, using the DllImport Method.
	/// </summary>
	////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public class IRU_InvokeCryptDll
	{
		[DllImport("CryptDllD.dll")]
		public static extern bool InitializeCrypto();

		[DllImport("CryptDllD.dll")]
		public static extern bool UninitializeCrypto();

		[DllImport("CryptDllD.DLL", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
		public static extern bool Encrypt(string text,
			[In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)] byte[] cipher, 
			ref uint length);

		[DllImport("CryptDllD.dll", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
		public static extern bool Decrypt(StringBuilder text, 
			[In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)]byte[] cipher, 
			ref uint length);

		[DllImport("CryptDllD.dll", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
		public static extern bool DecryptNumFixedLength(ref int text, ref int cipher, ref uint length);

		[DllImport("CryptDllD.dll", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
		public static extern bool EncryptNumFixedLength(ref int text, ref int cipher, ref uint length);
	}
}
