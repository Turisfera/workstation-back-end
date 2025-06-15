namespace tripmatch_back.Users.Domain.Models.Commands;


public record CreateTuristaCommand(
    Guid UserId,
    int Edad,
    string Genero,
    string Idioma,
    string Preferencias
);