namespace UniHealth.Api.Models;
public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Specialization { get; set; } = null!;

    public List<Patient> Patients { get; set; } = new();
}