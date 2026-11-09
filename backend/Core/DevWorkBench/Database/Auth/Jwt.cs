using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DevWorkBench;

public class Jwt
{
    public long  JwtId {get;set;}
    public long UserId {get;set;}
    public string Token {get;set;} = String.Empty;
    public string TokenType {get;set;} = String.Empty;
    public DateTimeOffset CreatedAt {get;set;}
    public User? User {get;set;}
    public bool IsRevoked {get;set;}

    public Jwt (string token, string tokenType, User user, long userId,bool isRevoked)
    {
        this.Token = token;
        this.TokenType = tokenType;
        this.User = User;
        this.UserId = userId;
        this.IsRevoked = isRevoked;
    }
}