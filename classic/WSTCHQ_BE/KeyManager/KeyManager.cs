using System;
using IRU.CommonInterfaces;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IRU.RTS.CryptoInterfaces;


namespace IRU.RTS.Crypto
{
	/// <summary>
	/// Performs Common Key Management Tasks
	/// </summary>
	public class KeyManager
	{


		//Keystatus consts; do not use these outside this class
		private const int SUBSCRIBERIDSDONOTMATCH = 7;
		private const int KEYDISCARDED = 3;
		private const int NOKEYFOUND = 9;
		private const int OK = 1;

			


		/// <summary>
		/// Selects a key from the subscriber keys table
		/// </summary>
		/// <param name="SubscriberID"></param>
		/// <param name="Key">Contains key components</param>
		/// <param name="EncryptionKeyID">ID of the key selected</param>
		/// <param name="KeysDBHelper"></param>
		/// <returns>1 if ok, 9 if no key found or inactive key found</returns>
		public static int  AssignSubscriberKey(string SubscriberID, out RSACryptoKey Key, out string EncryptionKeyID ,IDBHelper KeysDBHelper)
		{
			//we should get only one row we will use the top 1
			//mandar 8-12-2005 added key_active=1 and cert_expiry_date
			string sSelect = "select TOP 1 ENCRYPTION_KEY_ID,MODULUS,EXPONENT, KEY_ACTIVE  from dbo.subscriber_encryption_keys where SUBSCRIBER_ID=@SUBSCRIBER_ID AND KEY_ACTIVE=1 AND CERT_EXPIRY_DATE > @CURRENTDATE";

			SqlCommand sqlSelect = new SqlCommand(sSelect);

			sqlSelect.Parameters.Add("@SUBSCRIBER_ID",SqlDbType.VarChar,50);

			sqlSelect.Parameters["@SUBSCRIBER_ID"].Value= SubscriberID;

			sqlSelect.Parameters.Add("@CURRENTDATE",SqlDbType.DateTime);

			sqlSelect.Parameters["@CURRENTDATE"].Value= DateTime.Now;


			IDataReader sdr = KeysDBHelper.GetDataReader(sqlSelect,CommandBehavior.SingleRow);

			bool bRowsFound = false; //check if keyid is valid

			//result holders;
			byte[] aModulus = null;
			byte[] aExponent =   null;



			bool isKey_Active =false;
			string encryptionKeyID = "";

			Key = null;
			EncryptionKeyID="";

			try
			{
				//TOP 1 ENCRYPTION_KEY_ID,MODULUS,EXPONENT,KEY_ACTIVE  

				while (sdr.Read())
				{
					encryptionKeyID = (string)sdr["ENCRYPTION_KEY_ID"];
					aModulus =    (byte[])sdr["MODULUS"];
					aExponent =    (byte[])sdr["EXPONENT"];
					
					isKey_Active = (bool)sdr["KEY_ACTIVE"];
					bRowsFound = true;
				}
			}
			finally
			{
			
				sdr.Close();
			}

			if (bRowsFound==false)
			{
				
				return NOKEYFOUND;
			}



			//We need a new status for this one
			if (isKey_Active==false)
			{
				Key = null;
				return NOKEYFOUND;
			}



			

			Key  = new RSACryptoKey();
			
			Key.Modulus=aModulus;
			Key.Exponent=aExponent;
			
			EncryptionKeyID = encryptionKeyID;
			return OK;
		
		
		
		
		}

		/// <summary>
		/// Gets key details given a KeyID. Needs open connection to DB
		/// </summary>
		/// <param name="KeyID">ID for which to fetch details, </param>
		/// <param name="Key">Key struct will have D populated</param>
		/// <param name="SubscriberID"> Find key distributed to this subscriber</param>
		/// <param name="KeysDBHelper">Dbhelper to SubscriberDB</param>
		/// <returns>Keystatus 1,7,3,9</returns>
		
		public static int GetIRUKeyDetails(string KeyID, string SubscriberID,  out RSACryptoKey Key,  IDBHelper KeysDBHelper)
		{
/*

1	Ok

7	Subscriber IDs do not match
3	Key discarded
9	No key found
*/

            string sSelect = "select ENCRYPTION_KEY_ID,MODULUS,EXPONENT,D,P,Q,DP,DQ,INVERSEQ,DISTRIBUTED_TO,DISTRIBUTION_DATE,KEY_ACTIVE,KEY_ACTIVE_REASON,CERT_IS_CURRENT,CERT_EXPIRY_DATE from dbo.iru_encryption_keys where encryption_key_id=@KeyID";

			SqlCommand sqlSelect = new SqlCommand(sSelect);

			sqlSelect.Parameters.Add("@KeyID",SqlDbType.VarChar,50);

			sqlSelect.Parameters["@KeyID"].Value= KeyID;

			IDataReader sdr = KeysDBHelper.GetDataReader(sqlSelect,CommandBehavior.SingleRow);

			bool bRowsFound = false; //check if keyid is valid

			//result holders;
			



			string distributedTo ="";  
			bool isKey_Active =false;
			

			DateTime dtExpiry= DateTime.Now; //cert expiry ignored

			Key = null;


			Key = new RSACryptoKey(); 

			try
			{

				while (sdr.Read())
				{
//ENCRYPTION_KEY_ID,MODULUS,EXPONENT,D,DISTRIBUTED_TO,
//DISTRIBUTION_DATE,KEY_ACTIVE,KEY_ACTIVE_REASON,CERT_IS_CURRENT,CERT_EXPIRY_DATE

					Key.Modulus =    (byte[])sdr["MODULUS"];
					Key.Exponent =    (byte[])sdr["EXPONENT"];
					Key.D =    (byte[])sdr["D"];


					Key.P =    (byte[])sdr["P"];
					Key.Q =    (byte[])sdr["Q"];

					Key.DP =    (byte[])sdr["DP"];

					Key.DQ =    (byte[])sdr["DQ"];

					Key.INVERSEQ =    (byte[])sdr["INVERSEQ"];

					distributedTo =    (string)sdr["DISTRIBUTED_TO"];
					isKey_Active = (bool)sdr["KEY_ACTIVE"];
					dtExpiry = (DateTime) sdr["CERT_EXPIRY_DATE"];
				
					//dtExpiry = dtCertExpiry.Value;
					//byte[] Modulus
					bRowsFound = true;
					
				
			
				}
			}
			finally
			{
			
				sdr.Close();
			}

			if (bRowsFound==false)
			{
				Key = null;
				return NOKEYFOUND;
			}


			if (SubscriberID != distributedTo)
			{
				Key = null;
				return SUBSCRIBERIDSDONOTMATCH;
			
			}


			//expiry date not checked
			if (isKey_Active==false)
			{
				Key = null;
				return KEYDISCARDED;
			}


			if (dtExpiry < DateTime.Now)
			{
				Key = null;
				return KEYDISCARDED;
			
			}





			return OK;
		}

		
	}

}
