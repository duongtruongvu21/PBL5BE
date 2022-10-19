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
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] Product newProduct)
        {
            var isSuccess = _productService.CreateProduct(newProduct);
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
        public IActionResult UpdateProduct([FromBody] Product newProduct)
        {
            var isSuccess = _productService.UpdateProduct(newProduct);
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
        
        [HttpGet("GetProducts")]
        public IActionResult GetProducts() 
        {
            var returnData = new ReturnData() {
                isSuccess = true,
                Data = new List<object>(_productService.GetProducts())
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
    }
}