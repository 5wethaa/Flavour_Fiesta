using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Models
{
    public class FeedbackPageViewModel
    {
        public FeedbackViewModel Form { get; set; } = new FeedbackViewModel();
        public List<Feedback> FeedbackList { get; set; } = new List<Feedback>();
    }
}
