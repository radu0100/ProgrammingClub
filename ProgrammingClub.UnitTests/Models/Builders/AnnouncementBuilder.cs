using ProgrammingClub.Models;

namespace ProgrammingClub.UnitTests.Models.Builders
{
    internal class AnnouncementBuilder : BuilderBase<Announcement>
    {
        public AnnouncementBuilder()
        {
            _objectToBuild = new Announcement()
            {
                IdAnnouncement = Guid.NewGuid(),
                Title = "Reducere 55%",
                ValidFrom = DateTime.Parse("2025-05-30 00:00:00.000"),
                ValidTo = DateTime.Parse("2025-07-30 00:00:00.000"),
                Text = "Reducere in toate magazinele de 55%",
                EventDateTime = DateTime.Parse("2025-08-15 00:00:00.000"),
                Tags = "red_55%"
            };
        }
    }
}
