namespace DevWorkBench
{
    public class UserException : Exception
    {
        
        public UserException () : base() {}
        
        public UserException (string message) : base(message)
        {}

        // Inner Exception: an underlining error causes an second error
        public UserException(string message,Exception inner) : base(message,inner){}
    }
}