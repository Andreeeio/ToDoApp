using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ToDoApp.Application.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoApp.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace ToDoApp.Application.Extentions;

public static class ServiceCollectionExtentions
{
    public static void AddAplication(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            var tokenKey = configuration["TokenKey"] ?? throw new Exception("Token key was not found");
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
        );

        services.AddScoped<IUserContext, UserContext>();

        services.AddAutoMapper(typeof(ServiceCollectionExtentions).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtentions).Assembly));
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtentions).Assembly).AddFluentValidationAutoValidation();
    }
}