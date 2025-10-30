using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Application.Services;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Data;
using ApiPrueba.src.Infrastructure.Repositories;
using ApiPrueba.src.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration cfg)
    {
        // Conexión a MySQL
        var connectionString = cfg.GetConnectionString("Mysqllocal");
        services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        //Fin Mysql Conection
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IService<,>), typeof(Service<,>));
        services.AddScoped<IRepository<ListaSeguimiento, int>, Repository<ListaSeguimiento, int>>();
        services.AddScoped<IRepository<ElementoLista, int>, Repository<ElementoLista, int>>();
        services.AddScoped<IRepository<Titulo, int>, Repository<Titulo, int>>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IListaSeguimientoService, ListaSeguimientoService>();
        services.AddScoped<ITituloService, TituloService>();
        return services;
    }
}