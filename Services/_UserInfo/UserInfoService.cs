using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

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
                FirstName = userRegister == null ? "Unknow" : userRegister.FirstName == null ? "Unknow" : userRegister.FirstName,
                LastName = userRegister == null ? "Unknow" : userRegister.LastName == null ? "Unknow" : userRegister.LastName,
                PhoneNumber = userRegister == null ? "Unknow" : userRegister.PhoneNumber == null ? "Unknow" : userRegister.PhoneNumber,
                Sex = userRegister == null ? false : userRegister.Sex,
                Status = 1,
                Role = userRegister == null ? ALLCODE.Role_User.Key : userRegister.Role,
                Address = userRegister == null ? "Unknow" : userRegister.Address == null ? "Unknow" : userRegister.Address,
                CitizenID = userRegister == null ? "Unknow" : userRegister.CitizenID,
                CreateAt = DateTime.Now,
            };

            if (userRegister != null)
                Uploads.UpAvatar(userRegister.Avatar, GetAvatarPath(), newUserInfo.UserID);

            _context.UserInfos.Add(newUserInfo);
            _context.SaveChanges();
        }

        public Task<STTCode> EditUserInfo(UserInfoEditDTO uiEdit)
        {
            UserInfo existUser = _context.UserInfos.FirstOrDefault(
                x => x.UserID == uiEdit.UserID);
            try
            {
                existUser.FirstName = uiEdit.FirstName;
                existUser.LastName = uiEdit.LastName;
                Uploads.UpAvatar(uiEdit.Avatar, GetAvatarPath(), uiEdit.UserID);
                existUser.PhoneNumber = uiEdit.PhoneNumber;
                existUser.Sex = uiEdit.Sex;
                existUser.Status = 1;
                existUser.Address = uiEdit.Address;
                existUser.CitizenID = uiEdit.CitizenID;
                _context.Update(existUser);
                _context.SaveChanges();

                return Task.FromResult(STTCode.Success);
            }
            catch (Exception)
            {
                return Task.FromResult(STTCode.ServerCodeException);
            }
        }

        public List<UserInfoDTO> GetUserInfos()
        {
            List<UserInfoDTO> userInfos = new List<UserInfoDTO>();
            foreach (var i in _context.UserInfos.ToList())
            {
                userInfos.Add(new UserInfoDTO()
                {
                    UserID = i.UserID,
                    Email = _context.Users.ToList().FirstOrDefault(x => x.ID == i.UserID).Email,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    PhoneNumber = i.PhoneNumber,
                    Address = i.Address,
                    Role = i.Role,
                    Status = i.Status,
                    Sex = i.Sex,
                    CitizenID = i.CitizenID,
                });
            }
            return userInfos;
        }

        public UserInfoDTO GetUserInfoByID(int id)
        {
            return GetUserInfos().FirstOrDefault(p => p.UserID == id);
        }

        public STTCode ChangeRole(int UserID, string Role)
        {
            UserInfo existUser = _context.UserInfos.FirstOrDefault(x => x.UserID == UserID);
            try
            {
                existUser.Role = Role;
                _context.Update(existUser);
                _context.SaveChanges();

                return STTCode.Success;
            }
            catch (Exception)
            {
                return STTCode.ServerCodeException;
            }
        }

        public STTCode ChangeStatus(int UserID, byte Status)
        {
            UserInfo existUser = _context.UserInfos.FirstOrDefault(x => x.UserID == UserID);
            try
            {
                existUser.Status = Status;
                _context.Update(existUser);
                _context.SaveChanges();

                return STTCode.Success;
            }
            catch (Exception)
            {
                return STTCode.ServerCodeException;
            }
        }
    }
}