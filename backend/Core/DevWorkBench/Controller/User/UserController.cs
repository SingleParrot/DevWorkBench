using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization; // Password Hasher

namespace DevWorkBench
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService _userService) : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<User> GetUserWithId (string name)
        {
            User? user = await _userService.GetUserByName(name);
            return user!;   
        }

    }
}