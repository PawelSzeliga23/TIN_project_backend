using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public interface IUserRepository
{
    public Task<User> GetUserByAccessTokenAsync(string accessToken);
}