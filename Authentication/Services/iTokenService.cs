using Microsoft.AspNetCore.Identity;

namespace Authentication.Services
{
    public interface iTokenService
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
