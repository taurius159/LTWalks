using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs;
public class LoginRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }
}