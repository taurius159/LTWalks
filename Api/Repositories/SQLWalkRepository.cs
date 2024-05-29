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

    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
    string? sortBy = null, bool isAscending = true,
    int pageNumber = 1, int pageSize = 1000)
    {
        var walks = dbContext.Walks.Include(x => x.Difficulty)
            .Include(x => x.Region)
            .AsQueryable();

        // Filtering
        if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        // Sorting
        if(string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
        }

        // Pagination
        var skipResults = (pageNumber-1) * pageSize;

        return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        // check if walks exists
        var walkDomainModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkDomainModel == null)
        {
            return null;
        }

        //delete region
        dbContext.Walks.Remove(walkDomainModel);
        await dbContext.SaveChangesAsync();
        return walkDomainModel;
    }
}