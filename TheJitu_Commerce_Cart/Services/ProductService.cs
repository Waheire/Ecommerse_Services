using Newtonsoft.Json;
using TheJitu_Commerce_Cart.Model.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Services
{
    public class ProductService : IProductInterface
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            //create a client
            var client = _clientFactory.CreateClient("Product");
            var response = await client.GetAsync("/Product/getProducts");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (responseDto.IsSuccess) 
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}
