using Api.Data;
using Api.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;
public class SQLWalkRepository : IWalkRepository
{
    private readonly LTWalksDbContext dbContext;
    public SQLWalkRepository(LTWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walkDomainModel)
    {
        await dbContext.Walks.AddAsync(walkDomainModel);
        await dbContext.SaveChangesAsync();

        return walkDomainModel;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await dbContext.Walks.Include(x => x.Difficulty)
        .Include(x => x.Region)
        .ToListAsync();
    }
}