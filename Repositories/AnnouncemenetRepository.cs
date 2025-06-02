using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Repositories
{
    public class AnnouncementRepository : iAnnouncementRepository
    {
        private readonly ProgrammingClubDataContext _context;
        public AnnouncementRepository(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Announcement>> GetAllAnnouncementAsync()
        {
            return await _context.Announcement.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(Guid id)
        {
            return await _context.Announcement.FirstOrDefaultAsync(m => m.IdAnnouncement == id);
        }

        public async Task AddAnnouncementAsync(Announcement Announcement)
        {
            _context.Entry(Announcement).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnnouncementExistsAsync(string title)
        {
            return await _context.Announcement.AnyAsync(m => m.Title == title);
        }
    }
}