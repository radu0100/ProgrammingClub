using MediatR;
using ProgrammingClub.CQRS.DTOs;

namespace ProgrammingClub.CQRS.Commands
{
    public class CreateMembershipTypeCommand : IRequest<Guid>
    {

        public MembershipTypeDTO Dto { get; set; }
        public CreateMembershipTypeCommand(MembershipTypeDTO dto)
        {
            Dto = dto;
        }

    }
}
