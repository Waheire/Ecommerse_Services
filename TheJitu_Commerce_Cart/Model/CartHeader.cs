using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheJitu_Commerce_Cart.Model
{
    public class CartHeader
    {
        [Key]
        public Guid cartHeaderId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; } = string.Empty;
        //do not create a column for this
        [NotMapped]
        public int Discount { get; set; }
        [NotMapped]
        public int CartTotal { get; set; }

    }
}
