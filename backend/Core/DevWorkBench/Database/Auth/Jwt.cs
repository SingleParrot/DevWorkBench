using System.Security.Principal;

namespace DevWorkBench;

public class Jwt
{
    public long  JwtId {get;set;}
    public long UserId {get;set;}
    public string Token {get;set;} = String.Empty;
    public string TokenType {get;set;} = String.Empty;
    public DateTimeOffset CreatedAt {get;set;}
    public User? User {get;set;}
}