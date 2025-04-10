using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Persistanse;
using Services.Auth;
using Services.Options;
using Services.Sales;
using Services.Users;
using System.Text;
using WebApi.Endpoints;
using WebApi.Endpoints.Auth;
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
            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(nameof(AuthOptions)));

            builder.Services.AddScoped(typeof(IMongoDatabase), sp =>
            {
                var options = sp.GetService<IOptions<DatabaseOptions>>();
                var url = MongoUrl.Create(options.Value.ConnectionString);
                var client = new MongoClient(url);
                return client.GetDatabase(url.DatabaseName);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaleRepository, SaleRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISaleService, SaleService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IHashService, HashService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            builder.Services.AddDataProtection().UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                }
            );
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["AuthOptions:JwtIssurer"],
                    ValidAudience = builder.Configuration["AuthOptions:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthOptions:JwtSecret"] ?? string.Empty))
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapUserEndpoints();
            app.MapSalesEndPoints();
            app.MapAuthEndPoints(); 

            app.Run();
        }
    }
}
