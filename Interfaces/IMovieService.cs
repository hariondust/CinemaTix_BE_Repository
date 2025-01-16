using CinemaTix.DTOs;
using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movies>> GetAllAsync();
        Task<Movies?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id, Guid userId);
        Task<bool> CreateAsync(CreateUpdateMovieDTO data, Guid userId);
        Task<bool> UpdateAsync(Guid id, CreateUpdateMovieDTO data, Guid userId);
    }
}
