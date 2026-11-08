using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Security.Cryptography;

namespace DevWorkBench
{
    public class TokenService {
        private JwtContext _tokenContext;
        private readonly IConfiguration _configuration;

        public TokenService (JwtContext tokenContext, IConfiguration configuration)
        {
            this._tokenContext = tokenContext;
            this._configuration = configuration;
        }

        public async Task<Jwt?> GetJwtUserToken (long UserId,CancellationToken ct = default)
        {
            var jwt = await _tokenContext.Jwts
            .Include(j => j.User) // Where Request 
            .FirstOrDefaultAsync(j => j.UserId == UserId,ct); // set Limit and INNERJOIN

            return jwt;
        }

        public async Task<Jwt?> GetJwtToken (User user)
        {
           return await _tokenContext.Jwts.FirstOrDefaultAsync<Jwt>(j => j.UserId == user.Id);
        }

        public async Task<Jwt?> GetJwtToken (string token)
        {
            return await _tokenContext.Jwts.FirstOrDefaultAsync<Jwt>(j => j.Token == token);
        }

        public async Task<bool> AddToken (Jwt jwt)
        {
            await _tokenContext.Jwts.AddAsync(jwt);
            var result = await _tokenContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveToken (Jwt jwt)
        {
            _tokenContext.Jwts.Remove(jwt);
            var result = await _tokenContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveToken (string token)
        {
            var jwt = await GetJwtToken(token);
            if (jwt == null) return false;
            _tokenContext.Jwts.Remove(jwt);
            var result = await _tokenContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Jwt> UpdateToken (Jwt jwt)
        {
            _tokenContext.Jwts.Update(jwt);
            await _tokenContext.SaveChangesAsync();
            return jwt;
        }

        public string GenerateAccessToken (User user)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
           var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);

           // Create claims for the token, including the user's ID and username
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.Roles));


           var tokenDescriptor = new SecurityTokenDescriptor
           {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), // Token Expiration Time
                Issuer = _configuration["Jwt:Issuer"]!, // right address if the token is valid or not
                Audience = _configuration["Jwt:Audience"]!,// right platforem if the token is valid or not
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
           };
             
            var token = tokenHandler.CreateToken(tokenDescriptor);// create the token with the descriptor
            return tokenHandler.WriteToken(token);// WriteToken() = return the token as a string
           
        }

        public string GenerateRefreshToken (User user)
        {
            var randomNumber = new byte[64];// using = garbage collector automatically disposes of the object when it is no longer needed, freeing up resources and preventing memory leaks.    
            using (var rng = RandomNumberGenerator.Create()) // using System.Security.Cryptography; to generate a random number for the refresh token
            {
                rng.GetBytes(randomNumber);// fills in an array of bytes with a cryptographically strong sequence of random values
                return Convert.ToBase64String(randomNumber); // Convert the random number to a base64 string and return it as the refresh token
                // base64 = a way to encode binary data into a string format that can be easily transmitted over text-based protocols like HTTP. It uses a set of 64 characters (A-Z, a-z, 0-9, +, /) to represent the binary data. The resulting string is longer than the original binary data, but it can be safely transmitted as text without losing any information.
            }
        }

    }
}