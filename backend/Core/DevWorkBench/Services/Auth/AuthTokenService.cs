using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace DevWorkBench
{
    public class TokenService {
        private JwtContext _tokenContext;

        public TokenService (JwtContext tokenContext)
        {
            this._tokenContext = tokenContext;
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
    }
}