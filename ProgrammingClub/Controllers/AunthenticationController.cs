using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClub.Models.AuthenticationDTOs;
using ProgrammingClub.Services;

namespace ProgrammingClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AunthenticationController : ControllerBase
    {
        private readonly iTokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public AunthenticationController(iTokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Username
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if(result.Succeeded) {
                if(user.Roles != null && user.Roles.Length > 0)
                {
                    foreach (var role in user.Roles)
                    {
                        await _userManager.AddToRoleAsync(identityUser, role);
                    }
                    }
                return Ok(new { message = "User registered" });
            }
            return BadRequest(result.Errors);
            }
    }
}
