using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Repositories;

namespace ProjektSemestralnyTinWebApi.Services;

public class PersonalSummitListService : IPersonalSummitListService
{
    private readonly IPersonalSummitListRepository _personalSummitListRepository;

    public PersonalSummitListService(IPersonalSummitListRepository personalSummitListRepository)
    {
        _personalSummitListRepository = personalSummitListRepository;
    }

    public async Task<ICollection<SummitInfo>> GetSummitInfoFromPersonalListAsync(string accessToken)
    {
        return await _personalSummitListRepository.GetSummitInfoFromPersonalListAsync(accessToken);
    }

    public async Task AddSummitToListAsync(PersonalSummitListIn model)
    {
        await _personalSummitListRepository.AddSummitToListAsync(model);
    }

    public async Task RemoveSummitFromListAsync(PersonalSummitListIn model)
    {
        await _personalSummitListRepository.RemoveSummitFromListAsync(model);
    }
}