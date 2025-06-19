using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Users.Domain.Models.Entities;


public class Agencia : BaseEntity
{
    public Guid UserId { get; set; }       // FK → Usuario

    public string Ruc { get; set; }
    public string Descripcion { get; set; }
    public string LinkFacebook { get; set; }
    public string LinkInstagram { get; set; }

    public Usuario Usuario { get; set; }
}