using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public interface ISummitRepository
{
    public Task<SummitDetails> GetSummitDetailByIdAsync(int id);
    public Task<IEnumerable<SummitInfo>> GetAllSummitsInfoAsync();
    public Task AddSummitAsync(SummitIn summitIn);
    public Task DeleteSummitAsync(int id);
    public Task<IEnumerable<SummitInfo>> GetAllSummitsInfoByRegionAsync(int regionId);
    public Task<Summit> GetSummitByIdAsync(int id);
    public Task UpdateSummitAsync(SummitUpdateIn summitUpdateIn);
    public Task<SummitUpdateOut> GetSummitUpdateDetailsAsync(int id);
}