﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Auth.Model.Dtos;
using TheJitu_Commerce_Auth.Services.IService;
using TheJituMessageBus;

namespace TheJitu_Commerce_Auth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        private readonly ResponseDto _responseDto;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public UserController(IUserInterface userInterface, IMessageBus messageBus, IConfiguration configuration) 
        {
            _userInterface = userInterface;
            //Don't inject just Initialize
            _responseDto = new ResponseDto();
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("register")]
        //[AutoValidateAntiforgeryToken]
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
            //send a message to our service bus  - Queue
            var queueName = _configuration.GetSection("QueuesAndTopics:RegisterUser").Get<string>();
            var message = new UserMessage()
            {
                Email = registerRequestDto.Email,
                Name = registerRequestDto.Name
            };

            _messageBus.PublishMessage(message, queueName);
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


        [HttpPost("Assign-Role")]
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

        //[HttpDelete]
        //public async Task<ActionResult<ResponseDto>> DeleteUser() 
        //{
        //    var user = await _userInterface.
        //}
    }
}
