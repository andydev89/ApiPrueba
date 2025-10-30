using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Api.Dtos
{
    public class ElementoListaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public int? ListaSeguimientoId { get; set; }
        public int? TituloId { get; set; }
    }
}
