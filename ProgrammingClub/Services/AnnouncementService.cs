using ProgrammingClub.Models;
using ProgrammingClub.Repositories;

namespace ProgrammingClub.Services
{
    public class AnnouncementService : iAnnouncementService
    {
        private readonly iAnnouncementRepository _AnnouncementRepository;
        public AnnouncementService(iAnnouncementRepository repository)
        {
            _AnnouncementRepository = repository;
        }
        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _AnnouncementRepository.GetAllAnnouncementAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(Guid id)
        {
            return await _AnnouncementRepository.GetAnnouncementByIdAsync(id);
        }

        public async Task AddAnnouncementAsync(Announcement Announcement)
        {
            if (await _AnnouncementRepository.AnnouncementExistsAsync(Announcement.Title))
            {
                throw new ArgumentException("Announcement already exists.", nameof(Announcement.Title));
            }
            Announcement.IdAnnouncement = Guid.NewGuid();
            await _AnnouncementRepository.AddAnnouncementAsync(Announcement);
        }
    }
}