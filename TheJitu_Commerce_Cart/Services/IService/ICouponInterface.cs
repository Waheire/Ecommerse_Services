using TheJitu_Commerce_Cart.Model.Dtos;

namespace TheJitu_Commerce_Cart.Services.IService
{
    public interface ICouponInterface
    {
        Task<CouponDto> GetCouponData(string couponCode);
    }
}
