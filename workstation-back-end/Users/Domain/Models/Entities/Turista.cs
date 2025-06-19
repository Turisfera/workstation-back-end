using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Users.Domain.Models.Entities;


public class Turista : BaseEntity
{
    public Guid UserId { get; set; }       // FK → Usuario

    public int Edad { get; set; }
    public string Genero { get; set; }
    public string Idioma { get; set; }
    public string Preferencias { get; set; }

    public Usuario Usuario { get; set; }
}