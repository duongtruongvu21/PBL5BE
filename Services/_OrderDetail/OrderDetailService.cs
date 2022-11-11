using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data;

namespace PBL5BE.API.Services._OrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext _context;
        public OrderDetailService(DataContext context)
        {
            _context = context;
        }
        public int CreateOrderDetail(OrderDetailCreateDTO orderDetail)
        {
            try{
                if (!_context.Orders.Any(o => o.ID == orderDetail.OrderID)) throw new Exception("Foreign key not found");
                if (!_context.Products.Any(p => p.ID == orderDetail.ProductID)) throw new Exception("Foreign key not found");
                var od = new OrderDetail(){
                    OrderID = orderDetail.OrderID,
                    Description = orderDetail.Description,
                    ProductCount = orderDetail.ProductCount,
                    ProductID = orderDetail.ProductID,
                    Total = orderDetail.Total
                };
                _context.OrderDetails.Add(od);
                _context.SaveChanges();
                return od.ID;
            }
            catch(Exception){
                throw new Exception("exception");
            }
        }

        public STTCode DeleteOrderDetail(int id)
        {
            try{
                var od = _context.OrderDetails.FirstOrDefault(o => o.ID == id);
                if (od == null) return STTCode.IDNotFound;
                var order = _context.Orders.FirstOrDefault(o => o.ID == od.OrderID);
                if (order.Status != 0) return STTCode.OrderIsPaid;
                _context.OrderDetails.Remove(od);
                _context.SaveChanges();
                return STTCode.Success;
            }
            catch(Exception){
                return STTCode.ServerCodeException;
            }
        }

        public OrderDetail GetOrderDetailByID(int id)
        {
            var od = _context.OrderDetails.FirstOrDefault(o => o.ID == id);
            if (od == null) throw new Exception("not found");
            return od;
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            if (!_context.Orders.Any(o => o.ID == orderID)) throw new Exception("not found");
            return _context.OrderDetails.Where(od => od.OrderID == orderID).OrderByDescending(od => od.ID).ToList();
        }

        public STTCode UpdateOrderDetail(OrderDetailUpdateDTO orderDetail)
        {
            try{
                var od = _context.OrderDetails.FirstOrDefault(o => o.ID == orderDetail.ID);
                if (od == null) return STTCode.IDNotFound;
                od.Description = orderDetail.Description;
                od.ProductCount = orderDetail.ProductCount;
                od.Total = orderDetail.Total;
                _context.SaveChanges();
                return STTCode.Success;
            }
            catch(Exception){
                return STTCode.ServerCodeException;
            }
        }
    }
}