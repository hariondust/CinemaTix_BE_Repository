using CinemaTix.Interfaces;
using CinemaTix.Models;

namespace CinemaTix.Services
{
    public class UserServices : IUserService
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IUserRepository _userRepository;

        public UserServices(ILogger<UserServices> logger, IUserRepository userRepository) 
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public Task CreateAsync(Users User)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Users User)
        {
            throw new NotImplementedException();
        }
    }
}
