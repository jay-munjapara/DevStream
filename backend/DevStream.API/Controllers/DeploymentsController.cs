using DevStream.API.Data;
using DevStream.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStream.API.Controllers;

[Authorize]
[ApiController]
[Route("api/deployments")]
public class DeploymentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public DeploymentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Deployment>>> GetAll()
    {
        return await _db.Deployments
            .OrderByDescending(d => d.CreatedAtUtc)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Deployment>> Create([FromBody] Deployment deployment)
    {
        deployment.Id = 0;
        deployment.CreatedAtUtc = DateTime.UtcNow;
        _db.Deployments.Add(deployment);
        await _db.SaveChangesAsync();
        return Ok(deployment);
    }
}