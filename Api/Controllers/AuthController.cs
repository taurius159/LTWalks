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
    private readonly ITokenRepository tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;
    }
    
    // POST: /api/Auth/Register
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
    // POST: /api/Auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await userManager.FindByEmailAsync(loginRequestDto.username);

        if(user != null)
        {
            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.password);
            
            if(checkPasswordResult)
            {
                // Get roles for this user
                var roles = await userManager.GetRolesAsync(user);

                if(roles != null)
                {
                    //Create token
                    var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                    var response = new LoginResponseDTO
                    {
                        JwtToken = jwtToken
                    };

                    return Ok(response);
                }
            }
        }

        return BadRequest("username or password was incorrect.");
    }
}
