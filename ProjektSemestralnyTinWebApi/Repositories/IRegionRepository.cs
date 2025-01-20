using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public interface IRegionRepository
{
    public Task<RegionOut> GetRegionAsync(int id);
    public Task<IEnumerable<RegionOut>> GetRegionsAsync();
    public Task DeleteRegionAsync(int id);
    public Task<Region> AddRegionAsync(RegionIn region);
    public Task<bool> RegionExistsAsync(int regionId);
}