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
        private readonly IProductInterface _productInterface;
        private readonly ICouponInterface _couponInterface;

        public CartService(AppDbContext context, IMapper mapper , IProductInterface productInterface, ICouponInterface couponInterface)
        {
            _appDbContext = context;
            _mapper = mapper;
            _productInterface = productInterface;
            _couponInterface = couponInterface;
        }

        public async Task<bool> ApplyCoupons(CartDto cartDto)
        {
            //get the header
            CartHeader CartHeaderFromDb = await _appDbContext.CartHeader.FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);

            CartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            _appDbContext.CartHeader.Update(CartHeaderFromDb);
            await _appDbContext.SaveChangesAsync();
            return true;
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
                //assign CartHeaderId
                cartDto.CartDetails.First().CartHeaderId = newCartHeader.CartHeaderId;
                var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                _appDbContext.CartDetails.Add(cartDetails);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            else 
            {
                //I'm either adding a new Item or updating the count of an existing Item
                CartDetails CartDetailsFromDb = await _appDbContext.CartDetails.FirstOrDefaultAsync(
                    x => x.ProductId == cartDto.CartDetails.First().ProductId &&
                x.CartHeaderId == CartHeaderFromDb.CartHeaderId);

                if (CartDetailsFromDb == null)
                {
                    //it's a different product to be added to cart list
                    cartDto.CartDetails.First().CartHeaderId = CartHeaderFromDb.CartHeaderId;
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


        public async Task<CartDto> GetUserCart(Guid userId)
        {
            var cartHeader = await _appDbContext.CartHeader.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartDetails =  _appDbContext.CartDetails.Where(x => x.CartHeaderId == cartHeader.CartHeaderId);
            CartDto cart = new CartDto()
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails),
            };

            //calculate Cart Total
             var products =await _productInterface.GetProductsAsync();
            foreach (var item in cart.CartDetails) 
            {
                item.Product = products.FirstOrDefault(x => x.ProductId == item.ProductId);
                cart.CartHeader.CartTotal += (int)(item.Count * item.Product.ProductPrice);
            }

            //check if there is a coupon
            if (!string.IsNullOrWhiteSpace(cart.CartHeader.CouponCode))
            {
                //there is a coupon
                var coupon = await _couponInterface.GetCouponData(cart.CartHeader.CouponCode);
                if (coupon != null && cart.CartHeader.CartTotal > coupon.CouponMinAmount) 
                {
                    cart.CartHeader.CartTotal -= coupon.CouponAmount;
                    cart.CartHeader.Discount = coupon.CouponAmount;
                }
            }
            return cart;
        }

        public async Task<bool> RemoveFromCart(Guid CartDetailId)
        {
            CartDetails cartDetails = await _appDbContext.CartDetails.FirstOrDefaultAsync(x => x.CartDetailsId == CartDetailId );

            //is this the last item the user is deleting
            var itemCount = _appDbContext.CartDetails.Where(c => c.CartHeaderId == cartDetails.CartHeaderId).Count();

            _appDbContext.CartDetails.Remove(cartDetails);

            if (itemCount == 1) 
            {
                _appDbContext.CartHeader.Remove(_appDbContext.CartHeader.FirstOrDefault(x => x.CartHeaderId == cartDetails.CartHeaderId));
            }
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
