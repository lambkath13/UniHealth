namespace UniHealth.Api.Models;

public class PatientDisease
{
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public int DiseaseId { get; set; }
    public Disease Disease { get; set; } = null!;
}
