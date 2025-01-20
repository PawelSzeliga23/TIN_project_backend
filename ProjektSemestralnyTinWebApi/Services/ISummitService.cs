using ProjektSemestralnyTinWebApi.DTOs;

namespace ProjektSemestralnyTinWebApi.Services;

public interface ISummitService
{
    public Task<SummitDetails> GetSummitDetailByIdAsync(int id);
    public Task<IEnumerable<SummitInfo>> GetAllSummitsInfoAsync();
    public Task AddSummitAsync(SummitIn summitIn);
    public Task DeleteSummitAsync(int id);
    public Task<IEnumerable<SummitInfo>> GetAllSummitsInfoByRegionAsync(int regionId);
    public Task UpdateSummitAsync(SummitUpdateIn summitUpdateIn);
    public Task<SummitUpdateOut> GetSummitUpdateDetailsAsync(int id);
}