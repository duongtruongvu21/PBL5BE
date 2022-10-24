using System.Net;
using System.Net.Mail;

namespace PBL5BE.API.Services
{
    public class Uploads
    {
        public static string UpAvatar(IFormFile img, string path, int userID) 
        {
            try
            {
                var filePath = $"{path}\\user{userID}.png";
                if(System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.Delete(filePath);
                }

                using(FileStream stream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(stream);
                }
                return $"%5Cuploads%5Cavatars%5Cuser{userID}.png";
            }
            catch (Exception)
            {
                return "%5Cuploads%5Cavatars%5Cerror.png";
            }
        }
    }
}