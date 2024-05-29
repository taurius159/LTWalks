using System.Runtime.InteropServices;
using Api.Models.Domains;

namespace Api.Repositories;
public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);

    Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
    string? sortBy = null, bool isAscending = true);
    Task<Walk?> GetByIdAsync(Guid id);
    Task<Walk?> UpdateAsync(Guid id, Walk walk);
    Task<Walk?> DeleteAsync(Guid id);
}