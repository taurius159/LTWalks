using System.Runtime.InteropServices;
using Api.Models.Domains;

namespace Api.Repositories;
public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);

    Task<List<Walk>> GetAllAsync();
    Task<Walk> GetByIdAsync(Guid id);
}