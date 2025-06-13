using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;
using ProgrammingClub.Repositories;

namespace ProgrammingClub.Services
{
    public class AnnouncementService : iAnnouncementService
    {
        private readonly iAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;
        public AnnouncementService(iAnnouncementRepository repository, IMapper mapper)
        {
            _announcementRepository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _announcementRepository.GetAllAnnouncementAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(Guid id)
        {
            return await _announcementRepository.GetAnnouncementByIdAsync(id);
        }

        public async Task AddAnnouncementAsync(Announcement Announcement)
        {
            if (await _announcementRepository.TitleExistsAsync(Announcement.Title))
            {
                throw new ArgumentException("Announcement already exists.", nameof(Announcement.Title));
            }
            Announcement.IdAnnouncement = Guid.NewGuid();
            await _announcementRepository.AddAnnouncementAsync(Announcement);
        }

        public async Task<Announcement> UpdateAnnouncementAsync(Guid id, Announcement announcement)
        {
            if (!await _announcementRepository.AnnouncementExistAsync(id))
            {
                return null;
            }
            announcement.IdAnnouncement = id;
            await _announcementRepository.UpdateAnnouncementAsync(announcement);
            return announcement; 
        }

        public async Task<Announcement> UpdateAnnouncementPartiallyAsync(Guid id, UpdateAnnouncementPartially updateAnnouncement)
        {
            if (!await _announcementRepository.AnnouncementExistAsync(id))
            {
                return null;
            }
            Announcement announcement = _mapper.Map<Announcement>(updateAnnouncement);
            announcement.IdAnnouncement = id;
            await _announcementRepository.UpdateAnnouncementPartiallyAsync(announcement);
            return announcement;
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            if (!await _announcementRepository.AnnouncementExistAsync(id))
            {
                return false;
            }
            return await _announcementRepository.DeleteAnnouncementAsync(id);
        }
    }
}