using Microsoft.AspNetCore.Identity;

namespace Trasher.Domain.Common
{
    public class User : AppUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
