using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProgrammingClub.CQRS.Queries;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Handlers
{
    public class GetAllMembershipTypesHandler : IRequestHandler<GetAllMembershipTypesQuery, IEnumerable<MembershipType>>
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMemoryCache _cache;
        public GetAllMembershipTypesHandler(ProgrammingClubDataContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<MembershipType>> Handle(GetAllMembershipTypesQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "AllMembershipTypes";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<MembershipType> cachedMembershipTypes))
            {
                return cachedMembershipTypes;
            }

            return await _context.MembershipTypes.ToListAsync(cancellationToken);
        }
    }
}