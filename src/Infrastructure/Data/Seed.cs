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
            db.Users.Add(new User { Email = "admin@demo.com", Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"), Name = "Administrador", CreatedAt = DateTime.Now , Role = "admin" });
        }
        
        await db.SaveChangesAsync();
    }
}
