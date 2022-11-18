// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
// using PBL5BE.API.Data;
// using PBL5BE.API.Data.DTO;
// using PBL5BE.API.Data.Entities;
// using PBL5BE.API.Data.Enums;
// using PBL5BE.API.Services;
// using PBL5BE.API.Services._Category;
// using PBL5BE.API.Services._Order;

// namespace PBL5BE.API.Controllers
// {
//     public class OrderController : BaseController
//     {
//         private readonly IOrderService _orderService;

//         public OrderController(IOrderService orderService)
//         {
//             _orderService = orderService;
//         }
//         [HttpPost("CreateOrder")]
//         //[Authorize]
//         public IActionResult CreateOrder()
//         {
//             // string token = Request.Headers["Authorization"];
//             // token = token.Substring(7);
//             // var tokenHandler = new JwtSecurityTokenHandler();
//             // var jsonToken = tokenHandler.ReadToken(token);
//             // var tokenS = jsonToken as JwtSecurityToken;
//             // var userId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
//             var returnData = new ReturnData();
//             try{
//                 int orderID = _orderService.CreateOrder(1);
//                 returnData.isSuccess = true;
//                 returnData.Data = new List<object>(){
//                     orderID,
//                 };
//             }
//             catch(Exception){
//                 returnData.isSuccess = false;
//                 returnData.errMessage = StatusCodeService.toString(STTCode.ServerCodeException);
//             }
//             return Ok(JsonConvert.SerializeObject(returnData));
//         }
//         [HttpPut("UpdateOrderStatus")]
//         //[Authorize]
//         public IActionResult UpdateOrderStatus([FromBody] OrderStatusUpdateDTO order)
//         {
//             var isSuccess = _orderService.UpdateOrderStatus(order);
//             var returnData = new ReturnData();
//             if(isSuccess == STTCode.Success) 
//             {
//                 returnData.isSuccess = true;
//             } else 
//             {
//                 returnData.isSuccess = false;
//                 returnData.errMessage = StatusCodeService.toString(isSuccess);
//             }

//             return Ok(JsonConvert.SerializeObject(returnData));
//         }
//         [HttpPut("OrderPaid")]
//         //[Authorize]
//         public IActionResult OrderPaid([FromBody] OrderPaidDTO order)
//         {
//             var isSuccess = _orderService.OrderPaid(order);
//             var returnData = new ReturnData();
//             if(isSuccess == STTCode.Success) 
//             {
//                 returnData.isSuccess = true;
//             } else 
//             {
//                 returnData.isSuccess = false;
//                 returnData.errMessage = StatusCodeService.toString(isSuccess);
//             }

//             return Ok(JsonConvert.SerializeObject(returnData));
//         }
//         [HttpGet("GetOrderByID")]
//         public IActionResult GetOrderByID(int id)
//         {
//             try {
//                 Order o = _orderService.GetOrderByID(id);
//                 var returnData = new ReturnData() {
//                     isSuccess = true,
//                     Data = new List<object>() {
//                         o,
//                     }
//                 };
//                 return Ok(JsonConvert.SerializeObject(returnData));
//             }
//             catch(Exception){
//                 var returnData = new ReturnData() {
//                     isSuccess = false,
//                     errMessage = StatusCodeService.toString(STTCode.IDNotFound)
//                 };
//                 return Ok(JsonConvert.SerializeObject(returnData));
//             }
//         }
//         [HttpGet("GetOrders")]
//         public IActionResult GetOrders(int status = -1, int userID = 0, int recordQuantity = 999) 
//         {
//             var returnData = new ReturnData() {
//                 isSuccess = true,
//                 Data = new List<object>(_orderService.GetOrders(status, userID, recordQuantity))
//             };
//             return Ok(JsonConvert.SerializeObject(returnData));
//         }
//     }
// }