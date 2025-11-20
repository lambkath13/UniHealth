using Microsoft.EntityFrameworkCore;
using UniHealth.Api.Models;

namespace UniHealth.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Disease> Diseases => Set<Disease>();
    public DbSet<PatientDisease> PatientDiseases => Set<PatientDisease>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PatientDisease>()
            .HasKey(pd => new { pd.PatientId, pd.DiseaseId });

        modelBuilder.Entity<PatientDisease>()
            .HasOne(pd => pd.Patient)
            .WithMany(p => p.PatientDiseases)
            .HasForeignKey(pd => pd.PatientId);

        modelBuilder.Entity<PatientDisease>()
            .HasOne(pd => pd.Disease)
            .WithMany(d => d.PatientDiseases)
            .HasForeignKey(pd => pd.DiseaseId);
    }
}
