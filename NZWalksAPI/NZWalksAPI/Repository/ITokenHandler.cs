using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public interface ITokenHandler
    {
       Task<string> CreateTokenAsync(User user);
    }
}
