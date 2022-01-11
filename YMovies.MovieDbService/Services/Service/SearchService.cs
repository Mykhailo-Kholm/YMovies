using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Services.Service
{
    public class SearchService
    {
        private readonly ISearchRepository _repository;
        public SearchService(ISearchRepository repository) => _repository = repository;
        private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Models.Type, TypeDto>();
            cfg.CreateMap<Cast, CastDto>();
            cfg.CreateMap<Season, SeasonDto>();
            cfg.CreateMap<Country, CountryDto>();
            cfg.CreateMap<Genre, GenreDto>();
            cfg.CreateMap<Media, MediaDto>();
        });

        private readonly Mapper _mapper = new Mapper(Config);
        public List<MediaDto> GetMediaByTitle(string title)
        {
            var movies = _mapper.Map<List<Media>, List<MediaDto>>(_repository.GetMediaByTitle(title));
            return movies;
        }
    }
}
