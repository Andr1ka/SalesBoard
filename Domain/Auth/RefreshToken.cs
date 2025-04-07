namespace Domain.Auth
{
    public class RefreshToken : PersistableEntity
    {
        public Guid UserId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public string CreatedByIp { get; set; } = string.Empty;

        public DateTime? Revoked { get; set; }

        public string RevokedByIp { get; set; } = string.Empty;

        public string? ReplacedByToken { get; set; } = string.Empty;

        public bool IsActive => Revoked == null || !IsExpired;

        public void RevokeByToken(RefreshToken token)
        {
            Revoked = DateTime.UtcNow;
            RevokedByIp = token.CreatedByIp;
            ReplacedByToken = token.Token;

        }

        public void RevokeByIp(string ip)
        {
            Revoked = DateTime.UtcNow;
            RevokedByIp = ip;
            ReplacedByToken = null;
        }

    }
}
