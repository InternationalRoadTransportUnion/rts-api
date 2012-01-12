using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSDotNETClient
{
    /// <summary>
    /// The exception thrown when a RTS web service returns an error
    /// </summary>
    public class RTSWebServiceException : Exception
    {
        private int returnCode;

        /// <summary>
        /// The code returned by the web service
        /// </summary>
        public int ReturnCode { get { return this.returnCode; } }
        
        /// <summary>
        /// The constructor 
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="returnCode">The return code</param>
        public RTSWebServiceException(string message, int returnCode)
            : base(message)
        {
            this.returnCode = returnCode;
        }
    }

    /// <summary>
    /// The exception thrown when a xsd validation fails
    /// </summary>
    public class XsdValidationException : Exception
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="ex">The inner exception</param>
        public XsdValidationException(string message, Exception ex) : base(message, ex)
        { }
    }
}
