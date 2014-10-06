using System;

using System.IO;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml;
using System.Text;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace IRU.RTS.Crypto
{
	/// <summary>
	/// Summary description for CertGenerator.
	/// </summary>
	public class CertGenerator
	{
/*
 const uint CERT_SYSTEM_STORE_CURRENT_USER	= 0x00010000;
 const uint CERT_STORE_READONLY_FLAG		= 0x00008000;
 const uint CERT_STORE_OPEN_EXISTING_FLAG	= 0x00004000;
 const uint CERT_FIND_SUBJECT_STR	= 0x00080007;
 const uint X509_ASN_ENCODING 		= 0x00000001;
 const uint PKCS_7_ASN_ENCODING 	= 0x00010000;
 const uint RSA_CSP_PUBLICKEYBLOB	= 19;
 const int  AT_KEYEXCHANGE		= 1;  //keyspec values
 const int  AT_SIGNATURE		= 2;
 */
		static uint ENCODING_TYPE 		= PKCS_7_ASN_ENCODING | X509_ASN_ENCODING ;
		const uint RSA_CSP_PUBLICKEYBLOB	= 19;
		private const int CERT_KEY_PROV_INFO_PROP_ID = 2;
		private const int X509_ASN_ENCODING = 1;
		private const int PROV_RSA_FULL = 1;
		private const uint PKCS_7_ASN_ENCODING = 0x00010000;
		private const uint CRYPT_VERIFYCONTEXT = 0xF0000000;
		private const int CRYPT_NEWKEYSET = 8;
		private const int PUBLICKEYBLOB = 6; 

		private const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1;
		private const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2;
		private const int CERT_SYSTEM_STORE_LOCATION_SHIFT = 16;

		private const uint CERT_STORE_READONLY_FLAG =  0x00008000;


		private const uint CERT_STORE_PROV_SYSTEM_A = 9;

		private const uint CERT_SYSTEM_STORE_CURRENT_USER =
			CERT_SYSTEM_STORE_CURRENT_USER_ID << 
			CERT_SYSTEM_STORE_LOCATION_SHIFT;
		private const uint CERT_SYSTEM_STORE_LOCAL_MACHINE =
			CERT_SYSTEM_STORE_LOCAL_MACHINE_ID << 
			CERT_SYSTEM_STORE_LOCATION_SHIFT;



		public CertGenerator()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void GenerateCertificate(string CertFolder, string EMail, string Subscriber, DateTime StartDate, DateTime ExpiryDate, string signatureAlgorithm, out string StandardOutput, out string CommandLine, out DateTime dtGenerated, out string CertPath)
		{
		
			// create makecert command line
            
			FileInfo fi = new FileInfo("makecert.exe");
			if (!fi.Exists)
			{
				throw new FileNotFoundException("MakeCert.exe should be in the same folder as Admin Client","MakeCert.exe",null);
			}
			//need to store this time in DB rather than getdate
			dtGenerated = DateTime.Now;

			string keyContainer = "IRUKey" +  dtGenerated.ToString("yyyyMMddHHmmss");

			CertPath= CertFolder +  "\\" + Subscriber + dtGenerated.ToString("yyyyMMddHHmmss")+".cer";
			
			//-len is important as on win2k it gens 512 bit keys 
            CultureInfo ciUS = new CultureInfo("en-US");
            CommandLine = " -r -pe -len 1024 -n \"CN= IRU.org RTS, OU=Subscriber :" + Subscriber + " , O=IRU.org, E=" + EMail + "\"  -sr localmachine -ss IRUTEST -b " + StartDate.ToString("MM/dd/yyyy", ciUS) + " -e " + ExpiryDate.ToString("MM/dd/yyyy", ciUS) + " -a " + signatureAlgorithm + " \"" + CertPath + "\""; 

			//overwrite the cert if already exists

			ProcessStartInfo pStart = new ProcessStartInfo("makecert.exe");
			pStart.Arguments =  CommandLine;
			pStart.UseShellExecute=false;
			pStart.RedirectStandardOutput=true;
			pStart.WindowStyle = ProcessWindowStyle.Minimized;


			Process pMakeCert= Process.Start(pStart);

			StandardOutput = pMakeCert.StandardOutput.ReadToEnd();

			pMakeCert.WaitForExit();
			if (pMakeCert.ExitCode != 0)
			{
				throw new ApplicationException("MakeCert Failure, Pls. review the Standard output and contact administrator",null);
			}

			
		
		
		}



		[Obsolete]
		public void ExtractKeys(string CertPath,  out RSAParameters RsaParams, out string ThumbPrint)
		{
	
			string generatedKeyXML = null;

			//load the cert in class

			
			//load certificate
			
			X509Certificate2 xCert = new X509Certificate2();
            xCert.Import(CertPath, (string)null, X509KeyStorageFlags.Exportable);
			//try access properties

			ThumbPrint = xCert.GetCertHashString();

			string certCN = xCert.GetName();


			// Enumerate certs in IRUTEST Store and find matching thumbprint


			//debug
			//System.IntPtr hCertStore = new IntPtr( Crypt32Helper.CertOpenSystemStore(0, "IRUTEST"));



			System.IntPtr hCertStore =new IntPtr( Crypt32Helper.CertOpenStore(CERT_STORE_PROV_SYSTEM_A,
				X509_ASN_ENCODING | PKCS_7_ASN_ENCODING,
				0,
				CERT_SYSTEM_STORE_LOCAL_MACHINE | CERT_STORE_READONLY_FLAG, 
				"IRUTEST"));




			//ThumbPrint = "15D4B83D3A569C2461405D3F8496C6038402CF39";

			//uint hCertStore = Crypt32Helper.CertOpenSystemStore(0, "MY");
			
			if (hCertStore == IntPtr.Zero)
			{
				throw new ApplicationException ("CertOpenSystemStore failed: " + Marshal.GetLastWin32Error().ToString());
				return;
			}

			X509Certificate xCertInStore = null ;

			uint pMyCertContext = 0;
			uint pCertContext = Crypt32Helper.CertEnumCertificatesInStore((uint)hCertStore.ToInt32(), (uint)0);

			while (pCertContext != 0) 
			{
				xCertInStore = new X509Certificate((IntPtr)pCertContext);
				string thisCertCN= xCertInStore.GetName();
				string thisCertThumbPrint = xCertInStore.GetCertHashString();
				if ( thisCertCN==certCN  && thisCertThumbPrint == ThumbPrint  )
				{
					// Increment the reference count so that you can use this value later.
					pMyCertContext= Crypt32Helper.CertDuplicateCertificateContext(pCertContext);
					break;
				}
				
				pCertContext = Crypt32Helper.CertEnumCertificatesInStore((uint)hCertStore.ToInt32(), pCertContext);
			} 
		
			if (pMyCertContext==0) //no cert found
			{
				throw new ApplicationException ("CertEnumCertificatesInStore failed: Unable to locate by CN and thumbprint check certificate is in LocalComputer IRUTEST store using Certificate MMC snapin");
				
			
			}

			//Get hold of RSACryptoServiceProvider

			RSACryptoServiceProvider rsa;

			rsa = RSACryptoServiceProviderFromCertContext(new IntPtr((int)pMyCertContext));


			RsaParams = rsa.ExportParameters(true);

			Crypt32Helper.CertCloseStore(hCertStore,0);

			/*
			generatedKeyXML = rsa.ToXmlString(true);

			rsa.Clear();


			XmlDocument xKeys = new XmlDocument();

			xKeys.LoadXml(generatedKeyXML);

			string sModulus, sExponent, sKey;

			sModulus = xKeys.DocumentElement["Modulus"].InnerText;
			sExponent = xKeys.DocumentElement ["Exponent"].InnerText;
			sKey = xKeys.DocumentElement["D"].InnerText;

			Modulus = System.Convert.FromBase64String(sModulus);
			Exponent = System.Convert.FromBase64String(sExponent);
			D = System.Convert.FromBase64String(sKey);
*/
		
		
		}



		public void ExtractKeys2(string CertPath,  out RSAParameters RsaParams, out string ThumbPrint)
		{
	
			string generatedKeyXML = null;

			//load the cert in class

			
			//load certificate
			
			System.Security.Cryptography.X509Certificates.X509Certificate xCert = X509Certificate.CreateFromCertFile(CertPath);
			//Microsoft.Web.Services2.Security.X509.X509Certificate xCert =Microsoft.Web.Services2.Security.X509.X509Certificate.CreateCertFromFile(CertPath);
			//try access properties

			ThumbPrint = xCert.GetCertHashString();

			string certCN = xCert.GetName();
			// Enumerate certs in IRUTEST Store and find matching thumbprint
			//debug
			System.IntPtr hCertStore = new IntPtr( Crypt32Helper.CertOpenSystemStore(0, "IRUTEST"));

			//ThumbPrint = "15D4B83D3A569C2461405D3F8496C6038402CF39";

			//uint hCertStore = Crypt32Helper.CertOpenSystemStore(0, "MY");
			
			if (hCertStore == IntPtr.Zero)
			{
				throw new ApplicationException ("CertOpenSystemStore failed: " + Marshal.GetLastWin32Error().ToString());
				return;
			}

			X509Certificate2 xCertInStore = null ;

			uint pMyCertContext = 0;
			uint pCertContext = Crypt32Helper.CertEnumCertificatesInStore((uint)hCertStore.ToInt32(), (uint)0);

			while (pCertContext != 0) 
			{
				xCertInStore = new X509Certificate2((IntPtr)pCertContext);
				string thisCertCN= xCertInStore.GetName();
				string thisCertThumbPrint = xCertInStore.GetCertHashString();
				if ( thisCertCN==certCN  && thisCertThumbPrint == ThumbPrint  )
				{
					// Increment the reference count so that you can use this value later.
					pMyCertContext= Crypt32Helper.CertDuplicateCertificateContext(pCertContext);
					break;
				}
				
				pCertContext = Crypt32Helper.CertEnumCertificatesInStore((uint)hCertStore.ToInt32(), pCertContext);
			} 
		
			if (pMyCertContext==0) //no cert found
			{
				throw new ApplicationException ("CertEnumCertificatesInStore failed: Unable to locate by CN and thumbprint check certificate is in LocalComputer IRUTEST store using Certificate MMC snapin");
				
			
			}

			//Get hold of RSACryptoServiceProvider

			//RSA outRSA = xCertInStore.PublicKey;

			//RsaParams = outRSA.ExportParameters(false);


			RSA rsa = RSACryptoServiceProviderFromCertContext(new IntPtr((int)pMyCertContext));
			RsaParams = rsa.ExportParameters(true);

//Debug
			/*
			Hashtable ht = new Hashtable();
			ht["EXPONENT"]= RsaParams.Exponent;
			ht["MODULUS"] = RsaParams.Modulus;
			ht["P"] = RsaParams.P;
			ht["Q"] = RsaParams.Q;
			ht["DP"] = RsaParams.DP;
			ht["DQ"] = RsaParams.DQ;
			ht["INVERSEQ"] = RsaParams.InverseQ;
			ht["D"] =RsaParams.D;

							
			IRU.RTS.CryptoInterfaces.ICryptoOperations ic = 
				(IRU.RTS.CryptoInterfaces.ICryptoOperations )Activator.GetObject(typeof(IRU.RTS.CryptoInterfaces.ICryptoOperations ), "tcp://192.168.0.4:9012/CryptoProvider.rem");
		
		
			ic.Decrypt(new byte[60], "RSA", ht);
			
			*/
			//Debug



			Crypt32Helper.CertCloseStore(hCertStore,0);
		
		}





		/// This function creates and returns an RSACryptoServiceProvider
		/// object that is based on the supplied certificate context pointer.
		public RSACryptoServiceProvider RSACryptoServiceProviderFromCertContext (IntPtr pCertContext)
		{

			string sExceptionMessageBuffer ="";
			// Determine the size of the buffer that you need to allocate.
			uint cbData = 0;
			IntPtr pCryptKeyProvInfo= new IntPtr(0);
			bool fStatus = Crypt32Helper.CertGetCertificateContextProperty(
				pCertContext.ToInt32(), 
				CERT_KEY_PROV_INFO_PROP_ID, 
				pCryptKeyProvInfo, 
				ref cbData);
			if (fStatus)
			{
				sExceptionMessageBuffer+="\r\n CertGetCertificateContextProperty failed: " + Marshal.GetLastWin32Error().ToString();
				// Get the CERT_KEY_PROV_HANDLE_PROP_ID value and the HCRYPTPROV value.
				//cbData = 4;
				pCryptKeyProvInfo = Marshal.AllocHGlobal(new IntPtr(cbData));
				fStatus = Crypt32Helper.CertGetCertificateContextProperty(pCertContext.ToInt32(),CERT_KEY_PROV_INFO_PROP_ID, 
					pCryptKeyProvInfo, 
					ref cbData);
				
				
				
			}
			else
			{
				sExceptionMessageBuffer+="\r\nCertGetCertificateContextProperty failed: " + Marshal.GetLastWin32Error().ToString();
				throw new ApplicationException(sExceptionMessageBuffer,null);
			
			}
			//Mandar New code to get the container from the cert 17-oct-2005

			if (cbData!=0)
			{
				CRYPT_KEY_PROV_INFO ckinfo = 
					(CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(pCryptKeyProvInfo,
					typeof(CRYPT_KEY_PROV_INFO));
				Marshal.FreeHGlobal(pCryptKeyProvInfo);
				
				string keycontainer = ckinfo.ContainerName;
				int RSAkeytype = (int)ckinfo.KeySpec;
				CspParameters cp = new CspParameters();
				cp.KeyContainerName = keycontainer;
				cp.KeyNumber = RSAkeytype;
				cp.Flags=CspProviderFlags.UseMachineKeyStore | CspProviderFlags.UseExistingKey;
			
				RSACryptoServiceProvider oRSA = new  RSACryptoServiceProvider(cp);
			
				return oRSA;	
			}



			//New code ends //

			#region Old code commented was not using right container

			/*
			if (cbData != 0)
			{
				// Allocate an unmanaged buffer to store the CRYPT_KEY_PROV_INFO structure.
				IntPtr pCryptKeyProvInfo = Marshal.AllocHGlobal((int)cbData);
				// Get the CRYPT_KEY_PROV_INFO structure.
				fStatus = Crypt32Helper.CertGetCertificateContextProperty(pCertContext.ToInt32(),CERT_KEY_PROV_INFO_PROP_ID, 
					pCryptKeyProvInfo, 
					ref cbData);
				if (!fStatus)
				{
					sExceptionMessageBuffer+="\r\n CertGetCertificateContextProperty failed: " + Marshal.GetLastWin32Error().ToString();
					Marshal.FreeHGlobal(pCryptKeyProvInfo);
				}
				else
				{ 
					// Build a CspParameters object with the provider type, the provider name,
					// and the container name from the CRYPT_KEY_PROV_INFO structure.
					// The pointer to the container name is the first DWORD in the CRYPT_KEY_PROV_INFO
					// structure. The pointer to the provider name is the second DWORD. 
					// The provider type is the third DWORD.
						CspParameters CspParams = new CspParameters(Marshal.ReadInt32((IntPtr)((int)pCryptKeyProvInfo + 8)), 
							Marshal.PtrToStringUni((IntPtr)Marshal.ReadInt32((IntPtr)((int)pCryptKeyProvInfo + 4))), 
							Marshal.PtrToStringUni((IntPtr)Marshal.ReadInt32(pCryptKeyProvInfo)));
						// 
						// Free the unmanaged CRYPT_KEY_PROV_INFO buffer.
						// 
						Marshal.FreeHGlobal(pCryptKeyProvInfo);
						return new RSACryptoServiceProvider(CspParams);
				
				}
			}*/
			#endregion
			return null;
		}


/// <summary>
/// Used to Extract public key from a subcsriber x509 certificate
/// </summary>
/// <param name="CertPath">Path of the Cert File</param>
/// <param name="RsaParams">Will contain Modulus and Exponent</param>
/// <param name="ThumbPrint">ThumbPrint of the certificate</param>
		[Obsolete("Replaced by WSE 2.0 X509 class in 3",true)]
		public bool ExtractKeysFromSubscriberCertFile(string CertPath,  out RSAParameters RsaParams, out string ThumbPrint, out string Message, out DateTime ExpiryDate)
		{
	

			RsaParams = new RSAParameters();
			ThumbPrint = "";
			Message="";
			ExpiryDate = DateTime.Now;	

			//load the cert in class
			System.Security.Cryptography.X509Certificates.X509Certificate xCert = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(CertPath);
			//Microsoft.Web.Services2.Security.X509.X509Certificate xCert = Microsoft.Web.Services2.Security.X509.X509Certificate.CreateCertFromFile(CertPath);
						
			//try access properties

			ThumbPrint = xCert.GetCertHashString();

			ExpiryDate = DateTime.Parse( xCert.GetExpirationDateString());
	
			
			
			string certCN = xCert.GetName();

			byte[] publickeyblob ;
			byte[] encodedpubkey = xCert.GetPublicKey(); //ASN.1 encoded public 		key

			uint blobbytes=0;
			
			if(Crypt32Helper.CryptDecodeObject(ENCODING_TYPE, RSA_CSP_PUBLICKEYBLOB, 
				encodedpubkey, (uint)encodedpubkey.Length, 0, null, ref 
				blobbytes))
			{
				publickeyblob = new byte[blobbytes];
				if (!Crypt32Helper.CryptDecodeObject(ENCODING_TYPE, RSA_CSP_PUBLICKEYBLOB, 
					encodedpubkey, (uint)encodedpubkey.Length, 0, publickeyblob, 
					ref blobbytes))
				{
					Message="Couldn't decode publickeyblob from certificate publickey" ;
					return false;
				}
				
			}
			else
			{
				Message="Couldn't decode publickeyblob from certificate publickey" ;
				return false;
			}

			PUBKEYBLOBHEADERS pkheaders = new PUBKEYBLOBHEADERS() ;
			int headerslength = Marshal.SizeOf(pkheaders);
			IntPtr buffer = Marshal.AllocHGlobal( headerslength);
			Marshal.Copy( publickeyblob, 0, buffer, headerslength );
			pkheaders = (PUBKEYBLOBHEADERS) Marshal.PtrToStructure( buffer, 
				typeof(PUBKEYBLOBHEADERS) );
			Marshal.FreeHGlobal( buffer );
			
			
				Message+="\r\n ---- PUBLICKEYBLOB headers ------";
				Message+= String.Format("\r\n  btype     {0}", pkheaders.bType);
				Message+=String.Format("\r\n  bversion  {0}", pkheaders.bVersion);
				Message+=String.Format("\r\n  reserved  {0}", pkheaders.reserved);
				Message+=String.Format("\r\n  aiKeyAlg  0x{0:x8}", pkheaders.aiKeyAlg);
				String magicstring = (new 
					ASCIIEncoding()).GetString(BitConverter.GetBytes
					(pkheaders.magic)) ;
				Message+=String.Format("\r\n  magic     0x{0:x8}     '{1}'", 
					pkheaders.magic, magicstring);
				Message+=String.Format("\r\n  bitlen    {0}", pkheaders.bitlen);
				Message+=String.Format("\r\n  pubexp    {0}", pkheaders.pubexp);
				Message+=String.Format("\r\n --------------------------------");
			
			//-----  Get public key size in bits -------------
			

			//-----  Get public exponent -------------
			byte[] exponent = BitConverter.GetBytes(pkheaders.pubexp); //little-endian ordered
				Array.Reverse(exponent);    //convert to big-endian order
			
			
			//-----  Get modulus  -------------
			int modulusbytes = (int)pkheaders.bitlen/8 ;
			byte[] modulus = new byte[modulusbytes];
			try
			{
				Array.Copy(publickeyblob, headerslength, modulus, 0, 
					modulusbytes);
				Array.Reverse(modulus);   //convert from little to big-endian 	ordering.
			}
			catch(Exception)
			{
				Message+="Problem getting modulus from publickeyblob";
				return false;
			}
		
			RsaParams.Modulus=modulus;
			RsaParams.Exponent=exponent;
			return true;


		}


		public bool ExtractKeysFromSubscriberCertFile3(string CertPath,  out RSAParameters RsaParams, out string ThumbPrint, out string Message, out DateTime ExpiryDate)
		{
			X509Certificate2 xCert = new X509Certificate2();
            xCert.Import(CertPath, (string)null, X509KeyStorageFlags.Exportable);
			
			ThumbPrint = xCert.GetCertHashString();

			ExpiryDate = DateTime.Parse( xCert.GetExpirationDateString());
	
			Message =""; //initiate out
			RsaParams = new RSAParameters(); //initiate out param
			
			Message="";
			ExpiryDate = DateTime.Now;	
			//try access properties

			ThumbPrint = xCert.GetCertHashString();

			ExpiryDate = DateTime.Parse( xCert.GetExpirationDateString());
			//start
			RSA rs =  (RSA)xCert.PublicKey.Key;
			 RsaParams =  rs.ExportParameters(false);
			rs = null;
			//end
		
			return true;
			
		
		}


		[Obsolete("Replaced by WSE 2.0 X509 class in 3",true)]
		public bool ExtractKeysFromSubscriberCertFile2(string CertPath,  out RSAParameters RsaParams, out string ThumbPrint, out string Message, out DateTime ExpiryDate)
		{
				//Microsoft.Web.Services2.Security.X509.X509Certificate x509 = Microsoft.Web.Services2.Security.X509.X509Certificate.CreateFromCertFile(CertPath);
				System.Security.Cryptography.X509Certificates.X509Certificate x509 = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile (CertPath);
			
				ThumbPrint = x509.GetCertHashString();

				ExpiryDate = DateTime.Parse( x509.GetExpirationDateString());
	
				Message =""; //initiate out
				
				
				RsaParams = new RSAParameters(); //initiate out param


				uint hProv = 0;
				IntPtr pPublicKeyBlob = IntPtr.Zero;
				// Get a pointer to a CERT_CONTEXT structure from the raw certificate data.
				IntPtr pCertContext = IntPtr.Zero;
				pCertContext = (IntPtr)Crypt32Helper.CertCreateCertificateContext(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING,
					x509.GetRawCertData(),x509.GetRawCertData().Length);
				if (pCertContext == IntPtr.Zero)
				{
					Message+= "CertCreateCertificateContext failed: " + Marshal.GetLastWin32Error().ToString();
					goto Cleanup;
				}
				if (!Crypt32Helper.CryptAcquireContext(ref hProv, null, null, PROV_RSA_FULL, 0)) 
				{ 
					if (!Crypt32Helper.CryptAcquireContext(ref hProv, null, null, PROV_RSA_FULL, CRYPT_NEWKEYSET)) 
					{ 
						Message+= "CryptAcquireContext failed: " + Marshal.GetLastWin32Error().ToString();
						goto Cleanup;
					}
				}
         
				// Get a pointer to the CERT_INFO structure.
				// This pointer is the fourth DWORD of the CERT_CONTEXT structure.
         
				IntPtr pCertInfo = (IntPtr)Marshal.ReadInt32(pCertContext, 12);
         
				// Get a pointer to the CERT_PUBLIC_KEY_INFO structure.
				// This structure is located starting at the fifty-seventh byte
				// of the CERT_INFO structure.
         
				IntPtr pSubjectPublicKeyInfo = (IntPtr)(pCertInfo.ToInt32() + 56);
         
				// Import the public key information from the certificate context
				// into a key container by passing the pointer to the SubjectPublicKeyInfo
				// member of the CERT_INFO structure to the CryptImportPublicKeyInfoEx
				// Win32 API function.
         
				uint hKey = 0;
				if (!Crypt32Helper.CryptImportPublicKeyInfo(hProv,X509_ASN_ENCODING | PKCS_7_ASN_ENCODING,
					pSubjectPublicKeyInfo,ref hKey)) 
				{
					Message+= "CryptImportPublicKeyInfoEx failed: " + Marshal.GetLastWin32Error().ToString();
					goto Cleanup;
				}

				// Get the size of the buffer that is needed to contain the PUBLICKEYBLOB structure, and then
				// call the CryptExportKey Win32 API function to export the public key to the PUBLICKEYBLOB format.

				uint dwDataLen = 0;
				if (!Crypt32Helper.CryptExportKey(hKey, 0, PUBLICKEYBLOB, 0, 0, ref dwDataLen))
				{
					Message+="CryptExportKey failed: " + Marshal.GetLastWin32Error().ToString();
					goto Cleanup;
				}
         
				// Export the public key to the PUBLICKEYBLOB format.
         
				pPublicKeyBlob = Marshal.AllocHGlobal((int)dwDataLen);
				if (!Crypt32Helper.CryptExportKey(hKey, 0, PUBLICKEYBLOB, 0, (uint)pPublicKeyBlob.ToInt32(), ref dwDataLen))
				{
					Message+="CryptExportKey failed: " + Marshal.GetLastWin32Error().ToString();
					goto Cleanup;
				}
         
				// Get the public exponent.
				// The public exponent is located in bytes 17 through 20 of the 
				// earlier PUBLICKEYBLOB structure.
         
				byte[] Exponent = new byte[4];
				Marshal.Copy((IntPtr)(pPublicKeyBlob.ToInt32() + 16), Exponent, 0, 4);
				Array.Reverse(Exponent); // Reverse the byte order.

				// Reverse the byte order.
				// Get the length of the modulus.
				// To do this, extract the bit length of the modulus from the PUBLICKEYBLOB structure.
				// The bit length of the modulus is located in bytes 13 through 17 of the PUBLICKEYBLOB structure.
         
				int BitLength = Marshal.ReadInt32(pPublicKeyBlob, 12);

				// Get the modulus.
				// The modulus starts at the twenty-first byte of the PUBLICKEYBLOB structure,
				// and is BitLength/8 bytes in length.
         
				byte[] Modulus = new byte[BitLength / 8];
				Marshal.Copy((IntPtr)(pPublicKeyBlob.ToInt32() + 20), Modulus, 0, BitLength / 8);
				Array.Reverse(Modulus);
         
				// Reverse the byte order.
				// Put the modulus and the exponent into an RSAParameters object.
         
				
				RsaParams.Exponent = Exponent;
				RsaParams.Modulus = Modulus;
         
				// Import the modulus and the exponent into an RSACryptoServiceProvider object
				// by using the RSAParameters object.
         
				Cleanup:
					if (pCertContext != IntPtr.Zero)
						Crypt32Helper.CertFreeCertificateContext(pCertContext.ToInt32());
				if (hProv != 0)
					Crypt32Helper.CryptReleaseContext(hProv, 0);
				if (pPublicKeyBlob != IntPtr.Zero)
					Marshal.FreeHGlobal(pPublicKeyBlob);
				return true;
			
		
		}

	}

	public class Crypt32Helper
	{
		[DllImport("crypt32.dll", CharSet=CharSet.Auto)]
		public static extern bool CryptDecodeObject(
			uint CertEncodingType,
			uint lpszStructType,
			byte[] pbEncoded,
			uint cbEncoded,
			uint flags,
			[In, Out] byte[] pvStructInfo,
			ref uint cbStructInfo);

		[DllImport("Crypt32.dll", CharSet=CharSet.Auto)]
		internal extern static uint CertOpenSystemStore(int hprov, string szSubsystemProtocol);


		[DllImport("Crypt32.dll", CharSet=CharSet.Ansi, SetLastError=true)]
		public extern static uint CertOpenStore
			(uint lpszStoreProvider,
			uint dwMsgAndCertEncoding,
			uint hCryptProv,
			uint dwFlags,
			string storeName);



		[DllImport("crypt32.dll", SetLastError=true)]
		public static extern bool CertCloseStore(
			IntPtr hCertStore,
			uint dwFlags) ;


		[DllImport("Crypt32.dll", CharSet=CharSet.Auto)]
		internal extern static uint CertEnumCertificatesInStore(uint hCertStore, uint pPrevCertContext);
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto)]
		internal extern static uint CertDuplicateCertificateContext(uint pPrevCertContext);
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CertGetCertificateContextProperty(int pCertContext,int dwPropId,
			IntPtr pvData, ref uint pcbData);
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto)]
		internal extern static uint CertCreateCertificateContext(uint dwCertEncodingType,
			[MarshalAs(UnmanagedType.LPArray)]byte[] pbCertEncoded, int cbCertEncoded);
		[DllImport("Advapi32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CryptAcquireContext(ref uint phProv,string pszContainer,
			string pszProvider,uint dwProvType,uint dwFlags);
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CryptImportPublicKeyInfoEx(uint hCryptProv ,uint dwCertEncodingType, 
			IntPtr pInfo, uint aiKeyAlg, uint dwFlags ,uint pvAuxInfo, ref uint phKey); 
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CryptImportPublicKeyInfo(uint hCryptProv ,uint dwCertEncodingType, 
			IntPtr pInfo, ref uint phKey); 
		[DllImport("Advapi32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CryptExportKey(uint hKey,uint hExpKey, uint dwBlobType, 
			uint dwFlags ,uint pbData, ref uint pdwDataLen);
		[DllImport("Crypt32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CertFreeCertificateContext(int pCertContext);
		[DllImport("Advapi32.dll", CharSet=CharSet.Auto,SetLastError=true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal extern static bool CryptReleaseContext(uint hProv, uint dwFlags);
	}

	
	[StructLayout(LayoutKind.Sequential)]
	public struct PUBKEYBLOBHEADERS 
	{
		public byte bType;	//BLOBHEADER
		public byte bVersion;	//BLOBHEADER
		public short reserved;	//BLOBHEADER
		public uint aiKeyAlg;	//BLOBHEADER
		public uint magic;	//RSAPUBKEY
		public uint bitlen;	//RSAPUBKEY
		public uint pubexp;	//RSAPUBKEY
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CRYPT_KEY_PROV_INFO
	{
		[MarshalAs(UnmanagedType.LPWStr)] public String ContainerName;
		[MarshalAs(UnmanagedType.LPWStr)] public String ProvName;
		public uint ProvType;
		public uint Flags;
		public uint ProvParam;
		public IntPtr rgProvParam;
		public uint KeySpec;
	}

}
