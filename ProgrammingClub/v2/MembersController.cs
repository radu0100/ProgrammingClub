using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Helpers;
using ProgrammingClub.Services;
using ProgrammingClub.v2.DTOs;
using System.Net;

namespace ProgrammingClub.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class MembersController : ControllerBase
    {
        private readonly iMembersService _membersService;
        private readonly IMapper _mapper;
        public MembersController(iMembersService membersService, IMapper mapper)
        {
            _membersService = membersService;
            _mapper = mapper;
        }

        // GET api/<MembersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var members = await _membersService.GetAllMembersAsync();
                if (members.Count() <= 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, ErrorMessagesEnum.NoMembersFound);
                }
                var membersV2s = _mapper.Map<IEnumerable<MembersV2DTO>>(members);
                return Ok(membersV2s);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
