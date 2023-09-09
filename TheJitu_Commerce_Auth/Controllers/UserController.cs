using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Auth.Model.Dtos;
using TheJitu_Commerce_Auth.Services.IService;

namespace TheJitu_Commerce_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        private readonly ResponseDto _responseDto;
        public UserController(IUserInterface userInterface) 
        {
            _userInterface = userInterface;
            //Don't inject just Initialize
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> AddUser(RegisterRequestDto registerRequestDto) 
        {
            var errorMessage = await _userInterface.RegisterUser(registerRequestDto);
            if (!string.IsNullOrWhiteSpace(errorMessage)) 
            {
                //error
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var response = await _userInterface.Login(loginRequestDto);
            if (response.User == null)
            {
                //error
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Credentials";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = response;
            return Ok(_responseDto);
        }


        [HttpPost("Assign Role")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterRequestDto registerRequestDto)
        {
            var response = await _userInterface.AssignUserRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!response)
            {
                //error
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occurred.";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
    }
}
