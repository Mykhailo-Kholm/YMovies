using Microsoft.AspNet.Identity.EntityFramework;

namespace YMovies.Identity.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        { }
        public string Name { get; set; }
        public string SecondName { get; set; }
    }
}
