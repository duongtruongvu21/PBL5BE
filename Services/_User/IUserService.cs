using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._User
{
    public interface IUserService
    {
        STTCode CreateUser(UserLogin userLogin);
        List<User> GetUsers();
        STTCode LoginUser(UserLogin userLogin);
        User GetUserByEmail(String email);
    }
}