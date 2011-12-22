using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSDotNETClient
{
    public class RTSWebServiceException : Exception
    {
        private int returnCode;
        public int ReturnCode { get { return this.returnCode; } }
        public RTSWebServiceException(string message, int returnCode)
            : base(message)
        {
            this.returnCode = returnCode;
        }
    }

    public class XsdValidationException : Exception
    {
        public XsdValidationException(string message, Exception ex) : base(message, ex)
        { }
    }
}
