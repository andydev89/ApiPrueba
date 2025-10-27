namespace ApiPrueba.src.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddDefaultCors(this IServiceCollection services, IConfiguration cfg)
    {
        var origin = cfg["Cors:Origin"] ?? "http://localhost:5173";
        services.AddCors(o => o.AddDefaultPolicy(p =>
            p.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
        return services;
    }
}