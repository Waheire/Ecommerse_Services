using AutoMapper;
using TheJitu_Commerce_Cart.Model;
using TheJitu_Commerce_Cart.Model.Dtos;

namespace TheJitu_Commerce_Cart.Profiles
{
    public class CartProfiles: Profile
    {
        public CartProfiles()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
