using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> GetAllAsync()
        {
            return await _context.Orders.Where(x => x.StatusRecord != EnumStatusRecord.Delete).AsNoTracking().ToListAsync();
        }

        public async Task<Orders> GetByIdAsync(Guid Id) => await _context.Orders.FindAsync(Id);

        public async Task CreateAsync(Orders Order)
        {
            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orders Order)
        {
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();
        }

        // We don't use this because we use Soft Delete
        public async Task DeleteByIdAsync(Guid Id)
        {
            var Order = await _context.Orders.FindAsync(Id);
            if (Order != null)
            {
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
