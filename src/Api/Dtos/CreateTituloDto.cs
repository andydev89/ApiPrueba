using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Api.Dtos;

public class TituloCreateDto
{
    [Required, MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [Required, RegularExpression("pelicula|serie", ErrorMessage = "El tipo debe ser 'pelicula' o 'serie'.")]
    public string Tipo { get; set; } = "pelicula";

    [Range(1900, 2100)]
    public int? Año { get; set; }

    [MaxLength(100)]
    public string? Genero { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? ImagenUrl { get; set; }
}
