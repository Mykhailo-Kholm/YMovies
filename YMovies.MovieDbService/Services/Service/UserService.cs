using System.Collections.Generic;
using System.Linq;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class UserService : IService<UserDto>
    {
        public UserService(IIdentityUserService service)
        {
            _databaseIdentity = service;
            _repository = new UserRepository(new MoviesContext());
        }
        private readonly IIdentityUserService _databaseIdentity;
        private readonly UserRepository _repository;
        public IEnumerable<UserDto> Items => AutoMap.Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(_repository.Items);

        public void GetAllUsersFromIdentity()
        {
            var users = _databaseIdentity.GetAllUsers().ToList();
            foreach (var u in users)
            {
                _repository.AddItem(new User()
                {
                    IdentityId = u.Id,
                    FullName = u.Name + u.SecondName
                });
            }
        }
        public void AddUserToMovieDb(UserDTO user)
        {
            _repository.AddItem(new User()
            {
                IdentityId = user.Id,
                FullName = user.Name + user.SecondName
            });
        }
        public UserDto GetItem(int id)
        {
            var user = _repository.GetItem(id);
            return AutoMap.Mapper.Map<User, UserDto>(user);
        }

        public void AddItem(UserDto item)
        {
            var user = AutoMap.Mapper.Map<UserDto, User>(item);
            _repository.AddItem(user);
        }

        public void UpdateItem(UserDto item)
        {
            var user = AutoMap.Mapper.Map<UserDto, User>(item);
            _repository.UpdateItem(user);
        }

        public void DeleteItem(UserDto item)
        {
            var user = AutoMap.Mapper.Map<UserDto, User>(item);
            _repository.DeleteItem(user.Id);
        }
    }
}
