using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Services._UserInfo;

namespace PBL5BE.API.Services._UserInfo 
{
    public class UserInfoService : IUserInfoService
    {
        private readonly DataContext _context;
        public UserInfoService(DataContext context)
        {
            _context = context;
        }
        
        public void CreateUserInfo(User user)
        {
            var newUserInfo = new UserInfo() {
                UserID = user.ID,
                FirstName = "Unknow",
                LastName = "Unknow",
                PictureURL = "https://www.seekpng.com/png/detail/413-4139803_unknown-profile-profile-picture-unknown.png",
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

        public bool EditUserInfo(UserInfoDTO userInfoDTO, UserInfo existUserInfo)
        {
            try 
            {
                existUserInfo.UserID = userInfoDTO.UserID;
                existUserInfo.FirstName = userInfoDTO.FirstName;
                existUserInfo.LastName = userInfoDTO.LastName;
                existUserInfo.PictureURL = "https://www.seekpng.com/png/detail/413-4139803_unknown-profile-profile-picture-unknown.png";
                existUserInfo.PhoneNumber = userInfoDTO.PhoneNumber;
                existUserInfo.Sex = userInfoDTO.Sex;
                existUserInfo.Status = 1;
                existUserInfo.Address = userInfoDTO.Address;
                existUserInfo.CitizenID = userInfoDTO.CitizenID;
                _context.Update(existUserInfo);
                _context.SaveChanges();

                return true;
            } catch (Exception) 
            {
                return false;
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