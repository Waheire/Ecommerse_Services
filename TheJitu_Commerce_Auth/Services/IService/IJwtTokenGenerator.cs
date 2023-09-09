using TheJitu_Commerce_Auth.Model;

namespace TheJitu_Commerce_Auth.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
