using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByEmailAsync(string email) =>
        _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    public Task AddAsync(User user) => _db.Users.AddAsync(user).AsTask();
}
