using CinemaTix.Interfaces;
using CinemaTix.Models;

namespace CinemaTix.Services
{
    public class ReviewServices : IReviewService
    {
        private readonly ILogger<ReviewServices> _logger;
        private readonly IReviewRepository _reviewRepository;

        public ReviewServices(ILogger<ReviewServices> logger, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _reviewRepository = reviewRepository;
        }

        public Task CreateAsync(Reviews Review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reviews>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Reviews> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Reviews Review)
        {
            throw new NotImplementedException();
        }
    }
}
