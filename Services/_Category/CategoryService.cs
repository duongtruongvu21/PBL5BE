using PBL5BE.API.Data;
using PBL5BE.API.Data.Entities;
using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services._Category
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public STTCode CreateCategory(string categoryName)
        {
            try 
            {
                if(_context.Categories.Any(u => u.CategoryName.ToLower() == categoryName.ToLower()))
                {
                    return STTCode.Existed;
                }
                var newC = new Category(){
                    CategoryName = categoryName,
                    Status = 1
                };
                _context.Categories.Add(newC);
                _context.SaveChanges();
            } catch(Exception) 
            {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
        }
        public STTCode UpdateCategory(Category newCategory)
        {
            try {
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == newCategory.ID);
                if(currentCategory == null)  return STTCode.IDNotFound;
                if(_context.Categories.Any(u => u.CategoryName.ToLower() == newCategory.CategoryName.ToLower()))
                {
                    return STTCode.Existed;
                }
                currentCategory.CategoryName = newCategory.CategoryName;
                _context.SaveChanges();
            } 
            catch(Exception) {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
        }
        // cần cải thiện hàm delete, xoá các record phụ thuộc ở các bảng khác
        public STTCode DeleteCategory(int id){
            try{
                var currentCategory = _context.Categories.FirstOrDefault(u => u.ID == id);
                if (currentCategory != null) _context.Categories.Remove(currentCategory);
                else return STTCode.IDNotFound;
                _context.SaveChanges();
            }
            catch(Exception) {
                return STTCode.ServerCodeException;
            }
            return STTCode.Success;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
