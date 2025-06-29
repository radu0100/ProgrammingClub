using MediatR;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Queries
{
    public class GetAllCodeSnippetQuery : IRequest<IEnumerable<CodeSnippet>>
    {
    }
}
