namespace workstation_back_end.Users.Domain.Models.Commands;


public record CreateAgenciaCommand(
    Guid UserId,
    string Ruc,
    string Descripcion,
    string LinkFacebook,
    string LinkInstagram
);