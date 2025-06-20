using workstation_back_end.Shared.Domain.Model.Entities;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Inquiry.Domain.Models.Entities;

public class Inquiry : BaseEntity
{
    public int ExperienceId { get; set; }
    public Experience.Domain.Models.Entities.Experience Experience { get; set; }

    public Guid UserId { get; set; } 
    public Usuario Usuario { get; set; }

    public bool? IsAnswered { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }

    public DateTime? AskedAt { get; set; }
    public DateTime? AnsweredAt { get; set; }
}