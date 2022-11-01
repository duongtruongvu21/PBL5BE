using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;

namespace PBL5BE.API.Services._Category
{
    public interface IProductService
    {
        STTCode CreateProduct(ProductDTO newProduct, int userId);
        STTCode UpdateProduct(ProductDTO newProduct);
        STTCode DeleteProduct(int id);
        List<Product> GetProducts(int categoryId, string productName, byte status, int recordQuantity);
        Product GetProductByID(int id);
    }
}