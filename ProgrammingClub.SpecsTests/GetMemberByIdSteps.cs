using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Repositories;
using ProgrammingClub.Services;
using ProgrammingClub.Models;
using TechTalk.SpecFlow;

namespace ProgrammingClubAPI.SpecsTests
{
    [Binding]
    public class GetMemberByIdSteps
    {
        private MembersService _membersService;
        private ProgrammingClubDataContext _context;
        private MembersRepository _membersRepository;
        private Member _testMember;
        private Member _resultMember;


        [Given(@"a member exists with IDMember ""(.*)""")]
        public void GivenAMemberExistWithIdMember(string IDMember)
        {
            var id = Guid.Parse(IDMember);

            _testMember = new Member()
            {
                IDMember = id,
                Username = "testuser",
                Name = "Test User",
                Title = "Test Title",
                Description = "Test Description",
                Resume = "Test Resume",
                Position = "Test Position",
                Password = "TestPassword123"
            };

            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                 .Options;

            _context = new ProgrammingClubDataContext(options);

            _context.Database.EnsureCreated();

            _context.Members.Add(_testMember);
            _context.SaveChanges();

            _membersRepository = new MembersRepository(_context);
            _membersService = new MembersService(_membersRepository, null);
        }

        [When(@"the member is requested by IDMember")]
        public async Task WhenTheMemberIsRequestedByIdMember()
        {
            _resultMember = await _membersService.GetMemberByIdAsync(_testMember.IDMember);
        }

        [Then(@"the response should contain the member with the same IDMember")]
        public void ThenTheMemberShouldBeReturnedWithIdMember()
        {
            Assert.NotNull(_resultMember);
            Assert.Equal(_testMember.IDMember, _resultMember.IDMember);
            Assert.Equal(_testMember.Username, _resultMember.Username);
            Assert.Equal(_testMember.Name, _resultMember.Name);
            Assert.Equal(_testMember.Title, _resultMember.Title);
            Assert.Equal(_testMember.Description, _resultMember.Description);
            Assert.Equal(_testMember.Resume, _resultMember.Resume);
        }
    }
}