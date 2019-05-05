using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Hinox.Static.Enumerate;

namespace Hinox.Static.Extensions
{
    public static class StringExtensions
    {
        static Regex RegexForUnsignText = null;

        public static string ToUnsignText(this string text)
        {
            if (ReferenceEquals(RegexForUnsignText, null))
            {
                RegexForUnsignText = new Regex("p{IsCombiningDiacriticalMarks}+");
            }
            var temp = text.Normalize(NormalizationForm.FormD);
            return RegexForUnsignText.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D");
        }
        public static string RemoveSpecialCharacters(this string text)
        {
            var result = Regex.Replace(text, "[^a-zA-Z0-9 ]", string.Empty);
            return result;
        }

        public static List<string> ToListSubstring(this string input, string separator)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<string>();
            var elements = input.Split(separator);
            return elements.ToList();
        }
        public static List<int> ToListInteger(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<int>();
            var elements = input.Split(",");
            return elements.Select(s => int.Parse(s)).ToList();
        }
        public static List<int> ToListEnumerationValue<T>(this string input, string title = null) where T : Enumeration
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<int>();
            var inputElements = input.Split(",");
            var availableNames = Enumeration.GetAll<T>().Select(s => s.Name).ToList();

            var invalidStatuses = inputElements.Where(w => !availableNames.Contains(w));
            if (invalidStatuses.Count() > 0)
                throw new Exception(string.Format("{0} must be in ({1})", title ?? "value", string.Join(',', availableNames)));
            return inputElements.Select(s => Enumeration.FromDisplayName<T>(s).Id).ToList();
        }
        
        public static string NormalizeD(this string input)
        {
            return input.Normalize(NormalizationForm.FormD);
        }

        public static bool EqualTo(this string text1, string text2)
        {
            if (text1 == null && text2 == null)
                return true;
            if (text1 == null && text2 != null)
                return false;
            if (text1 != null && text2 == null)
                return false;
            return text1.Equals(text2);
        }
        public static bool TrimEquals(string text1, string text2)
        {
            if (!string.IsNullOrWhiteSpace(text1))
                text1 = text1.Trim();
            if (!string.IsNullOrWhiteSpace(text2))
                text2 = text2.Trim();

            if (text1 == null && text2 == null)
                return true;
            if (text1 == null && text2 != null)
                return false;
            if (text1 != null && text2 == null)
                return false;
            return text1.Equals(text2);
        }
    }
}
