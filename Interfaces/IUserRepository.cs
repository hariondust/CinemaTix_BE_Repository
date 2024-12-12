using CinemaTix.Models;

namespace CinemaTix.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(Guid Id);
        Task DeleteByIdAsync(Guid Id);
        Task CreateAsync(Users User);
        Task UpdateAsync(Users User);
    }
}
