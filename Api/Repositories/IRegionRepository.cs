using Api.Models.Domains;

namespace Api.Repositories;
public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    
}