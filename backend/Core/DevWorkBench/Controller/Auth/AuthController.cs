using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly UserService _userService;
        // POST Router Login 
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login ([FromBody] RequestUser requestUser)
        {
            bool isUserValid = await _userService.CompareUserCredentials(requestUser);
            if (!isUserValid)
            {
                return Unauthorized();
            }
            // Implement Access Token
            

            // Implement Refresh Token

            // Set Cookie in Refresh Token
            
            return Ok(); // Returns code 200 and access toekn to save in RAM
        }
        // POST Router Sign Up
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUp([FromBody] RequestUser requestUser)
        {
            // Implement Sign Up logic
            return Ok();
        }

        // POST Router Logout 
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            // Implement Logout logic
            return Ok();
        }

        // POST Router Refresh Token
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> Refresh()
        {
            // Implement Refresh Token logic
            return Ok();
        }
    }
}
