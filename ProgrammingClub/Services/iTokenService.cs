using Microsoft.AspNetCore.Identity;

namespace ProgrammingClub.Services
{
    public interface iTokenService
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
