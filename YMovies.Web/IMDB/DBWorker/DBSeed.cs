using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.Service;
using IMDbApiLib.Models;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;

namespace YMovies.Web.IMDB.DBWorker
{
    public class DBSeed : ISeed
    {
        private readonly MoviesContext _context;
        private readonly IRepository<Media> _movieRepository;
        private readonly APIworkerIMDB aPIworkerIMDB;
        public DBSeed()
        {
            _context = new MoviesContext();
            _movieRepository = new MovieRepository(_context);
            aPIworkerIMDB = new APIworkerIMDB();
        }
        public async Task AddMovieByImbdId(string imdbId)
        {
            if(_context.Medias.Any(m => m.ImdbId == imdbId))
            {
                return;
            }

            var media = await aPIworkerIMDB.MovieOrSeriesInfo(imdbId);

            if(media.Type == null || media.TvEpisodeInfo != null)
            {
                return;
            }

            _movieRepository.AddItem(MapMovieToDtoFromImdb(media));
            
        }

        public async Task AddMediaByExpression(string expression)
        {
            var movies = await aPIworkerIMDB.SearchMovieAsync(expression);
            var series = await aPIworkerIMDB.SearchSeriesAsync(expression);

            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    await AddMovieByImbdId(movie.Id);
                }
            }

            if (series != null)
            {
                foreach (var serie in series)
                {
                    await AddMovieByImbdId(serie.Id);
                }
            }
        }
        private Media MapMovieToDtoFromImdb(TitleData imdbModel)
        {
            var movie = new Media
            {
                ImdbId = imdbModel.Id,
                Title = imdbModel.Title,
                PosterUrl = imdbModel.Image,
                Year = imdbModel.Year,
                Plot = imdbModel.Plot,
                Companies = imdbModel.Companies,
                WeekFees = imdbModel.BoxOffice.OpeningWeekendUSA,
                GlobalFees = imdbModel.BoxOffice.CumulativeWorldwideGross,
                Type = new MovieDbService.Models.Type(),
                Cast = new List<Cast>(),
                Genres = new List<Genre>(),
                Countries = new List<Country>(),
                UsersLiked = new List<User>(),
                UsersWatched = new List<User>(),
                Seasons = new List<Season>()
            };

            movie.Type.Name = imdbModel.Type;
            if (imdbModel.TvSeriesInfo != null)
            {
                foreach (var imdbSeason in imdbModel.TvSeriesInfo.Seasons)
                {
                    movie.Seasons.Add(new Season(){Name = imdbSeason});
                }
            };

            movie.Budget = GetDecimal(imdbModel.BoxOffice.Budget);

            movie.ImdbRating = GetDecimal(imdbModel.IMDbRating);

            if (imdbModel.ActorList != null)
            {
                foreach (var actor in imdbModel.ActorList)
                {
                    movie.Cast.Add(new Cast
                    {
                        Name = actor.Name,
                        PictureUrl = actor.Image
                    });
                }
            }

            if (imdbModel.CountryList != null)
            {
                foreach (var country in imdbModel.CountryList)
                {
                    movie.Countries.Add(new Country
                    {
                        Name = country.Value
                    });
                }
            }

            if (imdbModel.GenreList != null)
            {
                foreach (var genre in imdbModel.GenreList)
                {
                    movie.Genres.Add(new Genre
                    {
                        Name = genre.Value
                    });
                }
            }

            return movie;
        }

        //public MediaDto MapMovieTWebDtotoDtoFromImdb(TitleData imdbModel)
        //{
        //    var movie = new MediaDto
        //    {
        //        ImdbId = imdbModel.Id,
        //        Title = imdbModel.Title,
        //        PosterUrl = imdbModel.Image,
        //        Year = imdbModel.Year,
        //        Plot = imdbModel.Plot,
        //        Companies = imdbModel.Companies,
        //        WeekFees = imdbModel.BoxOffice.OpeningWeekendUSA,
        //        GlobalFees = imdbModel.BoxOffice.CumulativeWorldwideGross,
        //        Type = new YMovies.Web.Dtos.TypeWebDto(),
        //        Cast = new List<CastWebDto>(),
        //        Genres = new List<GenreWebDto>(),
        //        Countries = new List<CountryWebDto>(),
        //        UsersLiked = new List<UserWebDto>(),
        //        UsersWatched = new List<UserWebDto>(),
        //        Seasons = new List<SeasonWebDto>()
        //    };

        //    movie.Type.Name = imdbModel.Type;
        //    if (imdbModel.TvSeriesInfo != null)
        //    {
        //        foreach (var imdbSeason in imdbModel.TvSeriesInfo.Seasons)
        //        {
        //            movie.Seasons.Add(new SeasonWebDto() { Name = imdbSeason });
        //        }
        //    };

        //    movie.Budget = GetDecimal(imdbModel.BoxOffice.Budget);

        //    movie.ImdbRating = GetDecimal(imdbModel.IMDbRating);

        //    if (imdbModel.ActorList != null)
        //    {
        //        foreach (var actor in imdbModel.ActorList)
        //        {
        //            movie.Cast.Add(new CastDto()
        //            {
        //                Name = actor.Name,
        //                PictureUrl = actor.Image
        //            });
        //        }
        //    }

        //    if (imdbModel.CountryList != null)
        //    {
        //        foreach (var country in imdbModel.CountryList)
        //        {
        //            movie.Countries.Add(new CountryDto()
        //            {
        //                Name = country.Value
        //            });
        //        }
        //    }

        //    if (imdbModel.GenreList != null)
        //    {
        //        foreach (var genre in imdbModel.GenreList)
        //        {
        //            movie.Genres.Add(new GenreDto()
        //            {
        //                Name = genre.Value
        //            });
        //        }
        //    }

        //    return movie;
        //}


        private decimal GetDecimal(string budget)
        {

            if (string.IsNullOrEmpty(budget))
            {
                return 0m;
            }

            string pattern = @"\d";

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(budget, pattern))
            {
                sb.Append(m);
            }
            string number = sb.ToString();
            decimal n = Convert.ToDecimal(number);

            return n;
        }
    }
}