﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IRU.RTS.Common.Extension
{
	public static class HttpRequestExtension
	{
		public static string GetCallerIP(this HttpRequest httpRequest)
		{
			string sAddress = httpRequest.UserHostAddress;

			string sAddressFromProxy = (HttpContext.Current.Request.Headers["X-Forwarded-For"] ?? String.Empty).Split(new char[] { ',' }).FirstOrDefault();
			if (String.IsNullOrEmpty(sAddressFromProxy))
				return sAddress;
			else
				return sAddressFromProxy;
		}

		public static string GetCallerProtocol(this HttpRequest httpRequest)
		{
			string sProtocol = httpRequest.Url.Scheme;

			string sProtocolFromProxy = (HttpContext.Current.Request.Headers["X-Forwarded-Proto"] ?? String.Empty).Split(new char[] { ',' }).FirstOrDefault();

			if (String.IsNullOrEmpty(sProtocolFromProxy))
				return sProtocol;
			else
				return sProtocolFromProxy;
		}
	}
}
