using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hinox.Static.Extensions
{
    public static class FilePathExtensions
    {
        public static string GetFileExtension(this string fileName)
        {
            try
            {
                Regex reg = new Regex(@"\.[0-9a-z]+$");
                Match match = reg.Match(fileName);
                if (match.Success)
                {
                    return match.Groups[0].Value;
                }
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
