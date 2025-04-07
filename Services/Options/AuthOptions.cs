namespace Services.Options
{
    public class AuthOptions
    {
        public string JwtSecret { get; set; } = string.Empty;

        public string JwtIssurer { get; set; } = string.Empty;

        public string JwtAudience {  get; set; } = string.Empty;

        public int AccessTokenTimeToLiveInMinutes { get; set; }

        public int RefreshTokenTimeToLiveInDays { get; set; }
    }
}
