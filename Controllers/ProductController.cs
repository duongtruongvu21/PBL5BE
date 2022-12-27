using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services;
using PBL5BE.API.Services._Category;
using PBL5BE.API.Services._Product;
using PBL5BE.API.Services._Token;

namespace PBL5BE.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITokenService _tokenService;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment, ITokenService tokenService)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _tokenService = tokenService;
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDTO newProduct)
        {
            var isSuccess = STTCode.ServerCodeException;
            try{
                string token = Request.Headers["Authorization"];
                int userId = _tokenService.getUserIDFromToken(token);
                if (_tokenService.isAdmin(_tokenService.getUserIDFromToken(token)))
                    isSuccess = await _productService.CreateProduct(newProduct, userId);
                else isSuccess = STTCode.NotAdmin;
            }
            catch(Exception){}
            var returnData = new ReturnData();
            if(isSuccess == STTCode.Success) 
            {
                returnData.isSuccess = true;
            } else 
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpPut("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateDTO newProduct)
        {
            var isSuccess = STTCode.ServerCodeException;
            try{
                string token = Request.Headers["Authorization"];
                int userId = _tokenService.getUserIDFromToken(token);
                if (_tokenService.isAdmin(_tokenService.getUserIDFromToken(token)))
                    isSuccess = await _productService.UpdateProduct(newProduct);
                else isSuccess = STTCode.NotAdmin;
            }
            catch(Exception){}
            var returnData = new ReturnData();
            if(isSuccess == STTCode.Success) 
            {
                returnData.isSuccess = true;
            } else 
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }

            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpDelete("DeleteProduct")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            var isSuccess = STTCode.ServerCodeException;
            try{
                string token = Request.Headers["Authorization"];
                int userId = _tokenService.getUserIDFromToken(token);
                if (_tokenService.isAdmin(_tokenService.getUserIDFromToken(token)))
                    isSuccess = isSuccess = _productService.DeleteProduct(id);
                else isSuccess = STTCode.NotAdmin;
            }
            catch(Exception){}
            var returnData = new ReturnData();
            if(isSuccess == STTCode.Success) 
            {
                returnData.isSuccess = true;
            } else 
            {
                returnData.isSuccess = false;
                returnData.errMessage = StatusCodeService.toString(isSuccess);
            }
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        
        [HttpGet("GetProducts")]
        public IActionResult GetProducts(int categoryId = 0, string productName = "", byte status = 1, int recordQuantity = 999) 
        {
            var returnData = new ReturnData() {
                isSuccess = true,
                Data = new List<object>(_productService.GetProducts(categoryId, productName, status, recordQuantity))
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpGet("GetProductByID")]
        public IActionResult GetProductByID(int id)
        {
            try {
                Product p = _productService.GetProductByID(id);
                var returnData = new ReturnData() {
                    isSuccess = true,
                    Data = new List<object>() {
                        p,
                    }
                };
                return Ok(JsonConvert.SerializeObject(returnData));
            }
            catch(Exception){
                var returnData = new ReturnData() {
                    isSuccess = false,
                    errMessage = StatusCodeService.toString(STTCode.IDNotFound)
                };
                return Ok(JsonConvert.SerializeObject(returnData));
            }
        }
        [HttpGet("GetProductImage")]
        public IActionResult GetProductImage(int productId, int imgNumber = 1)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\products";
            var imgPath = $"{path}\\product{productId}\\image{imgNumber}.png";
            Console.WriteLine(imgPath);
            if(!System.IO.File.Exists(imgPath)) 
            {
                imgPath = _webHostEnvironment.WebRootPath + "\\uploads\\others\\noProductImg.png";
                byte[] b = System.IO.File.ReadAllBytes(imgPath);
                return File(b, "image/png");
            }
            if(System.IO.File.Exists(imgPath)) 
            {
                byte[] b = System.IO.File.ReadAllBytes(imgPath);
                return File(b, "image/png");
            }
            return Ok("ERROR");
        }
    }
}