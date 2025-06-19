namespace workstation_back_end.Users.Interfaces.REST.Resources;


public class TuristaResource
{
    public Guid UserId { get; set; }
    public int Edad { get; set; }
    public string Genero { get; set; }
    public string Idioma { get; set; }
    public string Preferencias { get; set; }
}