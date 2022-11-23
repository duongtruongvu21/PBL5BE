using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services._UserInfo;

namespace PBL5BE.API.Services._UserInfo
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DataContext _context;
        public UserInfoService(DataContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHostEnvironment = webHost;
        }

        string GetAvatarPath()
        {
            return _webHostEnvironment.WebRootPath + "\\uploads\\avatars";
        }

        public void CreateUserInfo(User user, UserRegister userRegister = null)
        {
            var newUserInfo = new UserInfo()
            {
                UserID = user.ID,
                FirstName = userRegister == null ? "Unknow" : userRegister.FirstName,
                LastName = userRegister == null ? "Unknow" : userRegister.LastName,
                PhoneNumber = userRegister == null ? "Unknow" : userRegister.PhoneNumber,
                Sex = userRegister == null ? false : userRegister.Sex,
                Status = 0,
                Address = userRegister == null ? "Unknow" : userRegister.Address,
                CitizenID = "Unknow",
                Role = ALLCODE.Role_User.Key,
                CreateAt = DateTime.Now,
            };

            _context.UserInfos.Add(newUserInfo);
            _context.SaveChanges();
        }

        public Task<STTCode> EditUserInfo(UserInfoEditDTO uiEdit, UserInfo existUserInfo)
        {
            try
            {
                existUserInfo.UserID = uiEdit.UserID;
                existUserInfo.FirstName = uiEdit.FirstName;
                existUserInfo.LastName = uiEdit.LastName;
                Uploads.UpAvatar(uiEdit.Avatar, GetAvatarPath(), uiEdit.UserID);
                existUserInfo.Role = uiEdit.Role;
                existUserInfo.PhoneNumber = uiEdit.PhoneNumber;
                existUserInfo.Sex = uiEdit.Sex;
                existUserInfo.Status = 1;
                existUserInfo.Address = uiEdit.Address;
                existUserInfo.CitizenID = uiEdit.CitizenID;
                _context.Update(existUserInfo);
                _context.SaveChanges();

                return Task.FromResult(STTCode.Success);
            }
            catch (Exception)
            {
                return Task.FromResult(STTCode.ServerCodeException);
            }
        }

        public List<UserInfo> GetUserInfos()
        {
            return _context.UserInfos.ToList();
        }

        public UserInfo GetUserInfoByID(int id)
        {
            return _context.UserInfos.FirstOrDefault(p => p.UserID == id);
        }
    }
}