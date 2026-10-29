using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Password Hasher

namespace DevWorkBench
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController (UserContext userContext)
        {
            _context = userContext;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser ([FromBody] User user)
        {
            User DbUser = new User();
            // Hash Password & Apply Save Async
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(user),user);
        }
    }
}