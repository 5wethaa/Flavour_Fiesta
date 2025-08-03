using Flavour_Fiesta.Domain.Models;


namespace Flavour_Fiesta.Domain.Interfaces
{
    public interface IFeedbackRepository
    {
        Task AddAsync(Feedback feedback);
        Task<List<Feedback>> GetAllAsync();
    }
}
