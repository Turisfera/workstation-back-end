using tripmatch_back.Shared.Domain.Model.Entities;

namespace tripmatch_back.Users.Domain.Models.Entities;


public class Agencia : BaseEntity
{
    public Guid UserId { get; set; }       // FK → Usuario

    public string Ruc { get; set; }
    public string Descripcion { get; set; }
    public string LinkFacebook { get; set; }
    public string LinkInstagram { get; set; }

    public Usuario Usuario { get; set; }
}