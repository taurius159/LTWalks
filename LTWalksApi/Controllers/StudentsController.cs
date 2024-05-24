using Microsoft.AspNetCore.Mvc;

namespace LTWalksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        return Ok("success");
    }
}
