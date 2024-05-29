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

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IWalkRepository walkRepository;
    private readonly IMapper mapper;

    public WalksController(IWalkRepository walkRepository, IMapper mapper)
    {
        this.walkRepository = walkRepository;
        this.mapper = mapper;
    }


    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // map or convert DTO to Domain Model
        var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

        // use domain model to create Walk
        walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

        // map domain model to DTO
        var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

        return CreatedAtAction(nameof(GetById), new {id = walkDTO.Id}, walkDTO);
    }

    [HttpGet]
    // GET: Walks
    // GET: api/Walks?filterOn=name&filterQuery=mount&sortBy=name&isAscending=true
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
    [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
    {
        var walksDomainModels = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);

        //return map domain model to DTO
        return Ok(mapper.Map<List<WalkDTO>>(walksDomainModels));
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = await walkRepository.GetByIdAsync(id);

        if(walkDomainModel == null)
        {
            return NotFound();
        }

        //map domain model to DTO and return
        return Ok(mapper.Map<WalkDTO>(walkDomainModel));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
        //map DTO to domain model
        var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

        // attempt to update and check if walk exists as a consequence
        walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

        if (walkDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

        return Ok(walkDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // attempt to delete the walk and consequently know if walk exists
        var walkDomainModel = await walkRepository.DeleteAsync(id);

        if(walkDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

        return Ok(walkDto);
    }
}
