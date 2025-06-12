using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Controllers;
using ProgrammingClub.DataContext;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Repositories;
using ProgrammingClub.Services;
using ProgrammingClub.UnitTests.Helpers;



namespace ProgrammingClub.UnitTests.Controllers.Integration
{
    public class MembersControllerTests
    {
        private readonly iMembersService _membersService;
        private readonly MembersController _membersController;
        private readonly MembersRepository repository;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public MembersControllerTests(iMembersService membersService)
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();
            repository = new MembersRepository(_contextInMemory);
            _membersService = membersService;
            _membersController = new MembersController(_membersService);
        }

        [Fact]
        public async Task Get_ShouldReturnAllMembers()
        {
            // Arrange
            var member1 = await DBContextHelper.AddTestMember(_contextInMemory);
            var member2 = await DBContextHelper.AddTestMember(_contextInMemory);

            // Act
            var result = await _membersController.Get();
            var okResult = result as OkObjectResult;
            var members = okResult?.Value as IEnumerable<Member>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(members);
            Assert.Equal(2, members.Count());
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenNoMemberExist()
        {
            // Act
            var result = await _membersController.Get();

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(200, notFoundResult.StatusCode);
            Assert.Equal(ErrorMessagesEnum.NoMembersFound, notFoundResult.Value);
        }
    }
}
