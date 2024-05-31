using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Models.Domains;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Api.Models.DTOs;
using Api.Repositories;
using AutoMapper;
using api.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly LTWalksDbContext dbContext;
    private readonly IRegionRepository regionRepository;
    private readonly IMapper mapper;
    private readonly ILogger<RegionsController> logger;

    public RegionsController(LTWalksDbContext dbContext, 
    IRegionRepository regionRepository, 
    IMapper mapper,
    ILogger<RegionsController> logger)
    {
        this.dbContext = dbContext;
        this.regionRepository = regionRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("GetAllMethod was called");

        throw new Exception("This is a custom exception");

        // Get data from database - domain models
        var regionsDomain = await regionRepository.GetAllAsync();

        var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);
        // Map Domain Models to DTO
        // var regionsDto = new List<RegionDTO>();
        // foreach(var regionDomain in regionsDomain)
        // {
        //     regionsDto.Add(new RegionDTO()
        //     {
        //         Id = regionDomain.Id,
        //         Code = regionDomain.Code,
        //         Name = regionDomain.Name,
        //         RegionImageUrl = regionDomain.RegionImageUrl
        //     });
        // }

        // Return DTOs
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader, Writer")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var regionDomain = await regionRepository.GetByIdAsync(id);

        if (regionDomain == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<Region>(regionDomain));
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // map or convert DTO to Domain Model
        var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

        // use domain model to create Region
        regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

        // map domain model to DTO
        var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

        return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
    }
    
    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        //map DTO to domain model for passing to repository for updating
        var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
        
        // check if region exists
        regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

        if (regionDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // check if region exists
        var regionDomainModel = await regionRepository.DeleteAsync(id);

        if(regionDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

        return Ok(regionDto);
    }
}
