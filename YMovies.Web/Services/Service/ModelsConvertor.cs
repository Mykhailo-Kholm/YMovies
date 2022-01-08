using System.Collections.Generic;
using IMDbApiLib.Models;
using YMovies.Web.DTOs;
using YMovies.Web.TempModels;

namespace YMovies.Web.Services.Service
{
    public class ModelsConvertor
    {
        public List<MoviesInfo> ConvertToMoviesInfo(List<Top250DataDetail> films)
        {
            List<MoviesInfo> moviesInfos = new List<MoviesInfo>();
            foreach (var movie in films)
            {
                moviesInfos.Add
                (
                    new MoviesInfo()
                    {
                        ImdbId = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.Image,
                        //ImdbRating = movie.IMDbRating,
                    }
                );
            }
            return moviesInfos;
        }

        public List<MoviesInfo> ConvertToMoviesInfo(IEnumerable<MovieWebDto> movies)
        {
            List<MoviesInfo> moviesInfos = new List<MoviesInfo>();
            foreach (var movie in movies)
            {
                moviesInfos.Add
                (
                    new MoviesInfo()
                    {
                        Id = movie.MovieId,
                        Title = movie.Title,
                        PosterUrl = movie.PosterUrl,
                        ImdbRating = movie.ImdbRating,
                    }
                );
            }
            return moviesInfos;
        }
    }
}