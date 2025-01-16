using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTix.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly AppDbContext _context;
        private new readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(AppDbContext context, UserServices userServices, ILogger<UsersController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _userService = userServices;
            _logger = logger;
        }
    }
}
