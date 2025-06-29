using MediatR;
using ProgrammingClub.CQRS.Commands;
using ProgrammingClub.DataContext;

namespace ProgrammingClub.CQRS.Handlers
{
    public class DeleteCodeSnippetHandler : IRequestHandler<DeleteCodeSnippetCommand, bool>
    {
        private readonly ProgrammingClubDataContext _context;
        public DeleteCodeSnippetHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteCodeSnippetCommand request, CancellationToken cancellationToken)
        {
            var codeSnippet = await _context.CodeSnippets.FindAsync(request.IdCodeSnippet);
            if (codeSnippet == null)
            {
                return false; // Not found
            }
            _context.CodeSnippets.Remove(codeSnippet);
            await _context.SaveChangesAsync(cancellationToken);
            return true; // Successfully deleted
        }
    }
}
