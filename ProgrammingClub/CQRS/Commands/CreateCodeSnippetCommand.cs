using MediatR;
using ProgrammingClub.CQRS.DTOs;

namespace ProgrammingClub.CQRS.Commands
{
    public class CreateCodeSnippetCommand : IRequest<Guid>
    {
        public CodeSnippetDTO Dto { get; set; }
        public CreateCodeSnippetCommand(CodeSnippetDTO dto)
        {
            Dto = dto;
        }
    }
}
