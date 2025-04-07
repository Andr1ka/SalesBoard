namespace Domain.Exceptions
{
    public class InvalidCredentailsException : Exception
    {
        public InvalidCredentailsException() : base("User email or password are invalid")
        {

        }
    }
}
