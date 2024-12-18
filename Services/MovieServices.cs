using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Repositories;

namespace CinemaTix.Services
{
    public class MovieServices : IMovieService
    {
        private readonly ILogger<MovieServices> _logger;
        private readonly IMovieRepository _movieRepository;

        public MovieServices(ILogger<MovieServices> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        public Task CreateAsync(Movies Movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movies>> GetAllAsync()
        {
            var result = await _movieRepository.GetAllAsync();
            if (result.Any())
            {
                _logger.LogInformation("Movies successfully retrieved!");
                return result;
            }

            _logger.LogInformation("Movies data does not exists!");
            return new List<Movies>();
        }

        public Task<Movies> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Movies Movie)
        {
            throw new NotImplementedException();
        }
    }
}
