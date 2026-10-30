using System.Security.Principal;

namespace DevWorkBench;

public class Jwt
{
    public int JwtId {get;set;}
    public User? User {get;set;}
    public string Token {get;set;} = String.Empty;
    public string TokenType {get;set;} = String.Empty;
    public DateTimeOffset CreatedAt {get;set;}

}