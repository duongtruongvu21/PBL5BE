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
        public static string UpProductImgs(List<IFormFile> imgs, string path, int productId) 
        {
            try
            {
                string returnPath = $"%5Cuploads%5Cproducts%5Cproduct{productId}";
                // xoá thư mục cũ
                var directoryPath = $"{path}\\product{productId}";
                if(System.IO.Directory.Exists(directoryPath)){
                    System.IO.DirectoryInfo dir = new DirectoryInfo(directoryPath);
                    if (dir != null) dir.Delete(true);
                }
                // không có ảnh thì return
                if (imgs == null) return returnPath;
                // duyệt hết ảnh mới
                int i = 1;
                foreach (IFormFile img in imgs){
                    System.IO.Directory.CreateDirectory(directoryPath);
                    var imgPath = $"{directoryPath}\\image{i}.png";
                    i += 1;
                    using(FileStream stream = System.IO.File.Create(imgPath)){
                        img.CopyTo(stream);
                    }
                }
                // return path productId
                return returnPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public static string TestUpImg(IFormFile img, string path) 
        {
            try
            {
                var filePath = $"{path}\\test.png";
                if(System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.Delete(filePath);
                }

                using(FileStream stream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(stream);
                }
                return $"test.png";
            }
            catch (Exception)
            {
                return "failed";
            }
        }
    }
}