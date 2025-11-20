using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniHealth.Api.Data;
using UniHealth.Api.Models;

namespace UniHealth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PatientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
    {
        var patients = await _context.Patients
            .Include(p => p.Doctor)
            .Include(p => p.PatientDiseases)
                .ThenInclude(pd => pd.Disease)
            .ToListAsync();

        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Patient>> GetById(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Doctor)
            .Include(p => p.PatientDiseases)
                .ThenInclude(pd => pd.Disease)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient is null)
            return NotFound();

        return Ok(patient);
    }
}
