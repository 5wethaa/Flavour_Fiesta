using Flavour_Fiesta.DataAccess.Data;
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;

using Microsoft.EntityFrameworkCore;


namespace Flavour_Fiesta.DataAccess.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks
                .OrderByDescending(f => f.SubmittedAt)
                .ToListAsync();
        }
    }
}
