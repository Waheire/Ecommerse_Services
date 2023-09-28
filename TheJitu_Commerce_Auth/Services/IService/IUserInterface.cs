using TheJitu_Commerce_Auth.Model;
using TheJitu_Commerce_Auth.Model.Dtos;

namespace TheJitu_Commerce_Auth.Services.IService
{
    public interface IUserInterface
    {
        //Register user
        Task<string> RegisterUser(RegisterRequestDto registerRequestDto);
        //Login user
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        //Admin crud operations
        //assign user roles
        Task<bool> AssignUserRole(string email, string RoleName);
        //update user 
        Task<string> UpdateUser(ApplicationUser applicationUser);
        //Delete user
        Task<string> DeleteUser(ApplicationUser applicationUser);
    }
}
