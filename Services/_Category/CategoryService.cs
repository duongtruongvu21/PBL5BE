using System.Security.Cryptography;
using System.Text;
using PBL5BE.API.Data;
using PBL5BE.API.Data.DTO;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Services._Category
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public int CreateCategory(Category newCategory)
        {
            try 
            {
                if(_context.Categories.Any(u => u.CategoryName.ToLower() == newCategory.CategoryName.ToLower()))
                {
                    return -1;
                }
                var newC = new Category(){
                    CategoryName = newCategory.CategoryName,
                    Status = 1
                };
                _context.Categories.Add(newC);
                _context.SaveChanges();
            } catch(Exception) 
            {
                return 0;
            }
            return 1;
        }
        public int UpdateCategory(Category newCategory)
        {
            try {
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newCategory.ID);
                if(currentCategory == null)  return -2;
                if(_context.Categories.Any(u => u.CategoryName.ToLower() == newCategory.CategoryName.ToLower()))
                {
                    return -1;
                }
                currentCategory.CategoryName = newCategory.CategoryName;
                _context.SaveChanges();
            } 
            catch(Exception) {
                return 0;
            }
            return 1;
        }
        // cần cải thiện hàm delete, xoá các record phụ thuộc ở các bảng khác
        public int DeleteCategory(Category dCategory){
            try{
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == dCategory.ID);
                if (currentCategory != null) _context.Categories.Remove(currentCategory);
                else return -2;
                _context.SaveChanges();
            }
            catch(Exception) {
                return 0;
            }
            return 1;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
