using TheJitu_Commerce_Coupons.Model;

namespace TheJitu_Commerce_Coupons.Services.IService
{
    public interface ICouponInterface
    {
        Task<IEnumerable<Coupon>> GetCouponsAsync();
        Task<Coupon> GetCouponByIdAsync(Guid id);
        Task<Coupon> GetCouponByNameAsync(string Code);
        Task<string> AddCouponAsync(Coupon coupon);
        Task<string> UpdateCouponAsync(Coupon coupon);
        Task<string>DeleteCouponAsync(Coupon coupon);

    }
}
