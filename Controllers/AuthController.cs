using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoleBasedAuth.Dtos.Auth;
using RoleBasedAuth.Dtos.Auth.Response;
using RoleBasedAuth.Models.Auth;

namespace RoleBasedAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AppUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            dto.Roles = new List<string>()
            {
                Role.Admin,
                Role.User
            };
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

                if (dto.Roles.Count > 0)
                {
                    await _userManager.AddToRolesAsync(user, dto.Roles);
                }

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

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(await GetValidClaims(user)),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSecret"))),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(securityToken);

                var response = new LoginResponseDto()
                {
                    Token = token
                };

                return Ok(response);
            }

            return BadRequest(new {message = "Username or password is incorrect"});
        }

        private async Task<List<Claim>> GetValidClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id),
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }
    }
}