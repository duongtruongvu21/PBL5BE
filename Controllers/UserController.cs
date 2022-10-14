using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Services;
using PBL5BE.API.Services._User;
using PBL5BE.API.Services._UserInfo;

namespace PBL5BE.API.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("_myCORS")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserInfoService _userInfoService;

        public UserController(IUserService userService, IUserInfoService userInfoService)
        {
            _userService = userService;
            _userInfoService = userInfoService;
        }
        
        [HttpPost("UserRegister")]
        public IActionResult Register([FromBody] UserLogin userLogin)
        {
            var isSuccess = _userService.CreateUser(userLogin);

            if (isSuccess == 1) 
            {
                var newUser = _userService.GetUserByEmail(userLogin.Email);

                _userInfoService.CreateUserInfo(newUser);

                var _code = "TestCODE31";
                _ = SendMail.SendVerificationMail(userLogin.Email, _code);
            }

            var returnData = new ReturnData() {
                isSuccess = isSuccess,
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }
        
        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] UserLogin userLogin)
        {
            var isSuccess = _userService.LoginUser(userLogin);
            User user = null;
            UserInfo userinfo = null;
            if(isSuccess == 1) 
            {
                user = _userService.GetUserByEmail(userLogin.Email);
                userinfo = _userInfoService.GetUserInfoByID(user.ID);
            }

            var returnData = new ReturnData() {
                isSuccess = isSuccess,
                Data = new List<object>() {
                    userinfo,
                }
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers() 
        {
            var returnData = new ReturnData() {
                isSuccess = 1,
                Data = new List<object>(_userService.GetUsers())
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}