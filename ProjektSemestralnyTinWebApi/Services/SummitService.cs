using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Repositories;

namespace ProjektSemestralnyTinWebApi.Services;

public class SummitService(ISummitRepository summitRepository) : ISummitService
{
    private readonly ISummitRepository _summitRepository = summitRepository;

    public async Task<SummitDetails> GetSummitDetailByIdAsync(int id)
    {
        return await _summitRepository.GetSummitDetailByIdAsync(id);
    }

    public async Task<IEnumerable<SummitInfo>> GetAllSummitsInfoAsync()
    {
        return await _summitRepository.GetAllSummitsInfoAsync();
    }

    public async Task AddSummitAsync(SummitIn summitIn)
    {
        await _summitRepository.AddSummitAsync(summitIn);
    }

    public async Task DeleteSummitAsync(int id)
    {
        await _summitRepository.DeleteSummitAsync(id);
    }

    public async Task<IEnumerable<SummitInfo>> GetAllSummitsInfoByRegionAsync(int regionId)
    {
        return await _summitRepository.GetAllSummitsInfoByRegionAsync(regionId);
    }

    public async Task UpdateSummitAsync(SummitUpdateIn summitUpdateIn)
    {
        await _summitRepository.UpdateSummitAsync(summitUpdateIn);
    }

    public async Task<SummitUpdateOut> GetSummitUpdateDetailsAsync(int id)
    {
        return await _summitRepository.GetSummitUpdateDetailsAsync(id);
    }
}