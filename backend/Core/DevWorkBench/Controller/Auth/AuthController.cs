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
    public class AuthController(UserService _userService,TokenService _tokenService) : ControllerBase
    {
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

            User? dbUser = await _userService.GetUserByName(requestUser.Username);
            if (dbUser is null)
            {   
               return Unauthorized(); 
            }

            // Implement Access Token
            var accessToken = _tokenService.GenerateAccessToken(dbUser);

            // Implement Refresh Token
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Adds Token with TokenService
            await _tokenService.AddToken(new Jwt(refreshToken,"refresh",dbUser,dbUser.Id,false));

            // Set Cookie in Refresh Token & Access Token
            Response.Cookies.Append("access_token",accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // SSL 
                SameSite = SameSiteMode.Strict, // CSRF Protection = only sends if Request is from the same site
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                Path="/"
            });

            Response.Cookies.Append("refresh_token",refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // SSL 
                SameSite = SameSiteMode.Strict, // CSRF Protection   
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path="/api/auth" // send cookie to that path = frontend & API same origin 
            });
            
            // Creates new entry into the table jwt
            Jwt jwt = new Jwt(refreshToken,"refresh",dbUser,dbUser.Id,false);
            await _tokenService.AddToken(jwt);       
            
            return Ok(accessToken); // Returns code 200 and access toekn to save in RAM
        }
        // POST Router Sign Up
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUp([FromBody] RequestUser requestUser)
        {
            // User sends data to db and password is being hashed and inserted 


            // Implement Sign Up logic
            return Ok();
        }

        // POST Router Logout 
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            // Add Jwt Variable for Db setting flag so if user login it can be reactivated
            // Also update db table script and reload container
            // Refresh token löschen
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
    {
            // Wenn kein Cookie da ist, geben wir trotzdem 200 OK zurück (Sicherheit)
                return Ok(new { message = "Logged out successfully" });
            }


            var token = String.Empty;
            //Request.Cookies.TryGetValue("refresh_token",token);
            //Jwt jwt = new Jwt();
            //await _tokenService.UpdateToken(jwt);


            // Delete Cookies
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");

            // Implement Logout logic
            return Ok();
        }

        // POST Router Refresh Token
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> Refresh()
        {
            var refreshToken = _tokenService.GenerateRefreshToken();
            Response.Cookies.Append("refresh_token",refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // SSL 
                SameSite = SameSiteMode.Strict, // CSRF Protection   
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path="/api/auth" // send cookie to that path = frontend & API same origin 
            });

            // Implement Refresh Token logic
            return Ok();
        }
    }
}
