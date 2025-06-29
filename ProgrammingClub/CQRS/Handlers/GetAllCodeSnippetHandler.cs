using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProgrammingClub.CQRS.Queries;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Handlers
{
    public class GetAllCodeSnippetHandler : IRequestHandler<GetAllCodeSnippetQuery, IEnumerable<CodeSnippet>>
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMemoryCache _cache;
        public GetAllCodeSnippetHandler(ProgrammingClubDataContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<CodeSnippet>> Handle(GetAllCodeSnippetQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "AllCodeSnippet";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<CodeSnippet> cacheCodeSnippet))
            {
                return cacheCodeSnippet;
            }

            return await _context.CodeSnippets.ToListAsync(cancellationToken);
        }
    }
}
