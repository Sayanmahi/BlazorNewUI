using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food_Business.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<bool> EditCategory(Category category);
        Task<bool> AddCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
    }
}
