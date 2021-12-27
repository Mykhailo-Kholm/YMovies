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

        public static List<Genre> Genres = new List<Genre>()
        {
            new Genre()
            {
                Id = 1,
                Name = "Genre1"
            },
            new Genre()
            {
                Id = 2,
                Name = "Genre2"
            },
            new Genre()
            {
                Id = 3,
                Name = "Genre3"
            }
        };

        public static List<Cast> Casts = new List<Cast>()
        {
            new Cast()
            {
                Id = 1,
                Name = "First",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 2,
                Name = "Second",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 3,
                Name = "Third",
                Surname = "Actor"
            }
        };

        public static List<Movie> movies = new List<Movie>()
        {
            new Movie()
            {
                MovieId = 1,
                Title = "Movie one",
                ImdbRating = 3,
                Year = "2021",
                UsersRating = 1,
                Plot = "descriptionOne",
                Genres = new List<Genre>()
                {
                    Genres[0],
                    Genres[2],
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],

                },

                Cast = new List<Cast>()
                {
                    Casts[0]
                },
                Budget = 100
            },
            new Movie()
            {
                MovieId = 2,
                Title = "Movie one",
                ImdbRating = 3,
                Year = "2021",
                UsersRating = 1,
                Plot = "descriptionOne",
                Genres = new List<Genre>()
                {
                    Genres[1],

                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },

                Cast = new List<Cast>()
                {
                    Casts[1]
                },
                Budget = 100
            },
            new Movie()
            {
                MovieId = 3,
                Title = "Movie one",
                ImdbRating = 3,
                Year = "2021",
                UsersRating = 1,
                Plot = "descriptionOne",
                Genres = new List<Genre>()
                {
                    Genres[0],
                    Genres[1],
                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1],
                    countries[2]

                },
                Cast = new List<Cast>()
                {
                    Casts[1],
                    Casts[2]
                },
                Budget = 100
            },
            new Movie()
            {
                MovieId = 4,
                Title = "Movie two",
                ImdbRating = 2,
                Year = "1998",
                UsersRating = 12,
                Plot = "desc2",
                Genres = new List<Genre>()
                {
                    Genres[0],

                },
                Type = "Serial",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = new List<Cast>()
                {
                    Casts[0],
                    Casts[2]
                },
                Budget = 120
            },
            new Movie()
            {
                MovieId = 5,
                Title = "Movie one",
                ImdbRating = 3,
                Year = "2021",
                UsersRating = 1,
                Plot = "descriptionOne",
                Genres = new List<Genre>()
                {
                    Genres[0],

                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = new List<Cast>()
                {
                    Casts[1],
                    Casts[2]
                },
                Budget = 100
            },
            new Movie()
            {
                MovieId = 6,
                Title = "Movie one",
                ImdbRating = 3,
                Year = "2021",
                UsersRating = 1,
                Plot = "descriptionOne",
                Genres = new List<Genre>()
                {
                    Genres[0],

                },
                Type = "Movie",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = new List<Cast>()
                {
                    Casts[1],
                    Casts[2]
                },
                Budget = 100
            },
            new Movie()
            {
                MovieId = 7,
                Title = "Movie two",
                ImdbRating = 2,
                Year = "1998",
                UsersRating = 12,
                Plot = "desc2",
                Genres = new List<Genre>()
                {
                    Genres[0],

                },
                Type = "Serial",
                Countries = new List<Country>()
                {
                    countries[0],
                    countries[1]

                },
                Cast = new List<Cast>()
                {
                    Casts[1],
                    Casts[2]
                },
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
            Movie movie = movies.FirstOrDefault(m => m.MovieId == id);
            return View(movie);
        }
    }
}