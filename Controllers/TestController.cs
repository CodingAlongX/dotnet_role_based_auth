using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Attributes;
using RoleBasedAuth.Models.Auth;

namespace RoleBasedAuth.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("for-admin")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> GetForAdmin()
        {
            return Ok(new {message = "Hi! I guess you are an admin"});
        }

        [HttpGet("for-author")]
        [AuthorizeRoles(Role.Author)]
        public async Task<IActionResult> GetForAuthor()
        {
            return Ok(new {message = "Hi! I guess you are an author"});
        }

        [HttpGet("for-author-and-admin")]
        // User has to be both Admin and Author
        [AuthorizeRoles(Role.Author)]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> GetForAuthorAndAdmin()
        {
            return Ok(new {message = "Hi! I guess you are both author and admin"});
        }

        [HttpGet("for-author-or-admin")]
        //User has to be either Admin or Author
        [AuthorizeRoles(Role.Author, Role.Admin)]
        public async Task<IActionResult> GetForAuthorOrAdmin()
        {
            return Ok(new {message = "Hi! I guess you are both author and admin"});
        }
    }
}