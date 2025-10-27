using ApiPrueba.src.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ApiPrueba.src.Application.DTOs.AuthDtos;

namespace ApiPrueba.src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var token = await _auth.LoginAsync(req);
        return token is null ? Unauthorized() : Ok(token);
    }
}
