namespace UniHealth.Api.Models;

public class Disease
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public List<PatientDisease> PatientDiseases { get; set; } = new();
}
