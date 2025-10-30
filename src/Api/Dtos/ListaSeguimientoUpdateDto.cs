using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Api.Dtos;

public class ListaSeguimientoUpdateDto
{
    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }
}
