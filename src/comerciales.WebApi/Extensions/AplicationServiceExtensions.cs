using System.Text;
using comerciales.Application.Abstractions;
using comerciales.Application.Services;
using comerciales.Domain.Interfaces;
using comerciales.Infrastructure.Abstractions;
using comerciales.Infrastructure.Repository;
using comerciales.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace comerciales.WebApi.Extensions;

public static class AplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
                 builder.WithOrigins("http://localhost:5173")
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials()
                 );
         });


    public static void AddAplicacionServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRespository>();



    }

    public static void JwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<comercial.Infrastructure.Persistence.Models.AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("comerciales.WebApi")));
    }


}
