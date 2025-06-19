namespace workstation_back_end.Users.Interfaces.REST.Resources;


public class UsuarioResource
{
    public Guid UserId { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public int Telefono { get; set; }
    public string Email { get; set; }
}