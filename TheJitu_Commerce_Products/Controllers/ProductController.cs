using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Products.Model;
using TheJitu_Commerce_Products.Model.Dtos;
using TheJitu_Commerce_Products.Services;
using TheJitu_Commerce_Products.Services.IService;

namespace TheJitu_Commerce_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductInterface _productInterface;
        private readonly ResponseDto _responseDto;

        public ProductController(IMapper mapper, IProductInterface productInterface)
        {
            _mapper = mapper;
            _productInterface = productInterface;
            _responseDto = new ResponseDto();
        }

        [HttpGet("Products")]
        public async Task<ActionResult<ResponseDto>> GetAllProducts() 
        {
            var products = await _productInterface.GetProductsAsync();
            if (products == null) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = products;
            return Ok(_responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddProduct(ProductRequestDto addProductRequestDto) 
        {
            var newProduct = _mapper.Map<Product>(addProductRequestDto);
            var response = await _productInterface.AddProductAsync(newProduct);
            if (string.IsNullOrWhiteSpace(response))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            //product added
            _responseDto.Result = response;
            return Ok(_responseDto);
        }

        [HttpGet("GetByName{productName}")]
        public async Task<ActionResult<ResponseDto>> GetProductByName(string productName) 
        {
            var product = await _productInterface.GetProductByNameAsync(productName);
            if (product == null) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = product;
            return Ok(_responseDto);
        }

        [HttpGet("GetById{productId}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(Guid productId)
        {
            var product = await _productInterface.GetProductByIdAsync(productId);
            if (product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = product;
            return Ok(_responseDto);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid productId, ProductRequestDto productRequestDto)
        {
            var product = await _productInterface.GetProductByIdAsync(productId);
            if (product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            //update
            var updated = _mapper.Map(productRequestDto, product);
            var response = await _productInterface.UpdateProductAsync(updated);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid productId)
        {
            var product = await _productInterface.GetProductByIdAsync(productId);
            if (product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred";
                return BadRequest(_responseDto);
            }
            //Delete
            var response = await _productInterface.DeleteProductAsync(product);
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
    }
}
