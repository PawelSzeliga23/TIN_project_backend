using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Services;

public interface IRegionService
{
    public Task<RegionOut> GetRegionAsync(int id);
    public Task<IEnumerable<RegionOut>> GetRegionsAsync();
    public Task DeleteRegionAsync(int id);
    public Task<Region> AddRegionAsync(RegionIn region);
}