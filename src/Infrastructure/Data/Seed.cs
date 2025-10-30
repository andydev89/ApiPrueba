using ApiPrueba.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ApiPrueba.src.Infrastructure.Data
{
    public static class Seed
    {
        public static async Task EnsureAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();

          
            var admin = await db.Users.FirstOrDefaultAsync(u => u.Email == "admin@demo.com");
            if (admin == null)
            {
                admin = new User
                {
                    Email = "admin@demo.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Name = "Administrador",
                    CreatedAt = DateTime.UtcNow,
                    Role = "admin"
                };
                db.Users.Add(admin);
                await db.SaveChangesAsync();
            }

           
            if (!await db.Titles.AnyAsync())
            {
                var titulos = new List<Titulo>
                {
                    new Titulo
                    {
                        Nombre = "The Dark Knight",
                        Tipo = "pelicula",
                        Año = 2008,
                        Genero = "Acción",
                        Descripcion = "Batman enfrenta al Joker, un criminal que siembra el caos en Gotham.",
                        ImagenUrl = "https://m.media-amazon.com/images/I/51CbD6rJv0L._AC_.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Titulo
                    {
                        Nombre = "Inception",
                        Tipo = "pelicula",
                        Año = 2010,
                        Genero = "Ciencia Ficción",
                        Descripcion = "Un ladrón roba secretos a través de los sueños y busca redimirse.",
                        ImagenUrl = "https://m.media-amazon.com/images/I/81p+xe8cbnL._AC_SL1500_.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Titulo
                    {
                        Nombre = "Breaking Bad",
                        Tipo = "serie",
                        Año = 2008,
                        Genero = "Drama",
                        Descripcion = "Un profesor de química se convierte en fabricante de metanfetamina.",
                        ImagenUrl = "https://m.media-amazon.com/images/I/51xTjvO6K-L._AC_.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Titulo
                    {
                        Nombre = "Stranger Things",
                        Tipo = "serie",
                        Año = 2016,
                        Genero = "Ciencia Ficción",
                        Descripcion = "Un grupo de niños enfrenta fuerzas sobrenaturales en su pequeño pueblo.",
                        ImagenUrl = "https://m.media-amazon.com/images/I/81k3gP2YQfL._AC_SL1500_.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Titulo
                    {
                        Nombre = "The Matrix",
                        Tipo = "pelicula",
                        Año = 1999,
                        Genero = "Ciencia Ficción",
                        Descripcion = "Neo descubre que vive en una simulación controlada por máquinas.",
                        ImagenUrl = "https://m.media-amazon.com/images/I/71b9icFvVQL._AC_SL1024_.jpg",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await db.Titles.AddRangeAsync(titulos);
                await db.SaveChangesAsync();
            }

           
            if (!await db.ListasSeguimiento.AnyAsync())
            {
                var listas = new List<ListaSeguimiento>
                {
                    new ListaSeguimiento
                    {
                        UserId = admin.Id,
                        Name = "Películas Favoritas",
                        Description = "Colección de las mejores películas.",
                        CreatedAt = DateTime.UtcNow
                    },
                    new ListaSeguimiento
                    {
                        UserId = admin.Id,
                        Name = "Series Pendientes",
                        Description = "Series que quiero ver próximamente.",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await db.ListasSeguimiento.AddRangeAsync(listas);
                await db.SaveChangesAsync();
            }

            // 🎞️ 4️⃣ Elementos de las listas
            if (!await db.ElementosLista.AnyAsync())
            {
                var adminListas = await db.ListasSeguimiento
                    .Where(l => l.UserId == admin.Id)
                    .ToListAsync();

                var titulos = await db.Titles.ToListAsync();

                var elementos = new List<ElementoLista>
                {
                    new ElementoLista
                    {
                        ListaSeguimientoId = adminListas.First(l => l.Name.Contains("Favoritas")).Id,
                        TituloId = titulos.First(t => t.Nombre.Contains("Dark Knight")).Id,
                        AddAt = DateTime.UtcNow
                    },
                    new ElementoLista
                    {
                        ListaSeguimientoId = adminListas.First(l => l.Name.Contains("Favoritas")).Id,
                        TituloId = titulos.First(t => t.Nombre.Contains("Matrix")).Id,
                        AddAt = DateTime.UtcNow
                    },
                    new ElementoLista
                    {
                        ListaSeguimientoId = adminListas.First(l => l.Name.Contains("Pendientes")).Id,
                        TituloId = titulos.First(t => t.Nombre.Contains("Breaking Bad")).Id,
                        AddAt = DateTime.UtcNow
                    },
                    new ElementoLista
                    {
                        ListaSeguimientoId = adminListas.First(l => l.Name.Contains("Pendientes")).Id,
                        TituloId = titulos.First(t => t.Nombre.Contains("Stranger Things")).Id,
                        AddAt = DateTime.UtcNow
                    }
                };

                await db.ElementosLista.AddRangeAsync(elementos);
                await db.SaveChangesAsync();
            }
        }
    }
}
