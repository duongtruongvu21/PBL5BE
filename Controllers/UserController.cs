using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.OtherModulesce;

namespace PBL5BE.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        
        [HttpPost("UserRegister")]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            userRegister.Username = userRegister.Username.ToLower();
            if(_context.Users.Any(u => u.Username == userRegister.Username))
            {
                return BadRequest(false);
            }

            using var hmac = new HMACSHA512();
            var passwordByte = Encoding.UTF8.GetBytes(userRegister.Password);
            var newUser = new User() {
                Username = userRegister.Username,
                Email = userRegister.Email,
                PasswordSalt = hmac.Key,
                PasswordHashed = hmac.ComputeHash(passwordByte)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var _code = "TestCODE31";
            _ = SendMail.SendVerificationMail(userRegister.Email, _code);

            return Ok(true);
        }

        
        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] UserLogin userLogin)
        {
            userLogin.Username = userLogin.Username.ToLower();
            var currentUser = _context.Users.
                FirstOrDefault(u => u.Username == userLogin.Username);

            if(currentUser == null) {
                return Unauthorized(-1);
            }

            using(var hmac = new HMACSHA512(currentUser.PasswordSalt)) {
                var passwordBytes = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(userLogin.Password));

                for(int i = 0; i < currentUser.PasswordHashed.Length; i++) {
                    if (currentUser.PasswordHashed[i] != passwordBytes[i]) {
                        return Unauthorized(0);
                    }
                }

                return Ok(1);
            }
        }

        [HttpGet("getAllUser")]
        public IActionResult GetAllUser() 
        {
            return Ok(_context.Users.ToList());
        }
    }
}