using Newtonsoft.Json;
using TheJitu_Commerce_Cart.Model.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Services
{
    public class CouponService : ICouponInterface
    {
        private readonly IHttpClientFactory _clientFactory;
        public CouponService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<CouponDto> GetCouponData(string couponCode)
        {
            //create a client
            var client = _clientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/Coupon/GetByName/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return new CouponDto();
        }
    }
}
