using Api.Data;
using Api.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;
public class InMemoryRegionRepository : IRegionRepository
{
    private readonly LTWalksDbContext dbContext;
    public InMemoryRegionRepository(LTWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllAsync()
    {
        return new List<Region>()
        {
            new Region()
            {
                Id = new Guid(),
                Name = "name",
                Code = "code"
            }
        };
    }
}