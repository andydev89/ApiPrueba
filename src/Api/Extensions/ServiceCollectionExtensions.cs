using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Application.Services;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Data;
using ApiPrueba.src.Infrastructure.Repositories;
using ApiPrueba.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite(cfg.GetConnectionString("Default") ?? "Data Source=app.db"));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}