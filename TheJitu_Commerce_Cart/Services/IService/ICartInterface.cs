using TheJitu_Commerce_Cart.Model.Dtos;

namespace TheJitu_Commerce_Cart.Services.IService
{
    public interface ICartInterface
    {
        Task<bool> CartUpSert(CartDto cartDto);
        Task<CartDto> GetUserCart(CartDto cartDto);
        Task<bool> ApplyCoupon (CartDto cartDto);
        Task<bool> RemoveFromCart(int CartDetailId);
    }
}
