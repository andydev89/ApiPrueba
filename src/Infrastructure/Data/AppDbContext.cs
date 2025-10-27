using ApiPrueba.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

}
