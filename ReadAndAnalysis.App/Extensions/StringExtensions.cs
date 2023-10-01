using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Extensions
{
    public static class StringExtensions
    {
        public static string ShowInTable(this string text)
        {
            if (text.Length > 50)
            {
                var result = text.Substring(0, 50);
                return result + "...";
            }
            return text;

        }
        public static string ShowTitle(this string text)
        {
            if (text.Length > 20)
            {
                var result = text.Substring(0, 20);
                return result + "...";
            }
            return text;
        }
        public static string GetTagName(this string text)
        {
            var str = text.Replace("<", "");
            return str.Replace(">", "");
        }
    }
}
