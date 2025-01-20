using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Repositories;

namespace ProjektSemestralnyTinWebApi.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ICollection<ReviewOut>> GetReviewsBySummitIdAsync(int id)
    {
        return await _reviewRepository.GetReviewsBySummitIdAsync(id);
    }

    public async Task AddReviewAsync(ReviewIn reviewIn)
    {
        await _reviewRepository.AddReviewAsync(reviewIn);
    }
}