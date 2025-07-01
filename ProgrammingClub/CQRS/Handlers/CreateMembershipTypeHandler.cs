using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.CQRS.Commands;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;


namespace ProgrammingClub.CQRS.Handlers
{
    public class CreateMembershipTypeHandler : IRequestHandler<CreateMembershipTypeCommand, Guid>
    {
        private readonly ProgrammingClubDataContext _context;

        public CreateMembershipTypeHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMembershipType = await _context.MembershipTypes
      .AnyAsync(mt => mt.Name == request.Dto.Name);

            if (existingMembershipType)
            {
                throw new DuplicateWaitObjectException(request.Dto.Name);
            }
            var membershipType = new MembershipType
            {
                IDMembershipType = Guid.NewGuid(),
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                SubscriptionLengthInMonths = request.Dto.SubscriptionLengthInMonths
            };

            _context.MembershipTypes.Add(membershipType);
            await _context.SaveChangesAsync(cancellationToken);
            return membershipType.IDMembershipType;
        }
    }
}