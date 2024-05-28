using Api.Data;
using Api.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;
public class SQLRegionRepository : IRegionRepository
{
    private readonly LTWalksDbContext dbContext;
    public SQLRegionRepository(LTWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }
}