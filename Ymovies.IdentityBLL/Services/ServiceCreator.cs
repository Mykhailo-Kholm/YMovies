using Ymovies.Identity.BLL.Interfaces;
using YMovies.Identity.DAL.Repositories;

namespace Ymovies.Identity.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
            => new UserService(new IdentityUnitOfWork(connection));
    }
}
