using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.TempModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int UsersRating { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<Country> Countries { get; set; }
        //public Movie()
        //{
        //    Countries = new List<Country>();
        //}
        public int Year { get; set; }
        public List<string> GenresList { get; set; }
        public string Cast { get; set; }
        public decimal Budget { get; set; }
    }
}