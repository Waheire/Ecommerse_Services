using AutoMapper;
using TheJitu_Commerce_Coupons.Model;
using TheJitu_Commerce_Coupons.Model.Dtos;

namespace TheJitu_Commerce_Coupons.Profiles
{
    public class CouponsProfile:Profile
    {
        public CouponsProfile()
        {
            CreateMap<CouponRequestDto, Coupon>().ReverseMap();
        }
    }
}
