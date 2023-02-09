using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public interface IRegionRepository
    {
     Task< IEnumerable<Region>>  GetAllAsync();
    }
}
