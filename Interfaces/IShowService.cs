using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<Shows>> GetAllAsync();
        Task<Shows> GetByIdAsync(Guid Id);
        Task DeleteByIdAsync(Guid Id);
        Task CreateAsync(Shows Show);
        Task UpdateAsync(Shows Show);
    }
}
