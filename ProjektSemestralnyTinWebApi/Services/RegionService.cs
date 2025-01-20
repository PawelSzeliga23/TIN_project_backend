using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;
using ProjektSemestralnyTinWebApi.Repositories;

namespace ProjektSemestralnyTinWebApi.Services;

public class RegionService(IRegionRepository regionRepository) : IRegionService
{
    private readonly IRegionRepository _regionRepository = regionRepository;

    public async Task<RegionOut> GetRegionAsync(int id)
    {
        return await _regionRepository.GetRegionAsync(id);
    }

    public async Task<IEnumerable<RegionOut>> GetRegionsAsync()
    {
        return await _regionRepository.GetRegionsAsync();
    }

    public async Task DeleteRegionAsync(int id)
    {
        await _regionRepository.DeleteRegionAsync(id);
    }

    public async Task<Region> AddRegionAsync(RegionIn region)
    {
        return await _regionRepository.AddRegionAsync(region);
    }
}