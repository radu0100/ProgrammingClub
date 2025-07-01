using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.CQRS.Commands;
using ProgrammingClub.CQRS.DTOs;
using ProgrammingClub.CQRS.Queries;
using ProgrammingClub.CustomException;
using ProgrammingClub.Models;

namespace ProgrammingClub.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class MembershipTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MembershipTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/MembershipTypesController>/1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipType>>> GetAllMembershipTypes()
        {
            var query = new GetAllMembershipTypesQuery();
            var membershipTypes = await _mediator.Send(query);
            return Ok(membershipTypes);
        }

        // GET api by ID/<MembershipTypeController>/2
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipType>> GetMembershipTypeById(Guid id)
        {
            var query = new GetMembershipTypeByIdQuery(id);
            var membershipType = await _mediator.Send(query);

            if (membershipType == null)
            {
                return NotFound();
            }
            return Ok(membershipType);
        }

        // POST api/<MembershipTypeController>/3
        [HttpPost]
        public async Task<IActionResult> CreateMembershipType(MembershipTypeDto dto)
        {
            try
            {
                var command = new CreateMembershipTypeCommand(dto);
                var membershipTypeId = await _mediator.Send(command);
                return Ok(membershipTypeId);
            }

            catch (DuplicateMembershipTypeException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // DELETE api/<MembershipTypeController>/4
        [HttpDelete]
        public async Task<IActionResult> DeleteMembershipType(Guid id)
        {
            var command = new DeleteMembershipTypeCommand(id);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Removed");
            }
            return NotFound();
        }

        // PUT api/<MembershipTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembershipType(Guid id, MembershipType model)
        {
            var command = new UpdateMembershipTypeCommand(model);
            command.Dto.IDMembershipType = id;
            var updatedMembershipType = await _mediator.Send(command);
            return Ok(updatedMembershipType);
        }
    }
}
