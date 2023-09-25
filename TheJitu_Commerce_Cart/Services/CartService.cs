using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Cart.Data;
using TheJitu_Commerce_Cart.Model;
using TheJitu_Commerce_Cart.Model.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Services
{
    public class CartService : ICartInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CartService(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public Task<bool> ApplyCoupon(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CartUpSert(CartDto cartDto)
        {
            //check if the item is the first to be added by the user
            CartHeader CartHeaderFromDb = await _appDbContext.CartHeader.FirstOrDefaultAsync( x => x.UserId == cartDto.CartHeader.UserId);
            if (CartHeaderFromDb == null)
            {
                //create CartHeader and CartDetails
                var newCartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                _appDbContext.CartHeader.Add(newCartHeader);
                await _appDbContext.SaveChangesAsync();

                //Use Id above for CartDetails
                //assign CartHeader
                cartDto.CartDetails.First().CartHeaderId = newCartHeader.cartHeaderId;
                var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                _appDbContext.CartDetails.Add(cartDetails);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            else 
            {
                //I'm either adding a new Item or updating the count of an existing Item
                CartDetails CartDetailsFromDb = await _appDbContext.CartDetails.FirstOrDefaultAsync(x => x.ProductId == cartDto.CartDetails.First().ProductId &&
                x.CartDetailsId == CartHeaderFromDb.cartHeaderId);

                if (CartDetailsFromDb == null)
                {
                    //it's a different product add to cart list
                    cartDto.CartDetails.First().CartHeaderId = CartHeaderFromDb.cartHeaderId;
                    var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    _appDbContext.CartDetails.Add(cartDetails);
                    await _appDbContext.SaveChangesAsync();

                }
                else 
                {
                    //update count
                    CartDetailsFromDb.Count += cartDto.CartDetails.First().Count;
                    _appDbContext.CartDetails.Update(CartDetailsFromDb);
                    await _appDbContext.SaveChangesAsync();
                }
                return true;
            }
            return false;
           
        }

        public Task<CartDto> GetUserCart(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(int CartDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
