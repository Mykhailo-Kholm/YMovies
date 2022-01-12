using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class SeasonService : IService<SeasonDto>
    {
        private readonly IRepository<Season> _repository;
        public SeasonService(SeasonRepository repository) => _repository = repository;

        public IEnumerable<SeasonDto> Items => AutoMap.Mapper.Map<IEnumerable<Season>, IEnumerable<SeasonDto>>(_repository.Items);

        public SeasonDto GetItem(int id)
        {
            var season = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Season, SeasonDto>(season);
        }

        public void AddItem(SeasonDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonDto, Season>(item);
            _repository.AddItem(season);
        }

        public void UpdateItem(SeasonDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonDto, Season>(item);
            _repository.UpdateItem(season);
        }

        public void DeleteItem(SeasonDto item)
        {
            var season = AutoMap.Mapper.Map<SeasonDto, Season>(item);
            _repository.DeleteItem(season.SeasonId);
        }
    }
}
