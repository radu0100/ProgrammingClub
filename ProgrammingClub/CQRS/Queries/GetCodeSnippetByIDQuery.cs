using MediatR;
using ProgrammingClub.Models;

namespace ProgrammingClub.CQRS.Queries
{
    public class GetCodeSnippetByIDQuery : IRequest<CodeSnippet>
    {
        public Guid IdCodeSnippet { get; set; }
        public GetCodeSnippetByIDQuery(Guid idCodeSnippet)
        {
            IdCodeSnippet = idCodeSnippet;
        }
    }
}
