namespace turisfera_workstation_back_end.Experiences.Domain.Models.Entities;

public class Experience
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int DurationInHours { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}