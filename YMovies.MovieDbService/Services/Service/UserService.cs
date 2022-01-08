using System.Collections.Generic;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class UserService : IService<UserDto>
    {
        public UserService(IIdentityUserService service)
        {
            _databaseIdentity = service;                       
        }

        private IIdentityUserService _databaseIdentity;

        public IEnumerable<UserDto> Items => throw new System.NotImplementedException();

        public IEnumerable<UserDTO> GetAllUsersFromIdentity()
        {
            return _databaseIdentity.GetAllUsers();
        }

        public UserDto GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddItem(UserDto item)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateItem(UserDto item)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteItem(UserDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
