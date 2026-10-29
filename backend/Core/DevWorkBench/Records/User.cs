namespace DevWorkBench;

public record RequestUser
{
    public string Username {get;init;} = String.Empty;
    public string Password {get;init;} = String.Empty;
}