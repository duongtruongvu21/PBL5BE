using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;
namespace PBL5BE.API.Services._Order
{
    public interface IOrderService
    {
        int CreateOrder(int userID); // khong dung nua
        STTCode UpdateOrderStatus(OrderStatusUpdateDTO order);
        STTCode OrderPaid(OrderPaidDTO order); // khong dung nua
        Order GetOrderByID(int id);
        List<Order> GetOrders(int status, int userID, int recordQuantity);
        List<OrderDetailGetDTO> GetOrderDetailsByOrderID(int orderID);
    }
}