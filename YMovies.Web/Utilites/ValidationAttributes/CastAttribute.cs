using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Utilites.ValidationAttributes
{
    public class CastAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var casts = (List<CastViewModel>)value;
                var temp = casts.Where(cast => !cast.Id.Equals(0)).ToList();
                return temp.Count == 0 ? false : true;
            }
            return false;
        }
    }
}