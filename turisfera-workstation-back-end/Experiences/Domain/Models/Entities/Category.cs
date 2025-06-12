namespace turisfera_workstation_back_end.Experiences.Domain.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Experience> Experiences { get; set; }
}