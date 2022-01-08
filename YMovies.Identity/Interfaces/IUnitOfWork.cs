using System.Threading.Tasks;
using YMovies.Identity.DAL.Managers;

namespace YMovies.Identity.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ApplicationUserManager ApplicationUserManager { get; }
        ApplicationRoleManager ApplicationRoleManager { get; }
        Task SaveAsync();
    }
}
