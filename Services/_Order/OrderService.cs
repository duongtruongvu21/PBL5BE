using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Order
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        public OrderService(DataContext context)
        {
            _context = context;
        }
        public int CreateOrder(int userID)
        {
            if (_context.Users.Any(u => u.ID == userID)){
                var order = new Order(){
                    CreateBy = userID,
                    Status = 0,
                    CreateAt = DateTime.Now,
                    Address = ""
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order.ID;
            }
            else throw new Exception("not found");
        }

        public Order GetOrderByID(int id)
        {
            if (_context.Orders.Any(o => o.ID == id)){
                var order = _context.Orders.FirstOrDefault(o => o.ID == id);
                return order;
            }
            else throw new Exception("not found");
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

        public STTCode OrderPaid(OrderPaidDTO order)
        {
            try{
                var currentOrder = _context.Orders.FirstOrDefault(o => o.ID == order.ID);
                if (currentOrder == null) return STTCode.IDNotFound;
                // cập nhật Order
                currentOrder.Address = order.Address;
                currentOrder.Status = 1;
                currentOrder.CreateAt = DateTime.Now;
                _context.SaveChanges();
                // cập nhật số lượng bán và số lượng sản phẩm cho product
                List<OrderDetail> orderDetails = _context.OrderDetails.Where(od => od.OrderID == currentOrder.ID).ToList();
                foreach (OrderDetail od in orderDetails){
                    var product = _context.Products.FirstOrDefault(p => p.ID == od.ProductID);
                    product.Count -= od.ProductCount;
                    product.SoldQuantity += od.ProductCount;
                    _context.SaveChanges();
                }
                return STTCode.Success;
            }
            catch(Exception){
                return STTCode.ServerCodeException;
            }
        }

        public STTCode UpdateOrderStatus(OrderStatusUpdateDTO order)
        {
            try{
                var currentOrder = _context.Orders.FirstOrDefault(o => o.ID == order.ID);
                if (currentOrder == null) return STTCode.IDNotFound;
                currentOrder.Status = order.Status;
                _context.SaveChanges();
                return STTCode.Success;
            }
            catch(Exception){
                return STTCode.ServerCodeException;
            }
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
    }
}