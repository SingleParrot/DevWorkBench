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
    }
}