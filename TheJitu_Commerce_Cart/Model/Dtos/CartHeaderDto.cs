using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Cart.Model.Dtos
{
    public class CartHeaderDto
    {
        public Guid cartHeader { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; } = string.Empty;
        public int Discount { get; set; }
        public int CartTotal { get; set; }
    }
}
