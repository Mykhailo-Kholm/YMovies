﻿using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    class GenreService:IService<GenreDto>
    {
        private readonly IRepository<Genre> _repository;
        public GenreService(GenreRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<GenreDto> Items => _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(_repository.Items);
        public GenreDto GetItem(int id)
        {
            var genre = _repository.GetItem(id);
            return _mapper.Map<Genre, GenreDto>(genre);
        }

        public void AddItem(GenreDto item)
        {
            var genre = _mapper.Map<GenreDto, Genre>(item);
            _repository.AddItem(genre);
        }

        public void UpdateItem(GenreDto item)
        {
            var genre = _mapper.Map<GenreDto, Genre>(item);
            _repository.UpdateItem(genre);
        }

        public void DeleteItem(GenreDto item)
        {
            var genre = _mapper.Map<GenreDto, Genre>(item);
            _repository.DeleteItem(genre.Id);
        }
    }
}