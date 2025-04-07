using Models.Requests;
using Services.Auth;
using WebApi.Extentions;

namespace WebApi.Endpoints.Auth
{
    public static class AuthEndpoints
    {
        private const string _baseRoute = "/api/auth";
        public static void MapAuthEndPoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost($"{_baseRoute}/login", async (LoginRequest request, IAuthService service, HttpContext context, CancellationToken cancellationToken) =>
            {
                var result = await service.LoginAsync(request.Email, request.Password, context.Connection.RemoteIpAddress?.ToString() ?? string.Empty, cancellationToken);
                return result.ToResponse();
            })
            .AllowAnonymous();

            builder.MapPost($"{_baseRoute}/logout", async (AuthData data, IAuthService service, HttpContext context, CancellationToken cancellationToken) =>
            {
                await service.LogoutAsync(data.RefreshTokenId, context.Connection.RemoteIpAddress?.ToString() ?? string.Empty, cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization();

            builder.MapPost($"{_baseRoute}/refresh", async (RefreshPairRequest request, IAuthService service, HttpContext context, CancellationToken cancellationToken) =>
            {
                var result = await service.RefreshPairAsync(request.Token, context.Connection.RemoteIpAddress?.ToString() ?? string.Empty, cancellationToken);
                return result.ToResponse();
            })
            .RequireAuthorization();
        }
    }
}
