using System.Collections.Generic;

namespace Ymovies.Identity.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public List<string> Roles { get; set; }
    }
}
