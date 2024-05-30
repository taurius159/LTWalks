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
public class ImagesController : ControllerBase
{

    public ImagesController()
    {

    }
    
    // POST: /api/Images/Upload
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromBody] ImageUploadRequestDto request)
    {
        ValidateFileUpload(request)

        if (ModelState.IsValid)
        {
            //User repository to add image
        }

        return BadRequest(ModelState);
    }

    private void ValidateFileUpload(ImageUploadRequestDto request)
    {
        //extensions
        var allowedExtensions = new string[] {".jpg", ".jpeg", ".png"};
        if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
        {
            ModelState.AddModelError("file", $"Allowed file extensions are: {allowedExtensions}.");
        }

        //size of file
        if(request.File.Length > 10485760) //10mb in bytes
        {
            ModelState.AddModelError("file", "File size more than 10mb. Please upload acceptable size file.");
        }
    }
}
