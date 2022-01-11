using IMDbApiLib.Models;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;

namespace YMovies.Web.Services.Service
{
    public class ModelsConvertor
    {
        private List<MediaDto> _moviesInfos;
        private TypesConvertor _convertor;

        public List<MediaDto> ConvertToMoviesInfo(List<Top250DataDetail> films)
        {
            _convertor = new TypesConvertor();
            _moviesInfos = new List<MediaDto>();
            foreach (var movie in films)
            {
                _moviesInfos.Add
                (
                    new MediaDto()
                    {
                        ImdbId = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.Image,
                        ImdbRating = _convertor.ConvertTDecimal(movie.IMDbRating)
                    }
                );
            }
            return _moviesInfos;
        }

        public List<MediaDto> ConvertToMediaDtos(List<MostPopularDataDetail> films)
        {
            _convertor = new TypesConvertor();
            _moviesInfos = new List<MediaDto>();
            foreach (var movie in films)
            {
                _moviesInfos.Add
                (
                    new MediaDto()
                    {
                        ImdbId = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.Image,
                        ImdbRating = _convertor.ConvertTDecimal(movie.IMDbRating)
                    }
                );
            }
            return _moviesInfos;
        }

        //public List<MoviesInfo> ConvertToMoviesInfo(IEnumerable<MediaDto> movies)
        //{
        //    _moviesInfos = new List<MoviesInfo>();
        //    foreach (var movie in movies)
        //    {
        //        _moviesInfos.Add
        //        (
        //            new MoviesInfo()
        //            {
        //                Id = movie.MediaId,
        //                Title = movie.Title,
        //                PosterUrl = movie.PosterUrl,
        //                ImdbRating = movie.ImdbRating,
        //            }
        //        );
        //    }
        //    return _moviesInfos;
        //}
    }
}