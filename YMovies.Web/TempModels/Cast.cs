using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.TempModels
{
    public class Cast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}