using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Services;

namespace PBL5BE.API.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        string GetOthersPath()
        {
            return _webHostEnvironment.WebRootPath + "\\uploads\\others\\";
        }

        // [HttpPost("UploadImage")]
        // public async Task<IActionResult> UploadImage([FromForm]ImageUpload imageUpload)
        // {
        //     var fileName = imageUpload.Image.FileName;
        //     var filePath = GetFilePath(fileName);

        //     if(System.IO.Directory.Exists(filePath))
        //     {
        //         System.IO.Directory.Delete(filePath);
        //     }

        //     using(FileStream stream = System.IO.File.Create(filePath))
        //     {
        //         await imageUpload.Image.CopyToAsync(stream);
        //     }

        //     return Ok($"{fileName}\n{filePath}");
        // }

        // [HttpGet("GetAvatar")]
        // public IActionResult GetAvatar()
        // {
        //     string path = ""; 

        //     try {
        //         string token = Request.Headers["Authorization"];
        //         token = token.Substring(7);
        //         var tokenHandler = new JwtSecurityTokenHandler();
        //         var jsonToken = tokenHandler.ReadToken(token);
        //         var tokenS = jsonToken as JwtSecurityToken;
        //         var userID = Int32.Parse(tokenS.Claims.First(claim => claim.Type == "userid").Value);
        //         path = $"\\uploads\\avatars\\user{userID}.png";
        //     } catch(Exception) {
        //         path = "\\uploads\\others\\tokenError.png";
        //     }


        //     string Path = _webHostEnvironment.WebRootPath + path;

        //     if(System.IO.File.Exists(Path)) 
        //     {
        //         byte[] b = System.IO.File.ReadAllBytes(Path);
        //         return File(b, "image/png");
        //     }
        //     return Ok("ERROR");
        // }


        [HttpGet("GetAvatar/{userID}")]
        public IActionResult GetAvatar(string userID)
        {
            string Path = _webHostEnvironment.WebRootPath +
                            $"\\uploads\\avatars\\user{userID}.png";

            if (!System.IO.File.Exists(Path))
            {
                Path = _webHostEnvironment.WebRootPath + "\\uploads\\others\\noAvatar.png";
            }

            if (System.IO.File.Exists(Path))
            {
                byte[] b = System.IO.File.ReadAllBytes(Path);
                return File(b, "image/png");
            }
            return Ok("ERROR");
        }

        // [HttpPut("TestUpImage")]
        // public async Task<IActionResult> TestUpImage([FromForm] ImageDTO img)
        // {
        //     var a = Uploads.TestUpImg(img.Avatar, GetOthersPath());

        //     return Ok(a);
        // }

        // [HttpPut("TestUpImageBlob")]
        // public IActionResult TestUpImageBlob([FromBody] string img)
        // {
        //     Regex regex = new Regex(@"^[\w/:.-]+;base64,");
        //     var base64File = regex.Replace(img, string.Empty);
        //     byte[] imageByte = Convert.FromBase64String(img);

        //     System.IO.File.WriteAllBytes(GetOthersPath() + "testtt.png", imageByte);
        //     return Ok("aa");
        // }
    }
}