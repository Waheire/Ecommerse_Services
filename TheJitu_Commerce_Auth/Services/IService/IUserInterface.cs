using TheJitu_Commerce_Auth.Model.Dtos;

namespace TheJitu_Commerce_Auth.Services.IService
{
    public interface IUserInterface
    {
        Task<string> RegisterUser(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignUserRole(string email, string RoleName);
    }
}
