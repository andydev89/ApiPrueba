using Microsoft.OpenApi.Models;

namespace ApiPrueba.src.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPrueba", Version = "v1" });

            var securitySchema = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Ingrese el token JWT con el prefijo 'Bearer '",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new string[] { } }
                });
        });
        return services;
    }
    public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
    {
        app.UseSwagger(); app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPrueba v1");
            // opcional: c.RoutePrefix = string.Empty; // para servir UI en raíz
        });
        return app;
    }
}

