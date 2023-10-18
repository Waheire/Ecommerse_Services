using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Coupons.Model;
using TheJitu_Commerce_Coupons.Model.Dtos;
using TheJitu_Commerce_Coupons.Services.IService;

namespace TheJitu_Commerce_Coupons.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponInterface _couponInterface;
        private readonly ResponseDto _responseDto;
        public CouponController(IMapper mapper, ICouponInterface couponInterface)
        {
            _couponInterface = couponInterface; 
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet("getCoupons")]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons() 
        {
            var coupons = await _couponInterface.GetCouponsAsync();
            if (coupons == null) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = coupons;
            return Ok(_responseDto);
        }

        [HttpPost("add")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ResponseDto>> AddCoupon(CouponRequestDto couponRequestDto)
        {
            var newCoupon = _mapper.Map<Coupon>(couponRequestDto);
            var response = await _couponInterface.AddCouponAsync(newCoupon);
            if (string.IsNullOrWhiteSpace(response))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = response;
            return Ok(_responseDto);
        }

        [HttpGet("GetByName/{code}")]
        public async Task<ActionResult<ResponseDto>> GetCouponByName(string code)
        {
            var coupon = await _couponInterface.GetCouponByNameAsync(code);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }

        [HttpGet("GetById{couponId}")]
        public async Task<ActionResult<ResponseDto>> GetCouponById(Guid couponId)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(couponId);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }


        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateCoupon(Guid id, CouponRequestDto couponRequestDto)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            //update
            var updated = _mapper.Map(couponRequestDto, coupon);
            var response = await _couponInterface.UpdateCouponAsync(updated);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid id)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            //Delete
            var response = await _couponInterface.DeleteCouponAsync(coupon);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
    }
}
