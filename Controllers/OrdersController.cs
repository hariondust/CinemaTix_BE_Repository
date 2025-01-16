using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTix.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly AppDbContext _context;
        private new readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(AppDbContext context, OrderServices orderServices, ILogger<OrdersController> logger, IUserRoleService userRoleService) : base(logger, userRoleService)
        {
            _context = context;
            _orderService = orderServices;
            _logger = logger;
        }
    }
}
