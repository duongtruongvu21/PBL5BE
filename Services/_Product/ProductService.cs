using System.Security.Cryptography;
using System.Text;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._Category
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public int CreateProduct(Product newProduct)
        {
            try {
                // check có trùng tên hay không
                if(_context.Products.Any(u => u.ProductName.ToLower() == newProduct.ProductName.ToLower()))
                {
                    return -1;
                }
                // check category có tồn tại hay không
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newProduct.CategoryID);
                if (currentCategory == null)  return -2;
                // check xem người tạo có tồn tại hay không
                var currentUser = _context.Users.FirstOrDefault(u => u.ID == newProduct.CreateBy);
                if (currentUser == null)  return -2;

                var newP = new Product(){
                    CategoryID = newProduct.CategoryID,
                    CreateBy = newProduct.CreateBy,
                    ProductName = newProduct.ProductName,
                    Description = newProduct.Description,
                    Count = newProduct.Count,
                    PricePerOne = newProduct.PricePerOne,
                    Status = 1,
                    isReviewed = false,
                    CreateAt = DateTime.Now
                };
                _context.Products.Add(newP);
                _context.SaveChanges();
            } 
            catch(Exception) {
                return 0;
            }
            return 1;
        }
        // cần cải thiện hàm delete, xoá các record phụ thuộc ở các bảng khác
        public int DeleteProduct(int id)
        {
            try {
                var product = _context.Products.FirstOrDefault(u => u.ID == id);
                if (product == null) return -2;
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch(Exception) {
                return 0;
            }
            return 1;
        }

        public Product GetProductByID(int id)
        {
            var product = _context.Products.FirstOrDefault(u => u.ID == id);
            if (product == null) throw new Exception("Khong ton tai");
            else return product;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public int UpdateProduct(Product newProduct)
        {
            try {
                // check xem product có tồn tại hay không
                var currentProduct = _context.Products.FirstOrDefault(u => u.ID == newProduct.ID);
                if (currentProduct == null) return -1;
                // check category có tồn tại hay không
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newProduct.CategoryID);
                if (currentCategory == null)  return -2;
                // check xem người tạo có tồn tại hay không
                // var currentUser = _context.Users.FirstOrDefault(u => u.ID == newProduct.CreateBy);
                // if (currentUser == null)  return -2;
                // không sửa CreateBy, CreateAt và ID
                currentProduct.CategoryID = newProduct.CategoryID;
                currentProduct.ProductName = newProduct.ProductName;
                currentProduct.Description = newProduct.Description;
                currentProduct.Count = newProduct.Count;
                currentProduct.PricePerOne = newProduct.PricePerOne;
                currentProduct.Status = 1;
                currentProduct.isReviewed = false;
                _context.SaveChanges();
            } 
            catch(Exception) {
                return 0;
            }
            return 1;
        }
    }
}