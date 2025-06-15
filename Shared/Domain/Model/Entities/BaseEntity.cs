namespace tripmatch_back.Shared.Domain.Model.Entities;


public class BaseEntity
{
    public int Id { get; set; }                     // Clave primaria interna (puede convivir con UUIDs)
    public DateTime CreatedDate { get; set; }       // Fecha de creación
    public DateTime? ModifiedDate { get; set; }     // Fecha de modificación (opcional)

    public int UserId { get; set; }                 // Usuario que creó el recurso
    public int? UpdatedUserId { get; set; }         // Usuario que modificó por última vez (opcional)
    public bool IsActive { get; set; }              // Estado lógico
}