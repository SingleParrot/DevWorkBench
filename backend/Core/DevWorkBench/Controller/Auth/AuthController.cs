using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
// Cookie with HttpOnly 
// .NET Backens sets token back to Login dircetly as Cookie 
// as HTTP only and Secure
// best option
// setting cookie in backend
namespace DevWorkBench
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly UserContext _userContext;

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login ([FromBody] RequestUser user)
        {// JWT Token as return Item
            // Compare Username & Password and Hash Password 
            
            
        }
    }
}
