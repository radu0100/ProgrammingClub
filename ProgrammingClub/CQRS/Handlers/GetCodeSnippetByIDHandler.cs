using MediatR;
using ProgrammingClub.CQRS.Queries;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Handlers
{
    public class GetCodeSnippetByIDHandler : IRequestHandler<GetCodeSnippetByIDQuery, CodeSnippet>
    {
        private readonly ProgrammingClubDataContext _context;
        public GetCodeSnippetByIDHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<CodeSnippet> Handle(GetCodeSnippetByIDQuery request, CancellationToken cancellationToken)
        {
            var codeSnippet = await _context.CodeSnippets.FindAsync(request.IdCodeSnippet);
            if (codeSnippet == null)
            {
                throw new KeyNotFoundException($"Code snippet with ID {request.IdCodeSnippet} not found.");
            }
            return codeSnippet;
        }
    }
}
