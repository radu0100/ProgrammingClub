using ProgrammingClub.Models;

namespace ProgrammingClub.Repositories
{
    public interface iAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementAsync();
        Task<Announcement> GetAnnouncementByIdAsync(Guid id);

        //Task<Announcement> GetAnnouncementByUsernameAsync(string username);

        Task AddAnnouncementAsync(Announcement Announcement);
        Task<bool> AnnouncementExistsAsync(string Title);

        //Task UpdateAnnouncementAsync(Announcement Announcement);
        //Task DeleteAnnouncementAsync(Guid id);
        //Task<bool> AnnouncementExistsAsync(Guid id);
        //Task<bool> UsernameExistsAsync(string username);

    }
}