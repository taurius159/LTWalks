using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Models.Domains;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly LTWalksDbContext dbContext;
    public RegionsController(LTWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = dbContext.Regions.ToList();

        return Ok(regions);
    }
}
