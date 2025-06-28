using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.UnitTests.Models.Builders;
using ProgrammingClub.Models;

namespace ProgrammingClub.UnitTests.Helpers
{
    public class DBContextHelper
    {
        public static ProgrammingClubDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .Options;


            var context = new ProgrammingClubDataContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static async Task<Member> AddTestMember(ProgrammingClubDataContext context, Member? testMember = null)
        {
            testMember ??= new MemberBuilder().Build(); 

            context.Members.Add(testMember);

            await context.SaveChangesAsync();

            context.Entry(testMember).State = EntityState.Detached;

            return testMember;
        }

        public static async Task<Announcement> AddTestAnnouncement(ProgrammingClubDataContext context, Announcement? testAnnouncement = null)
        {
            testAnnouncement ??= new AnnouncementBuilder().Build();

            context.Announcement.Add(testAnnouncement);

            await context.SaveChangesAsync();

            context.Entry(testAnnouncement).State = EntityState.Detached;

            return testAnnouncement;
        }
    }
}
