using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;
using PBL5BE.API.Data.DTO;
namespace PBL5BE.API.Services._Category
{
    public interface ICategoryService
    {
        STTCode CreateCategory(CategoryCreateDTO newCategory);
        STTCode UpdateCategory(Category newCategory);
        STTCode DeleteCategory(int id);
        List<Category> GetCategories();
    }
}