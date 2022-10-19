using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Category
{
    public interface ICategoryService
    {
        STTCode CreateCategory(Category categoryDTO);
        STTCode UpdateCategory(Category categoryDTO);
        STTCode DeleteCategory(int id);
        List<Category> GetCategories();
    }
}