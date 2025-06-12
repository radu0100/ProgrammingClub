using ProgrammingClub.Models;

namespace ProgrammingClub.UnitTests.Models.Builders
{
    internal class MemberBuilder : BuilderBase<Member>
    {
        public MemberBuilder()
        {
            _objectToBuild = new Member
            {
                IDMember = Guid.NewGuid(),
                Name = "John",
                Title = "Member",
                Position = "Developer",
                Description = "A passionate developer with a love for coding.",
                Resume = "https://example.com/resume.pdf",
                Username = "john_doe",
                Password = "securepassword123"
            };
        }
    }
}
