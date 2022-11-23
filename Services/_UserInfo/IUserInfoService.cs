using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._UserInfo
{
    public interface IUserInfoService
    {
        void CreateUserInfo(User user, UserRegister userRegister = null);
        Task<STTCode> EditUserInfo(UserInfoEditDTO uiEdit, UserInfo existUserInfo);
        UserInfo GetUserInfoByID(int id);
        List<UserInfo> GetUserInfos();
    }
}