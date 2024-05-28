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

    public Task<Region> CreateAsync(Region region)
    {
        throw new NotImplementedException();
    }

    public Task<Region?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
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

    public Task<Region?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Region?> UpdateAsync(Guid id, Region region)
    {
        throw new NotImplementedException();
    }
}