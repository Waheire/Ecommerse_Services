using TheJitu_Commerce_Cart.Model.Dtos;

namespace TheJitu_Commerce_Cart.Services.IService
{
    public interface ICartInterface
    {
        Task<bool> CartUpSert(CartDto cartDto);
        Task<CartDto> GetUserCart(Guid userId);
        Task<bool> ApplyCoupons (CartDto cartDto);
        Task<bool> RemoveFromCart(Guid cartDetailId);
    }
}
