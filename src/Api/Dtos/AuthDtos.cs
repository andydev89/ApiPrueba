namespace ApiPrueba.src.Application.DTOs;

public class AuthDtos
{
    public record LoginRequest(string Email, string Password);
    public record TokenResponse(string Token, string Email, string Role, int Id, string Nombre);
}
