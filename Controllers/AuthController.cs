using CinemaTix.Data;
using CinemaTix.DTOs;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CinemaTix.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly JWTServices _jwtServices;
        private readonly IDistributedCache _cache;
        private new readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, JWTServices jwtServices, IDistributedCache cache, ILogger<AuthController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _jwtServices = jwtServices;
            _cache = cache;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            _logger.LogInformation("Login process has started.");

            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                _logger.LogWarning("Login failed: Missing credentials.");
                return BadRequest("Username and password are required.");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == login.Username);
            if (user == null)
            {
                _logger.LogWarning("Login failed: Invalid credentials.");
                return Unauthorized("Invalid username or password.");
            }

            bool passwordMatch = VerifyPassword(login.Password, user.Password);
            if (!passwordMatch)
            {
                _logger.LogWarning("Login failed: Invalid credentials.");
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtServices.GenerateToken(user.Id, user.Username);
            try
            {
                var cacheKey = $"user:{user.Id}:session";
                await _cache.SetStringAsync(cacheKey, token, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
            }
            catch (RedisConnectionException ex)
            {
                _logger.LogError(ex, "Redis connection error: {Message}", ex.Message);
                return StatusCode(503, "Redis service is currently unavailable. Please try again later.");
            }

            _logger.LogInformation("Login successful.");
            return Ok(new LoginResponseDTO { Token = token });
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Logout process has started.");

            try
            {
                (Guid userId, bool isAdmin) userLoginData = await CheckUserLoginData();

                var redisKey = $"CinemaTixuser:{userLoginData.userId}:session";
                await _cache.RemoveAsync(redisKey);
             
                _logger.LogInformation("Logout successful for user: {UserId}", userLoginData.userId);
                return Ok("Successfully logged out.");
            }
            catch (RedisConnectionException ex)
            {
                _logger.LogError(ex, "Redis connection error during the logout process.");
                return StatusCode(503, "Redis service is currently unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred during the logout process.");
                return StatusCode(500, "An error has occurred during the logout process.");
            }
        }

        private bool VerifyPassword (string loginPassword, string userHashPassword)
        {
            return HashPassword(loginPassword) == userHashPassword;
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash).ToUpper();
            }
        }
    }
}
