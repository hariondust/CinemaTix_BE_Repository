using CinemaTix.Data;
using CinemaTix.Interfaces;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDbContext _context;

        public UserRoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserAdmin(Guid userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user != null && user.Role == EnumUserRole.Admin;
        }
    }
}
