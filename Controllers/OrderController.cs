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
using PBL5BE.API.Services._Order;
using PBL5BE.API.Services._Token;
namespace PBL5BE.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ITokenService _tokenService;

        public OrderController(IOrderService orderService, ITokenService tokenService)
        {
            _orderService = orderService;
            _tokenService = tokenService;
        }
        [HttpPut("UpdateOrderStatus")]
        [Authorize]
        public IActionResult UpdateOrderStatus([FromBody] OrderStatusUpdateDTO order)
        {
            string token = Request.Headers["Authorization"];
            var isSuccess = STTCode.Success;
            if (_tokenService.isAdmin(_tokenService.getUserIDFromToken(token)))
                isSuccess = _orderService.UpdateOrderStatus(order);
            else isSuccess = STTCode.NotAdmin;
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
        // [HttpGet("GetOrderByID")]
        // public IActionResult GetOrderByID(int id)
        // {
        //     try {
        //         Order o = _orderService.GetOrderByID(id);
        //         var returnData = new ReturnData() {
        //             isSuccess = true,
        //             Data = new List<object>() {
        //                 o,
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
        [HttpGet("GetOrders")]
        public IActionResult GetOrders(int status = -1, int userID = 0, int recordQuantity = 999) 
        {
            var returnData = new ReturnData() {
                isSuccess = true,
                Data = new List<object>(_orderService.GetOrders(status, userID, recordQuantity))
            };
            return Ok(JsonConvert.SerializeObject(returnData));
        }
        [HttpGet("GetOrderDetailsByOrderID")]
        public IActionResult GetOrderDetailsByOrderID(int id)
        {
            try {
                var returnData = new ReturnData() {
                    isSuccess = true,
                    Data = new List<object>(_orderService.GetOrderDetailsByOrderID(id))
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