using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExtensions
{
    public static class StringExtension
    {
        public static string ToFeet(this String str)
        {
            int meters = 0;

            bool success = int.TryParse(str, out meters);

            if (success)
            {
                string feet = Math.Round(meters * 3.28084).ToString();

                return feet;
            }

            return "ERROR";
        }

        public static string ToInHg(this String str)
        {
            decimal inches = 0;

            bool success = decimal.TryParse(str, out inches);

            if (success)
            {
                string inhg = Math.Round(inches * (decimal)0.029529988, 2).ToString();

                return inhg;
            }

            return "ERROR";
        }
    }
}
