using Domain.Exceptions;
using Services.Auth;
using System.Reflection;

namespace WebApi.Endpoints.Auth
{
    public sealed record AuthData(Guid UserId, Guid RefreshTokenId)
    {
        public static ValueTask<AuthData> BindAsync(HttpContext context, ParameterInfo info) 
        {
            var identity = context.User.Identities.First();

            if (!Guid.TryParse(identity.Claims.First(x => x.Type == ApplicationClaims.UserId).Value, out var userId))
            {
                throw new AuthorizationException();
            }

            if (!Guid.TryParse(identity.Claims.First(x => x.Type == ApplicationClaims.RefreshTokenId).Value, out var refreshTokenId))
            {
                throw new AuthorizationException();
            }

            return ValueTask.FromResult(new AuthData(userId, refreshTokenId));
        }
    }
}
