using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Models.Domains;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Api.Models.DTOs;

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
    public async Task<IActionResult> GetAll()
    {
        // Get data from database - domain models
        var regionsDomain = await dbContext.Regions.ToListAsync();

        // Map Domain Models to DTO
        var regionsDto = new List<RegionDTO>();
        foreach(var regionDomain in regionsDomain)
        {
            regionsDto.Add(new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            });
        }

        // Return DTOs
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // Get Region from Database
        //var region = dbContext.Regions.Find(id); //find using primary key
        var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); //use LINQ

        if (regionDomain == null)
        {
            return NotFound();
        }

        // Map Region Domain Model to Region DTO
        var regionDTO = new RegionDTO()
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            RegionImageUrl = regionDomain.RegionImageUrl
        };

        return Ok(regionDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // map or convert DTO to Domain Model
        var regionDomainModel = new Region()
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };

        // use domain model to create Region
        await dbContext.Regions.AddAsync(regionDomainModel);
        await dbContext.SaveChangesAsync();

        // map domain model to DTO
        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
    }
    
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // check if region exists
        var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionDomainModel == null)
        {
            return NotFound();
        }

        //map DTO to domain model and save in db
        regionDomainModel.Code = updateRegionRequestDto.Code;
        regionDomainModel.Name = updateRegionRequestDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

        await dbContext.SaveChangesAsync();

        //convert domain model to DTO
        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // check if region exists
        var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionDomainModel == null)
        {
            return NotFound();
        }

        //delete region
        dbContext.Regions.Remove(regionDomainModel);
        await dbContext.SaveChangesAsync();

        //convert domain model to DTO
        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);
    }
}
