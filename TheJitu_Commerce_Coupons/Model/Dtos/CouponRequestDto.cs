namespace TheJitu_Commerce_Coupons.Model.Dtos
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public int CouponAmount { get; set; }
        public int CouponMinAmount { get; set; }
    }
}
