namespace Services.Auth
{
    public interface IHashService
    {
        string GenerateSalt();

        string GenerateToken();

        string CalculateHash(string password, string salt);

        bool VerifyHash(string password, string salt, string hash);
    }
}
