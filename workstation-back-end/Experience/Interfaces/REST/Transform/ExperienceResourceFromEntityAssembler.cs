using workstation_back_end.Experience.Domain.Models.Entities;
using workstation_back_end.Experience.Interfaces.REST.Resources;

namespace workstation_back_end.Experience.Interfaces.REST.Transform;

public static class ExperienceResourceFromEntityAssembler
{
    public static ExperienceResource ToResourceFromEntity(Domain.Models.Entities.Experience experience)
    {
        List<ExperienceImageResource> images = new List<ExperienceImageResource>();
        List<IncludeResource> includes = new List<IncludeResource>();
        List<ScheduleResource> schedule = new List<ScheduleResource>();

        foreach (var experienceImage in experience.ExperienceImages)
        {
            images.Add(new ExperienceImageResource(experienceImage.Url));
        }
        foreach (var experienceInclude in experience.Includes)
        {
            includes.Add(new IncludeResource(experienceInclude.Description));
        } 
        
        foreach (var experienceSchedule in experience.Schedules)
        {
            schedule.Add(new ScheduleResource(experienceSchedule.Time));
        } 
        
        
        return new ExperienceResource(experience.Id ,experience.Title, experience.Description, experience.Location, experience.Duration,experience.Price,
            experience.Frequencies, experience.Rating,  experience.CategoryId, images, includes, schedule);
    }
    
}