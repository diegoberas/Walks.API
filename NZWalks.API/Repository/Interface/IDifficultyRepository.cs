using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository.Interface
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();

        Task<Difficulty?> GetByIdAsync(Guid guid);
        Task<Difficulty?> CreateAsync(Difficulty difficulty);

    }
}
