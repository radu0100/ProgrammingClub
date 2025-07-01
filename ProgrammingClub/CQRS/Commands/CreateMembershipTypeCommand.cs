using MediatR;
using ProgrammingClub.CQRS.DTOs;

namespace ProgrammingClub.CQRS.Commands
{
    public class CreateMembershipTypeCommand : IRequest<Guid>
    {
        public MembershipTypeDto Dto { get; set; }
        public CreateMembershipTypeCommand(MembershipTypeDto dto)
        {
            Dto = dto;
        }
    }
}
