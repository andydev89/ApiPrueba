using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Repositories;
using static ApiPrueba.src.Application.DTOs.AuthDtos;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ApiPrueba.src.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly string _jwtKey;
        public AuthService(IUserRepository users, IConfiguration cfg)
        {
            _users = users;
            _jwtKey = cfg["Jwt:Key"] ?? "v3ry_long_local_dev_secret_key_please_change_1234567890!!";
        }
        public async Task<TokenResponse?> LoginAsync(LoginRequest req)
        {
            var user = await _users.GetByEmailAsync(req.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash)) return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse(jwt, user.Email, user.Role);
        }
    }
}
