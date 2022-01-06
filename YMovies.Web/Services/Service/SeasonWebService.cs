using System.Collections.Generic;
using AutoMapper;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;

namespace YMovies.Web.Services.Service
{
    public class SeasonWebService
    {
        private readonly IRepository<Season> _repository;
        public SeasonWebService(SeasonRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Season, SeasonWebDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<SeasonWebDto> Items => _mapper.Map<IEnumerable<Season>, IEnumerable<SeasonWebDto>>(_repository.Items);

        public SeasonWebDto GetItem(int id)
        {
            var season = _repository.GetItem(id);
            return _mapper.Map<Season, SeasonWebDto>(season);
        }

        public void AddItem(SeasonWebDto item)
        {
            var season = _mapper.Map<SeasonWebDto, Season>(item);
            _repository.AddItem(season);
        }

        public void UpdateItem(SeasonWebDto item)
        {
            var season = _mapper.Map<SeasonWebDto, Season>(item);
            _repository.UpdateItem(season);
        }

        public void DeleteItem(SeasonWebDto item)
        {
            var season = _mapper.Map<SeasonWebDto, Season>(item);
            _repository.DeleteItem(season.SeasonId);
        }
    }
}