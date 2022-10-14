using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._User
{
    public interface IUserService
    {
        int CreateUser(UserLogin userLogin);
        List<User> GetUsers();
        int LoginUser(UserLogin userLogin);
        User GetUserByEmail(String email);
    }
}