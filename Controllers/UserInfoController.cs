using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services;
using PBL5BE.API.Services._UserInfo;

namespace PBL5BE.API.Controllers
{
    public class UserInfoController : BaseController
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }


        [HttpPut("EditUserInfo")]
        public async Task<IActionResult> EditUserInfo([FromForm] UserInfoEditDTO userInfoDTO)
        {
            var existUI = _userInfoService.GetUserInfoByID(userInfoDTO.UserID);

            var isSuccess = await _userInfoService.EditUserInfo(userInfoDTO, existUI);


            var returnData = new ReturnData();
            returnData.Data = new List<object>() {
                    userInfoDTO.UserID,
                    userInfoDTO.CitizenID,
                };

            if (isSuccess == STTCode.Success)
            {
                returnData.isSuccess = true;
            }
            else
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpGet("GetUserInfos")]
        public IActionResult GetUserInfos()
        {
            var returnData = new ReturnData()
            {
                isSuccess = true,
                Data = new List<object>(_userInfoService.GetUserInfos())
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpGet("TestToken")] // lấy id và email từ token
        [Authorize] // có token mới gọi được api này
        public IActionResult TestToken()
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

                return Ok($"userID: {userid}, Email: {email}");
            }
            catch (Exception)
            {
                return Ok($"fails token: {token}");
            }
        }




        [HttpGet("GetUserInfoByID/{id}")]
        public IActionResult GetUserInfoByID(int id)
        {
            var returnData = new ReturnData()
            {
                isSuccess = true,
                Data = new List<object>()
            };

            returnData.Data = new List<object>() {
                _userInfoService.GetUserInfoByID(id)
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}