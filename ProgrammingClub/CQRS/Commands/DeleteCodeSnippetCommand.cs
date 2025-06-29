using MediatR;

namespace ProgrammingClub.CQRS.Commands
{
    public class DeleteCodeSnippetCommand : IRequest<bool>
    {
        public Guid IdCodeSnippet { get; set; }
        public DeleteCodeSnippetCommand(Guid idCodeSnippet)
        {
            IdCodeSnippet = idCodeSnippet;
        }
    }
}
