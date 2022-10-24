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

        public void CreateUserInfo(User user)
        {
            var newUserInfo = new UserInfo() {
                UserID = user.ID,
                FirstName = "Unknow",
                LastName = "Unknow",
                // %5C == \
                PictureURL = "%5Cuploads%5Cothers%5CnoAvatar.png",
                PhoneNumber = "Unknow",
                Sex = false,
                Status = 0,
                Address = "Unknow",
                CitizenID = "Unknow",
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
                existUserInfo.PictureURL = 
                    Uploads.UpAvatar(uiEdit.Avatar, GetAvatarPath(), uiEdit.UserID);
                existUserInfo.PhoneNumber = uiEdit.PhoneNumber;
                existUserInfo.Sex = uiEdit.Sex;
                existUserInfo.Status = 1;
                existUserInfo.Address = uiEdit.Address;
                existUserInfo.CitizenID = uiEdit.CitizenID;
                _context.Update(existUserInfo);
                _context.SaveChanges();

                return Task.FromResult(STTCode.Success);
            } catch (Exception) 
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