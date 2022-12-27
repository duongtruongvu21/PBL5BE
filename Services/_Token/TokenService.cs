using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PBL5BE.API.Data;

namespace PBL5BE.API.Services._Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public TokenService(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public string CreateToken(int userID, string email)
        {
            var claims = new List<Claim>()
            {
                // cái này tuỳ chọn, bỏ bao nhiêu thì bỏ
                new Claim("userid", "" + userID),
                new Claim("email", email),
            };

            var symmetricKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["TokenKey"])
            );

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);

            return tokenHandler.WriteToken(token);
        }

        public int getUserIDFromToken(string token)
        {
            token = token.Substring(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
            return(int.Parse(userId));
        }

        public bool isAdmin(int userID)
        {
            return _context.UserInfos.Any(u => u.UserID == userID && u.Role.Equals("R1"));
        }
    }
}