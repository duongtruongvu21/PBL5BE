using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Services;
using PBL5BE.API.Services._Cart;
using PBL5BE.API.Services._Token;

namespace PBL5BE.API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly ITokenService _tokenService;

        public CartController(ICartService cartService, ITokenService tokenService)
        {
            _cartService = cartService;
            _tokenService = tokenService;
        }
        [HttpPost("AddProductToCart")]
        [Authorize]
        public IActionResult AddProductToCart([FromBody] CartAddDTO c)
        {
            string token = Request.Headers["Authorization"];
            c.UserID = _tokenService.getUserIDFromToken(token);
            var isSuccess = _cartService.AddProductToCart(c);
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
        [HttpPut("EditCartItem")]
        [Authorize]
        public IActionResult EditCartItem([FromBody] CartEditDTO c)
        {
            var isSuccess = _cartService.EditCartItem(c);
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
        [HttpDelete("DeleteCartItemByID")]
        [Authorize]
        public IActionResult DeleteCartItemByID(int id)
        {
            var isSuccess = _cartService.DeleteCartItemByID(id);
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
        // [HttpGet("GetCartItemByID")]
        // public IActionResult GetCartItemByID(int id)
        // {
        //     try {
        //         Cart c = _cartService.GetCartItemByID(id);
        //         var returnData = new ReturnData() {
        //             isSuccess = true,
        //             Data = new List<object>() {
        //                 c,
        //             }
        //         };
        //         return Ok(JsonConvert.SerializeObject(returnData));
        //     }
        //     catch(Exception){
        //         var returnData = new ReturnData() {
        //             isSuccess = false,
        //             errMessage = StatusCodeService.toString(STTCode.IDNotFound)
        //         };
        //         return Ok(JsonConvert.SerializeObject(returnData));
        //     }
        // }
        [HttpGet("GetCartItemsByUserID")]
        public IActionResult GetCartItemsByUserID(int id) 
        {
            var returnData = new ReturnData() {
                isSuccess = true,
                Data = new List<object>(_cartService.GetCartItemsByUserID(id))
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpPost("OnPayment")]
        [Authorize]
        public IActionResult OnPayment([FromForm] CartOnPayment c)
        {
            string token = Request.Headers["Authorization"];
            c.userID = _tokenService.getUserIDFromToken(token);
            var isSuccess = _cartService.OnPayment(c.userID, c.cartItemsID, c.Address, c.ShippingFee);
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