namespace Ymovies.Identity.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IIdentityUserService CreateUserService(string connection);
    }
}
