using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class SeasonService:IService<SeasonDto>
    {
        private readonly IRepository<Season> _repository;
        public SeasonService(SeasonRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Season, SeasonDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<SeasonDto> Items => _mapper.Map<IEnumerable<Season>, IEnumerable<SeasonDto>>(_repository.Items);

        public SeasonDto GetItem(int id)
        {
            var season = _repository.GetItem(id);
            return _mapper.Map<Season, SeasonDto>(season);
        }

        public void AddItem(SeasonDto item)
        {
            var season = _mapper.Map<SeasonDto, Season>(item);
            _repository.AddItem(season);
        }

        public void UpdateItem(SeasonDto item)
        {
            var season = _mapper.Map<SeasonDto, Season>(item);
            _repository.UpdateItem(season);
        }

        public void DeleteItem(SeasonDto item)
        {
            var season = _mapper.Map<SeasonDto, Season>(item);
            _repository.DeleteItem(season.SeasonId);
        }
    }
}
