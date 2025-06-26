using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.Interface;

namespace NZWalks.API.Repository
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly WalksDbContext _context;
        public DifficultyRepository(WalksDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await _context.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid guid)
        {
            return await _context.Difficulties.SingleOrDefaultAsync(x => x.Id == guid);
        }

        public async Task<Difficulty?> CreateAsync(Difficulty difficulty)
        {
            await _context.Difficulties.AddAsync(difficulty);
            await _context.SaveChangesAsync();
            return difficulty;
        }

    }
}
