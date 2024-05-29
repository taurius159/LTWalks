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

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await dbContext.Walks
            .Include(x => x.Region)
            .Include(x => x.Difficulty)
            .FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        // check if walk exists
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (existingWalk == null)
        {
            return null;
        }

        //map DTO to domain model and save in db
        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;

        await dbContext.SaveChangesAsync();
        return existingWalk;
    }
}