using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Dtos.Auth;
using RoleBasedAuth.Models.Auth;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            var user = new AppUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            try
            {
                var result
                    = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    return Ok(new {message = "User created successfully"});
                }

                return BadRequest(result.Errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}