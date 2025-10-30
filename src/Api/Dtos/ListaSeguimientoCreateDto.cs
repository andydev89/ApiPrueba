using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Api.Dtos;

public class ListaSeguimientoCreateDto
{
    [Required]
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; } = string.Empty;
}