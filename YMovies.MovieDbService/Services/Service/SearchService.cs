using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class SearchService
    {
        private readonly ISearchRepository _repository;
        public SearchService(ISearchRepository repository) => _repository = repository;
        
        public List<MediaDto> GetMediaByTitle(string title)
        {
            var movies = AutoMap.Mapper.Map<List<Media>, List<MediaDto>>(_repository.GetMediaByTitle(title));
            return movies;
        }

        public List<MediaDto> GetMediaByParams(string genre = null, string country = null, string year = null, string type=null)
        {
            var movies = AutoMap.Mapper.Map<List<Media>, List<MediaDto>>(_repository.GetMediaByParams(genre, country,year,type));
            return movies;
        }
    }
}
