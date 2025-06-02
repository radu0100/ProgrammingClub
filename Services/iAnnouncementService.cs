using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public interface iAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task<Announcement> GetAnnouncementByIdAsync(Guid id);

        Task AddAnnouncementAsync(Announcement announcement);
    }
}