namespace ApiPrueba.src.Application.Dtos;

public class TituloResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = "pelicula";
    public int? Año { get; set; }
    public string? Genero { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string? ImagenUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
}