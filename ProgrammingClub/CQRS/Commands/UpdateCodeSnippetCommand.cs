using MediatR;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Commands
{
    public class UpdateCodeSnippetCommand : IRequest<CodeSnippet>
    {
        public CodeSnippet Dto { get; set; }
        public UpdateCodeSnippetCommand(CodeSnippet dto)
        {
            Dto = dto;
        }
    }
}
