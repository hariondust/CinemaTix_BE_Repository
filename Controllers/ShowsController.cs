using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTix.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : BaseController
    {
        private readonly AppDbContext _context;
        private new readonly ILogger<ShowsController> _logger;
        private readonly IShowService _showService;

        public ShowsController(AppDbContext context, ShowServices showServices, ILogger<ShowsController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _showService = showServices;
            _logger = logger;
        }
    }
}
