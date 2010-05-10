using System;
using IRU.CommonInterfaces;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Performs Common Key Management Tasks
	/// </summary>
	public class KeyManager
	{

		/// <summary>
		/// Initiates the key manager by storing the reference to DBHelper to the DB containing the keys table and the name of the keys Table 
		/// </summary>
		/// <param name="KeysDBHelper"></param>
		/// <param name="KeyTable"></param>
		public KeyManager(IDBHelper KeysDBHelper, string KeyTable )
		{
			//
			// TODO: Add constructor logic here
			//
		}
/// <summary>
/// Gets in the MessageTag which is used to lookup the keys in the KeyTable using the KeysDBHelper.
/// Once the key is retrieved the keys will be regenerated.  
/// </summary>
/// <param name="MessageTag">Key identifier in the Key Table</param>
/// <param name="ServiceID">Service ID for which these keys will be used</param>
/// <param name="EncryptionKeyID">Encryption Key byte array[]</param>
/// <param name="KeyIndexUsed">Internal Index</param>
/// <param name="EncryptionKey">Key used for decryption as byte array</param>
/// <param name="SignatureKey">Signature Keys as byte array</param>
/// <param name="SubscriberID">ID to whom keys were sent</param>
/// <param name="KeyStatus">
		///2 = Key already used,
		///3 = Key discarded,
		///9 = No record found,
	///</param>
		public void GetKeysforDecryption(string MessageTag, out string ServiceID, out string EncryptionKeyID, out long KeyIndexUsed, out byte[] EncryptionKey, out byte[] SignatureKey, out string SubscriberID, out int KeyStatus)
		{ MessageTag="";
			ServiceID="";
			EncryptionKeyID="";
			KeyIndexUsed =1;
			EncryptionKey = new byte[100];
			 SignatureKey= new byte[100];
			SubscriberID="324";
			KeyStatus=1;
		}

		/// <summary>
		/// Gets in the MessageTag which is used to lookup the keys in the KeyTable.
		/// Once the key is retreived the keys have to be regenerated this is done using the RegenKeys Function.
		/// </summary>
		/// <param name="SubscriberID">ID of the subscriber</param>
		/// <param name="ServiceID">Service ID</param>
		/// <param name="EncryptionKeyID">Message Tag
		/// </param>
		/// <param name="KeyIndexUsed">Value of Index field</param>
		/// <param name="EncryptionKey">byte array containing the value of the encryption key</param>
		/// <param name="SignatureKey">byte array containing the value of the signature key</param>
		/// <param name="KeyStatus">
		///2 = Key already used,
		///3 = Key discarded,
		///9 = No record found,
		///</param>
			public void GetKeysforEncryption( string SubscriberID, string ServiceID,  out string EncryptionKeyID, out long KeyIndexUsed, out byte[] EncryptionKey, out byte[] SignatureKey, out int KeyStatus)
		{

			ServiceID="";
			EncryptionKeyID="";
			KeyIndexUsed =1;
			EncryptionKey = new byte[100];
			SignatureKey= new byte[100];
			SubscriberID="324";
			KeyStatus=1;
		}


		/// <summary>
		/// Regenerates the keys corresponding to the index used. The Key regeneration algorithm is described in the Chapter 2 - Information Exchange design
		/// </summary>
		/// <param name="KeyIndexUsed"></param>
		private void RegenKeys(long KeyIndexUsed)
		{
		
		}


		

	}
}
