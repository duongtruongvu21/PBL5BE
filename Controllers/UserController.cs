using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services;
using PBL5BE.API.Services._Token;
using PBL5BE.API.Services._User;
using PBL5BE.API.Services._UserInfo;

namespace PBL5BE.API.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("_myCORS")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserInfoService _userInfoService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, IUserInfoService userInfoService, ITokenService tokenService)
        {
            _userService = userService;
            _userInfoService = userInfoService;
            _tokenService = tokenService;
        }

        [HttpPost("UserRegister")]
        public IActionResult Register([FromBody] UserLogin userLogin)
        {
            var isSuccess = _userService.CreateUser(userLogin);

            var returnData = new ReturnData();
            if (isSuccess == STTCode.Success)
            {
                var newUser = _userService.GetUserByEmail(userLogin.Email);

                _userInfoService.CreateUserInfo(newUser);

                var _code = "TestCODE31";
                _ = SendMail.SendVerificationMail(userLogin.Email, _code);
                returnData.isSuccess = true;
            }
            else
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpPost("UserRegisterFull")]
        public IActionResult RegisterFull([FromForm] UserRegister userRegister)
        {
            var userLogin = new UserLogin() { Email = userRegister.Email, Password = userRegister.Password };
            var isSuccess = _userService.CreateUser(userLogin);

            var returnData = new ReturnData();
            if (isSuccess == STTCode.Success)
            {
                var newUser = _userService.GetUserByEmail(userLogin.Email);
                _userInfoService.CreateUserInfo(newUser, userRegister);

                var _code = "TestCODE31";
                _ = SendMail.SendVerificationMail(userLogin.Email, _code);
                returnData.isSuccess = true;
            }
            else
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] UserLogin userLogin)
        {
            var returnData = new ReturnData();

            var isSuccess = _userService.LoginUser(userLogin);
            User user = null;
            UserInfo userinfo = null;
            if (isSuccess == STTCode.Success)
            {
                returnData.isSuccess = true;
                user = _userService.GetUserByEmail(userLogin.Email);
                userinfo = _userInfoService.GetUserInfoByID(user.ID);
                var token = _tokenService.CreateToken(user.ID, user.Email);

                returnData.Data = new List<object>() {
                    token,
                    userinfo,
                    _userService.GetAccountVerificationCode(user.ID),
                };
            }
            else
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var returnData = new ReturnData()
            {
                isSuccess = true,
                Data = new List<object>(_userService.GetUsers())
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpPut("ChangePassword")]
        [Authorize] // có token mới gọi được api này
        public IActionResult ChangePassword(UserChangePassword ucp)
        {
            string token = Request.Headers["Authorization"];
            // token nhận về có dạng "bearer " + token -> xoá 7 kí tự đầu
            token = token.Substring(7);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;

                var userid = tokenS.Claims.First(claim => claim.Type == "userid").Value;
                var email = tokenS.Claims.First(claim => claim.Type == "email").Value;

                STTCode code = _userService.ChangePassword(email, ucp.OldPassword, ucp.NewPassword);

                var returnData = new ReturnData();

                if (code == STTCode.Success)
                {
                    returnData.isSuccess = true;
                }
                else
                {
                    returnData.isSuccess = false;
                    returnData.errMessage = StatusCodeService.toString(code);
                }

                return Ok(JsonConvert.SerializeObject(returnData));
            }
            catch (Exception)
            {
                var returnData = new ReturnData()
                {
                    isSuccess = false
                };
                return Ok(JsonConvert.SerializeObject(returnData));
            }
        }
    }
}