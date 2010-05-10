using System;
using IRU.RTS.CryptoInterfaces;
using IRU.CommonInterfaces;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using IRU.RTS.CommonComponents;
using System.Xml;


namespace IRU.RTS.CryptoProvider
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CryptoOperations: MarshalByRefObject,  ICryptoOperations
	{
		public CryptoOperations()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region ICryptoOperations Members

		#region Encrypt
		public byte[] Encrypt(byte[] input, string algName,ref System.Collections.Hashtable encryptParams)
		{
			

			switch (algName)
			{
				case "RSA":
						return RSAEncrypt(input,encryptParams);
					break;
				case "3DES":
						return TripleDesEncrypt(input, ref encryptParams);
					break;
				default :
						throw new ApplicationException("UnSupported Algorithm," + algName);
					break;
			
			
					
			}

			// TODO:  Add CryptoOperations.Encrypt implementation
			return null;
		}

		private byte[] RSAEncrypt(byte[] input, Hashtable encryptParams)
		{
			
			RSAParameters rsaParams = new RSAParameters();

			rsaParams.Exponent= (byte[] )encryptParams["EXPONENT"]; 

			rsaParams.Modulus=(byte[] )encryptParams["MODULUS"]; 

			

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

			rsa.ImportParameters(rsaParams);

			byte[] returnArray =  rsa.Encrypt(input,false);

			
			return returnArray;





		
		}

		

		private byte[] TripleDesEncrypt(byte[] input, ref Hashtable encryptParams)
		{
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

			tdes.GenerateKey();
			
			byte[] bTDKey = tdes.Key;

			byte[] bTDIV = (byte[])encryptParams["IV"]  ;// {0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa};

			tdes.IV= bTDIV;

			ICryptoTransform iEnc=  tdes.CreateEncryptor();
			
			MemoryStream mStream = new MemoryStream();
			
			CryptoStream cs = new CryptoStream(mStream,iEnc,CryptoStreamMode.Write);
			
			cs.Write(input,0,input.Length);
			cs.FlushFinalBlock();
			cs.Close();

			//tdes.Clear();//release

			encryptParams["KEY"] = bTDKey; // set the value and send back to client
			return mStream.ToArray();
		
		}

		#endregion 

		#region Hash
		public bool VerifyHash(byte[] input, string algName, System.Collections.Hashtable hashParams, byte[] hashToVerify)
		{
			byte[] hash = null ;

			//calculate hash first 
			switch  (algName)
			{
				case "SHA1":
					SHA1 shHasher = new SHA1Managed();

					 hash =  shHasher.ComputeHash(input);
					
					
					break;

				case "MD5":
					MD5 mdHasher = new MD5CryptoServiceProvider();

					hash = mdHasher.ComputeHash(input);
					break;
				default:
					throw new ApplicationException("UnSupported Algorithm," + algName);

			}

			bool bEqual = false;
			//now compare EACH byte

			if (hash.Length == hashToVerify.Length)
			{
				int iCntr=0;
				while ((iCntr < hash.Length) && (hash[iCntr] == hashToVerify[iCntr]))
				{
					iCntr += 1;
				}
				if (iCntr == hashToVerify.Length)
				{
				bEqual = true;
				}
			}

			return bEqual;
		}

		public byte[] Hash(byte[] input, string algName, System.Collections.Hashtable hashParams)
		{
			switch  (algName)
			{
				case "SHA1":
					SHA1 shHasher = new SHA1Managed();

					return shHasher.ComputeHash(input);
					break;

				case "MD5":
					MD5 mdHasher = new MD5CryptoServiceProvider();

					return mdHasher.ComputeHash(input);
					break;
				default:
					throw new ApplicationException("UnSupported Algorithm," + algName);

			}

			return null;
		}

		
		#endregion Hash
		
		#region Decrypt
		public byte[] Decrypt(byte[] input, string algName, System.Collections.Hashtable decryptParams)
		{

			switch (algName)
			{
				case "RSA":
					return RSADecrypt(input,decryptParams);
					break;
				case "3DES":
					return TripleDesDecrypt(input,decryptParams);
					break;
				default :
					throw new ApplicationException("UnSupported Algorithm," + algName);
					break;
			
			
			}

			// TODO:  Add CryptoOperations.Encrypt implementation
			return null;		
		}


		private byte[] RSADecrypt(byte[] input, Hashtable encryptParams)
		{
			
			RSAParameters rsaParams = new RSAParameters();

		
			rsaParams.Exponent= (byte[] )encryptParams["EXPONENT"]; 

			rsaParams.Modulus=(byte[] )encryptParams["MODULUS"]; 
			
			rsaParams.D =(byte[] )encryptParams["D"]; 
			
			rsaParams.P =(byte[] )encryptParams["P"]; 
			rsaParams.Q=(byte[] )encryptParams["Q"]; 
			
			rsaParams.DP =(byte[] )encryptParams["DP"]; 
			rsaParams.DQ =(byte[] )encryptParams["DQ"]; 
			rsaParams.InverseQ =(byte[] )encryptParams["INVERSEQ"]; 

/*
			string rsaXML = "<RSAKeyValue><Modulus></Modulus><Exponent></Exponent><P></P><Q></Q><DP></DP><DQ></DQ><InverseQ></InverseQ><D></D></RSAKeyValue>";
			XmlDocument xd = new XmlDocument();

			xd.LoadXml(rsaXML);
 


			xd.DocumentElement["Modulus"].InnerText= System.Convert.ToBase64String( (byte[] )encryptParams["MODULUS"]); 
			xd.DocumentElement["Exponent"].InnerText= System.Convert.ToBase64String( (byte[] )encryptParams["EXPONENT"]); 

			
			
			xd.DocumentElement["D"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["D"]); 
			
			xd.DocumentElement["P"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["P"]); 
			xd.DocumentElement["Q"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["Q"]); 
			
			xd.DocumentElement["DP"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["DP"]); 
			xd.DocumentElement["DQ"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["DQ"]); 
			xd.DocumentElement["InverseQ"].InnerText= System.Convert.ToBase64String((byte[] )encryptParams["INVERSEQ"]); 


			CspParameters csp = new CspParameters();
			csp.KeyContainerName ="IRUKey20050927003602";

			csp.Flags=CspProviderFlags.UseMachineKeyStore;



			
*/
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			//rsa.FromXmlString(xd.OuterXml);
			
			rsa.ImportParameters(rsaParams);

			byte[] returnArray =  rsa.Decrypt(input,false);

			

			return returnArray;

		
		}


		private byte[] TripleDesDecrypt(byte[] input, Hashtable encryptParams)
		{
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
				
			byte[] bTDKey = (byte[])encryptParams["KEY"];

			byte[] bTDIV = (byte[])encryptParams["IV"]  ;// {0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa};

			tdes.IV= bTDIV;

			tdes.Key=bTDKey;

			ICryptoTransform iDec=  tdes.CreateDecryptor();
			MemoryStream mStream = new MemoryStream();
			
			CryptoStream cs = new CryptoStream(mStream,iDec,CryptoStreamMode.Write);
			
			cs.Write(input,0,input.Length);
			cs.FlushFinalBlock();
			cs.Close();
			//tdes.Clear();//release
			return mStream.ToArray();
		
		}


		#endregion Decrypt
		#endregion
	}
}
