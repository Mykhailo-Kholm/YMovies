using System.ComponentModel.DataAnnotations;
using Ymovies.Identity.BLL.Interfaces;
using Ymovies.Identity.BLL.Services;

namespace YMovies.Web.Utilites.ValidationAttributes
{
    public class IdentityUserAttribute : ValidationAttribute
    {
        public IdentityUserAttribute()
        {
            userService = serviceCreator.CreateUserService("IdentityDb");
        }

        IServiceCreator serviceCreator = new ServiceCreator();
        private IIdentityUserService userService;

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            return userService.GetUserByEmailAsync(value.ToString()) != null ? true : false;
        }
    }
}