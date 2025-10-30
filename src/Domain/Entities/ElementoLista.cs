namespace ApiPrueba.src.Domain.Entities;

public class ElementoLista : BaseEntity<int>
{
    public int ListaSeguimientoId { get; set; }

    public int TituloId { get; set;}

    public DateTime AddAt { get; set;} = DateTime.Now;

    ////-----------------Relaciones importartantes con otras Entidades-----------//
    public ListaSeguimiento? ListaSeguimiento { get; set; }
    public Titulo? Titulo { get; set;}
}
