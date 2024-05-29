using System.ComponentModel.DataAnnotations;
namespace Api.Models.DTOs;
public class UpdateRegionRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Minimum length of the code is 2")]
    [MaxLength(2, ErrorMessage = "Maximum length of the code is 2")]
    public string Code { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "Minimum length of the name is 5")]
    [MaxLength(25, ErrorMessage = "Maximum length of the name is 25")]
    public string Name { get; set; }
    
    public string? RegionImageUrl { get; set; }
}