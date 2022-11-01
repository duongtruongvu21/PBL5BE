using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
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

        public STTCode CreateProduct(ProductDTO newProduct, int userId)
        {
            try {
                // check có trùng tên hay không
                if(_context.Products.Any(u => u.ProductName.ToLower() == newProduct.ProductName.ToLower() && u.Status != 0))
                    return STTCode.Existed;
                // check category có tồn tại hay không
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newProduct.CategoryID);
                if (currentCategory == null)  return STTCode.ForeignKeyIDNotFound;
                // check xem người tạo có tồn tại hay không
                var currentUser = _context.Users.FirstOrDefault(u => u.ID == userId);
                if (currentUser == null)  return STTCode.ForeignKeyIDNotFound;

                var newP = new Product(){
                    CategoryID = newProduct.CategoryID,
                    CreateBy = userId,
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
                product.Status = 0;
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

        public List<Product> GetProducts(int categoryId, string productName, byte status, int recordQuantity)
        {
            if (categoryId < 1)
                return _context.Products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower())
                && p.Status == status).OrderByDescending(p => p.ID).Take(recordQuantity).ToList();
            else
                return _context.Products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower())
                && p.Status == status 
                && p.CategoryID == categoryId).OrderByDescending(p => p.ID).Take(recordQuantity).ToList();
        }

        public STTCode UpdateProduct(ProductDTO newProduct)
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
                    if(_context.Products.Any(u => u.ProductName.ToLower() == newProduct.ProductName.ToLower() && u.Status != 0))
                        return STTCode.Existed;
                }
                currentProduct.CategoryID = newProduct.CategoryID;
                currentProduct.ProductName = newProduct.ProductName;
                currentProduct.Description = newProduct.Description;
                currentProduct.Count = newProduct.Count;
                currentProduct.PricePerOne = newProduct.PricePerOne;
                currentProduct.Status = newProduct.Status;
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
