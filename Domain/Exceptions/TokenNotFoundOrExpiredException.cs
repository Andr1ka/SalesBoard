namespace Domain.Exceptions
{
    public class TokenNotFoundOrExpiredException : Exception
    {
        public TokenNotFoundOrExpiredException() : base("Token not found or expired")
        {

        }
    }
}
