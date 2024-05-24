using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Models.Domains;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = new List<Region>{
            new Region{
                Id = new Guid(),
                Name = "Dzukija",
                Code = "DZ",
            },
            new Region{
                Id = new Guid(),
                Name = "Aukstaitija",
                Code = "AT",
            }
        };

        return Ok(regions);
    }
}
