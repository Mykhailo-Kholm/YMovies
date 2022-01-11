using System.Collections.Generic;
using IMDbApiLib.Models;
using YMovies.Web.DTOs;
using YMovies.Web.TempModels;

namespace YMovies.Web.Services.Service
{
    public class ModelsConvertor
    {
        private List<MoviesInfo> _moviesInfos;

        public List<MoviesInfo> ConvertToMoviesInfo(List<Top250DataDetail> films)
        {
            _moviesInfos = new List<MoviesInfo>();
            foreach (var movie in films)
            {
                _moviesInfos.Add
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
            return _moviesInfos;
        }

        public List<MoviesInfo> ConvertToMoviesInfo(IEnumerable<MovieWebDto> movies)
        {
            _moviesInfos = new List<MoviesInfo>();
            foreach (var movie in movies)
            {
                _moviesInfos.Add
                (
                    new MoviesInfo()
                    {
                        Id = movie.MediaId,
                        Title = movie.Title,
                        PosterUrl = movie.PosterUrl,
                        ImdbRating = movie.ImdbRating,
                    }
                );
            }
            return _moviesInfos;
        }
    }
}