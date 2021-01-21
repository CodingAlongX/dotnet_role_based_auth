using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuth.Models.Auth;

namespace RoleBasedAuth.Data
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}