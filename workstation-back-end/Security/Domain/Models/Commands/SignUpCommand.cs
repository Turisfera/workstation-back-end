namespace workstation_back_end.Security.Domain.Models.Commands;

/// <summary>
/// Comando para registrar un nuevo usuario (Turista o Agencia)
/// </summary>
public class SignUpCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Rol { get; set; } // "turista" o "agencia"

    // Solo para agencias
    public string? AgencyName { get; set; }
    public string? Ruc { get; set; }
}