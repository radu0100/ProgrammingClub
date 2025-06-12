using ProgrammingClub.Models;
using ProgrammingClub.DataContext;
using ProgrammingClub.Repositories;
using ProgrammingClub.UnitTests.Helpers;
using ProgrammingClub.UnitTests.Models.Builders;

namespace ProgrammingClub.UnitTests.Repositories.Test
{ 
    public class MembersRepositoryTest
    {
        private readonly MembersRepository _membersRepository;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public MembersRepositoryTest()
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();

            _membersRepository = new MembersRepository(_contextInMemory);
        }

        #region GetAllMembersAsync Tests
        [Fact]
        public async Task GetAllMembersAsync_ShouldReturnAllMembers()
        {
            // Arrange
            var testMember1 = await DBContextHelper.AddTestMember(_contextInMemory);
            var testMember2 = await DBContextHelper.AddTestMember(_contextInMemory);

            // Act
            var members = await _membersRepository.GetAllMembersAsync();

            // Assert
            Assert.NotNull(members);
            Assert.Equal(2, members.Count());
            Assert.Contains(members, m => m.IDMember == testMember1.IDMember);
            Assert.Contains(members, m => m.IDMember == testMember2.IDMember);
        }

        [Fact]
        public async Task GetAllMembersASync_ShouldReturnNull_WhenNoMembersExist()
        {
            // Arrange
            // No members added

            // Act
            var members = await _membersRepository.GetAllMembersAsync();

            // Assert
            Assert.NotNull(members);
            Assert.Empty(members);
        }
        #endregion

        #region GetMemberByIdAsync Tests
        [Fact]
        public async Task GetMemberByIdAsync_ShouldReturnMember_WhenExists()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);

            // Act
            var member = await _membersRepository.GetMemberByIdAsync(testMember.IDMember);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(testMember.IDMember, member.IDMember);
        }

        [Fact]
        public async Task GetMemberByIdAsync_ShouldReturnNull_WhenMemberDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var member = await _membersRepository.GetMemberByIdAsync(nonExistentId);

            // Assert
            Assert.Null(member);
        }
        #endregion

        #region AddMemberAsync Tests    
        [Fact]
        public async Task AddMemberAsync_ShouldNotAddMember_WhenInvalIDMemberProvided()
        {
            // Arrange
            var newMember = new Member();

            // Act
            await _membersRepository.AddMemberAsync(newMember);
            var addedMember = await _membersRepository.GetMemberByIdAsync(newMember.IDMember);
            // Assert
            Assert.Null(addedMember);
            // Assert.Equal(newMember.IDMember, addedMember.IDMember);
        }

        [Fact]
        public async Task AddMemberAsync_ShouldAddMember_WhenValIDMemberProvided()
        {
            // Arrange
            var newMember = new MemberBuilder().With(x => { x.Name = "TestName"; x.IDMember = Guid.NewGuid(); }).Build(); ;

            // Act
            await _membersRepository.AddMemberAsync(newMember);
            var addedMember = await _membersRepository.GetMemberByIdAsync(newMember.IDMember);
            // Assert
            Assert.NotNull(addedMember);
            Assert.Equal(newMember.IDMember, addedMember.IDMember);
        }
        #endregion

        #region UsernameExistsAsync Tests
        [Fact]
        public async Task UsernameExistsAsync_ShouldReturnTrue_WhenUsernameExists()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);
            // Act
            var exists = await _membersRepository.UsernameExistsAsync(testMember.Username);
            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UsernameExistsAsync_ShouldReturnFalse_WhenUsernameDoesNotExist()
        {
            // Arrange
            var nonExistentUsername = "nonExistentUsername";
            // Act
            var exists = await _membersRepository.UsernameExistsAsync(nonExistentUsername);
            // Assert
            Assert.False(exists);
        }
        #endregion

        #region UpdateMemberAsync Tests
        [Fact]
        public async Task UpdateMemberAsync_ShouldUpdateMember_WhenValIDMemberProvided()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);
            testMember.Name = "UpdatedName";
            // Act
            var updatedMember = await _membersRepository.UpdateMemberAsync(testMember);
            // Assert
            Assert.NotNull(updatedMember);
            Assert.Equal(testMember.IDMember, updatedMember.IDMember);
            Assert.Equal("UpdatedName", updatedMember.Name);
        }

        [Fact]
        public async Task UpdateMemberAsync_ShouldReturnNull_WhenMemberDoesNotExist()
        {
            // Arrange
            var nonExistentMember = new MemberBuilder().With(x => x.IDMember = Guid.NewGuid()).Build();
            // Act
            var updatedMember = await _membersRepository.UpdateMemberAsync(nonExistentMember);
            // Assert
            Assert.Null(updatedMember);
        }

        [Fact]
        public async Task UpdateMemberAsync_ShouldReturnNull_WhenInvalIDMemberProvided()
        {
            // Arrange
            var invalIDMember = new Member(); // Invalid member with no IDMember set
            // Act
            var updatedMember = await _membersRepository.UpdateMemberAsync(invalIDMember);
            // Assert
            Assert.Null(updatedMember);
        }
        #endregion

        #region MemberExistsAsync Tests
        [Fact]
        public async Task MemberExistsAsync_ShouldReturnTrue_When_MemberExists()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);
            // Act
            var exists = await _membersRepository.MemberExistAsync(testMember.IDMember);
            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task MemberExistsAsync_ShouldReturnFalse_When_MemberDoesNotExist()
        {
            // Arrange
            var nonExistentUsername = Guid.NewGuid();
            // Act
            var exists = await _membersRepository.MemberExistAsync(nonExistentUsername);
            // Assert
            Assert.False(exists);
        }
        #endregion

        #region UpdatePartiallyMemberAsync Tests
        [Fact]
        public async Task UpdatePartiallyMember_MemberExistNoValidationError_MemberUpdatedAsync()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);
            testMember.Name = "UpdatedName";
            // Act
            var updatedMember = await _membersRepository.UpdateMemberAsync(testMember);
            // Assert
            Assert.NotNull(updatedMember);
            Assert.Equal(testMember.IDMember, updatedMember.IDMember);
            Assert.Equal("UpdatedName", updatedMember.Name);
        }
        #endregion

        #region DeleteMemberAsync Tests
        [Fact]
        public async Task DeleteMemberAsync_ShouldDeleteMember_WhenMemberExists()
        {
            // Arrange
            var testMember = await DBContextHelper.AddTestMember(_contextInMemory);
            // Act
            var deleted = await _membersRepository.DeleteMemberAsync(testMember.IDMember);
            // Assert
            Assert.True(deleted);
            var memberAfterDeletion = await _membersRepository.GetMemberByIdAsync(testMember.IDMember);
            Assert.Null(memberAfterDeletion);
        }
        [Fact]
        public async Task DeleteMemberAsync_ShouldReturnFalse_WhenMemberDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            // Act
            var deleted = await _membersRepository.DeleteMemberAsync(nonExistentId);
            // Assert
            Assert.False(deleted);
        }

        [Fact]
        public async Task DeleteMemberAsync_ShouldReturnFalse_WhenInvalIDMemberProvided()
        {
            // Arrange
            var invalIDMember = new Member(); // Invalid member with no IDMember set
            // Act
            var deleted = await _membersRepository.DeleteMemberAsync(invalIDMember.IDMember);
            // Assert
            Assert.False(deleted);
        }
        #endregion
    }
}
