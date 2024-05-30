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
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }
    
    // POST: /api/auth/register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequestDto.username,
            Email = registerRequestDto.username
        };
        var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);
    
        if (identityResult.Succeeded)
        {
            //Add roles to this user
            if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                if(identityResult.Succeeded)
                {
                    return Ok("User was created. Please login.");
                }
            }
        }

        return BadRequest("Something went wrong");
    }
}
