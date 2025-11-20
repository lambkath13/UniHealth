namespace UniHealth.Api.Models;
public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public List<PatientDisease> PatientDiseases { get; set; } = new();
}