using System;

namespace Insight.Application.Common.Helpers
{
    public class PostcodeFormatter
    {
        public static string FormatPostcode(string postcode)
        {
            return Uri.EscapeUriString(postcode.Trim().ToUpper());
        }
    }
}
