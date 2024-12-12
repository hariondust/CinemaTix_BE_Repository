using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Reviews>> GetAllAsync();
        Task<Reviews> GetByIdAsync(Guid Id);
        Task DeleteByIdAsync(Guid Id);
        Task CreateAsync(Reviews Review);
        Task UpdateAsync(Reviews Review);
    }
}
