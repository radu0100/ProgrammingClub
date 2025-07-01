using MediatR;
using ProgrammingClub.CQRS.DTOs;

namespace ProgrammingClub.CQRS.Commands
{
    public class CreateCodeSnippetCommand : IRequest<Guid>
    {
        public CodeSnippetDto Dto { get; set; }
        public CreateCodeSnippetCommand(CodeSnippetDto dto)
        {
            Dto = dto;
        }
    }
}
