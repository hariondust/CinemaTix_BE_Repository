using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly AppDbContext _context;

        public ShowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shows>> GetAllAsync()
        {
            return await _context.Shows.Where(x => x.StatusRecord != Constants.StatusRecordDelete).AsNoTracking().ToListAsync();
        }

        public async Task<Shows> GetByIdAsync(Guid Id) => await _context.Shows.FindAsync(Id);

        public async Task CreateAsync(Shows Show)
        {
            await _context.Shows.AddAsync(Show);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shows Show)
        {
            _context.Shows.Update(Show);
            await _context.SaveChangesAsync();
        }

        // We don't use this because we use Soft Delete
        public async Task DeleteByIdAsync(Guid Id)
        {
            var Show = await _context.Shows.FindAsync(Id);
            if (Show != null)
            {
                _context.Shows.Remove(Show);
                await _context.SaveChangesAsync();
            }
        }
    }
}
