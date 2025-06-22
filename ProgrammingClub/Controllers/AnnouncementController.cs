using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Helpers;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;
using ProgrammingClub.Services;
using System.Net;


namespace ProgrammingClub.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class AnnouncementController : ControllerBase
    {
        private readonly iAnnouncementService _announcementService;
        public AnnouncementController(iAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // GET: api/<AnnouncementsController>
        [HttpGet]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var announcement = await _announcementService.GetAllAnnouncementsAsync();
                if (announcement.Count() <= 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, ErrorMessagesEnum.NoAnnouncementFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api by ID/<AnnouncementsController>/5
        [HttpGet("{id:guid}")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.AnnouncementNotFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<AnnouncementController>
        [HttpPost]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> AddAnnouncement([FromBody] Announcement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
                }

                await _announcementService.AddAnnouncementAsync(announcement);
                return StatusCode((int)HttpStatusCode.Created, SuccessMessagesEnum.AnnouncementAdded);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<announcementsController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Announcement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
                }
                var updatedannouncement = await _announcementService.UpdateAnnouncementAsync(id, announcement);

                if (updatedannouncement != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.AnnouncementUpdated);
                }

                return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.NoAnnouncementFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> PatchAnnouncement(Guid id, [FromBody] UpdateAnnouncementPartially announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
                }
                Announcement updateannouncement = await _announcementService.UpdateAnnouncementPartiallyAsync(id, announcement);
                if (updateannouncement != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.AnnouncementAdded);
                }
                return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.AnnouncementNotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _announcementService.DeleteAnnouncementAsync(id);
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.AnnouncementRemoved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}