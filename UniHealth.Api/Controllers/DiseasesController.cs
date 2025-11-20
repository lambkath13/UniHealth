using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniHealth.Api.Data;
using UniHealth.Api.Models;

namespace UniHealth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiseasesController : ControllerBase
{
    private readonly AppDbContext _context;

    public DiseasesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Disease>>> GetAll()
    {
        var diseases = await _context.Diseases.ToListAsync();
        return Ok(diseases);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Disease dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var disease = await _context.Diseases.FindAsync(id);
        if (disease is null)
            return NotFound();

        disease.Name = dto.Name;
        disease.Description = dto.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
