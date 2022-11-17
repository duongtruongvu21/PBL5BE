using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;
namespace PBL5BE.API.Services._Cart
{
    public interface ICartService
    {
        STTCode AddProductToCart(CartAddDTO c);
        STTCode EditCartItem(CartEditDTO c);
        STTCode DeleteCartItemByID(int id);
        List<Cart> GetCartItemsByUserID(int id);
        Cart GetCartItemByID(int id);
        STTCode OnPayment(int userID, List<int> cartItemsID, string address);
        List<Order> GetOrders(int status, int userID, int recordQuantity);
        List<OrderDetail> GetOrderDetailsByOrderID(int orderID);
    }
}