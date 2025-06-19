namespace workstation_back_end.Security.Domain.Models.Commands;

/// <summary>
/// Comando para registrar un nuevo usuario (Turista o Agencia)
/// </summary>
public class SignUpCommand
{
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public int Telefono { get; set; }
    public string Email { get; set; }
    public string Contrasena { get; set; }

    // Tipo de usuario a registrar: "turista" o "agencia"
    public string Rol { get; set; }
}