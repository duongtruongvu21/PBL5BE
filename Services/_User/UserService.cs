using System.Security.Cryptography;
using System.Text;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._User
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public STTCode CreateUser(UserLogin userLogin)
        {
            try 
            {
                userLogin.Email = userLogin.Email.ToLower();
                if(_context.Users.Any(u => u.Email == userLogin.Email))
                {
                    return STTCode.UserExisted;
                }

                using var hmac = new HMACSHA512();
                var passwordByte = Encoding.UTF8.GetBytes(userLogin.Password);
                var newUser = new User() {
                    Email = userLogin.Email,
                    PasswordSalt = hmac.Key,
                    PasswordHashed = hmac.ComputeHash(passwordByte)
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
            } catch(Exception) 
            {
                return STTCode.ServerCodeException;
            }

            return STTCode.Success;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public STTCode LoginUser(UserLogin userLogin)
        {
            userLogin.Email = userLogin.Email.ToLower();
            var currentUser = _context.Users.
                FirstOrDefault(u => u.Email == userLogin.Email);

            if(currentUser == null) {
                return STTCode.UserNotExist;
            }

            using(var hmac = new HMACSHA512(currentUser.PasswordSalt)) {
                var passwordBytes = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(userLogin.Password));

                for(int i = 0; i < currentUser.PasswordHashed.Length; i++) {
                    if (currentUser.PasswordHashed[i] != passwordBytes[i]) {
                        return STTCode.IncorrectPassword;
                    }
                }

                return STTCode.Success;
            }
        }
    }
}
