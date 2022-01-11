using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.WebPages;

namespace YMovies.Web.Services.Service
{
    public class TypesConvertor
    {
        public decimal ConvertTDecimal(string input)
        {
            string pattern = @"\d";
            StringBuilder sb = new StringBuilder();
            foreach (Match m in Regex.Matches(input, pattern))
            {
                sb.Append(m);
            }

            var number = sb.ToString();
            if (number.IsEmpty())
                return 0;
            return Convert.ToDecimal(number);
        }
    }
}