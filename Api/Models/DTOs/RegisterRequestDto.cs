using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs;
public class RegisterRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }
    public string[] Roles { get; set; }
}