using Microsoft.AspNetCore.Mvc;
using PBL5BE.API.Data.DTO;
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

            if (isSuccess) 
            {
                var newUser = _userService.GetUserByEmail(userLogin.Email);

                _userInfoService.CreateUserInfo(newUser);

                var _code = "TestCODE31";
                _ = SendMail.SendVerificationMail(userLogin.Email, _code);
            }

            return Ok(isSuccess);
        }
        
        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] UserLogin userLogin)
        {
            return Ok(_userService.LoginUser(userLogin));
        }

        [HttpGet("getUsers")]
        public IActionResult GetAllUser() 
        {
            return Ok(_userService.GetUsers());
        }
    }
}