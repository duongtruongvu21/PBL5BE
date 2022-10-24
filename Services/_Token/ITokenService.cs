namespace PBL5BE.API.Services._Token
{
    public interface ITokenService
    {
        string CreateToken(int userID, string email); 
    }
}