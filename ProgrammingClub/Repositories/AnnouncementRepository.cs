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

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            _context.Entry(announcement).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _context.Announcement.AnyAsync(m => m.Title == title);
        }

        public async Task<Announcement> UpdateAnnouncementAsync(Announcement announcement)
        {
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> AnnouncementExistAsync(Guid id)
        {
            return await _context.Announcement.AnyAsync(m => m.IdAnnouncement == id);
        }

        public async Task<Announcement> UpdateAnnouncementPartiallyAsync(Announcement announcement)
        {
            Announcement announcementFromDb = await GetAnnouncementByIdAsync(announcement.IdAnnouncement);

            if (announcementFromDb == null)
            {
                return null;
            }

            // Nullable DateTime updates
            UpdateIfNotNull(announcement.ValidFrom, value => announcementFromDb.ValidFrom = value);
            UpdateIfNotNull(announcement.ValidTo, value => announcementFromDb.ValidTo = value);
            UpdateIfNotNull(announcement.EventDateTime, value => announcementFromDb.EventDateTime = value);

            // String updates
            UpdateIfNotNullOrEmpty(announcement.Title, value => announcementFromDb.Title = value);
            UpdateIfNotNullOrEmpty(announcement.Text, value => announcementFromDb.Text = value);
            UpdateIfNotNullOrEmpty(announcement.Tags, value => announcementFromDb.Tags = value);

            _context.Update(announcementFromDb);
            await _context.SaveChangesAsync();
            return announcementFromDb;
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            if (!await AnnouncementExistAsync(id))
            {
                return false;
            }

            _context.Announcement.Remove(new Announcement { IdAnnouncement = id });
            await _context.SaveChangesAsync();
            return true;
        }


        private void UpdateIfNotNullOrEmpty(string newValue, Action<string> setter)
        {
            if (!string.IsNullOrWhiteSpace(newValue))
            {
                setter(newValue);
            }
        }

        private void UpdateIfNotNull<T>(T? newValue, Action<T> setter) where T : struct
        {
            if (newValue.HasValue)
            {
                setter(newValue.Value);
            }
        }
    }
}