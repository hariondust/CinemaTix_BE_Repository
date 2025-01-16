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

        public async Task<IEnumerable<Movies>> GetAllAsync() => await _context.Movies.Where(x => x.StatusRecord != Constants.StatusRecordDelete).AsNoTracking().ToListAsync();

        public async Task<Movies?> GetByIdAsync(Guid Id) => await _context.Movies.FindAsync(Id);

        public async Task<Movies?> GetByTitleAsync(string Title) => await _context.Movies.Where(x => x.Title == Title && x.StatusRecord != Constants.StatusRecordDelete).AsNoTracking().FirstOrDefaultAsync();

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

        // We don't use this as we're using soft delete
        public async Task DeleteAsync(Guid Id)
        {
            var movie = await _context.Movies.FindAsync(Id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
