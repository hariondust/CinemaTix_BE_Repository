using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movies>> GetAllAsync();
        Task<Movies?> GetByIdAsync(Guid Id);
        Task<Movies?> GetByTitleAsync(string Title);
        Task DeleteAsync(Guid Id);
        Task CreateAsync(Movies Movie);
        Task UpdateAsync(Movies Movie);
    }
}
