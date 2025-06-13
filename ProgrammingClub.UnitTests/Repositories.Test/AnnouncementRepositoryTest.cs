

using ProgrammingClub.DataContext;
using ProgrammingClub.Repositories;
using ProgrammingClub.UnitTests.Helpers;

namespace ProgrammingClub.UnitTests.Repositories.Test
{
    internal class AnnouncementRepositoryTest
    {
        private readonly AnnouncementRepository _announcementRepository;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public AnnouncementRepositoryTest()
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();

            _announcementRepository = new AnnouncementRepository(_contextInMemory);
        }
    }
}
