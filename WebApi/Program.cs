using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistanse;
using Services;
using WebApi.Endpoints;
using WebApi.Options;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();  
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
            builder.Services.AddScoped(typeof(IMongoDatabase), sp =>
            {
                var options = sp.GetService<IOptions<DatabaseOptions>>();
                var url = MongoUrl.Create(options.Value.ConnectionString);
                var client = new MongoClient(url);
                return client.GetDatabase(url.DatabaseName);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaleRepository, SaleRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISaleService, SaleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapUserEndpoints();
            app.MapSalesEndPoints();

            app.Run();
        }
    }
}
