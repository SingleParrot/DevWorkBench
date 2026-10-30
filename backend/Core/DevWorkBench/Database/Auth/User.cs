using System.Numerics;
using System.Runtime.CompilerServices;

namespace DevWorkBench;

/**************************************************
 userid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    userpassword VARCHAR(255) NOT NULL, 
    email TEXT UNIQUE NOT NULL,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    isactive BOOLEAN NOT NULL DEFAULT FALSE,
    roles VARCHAR(10) not null DEFAULT "user"
**************************************************/
// Duplicate Usernames can't be allowed 
public class User
{
    public int Id {get;set;}
    public string Username {get;set;} = String.Empty;
    public string Password {get;set;} = String.Empty;
    public string Email {get;set;} = String.Empty;
    public bool IsActive {get;set;} = false;
    public string Roles {get;set;} = String.Empty;
}