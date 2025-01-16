using CinemaTix.Data;
using CinemaTix.DTOs;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Services;
using CinemaTix.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaTix.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly AppDbContext _context;
        private new readonly ILogger<MoviesController> _logger;
        private readonly IMovieService _movieService;


        public MoviesController(AppDbContext context, IMovieService movieService, ILogger<MoviesController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _movieService = movieService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Attempting to retrieve Movies data.");

            try
            {
                var movies = await _movieService.GetAllAsync();
                if (movies == null || !movies.Any())
                {
                    return NotFound("Movies does not exists.");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred when retrieving Movies data.");
                return StatusCode(500, "An error has occurred when retrieving Movies data.");
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve a movie with ID {Id}.", id);

            try
            {
                (Guid userId, bool isAdmin) userLoginData = await CheckUserLoginData();

                if (!userLoginData.isAdmin)
                {
                    throw new UnauthorizedAccessException("User does not have permission to do this process.");
                }

                var movie = await _movieService.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound($"Movie with ID {id} does not exists.");
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the movie with ID {Id}.", id);
                return StatusCode(500, "An error occurred while retrieving the movie.");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUpdateMovieDTO data)
        {
            _logger.LogInformation("Attempting to create a new movie.");

            if (!ModelState.IsValid)
            {
                _logger.LogError("The request payload is not valid.");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(data.Title))
            {
                _logger.LogError("The request payload is not valid.");
                return BadRequest("Title field is required");
            }

            try
            {
                (Guid userId, bool isAdmin) userLoginData = await CheckUserLoginData();

                if (!userLoginData.isAdmin)
                {
                    throw new UnauthorizedAccessException("User does not have permission to do this process.");
                }

                var isCreated = await _movieService.CreateAsync(data, userLoginData.userId);
                if (!isCreated)
                {
                    return StatusCode(200, "Movie with the same title already exists.");
                }

                return StatusCode(201, "New movie has been created!.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized user attempting to create new Movie. Process abruptly stopped.");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new movie.");
                return StatusCode(500, "An error occurred while creating the movie.");
            }
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateUpdateMovieDTO data)
        {
            _logger.LogInformation("Attempting to update the movie with ID {Id}.", id);

            if (!ModelState.IsValid)
            {
                _logger.LogError("The request payload is not valid.");
                return BadRequest(ModelState);
            }

            try
            {
                (Guid userId, bool isAdmin) userLoginData = await CheckUserLoginData();

                if (!userLoginData.isAdmin)
                {
                    throw new UnauthorizedAccessException("User does not have permission to do this process.");
                }

                var movie = await _movieService.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound($"Movie with ID {id} does not exists.");
                }

                await _movieService.UpdateAsync(id, data, userLoginData.userId);
                return StatusCode(200, "Movie has been updated!.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized user attempting to update Movie with ID {Id}. Process abruptly stopped.", id);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the movie with ID {Id}.", id);
                return StatusCode(500, "An error occurred while updating the movie.");
            }
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Attempting to delete the movie with ID {Id}.", id);

            try
            {
                (Guid userId, bool isAdmin) userLoginData = await CheckUserLoginData();

                if (!userLoginData.isAdmin)
                {
                    throw new UnauthorizedAccessException("User does not have permission to do this process.");
                }

                var movie = await _movieService.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound($"Movie with ID {id} does not exists.");
                }

                await _movieService.DeleteByIdAsync(id, userLoginData.userId);
                
                _logger.LogInformation("Movie {0} has been deleted", movie.Title);
                return StatusCode(200, "Movie successfully deleted");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized user attempting to delete Movie with ID {Id}. Process abruptly stopped.", id);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the movie with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting the movie.");
            }
        }
    }
}
