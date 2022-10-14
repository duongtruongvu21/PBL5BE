using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Services;
using PBL5BE.API.Services._User;
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
        public IActionResult EditUserInfo([FromBody] UserInfoDTO userInfoDTO)
        {
            var existUI = _userInfoService.GetUserInfoByID(userInfoDTO.UserID);
                
            bool isSuccess = _userInfoService.EditUserInfo(userInfoDTO, existUI);

            return Ok(isSuccess);
        }

        [HttpGet("GetUserInfos")]
        public IActionResult GetUserInfos() 
        {
            var returnData = new ReturnData() {
                isSuccess = 1,
                Data = new List<object>(_userInfoService.GetUserInfos())
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}