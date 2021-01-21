using Microsoft.AspNetCore.Identity;

namespace RoleBasedAuth.Models.Auth
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}