using AutoMapper;
using TheJitu_Commerce_Auth.Model;
using TheJitu_Commerce_Auth.Model.Dtos;

namespace TheJitu_Commerce_Auth.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles() 
        {
            CreateMap<RegisterRequestDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
