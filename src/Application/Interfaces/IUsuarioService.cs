using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Application.Interfaces
{
    public interface IUsuarioService : IService<User, int>
    {
      Task<User> GetByEmailAsync(string email);
      
    }
}
