using Microsoft.AspNetCore.Routing;
using Models.Requests;
using Services;
using WebApi.Extentions;

namespace WebApi.Endpoints
{
    public static class SalesEndpoints
    {
        private const string _baseRoute = "/api/sales";
        public static void MapSalesEndPoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet(_baseRoute, async (ISaleService saleService, CancellationToken cancellationToken) =>
            {
                var result = await saleService.GetAllAsync(cancellationToken);
                return Results.Ok(result);
            });

            builder.MapPost(_baseRoute, async (CreateSaleRequest request, ISaleService saleService, CancellationToken cancelltaionToken) => 
            { 
                var result = await saleService.CreateAsync(request.UserId, request.Title, request.Description, request.Price, cancelltaionToken);
                return result.ToResponse();
            });
        }
    }
}
