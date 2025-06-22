using MediatR;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Queries
{
    public class GetAllMembershipTypesQuery : IRequest<IEnumerable<MembershipType>>
    {



    }
}
