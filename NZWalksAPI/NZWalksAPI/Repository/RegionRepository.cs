using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZwalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZwalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
           await nZwalksDbContext.AddAsync(region);
            await nZwalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region = await nZwalksDbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if(region == null)
            {
                return null;
            }

            //delete the region
            nZwalksDbContext.Regions.Remove(region);
            await nZwalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
          return await nZwalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
          return await nZwalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);            

        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZwalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code=region.Code;
            existingRegion.Name=region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long=region.Long;
            existingRegion.Population = region.Population;
            await nZwalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
