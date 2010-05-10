using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for RegExHelper.
	/// </summary>
	public class RegExHelper
	{
	

    
		public static string ExtractBODYContents(string Message)
		{
			string nsPattern = "Body>(?<body>.*)</.*Body";

			Regex r = new Regex(nsPattern,RegexOptions.IgnoreCase| RegexOptions.Singleline | RegexOptions.ExplicitCapture );

			

			

			//.*xmlns:*(?<prefix>.*)="http://www\.iru\.org/SafeTIRUpload"

			Match msc = r.Match(Message); 

			//while (msc.Success)
			//{
			string bodyContents;

			if (msc!=null)
			{
				Group gg = msc.Groups[1];
			
				bodyContents= gg.Value;
				Debug.WriteLine(bodyContents);
				return bodyContents;
			}
			else
			{
				return "";
			
			}


		
		
		}
    
    
	
		public static string ExtractNameSpacePrefix(string Message, string NSURN)
		{
			string nsPrefix="";

			string nsPattern = ".*xmlns:*(?<prefix>\\S*)=\"" + NSURN +"\"";//".*xmlns:*(?<prefix>.*)=\"http://www\\.iru\\.org/SafeTIRUpload\"";

			Regex r = new Regex(nsPattern,RegexOptions.IgnoreCase| RegexOptions.Singleline | RegexOptions.ExplicitCapture );

			Match msc ;


			msc = r.Match(Message); 

			
			if (msc!=null)
			{
				Group gg = msc.Groups[1];
			
				nsPrefix= gg.Value;
				
			}
			else
			{
				
			
			}
			return nsPrefix;


		
		}
    
    
	
	
		public static string ExtractHASH(string Message)
		{
			string hashValue="";

			string nsPattern = "Hash>\\s*(?<hash>[\\S]*)\\s*</.*Hash>";

			Regex r = new Regex(nsPattern,RegexOptions.IgnoreCase| RegexOptions.Singleline | RegexOptions.ExplicitCapture );

			Match msc ;


			msc = r.Match(Message); 

			
			if (msc!=null)
			{
				Group gg = msc.Groups[1];
			
				hashValue= gg.Value;
				
			}
			else
			{
				
			
			}
			return hashValue;


		
		}
    
	
	
			
	
		public static string SetHASH(string Message, string NSPrefix, string hashValue)
		{
			string replacedString="";

			if (NSPrefix=="")
			{
				Regex regReplace= new Regex("<\\s*Envelope\\s*>.*<\\s*/Envelope\\s*>",RegexOptions.IgnoreCase|  RegexOptions.Singleline);
				string replaceHash= "<Envelope><Hash>" + hashValue +"</Hash></Envelope>";
				replacedString= regReplace.Replace(Message,replaceHash,1);
				System.Diagnostics.Debug.Write(replacedString);
			
			}


			if (NSPrefix!="")
			{
				Regex regReplace= new Regex("<\\s*"+NSPrefix + ":Envelope\\s*>.*<\\s*/"+ NSPrefix +":Envelope\\s*>",RegexOptions.IgnoreCase|  RegexOptions.Singleline);
				string replaceHash= "<"+NSPrefix +":Envelope><" + NSPrefix + ":Hash>" + hashValue +"</" +NSPrefix+":Hash></"+NSPrefix +":Envelope>";
				replacedString= regReplace.Replace(Message,replaceHash,1);
				System.Diagnostics.Debug.Write(replacedString);
			
			}

			return replacedString;
		}

	}
}
