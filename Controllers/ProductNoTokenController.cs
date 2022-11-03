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

namespace PBL5BE.API.Controllers
{
    public class ProductNoTokenController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DataContext _context;

        public ProductNoTokenController(IProductService productService, IWebHostEnvironment webHostEnvironment, DataContext context)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDTO newProduct)
        {
            var isSuccess = STTCode.ServerCodeException;
            try{
                int userId = _context.Users.FirstOrDefault().ID;
                isSuccess = await _productService.CreateProduct(newProduct, userId);
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
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateDTO newProduct)
        {
            var isSuccess = await _productService.UpdateProduct(newProduct);
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
        public IActionResult DeleteProduct(int id)
        {
            var isSuccess = _productService.DeleteProduct(id);
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
    }
}