using ProgrammingClub.Models;

namespace ProgrammingClub.Repositories
{
    public interface iAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementAsync();
        Task<Announcement> GetAnnouncementByIdAsync(Guid id);
        Task AddAnnouncementAsync(Announcement announcement);
        Task<bool> TitleExistsAsync(string title);
        Task<Announcement> UpdateAnnouncementAsync(Announcement announcement);
        Task<Announcement> UpdateAnnouncementPartiallyAsync(Announcement announcement);
        Task<bool> AnnouncementExistAsync(Guid id);
        Task<bool> DeleteAnnouncementAsync(Guid id);
    }
}