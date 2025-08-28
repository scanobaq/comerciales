using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using comerciales.Application.Abstractions;
using comerciales.Application.Services;
using comerciales.Domain.Interfaces;
using comerciales.Infrastructure.Abstractions;
using comerciales.Infrastructure.Repository;
using comerciales.Infrastructure.Security;
using comerciales.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
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
        services.AddScoped<IMunicipioRepository, MunicipioRepository>();
        services.AddScoped<IMunicipioService, MunicipioService>();
        services.AddScoped<IComercianteRepository, ComercianteRepository>();
        services.AddScoped<IComercianteService, ComercianteService>();
        services.AddScoped<ICurrentUser, HttpCurrentUser>();
        services.AddScoped<SaveChangesInterceptor, SessionContextInterceptor>();

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = JwtRegisteredClaimNames.Sub

            };
        });
    }

    // public static void AddAuthorization(this IServiceCollection services)
    // {
    //     services.AddAuthorization(options =>
    //     {
    //         options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrador"));
    //         options.AddPolicy("UserPolicy", policy => policy.RequireRole("Auxiliar"));
    //         options.AddPolicy("AdminOrUserPolicy", policy => policy.RequireRole("Administrador", "Auxiliar"));
    //     });
    // }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // ⇩ Interceptor que setea SESSION_CONTEXT con el userId
        services.AddScoped<SaveChangesInterceptor, SessionContextInterceptor>();

        services.AddDbContext<comercial.Infrastructure.Persistence.Models.AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly("comerciales.WebApi") // o donde tengas tus migraciones
            );

            // ⇩ Agregar el interceptor tomado desde el ServiceProvider (scope actual)
            options.AddInterceptors(sp.GetRequiredService<SaveChangesInterceptor>());
        });

        return services;
    }


}
