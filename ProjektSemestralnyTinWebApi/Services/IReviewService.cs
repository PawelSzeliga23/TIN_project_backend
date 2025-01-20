using ProjektSemestralnyTinWebApi.DTOs;

namespace ProjektSemestralnyTinWebApi.Services;

public interface IReviewService
{
    public Task<ICollection<ReviewOut>> GetReviewsBySummitIdAsync(int id);
    public Task AddReviewAsync(ReviewIn reviewIn);
}