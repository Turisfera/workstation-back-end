namespace workstation_back_end.Users.Domain.Models.Commands;


public record CreateTuristaCommand(
    Guid UserId,
    int Edad,
    string Genero,
    string Idioma,
    string Preferencias
);