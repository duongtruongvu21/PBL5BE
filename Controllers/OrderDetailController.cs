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
// using PBL5BE.API.Services._OrderDetail;

// namespace PBL5BE.API.Controllers
// {
//     public class OrderDetailController : BaseController
//     {
//         private readonly IOrderDetailService _orderDetailService;

//         public OrderDetailController(IOrderDetailService orderDetailService)
//         {
//             _orderDetailService = orderDetailService;
//         }
//         [HttpPost("CreateOrderDetail")]
//         //[Authorize]
//         public IActionResult CreateOrderDetail([FromBody] OrderDetailCreateDTO orderDetail)
//         {
//             var returnData = new ReturnData();
//             try{
//                 int orderDetailID = _orderDetailService.CreateOrderDetail(orderDetail);
//                 returnData.isSuccess = true;
//                 returnData.Data = new List<object>(){
//                     orderDetailID,
//                 };
//             }
//             catch(Exception){
//                 returnData.isSuccess = false;
//                 returnData.errMessage = StatusCodeService.toString(STTCode.ForeignKeyIDNotFound);
//             }
//             return Ok(JsonConvert.SerializeObject(returnData));
//         }
//         [HttpPut("UpdateOrderDetail")]
//         //[Authorize]
//         public IActionResult UpdateOrderDetail([FromBody] OrderDetailUpdateDTO orderDetail)
//         {
//             var isSuccess = _orderDetailService.UpdateOrderDetail(orderDetail);
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
//         [HttpDelete("DeleteOrderDetail")]
//         //[Authorize]
//         public IActionResult DeleteOrderDetail(int id)
//         {
//             var isSuccess = _orderDetailService.DeleteOrderDetail(id);
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
//         [HttpGet("GetOrderDetailByID")]
//         public IActionResult GetOrderDetailByID(int id)
//         {
//             try {
//                 OrderDetail od = _orderDetailService.GetOrderDetailByID(id);
//                 var returnData = new ReturnData() {
//                     isSuccess = true,
//                     Data = new List<object>() {
//                         od,
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
//         [HttpGet("GetOrderDetailsByOrderID")]
//         public IActionResult GetOrderDetailsByOrderID(int id)
//         {
//             try {
//                 var returnData = new ReturnData() {
//                     isSuccess = true,
//                     Data = new List<object>(_orderDetailService.GetOrderDetailsByOrderID(id))
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
//     }
// }