using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
