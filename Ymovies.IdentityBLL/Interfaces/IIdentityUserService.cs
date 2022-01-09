using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Infrastructure;

namespace Ymovies.Identity.BLL.Interfaces
{
    public interface IIdentityUserService : IDisposable
    {
        Task<OperationDetails> CreateAsync(UserDTO userDTO);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDTO);        
        Task<UserDTO> GetUserByEmailAsync(string userEmal);
        IEnumerable<UserDTO> GetAllUsers();
        Task<OperationDetails> ResetPasswordAsync(string userEmail, string newPassword);
        Task<OperationDetails> GiveAdminRightsByEmail(string userEmail);
        Task SetInitialDataAsync(UserDTO userDTO, List<string> roles);
    }
}
