using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._Category
{
    public interface ICategoryService
    {
        int CreateCategory(Category categoryDTO);
        int UpdateCategory(Category categoryDTO);
        int DeleteCategory(int id);
        List<Category> GetCategories();
    }
}