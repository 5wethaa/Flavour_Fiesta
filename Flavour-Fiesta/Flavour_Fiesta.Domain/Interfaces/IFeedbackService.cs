using Flavour_Fiesta.Domain.Models;


namespace Flavour_Fiesta.Domain.Interfaces
{
    public interface IFeedbackService
    {
        Task SaveFeedbackAsync(Feedback feedback);
        Task<List<Feedback>> GetAllFeedbackAsync();
    }
}
