using ApiPrueba.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Infrastructure.Data;

public static class Seed
{
    public static async Task EnsureAsync(AppDbContext db)
    {
        await db.Database.MigrateAsync();
        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new User { Email = "admin@demo.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role = "admin" });
            db.Users.Add(new User { Email = "user@demo.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"), Role = "user" });
        }
        if (!await db.Products.AnyAsync())
        {
            db.Products.Add(new Product { Name = "Café Molido", Price = 4.50m, Stock = 50 });
            db.Products.Add(new Product { Name = "Leche Entera", Price = 1.20m, Stock = 100 });
        }
        await db.SaveChangesAsync();
    }
}
