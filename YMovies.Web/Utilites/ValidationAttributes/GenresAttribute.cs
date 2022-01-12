using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YMovies.MovieDbService.DTOs;

namespace YMovies.Web.Utilites.ValidationAttributes
{
    public class GenresAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var items = (List<GenreDto>)value;
                var temp = items.Where(i => !i.Id.Equals(0)).ToList();
                return temp.Count == 0 ? false : true;
            }
            return false;
        }
    }
}