using Domain.Auth;

namespace Services.Auth
{
    public interface IJwtService
    {
        TokenPair CreateTokenPair(Guid userId, string ip);
    }
}
