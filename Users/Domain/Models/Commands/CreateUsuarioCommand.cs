namespace tripmatch_back.Users.Domain.Models.Commands;


public record CreateUsuarioCommand(
    string Nombres,
    string Apellidos,
    int Telefono,
    string Email,
    string Contrasena
);