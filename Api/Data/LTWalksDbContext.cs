using Api.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class LTWalksDbContext : DbContext
{
    public LTWalksDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
    {
        
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}