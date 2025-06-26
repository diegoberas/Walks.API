using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository.Interface;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        // REPOSITORY PATTERN

        private readonly WalksDbContext _context;
        public RegionRepository(WalksDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var searching = await _context.Regions.SingleOrDefaultAsync(r => r.Id == id);
            if (searching == null) { return null; }

            searching.Code = region.Code;
            searching.Name = region.Name;
            searching.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();
            return searching;
        }

        public async Task<Region?> RemoveAsync(Guid id)
        {
            var searching = await _context.Regions.SingleOrDefaultAsync(r => r.Id == id);
            if (searching == null) { return null; }

            _context.Regions.Remove(searching);
            await _context.SaveChangesAsync();
            return searching;
        }
    }
}
