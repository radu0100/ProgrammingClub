using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Repositories;
using ProgrammingClub.Services;
using ProgrammingClub.Controllers;
using ProgrammingClub.Models;
using ProgrammingClub.UnitTests.Helpers;


namespace ProgrammingClub.UnitTests.Controllers.Integration
{
    public class AnnouncementControllerTests
    {
        private readonly AnnouncementService _announcementService;
        private readonly AnnouncementRepository _repository;
        private readonly AnnouncementController _announcementController;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public AnnouncementControllerTests()
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();
            _repository = new AnnouncementRepository(_contextInMemory);
            _announcementService = new AnnouncementService(_repository, null);
            _announcementController = new AnnouncementController(_announcementService);
        }

        [Fact]
        public async Task Get_ShouldReturnAllAnnouncements_WhenAnnouncementsExist()
        {
            // Arrange
            var testAnnouncement1 = await DBContextHelper.AddTestAnnouncement(_contextInMemory);
            var testAnnouncement2 = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var result = await _announcementController.Get();
            var okResult = result as OkObjectResult;
            var announcements = okResult?.Value as IEnumerable<Announcement>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(announcements);
            Assert.Equal(2, announcements.Count());
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenNoAnnouncementsExist()
        {
            // Act
            var result = await _announcementController.Get();

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(200, notFoundResult.StatusCode);
            Assert.Equal(ErrorMessagesEnum.NoAnnouncementFound, notFoundResult.Value);
        }

        [Fact]
        public async Task GetAnnouncementById_ShouldReturnAnnouncement_WhenExists()
        {
            // Arrange
            var testAnnouncement = await DBContextHelper.AddTestAnnouncement(_contextInMemory);

            // Act
            var result = await _announcementController.GetAnnouncementById(testAnnouncement.IdAnnouncement);
            var okResult = result as OkObjectResult;
            var announcement = okResult?.Value as Announcement;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(announcement);
            Assert.Equal(testAnnouncement.IdAnnouncement, announcement.IdAnnouncement);
        }

        [Fact]
        public async Task GetAnnouncementById_ShouldReturnNotFound_WhenNotExists()
        {
            // Act
            var result = await _announcementController.GetAnnouncementById(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal(ErrorMessagesEnum.AnnouncementNotFound, notFoundResult.Value);
        }
    }
}