using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Cart.Model.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartInterface _cartService;
        private readonly ResponseDto _responseDto;

        public CartController(ICartInterface cartInterface)
        {
            _cartService = cartInterface;
            _responseDto = new ResponseDto();
        }


        [HttpPost("updateCart")]
        public async Task<ActionResult<ResponseDto>> CartUpSert(CartDto cartDto) 
        {
            try 
            {
                var response = await _cartService.CartUpSert(cartDto);
                if (response)
                {
                    _responseDto.Result = response;
                }
                else 
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            } 
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

    }
}
