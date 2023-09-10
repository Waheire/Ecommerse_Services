using AutoMapper;
using TheJitu_Commerce_Products.Model;
using TheJitu_Commerce_Products.Model.Dtos;

namespace TheJitu_Commerce_Products.Profiles
{
    public class ProductProfiles:Profile
    {
        
        public ProductProfiles()
        {
            CreateMap<ProductRequestDto, Product>().ReverseMap();
        }
    }
}
