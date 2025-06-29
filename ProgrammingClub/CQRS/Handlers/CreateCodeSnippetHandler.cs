using MediatR;
using ProgrammingClub.CQRS.Commands;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Handlers
{
    public class CreateCodeSnippetHandler : IRequestHandler<CreateCodeSnippetCommand, Guid>
    {
        private readonly ProgrammingClubDataContext _context;

        public CreateCodeSnippetHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateCodeSnippetCommand request, CancellationToken cancellationToken)
        {
            var codeSnippet = new CodeSnippet
            {
                IDCodeSnippet = Guid.NewGuid(),
                Title = request.Dto.Title,
                ContentCode = Guid.NewGuid(),
                IDMember = request.Dto.IDMember,
                Revision = request.Dto.Revision,
                IDSnippetPreviousVersion = Guid.NewGuid(),
                DateTimeAdded = DateTime.UtcNow,
                IsPublished = request.Dto.IsPublished
            };

            _context.CodeSnippets.Add(codeSnippet);
            await _context.SaveChangesAsync(cancellationToken);
            return codeSnippet.IDCodeSnippet;
        }
    }
}
