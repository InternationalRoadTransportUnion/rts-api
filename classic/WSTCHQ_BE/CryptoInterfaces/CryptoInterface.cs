using System;
using System.Collections;
using System.ServiceModel;

namespace IRU.RTS.CryptoInterfaces
{
	/// <summary>
	/// Interface to be implemented by CryptoProvider. Covers Common Crypto operations. This needs to be revisited once details of algorithms (GOST etc. are in).
	/// The interface is packaged in separate assembly so there is bare minimum dependency in case the crypto operations need to run on separate box
	/// </summary>
	[ServiceContract]
	public interface ICryptoOperations
	{
		/*
		/// <summary>
		/// Initialises the Crypto context if required
		/// </summary>
		/// <param name="initParams">Hashtable containing the parameters requried for initialisation</param>
		void Initialize(Hashtable initParams);
		*/

		/// <summary>
		/// Encrypt routine
		/// </summary>
		/// <param name="input">byte Array of data to encrypt</param>
		/// <param name="algName">Name of Alg implemented e.g. DES, RSA etc.</param>
		/// <param name="encryptParams">Hashtable containing the keys, IV etc.</param>
		/// <returns>encrypted byte array</returns>
		[OperationContract]
		byte[] Encrypt(byte[] input, string algName, ref Hashtable encryptParams);


		/// <summary>
		/// Decrypt routine
		/// </summary>
		/// <param name="input">byte array of Encrypted Data</param>
		/// <param name="algName">Name of algorithm to use</param>
		/// <param name="decryptParams">HAshtable containing parameters to decrypt</param>
		/// <returns>decrypted data byte array</returns>
		[OperationContract]
		byte[] Decrypt(byte[] input, string algName, Hashtable decryptParams);

		/// <summary>
		/// Hashing implementation
		/// </summary>
		/// <param name="input">Byte array of data to hash</param>
		/// <param name="algName">Hashing algorithm name, currently SHA1  .</param>
		/// <param name="hashParams">Hashtable of parameters</param>
		/// <returns>Hash value</returns>
		[OperationContract]
		byte[] Hash(byte[] input, string algName, Hashtable hashParams);



		/// <summary>
		/// Hashing implementation verification
		/// </summary>
		/// <param name="input">Byte array of data to hash</param>
		/// <param name="algName">Hashing algorithm name e.g. SHA-1 etc.</param>
		/// <param name="hashParams">Hashtable of parameters</param>
		/// <param name="hashToVerify">Hashvalue to Verify</param>
		/// <returns>true Verified, false not verified</returns>
		[OperationContract]
		bool VerifyHash(byte[] input, string algName, Hashtable hashParams, byte[] hashToVerify);




		/* Not required post Mos*/
//////
//////		/// <summary>
//////		/// Generate key
//////		/// </summary>
//////		/// <param name="algName">Keygeneration algorithm to use e.g. RSA</param>
//////		/// <param name="keyGenParams">Hashtable containing parameters to use</param>
//////		/// <returns>Hashtable containg the key</returns>
//////		object GenerateKey(string algName, Hashtable keyGenParams);


/*
		/// <summary>
		/// Generate random number of specified length
		/// </summary>
		/// <param name="length">Length in bytes</param>
		/// <param name="rngParams">hashtable of parameters</param>
		/// <returns>bytearray containing the number</returns>
		byte[] GenerateRandom(int length, Hashtable rngParams);
		
*/

		/*
		/// <summary>
		/// Used to release context etc.
		/// </summary>
		/// <param name="deInitParams"></param>
		void DeInitialize(Hashtable deInitParams);
		*/
	}


	/// <summary>
	/// Holds Key for RSA Alg
	/// </summary>
	[Serializable]
	public class RSACryptoKey
	{
		public byte[] Modulus ;//= new byte[128];
		public byte[] Exponent ;//= new byte[3];
		public byte[] D ;//= new byte[128];
		public byte[] P ;//64
		public byte[] Q ;//64
		public byte[] DP ;//64
		public byte[] DQ ;//64
		public byte[] INVERSEQ;//64
		
	}	
}
