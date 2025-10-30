using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.src.Domain.Entities;

public class User : BaseEntity<int>
{  

    [Required , MaxLength (150) ]
    public string Email { get; set; } = String.Empty;
    [Required , MaxLength(100) ]
    public string Name { get; set; } = String.Empty;
    [Required , MaxLength(255)]
    public string Password{ get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<ListaSeguimiento> ListasSeguimiento { get; set; } = new List<ListaSeguimiento>();
    public string Role { get; set; } = "user"; 
} 
