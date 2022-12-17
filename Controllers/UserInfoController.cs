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
            var isSuccess = await _userInfoService.EditUserInfo(userInfoDTO);


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

        [HttpPut("ChangeRole")]
        [Authorize] // có token mới gọi được api này
        public IActionResult ChangeRole([FromBody] ChangeRoleDTO change)
        {
            string token = Request.Headers["Authorization"];
            token = token.Substring(7);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;

                var userid = tokenS.Claims.First(claim => claim.Type == "userid").Value;

                bool isAdmin = _userInfoService.GetUserInfoByID(Int32.Parse(userid)).Role == ALLCODE.Role_Admin.Key;

                if (isAdmin)
                {
                    STTCode code = _userInfoService.ChangeRole(change.UserID, change.Role);

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
                else
                {
                    var returnData = new ReturnData();
                    returnData.isSuccess = false;
                    returnData.errMessage = StatusCodeService.toString(STTCode.NotAdmin);
                    return Ok(JsonConvert.SerializeObject(returnData));
                }
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


        [HttpPut("ChangeStatus")]
        [Authorize] // có token mới gọi được api này
        public IActionResult ChangeStatus([FromBody] ChangeSTTDTO change)
        {
            string token = Request.Headers["Authorization"];
            token = token.Substring(7);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;

                var userid = tokenS.Claims.First(claim => claim.Type == "userid").Value;

                bool isAdmin = _userInfoService.GetUserInfoByID(Int32.Parse(userid)).Role == ALLCODE.Role_Admin.Key;

                if (isAdmin)
                {
                    STTCode code = _userInfoService.ChangeStatus(change.UserID, change.Status);

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
                else
                {
                    var returnData = new ReturnData();
                    returnData.isSuccess = false;
                    returnData.errMessage = StatusCodeService.toString(STTCode.NotAdmin);
                    return Ok(JsonConvert.SerializeObject(returnData));
                }
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