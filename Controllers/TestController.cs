using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuth.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        //User has to be either Admin or Author
        // [AuthorizeRoles(Role.Admin,Role.Author)]
        // User has to be both Admin and Author
        // [AuthorizeRoles(Role.Admin)]
        // [AuthorizeRoles(Role.Author)]
        public async Task<IActionResult> Get()
        {
            return Ok(new {message = "Hi"});
        }
    }
}