using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSDotNETClient
{
    /// <summary>
    /// Adds the ToWSString() method to the .NET DateTime datatype in order to transform
    /// a DateTime to a string including the UTC/Local mark.
    /// </summary>
    public static class DateTimeExtension
    {
        private const string DATE_FORMAT = "yyyy-MM-ddTHH:mm:sszzz";
        private const string DATE_FORMAT_UTC = "yyyy-MM-ddTHH:mm:ss'Z'";
        private const string DATE_FORMAT_UNSPECIFIED = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// Adds the ToWSString() method to the .NET DateTime datatype in order to transform
        /// a DateTime to a string including the UTC/Local mark.
        /// </summary>
        public static string ToWSString(this DateTime value)
        {
            switch (value.Kind)
            {
                case DateTimeKind.Local:
                    return value.ToString(DATE_FORMAT);
                case DateTimeKind.Utc:
                    return value.ToString(DATE_FORMAT_UTC);
                default:
                    return value.ToString(DATE_FORMAT_UNSPECIFIED);
            }
        }
    }
}
