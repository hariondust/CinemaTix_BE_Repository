using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        private readonly AppDbContext _context;
        private new readonly ILogger<ReviewsController> _logger;
        private readonly IReviewService _Reviewservice;

        public ReviewsController(AppDbContext context, ReviewServices Reviewservices, ILogger<ReviewsController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _Reviewservice = Reviewservices;
            _logger = logger;
        }
    }
}
