using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string input)
        {
            int output = 0;
            var result = int.TryParse(input, out output);
            return result;
        }
        public static bool IsMinSize(this string input, int minSize)
        {
            return input != null && input.Length >= minSize;
        }
    }
}
