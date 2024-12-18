using CinemaTix.Interfaces;
using CinemaTix.Models;

namespace CinemaTix.Services
{
    public class OrderServices : IOrderService
    {
        private readonly ILogger<OrderServices> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderServices(ILogger<OrderServices> logger, IOrderRepository orderRepository) 
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public Task CreateAsync(Orders Order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Orders> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Orders Order)
        {
            throw new NotImplementedException();
        }
    }
}
