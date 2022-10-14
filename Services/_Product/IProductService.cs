using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._Category
{
    public interface IProductService
    {
        int CreateProduct(Product newProduct);
        int UpdateProduct(Product newProduct);
        int DeleteProduct(int id);
        List<Product> GetProducts();
        Product GetProductByID(int id);
    }
}