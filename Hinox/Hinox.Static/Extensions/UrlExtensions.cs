using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hinox.Static.Extensions
{
    public static class UrlExtensions
    {
        public static bool IsAbsoluteUrl(this string url)
        {
            return url.StartsWith("http://") || url.StartsWith("https://");
        }

        public static string GetRelativePath(this string url)
        {
            if (!url.IsAbsoluteUrl())
            {
                return url;
            }
            return new Regex("/^(http://|https://)[^/] +/g").Replace(url, string.Empty);
        }
    }
}
