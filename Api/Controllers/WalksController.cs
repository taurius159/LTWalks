using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Models.Domains;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Api.Models.DTOs;
using Api.Repositories;
using AutoMapper;

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
    public async Task<IActionResult> GetAll()
    {
        var walksDomainModels = await walkRepository.GetAllAsync();

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
}
