using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _repository;
        public SearchService(ISearchRepository repository) => _repository = repository;
        
        public List<MediaDto> GetMediaByTitle(string title)
        {
            var movies = AutoMap.Mapper.Map<List<Media>, List<MediaDto>>(_repository.GetMediaByTitle(title));
            return movies;
        }

        public List<MediaDto> GetMediaByParams(FilterInfoDto filterInfoDto)
        {                
            var movies = AutoMap.Mapper.Map<List<Media>, List<MediaDto>>(_repository.GetMediaByParams(filterInfoDto));
            return movies;
        }

        public MediaDto GetItem(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<MediaDto> GetOneHundredMediaRandom()
        {
            throw new System.NotImplementedException();
        }

        public List<MediaDto> GetMostLiked()
        {
            throw new System.NotImplementedException();
        }

        public List<MediaDto> GetMediaByParams(string genre, string country, string year, string type)
        {
            throw new System.NotImplementedException();
        }
    }
}
