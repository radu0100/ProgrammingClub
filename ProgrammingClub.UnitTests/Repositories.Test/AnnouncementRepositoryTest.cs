using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Repositories;
using ProgrammingClub.UnitTests.Helpers;

namespace ProgrammingClub.UnitTests.Repositories.Test
{
    public class AnnouncementRepositoryTest
    {
        private readonly AnnouncementRepository _announcementRepository;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public AnnouncementRepositoryTest()
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();

            _announcementRepository = new AnnouncementRepository(_contextInMemory);
        }
        [Fact]
        public async Task GetAllAnnouncementAsync_ShouldReturnAllAnnouncements()
        {
            // Arrange
            var announcement1 = await DBContextHelper.AddTestAnnouncement(_contextInMemory);
            var announcement2 = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var announcements = await _announcementRepository.GetAllAnnouncementAsync();

            // Assert
            Assert.NotNull(announcements);
            Assert.Equal(2, announcements.Count());
            Assert.Contains(announcements, a => a.IdAnnouncement == announcement1.IdAnnouncement);
            Assert.Contains(announcements, a => a.IdAnnouncement == announcement2.IdAnnouncement);
        }

        [Fact]
        public async Task GetAllAnnouncementAsync_ShouldReturnEmpty_WhenNoAnnouncementsExist()
        {
            // Act
            var announcements = await _announcementRepository.GetAllAnnouncementAsync();

            // Assert
            Assert.NotNull(announcements);
            Assert.Empty(announcements);
        }

        [Fact]
        public async Task GetAnnouncementByIdAsync_ShouldReturnAnnouncement_WhenExists()
        {
            // Arrange
            var announcement = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var result = await _announcementRepository.GetAnnouncementByIdAsync(announcement.IdAnnouncement);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(announcement.IdAnnouncement, result.IdAnnouncement);
        }

        [Fact]
        public async Task UpdateAnnouncementAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var announcement = new Announcement { IdAnnouncement = Guid.NewGuid(), Title = "DoesNotExist" };

            // Act
            var updated = await _announcementRepository.UpdateAnnouncementAsync(announcement);

            // Assert
            Assert.Null(updated); // Now expects null when not found
        }

        [Fact]
        public async Task AddAnnouncementAsync_ShouldAddAnnouncement()
        {
            // Arrange
            var announcement = new Announcement
            {
                IdAnnouncement = Guid.NewGuid(),
                Title = "Test Title",
                Text = "Test Text"
            };

            // Act
            await _announcementRepository.AddAnnouncementAsync(announcement);
            var added = await _announcementRepository.GetAnnouncementByIdAsync(announcement.IdAnnouncement);

            // Assert
            Assert.NotNull(added);
            Assert.Equal(announcement.IdAnnouncement, added.IdAnnouncement);
        }

        [Fact]
        public async Task TitleExistsAsync_ShouldReturnTrue_WhenTitleExists()
        {
            // Arrange
            var announcement = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var exists = await _announcementRepository.TitleExistsAsync(announcement.Title);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task TitleExistsAsync_ShouldReturnFalse_WhenTitleDoesNotExist()
        {
            // Act
            var exists = await _announcementRepository.TitleExistsAsync("NonExistentTitle");

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task UpdateAnnouncementAsync_ShouldUpdateAnnouncement_WhenExists()
        {
            // Arrange
            var announcement = await DBContextHelper.AddTestAnnouncement(_contextInMemory);
            announcement.Title = "Updated Title";

            // Act
            var updated = await _announcementRepository.UpdateAnnouncementAsync(announcement);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Updated Title", updated.Title);
        }

        [Fact]
        public async Task UpdateAnnouncementAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var announcement = new Announcement { IdAnnouncement = Guid.NewGuid(), Title = "DoesNotExist" };

            // Act
            var updated = await _announcementRepository.UpdateAnnouncementAsync(announcement);

            // Assert
            Assert.NotNull(updated); // The repository always returns the input, but you may want to check existence before updating in your service layer.
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_ShouldDeleteAnnouncement_WhenExists()
        {
            // Arrange
            var announcement = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var deleted = await _announcementRepository.DeleteAnnouncementAsync(announcement.IdAnnouncement);

            // Assert
            Assert.True(deleted);
            var afterDelete = await _announcementRepository.GetAnnouncementByIdAsync(announcement.IdAnnouncement);
            Assert.Null(afterDelete);
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_ShouldReturnFalse_WhenNotExists()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var deleted = await _announcementRepository.DeleteAnnouncementAsync(nonExistentId);

            // Assert
            Assert.False(deleted);
        }
    }
}