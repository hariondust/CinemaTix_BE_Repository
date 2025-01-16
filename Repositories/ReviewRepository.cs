using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reviews>> GetAllAsync()
        {
            return await _context.Reviews.Where(x => x.StatusRecord != Constants.StatusRecordDelete).AsNoTracking().ToListAsync();
        }

        public async Task<Reviews?> GetByIdAsync(Guid Id) => await _context.Reviews.FindAsync(Id);

        public async Task CreateAsync(Reviews Review)
        {
            await _context.Reviews.AddAsync(Review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reviews Review)
        {
            _context.Reviews.Update(Review);
            await _context.SaveChangesAsync();
        }

        // We don't use this because we use Soft Delete
        public async Task DeleteByIdAsync(Guid Id)
        {
            var Review = await _context.Reviews.FindAsync(Id);
            if (Review != null)
            {
                _context.Reviews.Remove(Review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
