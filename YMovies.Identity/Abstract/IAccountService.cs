using YMovies.Identity.Dtos;
using YMovies.Identity.Users;

namespace YMovies.Identity.Abstract
{
    public interface IAccountService
    {
        bool SignIn(UserDto user);
        void Login(UserDto user);
        void ChangePasword(string id);
        ApplicationUser GetUser(string id);

    }
}
