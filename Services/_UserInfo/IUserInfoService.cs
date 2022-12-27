using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._UserInfo
{
    public interface IUserInfoService
    {
        void CreateUserInfo(User user, UserRegister userRegister = null);
        Task<STTCode> EditUserInfo(UserInfoEditDTO uiEdit);
        UserInfoDTO GetUserInfoByID(int id);
        List<UserInfoDTO> GetUserInfos();
        STTCode ChangeRole(int UserID, string Role);
        STTCode ChangeStatus(int UserID, byte Status);
    }
}