using MediatR;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Commands
{
        public class UpdateMembershipTypeCommand : IRequest<MembershipType>
        {
            public MembershipType Dto { get; set; }
            public UpdateMembershipTypeCommand(MembershipType dto)
            {
                Dto = dto;
            }
        }
}
