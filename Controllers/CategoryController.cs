using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services;
using PBL5BE.API.Services._Category;

namespace PBL5BE.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory([FromBody] Category newCategory)
        {
            var isSuccess = _categoryService.CreateCategory(newCategory);

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
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] Category newCategory)
        {
            var isSuccess = _categoryService.UpdateCategory(newCategory);

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
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            var isSuccess = _categoryService.DeleteCategory(id);
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
        
        [HttpGet("GetCategories")]
        public IActionResult GetCategories() 
        {
            var data = new List<object>(_categoryService.GetCategories());
            var returnData = new ReturnData();
            returnData.isSuccess = true;
            returnData.Data = data;
            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}