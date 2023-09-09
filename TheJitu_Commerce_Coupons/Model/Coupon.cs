using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Coupons.Model
{
    public class Coupon
    {
        [Required]
        public Guid CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        public int CouponAmount { get; set; }
        [Required]
        public int CouponMinAmount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
