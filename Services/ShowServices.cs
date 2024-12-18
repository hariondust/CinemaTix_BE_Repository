using CinemaTix.Interfaces;
using CinemaTix.Models;

namespace CinemaTix.Services
{
    public class ShowServices : IShowService
    {
        private readonly ILogger<ShowServices> _logger;
        private readonly IShowRepository _showRepository;

        public ShowServices(ILogger<ShowServices> logger, IShowRepository showRepository)
        {
            _logger = logger;
            _showRepository = showRepository;
        }

        public Task CreateAsync(Shows Show)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shows>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Shows> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Shows Show)
        {
            throw new NotImplementedException();
        }
    }
}
