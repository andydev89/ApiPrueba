using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiPrueba.src.Api.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration cfg)
    {
        var key = cfg["Jwt:Key"] ?? "v3ry_long_local_dev_secret_key_please_change_1234567890!!";
        var skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = skey
                };
            });
        services.AddAuthorization();
        return services;
    }
}