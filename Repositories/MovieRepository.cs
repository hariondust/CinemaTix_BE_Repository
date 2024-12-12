using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movies>> GetAllAsync()
        {
            return await _context.Movies.Where(x => x.StatusRecord != EnumStatusRecord.Delete).AsNoTracking().ToListAsync();
        }

        public async Task<Movies> GetByIdAsync(Guid Id) => await _context.Movies.FindAsync(Id);

        public async Task CreateAsync(Movies Movie)
        {
            await _context.Movies.AddAsync(Movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movies Movie)
        {
            _context.Movies.Update(Movie);
            await _context.SaveChangesAsync();
        }

        // We don't use this because we use Soft Delete
        public async Task DeleteByIdAsync(Guid Id)
        {
            var Movie = await _context.Movies.FindAsync(Id);
            if (Movie != null)
            {
                _context.Movies.Remove(Movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
