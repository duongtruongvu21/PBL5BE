using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Enums;
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
        public IActionResult EditUserInfo([FromBody] UserInfoEditDTO userInfoDTO)
        {
            var existUI = _userInfoService.GetUserInfoByID(userInfoDTO.UserID);
                
            var isSuccess = _userInfoService.EditUserInfo(userInfoDTO, existUI);

            var returnData = new ReturnData();
            if(isSuccess == STTCode.Success) 
            {
                returnData.isSuccess = true;
            } else 
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }

        [HttpGet("GetUserInfos")]
        public IActionResult GetUserInfos() 
        {
            var returnData = new ReturnData() {
                isSuccess = true,
                Data = new List<object>(_userInfoService.GetUserInfos())
            };

            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}