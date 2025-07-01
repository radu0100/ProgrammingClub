using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.CQRS.Commands;
using ProgrammingClub.CQRS.DTOs;
using ProgrammingClub.CQRS.Queries;
using ProgrammingClub.Models;


namespace ProgrammingClub.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CodeSnippetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CodeSnippetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/CodeSnippetController>/1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeSnippet>>> GetAllCodeSnippets()
        {
            var query = new GetAllCodeSnippetQuery();
            var CodeSnippets = await _mediator.Send(query);
            return Ok(CodeSnippets);
        }

        // GET api by ID/<CodeSnippetController>/2
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeSnippet>> GetCodeSnippetById(Guid id)
        {
            var query = new GetCodeSnippetByIDQuery(id);
            var CodeSnippet = await _mediator.Send(query);

            if (CodeSnippet == null)
            {
                return NotFound();
            }
            return Ok(CodeSnippet);
        }

        // POST api/<CodeSnippetController>/3
        [HttpPost]
        public async Task<IActionResult> CreateCodeSnippet(CodeSnippetDto dto)
        {
            var command = new CreateCodeSnippetCommand(dto);
            var CodeSnippetId = await _mediator.Send(command);
            return Ok(CodeSnippetId);
        }

        // DELETE api/<CodeSnippetController>/4
        [HttpDelete]
        public async Task<IActionResult> DeleteCodeSnippet(Guid id)
        {
            var command = new DeleteCodeSnippetCommand(id);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Removed");
            }
            return NotFound();
        }

        // PUT api/<CodeSnippetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCodeSnippet(Guid id, CodeSnippet model)
        {
            var command = new UpdateCodeSnippetCommand(model);
            command.Dto.IDCodeSnippet = id;
            var updatedCodeSnippet = await _mediator.Send(command);
            return Ok(updatedCodeSnippet);
        }
    }
}
