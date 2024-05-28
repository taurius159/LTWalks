using System.Runtime.InteropServices;
using Api.Models.Domains;

namespace Api.Repositories;
public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetByIdAsync(Guid id);
    
    Task<Region> CreateAsync(Region region);

    Task<Region?> UpdateAsync(Guid id, Region region);
    Task<Region?> DeleteAsync(Guid id);
}