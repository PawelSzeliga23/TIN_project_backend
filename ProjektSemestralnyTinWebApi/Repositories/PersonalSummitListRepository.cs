using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public class PersonalSummitListRepository : IPersonalSummitListRepository
{
    private readonly MasterContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ISummitRepository _summitRepository;

    public PersonalSummitListRepository(MasterContext context, IUserRepository userRepository,
        ISummitRepository summitRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _summitRepository = summitRepository;
    }

    public async Task<ICollection<SummitInfo>> GetSummitInfoFromPersonalListAsync(string accessToken)
    {
        var user = await _userRepository.GetUserByAccessTokenAsync(accessToken);
        if (user == null)
        {
            throw new Exception($"User not found");
        }

        var summits = await _context.PersonalSummitLists
            .Where(ps => ps.User == user)
            .Include(ps => ps.Summit)
            .ThenInclude(s => s.SummitsImages)
            .Select(
                ps => new SummitInfo()
                {
                    Id = ps.Summit.Id,
                    Name = ps.Summit.Name,
                    Height = ps.Summit.Height,
                    Images = ps.Summit.SummitsImages.Select(img => new SummitImageOut
                    {
                        Id = img.Id,
                        ImageUrl = img.ImageUrl,
                        NameEn = img.NameEn,
                        NamePl = img.NamePl,
                    }).ToList()
                }).ToListAsync();

        return summits;
    }

    public async Task AddSummitToListAsync(PersonalSummitListIn model)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetUserByAccessTokenAsync(model.AccessToken);
            if (user == null)
            {
                throw new Exception($"User not found");
            }

            var summit = await _summitRepository.GetSummitByIdAsync(model.SummitId);
            if (summit == null)
            {
                throw new Exception($"Summit not found");
            }

            var personalSummit = new PersonalSummitList()
            {
                Summit = summit,
                User = user
            };

            await _context.PersonalSummitLists.AddAsync(personalSummit);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveSummitFromListAsync(PersonalSummitListIn model)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetUserByAccessTokenAsync(model.AccessToken);
            if (user == null)
            {
                throw new Exception($"User not found");
            }

            var summit = await _summitRepository.GetSummitByIdAsync(model.SummitId);
            if (summit == null)
            {
                throw new Exception($"Summit not found");
            }

            var personalSummit =
                await _context.PersonalSummitLists.FirstOrDefaultAsync(ps => ps.Summit == summit && ps.User == user);
            if (personalSummit == null)
            {
                throw new Exception($"PersonalSummit not found");
            }

            _context.PersonalSummitLists.Remove(personalSummit);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}