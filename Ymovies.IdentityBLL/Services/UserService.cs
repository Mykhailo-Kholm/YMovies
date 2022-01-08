using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Infrastructure;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.Identity.DAL.Interfaces;
using YMovies.Identity.DAL.Models;

namespace Ymovies.Identity.BLL.Services
{
    public class UserService : IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        private IUnitOfWork DataBase;        
        private bool disposedValue;

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDTO)
        {
            ClaimsIdentity claims = null;
            var user = await DataBase.ApplicationUserManager.FindAsync(userDTO.Email, userDTO.Password);
            if (user != null)
                claims = await DataBase.ApplicationUserManager
                    .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claims;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDTO)
        {
            var user = await DataBase.ApplicationUserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser {
                    Email = userDTO.Email,
                    UserName = userDTO.UserName,
                    Name = userDTO.Name,
                    SecondName = userDTO.SecondName
                };
                var result = await DataBase.ApplicationUserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await DataBase.ApplicationUserManager.AddToRoleAsync(user.Id, userDTO.Role);                
                await DataBase.SaveAsync();
                return new OperationDetails(true, "", "");
            }
            else
                return new OperationDetails(false, "User with this login is already existst", "Email");
        }

        public async Task SetInitialDataAsync(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await DataBase.ApplicationRoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await DataBase.ApplicationRoleManager.CreateAsync(role);
                }
            }
            await CreateAsync(adminDto);
        }

        public async Task<UserDTO> GetUserByEmailAsync(string userEmal)
        {
            var user = await DataBase.ApplicationUserManager
                .FindByEmailAsync(userEmal);
            return new UserDTO
            {
                Email = userEmal,
                UserName = user.UserName,
                Name = user.Name,
                SecondName = user.SecondName,
            };                
        }

        public async Task<OperationDetails> ResetPasswordAsync(string userEmail, string newPassword)
        {
            var user = await DataBase.ApplicationUserManager
                .FindByEmailAsync(userEmail);
            if (user == null)
                return new OperationDetails(false, "User with this login doesn't exists", "");
            await DataBase.ApplicationUserManager.RemovePasswordAsync(user.Id);
            await DataBase.ApplicationUserManager.AddPasswordAsync(user.Id, newPassword);
            await DataBase.SaveAsync();
            return new OperationDetails(true, "", "");
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }              
    }
}
