using Persistanse;
using Services;
using WebApi.Endpoints;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddOpenApi();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaleRepository, SaleRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISaleService, SaleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapUserEndpoints();
            app.MapSalesEndPoints();

            app.Run();
        }
    }
}
