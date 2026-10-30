using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace DevWorkBench
{
    public class UserService
    {
        private UserContext _userContext;
        private PasswordHasher<string> hasher = new PasswordHasher<string>();

        public UserService (UserContext context)
        {
            _userContext = context;
        }

        public async Task<List<User>> GetAllUsers ()
        {
            return await _userContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByName (string name) // Implemet ? because user can't be implemented
        {
            return await _userContext.Users.FindAsync(name);
        }

        public async Task<User?> CreateUser (User user)
        {
            _userContext.Add(user);
            await _userContext.SaveChangesAsync();
            ConsoleLogger.Info($"Created User:" + user.Username);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
        
            var dbUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
    
            if (dbUser == null)
            {
                var message = $"Error while fetching User from Update User: {user.Username}";
                ConsoleLogger.Error(message);
                // Actually throw an exception instead of returning null
                throw new UserException(message);

            }
            dbUser.Username = user.Username;
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                dbUser.Password = hasher.HashPassword(null!, user.Password);
            }

            dbUser.Email = user.Email;
            dbUser.IsActive = user.IsActive;
            dbUser.Roles = user.Roles;

            await _userContext.SaveChangesAsync();
            ConsoleLogger.Info($"Updated User:" + dbUser.Username);
            return dbUser;
        }

        public async Task<User> DeleteUser (User user)
        {
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
            ConsoleLogger.Info($"Deleted User:" + user.Username);
            return user;
        }

        public async Task<bool> CompareUserCredentials (RequestUser user)
        {
            var userPassword = hasher.HashPassword(null!,user.Password);
            var dbUser = await _userContext.Users.FindAsync(user.Username);
            if (dbUser != null && dbUser.Password.Equals(userPassword))
            {
                return true;
            }
            return false;
        }
    }
}