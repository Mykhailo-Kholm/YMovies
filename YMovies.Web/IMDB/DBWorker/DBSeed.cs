using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.Service;
using IMDbApiLib.Models;

namespace YMovies.Web.IMDB.DBWorker
{
    public class DBSeed : ISeed
    {
        private readonly MoviesContext _context;
        private readonly IService<MovieDto> _movieService;
        private readonly APIworkerIMDB aPIworkerIMDB;
        public DBSeed()
        {
            _context = new MoviesContext();
            _movieService = new MovieService(new MovieDbService.Repositories.Repository.MovieRepository(_context));
            aPIworkerIMDB = new APIworkerIMDB();
        }
        public async void AddMovieByImbdId(string imdbId)
        {
            if(_context.Movies.Any(m => m.ImdbId == imdbId))
            {
                return;
            }

            var media = await aPIworkerIMDB.MovieOrSeriesInfo(imdbId);

            if(media.Type is null)
            {
                return;
            }



            _movieService.AddItem(MapMovieToDtoFromImdb(media));
        }
        private MovieDto MapMovieToDtoFromImdb(TitleData imdbModel)
        {
            var movie = new MovieDto
            {
                ImdbId = imdbModel.Id,
                Title = imdbModel.Title,
                PosterUrl = imdbModel.Image,
                Year = imdbModel.Year,
                Plot = imdbModel.Plot,
                //Budget = 0m,
                BoxOffice = imdbModel.Companies,
                //ImdbRating = Decimal.Parse(imdbModel.IMDbRating),
                Type = imdbModel.Type,
                Cast = new List<CastDto>(),
                Genres = new List<GenreDto>(),
                Countries = new List<CountryDto>()
            };



            foreach(var actor in imdbModel.ActorList)
            {
                movie.Cast.Add(new CastDto
                {
                    Name = actor.Name,
                    PictureUrl = actor.Image
                });
            }

            foreach(var country in imdbModel.CountryList)
            {
                movie.Countries.Add(new CountryDto
                {
                    Name = country.Value
                });
            }

            foreach (var genre in imdbModel.GenreList)
            {
                movie.Genres.Add(new GenreDto
                {
                    Name = genre.Value
                });
            }

            return movie;
        }

        private SeriesDto MapSeriesToDtoFromImdb(TitleData imdbModel)
        {
            return null;
        }
    }
}