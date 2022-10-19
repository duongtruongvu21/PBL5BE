using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Category
{
    public interface IProductService
    {
        STTCode CreateProduct(Product newProduct);
        STTCode UpdateProduct(Product newProduct);
        STTCode DeleteProduct(int id);
        List<Product> GetProducts();
        Product GetProductByID(int id);
    }
}