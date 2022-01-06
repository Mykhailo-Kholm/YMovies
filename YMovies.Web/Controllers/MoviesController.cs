using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.Services.Service;
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

        public async Task<ActionResult> Like(int id)
        {
            return RedirectToAction("Details", id);

        }

        public async Task<ActionResult> DisLike(int id)
        {
            return RedirectToAction("Details", id);

        }

        public async Task<ActionResult> MostLiked()
        {
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> MostWatched()
        {
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> TopByIMDb()
        {
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Search()
        {
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Index(int? page, string action)
        {
            
            var pageSize = 10;
            var pageNumber = page ?? 1;
            List<MoviesInfo> moviesInfos = new List<MoviesInfo>();
            if (Request.UrlReferrer != null)
            {
                string prev = Request.UrlReferrer.ToString();
            }

            foreach (var movie in movies)
            {
                moviesInfos.Add
                (
new MoviesInfo(){Id = movie.MovieId, ImdbRating = movie.ImdbRating, Genres = movie.Genres}
                );
            }
            var countryMovieViewModel = new CountryMovieViewModel()
            {
                MoviePageList = moviesInfos.ToPagedList(pageNumber, pageSize),
                Countries = countries,
                MoviesInfo = moviesInfos
            };
            if (Session["Movies"] != null)
            {
                countryMovieViewModel.MoviesInfo = Session["Movies"] as List<MoviesInfo>;
            }
            else
            {
                countryMovieViewModel.MoviesInfo = moviesInfos;
            }
            //Session["Countries"] = countries;
            return View(countryMovieViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            Movie movie = movies.FirstOrDefault(m => m.MovieId == id);
            return View(movie);
        }

        public ActionResult Partial()
        {
            return PartialView(countries);
        }

        public async Task<ActionResult> FilterInclude(string action, int countryId)
        {

            List<Movie> newMovies = new List<Movie>();
            if (Session["Movies"] != null)
            {
                newMovies  = Session["Movies"] as List<Movie>;
            }

            //newMovies = movies.Movies.Where(p => countries.Any(p2 => countryId == p.Id)).ToList();
            //foreach (var m in movies)
            //{
            //    foreach (var c in m.Countries)
            //    {   
            //        if(c.Id==countryId)
            //            newMovies.Add(m);
            //    }
            //}

            Session["Movies"] = newMovies;
            //List<Movie> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> FilterExclude(int countryId)
        {

            List<Movie> newMovies = new List<Movie>();
            if (Session["Movies"] != null)
            {
                newMovies = Session["Movies"] as List<Movie>;
            }

            //newMovies = movies.Movies.Where(p => countries.Any(p2 => countryId == p.Id)).ToList();
            //foreach (var m in movies)
            //{
            //    foreach (var c in m.Countries)
            //    {
            //        if (c.Id != countryId)
            //        {
            //            newMovies.Remove(m);
            //        }
            //        break;
            //    }
            //}

            Session["Movies"] = newMovies;
            //List<Movie> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }
    }
}