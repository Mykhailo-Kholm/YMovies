using System.Collections.Generic;
using IMDbApiLib.Models;
using YMovies.Web.DTOs;
using YMovies.Web.TempModels;

namespace YMovies.Web.Services.Service
{
    public class ModelsConvertor
    {
        private List<MoviesInfo> _moviesInfos;
        private TypesConverter _typesConvertor;


        public List<MoviesInfo> ConvertToMoviesInfo(List<Top250DataDetail> films)
        {
            _moviesInfos = new List<MoviesInfo>();
            _typesConvertor = new TypesConverter();
            foreach (var movie in films)
            {
                _moviesInfos.Add
                (
                    new MoviesInfo()
                    {
                        ImdbId = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.Image,
                        ImdbRating = _typesConvertor.StringToDecimal(movie.IMDbRating)
                    }
                );
            }
            return _moviesInfos;
        }

        public List<MoviesInfo> ConvertToMoviesInfo(IEnumerable<MediaWebDto> movies)
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