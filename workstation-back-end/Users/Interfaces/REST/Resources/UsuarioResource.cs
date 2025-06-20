namespace workstation_back_end.Users.Interfaces.REST.Resources;


public class UsuarioResource
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }  // Renombrado desde Nombres
    public string LastName { get; set; }   // Renombrado desde Apellidos
    public string Number { get; set; }
    public string Email { get; set; }

    public bool EsAgencia { get; set; }
    public bool EsTurista { get; set; }
}