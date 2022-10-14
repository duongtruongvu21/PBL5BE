using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
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
            var returnData = new ReturnData() {
                isSuccess = isSuccess,
                Data = new List<object>() {
                }
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpPost("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] Category newCategory)
        {
            var isSuccess = _categoryService.UpdateCategory(newCategory);
            var returnData = new ReturnData() {
                isSuccess = isSuccess,
                Data = new List<object>() {
                }
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory([FromBody] Category dCategory)
        {
            var isSuccess = _categoryService.DeleteCategory(dCategory);
            var returnData = new ReturnData() {
                isSuccess = isSuccess,
                Data = new List<object>() {
                }
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        
        [HttpGet("GetCategories")]
        public IActionResult GetCategories() 
        {
            var returnData = new ReturnData() {
                isSuccess = 1,
                Data = new List<object>(_categoryService.GetCategories())
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}