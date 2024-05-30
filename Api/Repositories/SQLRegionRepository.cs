using Api.Controllers;
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

    public async Task<Region> CreateAsync(Region regionDomainModel)
    {
        await dbContext.Regions.AddAsync(regionDomainModel);
        await dbContext.SaveChangesAsync();

        return regionDomainModel;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        // check if region exists
        var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionDomainModel == null)
        {
            return null;
        }

        //delete region
        dbContext.Regions.Remove(regionDomainModel);
        await dbContext.SaveChangesAsync();
        return regionDomainModel;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        // Get Region from Database
        //var region = dbContext.Regions.Find(id); //find using primary key
        return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }


    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        // check if region exists
        var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (existingRegion == null)
        {
            return null;
        }

        //map DTO to domain model and save in db
        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await dbContext.SaveChangesAsync();
        return existingRegion;
    }
}