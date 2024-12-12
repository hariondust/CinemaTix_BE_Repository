using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Orders>> GetAllAsync();
        Task<Orders> GetByIdAsync(Guid Id);
        Task DeleteByIdAsync(Guid Id);
        Task CreateAsync(Orders Order);
        Task UpdateAsync(Orders Order);
    }
}
