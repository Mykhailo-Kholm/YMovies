//using System.Collections.Generic;

using System.Collections.Generic;
using AutoMapper;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;

namespace YMovies.Web.Services.Service
{
    public class SeasonWebService
    {
        private readonly IRepository<Season> _repository;
        public SeasonWebService(SeasonRepository repository) => _repository = repository;

        //private static readonly MapperConfiguration Config =
        //    new MapperConfiguration(cfg => cfg.CreateMap<Season, SeasonWebDto>());

        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<SeasonWebDto> Items => AutoMap.Mapper.Map<IEnumerable<Season>, IEnumerable<SeasonWebDto>>(_repository.Items);

        public SeasonWebDto GetItem(int id)
        {
            var season = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Season, SeasonWebDto>(season);
        }

        public void AddItem(SeasonWebDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonWebDto, Season>(item);
            _repository.AddItem(season);
        }

        public void UpdateItem(SeasonWebDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonWebDto, Season>(item);
            _repository.UpdateItem(season);
        }

        public void DeleteItem(SeasonWebDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonWebDto, Season>(item);
            _repository.DeleteItem(season.SeasonId);
        }
    }
}