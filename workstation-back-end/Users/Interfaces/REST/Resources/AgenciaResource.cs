namespace workstation_back_end.Users.Interfaces.REST.Resources;


public class AgenciaResource
{
    public Guid UserId { get; set; }
    public string Ruc { get; set; }
    public string Descripcion { get; set; }
    public string? LinkFacebook { get; set; }
    public string? LinkInstagram { get; set; }
}