namespace workstation_back_end.Users.Domain.Models.Commands;



public record CreateUsuarioCommand(
    string FirstName,
    string LastName,
    string Number,
    string Email,
    string Password,

    // Rol puede ser "agencia" o "turista"
    string Rol,

    // Datos opcionales para Agencia
    string? AgencyName,
    string? Ruc,
    string? Description,
    string? ContactEmail,
    string? ContactPhone,
    string? SocialLinkFacebook,
    string? SocialLinkInstagram,
    string? SocialLinkWhatsapp,

    // Datos opcionales para Turista
    string? AvatarUrl
);
