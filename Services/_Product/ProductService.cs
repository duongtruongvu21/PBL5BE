using PBL5BE.API.Data;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Category
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public STTCode CreateProduct(Product newProduct)
        {
            try {
                // check có trùng tên hay không
                if(_context.Products.Any(u => u.ProductName.ToLower() == newProduct.ProductName.ToLower()))
                    return STTCode.Existed;
                // check category có tồn tại hay không
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newProduct.CategoryID);
                if (currentCategory == null)  return STTCode.ForeignKeyIDNotFound;
                // check xem người tạo có tồn tại hay không
                var currentUser = _context.Users.FirstOrDefault(u => u.ID == newProduct.CreateBy);
                if (currentUser == null)  return STTCode.ForeignKeyIDNotFound;

                var newP = new Product(){
                    CategoryID = newProduct.CategoryID,
                    CreateBy = newProduct.CreateBy,
                    ProductName = newProduct.ProductName,
                    Description = newProduct.Description,
                    Count = newProduct.Count,
                    PricePerOne = newProduct.PricePerOne,
                    Status = 1,
                    isReviewed = false,
                    PictureURL = newProduct.PictureURL,
                    CreateAt = DateTime.Now
                };
                _context.Products.Add(newP);
                _context.SaveChanges();
            } 
            catch(Exception) {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
        }
        // cần cải thiện hàm delete, xoá các record phụ thuộc ở các bảng khác
        public STTCode DeleteProduct(int id)
        {
            try {
                var product = _context.Products.FirstOrDefault(u => u.ID == id);
                if (product == null) return STTCode.IDNotFound;
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch(Exception) {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
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

        public STTCode UpdateProduct(Product newProduct)
        {
            try {
                // check xem product có tồn tại hay không
                var currentProduct = _context.Products.FirstOrDefault(u => u.ID == newProduct.ID);
                if (currentProduct == null) return STTCode.IDNotFound;
                // check category có tồn tại hay không
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newProduct.CategoryID);
                if (currentCategory == null)  return STTCode.ForeignKeyIDNotFound;
                // check có trùng tên hay không
                if (currentProduct.ProductName.ToLower() != newProduct.ProductName.ToLower()){
                    if(_context.Products.Any(u => u.ProductName.ToLower() == newProduct.ProductName.ToLower()))
                        return STTCode.Existed;
                }
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
                currentProduct.PictureURL = newProduct.PictureURL;
                _context.SaveChanges();
            } 
            catch(Exception) {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
        }
    }
}
