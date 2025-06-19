namespace workstation_back_end.Users.Domain.Models.Commands;


public record CreateUsuarioCommand(
    string Nombres,
    string Apellidos,
    int Telefono,
    string Email,
    string Contrasena
);