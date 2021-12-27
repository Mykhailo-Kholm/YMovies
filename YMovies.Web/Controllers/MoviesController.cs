using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using YMovies.Web.TempModels;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class MoviesController : Controller
    {
        public static List<Country> countries = new List<Country>()
        {
            new Country()
            {
                Id = 1,
                Name = "US"
            },
            new Country()
            {
                Id = 2,
                Name = "Germany"
            },
            new Country()
            {
                Id = 3,
                Name = "Ukraine"
            }
        };

        public static List<Movie> movies = new List<Movie>()
        {
            new Movie()
            {
                Id = 1,
                Name = "Movie one",
                Rating = 3,
                Year = 2021,
                UsersRating = 1,
                Description = "descriptionOne",
                GenresList = new List<string>()
                {
                    "FirstGenre",
                    "SecondGenre"
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],

                },

                Cast = "Actor, Director",
                Budget = 100
            },
            new Movie()
            {
                Id = 1,
                Name = "Movie one",
                Rating = 3,
                Year = 2021,
                UsersRating = 1,
                Description = "descriptionOne",
                GenresList = new List<string>()
                {
                    "FirstGenre",
                    "SecondGenre"
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },

                Cast = "Actor, Director",
                Budget = 100
            },
            new Movie()
            {
                Id = 3,
                Name = "Movie one",
                Rating = 3,
                Year = 2021,
                UsersRating = 1,
                Description = "descriptionOne",
                GenresList = new List<string>()
                {
                    "FirstGenre",
                    "SecondGenre"
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1],
                    countries[2]

                },
                Cast = "Actor, Director",
                Budget = 100
            },
            new Movie()
            {
                Id = 2,
                Name = "Movie two",
                Rating = 2,
                Year = 1998,
                UsersRating = 12,
                Description = "desc2",
                GenresList = new List<string>()
                {
                    "ThirdGenre",
                    "SecondGenre"
                },
                Type = "Serial",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = "Actors",
                Budget = 120
            },
            new Movie()
            {
                Id = 1,
                Name = "Movie one",
                Rating = 3,
                Year = 2021,
                UsersRating = 1,
                Description = "descriptionOne",
                GenresList = new List<string>()
                {
                    "FirstGenre",
                    "SecondGenre"
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = "Actor, Director",
                Budget = 100
            },
            new Movie()
            {
                Id = 1,
                Name = "Movie one",
                Rating = 3,
                Year = 2021,
                UsersRating = 1,
                Description = "descriptionOne",
                GenresList = new List<string>()
                {
                    "FirstGenre",
                    "SecondGenre"
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = "Actor, Director",
                Budget = 100
            },
            new Movie()
            {
                Id = 2,
                Name = "Movie two",
                Rating = 2,
                Year = 1998,
                UsersRating = 12,
                Description = "desc2",
                GenresList = new List<string>()
                {
                    "ThirdGenre",
                    "SecondGenre"
                },
                Type = "Serial",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = "Actors",
                Budget = 120
            }
        };

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            var pageNumber = page ?? 1;
            var countryMovieViewModel = new CountryMovieViewModel()
            {
                MoviePageList = movies.ToPagedList(pageNumber, pageSize),
                Movies = movies
            };
            return View(countryMovieViewModel);
        }

        public ActionResult Details(int id)
        {
            Movie movie = movies.FirstOrDefault(m => m.Id == id);
            return View(movie);
        }
    }
}