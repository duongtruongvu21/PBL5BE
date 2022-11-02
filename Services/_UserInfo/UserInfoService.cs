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
                // %5C == \
                PictureURL = "%5Cuploads%5Cothers%5CnoAvatar.png",
                PhoneNumber = userRegister == null ? "Unknow" : userRegister.PhoneNumber,
                Sex = userRegister == null ? false : userRegister.Sex,
                Status = 0,
                Address = userRegister == null ? "Unknow" : userRegister.Address,
                CitizenID = "Unknow",
                CreateAt = DateTime.Now,
            };

            _context.UserInfos.Add(newUserInfo);
            _context.SaveChanges();
        }

        public Task<STTCode> EditUserInfo(UserInfoEditDTO uiEdit, UserInfo existUserInfo)
        {
            STTCode e = STTCode.Existed;
            try
            {
                e = STTCode.E5;
                existUserInfo.UserID = uiEdit.UserID; e = STTCode.E1;
                existUserInfo.FirstName = uiEdit.FirstName; e = STTCode.E6;
                existUserInfo.LastName = uiEdit.LastName; e = STTCode.E2;
                existUserInfo.PictureURL =
                    Uploads.UpAvatar(uiEdit.Avatar, GetAvatarPath(), uiEdit.UserID);
                existUserInfo.PhoneNumber = uiEdit.PhoneNumber; e = STTCode.E3;
                existUserInfo.Sex = uiEdit.Sex; e = STTCode.E4;
                existUserInfo.Status = 1;
                existUserInfo.Address = uiEdit.Address;
                existUserInfo.CitizenID = uiEdit.CitizenID;
                _context.Update(existUserInfo);
                _context.SaveChanges();

                return Task.FromResult(STTCode.Success);
            }
            catch (Exception)
            {
                return Task.FromResult(e);
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