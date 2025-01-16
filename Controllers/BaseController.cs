using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CinemaTix.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly IUserRoleService _userRoleService;

        protected BaseController(ILogger<BaseController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
        }

        protected async Task<(Guid, bool)> CheckUserLoginData()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("User ID not found in the token.");
                throw new UnauthorizedAccessException("Invalid token.");
            }

            Guid userId = new(userIdClaim.Value);

            bool isAdmin = await _userRoleService.IsUserAdmin(userId);

            return (userId, isAdmin);
        }
    }
}
