using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Extensions
{
    public static class DigitsExtensions
    {
        public static string SeparateDigits(this long  digit)
        {
            return digit.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = ","
            });
        }
        public static string DigitSeparation(long  digit)
        {
            return digit.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = ","
            });
        }
    }
}
