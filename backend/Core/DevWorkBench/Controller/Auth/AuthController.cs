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

        // POST Router Login 
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login ([FromBody] RequestUser requestUser)
        {
            // Implement Access Token

            // Implement Refresh Token

            // Set Cookie in Refresh Token

            return Ok(); // Returns code 200 and access toekn to save in RAM
        }
        // POST Router Sign Up
        
    }
}
