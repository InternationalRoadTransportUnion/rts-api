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
            string nsPattern = @"<([^:]+:){0,1}Body[^>]*>(.*)<[^/]*/.*Body\s*>";

            Regex r = new Regex(nsPattern,RegexOptions.IgnoreCase| RegexOptions.Singleline);

            

            

            //.*xmlns:*(?<prefix>.*)="http://www\.iru\.org/SafeTIRUpload"

            Match msc = r.Match(Message); 

            //while (msc.Success)
            //{
            string bodyContents;

            if (msc!=null && msc.Success)
            {
                Group gg = msc.Groups[2];
            
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

            string nsPattern = "Hash[^>]*>\\s*(?<hash>[\\S]*)\\s*</.*Hash>";

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



        public static string SetHASH(string Message, string EnvelopeNSPrefix, string HashNSPrefix, string hashValue)
        {
            string replacedString = "";

            EnvelopeNSPrefix = (EnvelopeNSPrefix ?? String.Empty).Trim();
            if (EnvelopeNSPrefix.Length > 0 && !EnvelopeNSPrefix.EndsWith(":"))
            {
                EnvelopeNSPrefix += ":";
            }
            HashNSPrefix = (HashNSPrefix ?? String.Empty).Trim();
            if (HashNSPrefix.Length > 0 && !HashNSPrefix.EndsWith(":"))
            {
                HashNSPrefix += ":";
            }

            string pattern = String.Format(@"(<\s*{0}Envelope\s*>\s*<\s*{1}Hash\s*>\s*)([^\s]*)(\s*<\s*/{1}Hash\s*>\s*<\s*/{0}Envelope\s*>)", EnvelopeNSPrefix, HashNSPrefix);
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            string replaceHash = "${1}" + hashValue + "${3}";
            replacedString = regex.Replace(Message, replaceHash);
            System.Diagnostics.Debug.Write(replacedString);

            return replacedString;
        }

        public static string SetHASH(string Message, string NSPrefix, string hashValue)
        {
            return SetHASH(Message, NSPrefix, NSPrefix, hashValue);
        }

    }
}
