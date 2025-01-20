using ProjektSemestralnyTinWebApi.DTOs;

namespace ProjektSemestralnyTinWebApi.Repositories;

public interface IReviewRepository
{
    public Task<ICollection<ReviewOut>> GetReviewsBySummitIdAsync(int id);
    public Task AddReviewAsync(ReviewIn reviewIn);
}