using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Api.Dtos;

public class ElementoListaCreateDto
{
    [Required]
    public int ListaSeguimientoId { get; set; }

    [Required]
    public int TituloId { get; set; }
}
