using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Auth.Data;
using TheJitu_Commerce_Auth.Model;
using TheJitu_Commerce_Auth.Model.Dtos;
using TheJitu_Commerce_Auth.Services.IService;

namespace TheJitu_Commerce_Auth.Services
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(AppDbContext database, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper )
        {
            _context = database;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<bool> AssignUserRole(string email, string RoleName)
        {
            //check if user exists by checking email
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null) 
            {
                //user exists
                //check if role exists
                if (!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult()) 
                {
                    //create role
                    _roleManager.CreateAsync(new IdentityRole(RoleName)).GetAwaiter().GetResult();
                    
                }
                //link user to role
                await _userManager.AddToRoleAsync(user, RoleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //Get user by username
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            //Check if password is the right one
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            //check if user is null or password is wrong
            if (!isValid || user == null) 
            {
                return new LoginResponseDto();
            }

            //user provided right credentials
            //create token

            var LoggedUser = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = "Coming Soon"
            };
            return LoggedUser;
        }

        public async Task<string> RegisterUser(RegisterRequestDto registerRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerRequestDto);

            try 
            {
                //user is created
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    //if success return empty string
                    return "";
                }
                else 
                {
                    //get identity error if any
                    return result.Errors.FirstOrDefault().Description;
                }
            } catch(Exception ex) 
            {
                return ex.Message;
            }
        }
    }
}
