using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.WebPages;

namespace YMovies.Web.Utilites
{
    public static class TypeConverter
    {
        public static string ToJson(object items) =>
            JsonConvert.SerializeObject(
                items,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

        public static decimal ToDecimal(string input)
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