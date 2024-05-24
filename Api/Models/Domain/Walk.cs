namespace Api.Models.Domains;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }

    // Navigation properties
    public Difficulty Difficulty { get; set; } // 1-1 relationship
    public Region Region { get; set; } // 1-1 relationship
}
