using ProjektSemestralnyTinWebApi.DTOs;

namespace ProjektSemestralnyTinWebApi.Services;

public interface IPersonalSummitListService
{
    public Task<ICollection<SummitInfo>> GetSummitInfoFromPersonalListAsync(string accessToken);
    public Task AddSummitToListAsync(PersonalSummitListIn model);
    public Task RemoveSummitFromListAsync(PersonalSummitListIn model);
}