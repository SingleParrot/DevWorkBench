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

        

        // POST Router Login 


        // POST Router Sign Up
    }
}
