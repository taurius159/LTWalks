using Api.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class LTWalksDbContext : DbContext
{
    public LTWalksDbContext(DbContextOptions<LTWalksDbContext> dbContextOptions) :base(dbContextOptions)
    {
        
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //seed difficulties
        //easy, medium, hard
        var difficulties = new List<Difficulty>()
        {
            new Difficulty
            {
                Id = Guid.Parse("6ac93600-60dc-4645-a943-e6efad116d2a"),
                Name = "easy"
            },
            new Difficulty
            {
                Id = Guid.Parse("4b1c85d7-57fb-48c4-9917-6e0f0823ceed"),
                Name = "medium"
            },
            new Difficulty
            {
                Id = Guid.Parse("c25afd7a-ba3b-46bb-b99e-aa862ad29ca6"),
                Name = "hard"
            }
        };

        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        //seed regions
        var regions = new List<Region>()
        {
            new Region
            {
                Id = Guid.Parse("72dcadb4-7947-4ce9-a154-89ab44d075e3"),
                Name = "Dzukija",
                Code = "DZ",
                RegionImageUrl = ""
            }
        };

        modelBuilder.Entity<Region>().HasData(regions);
    }
}