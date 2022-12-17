using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;
namespace PBL5BE.API.Services._OrderDetail
{
    public interface IOrderDetailService
    {
        int CreateOrderDetail(OrderDetailCreateDTO orderDetail);
        STTCode UpdateOrderDetail(OrderDetailUpdateDTO orderDetail);
        STTCode DeleteOrderDetail(int id);
        OrderDetail GetOrderDetailByID(int id);
        List<OrderDetail> GetOrderDetailsByOrderID(int orderID);
    }
}