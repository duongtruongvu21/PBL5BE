using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._UserInfo
{
    public interface IUserInfoService
    {
        void CreateUserInfo(User user);
        bool EditUserInfo(UserInfoDTO userInfoDTO, UserInfo existUserInfo);
        UserInfo GetUserInfoByID(int id);
        List<UserInfo> GetAllUserInfo();
    }
}