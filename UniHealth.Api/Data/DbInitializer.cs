using UniHealth.Api.Models;

namespace UniHealth.Api.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Patients.Any())
            return;

        var diseases = new[]
        {
            new Disease { Name = "Грипп", Description = "Вирусная инфекция" },
            new Disease { Name = "Диабет", Description = "Хроническое заболевание" },
            new Disease { Name = "Гипертония", Description = "Повышенное давление" },
        };
        context.Diseases.AddRange(diseases);

        var doctors = new[]
        {
            new Doctor { FullName = "Иванов Иван Иванович", Specialization = "Терапевт" },
            new Doctor { FullName = "Петров Пётр Петрович", Specialization = "Кардиолог" },
        };
        context.Doctors.AddRange(doctors);
        context.SaveChanges();

        var patients = new[]
        {
            new Patient
            {
                FullName = "Сидоров Сидор Сидорович",
                BirthDate = new DateTime(1990, 1, 1),
                DoctorId = doctors[0].Id
            },
            new Patient
            {
                FullName = "Анна Смирнова",
                BirthDate = new DateTime(1985, 5, 10),
                DoctorId = doctors[1].Id
            }
        };
        context.Patients.AddRange(patients);
        context.SaveChanges();

        var links = new[]
        {
            new PatientDisease { PatientId = patients[0].Id, DiseaseId = diseases[0].Id },
            new PatientDisease { PatientId = patients[1].Id, DiseaseId = diseases[1].Id },
            new PatientDisease { PatientId = patients[1].Id, DiseaseId = diseases[2].Id },
        };
        context.PatientDiseases.AddRange(links);

        context.SaveChanges();
    }
}
