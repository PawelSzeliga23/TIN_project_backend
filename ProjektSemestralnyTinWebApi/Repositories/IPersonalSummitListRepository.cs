using ProjektSemestralnyTinWebApi.DTOs;

namespace ProjektSemestralnyTinWebApi.Repositories;

public interface IPersonalSummitListRepository
{
    public Task<ICollection<SummitInfo>> GetSummitInfoFromPersonalListAsync(string accessToken);
    public Task AddSummitToListAsync(PersonalSummitListIn model);
    public Task RemoveSummitFromListAsync(PersonalSummitListIn model);
}