namespace tripmatch_back.Users.Domain.Models.Commands;


public record CreateAgenciaCommand(
    Guid UserId,
    string Ruc,
    string Descripcion,
    string LinkFacebook,
    string LinkInstagram
);