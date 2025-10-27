using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}
