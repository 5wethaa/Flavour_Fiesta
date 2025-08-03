using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;


namespace Flavour_Fiesta.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            await _repository.AddAsync(feedback);
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
