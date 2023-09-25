using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Cart.Model.Dtos
{
    public class CouponDto
    {
        public Guid CouponId { get; set; }
    
        public string CouponCode { get; set; } = string.Empty;
       
        public int CouponAmount { get; set; }
       
        public int CouponMinAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
