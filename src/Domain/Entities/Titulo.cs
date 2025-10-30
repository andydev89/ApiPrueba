using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Domain.Entities;

public class Titulo : BaseEntity<int>
{
   [Required , MaxLength (200) ]
   public string Nombre { get; set; } = String.Empty; 
   public string Tipo { get; set; }  = "pelicula" ; /* ----->> Enum : pelicula | serie <<-------- */   
   public int? Año { get; set; }
   
   [MaxLength(100)]
   public string? Genero { get; set; }
   public string Descripcion { get; set; }

   [MaxLength(255)]
   public string? ImagenUrl { get; set;}
   public DateTime? CreatedAt { get; set; }

   //--------------Relaciones importantes entre entre entidades --------------//
   public ICollection<ElementoLista> ElementosLista { get; set; }

}
