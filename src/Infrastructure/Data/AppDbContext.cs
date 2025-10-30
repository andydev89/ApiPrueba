using ApiPrueba.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
        
    public DbSet<User> Users => Set<User>();
    public DbSet<ListaSeguimiento> ListasSeguimiento => Set<ListaSeguimiento>();
    public DbSet<Titulo> Titles => Set<Titulo>();
    public DbSet<ElementoLista> ElementosLista => Set<ElementoLista>();


    //------- ajustes importantes para las relaciones en bd entre entidades ----------//////

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aca se debe Evitar duplicados en la misma lista
        modelBuilder.Entity<ElementoLista>()
            .HasIndex(x => new { x.ListaSeguimientoId, x.TituloId })
            .IsUnique();

        // 🔁 Relaciones
        modelBuilder.Entity<User>()
            .HasMany(u => u.ListasSeguimiento)
            .WithOne(w => w.User!)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ListaSeguimiento>()
            .HasMany(w => w.ElementoLista)
            .WithOne(wi => wi.ListaSeguimiento!)
            .HasForeignKey(wi => wi.ListaSeguimientoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Titulo>()
            .HasMany(t => t.ElementosLista)
            .WithOne(wi => wi.Titulo!)
            .HasForeignKey(wi => wi.TituloId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}