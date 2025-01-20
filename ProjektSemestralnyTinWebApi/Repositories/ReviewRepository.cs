using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MasterContext _masterContext;
    private readonly IUserRepository _userRepository;
    private readonly ISummitRepository _summitRepository;

    public ReviewRepository(MasterContext masterContext, IUserRepository userRepository,
        ISummitRepository summitRepository)
    {
        _masterContext = masterContext;
        _userRepository = userRepository;
        _summitRepository = summitRepository;
    }

    public async Task<ICollection<ReviewOut>> GetReviewsBySummitIdAsync(int id)
    {
        var summit = await _masterContext.Summits.FirstOrDefaultAsync(s => s.Id == id);

        if (summit == null)
        {
            throw new Exception("Summit id not found");
        }

        var reviews = await _masterContext.Reviews.Where(r => r.SummitId == id).Select(r => new ReviewOut()
        {
            Id = r.Id,
            Title = r.Title,
            Body = r.Body,
            UserName = r.User.Login,
            Date = r.Date
        }).ToListAsync();
        return reviews;
    }

    public async Task AddReviewAsync(ReviewIn reviewIn)
    {
        await using var transaction = await _masterContext.Database.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetUserByAccessTokenAsync(reviewIn.AccessToken);
            var summit = await _summitRepository.GetSummitByIdAsync(reviewIn.SummitId);

            var review = new Review()
            {
                Title = reviewIn.Title,
                Body = reviewIn.Body,
                Summit = summit,
                User = user,
                Date = DateTime.Today
            };

            Console.WriteLine(
                $"Review details: Title={review.Title}, Body={review.Body}, SummitId={summit.Id}, UserId={user.Id}");

            await _masterContext.Reviews.AddAsync(review);
            await _masterContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
}