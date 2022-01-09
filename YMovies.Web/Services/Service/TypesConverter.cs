using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace YMovies.Web.Services.Service
{
    public class TypesConverter
    {
        public decimal StringToDecimal(string input)
        {
            // Split on one or more non-digit characters.
            string pattern = @"\d";

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(input, pattern))
            {
                sb.Append(m);
            }
            return Convert.ToDecimal(sb.ToString());
        }
    }
}