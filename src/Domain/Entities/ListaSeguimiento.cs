using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPrueba.src.Domain.Entities;

public class ListaSeguimiento : BaseEntity<int>
{
    [Required]
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = String.Empty;

    public string? Description {  get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //-------------Relaciones importantes entre y con otras entidades--------------------------//
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; } 

    public ICollection<ElementoLista> ElementoLista { get; set; } = new List<ElementoLista>();
}
