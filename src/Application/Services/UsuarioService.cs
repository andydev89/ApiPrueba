using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Exeptions;
using ApiPrueba.src.Domain.Repositories;

namespace ApiPrueba.src.Application.Services
{
    public class UsuarioService : Service<User, int>, IUsuarioService
    {
        private readonly IRepository<User, int> _repository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IRepository<User, int> repository, ILogger<UsuarioService> logger)
            : base(repository, logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try {
                var users = await _repository.ListAsync(u => u.Email == email);
                return users.FirstOrDefault() ?? throw new NotFoundException("Usuario no encontrado");
            } catch(Exception e) {
                _logger.LogError(e, "Error al obtener usuario por email", e.Message);                
                throw new Exception("Error interno al obtener usuario", e);
            }
        }         

        
    }
}
