using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;

namespace ProgrammingClub.Services
{
    public interface iAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task<Announcement> GetAnnouncementByIdAsync(Guid id);
        Task AddAnnouncementAsync(Announcement announcement);
        Task<Announcement> UpdateAnnouncementAsync(Guid id, Announcement announcement);
        Task<Announcement> UpdateAnnouncementPartiallyAsync(Guid id, UpdateAnnouncementPartially announcement);
        Task<bool> DeleteAnnouncementAsync(Guid id);
    }
}