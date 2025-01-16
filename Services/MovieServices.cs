using CinemaTix.DTOs;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Repositories;
using CinemaTix.Utils;

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

        public async Task<bool> CreateAsync(CreateUpdateMovieDTO data, Guid userId)
        {
            try
            {
                var movie = await _movieRepository.GetByTitleAsync(data.Title!);
                if (movie != null)
                {
                    _logger.LogInformation("Movie with the same Title already exists!.");
                    return false;
                }

                var payload = new Movies()
                {
                    Title = data.Title!,
                    Synopsis = data.Synopsis,
                    Duration = data.Duration ?? 120, // default movie 120 min
                    Rating = 0,
                    PosterImageUrl = data.PosterImageUrl
                };

                await _movieRepository.CreateAsync(payload);
                _logger.LogInformation("Movie: {title} has been created!", data.Title);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred when creating new movie");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, CreateUpdateMovieDTO data, Guid userId)
        {
            try
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogInformation("Movie does not exists");
                    return false;
                }

                movie.Title = data.Title ?? movie.Title;
                movie.Synopsis = data.Synopsis ?? movie.Synopsis;
                movie.Duration = data.Duration ?? movie.Duration;
                movie.Rating = data.Rating ?? movie.Rating;
                movie.PosterImageUrl = data.PosterImageUrl ?? movie.PosterImageUrl;
                movie.ProcessData(Constants.StatusRecordUpdate, userId);

                await _movieRepository.UpdateAsync(movie);
                _logger.LogInformation("Movie: {title} has been updated!", movie.Title);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred when updating a movie");
                throw;
            }
        }

        public async Task DeleteByIdAsync(Guid id, Guid userId)
        {
            try
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie != null)
                {
                    movie.ProcessData(Constants.StatusRecordDelete, userId);
                
                    await _movieRepository.UpdateAsync(movie);
                    _logger.LogInformation("Movie: {title} has been deleted!", movie.Title);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred when deleting a movie");
                throw;
            }
        }

        public async Task<IEnumerable<Movies>> GetAllAsync()
        {
            var result = await _movieRepository.GetAllAsync();
            if (result.Any())
            {
                _logger.LogInformation("Movies successfully retrieved!");
                return result;
            }

            _logger.LogWarning("Movies data does not exists!");
            return result;
        }

        public async Task<Movies?> GetByIdAsync(Guid Id)
        {
            return await _movieRepository.GetByIdAsync(Id);
        }

    }
}
