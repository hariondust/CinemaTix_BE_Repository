using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.Where(x => x.StatusRecord != Constants.StatusRecordDelete).AsNoTracking().ToListAsync();
        }

        public async Task<Users> GetByIdAsync(Guid Id) => await _context.Users.FindAsync(Id);

        public async Task CreateAsync(Users User)
        {
            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users User)
        {
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }

        // We don't use this because we use Soft Delete
        public async Task DeleteByIdAsync(Guid Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
