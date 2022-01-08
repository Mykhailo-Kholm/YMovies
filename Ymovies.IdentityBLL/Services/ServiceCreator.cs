using Ymovies.Identity.BLL.Interfaces;
using YMovies.Identity.DAL.Repositories;

namespace Ymovies.Identity.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IIdentityUserService CreateUserService(string connection)
            => new IdentityUserService(new IdentityUnitOfWork(connection));
    }
}
