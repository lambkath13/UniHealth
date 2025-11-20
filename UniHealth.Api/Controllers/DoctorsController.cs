using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniHealth.Api.Data;
using UniHealth.Api.Models;

namespace UniHealth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DoctorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetBySpecialization(
        [FromQuery] string? specialization)
    {
        var query = _context.Doctors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(specialization))
            query = query.Where(d => d.Specialization == specialization);

        var doctors = await query.ToListAsync();
        return Ok(doctors);
    }
}
