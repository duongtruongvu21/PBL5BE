using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Cart
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        public CartService(DataContext context)
        {
            _context = context;
        }

        public STTCode AddProductToCart(CartAddDTO c)
        {
            try{
                if (!_context.Users.Any(u => u.ID == c.UserID)) return STTCode.ForeignKeyIDNotFound;
                var p = _context.Products.FirstOrDefault(p => p.ID == c.ProductID);
                if (p == null) return STTCode.ForeignKeyIDNotFound;
                var newC = new Cart{
                    UserID = c.UserID,
                    Description = c.Description,
                    PricePerOne = p.PricePerOne,
                    ProductCount = c.ProductCount,
                    ProductID = c.ProductID
                };
                _context.CartItems.Add(newC);
                _context.SaveChanges();
                return STTCode.Success;            }
            catch (Exception){
                return STTCode.ServerCodeException;
            }
        }

        public STTCode DeleteCartItemByID(int id)
        {
            try{
                var c = _context.CartItems.FirstOrDefault(c => c.ID == id);
                if (c == null) return STTCode.IDNotFound;
                _context.Remove(c);
                _context.SaveChanges();
                return STTCode.Success;            }
            catch (Exception){
                return STTCode.ServerCodeException;
            }
        }

        public STTCode EditCartItem(CartEditDTO c)
        {
            try{
                var currentC = _context.CartItems.FirstOrDefault(cc => cc.ID == c.ID);
                if (currentC == null) return STTCode.IDNotFound;
                currentC.Description = c.Description;
                currentC.ProductCount = c.ProductCount;
                _context.SaveChanges();
                return STTCode.Success;            
            }
            catch (Exception){
                return STTCode.ServerCodeException;
            }
        }

        public List<CartGetDTO> GetCartItemsByUserID(int id)
        {
            List<CartGetDTO> data = new List<CartGetDTO>();
            List<Cart> cs = _context.CartItems.Where(c => c.UserID == id).OrderByDescending(c => c.ID).ToList();
            foreach (Cart c in cs){
                Product p = _context.Products.FirstOrDefault(p => p.ID == c.ProductID);
                CartGetDTO cgd = new CartGetDTO{
                    Description = c.Description,
                    ID = c.ID,
                    PricePerOne = c.PricePerOne,
                    ProductCount = c.ProductCount,
                    ProductID = c.ProductID,
                    UserID = c.UserID,
                    ProductName = p.ProductName,
                    ProductQuantityLeft = p.Count
                };
                data.Add(cgd);
            }
            return data;
        }

        public List<OrderDetailGetDTO> GetOrderDetailsByOrderID(int orderID)
        {
            if (!_context.Orders.Any(o => o.ID == orderID)) throw new Exception("not found");
            List<OrderDetail> ods =  _context.OrderDetails.Where(od => od.OrderID == orderID).OrderByDescending(od => od.ID).ToList();
            List<OrderDetailGetDTO> data = new List<OrderDetailGetDTO>();
            foreach(OrderDetail od in ods){
                Product p = _context.Products.FirstOrDefault(p => p.ID == od.ProductID);
                OrderDetailGetDTO o = new OrderDetailGetDTO{
                    ID = od.ID,
                    Description = od.Description,
                    OrderID = od.OrderID,
                    PricePerOne = od.PricePerOne,
                    ProductCount = od.ProductCount,
                    ProductID = od.ProductID,
                    ProductName = p.ProductName
                };
                data.Add(o);
            }
            return data;
        }

        public List<Order> GetOrders(int status, int userID, int recordQuantity)
        {
            List<Order> orders = new List<Order>();
            if (status < 0){
                orders = _context.Orders.Where(o => o.Status != 0).OrderByDescending(o => o.ID).Take(recordQuantity).ToList();
            }
            else {
                orders = _context.Orders.Where(o => o.Status == status).OrderByDescending(o => o.ID).Take(recordQuantity).ToList();
            }
            if (userID > 0){
                orders = orders.FindAll(o => o.CreateBy == userID);
            }
            return orders;
        }
        public Cart GetCartItemByID(int id){
            return _context.CartItems.FirstOrDefault(c => c.ID == id);
        }

        public STTCode OnPayment(int userID, List<int> cartItemsID, string address, float ShippingFee)
        {
            try{
                var o = new Order{
                    CreateAt = DateTime.Now,
                    CreateBy = userID,
                    Status = 0,
                    Address = address,
                    NumberOfProducts = cartItemsID.Count(),
                    ShippingFee = ShippingFee
                };
                _context.Orders.Add(o);
                _context.SaveChanges();
                foreach(int cartItemID in cartItemsID){
                    Cart c = GetCartItemByID(cartItemID);
                    if (c == null) return STTCode.ForeignKeyIDNotFound;
                    if (c.UserID != userID) return STTCode.NoAccess;
                    Product p = _context.Products.FirstOrDefault(p => p.ID == c.ProductID);
                    if (p == null) return STTCode.ForeignKeyIDNotFound;
                    var od = new OrderDetail{
                        Description = c.Description,
                        OrderID = o.ID,
                        PricePerOne = p.PricePerOne,
                        ProductCount = c.ProductCount,
                        ProductID = c.ProductID
                    };
                    _context.OrderDetails.Add(od);
                    if (p.Count < c.ProductCount) return STTCode.ProductBuyAmountExcessProductCount;
                    p.Count -= c.ProductCount;
                    p.SoldQuantity += c.ProductCount;
                    _context.Remove(c);
                }
                o.Status = 1;
                _context.SaveChanges();
                return STTCode.Success;            
            }
            catch (Exception){
                return STTCode.ServerCodeException;
            }
        }
    }
}