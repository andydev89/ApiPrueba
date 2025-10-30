using ApiPrueba.src.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace ApiPrueba.src.Api.Dtos;

public class UsuarioDto
{
    public string Email { get; set; } = String.Empty;
    [Required, MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    [Required, MaxLength(255)]
    public string Password { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
  
    public string Role { get; set; } = "user";
}

public class UsuarioUpdateDto
{
    [MaxLength(150)]
    public string? Email { get; set; } = String.Empty;
    [MaxLength(100)]
    public string? Name { get; set; } = String.Empty;
    
    public string? Password { get; set; } = default!;

    public string? Role { get; set; }
}
