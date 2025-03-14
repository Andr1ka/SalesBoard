using Microsoft.AspNetCore.Routing;
using Models.Requests;
using Services;
using WebApi.Extentions;
namespace WebApi.Endpoints
{
    public static class UserEndpoints
    {
        private const string _baseRoute = "/api/users";
        public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost(_baseRoute, async (CreateUserRequest request, IUserService userService, CancellationToken cancellationToken) =>
            {
                var result = await userService.CreateAsync(request.FirstName, request.LastName, request.Email, request.Password, cancellationToken);

                return result.ToResponse();

            });
        }
    }
}
