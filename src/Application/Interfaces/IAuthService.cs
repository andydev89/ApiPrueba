using static ApiPrueba.src.Application.DTOs.AuthDtos;

namespace ApiPrueba.src.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponse?> LoginAsync(LoginRequest request);

}
