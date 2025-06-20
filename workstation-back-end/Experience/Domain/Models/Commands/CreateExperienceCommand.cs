namespace workstation_back_end.Experience.Domain.Models.Commands;

public record CreateExperienceCommand(    
    string Title,
    string Description,
    string Location,
    int Duration,
    decimal Price,
    string Frequencies,
    decimal Rating,
    int CategoryId,
    List<ImageCommand> ExperienceImages,
    List<IncludeCommand> Includes, 
    List<ScheduleCommand> Schedules)
{
    
}